using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloo;



namespace GatelessGateSharp
{
    class OpenCLDevice : Device
    {
        private ComputeDevice mComputeDevice;
        private ComputeContext mContext = null;
        private System.Threading.Mutex mMutex = new System.Threading.Mutex();
        private List<ComputeDevice> mDeviceList;
        private String mName;

        public override String GetVendor() {
            return (mComputeDevice.Vendor == "Advanced Micro Devices, Inc.") ? "AMD" :
                    (mComputeDevice.Vendor == "NVIDIA Corporation") ? "NVIDIA" :
                    (mComputeDevice.Vendor == "Intel Corporation") ? "Intel" :
                    (mComputeDevice.Vendor == "GenuineIntel") ? "Intel" :
                    mComputeDevice.Vendor;
        }

        public override String GetName() {
            return mName;
        }

        public List<ComputeDevice> DeviceList { get { return mDeviceList; } }

        public ComputeContext Context
        {
            get
            {
                try  { mMutex.WaitOne(5000); } catch (Exception) { }
                if (mContext == null)
                {
                    mDeviceList = new List<ComputeDevice>();
                    mDeviceList.Add(mComputeDevice);
                    var contextProperties = new ComputeContextPropertyList(mComputeDevice.Platform);
                    mContext = new ComputeContext(mDeviceList, contextProperties, null, IntPtr.Zero);
                }
                try  { mMutex.ReleaseMutex(); } catch (Exception) { }
                return mContext;
            }
        }

        public override long GetMaxComputeUnits() { return mComputeDevice.MaxComputeUnits; }
        public ComputeDevice GetComputeDevice() { return mComputeDevice; }

        public OpenCLDevice(int aDeviceIndex, ComputeDevice aComputeDevice)
            : base(aDeviceIndex)
        {
            mComputeDevice = aComputeDevice;
            if (GetVendor() == "AMD") {
                mName = System.Text.Encoding.ASCII.GetString(mComputeDevice.BoardNameAMD)
                    .Replace("AMD ", "")
                    .Replace("(TM)", "")
                    //.Replace(" Series", "")
                    .Replace(" Graphics", "")
                    .Replace("  ", " ");
                mName = (new Regex("[^a-zA-Z0-9]+$")).Replace(mName, ""); // Drop '\0'
                if (mName == "Radeon R9 Fury Series" && mComputeDevice.MaxComputeUnits == 64) {
                    mName = "Radeon R9 Nano";
                } else if (mName == "Radeon HD 7900 Series" && mComputeDevice.MaxComputeUnits == 32) {
                    mName = "Radeon HD 7970";
                }
            } else {
                mName = mComputeDevice.Name;
            }
        }

        public ComputeDevice GetNewComputeDevice()
        {
            var computeDeviceArrayList = new ArrayList();
            foreach (var platform in ComputePlatform.Platforms)
            {
                IList<ComputeDevice> openclDevices = platform.Devices;
                var properties = new ComputeContextPropertyList(platform);
                using (var context = new ComputeContext(openclDevices, properties, null, IntPtr.Zero))
                {
                    foreach (var openclDevice in context.Devices)
                    {
                        if (IsOpenCLDeviceIgnored(openclDevice))
                            continue;
                        computeDeviceArrayList.Add(openclDevice);
                    }
                }
            }
            return (ComputeDevice)computeDeviceArrayList[DeviceIndex];
        }

        public static bool IsOpenCLDeviceIgnored(ComputeDevice device)
        {
            return Regex.Match(device.Name, "Intel").Success || Regex.Match(device.Vendor, "Intel").Success || device.Type == ComputeDeviceTypes.Cpu;
        }

        public static OpenCLDevice[] GetAllOpenCLDevices()
        {
            var computeDeviceArrayList = new ArrayList();

            foreach (var platform in ComputePlatform.Platforms)
            {
                IList<ComputeDevice> openclDevices = platform.Devices;
                var properties = new ComputeContextPropertyList(platform);
                using (var context = new ComputeContext(openclDevices, properties, null, IntPtr.Zero))
                {
                    foreach (var openclDevice in context.Devices)
                    {
                        if (IsOpenCLDeviceIgnored(openclDevice))
                            continue;
                        computeDeviceArrayList.Add(openclDevice);
                    }
                }

            }
            var computeDevices = Array.ConvertAll(computeDeviceArrayList.ToArray(), item => (ComputeDevice)item);
            OpenCLDevice[] devices = new OpenCLDevice[computeDevices.Length];
            var deviceIndex = 0;
            foreach (var computeDevice in computeDevices)
            {
                devices[deviceIndex] = new OpenCLDevice(deviceIndex, computeDevice);
                deviceIndex++;
            }

            return devices;
        }
    }
}
