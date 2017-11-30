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
    class OpenCLLbryMiner : OpenCLMiner
    {
        static Mutex mProgramArrayMutex = new Mutex();

        static Dictionary<ProgramArrayIndex, ComputeProgram> mLbryProgramArray = new Dictionary<ProgramArrayIndex, ComputeProgram>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLbrySearchKernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        ComputeProgram mLbryProgram;
        ComputeKernel mLbrySearchKernel = null;            
        private ComputeBuffer<byte> mLbryInputBuffer = null;
        private ComputeBuffer<UInt32> mLbryOutputBuffer = null;
        private LbryStratum mLbryStratum;
        long[] mLbryGlobalWorkSizeArray = new long[] { 0 };
        long[] mLbryLocalWorkSizeArray = new long[] { 0 };
        long[] mLbryGlobalWorkOffsetArray = new long[] { 0 };
        private static readonly int lbryOutputSize = 256 + 255 * 8;
        UInt32[] mLbryOutput = new UInt32[lbryOutputSize];
        byte[] mLbryInput = new byte[112];
            


        public OpenCLLbryMiner(Device aGatelessGateDevice)
            : base(aGatelessGateDevice, "Lbry")
        {
            mLbryInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 112);
            mLbryOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, lbryOutputSize);
        }

        public void Start(LbryStratum aLbryStratum, int aLbryIntensity, int aLbryLocalWorkSize)
        {
            mLbryStratum = aLbryStratum;
            mLbryGlobalWorkSizeArray[0] = aLbryIntensity * Device.MaxComputeUnits;
            mLbryLocalWorkSizeArray[0] = aLbryLocalWorkSize;
            if (mLbryGlobalWorkSizeArray[0] % aLbryLocalWorkSize != 0)
                mLbryGlobalWorkSizeArray[0] = aLbryLocalWorkSize - mLbryGlobalWorkSizeArray[0] % aLbryLocalWorkSize;

            base.Start();
        }

        public void BuildLbryProgram()
        {
            ComputeDevice computeDevice = Device.GetComputeDevice();

            try { mProgramArrayMutex.WaitOne(5000); } catch (Exception) { }

            if (mLbryProgramArray.ContainsKey(new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])))
            {
                mLbryProgram = mLbryProgramArray[new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])];
                mLbrySearchKernel = mLbrySearchKernelArray[new ProgramArrayIndex(DeviceIndex, mLbryLocalWorkSizeArray[0])];
            }
            else
            {
                String source = System.IO.File.ReadAllText(@"Kernels\lbry.cl");
                mLbryProgram = new ComputeProgram(Context, source);
                MainForm.Logger("Loaded Lbry program for Device #" + DeviceIndex + ".");
                String buildOptions = (Device.Vendor == "AMD" ? "-O1 " : //"-O1 " :
                                       Device.Vendor == "NVIDIA" ? "" : //"-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + mLbryLocalWorkSizeArray[0];
                try
                {
                    mLbryProgram.Build(Device.DeviceList, buildOptions, null, IntPtr.Zero);
                }
                catch (Exception)
                {
                    MainForm.Logger(mLbryProgram.GetBuildLog(computeDevice));
                    throw;
                }
                MainForm.Logger("Built Lbry program for Device #" + DeviceIndex + ".");
                MainForm.Logger("Built options: " + buildOptions);
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

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

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

                    // Wait for the first lbryJob to arrive.
                    int elapsedTime = 0;
                    while ((mLbryStratum == null || mLbryStratum.GetJob() == null) && elapsedTime < 5000)
                    {
                        Thread.Sleep(10);
                        elapsedTime += 10;
                    }
                    if (mLbryStratum == null || mLbryStratum.GetJob() == null)
                        throw new TimeoutException("Stratum server failed to send a new lbryJob.");

                    System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                    LbryStratum.Work lbryWork;

                    while (!Stopped && (lbryWork = mLbryStratum.GetWork()) != null)
                    {
                        MarkAsAlive();

                        var lbryJob = lbryWork.Job;
                        Array.Copy(lbryWork.Blob, mLbryInput, 112);
                        UInt32 lbryStartNonce = (UInt32)(r.Next(0, int.MaxValue));
                        UInt64 lbryTarget = (UInt64) ((double) 0xffff0000UL / (mLbryStratum.Difficulty / 256));
                        mLbrySearchKernel.SetValueArgument<UInt64>(3, lbryTarget);
                        Queue.Write<byte>(mLbryInputBuffer, true, 0, 112, (IntPtr)lbryInputPtr, null);

                        consoleUpdateStopwatch.Start();

                        while (!Stopped && mLbryStratum.GetJob().Equals(lbryJob))
                        {
                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();

                            MarkAsAlive();

                            mLbrySearchKernel.SetValueArgument<UInt32>(2, lbryStartNonce);

                            // Get a new local extranonce if necessary.
                            if (0xffffffffu - lbryStartNonce < (UInt32)mLbryGlobalWorkSizeArray[0])
                                break;

                            mLbryOutput[255] = 0; // mLbryOutput[255] is used as an atomic counter.
                            Queue.Write<UInt32>(mLbryOutputBuffer, true, 0, lbryOutputSize, (IntPtr)lbryOutputPtr, null);
                            Queue.Execute(mLbrySearchKernel, mLbryGlobalWorkOffsetArray, mLbryGlobalWorkSizeArray, mLbryLocalWorkSizeArray, null);
                            Queue.Read<UInt32>(mLbryOutputBuffer, true, 0, lbryOutputSize, (IntPtr)lbryOutputPtr, null);
                            if (mLbryStratum.GetJob().Equals(lbryJob))
                            {
                                for (int i = 0; i < mLbryOutput[255]; ++i)
                                {
                                    String result = "";
                                    for (int j = 0; j < 8; ++j)
                                    {
                                        UInt32 word = mLbryOutput[256 + i * 8 + j];
                                        result += String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((word >> 0) & 0xff), ((word >> 8) & 0xff), ((word >> 16) & 0xff), ((word >> 24) & 0xff));
                                    }
                                    mLbryStratum.Submit(GatelessGateDevice, lbryWork, mLbryOutput[i], result);
                                }
                            }
                            lbryStartNonce += (UInt32)mLbryGlobalWorkSizeArray[0];

                            sw.Stop();
                            Speed = ((double)mLbryGlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                            if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                            {
                                MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Mh/s (Lbry)", Speed / 1000000));
                                consoleUpdateStopwatch.Restart();
                            }
                        }
                    }
                } catch (Exception ex) {
                    MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                    MainForm.Logger("Restarting miner thread...");
                }

                Speed = 0;

                if (!Stopped)
                    System.Threading.Thread.Sleep(5000);
            }
            MarkAsDone();
        }
    }
}

