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
        private String mName;

        public String Vendor
        {
            get
            {
                return (mComputeDevice.Vendor == "Advanced Micro Devices, Inc.") ? "AMD" :
                       (mComputeDevice.Vendor == "NVIDIA Corporation") ? "NVIDIA" :
                       (mComputeDevice.Vendor == "Intel Corporation") ? "Intel" :
                       (mComputeDevice.Vendor == "GenuineIntel") ? "Intel" :
                       mComputeDevice.Vendor;
            }
        }
        
        public String Name
        {
            get
            {
                return mName;
            }
        }

        public int DeviceIndex { get { return mDeviceIndex; } }
        public int AcceptedShares { get { return mAcceptedShares; } }
        public int RejectedShares { get { return mRejectedShares; } }
        public long MaxComputeUnits { get { return mComputeDevice.MaxComputeUnits; } }
        public ComputeDevice GetComputeDevice() { return mComputeDevice; }
        public Device(int aDeviceIndex, ComputeDevice aComputeDevice)
        {
            mComputeDevice = aComputeDevice;
            mDeviceIndex = aDeviceIndex;
            mAcceptedShares = 0;
            mRejectedShares = 0;
            mName = aComputeDevice.Name;

        }

        public void SetADLName(String aName)
        {
            aName = aName.Replace("AMD ", "");
            aName = aName.Replace("(TM)", "");
            aName = aName.Replace(" Series", "");
            aName = aName.Replace(" Graphics", "");
            aName = aName.Replace("  ", " ");
            if (aName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 64) // TODO
                aName = "Radeon R9 Fury X/Nano";
            mName = aName;
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
