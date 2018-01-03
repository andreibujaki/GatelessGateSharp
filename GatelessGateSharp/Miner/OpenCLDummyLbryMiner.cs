// Copyright 2017 Yurio Miyazawa (a.k.a zawawa) <me@yurio.net>
//
// This file is part of Gateless Gate Sharp.
//
// Gateless Gate Sharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Gateless Gate Sharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Gateless Gate Sharp.  If not, see <http://www.gnu.org/licenses/>.



using System;
using System.Collections.Generic;
using System.Threading;
using Cloo;



namespace GatelessGateSharp
{
    class OpenCLDummyLbryMiner : OpenCLMiner
    {
        static Mutex mProgramArrayMutex = new Mutex();

        static Dictionary<ProgramArrayIndex, ComputeProgram> mLbryProgramArray = new Dictionary<ProgramArrayIndex, ComputeProgram>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLbrySearchKernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        ComputeProgram mLbryProgram;
        ComputeKernel mLbrySearchKernel = null;            
        private ComputeBuffer<byte> mLbryInputBuffer = null;
        private ComputeBuffer<UInt32> mLbryOutputBuffer = null;
        long[] mLbryGlobalWorkSizeArray = new long[] { 0 };
        long[] mLbryLocalWorkSizeArray = new long[] { 0 };
        long[] mLbryGlobalWorkOffsetArray = new long[] { 0 };
        private static readonly int lbryOutputSize = 256 + 255 * 8;
        UInt32[] mLbryOutput = new UInt32[lbryOutputSize];
        byte[] mLbryInput = new byte[112];
        private int mIterations = 1;


        public OpenCLDummyLbryMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "Lbry")
        {
            mLbryInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 112);
            mLbryOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, lbryOutputSize);
            mIterations = (aGatelessGateDevice.GetVendor() == "NVIDIA") ? 8 : 1;
        }

        public void Start(int aLbryIntensity = 1, int aLbryLocalWorkSize = 256)
        {
            mLbryGlobalWorkSizeArray[0] = aLbryIntensity * OpenCLDevice.GetMaxComputeUnits();
            mLbryLocalWorkSizeArray[0] = aLbryLocalWorkSize;
            if (mLbryGlobalWorkSizeArray[0] % aLbryLocalWorkSize != 0)
                mLbryGlobalWorkSizeArray[0] = aLbryLocalWorkSize - mLbryGlobalWorkSizeArray[0] % aLbryLocalWorkSize;

            base.Start();
        }

        public void BuildLbryProgram()
        {
            ComputeDevice computeDevice = OpenCLDevice.GetComputeDevice();

            try { mProgramArrayMutex.WaitOne(5000); } catch (Exception) { }

            if (mLbryProgramArray.ContainsKey(new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])))
            {
                mLbryProgram = mLbryProgramArray[new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])];
                mLbrySearchKernel = mLbrySearchKernelArray[new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])];
            }
            else
            {
                try
                {
                    if (mLbryLocalWorkSizeArray[0] != 256)
                        throw new Exception("No suitable binary file was found.");
                    string fileName = @"BinaryKernels\" + computeDevice.Name + "_lbry.bin";
                    byte[] binary = System.IO.File.ReadAllBytes(fileName);
                    mLbryProgram = new ComputeProgram(Context, new List<byte[]>() { binary }, new List<ComputeDevice>() { computeDevice });
                    //MainForm.Logger("Loaded " + fileName + " for Device #" + DeviceIndex + ".");
                }
                catch (Exception)
                {
                    String source = System.IO.File.ReadAllText(@"Kernels\lbry.cl");
                    mLbryProgram = new ComputeProgram(Context, source);
                    //MainForm.Logger(@"Loaded Kernels\lbry.cl for Device #" + DeviceIndex + ".");
                }
                String buildOptions = (OpenCLDevice.GetVendor() == "AMD" ? "-O1 " : //"-O1 " :
                                       OpenCLDevice.GetVendor() == "NVIDIA" ? "" : //"-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + mLbryLocalWorkSizeArray[0] + " -DITERATIONS=" + mIterations;
                try
                {
                    mLbryProgram.Build(OpenCLDevice.DeviceList, buildOptions, null, IntPtr.Zero);
                }
                catch (Exception)
                {
                    MainForm.Logger(mLbryProgram.GetBuildLog(computeDevice));
                    throw;
                }
                //MainForm.Logger("Built Lbry program for Device #" + DeviceIndex + ".");
                //MainForm.Logger("Build options: " + buildOptions);
                mLbryProgramArray[new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])] = mLbryProgram;
                mLbrySearchKernelArray[new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])] = mLbrySearchKernel = mLbryProgram.CreateKernel("search_combined");
            }

            try { mProgramArrayMutex.ReleaseMutex(); } catch (Exception) { }
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread()
        {
            Random r = new Random();
            
            MarkAsAlive();

            //MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

            BuildLbryProgram();

            fixed (long* lbryGlobalWorkOffsetArrayPtr = mLbryGlobalWorkOffsetArray)
            fixed (long* lbryGlobalWorkSizeArrayPtr = mLbryGlobalWorkSizeArray)
            fixed (long* lbryLocalWorkSizeArrayPtr = mLbryLocalWorkSizeArray)
            fixed (byte* lbryInputPtr = mLbryInput)
            fixed (UInt32* lbryOutputPtr = mLbryOutput)
            while (!Stopped)
            {
                MarkAsAlive();

                try
                {
                    mLbrySearchKernel.SetMemoryArgument(0, mLbryInputBuffer);
                    mLbrySearchKernel.SetMemoryArgument(1, mLbryOutputBuffer);

                    while (!Stopped)
                    {
                        MarkAsAlive();

                        mLbryInput = new byte[112];
                        UInt32 lbryStartNonce = (UInt32)(r.Next(0, int.MaxValue));
                        UInt64 lbryTarget = (UInt64) ((double) 0xffff0000UL / (256 / 256));
                        mLbrySearchKernel.SetValueArgument<UInt64>(3, lbryTarget);
                        Queue.Write<byte>(mLbryInputBuffer, true, 0, 112, (IntPtr)lbryInputPtr, null);

                        while (!Stopped)
                        {
                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();

                            MarkAsAlive();

                            mLbrySearchKernel.SetValueArgument<UInt32>(2, lbryStartNonce);

                            // Get a new local extranonce if necessary.
                            if (0xffffffffu - lbryStartNonce < (UInt32)mLbryGlobalWorkSizeArray[0] * mIterations)
                                break;

                            mLbryOutput[255] = 0; // mLbryOutput[255] is used as an atomic counter.
                            Queue.Write<UInt32>(mLbryOutputBuffer, true, 0, lbryOutputSize, (IntPtr)lbryOutputPtr, null);
                            Queue.Execute(mLbrySearchKernel, mLbryGlobalWorkOffsetArray, mLbryGlobalWorkSizeArray, mLbryLocalWorkSizeArray, null);
                            Queue.Read<UInt32>(mLbryOutputBuffer, true, 0, lbryOutputSize, (IntPtr)lbryOutputPtr, null);
                            lbryStartNonce += (UInt32)mLbryGlobalWorkSizeArray[0] * (uint)mIterations;
                        }
                    }
                } catch (Exception ex) {
                    MainForm.Logger("Exception in dummy miner thread: " + ex.Message + ex.StackTrace);
                    MainForm.Logger("Restarting dummy miner thread...");
                }
            }

            mLbryInputBuffer.Dispose();
            mLbryOutputBuffer.Dispose();
            //foreach (var pair in mLbrySearchKernelArray)
            //    pair.Value.Dispose();
            foreach (var pair in mLbryProgramArray)
                pair.Value.Dispose();

            MarkAsDone();
        }
    }
}

