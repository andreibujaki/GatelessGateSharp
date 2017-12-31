using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloo;
using ATI.ADL;
using System.Runtime.InteropServices;



namespace GatelessGateSharp
{
    class OpenCLDevice : Device
    {
        private ComputeDevice mComputeDevice;
        private ComputeContext mContext = null;
        private System.Threading.Mutex mMutex = new System.Threading.Mutex();
        private List<ComputeDevice> mDeviceList;
        private String mName;

        public int ADLAdapterIndex { get; set; }

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

        public static bool InitializeADL(OpenCLDevice[] mDevices) {
            var ADLRet = -1;
            var NumberOfAdapters = 0;
            if (null != ADL.ADL_Main_Control_Create)
                ADLRet = ADL.ADL_Main_Control_Create(ADL.ADL_Main_Memory_Alloc, 1);
            if (ADL.ADL_SUCCESS == ADLRet) {
                MainForm.Logger("Successfully initialized AMD Display Library.");
                if (null != ADL.ADL_Adapter_NumberOfAdapters_Get)
                    ADL.ADL_Adapter_NumberOfAdapters_Get(ref NumberOfAdapters);
                MainForm.Logger("Number of ADL Adapters: " + NumberOfAdapters.ToString());

                if (0 < NumberOfAdapters) {
                    ADLAdapterInfoArray OSAdapterInfoData;
                    OSAdapterInfoData = new ADLAdapterInfoArray();

                    if (null == ADL.ADL_Adapter_AdapterInfo_Get) {
                        MainForm.Logger("ADL.ADL_Adapter_AdapterInfo_Get() is not available.");
                    } else {
                        var AdapterBuffer = IntPtr.Zero;
                        var size = Marshal.SizeOf(OSAdapterInfoData);
                        AdapterBuffer = Marshal.AllocCoTaskMem((int)size);
                        Marshal.StructureToPtr(OSAdapterInfoData, AdapterBuffer, false);

                        ADLRet = ADL.ADL_Adapter_AdapterInfo_Get(AdapterBuffer, size);
                        if (ADL.ADL_SUCCESS == ADLRet) {
                            OSAdapterInfoData = (ADLAdapterInfoArray)Marshal.PtrToStructure(AdapterBuffer, OSAdapterInfoData.GetType());
                            bool adrenalineWorkaroundRequired = false;
                            foreach (var device in mDevices) {
                                var openclDevice = device.GetComputeDevice();
                                if (device.GetVendor() == "AMD" && openclDevice.PciBusIdAMD <= 0)
                                    adrenalineWorkaroundRequired = true;
                            }
                            if (adrenalineWorkaroundRequired) {
                                // workaround for Adrenalin drivers as PciBusIdAMD does not work properly.
                                MainForm.Logger("Manually matching OpenCL devices with ADL devices...");
                                bool[] taken = new bool[NumberOfAdapters];
                                for (var i = 0; i < NumberOfAdapters; ++i)
                                    taken[i] = false;
                                foreach (var device in mDevices) {
                                    var openclDevice = device.GetComputeDevice();
                                    if (device.GetVendor() == "AMD") {
                                        string boardName = (new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]+$")).Replace(System.Text.Encoding.ASCII.GetString(openclDevice.BoardNameAMD), ""); // Drop '\0'
                                        int boardCounter = 0;
                                        for (var i = 0; i < NumberOfAdapters; ++i) {
                                            if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName)
                                                boardCounter++;
                                            while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                ++i;
                                        }
                                        if (boardCounter <= 1) {
                                            for (var i = 0; i < NumberOfAdapters; ++i) {
                                                if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName) {
                                                    device.ADLAdapterIndex = i;
                                                    taken[i] = true;
                                                }
                                                while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                    ++i;
                                            }
                                        } else {
                                            OpenCLDummyLbryMiner dummyMiner = new OpenCLDummyLbryMiner(device);
                                            dummyMiner.Start();
                                            System.Threading.Thread.Sleep(3000);
                                            int candidate = -1;
                                            int candidateActivity = 0;
                                            for (var i = 0; i < NumberOfAdapters; ++i) {
                                                if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName && !taken[i]) {
                                                    ADLPMActivity OSADLPMActivityData;
                                                    OSADLPMActivityData = new ADLPMActivity();
                                                    var activityBuffer = IntPtr.Zero;
                                                    size = Marshal.SizeOf(OSADLPMActivityData);
                                                    activityBuffer = Marshal.AllocCoTaskMem((int)size);
                                                    Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);

                                                    if (null != ADL.ADL_Overdrive5_CurrentActivity_Get) {
                                                        ADLRet = ADL.ADL_Overdrive5_CurrentActivity_Get(i, activityBuffer);
                                                        if (ADL.ADL_SUCCESS == ADLRet) {
                                                            OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                                                            if (OSADLPMActivityData.iActivityPercent > candidateActivity) {
                                                                candidateActivity = OSADLPMActivityData.iActivityPercent;
                                                                candidate = i;
                                                            }
                                                        }
                                                    }
                                                }
                                                while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                    ++i;
                                            }
                                            if (candidate >= 0) {
                                                taken[candidate] = true;
                                                device.ADLAdapterIndex = candidate;
                                            }
                                            dummyMiner.Stop();
                                        }
                                    }
                                }
                            } else {
                                // Use openclDevice.PciBusIdAMD for matching.
                                foreach (var device in mDevices) {
                                    var openclDevice = device.GetComputeDevice();
                                    if (device.GetVendor() == "AMD") {
                                        for (var i = 0; i < NumberOfAdapters; i++) {
                                            var IsActive = 0;
                                            if (null != ADL.ADL_Adapter_Active_Get)
                                                ADLRet = ADL.ADL_Adapter_Active_Get(OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex, ref IsActive);
                                            if (OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == openclDevice.PciBusIdAMD
                                                && (device.ADLAdapterIndex < 0 || IsActive != 0)) {
                                                device.ADLAdapterIndex = OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex;
                                            }
                                        }
                                    }
                                }
                            }
                        } else {
                            MainForm.Logger("ADL_Adapter_AdapterInfo_Get() returned error code " + ADLRet.ToString());
                        }

                        // Release the memory for the AdapterInfo structure
                        if (IntPtr.Zero != AdapterBuffer)
                            Marshal.FreeCoTaskMem(AdapterBuffer);
                    }
                }
                return true;
            } else {
                MainForm.Logger("Failed to initialize AMD Display Library.");
                return false;
            }
        }

        public int Temperature {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_Temperature_Get)
                    return -1;

                ADLTemperature OSADLTemperatureData;
                OSADLTemperatureData = new ADLTemperature();
                var tempBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLTemperatureData);
                tempBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLTemperatureData, tempBuffer, false);
                if (ADL.ADL_Overdrive5_Temperature_Get(ADLAdapterIndex, 0, tempBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLTemperatureData = (ADLTemperature)Marshal.PtrToStructure(tempBuffer, OSADLTemperatureData.GetType());
                return (OSADLTemperatureData.Temperature / 1000);
            }
        }

        public int FanSpeed {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_FanSpeed_Get)
                    return -1;

                ADLFanSpeedValue OSADLFanSpeedValueData;
                OSADLFanSpeedValueData = new ADLFanSpeedValue();
                var fanSpeedValueBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLFanSpeedValueData);
                OSADLFanSpeedValueData.iSpeedType = 1;
                fanSpeedValueBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLFanSpeedValueData, fanSpeedValueBuffer, false);

                if (ADL.ADL_Overdrive5_FanSpeed_Get(ADLAdapterIndex, 0, fanSpeedValueBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLFanSpeedValueData = (ADLFanSpeedValue)Marshal.PtrToStructure(fanSpeedValueBuffer, OSADLFanSpeedValueData.GetType());
                return OSADLFanSpeedValueData.iFanSpeed;
            }
        }

        public int Activity {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_CurrentActivity_Get)
                    return -1;

                // activity
                ADLPMActivity OSADLPMActivityData;
                OSADLPMActivityData = new ADLPMActivity();
                var activityBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLPMActivityData);
                activityBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);

                if (ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                return OSADLPMActivityData.iActivityPercent;
            }
        }
        
        public int CoreClock {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_CurrentActivity_Get)
                    return -1;

                // activity
                ADLPMActivity OSADLPMActivityData;
                OSADLPMActivityData = new ADLPMActivity();
                var activityBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLPMActivityData);
                activityBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);

                if (ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                return OSADLPMActivityData.iEngineClock / 100;
            }
        }

        public int MemoryClock {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_CurrentActivity_Get)
                    return -1;

                // activity
                ADLPMActivity OSADLPMActivityData;
                OSADLPMActivityData = new ADLPMActivity();
                var activityBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLPMActivityData);
                activityBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);

                if (ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                return OSADLPMActivityData.iMemoryClock / 100;
            }
        }
    }
}
