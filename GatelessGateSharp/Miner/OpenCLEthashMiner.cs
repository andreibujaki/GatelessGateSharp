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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cloo;
using HashLib;



namespace GatelessGateSharp
{
    class OpenCLEthashMiner : OpenCLMiner
    {
        private long[] mEthashGlobalWorkOffsetArray = new long[1];
        private long[] mEthashGlobalWorkSizeArray = new long[1];
        private long[] mEthashLocalWorkSizeArray = new long[1];
        
        public OpenCLEthashMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "ethash")
        {
        }

        public void Start(EthashStratum aEthashStratum, int aEthashIntensity, int aEthashLocalWorkSize)
        {
            Stratum = aEthashStratum;
            mEthashLocalWorkSizeArray[0] = aEthashLocalWorkSize;
            mEthashGlobalWorkSizeArray[0] = aEthashIntensity * mEthashLocalWorkSizeArray[0] * OpenCLDevice.GetComputeDevice().MaxComputeUnits;

            base.Start();
        }

        public EthashStratum Stratum { get; set; }

        public override void SetPrimaryStratum(Stratum stratum) {
            Stratum = (EthashStratum)stratum;
        }

        override unsafe protected void MinerThread()
        {
            ComputeProgram program = null;
            try { 
                Random r = new Random();
                ComputeDevice computeDevice = OpenCLDevice.GetComputeDevice();
                UInt32[] ethashOutput = new UInt32[256];
                byte[] ethashHeaderhash = new byte[32];

                MarkAsAlive();

                MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

                program = BuildProgram("ethash", mEthashLocalWorkSizeArray[0], "-O1", "", "");

                MemoryUsage = 256 + 32;

                using (var searchKernel = program.CreateKernel("search"))
                using (var DAGKernel = program.CreateKernel("GenerateDAG"))
                using (var ethashOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, 256))
                using (var ethashHeaderBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 32))
                fixed (UInt32* ethashOutputPtr = ethashOutput)
                fixed (byte* ethashHeaderhashPtr = ethashHeaderhash)
                while (!Stopped) {
                    ComputeBuffer<byte> ethashDAGBuffer = null;

                    MarkAsAlive();

                    try
                    {
                        int ethashEpoch = -1;
                        long ethashDAGSize = 0;

                        // Wait for the first job to arrive.
                        int elapsedTime = 0;
                        while ((Stratum == null || Stratum.GetJob() == null) && elapsedTime < Parameters.TimeoutForFirstJobInMilliseconds && !Stopped) {
                            Thread.Sleep(100);
                            elapsedTime += 100;
                        }
                        if (Stratum == null || Stratum.GetJob() == null)
                        {
                            MainForm.Logger("Ethash stratum server failed to send a new job.");
                            //throw new TimeoutException("Stratum server failed to send a new job.");
                            return;
                        }
                    
                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        EthashStratum.Work ethashWork;
                        EthashStratum.Job ethashJob;

                        while (!Stopped && (ethashWork = Stratum.GetWork()) != null && (ethashJob = ethashWork.GetJob()) != null)
                        {
                            MarkAsAlive();

                            String ethashPoolExtranonce = Stratum.PoolExtranonce;
                            byte[] ethashExtranonceByteArray = Utilities.StringToByteArray(ethashPoolExtranonce);
                            byte ethashLocalExtranonce = (byte)ethashWork.LocalExtranonce;
                            UInt64 ethashStartNonce = (UInt64)ethashLocalExtranonce << (8 * (7 - ethashExtranonceByteArray.Length));
                            for (int i = 0; i < ethashExtranonceByteArray.Length; ++i)
                                ethashStartNonce |= (UInt64)ethashExtranonceByteArray[i] << (8 * (7 - i));
                            ethashStartNonce += (ulong)r.Next(0, int.MaxValue) & (0xfffffffffffffffful >> (ethashExtranonceByteArray.Length * 8 + 8));
                            String ethashJobID = ethashJob.ID;
                            String ethashSeedhash = ethashJob.Seedhash;
                            double ethashDifficulty = Stratum.Difficulty;

                            Buffer.BlockCopy(Utilities.StringToByteArray(ethashWork.GetJob().Headerhash), 0, ethashHeaderhash, 0, 32);
                            Queue.Write<byte>(ethashHeaderBuffer, true, 0, 32, (IntPtr)ethashHeaderhashPtr, null);

                            if (ethashEpoch != ethashWork.GetJob().Epoch)
                            {
                                if (ethashDAGBuffer != null)
                                {
                                    MemoryUsage -= ethashDAGBuffer.Size;
                                    ethashDAGBuffer.Dispose();
                                    ethashDAGBuffer = null;
                                }
                                ethashEpoch = ethashWork.GetJob().Epoch;
                                DAGCache cache = new DAGCache(ethashEpoch, ethashWork.GetJob().Seedhash);
                                ethashDAGSize = Utilities.GetDAGSize(ethashEpoch);

                                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                sw.Start();
                                mEthashGlobalWorkSizeArray[0] = ethashDAGSize / 64;
                                mEthashGlobalWorkSizeArray[0] /= 8;
                                if (mEthashGlobalWorkSizeArray[0] % mEthashLocalWorkSizeArray[0] > 0)
                                    mEthashGlobalWorkSizeArray[0] += mEthashLocalWorkSizeArray[0] - mEthashGlobalWorkSizeArray[0] % mEthashLocalWorkSizeArray[0];

                                ComputeBuffer<byte> DAGCacheBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, cache.GetData().Length);
                                fixed (byte* p = cache.GetData())
                                    Queue.Write<byte>(DAGCacheBuffer, true, 0, cache.GetData().Length, (IntPtr)p, null);
                                ethashDAGBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, mEthashGlobalWorkSizeArray[0] * 8 * 64 /* ethashDAGSize */); // With this, we can remove a conditional statement in the DAG kernel.
                                MemoryUsage += DAGCacheBuffer.Size + ethashDAGBuffer.Size;

                                DAGKernel.SetValueArgument<UInt32>(0, 0);
                                DAGKernel.SetMemoryArgument(1, DAGCacheBuffer);
                                DAGKernel.SetMemoryArgument(2, ethashDAGBuffer);
                                DAGKernel.SetValueArgument<UInt32>(3, (UInt32)cache.GetData().Length / 64);
                                DAGKernel.SetValueArgument<UInt32>(4, 0xffffffffu);

                                for (long start = 0; start < ethashDAGSize / 64; start += mEthashGlobalWorkSizeArray[0])
                                {
                                    mEthashGlobalWorkOffsetArray[0] = start;
                                    Queue.Execute(DAGKernel, mEthashGlobalWorkOffsetArray, mEthashGlobalWorkSizeArray, mEthashLocalWorkSizeArray, null);
                                    Queue.Finish();
                                    if (Stopped || !Stratum.GetJob().ID.Equals(ethashJobID))
                                        break;
                                }
                                MemoryUsage -= DAGCacheBuffer.Size;
                                DAGCacheBuffer.Dispose();
                                if (Stopped || !Stratum.GetJob().ID.Equals(ethashJobID))
                                    break;
                                sw.Stop();
                                MainForm.Logger("Generated DAG for Epoch #" + ethashEpoch + " (" + (long)sw.Elapsed.TotalMilliseconds + "ms).");
                            }

                            consoleUpdateStopwatch.Start();

                            while (!Stopped && Stratum.GetJob() != null && Stratum.GetJob().ID.Equals(ethashJobID) && Stratum.PoolExtranonce.Equals(ethashPoolExtranonce))
                            {
                                MarkAsAlive();

                                // Get a new local extranonce if necessary.
                                if ((ethashStartNonce & (0xfffffffffffffffful >> (ethashExtranonceByteArray.Length * 8 + 8)) + (ulong)mEthashGlobalWorkSizeArray[0]) >= ((ulong)0x1 << (64 - (ethashExtranonceByteArray.Length * 8 + 8))))
                                    break;

                                UInt64 target = (UInt64)((double)0xffff0000U / ethashDifficulty);
                                searchKernel.SetMemoryArgument(0, ethashOutputBuffer); // g_output
                                searchKernel.SetMemoryArgument(1, ethashHeaderBuffer); // g_header
                                searchKernel.SetMemoryArgument(2, ethashDAGBuffer); // _g_dag
                                searchKernel.SetValueArgument<UInt32>(3, (UInt32)(ethashDAGSize / 128)); // DAG_SIZE
                                searchKernel.SetValueArgument<UInt64>(4, ethashStartNonce); // start_nonce
                                searchKernel.SetValueArgument<UInt64>(5, target); // target
                                searchKernel.SetValueArgument<UInt32>(6, 0xffffffffu); // isolate

                                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                sw.Start();
                                ethashOutput[255] = 0; // ethashOutput[255] is used as an atomic counter.
                                Queue.Write<UInt32>(ethashOutputBuffer, true, 0, 256, (IntPtr)ethashOutputPtr, null);
                                mEthashGlobalWorkOffsetArray[0] = 0;
                                Queue.Execute(searchKernel, mEthashGlobalWorkOffsetArray, mEthashGlobalWorkSizeArray, mEthashLocalWorkSizeArray, null);
                                Queue.Read<UInt32>(ethashOutputBuffer, true, 0, 256, (IntPtr)ethashOutputPtr, null);
                                if (Stratum.GetJob() != null && Stratum.GetJob().ID.Equals(ethashJobID))
                                {
                                    for (int i = 0; i < ethashOutput[255]; ++i)
                                        Stratum.Submit(GatelessGateDevice, ethashWork.GetJob(), ethashStartNonce + (UInt64)ethashOutput[i]);
                                }
                                ethashStartNonce += (UInt64)mEthashGlobalWorkSizeArray[0];

                                sw.Stop();
                                Speed = ((double)mEthashGlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                                if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                                {
                                    MainForm.Logger("Device #" + DeviceIndex + " (Ethash): " + String.Format("{0:N2} Mh/s", Speed / (1000000)));
                                    consoleUpdateStopwatch.Restart();
                                }
                            }
                        }
                    } catch (Exception ex) {
                        MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                        Speed = 0;
                        if (UnrecoverableException.IsUnrecoverableException(ex)) {
                            this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
                            Stop();
                        } else {
                            MainForm.Logger("Restarting miner thread...");
                            System.Threading.Thread.Sleep(Parameters.WaitTimeForRestartingMinerThreadInMilliseconds);
                        }
                    } finally {
                        if (ethashDAGBuffer != null) {
                            MemoryUsage -= ethashDAGBuffer.Size;
                            ethashDAGBuffer.Dispose();
                            ethashDAGBuffer = null;
                        }
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
