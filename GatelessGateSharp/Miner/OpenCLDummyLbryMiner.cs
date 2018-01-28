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

        long[] mLbryGlobalWorkSizeArray = new long[] { 0 };
        long[] mLbryLocalWorkSizeArray = new long[] { 0 };
        long[] mLbryGlobalWorkOffsetArray = new long[] { 0 };
        private static readonly int lbryOutputSize = 256 + 255 * 8;
        UInt32[] mLbryOutput = new UInt32[lbryOutputSize];
        byte[] mLbryInput = new byte[112];
        private int mIterations = 1;


        public OpenCLDummyLbryMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "lbry")
        {
            mIterations = (aGatelessGateDevice.GetVendor() == "NVIDIA") ? 8 : 1;
        }

        public void Start(int aLbryIntensity = 256, int aLbryLocalWorkSize = 256)
        {
            mLbryGlobalWorkSizeArray[0] = aLbryIntensity * OpenCLDevice.GetMaxComputeUnits() * aLbryLocalWorkSize;
            mLbryLocalWorkSizeArray[0] = aLbryLocalWorkSize;

            base.Start();
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread()
        {
            ComputeProgram program = null;
            try { 
                ComputeDevice computeDevice = OpenCLDevice.GetComputeDevice();
                Random r = new Random();
            
                MarkAsAlive();

                //MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

                program = BuildProgram(
                    "lbry",
                    mLbryLocalWorkSizeArray[0],
                    "-O1 -DITERATIONS=" + mIterations,
                    "-DITERATIONS=" + mIterations,
                    "-DITERATIONS=" + mIterations);

                using (var mLbryInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 112))
                using (var mLbryOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, lbryOutputSize))
                using (var mLbrySearchKernel = program.CreateKernel("search_combined"))
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
            } catch (UnrecoverableException ex) {
                this.UnrecoverableException = ex;
            } catch (Exception ex) {
                this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
            } finally {
                MarkAsDone();
                MemoryUsage = 0;
                if (program != null)
                    program.Dispose();
                program = null;
            }
        }
    }
}

