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

        static Mutex mProgramArrayMutex = new Mutex();

        static Dictionary<ProgramArrayIndex, ComputeProgram> mNeoScryptProgramArray = new Dictionary<ProgramArrayIndex, ComputeProgram>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mNeoScryptSearchKernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        ComputeProgram mNeoScryptProgram;
        ComputeKernel mNeoScryptSearchKernel = null;
        private ComputeBuffer<byte> mNeoScryptInputBuffer = null;
        private ComputeBuffer<byte> mNeoScryptGlobalCacheBuffer = null;
        private ComputeBuffer<UInt32> mNeoScryptOutputBuffer = null;
        private NeoScryptStratum mNeoScryptStratum;
        long[] mNeoScryptGlobalWorkSizeArray = new long[] { 0 };
        long[] mNeoScryptLocalWorkSizeArray = new long[] { 0 };
        long[] mNeoScryptGlobalWorkOffsetArray = new long[] { 0 };
        UInt32[] mNeoScryptOutput = new UInt32[sNeoScryptOutputSize];
        byte[] mNeoScryptInput = new byte[sNeoScryptInputSize];
          


        public OpenCLNeoScryptMiner(Device aGatelessGateDevice)
            : base(aGatelessGateDevice, "NeoScrypt")
        {
            mNeoScryptInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, sNeoScryptInputSize);
            mNeoScryptOutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, sNeoScryptOutputSize);
        }

        public void Start(NeoScryptStratum aNeoScryptStratum, int aNeoScryptIntensity, int aNeoScryptLocalWorkSize)
        {
            mNeoScryptStratum = aNeoScryptStratum;
            mNeoScryptGlobalWorkSizeArray[0] = aNeoScryptIntensity * Device.MaxComputeUnits;
            mNeoScryptLocalWorkSizeArray[0] = aNeoScryptLocalWorkSize;
            if (mNeoScryptGlobalWorkSizeArray[0] % aNeoScryptLocalWorkSize != 0)
                mNeoScryptGlobalWorkSizeArray[0] = aNeoScryptLocalWorkSize - mNeoScryptGlobalWorkSizeArray[0] % aNeoScryptLocalWorkSize;

            base.Start();
        }

        public void BuildNeoScryptProgram()
        {
            ComputeDevice computeDevice = Device.GetComputeDevice();

            try { mProgramArrayMutex.WaitOne(5000); } catch (Exception) { }

            if (mNeoScryptProgramArray.ContainsKey(new ProgramArrayIndex(DeviceIndex, mNeoScryptLocalWorkSizeArray[0])))
            {
                mNeoScryptProgram = mNeoScryptProgramArray[new ProgramArrayIndex(DeviceIndex, mNeoScryptLocalWorkSizeArray[0])];
                mNeoScryptSearchKernel = mNeoScryptSearchKernelArray[new ProgramArrayIndex(DeviceIndex, mNeoScryptLocalWorkSizeArray[0])];
            }
            else
            {
                String source = System.IO.File.ReadAllText(@"Kernels\neoscrypt.cl");
                mNeoScryptProgram = new ComputeProgram(Context, source);
                MainForm.Logger(@"Loaded Kernels\neoscrypt.cl for Device #" + DeviceIndex + ".");
                String buildOptions = (Device.Vendor == "AMD" ? "-O5 " : // "-legacy" : //"-O1 " :
                                       Device.Vendor == "NVIDIA" ? "" : //"-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + mNeoScryptLocalWorkSizeArray[0];
                try
                {
                    mNeoScryptProgram.Build(Device.DeviceList, buildOptions, null, IntPtr.Zero);
                }
                catch (Exception)
                {
                    MainForm.Logger(mNeoScryptProgram.GetBuildLog(computeDevice));
                    throw;
                }
                MainForm.Logger("Built NeoScrypt program for Device #" + DeviceIndex + ".");
                MainForm.Logger("Built options: " + buildOptions);
                mNeoScryptProgramArray[new ProgramArrayIndex(DeviceIndex, mNeoScryptLocalWorkSizeArray[0])] = mNeoScryptProgram;
                mNeoScryptSearchKernelArray[new ProgramArrayIndex(DeviceIndex, mNeoScryptLocalWorkSizeArray[0])] = mNeoScryptSearchKernel = mNeoScryptProgram.CreateKernel("search");
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

            BuildNeoScryptProgram();

            fixed (long* neoscryptGlobalWorkOffsetArrayPtr = mNeoScryptGlobalWorkOffsetArray)
            fixed (long* neoscryptGlobalWorkSizeArrayPtr = mNeoScryptGlobalWorkSizeArray)
            fixed (long* neoscryptLocalWorkSizeArrayPtr = mNeoScryptLocalWorkSizeArray)
            fixed (byte* neoscryptInputPtr = mNeoScryptInput)
            fixed (UInt32* neoscryptOutputPtr = mNeoScryptOutput)
                using (mNeoScryptGlobalCacheBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, (mNeoScryptGlobalWorkSizeArray[0] * 32768)))
                    while (!Stopped) {
                        MarkAsAlive();

                        try {
                            mNeoScryptSearchKernel.SetMemoryArgument(0, mNeoScryptInputBuffer);
                            mNeoScryptSearchKernel.SetMemoryArgument(1, mNeoScryptOutputBuffer);
                            mNeoScryptSearchKernel.SetMemoryArgument(2, mNeoScryptGlobalCacheBuffer);

                            // Wait for the first NeoScryptJob to arrive.
                            int elapsedTime = 0;
                            while ((mNeoScryptStratum == null || mNeoScryptStratum.GetJob() == null) && elapsedTime < 5000) {
                                Thread.Sleep(10);
                                elapsedTime += 10;
                            }
                            if (mNeoScryptStratum == null || mNeoScryptStratum.GetJob() == null)
                                throw new TimeoutException("Stratum server failed to send a new NeoScryptJob.");

                            System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                            NeoScryptStratum.Work neoscryptWork;

                            while (!Stopped && (neoscryptWork = mNeoScryptStratum.GetWork()) != null) {
                                MarkAsAlive();

                                var neoscryptJob = neoscryptWork.Job;
                                Array.Copy(neoscryptWork.Blob, mNeoScryptInput, sNeoScryptInputSize);
                                UInt32 neoscryptStartNonce = (UInt32)(r.Next(0, int.MaxValue));
                                Queue.Write<byte>(mNeoScryptInputBuffer, true, 0, sNeoScryptInputSize, (IntPtr)neoscryptInputPtr, null);

                                consoleUpdateStopwatch.Start();

                                while (!Stopped && mNeoScryptStratum.GetJob().Equals(neoscryptJob)) {
                                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                    sw.Start();

                                    MarkAsAlive();

                                    UInt32 NeoScryptTarget = (UInt32)((double)0xffff0000U / (mNeoScryptStratum.Difficulty * 65536));
                                    mNeoScryptSearchKernel.SetValueArgument<UInt32>(3, NeoScryptTarget);
                                    mNeoScryptGlobalWorkOffsetArray[0] = neoscryptStartNonce;

                                    // Get a new local extranonce if necessary.
                                    if (0xffffffffu - neoscryptStartNonce < (UInt32)mNeoScryptGlobalWorkSizeArray[0])
                                        break;

                                    mNeoScryptOutput[255] = 0; // mNeoScryptOutput[255] is used as an atomic counter.
                                    Queue.Write<UInt32>(mNeoScryptOutputBuffer, true, 0, sNeoScryptOutputSize, (IntPtr)neoscryptOutputPtr, null);
                                    Queue.Execute(mNeoScryptSearchKernel, mNeoScryptGlobalWorkOffsetArray, mNeoScryptGlobalWorkSizeArray, mNeoScryptLocalWorkSizeArray, null);
                                    Queue.Read<UInt32>(mNeoScryptOutputBuffer, true, 0, sNeoScryptOutputSize, (IntPtr)neoscryptOutputPtr, null);
                                    if (mNeoScryptStratum.GetJob().Equals(neoscryptJob)) {
                                        for (int i = 0; i < mNeoScryptOutput[255]; ++i)
                                            mNeoScryptStratum.Submit(GatelessGateDevice, neoscryptWork, mNeoScryptOutput[i]);
                                    }
                                    neoscryptStartNonce += (UInt32)mNeoScryptGlobalWorkSizeArray[0];

                                    sw.Stop();
                                    Speed = ((double)mNeoScryptGlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                                    if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000) {
                                        MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Kh/s (NeoScrypt)", Speed / 1000));
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

