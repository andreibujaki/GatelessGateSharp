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
        private static Mutex mProgramArrayMutex = new Mutex();
        private static Dictionary<long[], ComputeProgram> mEthashProgramArray = new Dictionary<long[], ComputeProgram>();
        private static Dictionary<long[], ComputeKernel> mEthashDAGKernelArray = new Dictionary<long[], ComputeKernel>();
        private static Dictionary<long[], ComputeKernel> mEthashSearchKernelArray = new Dictionary<long[], ComputeKernel>();
        private EthashStratum mEthashStratum;
        private ComputeProgram mEthashProgram;
        private ComputeKernel mEthashDAGKernel;
        private ComputeKernel mEthashSearchKernel;
        private ComputeBuffer<UInt32> mEthashOutputBuffer;
        private ComputeBuffer<byte> mEthashHeaderBuffer;
        private long[] mEthashGlobalWorkOffsetArray = new long[1];
        private long[] mEthashGlobalWorkSizeArray = new long[1];
        private long[] mEthashLocalWorkSizeArray = new long[1];
        
        public OpenCLEthashMiner(Device aGatelessGateDevice)
            : base(aGatelessGateDevice, "Ethash")
        {
            mEthashOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, 256);
            mEthashHeaderBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 32);
        }

        public void Start(EthashStratum aEthashStratum, int aEthashIntensity, int aEthashLocalWorkSize)
        {
            mEthashStratum = aEthashStratum;
            mEthashLocalWorkSizeArray[0] = aEthashLocalWorkSize;
            mEthashGlobalWorkSizeArray[0] = aEthashIntensity * mEthashLocalWorkSizeArray[0] * Device.GetComputeDevice().MaxComputeUnits;

            base.Start();
        }

        private void BuildEthashProgram()
        {
            ComputeDevice computeDevice = Device.GetComputeDevice();

            try { mProgramArrayMutex.WaitOne(5000); } catch (Exception) { }
            
            if (mEthashProgramArray.ContainsKey(new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }))
            {
                mEthashProgram = mEthashProgramArray[new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }];
                mEthashDAGKernel = mEthashDAGKernelArray[new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }];
                mEthashSearchKernel = mEthashSearchKernelArray[new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }];
            }
            else
            {
                String source = System.IO.File.ReadAllText(@"Kernels\ethash.cl");
                mEthashProgram = new ComputeProgram(Context, source);
                MainForm.Logger("Loaded ethash mEthashProgram for Device #" + DeviceIndex + ".");
                String buildOptions = (Device.Vendor == "AMD"    ? "-O1 " :
                                       Device.Vendor == "NVIDIA" ? "" : // "-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + mEthashLocalWorkSizeArray[0];
                try
                {
                    mEthashProgram.Build(Device.DeviceList, buildOptions, null, IntPtr.Zero);
                }
                catch (Exception)
                {
                    MainForm.Logger(mEthashProgram.GetBuildLog(computeDevice));
                    throw;
                }
                MainForm.Logger("Built ethash mEthashProgram for Device #" + DeviceIndex + ".");
                MainForm.Logger("Build options: " + buildOptions);
                mEthashProgramArray[new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }] = mEthashProgram;
                mEthashDAGKernelArray[new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }] = mEthashDAGKernel = mEthashProgram.CreateKernel("GenerateDAG");
                mEthashSearchKernelArray[new long[] { DeviceIndex, mEthashLocalWorkSizeArray[0] }] = mEthashSearchKernel = mEthashProgram.CreateKernel("search");
            }

            try { mProgramArrayMutex.ReleaseMutex(); } catch (Exception) { }
        }

        override unsafe protected void MinerThread()
        {
            Random r = new Random();
            UInt32[] ethashOutput = new UInt32[256];
            byte[] ethashHeaderhash = new byte[32];

            MarkAsAlive();

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

            BuildEthashProgram();

            fixed (UInt32* ethashOutputPtr = ethashOutput)
            fixed (byte* ethashHeaderhashPtr = ethashHeaderhash)
            while (!Stopped)
            {
                MarkAsAlive();

                try
                {
                    int ethashEpoch = -1;
                    long ethashDAGSize = 0;
                    ComputeBuffer<byte> ethashDAGBuffer = null;

                    // Wait for the first job to arrive.
                    int elapsedTime = 0;
                    while ((mEthashStratum == null || mEthashStratum.GetJob() == null) && elapsedTime < 5000)
                    {
                        Thread.Sleep(10);
                        elapsedTime += 10;
                    }
                    if (mEthashStratum == null || mEthashStratum.GetJob() == null)
                    {
                        MainForm.Logger("Ethash stratum server failed to send a new job.");
                        //throw new TimeoutException("Stratum server failed to send a new job.");
                        return;
                    }
                    
                    System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                    EthashStratum.Work ethashWork;
 
                    while (!Stopped && (ethashWork = mEthashStratum.GetWork()) != null)
                    {
                        MarkAsAlive();

                        String ethashPoolExtranonce = mEthashStratum.PoolExtranonce;
                        byte[] ethashExtranonceByteArray = Utilities.StringToByteArray(ethashPoolExtranonce);
                        byte ethashLocalExtranonce = ethashWork.LocalExtranonce;
                        UInt64 ethashStartNonce = (UInt64)ethashLocalExtranonce << (8 * (7 - ethashExtranonceByteArray.Length));
                        for (int i = 0; i < ethashExtranonceByteArray.Length; ++i)
                            ethashStartNonce |= (UInt64)ethashExtranonceByteArray[i] << (8 * (7 - i));
                        ethashStartNonce += (ulong)r.Next(0, int.MaxValue) & (0xfffffffffffffffful >> (ethashExtranonceByteArray.Length * 8 + 8));
                        String ethashJobID = ethashWork.GetJob().ID;
                        String ethashSeedhash = ethashWork.GetJob().Seedhash;
                        double ethashDifficulty = mEthashStratum.Difficulty;

                        Buffer.BlockCopy(Utilities.StringToByteArray(ethashWork.GetJob().Headerhash), 0, ethashHeaderhash, 0, 32);
                        Queue.Write<byte>(mEthashHeaderBuffer, true, 0, 32, (IntPtr)ethashHeaderhashPtr, null);

                        if (ethashEpoch != ethashWork.GetJob().Epoch)
                        {
                            if (ethashDAGBuffer != null)
                            {
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

                            mEthashDAGKernel.SetValueArgument<UInt32>(0, 0);
                            mEthashDAGKernel.SetMemoryArgument(1, DAGCacheBuffer);
                            mEthashDAGKernel.SetMemoryArgument(2, ethashDAGBuffer);
                            mEthashDAGKernel.SetValueArgument<UInt32>(3, (UInt32)cache.GetData().Length / 64);
                            mEthashDAGKernel.SetValueArgument<UInt32>(4, 0xffffffffu);

                            for (long start = 0; start < ethashDAGSize / 64; start += mEthashGlobalWorkSizeArray[0])
                            {
                                mEthashGlobalWorkOffsetArray[0] = start;
                                Queue.Execute(mEthashDAGKernel, mEthashGlobalWorkOffsetArray, mEthashGlobalWorkSizeArray, mEthashLocalWorkSizeArray, null);
                                Queue.Finish();
                                if (Stopped || !mEthashStratum.GetJob().ID.Equals(ethashJobID))
                                    break;
                            }
                            DAGCacheBuffer.Dispose();
                            if (Stopped || !mEthashStratum.GetJob().ID.Equals(ethashJobID))
                                break;
                            sw.Stop();
                            MainForm.Logger("Generated DAG for Epoch #" + ethashEpoch + " (" + (long)sw.Elapsed.TotalMilliseconds + "ms).");
                        }

                        consoleUpdateStopwatch.Start();

                        while (!Stopped && mEthashStratum.GetJob().ID.Equals(ethashJobID) && mEthashStratum.PoolExtranonce.Equals(ethashPoolExtranonce))
                        {
                            MarkAsAlive();

                            // Get a new local extranonce if necessary.
                            if ((ethashStartNonce & (0xfffffffffffffffful >> (ethashExtranonceByteArray.Length * 8 + 8)) + (ulong)mEthashGlobalWorkSizeArray[0]) >= ((ulong)0x1 << (64 - (ethashExtranonceByteArray.Length * 8 + 8))))
                                break;

                            UInt64 target = (UInt64)((double)0xffff0000U / ethashDifficulty);
                            mEthashSearchKernel.SetMemoryArgument(0, mEthashOutputBuffer); // g_output
                            mEthashSearchKernel.SetMemoryArgument(1, mEthashHeaderBuffer); // g_header
                            mEthashSearchKernel.SetMemoryArgument(2, ethashDAGBuffer); // _g_dag
                            mEthashSearchKernel.SetValueArgument<UInt32>(3, (UInt32)(ethashDAGSize / 128)); // DAG_SIZE
                            mEthashSearchKernel.SetValueArgument<UInt64>(4, ethashStartNonce); // start_nonce
                            mEthashSearchKernel.SetValueArgument<UInt64>(5, target); // target
                            mEthashSearchKernel.SetValueArgument<UInt32>(6, 0xffffffffu); // isolate

                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();
                            ethashOutput[255] = 0; // ethashOutput[255] is used as an atomic counter.
                            Queue.Write<UInt32>(mEthashOutputBuffer, true, 0, 256, (IntPtr)ethashOutputPtr, null);
                            mEthashGlobalWorkOffsetArray[0] = 0;
                            Queue.Execute(mEthashSearchKernel, mEthashGlobalWorkOffsetArray, mEthashGlobalWorkSizeArray, mEthashLocalWorkSizeArray, null);
                            Queue.Read<UInt32>(mEthashOutputBuffer, true, 0, 256, (IntPtr)ethashOutputPtr, null);
                            if (mEthashStratum.GetJob().ID.Equals(ethashJobID))
                            {
                                for (int i = 0; i < ethashOutput[255]; ++i)
                                    mEthashStratum.Submit(GatelessGateDevice, ethashWork.GetJob(), ethashStartNonce + (UInt64)ethashOutput[i]);
                            }
                            ethashStartNonce += (UInt64)mEthashGlobalWorkSizeArray[0];

                            sw.Stop();
                            mSpeed = ((double)mEthashGlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                            if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                            {
                                MainForm.Logger("Device #" + DeviceIndex + " (Ethash): " + String.Format("{0:N2} Mh/s", mSpeed / (1000000)));
                                consoleUpdateStopwatch.Restart();
                            }
                        }
                    }

                    if (ethashDAGBuffer != null)
                    {
                        ethashDAGBuffer.Dispose();
                        ethashDAGBuffer = null;
                    }
                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                    MainForm.Logger("Restarting miner thread...");
                }
            }

            MarkAsDone();
        }
    }
}
