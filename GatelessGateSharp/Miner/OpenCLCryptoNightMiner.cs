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
        private CryptoNightStratum mStratum;
        private Thread mMinerThread = null;
        private long mLocalWorkSize;
        private long mGlobalWorkSize;
        public bool mNicehashMode = false;

        public bool NiceHashMode { get { return mNicehashMode; } }

        public OpenCLCryptoNightMiner(Device aGatelessGateDevice, CryptoNightStratum aStratum, bool aNicehashMode = false)
            : base(aGatelessGateDevice, "CrypoNight")
        {
            mStratum = aStratum;
            mLocalWorkSize = (Device.Vendor == "AMD" ? 4 : 4);
            mGlobalWorkSize = (Device.Vendor == "AMD"    && Device.Name == "Radeon RX 480")         ? (16 * Device.MaxComputeUnits) :
                              (Device.Vendor == "AMD"    && Device.Name == "Radeon R9 Fury X/Nano") ? (16 * Device.MaxComputeUnits) :
                              (Device.Vendor == "NVIDIA" && Device.Name == "GeForce GTX 1080 Ti")   ? (16 * Device.MaxComputeUnits) :
                                                                                                      (16 * Device.MaxComputeUnits);
            mNicehashMode = aNicehashMode;
            StartMinerThread();
        }

        override unsafe protected void MinerThread()
        {
            ComputeBuffer<byte> scratchpadsBuffer = null;
            ComputeBuffer<UInt32>[] branchBuffers = null;
            ComputeBuffer<byte> statesBuffer = null;
            ComputeBuffer<byte> inputBuffer = null;
            ComputeBuffer<UInt32> outputBuffer = null;
            Random r = new Random();

            MarkAsAlive();

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");
            MainForm.Logger("NiceHash mode is " + (NiceHashMode ? "on" : "off") + ".");

            mProgram = new ComputeProgram(this.Context, System.IO.File.ReadAllText(@"Kernels\cryptonight.cl"));
            MainForm.Logger("Loaded cryptonight program for Device #" + DeviceIndex + ".");
            List<ComputeDevice> deviceList = new List<ComputeDevice>();
            deviceList.Add(Device.GetComputeDevice());
            try
            {
                String options = (Device.Vendor == "AMD" ? "-O1 " :
                                  Device.Vendor == "NVIDIA" ? "" :
                                  "")
                               + " -IKernels -DWORKSIZE=" + mLocalWorkSize;
                mProgram.Build(deviceList, options, null, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                MainForm.Logger(mProgram.GetBuildLog(Device.GetComputeDevice()));
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

            while (!Stopped)
            {
                MarkAsAlive();

                try
                {
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
                    CryptoNightStratum.Work work;
                    scratchpadsBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, ((long)1 << 21) * mGlobalWorkSize);
                    branchBuffers = new ComputeBuffer<UInt32>[] {
                        new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2),
                        new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2),
                        new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2),
                        new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, mGlobalWorkSize + 2)
                    };
                    statesBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, 200 + mGlobalWorkSize);
                    inputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, 76);
                    outputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, 256 + 255 * 8);
                    UInt32[] output = new UInt32[256 + 255 * 8];
                    UInt32[] branchBufferCount = new UInt32[1];
                    while (!Stopped && (work = mStratum.GetWork()) != null)
                    {
                        MarkAsAlive();

                        var job = work.GetJob();
                        byte[] blobByteArray = Utilities.StringToByteArray(job.Blob);
                        byte localExtranonce = work.LocalExtranonce;
                        byte[] targetByteArray = Utilities.StringToByteArray(job.Target);
                        UInt32 startNonce;
                        if (NiceHashMode)
                        {
                            startNonce = ((UInt32)blobByteArray[42] << (8 * 3)) | ((UInt32)localExtranonce << (8 * 2)) | (UInt32)(r.Next(0, int.MaxValue) & (0x0000ffffu));
                        }
                        else
                        {
                            startNonce = ((UInt32)localExtranonce << (8 * 3)) | (UInt32)(r.Next(0, int.MaxValue) & (0x00ffffffu));
                        }
                        UInt32 target = ((UInt32)targetByteArray[0] << 0)
                                        | ((UInt32)targetByteArray[1] << 8)
                                        | ((UInt32)targetByteArray[2] << 16)
                                        | ((UInt32)targetByteArray[3] << 24);

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

                        mSearchKernels[4].SetMemoryArgument(0, statesBuffer);
                        mSearchKernels[4].SetMemoryArgument(1, branchBuffers[1]);
                        mSearchKernels[4].SetMemoryArgument(2, outputBuffer);

                        mSearchKernels[5].SetMemoryArgument(0, statesBuffer);
                        mSearchKernels[5].SetMemoryArgument(1, branchBuffers[2]);
                        mSearchKernels[5].SetMemoryArgument(2, outputBuffer);

                        mSearchKernels[6].SetMemoryArgument(0, statesBuffer);
                        mSearchKernels[6].SetMemoryArgument(1, branchBuffers[3]);
                        mSearchKernels[6].SetMemoryArgument(2, outputBuffer); 
                        
                        consoleUpdateStopwatch.Start();

                        while (!Stopped && mStratum.GetJob().ID == job.ID && mStratum.GetJob().Blob == job.Blob && mStratum.GetJob().Target == job.Target)
                        {
                            MarkAsAlive();

                            // Get a new local extranonce if necessary.
                            if (NiceHashMode)
                            {
                                if ((startNonce & 0xffff) + (UInt32)mGlobalWorkSize >= 0x10000)
                                    break;
                            }
                            else
                            {
                                if ((startNonce & 0xffffff) + (UInt32)mGlobalWorkSize >= 0x1000000)
                                    break;
                            }

                            mSearchKernels[3].SetValueArgument<UInt32>(3, target);
                            mSearchKernels[4].SetValueArgument<UInt32>(3, target);
                            mSearchKernels[5].SetValueArgument<UInt32>(3, target);
                            mSearchKernels[6].SetValueArgument<UInt32>(3, target);

                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();
                            UInt32[] zero = new UInt32[mGlobalWorkSize + 2];
                            for (int i = 0; i < mGlobalWorkSize + 2; ++i)
                                zero[i] = 0;
                            fixed (UInt32* p = zero)
                                for (int i = 0; i < 4; ++i)
                                    Queue.Write<UInt32>(branchBuffers[i], true, 0, mGlobalWorkSize + 2, (IntPtr)p, null);
                            fixed (UInt32* p = output)
                            {
                                output[255] = 0; // output[255] is used as an atomic counter.
                                Queue.Write<UInt32>(outputBuffer, true, 0, 256 + 255 * 8, (IntPtr)p, null);
                                Queue.Execute(mSearchKernels[0], new long[] { startNonce, 1 }, new long[] { mGlobalWorkSize, 8 }, new long[] { mLocalWorkSize, 8 }, null);
                                Queue.Execute(mSearchKernels[1], new long[] { startNonce }, new long[] { mGlobalWorkSize }, new long[] { mLocalWorkSize }, null);
                                Queue.Execute(mSearchKernels[2], new long[] { startNonce, 1 }, new long[] { mGlobalWorkSize, 8 }, new long[] { mLocalWorkSize, 8 }, null);
                                for (int i = 0; i < 4; ++i)
                                {
                                    fixed (UInt32* q = branchBufferCount)
                                        Queue.Read<UInt32>(branchBuffers[i], true, mGlobalWorkSize, 1, (IntPtr)q, null);
                                    mSearchKernels[i + 3].SetValueArgument<UInt64>(4, branchBufferCount[0]);
                                    if ((branchBufferCount[0] % (ulong)mLocalWorkSize) != 0)
                                        branchBufferCount[0] += (uint)mLocalWorkSize - branchBufferCount[0] % (uint)mLocalWorkSize;
                                    Queue.Execute(mSearchKernels[i + 3], new long[] { startNonce }, new long[] { branchBufferCount[0] }, new long[] { mLocalWorkSize }, null);
                                    Queue.Finish(); // Run the above statement before leaving the current local scope.
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
                                mStratum.Submit(GatelessGateDevice, job, output[i], result);
                            }
                            startNonce += (UInt32)mGlobalWorkSize;
                        }
                    }
                } catch (Exception ex) {
                    MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                    MainForm.Logger("Restarting miner thread...");
                }

                try { if (scratchpadsBuffer != null) scratchpadsBuffer.Dispose(); } catch (Exception ex) { }
                try { if (branchBuffers != null) foreach (var buffer in branchBuffers) buffer.Dispose(); } catch (Exception ex) { }
                try { if (statesBuffer != null) statesBuffer.Dispose(); } catch (Exception ex) { }
                try { if (inputBuffer != null) inputBuffer.Dispose(); } catch (Exception ex) { }
                try { if (outputBuffer != null) outputBuffer.Dispose(); } catch (Exception ex) { }
 
                scratchpadsBuffer = null;
                branchBuffers = null;
                statesBuffer = null;
                inputBuffer = null;
                outputBuffer = null;

                mSpeed = 0;

                if (!Stopped)
                    System.Threading.Thread.Sleep(5000);
            }

            foreach (var kernel in mSearchKernels)
                kernel.Dispose();
            mProgram.Dispose();
            Dispose();

            MarkAsDone();
        }
    }
}
