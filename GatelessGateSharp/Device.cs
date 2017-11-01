using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;



namespace GatelessGateSharp
{
    class Device
    {
        private int mDeviceIndex;
        private ComputeDevice mComputeDevice;
        private int mAcceptedShares;
        private int mRejectedShares;

        public int DeviceIndex { get { return mDeviceIndex; } }
        public int AcceptedShares { get { return mAcceptedShares; } }
        public int RejectedShares { get { return mRejectedShares; } }
        public ComputeDevice GetComputeDevice() { return mComputeDevice; }
        public Device(int aDeviceIndex, ComputeDevice aComputeDevice)
        {
            mComputeDevice = aComputeDevice;
            mDeviceIndex = aDeviceIndex;
            mAcceptedShares = 0;
            mRejectedShares = 0;
        }

        public int IncrementAcceptedShares()
        {
            return ++mAcceptedShares;
        }

        public int IncrementRejectedShares()
        {
            return ++mRejectedShares;
        }

        public void ClearShares()
        {
            mAcceptedShares = 0;
            mRejectedShares = 0;
        }
    }
}
