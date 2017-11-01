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
    class OpenCLCryptoNightMiner : OpenCLMiner
    {
        private ComputeProgram mProgram;
        private ComputeKernel[] mSearchKernels = new ComputeKernel[7];
        private NiceHashCryptoNightStratum mStratum;
        private Thread mMinerThread = null;
        private long mLocalWorkSize = 32;
        private long mGlobalWorkSize;

        public OpenCLCryptoNightMiner(Device aGatelessGateDevice, NiceHashCryptoNightStratum aStratum)
            : base(aGatelessGateDevice, "CrypoNight")
        {
            mStratum = aStratum;
            mGlobalWorkSize = mLocalWorkSize * Device.MaxComputeUnits;

            mProgram = new ComputeProgram(this.Context, System.IO.File.ReadAllText(@"Kernels\cryptonight.cl"));
            MainForm.Logger("Loaded cryptonight program for Device #" + DeviceIndex + ".");
            List<ComputeDevice> deviceList = new List<ComputeDevice>();
            deviceList.Add(Device);
            try
            {
                String options = (Device.Vendor == "Advanced Micro Devices, Inc." ? "-O1 " : "") + "-IKernels -DWORKSIZE=" + mLocalWorkSize;
                mProgram.Build(deviceList, options, null, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                MainForm.Logger(mProgram.GetBuildLog(Device));
                throw ex;
            }
            MainForm.Logger("Built cryptonight program for Device #" + DeviceIndex + ".");
            mSearchKernels[0] = mProgram.CreateKernel("search");
            mSearchKernels[1] = mProgram.CreateKernel("search1");
            mSearchKernels[2] = mProgram.CreateKernel("search2");
            mSearchKernels[3] = mProgram.CreateKernel("search3");
            mSearchKernels[4] = mProgram.CreateKernel("search4");
            mSearchKernels[5] = mProgram.CreateKernel("search5");
            mSearchKernels[6] = mProgram.CreateKernel("search6");

            mMinerThread = new Thread(new ThreadStart(MinerThread));
            mMinerThread.IsBackground = true;
            mMinerThread.Start();
        }

        unsafe public void MinerThread()
        {
            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

            // Wait for the first job to arrive.
            int timePassed = 0;
            while ((mStratum == null || mStratum.GetJob() == null) && timePassed < 5000)
            {
                Thread.Sleep(10);
                timePassed += 10;
            }
            if (mStratum == null || mStratum.GetJob() == null)
            {
                MainForm.Logger("Stratum server failed to send a new job.");
                //throw new TimeoutException("Stratum server failed to send a new job.");
                return;
            }

            System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
            NiceHashCryptoNightStratum.Work work;
            ComputeBuffer<byte> inputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 76);
            ComputeBuffer<UInt32> outputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, 256 + 255 * 8);
            ComputeBuffer<byte> scratchpadsBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, ((long)1 << 21) * mGlobalWorkSize);
            ComputeBuffer<byte> statesBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, 200 + mGlobalWorkSize);
            ComputeBuffer<UInt32>[] branchBuffers = new ComputeBuffer<UInt32>[] {
                new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2),
                new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2),
                new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2),
                new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2)
            };
            UInt32[] output = new UInt32[256 + 255 * 8];
            UInt64[] branchBufferCount = new UInt64[1];
            while (!Stopped && (work = mStratum.GetWork()) != null)
            {
                byte[] blobByteArray = Utilities.StringToByteArray(work.GetJob().Blob);
                byte localExtranonce = work.LocalExtranonce;
                String jobID = work.GetJob().ID;
                byte[] targetByteArray = Utilities.StringToByteArray(work.GetJob().Target);
                UInt32 startNonce = ((UInt32)blobByteArray[42] << (8 * 3)) | ((UInt32)localExtranonce << (8 * 2));
                UInt32 target =   ((UInt32)targetByteArray[0] << 0) 
                                | ((UInt32)targetByteArray[1] << 8)
                                | ((UInt32)targetByteArray[2] << 16)
                                | ((UInt32)targetByteArray[3] << 24);

                consoleUpdateStopwatch.Start();

                while (!Stopped && mStratum.GetJob().ID.Equals(jobID))
                {
                    // Get a new local extranonce if necessary.
                    if ((startNonce & 0xffff) + (UInt32)mGlobalWorkSize >= 0x10000)
                        break;

                    blobByteArray[39] = (byte)((startNonce >> 0) & 0xff);
                    blobByteArray[40] = (byte)((startNonce >> 8) & 0xff);
                    blobByteArray[41] = (byte)localExtranonce;
                    fixed (byte* p = blobByteArray)
                        Queue.Write<byte>(inputBuffer, true, 0, 76, (IntPtr)p, null);

                    mSearchKernels[0].SetMemoryArgument(0, inputBuffer);
                    mSearchKernels[0].SetMemoryArgument(1, scratchpadsBuffer);
                    mSearchKernels[0].SetMemoryArgument(2, statesBuffer);

                    mSearchKernels[1].SetMemoryArgument(0, scratchpadsBuffer);
                    mSearchKernels[1].SetMemoryArgument(1, statesBuffer);

                    mSearchKernels[2].SetMemoryArgument(0, scratchpadsBuffer);
                    mSearchKernels[2].SetMemoryArgument(1, statesBuffer);
                    mSearchKernels[2].SetMemoryArgument(2, branchBuffers[0]);
                    mSearchKernels[2].SetMemoryArgument(3, branchBuffers[1]);
                    mSearchKernels[2].SetMemoryArgument(4, branchBuffers[2]);
                    mSearchKernels[2].SetMemoryArgument(5, branchBuffers[3]);

                    mSearchKernels[3].SetMemoryArgument(0, statesBuffer);
                    mSearchKernels[3].SetMemoryArgument(1, branchBuffers[0]);
                    mSearchKernels[3].SetMemoryArgument(2, outputBuffer);
                    mSearchKernels[3].SetValueArgument<UInt32>(3, target);

                    mSearchKernels[4].SetMemoryArgument(0, statesBuffer);
                    mSearchKernels[4].SetMemoryArgument(1, branchBuffers[1]);
                    mSearchKernels[4].SetMemoryArgument(2, outputBuffer);
                    mSearchKernels[4].SetValueArgument<UInt32>(3, target);

                    mSearchKernels[5].SetMemoryArgument(0, statesBuffer);
                    mSearchKernels[5].SetMemoryArgument(1, branchBuffers[2]);
                    mSearchKernels[5].SetMemoryArgument(2, outputBuffer);
                    mSearchKernels[5].SetValueArgument<UInt32>(3, target);

                    mSearchKernels[6].SetMemoryArgument(0, statesBuffer);
                    mSearchKernels[6].SetMemoryArgument(1, branchBuffers[3]);
                    mSearchKernels[6].SetMemoryArgument(2, outputBuffer);
                    mSearchKernels[6].SetValueArgument<UInt32>(3, target);

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    UInt32[] zero = new UInt32[mGlobalWorkSize + 2];
                    for (int i = 0; i < mGlobalWorkSize + 2; ++i)
                        zero[i] = 0;
                    fixed (UInt32* p = zero)
                        for (int i = 0; i < 4; ++i)
                            Queue.Write<UInt32>(branchBuffers[i], false, 0, mGlobalWorkSize + 2, (IntPtr)p, null);
                    fixed (UInt32* p = output)
                    {
                        output[255] = 0; // output[255] is used as an atomic counter.
                        Queue.Write<UInt32>(outputBuffer, false, 0, 256 + 255 * 8, (IntPtr)p, null);
                        Queue.Execute(mSearchKernels[0], new long[] { startNonce, 1 }, new long[] { mGlobalWorkSize, 8 }, new long[] { mLocalWorkSize, 8 }, null);
                        Queue.Execute(mSearchKernels[1], new long[] { 0 }, new long[] { mGlobalWorkSize }, new long[] { mLocalWorkSize }, null);
                        Queue.Execute(mSearchKernels[2], new long[] { startNonce, 1 }, new long[] { mGlobalWorkSize, 8 }, new long[] { mLocalWorkSize, 8 }, null);
                        for (int i = 0; i < 4; ++i)
                        {
                            fixed (UInt64* q = branchBufferCount)
                                Queue.Read<UInt32>(branchBuffers[i], true, mGlobalWorkSize, 2, (IntPtr)q, null);
                            if ((branchBufferCount[0] % (ulong)mLocalWorkSize) != 0)
                                branchBufferCount[0] += (ulong)mLocalWorkSize - branchBufferCount[0] % (ulong)mLocalWorkSize;
                            mSearchKernels[3 + i].SetValueArgument<UInt64>(4, branchBufferCount[0]);
                            Queue.Execute(mSearchKernels[i + 3], new long[] { 0 }, new long[] { mGlobalWorkSize }, new long[] { mLocalWorkSize }, null);
                        }
                        Queue.Read<UInt32>(outputBuffer, true, 0, 256 + 255 * 8, (IntPtr)p, null);
                    }
                    sw.Stop();
                    mSpeed = ((double)mGlobalWorkSize) / sw.Elapsed.TotalSeconds;
                    if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                    {
                        MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} h/s", mSpeed));
                        consoleUpdateStopwatch.Restart();
                    }
                    for (int i = 0; i < output[255]; ++i)
                    {
                        String result = "";
                        for (int j = 0; j < 8; ++j)
                        {
                            UInt32 word = output[256 + i * 8 + j];
                            result += String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((word >> 0) & 0xff), ((word >> 8) & 0xff), ((word >> 16) & 0xff), ((word >> 24) & 0xff));
                        }
                        mStratum.Submit(GatelessGateDevice, work.GetJob(), startNonce + output[i], result);

                    }
                    startNonce += (UInt32)mGlobalWorkSize;
                }
            }

            inputBuffer.Dispose();
            outputBuffer.Dispose();
            scratchpadsBuffer.Dispose();
            foreach (var buffer in branchBuffers)
                buffer.Dispose();
            mSpeed = 0;
        }
    }
}
