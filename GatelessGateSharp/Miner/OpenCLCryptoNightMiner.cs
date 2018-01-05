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
    class OpenCLCryptoNightMiner : OpenCLMiner
    {
        private static readonly int outputSize = 256 + 255 * 8;

        private CryptoNightStratum mStratum;
        private bool mNicehashMode = false;

        long[] globalWorkSizeA = new long[] { 0, 8 };
        long[] globalWorkSizeB = new long[] { 0 };
        long[] localWorkSizeA = new long[] { 0, 8 };
        long[] localWorkSizeB = new long[] { 0 };
        long[] globalWorkOffsetA = new long[] { 0, 1 };
        long[] globalWorkOffsetB = new long[] { 0 };

        Int32[] terminate = new Int32[1];
        UInt32[] output = new UInt32[outputSize];
        byte[] input = new byte[76];

        static Mutex mProgramArrayMutex = new Mutex();

        static Dictionary<ProgramArrayIndex, ComputeProgram> mProgramArray = new Dictionary<ProgramArrayIndex, ComputeProgram>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mSearchKernel0Array = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mSearchKernel1Array = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mSearchKernel2Array = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mSearchKernel3Array = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        ComputeKernel searchKernel0 = null;
        ComputeKernel searchKernel1 = null;
        ComputeKernel searchKernel2 = null;
        ComputeKernel searchKernel3 = null;
            
        private ComputeBuffer<byte> statesBuffer = null;
        private ComputeBuffer<byte> inputBuffer = null;
        private ComputeBuffer<UInt32> outputBuffer = null;
        private ComputeBuffer<Int32> terminateBuffer = null;
        private ComputeBuffer<byte> scratchpadsBuffer = null;
            
        public bool NiceHashMode { get { return mNicehashMode; } }

        public OpenCLCryptoNightMiner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "CryptoNight")
        {
            try {
                inputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 76);
                outputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, outputSize);
                terminateBuffer = new ComputeBuffer<Int32>(Context, ComputeMemoryFlags.ReadWrite, 1);
            } catch (Exception ex) {
                throw new UnrecoverableException(ex, GatelessGateDevice);
            }
        }

        public void Start(CryptoNightStratum aStratum, int aRowIntensity, int aLocalWorkSize, bool aNicehashMode = false)
        {
            var prevGlobalWorkSize = globalWorkSizeA[0];

            mStratum = aStratum;
            globalWorkSizeA[0] = globalWorkSizeB[0] = aRowIntensity * aLocalWorkSize;
            localWorkSizeA[0] = localWorkSizeB[0] = aLocalWorkSize;
            mNicehashMode = aNicehashMode;

            if (prevGlobalWorkSize != 0 && prevGlobalWorkSize != globalWorkSizeA[0])
                Environment.Exit(1);

            try {
                if (statesBuffer == null) statesBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, 200 * globalWorkSizeA[0]);
                if (scratchpadsBuffer == null) scratchpadsBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, ((long)1 << 21) * globalWorkSizeA[0]);
            } catch (Exception ex) {
                throw new UnrecoverableException(ex, GatelessGateDevice);
            }

            base.Start();
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread()
        {
            Random r = new Random();
            ComputeDevice computeDevice = OpenCLDevice.GetComputeDevice();
            
            MarkAsAlive();

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");
            MainForm.Logger("NiceHash mode is " + (NiceHashMode ? "on" : "off") + ".");

            ComputeProgram program;
            try { mProgramArrayMutex.WaitOne(5000); } catch (Exception) { }
            if (mProgramArray.ContainsKey(new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])))
            {
                program = mProgramArray[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])];
                searchKernel0 = mSearchKernel0Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])];
                searchKernel1 = mSearchKernel1Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])];
                searchKernel2 = mSearchKernel2Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])];
                searchKernel3 = mSearchKernel3Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])];
            } 
            else
            {
                try
                {
                    if (localWorkSizeA[0] != 8)
                        throw new Exception("No suitable binary file was found.");
                    string fileName = @"BinaryKernels\" + computeDevice.Name + "_cryptonight.bin";
                    byte[] binary = System.IO.File.ReadAllBytes(fileName);
                    program = new ComputeProgram(Context, new List<byte[]>() { binary }, new List<ComputeDevice>() { computeDevice });
                    MainForm.Logger("Loaded " + fileName + " for Device #" + DeviceIndex + ".");
                }
                catch (Exception)
                {
                    String source = System.IO.File.ReadAllText(@"Kernels\cryptonight.cl");
                    program = new ComputeProgram(Context, source);
                    MainForm.Logger(@"Loaded Kernels\cryptonight.cl for Device #" + DeviceIndex + ".");
                }
                String buildOptions = (OpenCLDevice.GetVendor() == "AMD"    ? "-O5" : //"-O1 " :
                                       OpenCLDevice.GetVendor() == "NVIDIA" ? "" : //"-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + localWorkSizeA[0];
                try
                {
                    program.Build(OpenCLDevice.DeviceList, buildOptions, null, IntPtr.Zero);
                }
                catch (Exception)
                {
                    MainForm.Logger(program.GetBuildLog(computeDevice));
                    throw;
                }
                MainForm.Logger("Built CryptoNight program for Device #" + DeviceIndex + ".");
                MainForm.Logger("Build options: " + buildOptions);
                mProgramArray[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])] = program;
                mSearchKernel0Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])] = searchKernel0 = program.CreateKernel("search");
                mSearchKernel1Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])] = searchKernel1 = program.CreateKernel("search1");
                mSearchKernel2Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])] = searchKernel2 = program.CreateKernel("search2");
                mSearchKernel3Array[new ProgramArrayIndex(DeviceIndex, localWorkSizeA[0])] = searchKernel3 = program.CreateKernel("search3");
            }
            try { mProgramArrayMutex.ReleaseMutex(); } catch (Exception) { }

            fixed (long* globalWorkOffsetAPtr = globalWorkOffsetA)
            fixed (long* globalWorkOffsetBPtr = globalWorkOffsetB)
            fixed (long* globalWorkSizeAPtr = globalWorkSizeA)
            fixed (long* globalWorkSizeBPtr = globalWorkSizeB)
            fixed (long* localWorkSizeAPtr = localWorkSizeA)
            fixed (long* localWorkSizeBPtr = localWorkSizeB)
            fixed (Int32* terminatePtr = terminate)
            fixed (byte* inputPtr = input)
            fixed (UInt32* outputPtr = output) {
                while (!Stopped) {
                    MarkAsAlive();

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
                        while ((mStratum == null || mStratum.GetJob() == null) && elapsedTime < 5000) {
                            Thread.Sleep(10);
                            elapsedTime += 10;
                        }
                        if (mStratum == null || mStratum.GetJob() == null)
                            throw new TimeoutException("Stratum server failed to send a new job.");

                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        CryptoNightStratum.Work work;

                        while (!Stopped && (work = mStratum.GetWork()) != null) {
                            MarkAsAlive();

                            var job = work.GetJob();
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

                            while (!Stopped && mStratum.GetJob().Equals(job)) {
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
                                if (mStratum.GetJob().Equals(job)) {
                                    for (int i = 0; i < output[255]; ++i) {
                                        String result = "";
                                        for (int j = 0; j < 8; ++j) {
                                            UInt32 word = output[256 + i * 8 + j];
                                            result += String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((word >> 0) & 0xff), ((word >> 8) & 0xff), ((word >> 16) & 0xff), ((word >> 24) & 0xff));
                                        }
                                        mStratum.Submit(GatelessGateDevice, job, output[i], result);
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
                        MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                        MainForm.Logger("Restarting miner thread...");
                    }

                    Speed = 0;

                    if (!Stopped)
                        System.Threading.Thread.Sleep(5000);
                }
            }

            MarkAsDone();
        }
    }
}

