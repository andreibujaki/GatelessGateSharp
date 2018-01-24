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



namespace GatelessGateSharp {
    class OpenCLCryptoNightMiner : OpenCLMiner, IDisposable {
        private static readonly int outputSize = 256 + 255 * 8;

        private bool mNicehashMode = false;
        private bool mSavedNicehashMode = false;

        long[] globalWorkSizeA = new long[] { 0, 8 };
        long[] globalWorkSizeB = new long[] { 0 };
        long[] localWorkSizeA = new long[] { 0, 8 };
        long[] localWorkSizeB = new long[] { 0 };
        long[] globalWorkOffsetA = new long[] { 0, 1 };
        long[] globalWorkOffsetB = new long[] { 0 };

        Int32[] terminate = new Int32[1];
        UInt32[] output = new UInt32[outputSize];
        byte[] input = new byte[76];

        public bool NiceHashMode { get { return mNicehashMode; } }
        public void SaveNiceHashMode() { mSavedNicehashMode = mNicehashMode; }
        public void RestoreNiceHashMode() { mNicehashMode = mSavedNicehashMode; }

        public OpenCLCryptoNightMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "cryptonight") {
        }

        public void Start(CryptoNightStratum aStratum, int aRowIntensity, int aLocalWorkSize, bool aNicehashMode = false) {
            Stratum = aStratum;
            globalWorkSizeA[0] = globalWorkSizeB[0] = aRowIntensity * aLocalWorkSize;
            localWorkSizeA[0] = localWorkSizeB[0] = aLocalWorkSize;
            mNicehashMode = aNicehashMode;

            base.Start();
        }

        public CryptoNightStratum Stratum { get; set; }

        public override void SetPrimaryStratum(Stratum stratum) {
            Stratum = (CryptoNightStratum)stratum;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread() {
            ComputeProgram program = null;
            ComputeBuffer<byte> statesBuffer = null;
            ComputeBuffer<byte> scratchpadsBuffer = null;
            try {
                Random r = new Random();
                var openCLName = OpenCLDevice.GetComputeDevice().Name;
                var GCN1 = openCLName == "Capeverde" || openCLName == "Hainan" || openCLName == "Oland" || openCLName == "Pitcairn" || openCLName == "Tahiti";

                MarkAsAlive();

                MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");
                MainForm.Logger("NiceHash mode is " + (NiceHashMode ? "on" : "off") + ".");

                program = BuildProgram("cryptonight", localWorkSizeA[0], "-O5" + (GCN1 ? " -legacy" : ""), "", "");

                statesBuffer = OpenCLDevice.RequestComputeByteBuffer(ComputeMemoryFlags.ReadWrite, 200 * globalWorkSizeA[0]);
                scratchpadsBuffer = OpenCLDevice.RequestComputeByteBuffer(ComputeMemoryFlags.ReadWrite, ((long)1 << 21) * globalWorkSizeA[0]);

                using (var searchKernel0 = program.CreateKernel("search"))
                using (var searchKernel1 = program.CreateKernel("search1"))
                using (var searchKernel2 = program.CreateKernel("search2"))
                using (var searchKernel3 = program.CreateKernel("search3"))
                using (var inputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 76))
                using (var outputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, outputSize))
                using (var terminateBuffer = new ComputeBuffer<Int32>(Context, ComputeMemoryFlags.ReadWrite, 1))
                fixed (Int32* terminatePtr = terminate)
                fixed (byte* inputPtr = input)
                fixed (UInt32* outputPtr = output)
                while (!Stopped) {
                    MarkAsAlive();
                    MemoryUsage =
                        200 * globalWorkSizeA[0]
                        + ((long)1 << 21) * globalWorkSizeA[0]
                        + 76 + outputSize + 1;

                    try {
                        searchKernel0.SetMemoryArgument(0, inputBuffer);
                        searchKernel0.SetMemoryArgument(1, scratchpadsBuffer);
                        searchKernel0.SetMemoryArgument(2, statesBuffer);

                        searchKernel1.SetMemoryArgument(0, scratchpadsBuffer);
                        searchKernel1.SetMemoryArgument(1, statesBuffer);
                        searchKernel1.SetMemoryArgument(2, terminateBuffer);

                        searchKernel2.SetMemoryArgument(0, scratchpadsBuffer);
                        searchKernel2.SetMemoryArgument(1, statesBuffer);

                        searchKernel3.SetMemoryArgument(0, statesBuffer);
                        searchKernel3.SetMemoryArgument(1, outputBuffer);

                        // Wait for the first job to arrive.
                        int elapsedTime = 0;
                        while ((Stratum == null || Stratum.GetJob() == null) && elapsedTime < Parameters.TimeoutForFirstJobInMilliseconds && !Stopped) {
                            Thread.Sleep(100);
                            elapsedTime += 100;
                        }
                        if (Stratum == null || Stratum.GetJob() == null)
                            throw new TimeoutException("Stratum server failed to send a new job.");

                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        CryptoNightStratum.Work work;
                        CryptoNightStratum.Job job;

                        while (!Stopped && (work = Stratum.GetWork()) != null && (job = work.GetJob()) != null) {
                            MarkAsAlive();

                            Array.Copy(Utilities.StringToByteArray(job.Blob), input, 76);
                            byte localExtranonce = (byte)work.LocalExtranonce;
                            byte[] targetByteArray = Utilities.StringToByteArray(job.Target);
                            UInt32 startNonce;
                            if (NiceHashMode) {
                                startNonce = ((UInt32)input[42] << (8 * 3)) | ((UInt32)localExtranonce << (8 * 2)) | (UInt32)(r.Next(0, int.MaxValue) & (0x0000ffffu));
                            } else {
                                startNonce = ((UInt32)localExtranonce << (8 * 3)) | (UInt32)(r.Next(0, int.MaxValue) & (0x00ffffffu));
                            }
                            UInt32 target = ((UInt32)targetByteArray[0] << 0)
                                            | ((UInt32)targetByteArray[1] << 8)
                                            | ((UInt32)targetByteArray[2] << 16)
                                            | ((UInt32)targetByteArray[3] << 24);
                            searchKernel3.SetValueArgument<UInt32>(2, target);

                            Queue.Write<byte>(inputBuffer, true, 0, 76, (IntPtr)inputPtr, null);

                            consoleUpdateStopwatch.Start();

                            while (!Stopped && Stratum.GetJob() != null && Stratum.GetJob().Equals(job)) {
                                MarkAsAlive();

                                globalWorkOffsetA[0] = globalWorkOffsetB[0] = startNonce;

                                // Get a new local extranonce if necessary.
                                if (NiceHashMode) {
                                    if ((startNonce & 0xffff) + (UInt32)globalWorkSizeA[0] >= 0x10000)
                                        break;
                                } else {
                                    if ((startNonce & 0xffffff) + (UInt32)globalWorkSizeA[0] >= 0x1000000)
                                        break;
                                }

                                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                sw.Start();
                                output[255] = 0; // output[255] is used as an atomic counter.
                                Queue.Write<UInt32>(outputBuffer, true, 0, outputSize, (IntPtr)outputPtr, null);
                                terminate[0] = 0;
                                Queue.Write<Int32>(terminateBuffer, true, 0, 1, (IntPtr)terminatePtr, null);
                                Queue.Execute(searchKernel0, globalWorkOffsetA, globalWorkSizeA, localWorkSizeA, null);
                                Queue.Finish();
                                if (Stopped)
                                    break;
                                Queue.Execute(searchKernel1, globalWorkOffsetB, globalWorkSizeB, localWorkSizeB, null);
                                Queue.Finish();
                                if (Stopped)
                                    break;
                                Queue.Execute(searchKernel2, globalWorkOffsetA, globalWorkSizeA, localWorkSizeA, null);
                                Queue.Finish();
                                if (Stopped)
                                    break;
                                Queue.Execute(searchKernel3, globalWorkOffsetB, globalWorkSizeB, localWorkSizeB, null);
                                Queue.Finish(); // Run the above statement before leaving the current local scope.
                                if (Stopped)
                                    break;

                                Queue.Read<UInt32>(outputBuffer, true, 0, outputSize, (IntPtr)outputPtr, null);
                                if (Stratum.GetJob() != null && Stratum.GetJob().Equals(job)) {
                                    for (int i = 0; i < output[255]; ++i) {
                                        String result = "";
                                        for (int j = 0; j < 8; ++j) {
                                            UInt32 word = output[256 + i * 8 + j];
                                            result += String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((word >> 0) & 0xff), ((word >> 8) & 0xff), ((word >> 16) & 0xff), ((word >> 24) & 0xff));
                                        }
                                        Stratum.Submit(GatelessGateDevice, job, output[i], result);
                                    }
                                }
                                startNonce += (UInt32)globalWorkSizeA[0];

                                sw.Stop();
                                Speed = ((double)globalWorkSizeA[0]) / sw.Elapsed.TotalSeconds;
                                if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000) {
                                    MainForm.Logger("Device #" + DeviceIndex + " (CryptoNight): " + String.Format("{0:N2} h/s", Speed));
                                    consoleUpdateStopwatch.Restart();
                                }
                            }
                        }
                    } catch (Exception ex) {
                        if (statesBuffer != null) { OpenCLDevice.ReleaseComputeByteBuffer(statesBuffer); statesBuffer = null; }
                        if (scratchpadsBuffer != null) { OpenCLDevice.ReleaseComputeByteBuffer(scratchpadsBuffer); scratchpadsBuffer = null; }
                        MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                        if (UnrecoverableException.IsUnrecoverableException(ex)) {
                            this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
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
                this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
            } finally {
                MarkAsDone();
                MemoryUsage = 0;
                if (program != null) { program.Dispose(); program = null; }
                if (statesBuffer != null) { OpenCLDevice.ReleaseComputeByteBuffer(statesBuffer); statesBuffer = null; }
                if (scratchpadsBuffer != null) { OpenCLDevice.ReleaseComputeByteBuffer(scratchpadsBuffer); scratchpadsBuffer = null; }
            }
        }
    }
}

