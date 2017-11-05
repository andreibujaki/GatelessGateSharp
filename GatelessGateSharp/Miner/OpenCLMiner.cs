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
using System.Threading.Tasks;
using Cloo;



namespace GatelessGateSharp
{
    class OpenCLMiner : Miner
    {
        private Device mDevice;
        private ComputeContext mContext = null;
        private ComputeCommandQueue mQueue = null;

        public Device Device { get { return mDevice; } }
        public ComputeContext Context { get { return mContext; } }
        public ComputeCommandQueue Queue { get { return mQueue; } }

        public ComputeDevice GetComputeDevice() { return mDevice.GetComputeDevice(); }

        protected OpenCLMiner(Device aDevice, String aAlgorithmName)
            : base(aDevice, aAlgorithmName)
        {
            mDevice = aDevice;
            List<ComputeDevice> deviceList = new List<ComputeDevice>();
            deviceList.Add(mDevice.GetComputeDevice());
            ComputeContextPropertyList properties = new ComputeContextPropertyList(GetComputeDevice().Platform);
            mContext = new ComputeContext(deviceList, properties, null, IntPtr.Zero);
            mQueue = new ComputeCommandQueue(mContext, GetComputeDevice(), ComputeCommandQueueFlags.None);
        }

        protected void Dispose()
        {
            mQueue.Dispose();
            mContext.Dispose();
        }
    }
}

