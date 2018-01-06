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
    class OpenCLLyra2REv2Miner : OpenCLMiner
    {
        public static readonly int sLyra2REv2InputSize = 80;
        public static readonly int sLyra2REv2OutputSize = 256;

        static Mutex mProgramArrayMutex = new Mutex();

        static Dictionary<ProgramArrayIndex, ComputeProgram> mLyra2REv2ProgramArray = new Dictionary<ProgramArrayIndex, ComputeProgram>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2SearchKernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2Search1KernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2Search2KernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2Search3KernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2Search4KernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2Search5KernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        static Dictionary<ProgramArrayIndex, ComputeKernel> mLyra2REv2Search6KernelArray = new Dictionary<ProgramArrayIndex, ComputeKernel>();
        ComputeProgram mLyra2REv2Program;
        ComputeKernel mLyra2REv2SearchKernel = null;
        ComputeKernel mLyra2REv2Search1Kernel = null;
        ComputeKernel mLyra2REv2Search2Kernel = null;
        ComputeKernel mLyra2REv2Search3Kernel = null;
        ComputeKernel mLyra2REv2Search4Kernel = null;
        ComputeKernel mLyra2REv2Search5Kernel = null;
        ComputeKernel mLyra2REv2Search6Kernel = null;
        private ComputeBuffer<byte> mLyra2REv2InputBuffer = null;
        private ComputeBuffer<byte> mLyra2REv2GlobalCacheBuffer = null;
        private ComputeBuffer<byte> mLyra2REv2PadBuffer = null;
        private ComputeBuffer<UInt32> mLyra2REv2OutputBuffer = null;
        private Lyra2REv2Stratum mLyra2REv2Stratum;
        long[] mLyra2REv2GlobalWorkSizeArray = new long[] { 0 };
        long[] mLyra2REv2LocalWorkSizeArray = new long[] { 0 };
        long[] mLyra2REv2GlobalWorkOffsetArray = new long[] { 0 };
        UInt32[] mLyra2REv2Output = new UInt32[sLyra2REv2OutputSize];
        byte[] mLyra2REv2Input = new byte[sLyra2REv2InputSize];



        public OpenCLLyra2REv2Miner(OpenCLDevice aGatelessGateDevice)
            : base(aGatelessGateDevice, "Lyra2REv2") {
            try {
                mLyra2REv2InputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, sLyra2REv2InputSize);
                mLyra2REv2OutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, sLyra2REv2OutputSize);
            } catch (Exception ex) {
                throw new UnrecoverableException(ex, GatelessGateDevice);
            }
        }

        public void Start(Lyra2REv2Stratum aLyra2REv2Stratum, int aLyra2REv2Intensity, int aLyra2REv2LocalWorkSize) {
            mLyra2REv2Stratum = aLyra2REv2Stratum;
            mLyra2REv2GlobalWorkSizeArray[0] = aLyra2REv2Intensity * OpenCLDevice.GetMaxComputeUnits() * aLyra2REv2LocalWorkSize;
            mLyra2REv2LocalWorkSizeArray[0] = aLyra2REv2LocalWorkSize;

            base.Start();
        }

        public void BuildLyra2REv2Program() {
            ComputeDevice computeDevice = OpenCLDevice.GetComputeDevice();

            try { mProgramArrayMutex.WaitOne(5000); } catch (Exception) { }

            if (mLyra2REv2ProgramArray.ContainsKey(new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0]))) {
                mLyra2REv2Program = mLyra2REv2ProgramArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2SearchKernel = mLyra2REv2SearchKernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2Search1Kernel = mLyra2REv2Search1KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2Search2Kernel = mLyra2REv2Search2KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2Search3Kernel = mLyra2REv2Search3KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2Search4Kernel = mLyra2REv2Search4KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2Search5Kernel = mLyra2REv2Search5KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
                mLyra2REv2Search6Kernel = mLyra2REv2Search6KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])];
            } else {
                String source = System.IO.File.ReadAllText(@"Kernels\lyra2rev2.cl");
                mLyra2REv2Program = new ComputeProgram(Context, source);
                MainForm.Logger(@"Loaded Kernels\lyra2rev2.cl for Device #" + DeviceIndex + ".");
                String buildOptions = (OpenCLDevice.GetVendor() == "AMD" ? "-O5" : // "-legacy" :
                                       OpenCLDevice.GetVendor() == "NVIDIA" ? "" : //"-cl-nv-opt-level=1 -cl-nv-maxrregcount=256 " :
                                                                   "")
                                      + " -IKernels -DWORKSIZE=" + mLyra2REv2LocalWorkSizeArray[0] 
                                      + " -DMAX_GLOBAL_THREADS=" + mLyra2REv2GlobalWorkSizeArray[0]; // TODO
                try {
                    mLyra2REv2Program.Build(OpenCLDevice.DeviceList, buildOptions, null, IntPtr.Zero);
                } catch (Exception) {
                    MainForm.Logger(mLyra2REv2Program.GetBuildLog(computeDevice));
                    throw;
                }
                MainForm.Logger("Built Lyra2REv2 program for Device #" + DeviceIndex + ".");
                MainForm.Logger("Build options: " + buildOptions);
                mLyra2REv2ProgramArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Program;
                mLyra2REv2SearchKernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2SearchKernel = mLyra2REv2Program.CreateKernel("search");
                mLyra2REv2Search1KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Search1Kernel = mLyra2REv2Program.CreateKernel("search1");
                mLyra2REv2Search2KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Search2Kernel = mLyra2REv2Program.CreateKernel("search2");
                mLyra2REv2Search3KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Search3Kernel = mLyra2REv2Program.CreateKernel("search3");
                mLyra2REv2Search4KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Search4Kernel = mLyra2REv2Program.CreateKernel("search4");
                mLyra2REv2Search5KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Search5Kernel = mLyra2REv2Program.CreateKernel("search5");
                mLyra2REv2Search6KernelArray[new ProgramArrayIndex(DeviceIndex, mLyra2REv2LocalWorkSizeArray[0])] = mLyra2REv2Search6Kernel = mLyra2REv2Program.CreateKernel("search6");
            }

            try { mProgramArrayMutex.ReleaseMutex(); } catch (Exception) { }
        }

        private void PrecalcMidstate()
        {
            uint[] m_state = new uint[] { 0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19 };
            uint m0 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 0);
            uint m1 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 1);
            uint m2 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 2);
            uint m3 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 3);
            uint m4 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 4);
            uint m5 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 5);
            uint m6 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 6);
            uint m7 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 7);
            uint m8 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 8);
            uint m9 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 9);
            uint m10 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 10);
            uint m11 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 11);
            uint m12 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 12);
            uint m13 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 13);
            uint m14 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 14);
            uint m15 = HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 15);
            const uint c32_0 = 0x243F6A88;
            const uint c32_1 = 0x85A308D3;
            const uint c32_2 = 0x13198A2E;
            const uint c32_3 = 0x03707344;
            const uint c32_4 = 0xA4093822;
            const uint c32_5 = 0x299F31D0;
            const uint c32_6 = 0x082EFA98;
            const uint c32_7 = 0xEC4E6C89;
            const uint c32_8 = 0x452821E6;
            const uint c32_9 = 0x38D01377;
            const uint c32_10 = 0xBE5466CF;
            const uint c32_11 = 0x34E90C6C;
            const uint c32_12 = 0xC0AC29B7;
            const uint c32_13 = 0xC97C50DD;
            const uint c32_14 = 0x3F84D5B5;
            const uint c32_15 = 0xB5470917;

            uint v0 = m_state[0];
            uint v1 = m_state[1];
            uint v2 = m_state[2];
            uint v3 = m_state[3];
            uint v4 = m_state[4];
            uint v5 = m_state[5];
            uint v6 = m_state[6];
            uint v7 = m_state[7];
            uint v8 = c32_0;
            uint v9 = c32_1;
            uint v10 = c32_2;
            uint v11 = c32_3;
            uint v12 = c32_4 ^ (64 * 8);
            uint v13 = c32_5 ^ (64 * 8);
            uint v14 = c32_6;
            uint v15 = c32_7;

            v0 = v0 + v4 + (m0 ^ c32_1);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m1 ^ c32_0);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m2 ^ c32_3);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m3 ^ c32_2);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m4 ^ c32_5);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m5 ^ c32_4);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m6 ^ c32_7);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m7 ^ c32_6);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m8 ^ c32_9);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m9 ^ c32_8);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m10 ^ c32_11);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m11 ^ c32_10);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m12 ^ c32_13);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m13 ^ c32_12);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m14 ^ c32_15);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m15 ^ c32_14);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m14 ^ c32_10);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m10 ^ c32_14);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m4 ^ c32_8);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m8 ^ c32_4);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m9 ^ c32_15);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m15 ^ c32_9);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m13 ^ c32_6);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m6 ^ c32_13);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m1 ^ c32_12);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m12 ^ c32_1);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m0 ^ c32_2);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m2 ^ c32_0);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m11 ^ c32_7);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m7 ^ c32_11);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m5 ^ c32_3);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m3 ^ c32_5);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m11 ^ c32_8);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m8 ^ c32_11);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m12 ^ c32_0);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m0 ^ c32_12);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m5 ^ c32_2);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m2 ^ c32_5);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m15 ^ c32_13);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m13 ^ c32_15);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m10 ^ c32_14);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m14 ^ c32_10);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m3 ^ c32_6);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m6 ^ c32_3);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m7 ^ c32_1);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m1 ^ c32_7);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m9 ^ c32_4);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m4 ^ c32_9);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m7 ^ c32_9);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m9 ^ c32_7);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m3 ^ c32_1);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m1 ^ c32_3);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m13 ^ c32_12);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m12 ^ c32_13);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m11 ^ c32_14);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m14 ^ c32_11);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m2 ^ c32_6);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m6 ^ c32_2);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m5 ^ c32_10);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m10 ^ c32_5);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m4 ^ c32_0);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m0 ^ c32_4);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m15 ^ c32_8);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m8 ^ c32_15);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m9 ^ c32_0);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m0 ^ c32_9);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m5 ^ c32_7);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m7 ^ c32_5);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m2 ^ c32_4);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m4 ^ c32_2);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m10 ^ c32_15);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m15 ^ c32_10);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m14 ^ c32_1);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m1 ^ c32_14);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m11 ^ c32_12);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m12 ^ c32_11);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m6 ^ c32_8);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m8 ^ c32_6);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m3 ^ c32_13);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m13 ^ c32_3);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m2 ^ c32_12);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m12 ^ c32_2);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m6 ^ c32_10);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m10 ^ c32_6);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m0 ^ c32_11);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m11 ^ c32_0);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m8 ^ c32_3);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m3 ^ c32_8);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m4 ^ c32_13);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m13 ^ c32_4);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m7 ^ c32_5);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m5 ^ c32_7);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m15 ^ c32_14);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m14 ^ c32_15);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m1 ^ c32_9);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m9 ^ c32_1);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m12 ^ c32_5);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m5 ^ c32_12);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m1 ^ c32_15);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m15 ^ c32_1);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m14 ^ c32_13);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m13 ^ c32_14);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m4 ^ c32_10);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m10 ^ c32_4);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m0 ^ c32_7);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m7 ^ c32_0);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m6 ^ c32_3);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m3 ^ c32_6);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m9 ^ c32_2);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m2 ^ c32_9);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m8 ^ c32_11);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m11 ^ c32_8);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m13 ^ c32_11);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m11 ^ c32_13);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m7 ^ c32_14);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m14 ^ c32_7);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m12 ^ c32_1);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m1 ^ c32_12);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m3 ^ c32_9);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m9 ^ c32_3);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m5 ^ c32_0);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m0 ^ c32_5);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m15 ^ c32_4);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m4 ^ c32_15);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m8 ^ c32_6);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m6 ^ c32_8);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m2 ^ c32_10);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m10 ^ c32_2);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m6 ^ c32_15);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m15 ^ c32_6);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m14 ^ c32_9);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m9 ^ c32_14);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m11 ^ c32_3);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m3 ^ c32_11);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m0 ^ c32_8);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m8 ^ c32_0);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m12 ^ c32_2);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m2 ^ c32_12);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m13 ^ c32_7);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m7 ^ c32_13);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m1 ^ c32_4);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m4 ^ c32_1);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m10 ^ c32_5);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m5 ^ c32_10);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m10 ^ c32_2);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m2 ^ c32_10);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m8 ^ c32_4);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m4 ^ c32_8);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m7 ^ c32_6);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m6 ^ c32_7);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m1 ^ c32_5);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m5 ^ c32_1);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m15 ^ c32_11);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m11 ^ c32_15);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m9 ^ c32_14);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m14 ^ c32_9);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m3 ^ c32_12);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m12 ^ c32_3);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m13 ^ c32_0);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m0 ^ c32_13);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m0 ^ c32_1);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m1 ^ c32_0);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m2 ^ c32_3);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m3 ^ c32_2);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m4 ^ c32_5);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m5 ^ c32_4);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m6 ^ c32_7);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m7 ^ c32_6);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m8 ^ c32_9);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m9 ^ c32_8);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m10 ^ c32_11);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m11 ^ c32_10);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m12 ^ c32_13);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m13 ^ c32_12);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m14 ^ c32_15);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m15 ^ c32_14);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m14 ^ c32_10);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m10 ^ c32_14);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m4 ^ c32_8);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m8 ^ c32_4);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m9 ^ c32_15);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m15 ^ c32_9);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m13 ^ c32_6);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m6 ^ c32_13);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m1 ^ c32_12);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m12 ^ c32_1);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m0 ^ c32_2);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m2 ^ c32_0);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m11 ^ c32_7);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m7 ^ c32_11);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m5 ^ c32_3);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m3 ^ c32_5);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m11 ^ c32_8);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m8 ^ c32_11);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m12 ^ c32_0);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m0 ^ c32_12);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m5 ^ c32_2);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m2 ^ c32_5);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m15 ^ c32_13);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m13 ^ c32_15);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m10 ^ c32_14);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m14 ^ c32_10);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m3 ^ c32_6);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m6 ^ c32_3);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m7 ^ c32_1);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m1 ^ c32_7);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m9 ^ c32_4);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m4 ^ c32_9);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            v0 = v0 + v4 + (m7 ^ c32_9);
            v12 = ((v12 ^ v0) << 16) | ((v12 ^ v0) >> 16);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 20) | ((v4 ^ v8) >> 12);
            v0 = v0 + v4 + (m9 ^ c32_7);
            v12 = ((v12 ^ v0) << 24) | ((v12 ^ v0) >> 8);
            v8 = v8 + v12;
            v4 = ((v4 ^ v8) << 25) | ((v4 ^ v8) >> 7);

            v1 = v1 + v5 + (m3 ^ c32_1);
            v13 = ((v13 ^ v1) << 16) | ((v13 ^ v1) >> 16);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 20) | ((v5 ^ v9) >> 12);
            v1 = v1 + v5 + (m1 ^ c32_3);
            v13 = ((v13 ^ v1) << 24) | ((v13 ^ v1) >> 8);
            v9 = v9 + v13;
            v5 = ((v5 ^ v9) << 25) | ((v5 ^ v9) >> 7);

            v2 = v2 + v6 + (m13 ^ c32_12);
            v14 = ((v14 ^ v2) << 16) | ((v14 ^ v2) >> 16);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 20) | ((v6 ^ v10) >> 12);
            v2 = v2 + v6 + (m12 ^ c32_13);
            v14 = ((v14 ^ v2) << 24) | ((v14 ^ v2) >> 8);
            v10 = v10 + v14;
            v6 = ((v6 ^ v10) << 25) | ((v6 ^ v10) >> 7);

            v3 = v3 + v7 + (m11 ^ c32_14);
            v15 = ((v15 ^ v3) << 16) | ((v15 ^ v3) >> 16);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 20) | ((v7 ^ v11) >> 12);
            v3 = v3 + v7 + (m14 ^ c32_11);
            v15 = ((v15 ^ v3) << 24) | ((v15 ^ v3) >> 8);
            v11 = v11 + v15;
            v7 = ((v7 ^ v11) << 25) | ((v7 ^ v11) >> 7);

            v0 = v0 + v5 + (m2 ^ c32_6);
            v15 = ((v15 ^ v0) << 16) | ((v15 ^ v0) >> 16);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 20) | ((v5 ^ v10) >> 12);
            v0 = v0 + v5 + (m6 ^ c32_2);
            v15 = ((v15 ^ v0) << 24) | ((v15 ^ v0) >> 8);
            v10 = v10 + v15;
            v5 = ((v5 ^ v10) << 25) | ((v5 ^ v10) >> 7);

            v1 = v1 + v6 + (m5 ^ c32_10);
            v12 = ((v12 ^ v1) << 16) | ((v12 ^ v1) >> 16);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 20) | ((v6 ^ v11) >> 12);
            v1 = v1 + v6 + (m10 ^ c32_5);
            v12 = ((v12 ^ v1) << 24) | ((v12 ^ v1) >> 8);
            v11 = v11 + v12;
            v6 = ((v6 ^ v11) << 25) | ((v6 ^ v11) >> 7);

            v2 = v2 + v7 + (m4 ^ c32_0);
            v13 = ((v13 ^ v2) << 16) | ((v13 ^ v2) >> 16);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 20) | ((v7 ^ v8) >> 12);
            v2 = v2 + v7 + (m0 ^ c32_4);
            v13 = ((v13 ^ v2) << 24) | ((v13 ^ v2) >> 8);
            v8 = v8 + v13;
            v7 = ((v7 ^ v8) << 25) | ((v7 ^ v8) >> 7);

            v3 = v3 + v4 + (m15 ^ c32_8);
            v14 = ((v14 ^ v3) << 16) | ((v14 ^ v3) >> 16);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 20) | ((v4 ^ v9) >> 12);
            v3 = v3 + v4 + (m8 ^ c32_15);
            v14 = ((v14 ^ v3) << 24) | ((v14 ^ v3) >> 8);
            v9 = v9 + v14;
            v4 = ((v4 ^ v9) << 25) | ((v4 ^ v9) >> 7);

            m_state[0] ^= v0 ^ v8;
            m_state[1] ^= v1 ^ v9;
            m_state[2] ^= v2 ^ v10;
            m_state[3] ^= v3 ^ v11;
            m_state[4] ^= v4 ^ v12;
            m_state[5] ^= v5 ^ v13;
            m_state[6] ^= v6 ^ v14;
            m_state[7] ^= v7 ^ v15;
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(1, m_state[0]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(2, m_state[1]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(3, m_state[2]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(4, m_state[3]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(5, m_state[4]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(6, m_state[5]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(7, m_state[6]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(8, m_state[7]);
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(9, HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 16));
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(10, HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 17));
            mLyra2REv2SearchKernel.SetValueArgument<UInt32>(11, HashLib.Converters.ConvertBytesToUIntSwapOrder(mLyra2REv2Input, 4 * 18));
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread() {
            Random r = new Random();

            MarkAsAlive();

            MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

            BuildLyra2REv2Program();

            fixed (long* lyra2rev2GlobalWorkOffsetArrayPtr = mLyra2REv2GlobalWorkOffsetArray)
            fixed (long* lyra2rev2GlobalWorkSizeArrayPtr = mLyra2REv2GlobalWorkSizeArray)
            fixed (long* lyra2rev2LocalWorkSizeArrayPtr = mLyra2REv2LocalWorkSizeArray)
            fixed (byte* lyra2rev2InputPtr = mLyra2REv2Input)
            fixed (UInt32* lyra2rev2OutputPtr = mLyra2REv2Output)
            while (!Stopped) {
                MarkAsAlive();

                try {
                    using (mLyra2REv2GlobalCacheBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, (mLyra2REv2GlobalWorkSizeArray[0] * 32)))
                    using (mLyra2REv2PadBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, (mLyra2REv2GlobalWorkSizeArray[0] * 32768))) {
                        mLyra2REv2SearchKernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);

                        mLyra2REv2Search1Kernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);

                        mLyra2REv2Search2Kernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);

                        mLyra2REv2Search3Kernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);
                        mLyra2REv2Search3Kernel.SetMemoryArgument(1, mLyra2REv2PadBuffer);

                        mLyra2REv2Search4Kernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);

                        mLyra2REv2Search5Kernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);

                        mLyra2REv2Search6Kernel.SetMemoryArgument(0, mLyra2REv2GlobalCacheBuffer);
                        mLyra2REv2Search6Kernel.SetMemoryArgument(1, mLyra2REv2OutputBuffer);

                        // Wait for the first job to arrive.
                        int elapsedTime = 0;
                        while ((mLyra2REv2Stratum == null || mLyra2REv2Stratum.GetJob() == null) && elapsedTime < 10000) {
                            Thread.Sleep(10);
                            elapsedTime += 10;
                        }
                        if (mLyra2REv2Stratum == null || mLyra2REv2Stratum.GetJob() == null)
                            throw new TimeoutException("Stratum server failed to send a new job.");

                        System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                        Lyra2REv2Stratum.Work lyra2rev2Work;

                        while (!Stopped && (lyra2rev2Work = mLyra2REv2Stratum.GetWork()) != null) {
                            MarkAsAlive();

                            var lyra2rev2Job = lyra2rev2Work.Job;
                            Array.Copy(lyra2rev2Work.Blob, mLyra2REv2Input, sLyra2REv2InputSize);
                            UInt32 lyra2rev2StartNonce = (UInt32)(r.Next(0, int.MaxValue));
                            Queue.Write<byte>(mLyra2REv2InputBuffer, true, 0, sLyra2REv2InputSize, (IntPtr)lyra2rev2InputPtr, null);

                            consoleUpdateStopwatch.Start();

                            while (!Stopped && mLyra2REv2Stratum.GetJob().Equals(lyra2rev2Job)) {
                                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                                sw.Start();

                                MarkAsAlive();

                                UInt64 lyra2REv2Target = (UInt64)((double)0xffff0000U / (mLyra2REv2Stratum.Difficulty / 256));
                                mLyra2REv2Search6Kernel.SetValueArgument<UInt64>(2, lyra2REv2Target);
                                mLyra2REv2GlobalWorkOffsetArray[0] = lyra2rev2StartNonce;

                                // Get a new local extranonce if necessary.
                                if (0xffffffffu - lyra2rev2StartNonce < (UInt32)mLyra2REv2GlobalWorkSizeArray[0])
                                    break;

                                PrecalcMidstate();

                                mLyra2REv2Output[255] = 0; // mLyra2REv2Output[255] is used as an atomic counter.
                                Queue.Write<UInt32>(mLyra2REv2OutputBuffer, true, 0, sLyra2REv2OutputSize, (IntPtr)lyra2rev2OutputPtr, null);
                                Queue.Execute(mLyra2REv2SearchKernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Execute(mLyra2REv2Search1Kernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Execute(mLyra2REv2Search2Kernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Execute(mLyra2REv2Search3Kernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Execute(mLyra2REv2Search4Kernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Execute(mLyra2REv2Search5Kernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Execute(mLyra2REv2Search6Kernel, mLyra2REv2GlobalWorkOffsetArray, mLyra2REv2GlobalWorkSizeArray, mLyra2REv2LocalWorkSizeArray, null); Queue.Finish();
                                Queue.Read<UInt32>(mLyra2REv2OutputBuffer, true, 0, sLyra2REv2OutputSize, (IntPtr)lyra2rev2OutputPtr, null);
                                if (mLyra2REv2Stratum.GetJob().Equals(lyra2rev2Job)) {
                                    for (int i = 0; i < mLyra2REv2Output[255]; ++i)
                                        mLyra2REv2Stratum.Submit(GatelessGateDevice, lyra2rev2Work, mLyra2REv2Output[i]);
                                }
                                lyra2rev2StartNonce += (UInt32)mLyra2REv2GlobalWorkSizeArray[0];

                                sw.Stop();
                                Speed = ((double)mLyra2REv2GlobalWorkSizeArray[0]) / sw.Elapsed.TotalSeconds;
                                if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000) {
                                    MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Kh/s (Lyra2REv2)", Speed / 1000));
                                    consoleUpdateStopwatch.Restart();
                                }
                            }
                        }
                    }
                } catch (Exception ex) {
                    MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                    if (UnrecoverableException.IsUnrecoverableException(ex)) {
                        this.UnrecoverableException = new UnrecoverableException(ex, GatelessGateDevice);
                        Stop();
                    }
                }

                Speed = 0;

                if (!Stopped) {
                    MainForm.Logger("Restarting miner thread...");
                    System.Threading.Thread.Sleep(5000);
                }
            }

            MarkAsDone();
        }
    }
}

