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



namespace GatelessGateSharp
{
    class OpenCLMiner : Miner, IDisposable
    {
        public class ProgramArrayIndex
        {
            private int mDeviceIndex;
            private long mLocalWorkSize;

            public ProgramArrayIndex(int aDeviceIndex, long aLocalWorkSize)
            {
                mDeviceIndex = aDeviceIndex;
                mLocalWorkSize = aLocalWorkSize;
            }

            public bool Equals(ProgramArrayIndex mValue)
            {
                return mDeviceIndex == mValue.mDeviceIndex && mLocalWorkSize == mValue.mLocalWorkSize;
            }
        }


        private OpenCLDevice mDevice;
        private ComputeCommandQueue mQueue;

        public OpenCLDevice OpenCLDevice { get { return mDevice; } }
        public ComputeCommandQueue Queue { get { return mQueue; } }

        public ComputeDevice ComputeDevice { get { return mDevice.GetComputeDevice(); } }

        protected OpenCLMiner(OpenCLDevice aDevice, String aAlgorithmName, String aFirstAlgorithmName = "", String aSecondAlgorithmName = "")
            : base(aDevice, aAlgorithmName, aFirstAlgorithmName, aSecondAlgorithmName)
        {
            mDevice = aDevice;
            mQueue = new ComputeCommandQueue(Context, ComputeDevice, ComputeCommandQueueFlags.OutOfOrderExecution);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (mQueue != null) {
                    mQueue.Dispose();
                    mQueue = null;
                }
            }
        }
    }
}

