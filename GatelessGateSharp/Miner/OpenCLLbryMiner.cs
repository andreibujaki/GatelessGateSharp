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
        private static readonly int outputSize = 256 + 8 * 255;

        private LbryStratum mStratum;
        private bool mNicehashMode = false;

        long[] globalWorkSizeArray = new long[] { 0 };
        long[] localWorkSizeArray = new long[] { 0 };
        long[] globalWorkOffsetArray = new long[] { 0 };

        UInt32[] output = new UInt32[outputSize];
        byte[] input = new byte[112];

        static Mutex mProgramArrayMutex = new Mutex();

        static Dictionary<ProgramArrayIndex, ComputeProgram> mProgramArray = new Dictionary<ProgramArrayIndex, ComputeProgram>();
        ComputeKernel searchKernel0 = null;
        ComputeKernel searchKernel1 = null;
        ComputeKernel searchKernel2 = null;
        ComputeKernel combinedSearchKernel = null;
            
        private ComputeBuffer<byte> contextBuffer = null;
        private ComputeBuffer<byte> inputBuffer = null;
        private ComputeBuffer<UInt32> outputBuffer = null;
            
        public bool NiceHashMode { get { return mNicehashMode; } }

        public OpenCLLbryMiner(Device aGatelessGateDevice)
            : base(aGatelessGateDevice, "Lbry")
        {
        }

        public void Start(LbryStratum aStratum, int aIntensity, int aLocalWorkSize, bool aNicehashMode = false)
        {
            mStratum = aStratum;
            globalWorkSizeArray[0] = aIntensity * Device.MaxComputeUnits;
            localWorkSizeArray[0] = aLocalWorkSize;
            if (globalWorkSizeArray[0] % aLocalWorkSize != 0)
                globalWorkSizeArray[0] = aLocalWorkSize - globalWorkSizeArray[0] % aLocalWorkSize;
            mNicehashMode = aNicehashMode;

            if (localWorkSizeArray[0] != aLocalWorkSize)
            {
                contextBuffer = null;
            }

            base.Start();
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread()
        {
            Random r = new Random();
            ComputeDevice computeDevice = Device.GetComputeDevice();
            
            MarkAsAlive();

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");
            MainForm.Logger("NiceHash mode is " + (NiceHashMode ? "on" : "off") + ".");

            ComputeProgram program;
            try { mProgramArrayMutex.WaitOne(); } catch (Exception) { }
            if (mProgramArray.ContainsKey(new ProgramArrayIndex(DeviceIndex, localWorkSizeArray[0])))
            {
                program = mProgramArray[new ProgramArrayIndex(DeviceIndex, localWorkSizeArray[0])];
            } 
            else
            {
                String source = System.IO.File.ReadAllText(@"Kernels\lbry.cl");
                program = new ComputeProgram(Context, source);
                MainForm.Logger("Loaded Lbry program for Device #" + DeviceIndex + ".");
                String buildOptions = (Device.Vendor == "AMD"    ? "-O1 " : //"-O1 " :
                                       Device.Vendor == "NVIDIA" ? "" : //"-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + localWorkSizeArray[0];
                try
                {
                    program.Build(Device.DeviceList, buildOptions, null, IntPtr.Zero);
                }
                catch (Exception)
                {
                    MainForm.Logger(program.GetBuildLog(computeDevice));
                    throw;
                }
                MainForm.Logger("Built Lbry program for Device #" + DeviceIndex + ".");
                MainForm.Logger("Built options: " + buildOptions);
                mProgramArray[new ProgramArrayIndex(DeviceIndex, localWorkSizeArray[0])] = program;
            }
            try { mProgramArrayMutex.ReleaseMutex(); } catch (Exception) { }

            if (searchKernel0 == null) searchKernel0 = program.CreateKernel("search");
            if (searchKernel1 == null) searchKernel1 = program.CreateKernel("search1");
            if (searchKernel2 == null) searchKernel2 = program.CreateKernel("search2");
            if (combinedSearchKernel == null) combinedSearchKernel = program.CreateKernel("search_combined");
            if (contextBuffer == null) contextBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, 32 * globalWorkSizeArray[0]);
            if (inputBuffer == null) inputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 112);
            if (outputBuffer == null) outputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, outputSize);

            fixed (long* globalWorkOffsetArrayPtr = globalWorkOffsetArray)
            fixed (long* globalWorkSizeArrayPtr = globalWorkSizeArray)
            fixed (long* localWorkSizeArrayPtr = localWorkSizeArray)
            fixed (byte* inputPtr = input)
            fixed (UInt32* outputPtr = output)
            while (!Stopped)
            {
                MarkAsAlive();

                try
                {
                    searchKernel0.SetMemoryArgument(0, inputBuffer);
                    searchKernel0.SetMemoryArgument(1, contextBuffer);

                    searchKernel1.SetMemoryArgument(0, contextBuffer);

                    searchKernel2.SetMemoryArgument(0, contextBuffer);
                    searchKernel2.SetMemoryArgument(1, outputBuffer);

                    combinedSearchKernel.SetMemoryArgument(0, inputBuffer);
                    combinedSearchKernel.SetMemoryArgument(1, outputBuffer);

                    // Wait for the first job to arrive.
                    int elapsedTime = 0;
                    while ((mStratum == null || mStratum.GetJob() == null) && elapsedTime < 5000)
                    {
                        Thread.Sleep(10);
                        elapsedTime += 10;
                    }
                    if (mStratum == null || mStratum.GetJob() == null)
                        throw new TimeoutException("Stratum server failed to send a new job.");

                    System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                    LbryStratum.Work work;

                    while (!Stopped && (work = mStratum.GetWork()) != null)
                    {
                        MarkAsAlive();

                        var job = work.Job;
                        Array.Copy(work.Blob, input, 112);
                        byte localExtranonce = work.LocalExtranonce;
                        UInt32 startNonce = (UInt32)(r.Next(0, int.MaxValue));
                        UInt64 target = (UInt64) ((double) 0xffff0000UL / (mStratum.Difficulty / 256));
                        searchKernel2.SetValueArgument<UInt64>(2, target);
                        combinedSearchKernel.SetValueArgument<UInt64>(2, target);

                        Queue.Write<byte>(inputBuffer, true, 0, 112, (IntPtr)inputPtr, null);

                        consoleUpdateStopwatch.Start();

                        while (!Stopped && mStratum.GetJob().Equals(job))
                        {
                            MarkAsAlive();

                            globalWorkOffsetArray[0] = startNonce;

                            // Get a new local extranonce if necessary.
                            if (0xffffffffu - startNonce < (UInt32)globalWorkSizeArray[0])
                                break;

                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();
                            output[255] = 0; // output[255] is used as an atomic counter.
                            Queue.Write<UInt32>(outputBuffer, true, 0, outputSize, (IntPtr)outputPtr, null);

                            /*
                            Queue.Execute(searchKernel0, globalWorkOffsetArray, globalWorkSizeArray, localWorkSizeArray, null);
                            Queue.Finish(); 
                            if (Stopped)
                                break;
                            Queue.Execute(searchKernel1, globalWorkOffsetArray, globalWorkSizeArray, localWorkSizeArray, null);
                            Queue.Finish();
                            if (Stopped)
                                break;
                            Queue.Execute(searchKernel2, globalWorkOffsetArray, globalWorkSizeArray, localWorkSizeArray, null);
                            Queue.Finish(); 
                            if (Stopped)
                                break;
                            */
                            Queue.Execute(combinedSearchKernel, globalWorkOffsetArray, globalWorkSizeArray, localWorkSizeArray, null);

                            Queue.Read<UInt32>(outputBuffer, true, 0, outputSize, (IntPtr)outputPtr, null);
                            sw.Stop();
                            mSpeed = ((double)globalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                            if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                            {
                                MainForm.Logger("Device #" + DeviceIndex + " (Lbry): " + String.Format("{0:N2} Mh/s", mSpeed / 1000000));
                                consoleUpdateStopwatch.Restart();
                            }
                            if (mStratum.GetJob().Equals(job))
                            {
                                for (int i = 0; i < output[255]; ++i)
                                {
                                    String result = "";
                                    for (int j = 0; j < 8; ++j)
                                    {
                                        UInt32 word = output[256 + i * 8 + j];
                                        result += String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((word >> 0) & 0xff), ((word >> 8) & 0xff), ((word >> 16) & 0xff), ((word >> 24) & 0xff));
                                    }
                                    mStratum.Submit(GatelessGateDevice, work, output[i], result);
                                }
                            }
                            startNonce += (UInt32)globalWorkSizeArray[0];
                            IncrementKernelExecutionCount();
                            WaitForDualMiningPair();
                        }
                    }
                } catch (Exception ex) {
                    MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                    MainForm.Logger("Restarting miner thread...");
                }

                mSpeed = 0;

                if (!Stopped)
                    System.Threading.Thread.Sleep(5000);
            }
            MarkAsDone();
        }
    }
}

