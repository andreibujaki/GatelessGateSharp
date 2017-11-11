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
        private readonly int outputSize = 256 + 255 * 8;

        private readonly CryptoNightStratum mStratum;
        private readonly long mLocalWorkSize;
        private readonly long mGlobalWorkSize;
        private readonly bool mNicehashMode = false;

        private Thread mMinerThread = null;

        public bool NiceHashMode { get { return mNicehashMode; } }

        public OpenCLCryptoNightMiner(Device aGatelessGateDevice, CryptoNightStratum aStratum, int aIntensity, int aLocalWorkSize, bool aNicehashMode = false)
            : base(aGatelessGateDevice, "CryptoNight")
        {
            mStratum = aStratum;
            mLocalWorkSize = aLocalWorkSize;
            mGlobalWorkSize = aIntensity * Device.MaxComputeUnits;
            if (mGlobalWorkSize % mLocalWorkSize != 0)
                mGlobalWorkSize = mLocalWorkSize - mGlobalWorkSize % mLocalWorkSize;
            mNicehashMode = aNicehashMode;
            StartMinerThread();
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
[System.Security.SecurityCritical]
override unsafe protected void MinerThread()
        {
            Random r = new Random();

            long[] globalWorkSizeA = new long[] { mGlobalWorkSize, 8 };
            long[] globalWorkSizeB = new long[] { mGlobalWorkSize };

            long[] localWorkSizeA = new long[] { mLocalWorkSize, 8 };
            long[] localWorkSizeB = new long[] { mLocalWorkSize };

            MarkAsAlive();

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");
            MainForm.Logger("NiceHash mode is " + (NiceHashMode ? "on" : "off") + ".");

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

                    String source = System.IO.File.ReadAllText(@"Kernels\cryptonight.cl");
List<ComputeDevice> deviceList = new List<ComputeDevice>();
                    var computeDevice = Device.GetNewComputeDevice();
                    deviceList.Add(computeDevice);
                    var contextProperties = new ComputeContextPropertyList(computeDevice.Platform);

                    using (ComputeContext context = new ComputeContext(deviceList, contextProperties, null, IntPtr.Zero))
                    using (ComputeCommandQueue queue = new ComputeCommandQueue(context, computeDevice, ComputeCommandQueueFlags.None))
                    using (ComputeProgram program = new ComputeProgram(context, source))
                    { 
                        MainForm.Logger("Loaded cryptonight program for Device #" + DeviceIndex + ".");

                        String buildOptions = (Device.Vendor == "AMD" ? "-O1 " :
                                               Device.Vendor == "NVIDIA" ? "-cl-nv-opt-level=4 -cl-nv-maxrregcount=128 " :
                                               "")
                                             + " -IKernels -DWORKSIZE=" + mLocalWorkSize;
                        try
                        {
                            program.Build(Device.DeviceList, buildOptions, null, IntPtr.Zero);
                        }
                        catch (Exception)
                        {
                            MainForm.Logger(program.GetBuildLog(computeDevice));
                            throw;
                        }
                        MainForm.Logger("Built cryptonight program for Device #" + DeviceIndex + ".");
                        MainForm.Logger("Built options: " + buildOptions);

                        using (ComputeKernel searchKernel0 = program.CreateKernel("search"))
                        using (ComputeKernel searchKernel1 = program.CreateKernel("search1"))
                        using (ComputeKernel searchKernel2 = program.CreateKernel("search2"))
                        using (ComputeKernel searchKernel3 = program.CreateKernel("search3"))
                        using (ComputeBuffer<byte> scratchpadsBuffer = new ComputeBuffer<byte>(context, ComputeMemoryFlags.ReadWrite, ((long)1 << 21) * mGlobalWorkSize))
                        using (ComputeBuffer<byte> statesBuffer = new ComputeBuffer<byte>(context, ComputeMemoryFlags.ReadWrite, 200 + mGlobalWorkSize))
                        using (ComputeBuffer<byte> inputBuffer = new ComputeBuffer<byte>(context, ComputeMemoryFlags.ReadOnly, 76))
                        using (ComputeBuffer<UInt32> outputBuffer = new ComputeBuffer<UInt32>(context, ComputeMemoryFlags.ReadWrite, outputSize))
                        using (ComputeBuffer<Int32> terminateBuffer = new ComputeBuffer<Int32>(context, ComputeMemoryFlags.ReadWrite, 1))
                        { 
                            System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                            CryptoNightStratum.Work work;
                            Int32[] terminate = new Int32[1];
                            UInt32[] output = new UInt32[outputSize];

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
                                searchKernel3.SetValueArgument<UInt32>(2, target);

                                fixed (byte* p = blobByteArray)
                                    queue.Write<byte>(inputBuffer, true, 0, 76, (IntPtr)p, null);

                                consoleUpdateStopwatch.Start();

                                while (!Stopped && mStratum.GetJob().Equals(job))
                                {
                                    MarkAsAlive();

                                    long[] globalWorkOffsetA = new long[] { startNonce, 1 };
                                    long[] globalWorkOffsetB = new long[] { startNonce };

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

                                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                    sw.Start();
                                    output[255] = 0; // output[255] is used as an atomic counter.
                                    fixed (UInt32* p = output)
                                        queue.Write<UInt32>(outputBuffer, true, 0, outputSize, (IntPtr)p, null);
                                    terminate[0] = 0;
                                    fixed (Int32* p = terminate)
                                        queue.Write<Int32>(terminateBuffer, true, 0, 1, (IntPtr)p, null);
                                    ComputeEventList eventList = new ComputeEventList();
                                    queue.Execute(searchKernel0, globalWorkOffsetA, globalWorkSizeA, localWorkSizeA, null);
                                    queue.Finish(); 
                                    if (Stopped)
                                        break;
                                    queue.Execute(searchKernel1, globalWorkOffsetB, globalWorkSizeB, localWorkSizeB, eventList);
                                    queue.Flush();
                                    while (eventList[0].Status != ComputeCommandExecutionStatus.Complete)
                                    {
                                        if (Stopped && terminate[0] == 0)
                                        {
                                            terminate[0] = 1;
                                            fixed (Int32* p = terminate)
                                                queue.Write<Int32>(terminateBuffer, true, 0, 1, (IntPtr)p, null);
                                        }
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    eventList[0].Dispose();
                                    if (Stopped)
                                        break;
                                    queue.Execute(searchKernel2, globalWorkOffsetA, globalWorkSizeA, localWorkSizeA, null);
                                    queue.Finish(); 
                                    if (Stopped)
                                        break;
                                    queue.Execute(searchKernel3, globalWorkOffsetB, globalWorkSizeB, localWorkSizeB, null);
                                    queue.Finish(); // Run the above statement before leaving the current local scope.
                                    if (Stopped)
                                        break;
                                    fixed (UInt32* p = output)
                                        queue.Read<UInt32>(outputBuffer, true, 0, outputSize, (IntPtr)p, null);
                                    sw.Stop();
                                    mSpeed = ((double)mGlobalWorkSize) / sw.Elapsed.TotalSeconds;
                                    if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000)
                                    {
                                        MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} h/s", mSpeed));
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
                                            mStratum.Submit(GatelessGateDevice, job, output[i], result);
                                        }
                                    }
                                    startNonce += (UInt32)mGlobalWorkSize;
                                }
                            }
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
