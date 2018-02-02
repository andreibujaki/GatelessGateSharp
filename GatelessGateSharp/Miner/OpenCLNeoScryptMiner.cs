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
    class OpenCLNeoScryptMiner : OpenCLMiner
    {
        public static readonly int sNeoScryptInputSize = 80;
        public static readonly int sNeoScryptOutputSize = 256;

        long[] mNeoScryptGlobalWorkSizeArray = new long[] { 0 };
        long[] mNeoScryptLocalWorkSizeArray = new long[] { 0 };
        long[] mNeoScryptGlobalWorkOffsetArray = new long[] { 0 };
        UInt32[] mNeoScryptOutput = new UInt32[sNeoScryptOutputSize];
        byte[] mNeoScryptInput = new byte[sNeoScryptInputSize];
        
        public OpenCLNeoScryptMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "neoscrypt") {
        }

        public void Start(NeoScryptStratum aNeoScryptStratum, int aNeoScryptRawIntensity, int aNeoScryptLocalWorkSize) {
            Stratum = aNeoScryptStratum;
            mNeoScryptGlobalWorkSizeArray[0] = aNeoScryptRawIntensity * aNeoScryptLocalWorkSize;
            mNeoScryptLocalWorkSizeArray[0] = aNeoScryptLocalWorkSize;

            base.Start();
        }

        public NeoScryptStratum Stratum { get; set; }        
        
        public override void SetPrimaryStratum(Stratum stratum) {
            Stratum = (NeoScryptStratum)stratum;
        }
        
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread() {
            ComputeProgram program = null;
            ComputeBuffer<byte> neoScryptGlobalCacheBuffer = null;
            
            try {
                Random r = new Random();

                MarkAsAlive();

                MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

                program = BuildProgram("neoscrypt", mNeoScryptLocalWorkSizeArray[0], "-O5 -legacy", "", "");

                neoScryptGlobalCacheBuffer = OpenCLDevice.RequestComputeByteBuffer(ComputeMemoryFlags.ReadWrite, (mNeoScryptGlobalWorkSizeArray[0] * 32768));
                MemoryUsage = sNeoScryptInputSize + sNeoScryptOutputSize + neoScryptGlobalCacheBuffer.Size;

                using (var neoScryptSearchKernel = program.CreateKernel("search"))
                using (var neoScryptInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, sNeoScryptInputSize))
                using (var neoScryptOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, sNeoScryptOutputSize))
                fixed (long* neoscryptGlobalWorkOffsetArrayPtr = mNeoScryptGlobalWorkOffsetArray)
                fixed (long* neoscryptGlobalWorkSizeArrayPtr = mNeoScryptGlobalWorkSizeArray)
                fixed (long* neoscryptLocalWorkSizeArrayPtr = mNeoScryptLocalWorkSizeArray)
                fixed (byte* neoscryptInputPtr = mNeoScryptInput)
                fixed (UInt32* neoscryptOutputPtr = mNeoScryptOutput)
                while (!Stopped) {
                    MarkAsAlive();

                    try {
                        neoScryptSearchKernel.SetMemoryArgument(0, neoScryptInputBuffer);
                        neoScryptSearchKernel.SetMemoryArgument(1, neoScryptOutputBuffer);
                        neoScryptSearchKernel.SetMemoryArgument(2, neoScryptGlobalCacheBuffer);

                        // Wait for the first NeoScryptJob to arrive.
                        int elapsedTime = 0;
                        while ((Stratum == null || Stratum.GetJob() == null) && elapsedTime < Parameters.TimeoutForFirstJobInMilliseconds && !Stopped) {
                            Thread.Sleep(100);
                            elapsedTime += 100;
                        }
                        if (Stratum == null || Stratum.GetJob() == null)
                            throw new TimeoutException("Stratum server failed to send a new job.");

                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        NeoScryptStratum.Work neoscryptWork;
                        NeoScryptStratum.Job neoscryptJob;

                        while (!Stopped && (neoscryptWork = Stratum.GetWork()) != null && (neoscryptJob = neoscryptWork.Job) != null) {
                            MarkAsAlive();

                            Array.Copy(neoscryptWork.Blob, mNeoScryptInput, sNeoScryptInputSize);
                            UInt32 neoscryptStartNonce = (UInt32)(r.Next(0, int.MaxValue));
                            Queue.Write<byte>(neoScryptInputBuffer, true, 0, sNeoScryptInputSize, (IntPtr)neoscryptInputPtr, null);

                            consoleUpdateStopwatch.Start();

                            while (!Stopped && Stratum.GetJob() != null && Stratum.GetJob().Equals(neoscryptJob)) {
                                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                sw.Start();

                                MarkAsAlive();

                                UInt32 NeoScryptTarget = (UInt32)((double)0xffff0000U / (Stratum.Difficulty * 65536));
                                neoScryptSearchKernel.SetValueArgument<UInt32>(3, NeoScryptTarget);
                                mNeoScryptGlobalWorkOffsetArray[0] = neoscryptStartNonce;

                                // Get a new local extranonce if necessary.
                                if (0xffffffffu - neoscryptStartNonce < (UInt32)mNeoScryptGlobalWorkSizeArray[0])
                                    break;

                                mNeoScryptOutput[255] = 0; // mNeoScryptOutput[255] is used as an atomic counter.
                                Queue.Write<UInt32>(neoScryptOutputBuffer, true, 0, sNeoScryptOutputSize, (IntPtr)neoscryptOutputPtr, null);
                                Queue.Execute(neoScryptSearchKernel, mNeoScryptGlobalWorkOffsetArray, mNeoScryptGlobalWorkSizeArray, mNeoScryptLocalWorkSizeArray, null);
                                Queue.Read<UInt32>(neoScryptOutputBuffer, true, 0, sNeoScryptOutputSize, (IntPtr)neoscryptOutputPtr, null);
                                if (Stratum.GetJob() != null && Stratum.GetJob().Equals(neoscryptJob)) {
                                    for (int i = 0; i < mNeoScryptOutput[255]; ++i)
                                        Stratum.Submit(Device, neoscryptWork, mNeoScryptOutput[i]);
                                }
                                neoscryptStartNonce += (UInt32)mNeoScryptGlobalWorkSizeArray[0];

                                sw.Stop();
                                Speed = ((double)mNeoScryptGlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                                Device.TotalHashesPrimaryAlgorithm += (double)mNeoScryptGlobalWorkSizeArray[0];
                                if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000) {
                                    MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Kh/s (NeoScrypt)", Speed / 1000));
                                    consoleUpdateStopwatch.Restart();
                                }
                            }
                        }
                    } catch (Exception ex) {
                        MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                        if (UnrecoverableException.IsUnrecoverableException(ex)) {
                            this.UnrecoverableException = new UnrecoverableException(ex, Device);
                            Stop();
                        }
                    }

                    Speed = 0;

                    if (!Stopped) {
                        MainForm.Logger("Restarting miner thread...");
                        System.Threading.Thread.Sleep(Parameters.WaitTimeForRestartingMinerThreadInMilliseconds);
                    }
                }
            } catch (UnrecoverableException ex) {
                this.UnrecoverableException = ex;
            } catch (Exception ex) {
                this.UnrecoverableException = new UnrecoverableException(ex, Device);
            } finally {
                MarkAsDone();
                MemoryUsage = 0;
                if (program != null) { program.Dispose(); program = null; }
                if (neoScryptGlobalCacheBuffer != null) { OpenCLDevice.ReleaseComputeByteBuffer(neoScryptGlobalCacheBuffer); neoScryptGlobalCacheBuffer = null; }
            }
        }
    }
}
