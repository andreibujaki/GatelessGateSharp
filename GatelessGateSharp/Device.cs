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
        private ComputeContext mContext = null;
        private System.Threading.Mutex mMutex = new System.Threading.Mutex();
        private List<ComputeDevice> mDeviceList;
        private List<ComputeCommandQueue> mQueues = new List<ComputeCommandQueue>();
        private List<ComputeCommandQueue> mUnusedQueues = new List<ComputeCommandQueue>();

        public ComputeContext Context
        {
            get
            {
                ComputeContext ret;
                mMutex.WaitOne();
                if (mContext == null)
                {
                    mDeviceList = new List<ComputeDevice>();
                    mDeviceList.Add(mComputeDevice);
                    var contextProperties = new ComputeContextPropertyList(GetComputeDevice().Platform);
                    mContext = new ComputeContext(mDeviceList, contextProperties, null, IntPtr.Zero);
                }
                ret = mContext;
                mMutex.ReleaseMutex();
                return ret;
            }
        }

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

        public String Name { get { return mName; } }
        public List<ComputeDevice> DeviceList { get { return mDeviceList; } }
        public ComputeContext ComputeContext { get { return mContext; } }

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

        public ComputeCommandQueue GetQueue()
        {
            ComputeCommandQueue ret;
            mMutex.WaitOne();
            if (mUnusedQueues.Count > 0)
            {
                ret = mUnusedQueues[0];
                mUnusedQueues.RemoveAt(0);
            }
            else
            {
                ret = new ComputeCommandQueue(Context, GetComputeDevice(), ComputeCommandQueueFlags.None);
                mQueues.Add(ret);
            }
            mMutex.ReleaseMutex();

            return ret;
        }

        public void ResetQueues()
        {
            mMutex.WaitOne();
            if (Vendor == "NVIDIA")
            {
                foreach (var queue in mQueues)
                    queue.Dispose();
                mQueues.Clear();
            }
            else
            {
                mUnusedQueues = new List<ComputeCommandQueue>(mQueues);
            }
            mMutex.ReleaseMutex();
        }
    }
}
