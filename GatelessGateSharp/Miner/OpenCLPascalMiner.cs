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
    class OpenCLPascalMiner : OpenCLMiner
    {
        public static readonly int sPascalInputSize = 196;
        public static readonly int sPascalOutputSize = 256;
        public static readonly int sPascalMidstateSize = 32;

        long[] mPascalGlobalWorkSizeArray = new long[] { 0 };
        long[] mPascalLocalWorkSizeArray = new long[] { 0 };
        long[] mPascalGlobalWorkOffsetArray = new long[] { 0 };
        UInt32[] mPascalOutput = new UInt32[sPascalOutputSize];
        byte[] mPascalInput = new byte[sPascalInputSize];
        byte[] mPascalMidstate = new byte[sPascalMidstateSize];
          


        public OpenCLPascalMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "pascal")
        {
        }

        public void Start(PascalStratum aPascalStratum, int aPascalIntensity, int aPascalLocalWorkSize, int aKernelOptimizationLevel = -1)
        {
            KernelOptimizationLevel = aKernelOptimizationLevel;
            Stratum = aPascalStratum;
            mPascalGlobalWorkSizeArray[0] = aPascalIntensity * OpenCLDevice.GetMaxComputeUnits() * aPascalLocalWorkSize;
            mPascalLocalWorkSizeArray[0] = aPascalLocalWorkSize;

            base.Start();
        }

        // based on HashLib's SHA256 implementation
        private static readonly uint[] s_K = 
        {
            0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
            0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
            0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
            0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
            0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85, 
            0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
            0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
            0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
        };
        void CalculatePascalMidState() {
            uint[] state = new uint[] { 0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a, 0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19 };

            for (int block = 0; block < 3; ++block) {
                uint[] data = new uint[80];

                uint A = state[0];
                uint B = state[1];
                uint C = state[2];
                uint D = state[3];
                uint E = state[4];
                uint F = state[5];
                uint G = state[6];
                uint H = state[7];

                for (int j = 0; j < 16; ++j)
                    data[j] = (uint)(mPascalInput[block * 64 + j * 4 + 0] << 24)
                              | (uint)(mPascalInput[block * 64 + j * 4 + 1] << 16)
                              | (uint)(mPascalInput[block * 64 + j * 4 + 2] << 8)
                              | (uint)(mPascalInput[block * 64 + j * 4 + 3] << 0);

                for (int r = 16; r < 64; r++) {
                    uint T = data[r - 2];
                    uint T2 = data[r - 15];
                    data[r] = (((T >> 17) | (T << 15)) ^ ((T >> 19) | (T << 13)) ^ (T >> 10)) + data[r - 7] +
                        (((T2 >> 7) | (T2 << 25)) ^ ((T2 >> 18) | (T2 << 14)) ^ (T2 >> 3)) + data[r - 16];
                }

                for (int r = 0; r < 64; r++) {
                    uint T = s_K[r] + data[r] + H + (((E >> 6) | (E << 26)) ^ ((E >> 11) | (E << 21)) ^ ((E >> 25) |
                             (E << 7))) + ((E & F) ^ (~E & G));
                    uint T2 = (((A >> 2) | (A << 30)) ^ ((A >> 13) | (A << 19)) ^
                              ((A >> 22) | (A << 10))) + ((A & B) ^ (A & C) ^ (B & C));
                    H = G;
                    G = F;
                    F = E;
                    E = D + T;
                    D = C;
                    C = B;
                    B = A;
                    A = T + T2;
                }

                state[0] += A;
                state[1] += B;
                state[2] += C;
                state[3] += D;
                state[4] += E;
                state[5] += F;
                state[6] += G;
                state[7] += H;
            }

            for (int j = 0; j < 8; ++j) {
                mPascalMidstate[j * 4 + 0] = (byte)((state[j] >> 0) & 0xff);
                mPascalMidstate[j * 4 + 1] = (byte)((state[j] >> 8) & 0xff);
                mPascalMidstate[j * 4 + 2] = (byte)((state[j] >> 16) & 0xff);
                mPascalMidstate[j * 4 + 3] = (byte)((state[j] >> 24) & 0xff);
            }
        }

        public PascalStratum Stratum { get; set; }

        public override void SetPrimaryStratum(StratumServer stratum) {
            Stratum = (PascalStratum)stratum;
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

                program = BuildProgram("pascal", mPascalLocalWorkSizeArray[0], "", "", "");

                using (var pascalSearchKernel = program.CreateKernel("search"))
                using (var pascalInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, sPascalInputSize))
                using (var pascalOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, sPascalOutputSize))
                using (var pascalMidstateBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, sPascalMidstateSize))
                fixed (long* pascalGlobalWorkOffsetArrayPtr = mPascalGlobalWorkOffsetArray)
                fixed (long* pascalGlobalWorkSizeArrayPtr = mPascalGlobalWorkSizeArray)
                fixed (long* pascalLocalWorkSizeArrayPtr = mPascalLocalWorkSizeArray)
                fixed (byte* pascalMidstatePtr = mPascalMidstate)
                fixed (byte* pascalInputPtr = mPascalInput)
                fixed (UInt32* pascalOutputPtr = mPascalOutput)
                while (!Stopped)
                {
                    MarkAsAlive();

                    try
                    {
                        pascalSearchKernel.SetMemoryArgument(0, pascalInputBuffer);
                        pascalSearchKernel.SetMemoryArgument(1, pascalOutputBuffer);
                        pascalSearchKernel.SetMemoryArgument(4, pascalMidstateBuffer);

                        // Wait for the first PascalJob to arrive.
                        System.Diagnostics.Stopwatch firstJobStopwatch = new System.Diagnostics.Stopwatch();
                        firstJobStopwatch.Start();
                        while ((Stratum == null || Stratum.GetJob() == null) && firstJobStopwatch.ElapsedMilliseconds < Parameters.TimeoutForFirstJobInMilliseconds && !Stopped)
                            Thread.Sleep(100);
                        if (Stratum == null || Stratum.GetJob() == null)
                            throw new TimeoutException("Stratum server failed to send a new PascalJob.");

                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        PascalStratum.Work pascalWork;
                        PascalStratum.Job pascalJob;

                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        sw.Start();
                        while (!Stopped && (pascalWork = Stratum.GetWork()) != null && (pascalJob = pascalWork.Job) != null)
                        {
                            MarkAsAlive();

                            Array.Copy(pascalWork.Blob, mPascalInput, sPascalInputSize);
                            CalculatePascalMidState();
                            Queue.Write<byte>(pascalMidstateBuffer, true, 0, sPascalMidstateSize, (IntPtr)pascalMidstatePtr, null);
                            UInt32 pascalStartNonce = (UInt32)(r.Next(0, int.MaxValue));
                            UInt64 PascalTarget = (UInt64) ((double) 0xffff0000UL / Stratum.Difficulty);
                            pascalSearchKernel.SetValueArgument<UInt64>(3, PascalTarget);
                            Queue.Write<byte>(pascalInputBuffer, true, 0, sPascalInputSize, (IntPtr)pascalInputPtr, null);

                            consoleUpdateStopwatch.Start();

                            while (!Stopped && Stratum.GetJob() != null && Stratum.GetJob().Equals(pascalJob))
                            {
                                MarkAsAlive();

                                pascalSearchKernel.SetValueArgument<UInt32>(2, pascalStartNonce);

                                // Get a new local extranonce if necessary.
                                if (0xffffffffu - pascalStartNonce < (UInt32)mPascalGlobalWorkSizeArray[0])
                                    break;

                                mPascalOutput[255] = 0; // mPascalOutput[255] is used as an atomic counter.
                                Queue.Write<UInt32>(pascalOutputBuffer, true, 0, sPascalOutputSize, (IntPtr)pascalOutputPtr, null);
                                Queue.Execute(pascalSearchKernel, mPascalGlobalWorkOffsetArray, mPascalGlobalWorkSizeArray, mPascalLocalWorkSizeArray, null);
                                Queue.Read<UInt32>(pascalOutputBuffer, true, 0, sPascalOutputSize, (IntPtr)pascalOutputPtr, null);
                                if (Stratum.GetJob() != null && Stratum.GetJob().Equals(pascalJob))
                                {
                                    for (int i = 0; i < mPascalOutput[255]; ++i)
                                        Stratum.Submit(Device, pascalWork, mPascalOutput[i]);
                                }
                                pascalStartNonce += (UInt32)mPascalGlobalWorkSizeArray[0];

                                ReportHashCount((double)mPascalGlobalWorkSizeArray[0], 0, sw.Elapsed.TotalSeconds);
                                sw.Restart();
                                if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                                {
                                    MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Mh/s (Pascal)", Speed / 1000000));
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
                MarkAsDone();

                program.Dispose();
            } catch (UnrecoverableException ex) {
                if (program != null)
                    program.Dispose();
                this.UnrecoverableException = ex;
            } catch (Exception ex) {
                if (program != null)
                    program.Dispose();
                this.UnrecoverableException = new UnrecoverableException(ex, Device);
            }
        }
    }
}

