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
        long[] mLbryGlobalWorkSizeArray = new long[] { 0 };
        long[] mLbryLocalWorkSizeArray = new long[] { 0 };
        long[] mLbryGlobalWorkOffsetArray = new long[] { 0 };
        private static readonly int lbryOutputSize = 256 + 255 * 8;
        UInt32[] mLbryOutput = new UInt32[lbryOutputSize];
        byte[] mLbryInput = new byte[112];
        private int mIterations = 1;



        public OpenCLLbryMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "lbry")
        {
        }

        public void Start(LbryStratum aLbryStratum, int aLbryIntensity, int aLbryLocalWorkSize)
        {
            Stratum = aLbryStratum;
            mLbryGlobalWorkSizeArray[0] = aLbryIntensity * OpenCLDevice.GetMaxComputeUnits() * aLbryLocalWorkSize;
            mLbryLocalWorkSizeArray[0] = aLbryLocalWorkSize;

            base.Start();
        }

        public LbryStratum Stratum { get; set; }

        public override void SetPrimaryStratum(Stratum stratum) {
            Stratum = (LbryStratum)stratum;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread()
        {
            ComputeProgram program = null;

            try {
                Random r = new Random();
            
                MarkAsAlive();

                MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

                program = BuildProgram(
                    "lbry",
                    mLbryLocalWorkSizeArray[0], 
                    "-O1 -DITERATIONS=" + mIterations,
                    "-DITERATIONS=" + mIterations,
                    "-DITERATIONS=" + mIterations);
                
                using (var lbrySearchKernel = program.CreateKernel("search_combined"))
                using (var lbryInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 112))
                using (var lbryOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, lbryOutputSize))
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
                        lbrySearchKernel.SetMemoryArgument(0, lbryInputBuffer);
                        lbrySearchKernel.SetMemoryArgument(1, lbryOutputBuffer);

                        // Wait for the first lbryJob to arrive.
                        int elapsedTime = 0;
                        while ((Stratum == null || Stratum.GetJob() == null) && elapsedTime < 60000) {
                            Thread.Sleep(100);
                            elapsedTime += 100;
                        }
                        if (Stratum == null || Stratum.GetJob() == null)
                            throw new TimeoutException("Stratum server failed to send a new lbryJob.");

                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        LbryStratum.Work lbryWork;
                        LbryStratum.Job lbryJob;

                        while (!Stopped && (lbryWork = Stratum.GetWork()) != null && (lbryJob = lbryWork.Job) != null)
                        {
                            MarkAsAlive();

                            Array.Copy(lbryWork.Blob, mLbryInput, 112);
                            UInt32 lbryStartNonce = (UInt32)(r.Next(0, int.MaxValue));
                            UInt64 lbryTarget = (UInt64) ((double) 0xffff0000UL / (Stratum.Difficulty / 256));
                            lbrySearchKernel.SetValueArgument<UInt64>(3, lbryTarget);
                            Queue.Write<byte>(lbryInputBuffer, true, 0, 112, (IntPtr)lbryInputPtr, null);

                            consoleUpdateStopwatch.Start();

                            while (!Stopped && Stratum.GetJob() != null && Stratum.GetJob().Equals(lbryJob))
                            {
                                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                sw.Start();

                                MarkAsAlive();

                                lbrySearchKernel.SetValueArgument<UInt32>(2, lbryStartNonce);

                                // Get a new local extranonce if necessary.
                                if (0xffffffffu - lbryStartNonce < (UInt32)mLbryGlobalWorkSizeArray[0] * mIterations)
                                    break;

                                mLbryOutput[255] = 0; // mLbryOutput[255] is used as an atomic counter.
                                Queue.Write<UInt32>(lbryOutputBuffer, true, 0, lbryOutputSize, (IntPtr)lbryOutputPtr, null);
                                Queue.Execute(lbrySearchKernel, mLbryGlobalWorkOffsetArray, mLbryGlobalWorkSizeArray, mLbryLocalWorkSizeArray, null);
                                Queue.Read<UInt32>(lbryOutputBuffer, true, 0, lbryOutputSize, (IntPtr)lbryOutputPtr, null);
                                if (Stratum.GetJob() != null && Stratum.GetJob().Equals(lbryJob))
                                {
                                    for (int i = 0; i < mLbryOutput[255]; ++i)
                                    {
                                        String result = "";
                                        for (int j = 0; j < 8; ++j)
                                        {
                                            UInt32 word = mLbryOutput[256 + i * 8 + j];
                                            result += String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((word >> 0) & 0xff), ((word >> 8) & 0xff), ((word >> 16) & 0xff), ((word >> 24) & 0xff));
                                        }
                                        Stratum.Submit(GatelessGateDevice, lbryWork, mLbryOutput[i], result);
                                    }
                                }
                                lbryStartNonce += (UInt32)mLbryGlobalWorkSizeArray[0] * (uint)mIterations;

                                sw.Stop();
                                Speed = ((double)mLbryGlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds * mIterations;
                                if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                                {
                                    MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Mh/s (Lbry)", Speed / 1000000));
                                    consoleUpdateStopwatch.Restart();
                                }
                            }
                        }
                    } catch (Exception ex) {
                        MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                        if (UnrecoverableException.IsUnrecoverableException(ex)) {
                            this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
                            Stop();
                        }
                    }

                    Speed = 0;

                    if (!Stopped) {
                        MainForm.Logger("Restarting miner thread...");
                        System.Threading.Thread.Sleep(5000);
                    }
                }
            } catch (UnrecoverableException ex) {
                this.UnrecoverableException = ex;
            } catch (Exception ex) {
                this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
            } finally {
                MarkAsDone();
                MemoryUsage = 0;
                if (program != null) { program.Dispose(); program = null; }
            }
        }
    }
}

