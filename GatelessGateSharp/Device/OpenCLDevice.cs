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
        public long          GetMemorySize() { return mComputeDevice.GlobalMemorySize; }

        public OpenCLDevice(int aDeviceIndex, ComputeDevice aComputeDevice)
            : base(aDeviceIndex)
        {
            ADLAdapterIndex = -1;
            mComputeDevice = aComputeDevice;
            if (GetVendor() == "AMD") {
                mName = System.Text.Encoding.ASCII.GetString(mComputeDevice.BoardNameAMD)
                    .Replace("AMD ", "")
                    .Replace("(TM)", "")
                    .Replace(" Series", "")
                    .Replace(" Graphics", "")
                    .Replace("  ", " ");
                mName = (new Regex("[^a-zA-Z0-9]+$")).Replace(mName, ""); // Drop '\0'
                
                if      (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 6)  { mName = "Radeon HD 7730"; } 
                else if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 8)  { mName = "Radeon HD 7750"; }
                else if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 10) { mName = "Radeon HD 7770"; } 
                else if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 14) { mName = "Radeon HD 7790"; } 
                else if (mName == "Radeon HD 7800" && mComputeDevice.MaxComputeUnits == 16) { mName = "Radeon HD 7850"; } 
                else if (mName == "Radeon HD 7800" && mComputeDevice.MaxComputeUnits == 20) { mName = "Radeon HD 7870"; }
                else if (mName == "Radeon HD 7800" && mComputeDevice.MaxComputeUnits == 24) { mName = "Radeon HD 7870 XT"; }
                else if (mName == "Radeon HD 7900" && mComputeDevice.MaxComputeUnits == 28) { mName = "Radeon HD 7950"; }
                else if (mName == "Radeon HD 7900" && mComputeDevice.MaxComputeUnits == 32) { mName = "Radeon HD 7970"; } 
                
                else if (mName == "Radeon R5 200" && mComputeDevice.MaxComputeUnits == 320 / 64) { mName = "Radeon R5 240"; }
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 320 / 64) { mName = "Radeon R7 240"; }
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R7 250"; }
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 512 / 64) { mName = "Radeon R7 250E"; }
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 640 / 64) { mName = "Radeon R7 250X"; }
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 768 / 64) { mName = "Radeon R7 260"; }
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 896 / 64) { mName = "Radeon R7 260X"; } 
                else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 1024 / 64) { mName = "Radeon R7 265"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1280 / 64) { mName = "Radeon R9 270"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1280 / 64) { mName = "Radeon R9 270X"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 280"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 2048 / 64) { mName = "Radeon R9 280X"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 285"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 2560 / 64) { mName = "Radeon R9 290"; } 
                else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 2816 / 64) { mName = "Radeon R9 290X"; }
                
                else if (mName == "Radeon R5 300" && mComputeDevice.MaxComputeUnits == 320 / 64) { mName = "Radeon R5 330"; }
                else if (mName == "Radeon R5 300" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R5 340"; } 
                else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R7 340"; } 
                else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R7 350"; }
                else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 512 / 64) { mName = "Radeon R7 350"; }
                else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 768 / 64) { mName = "Radeon R7 360"; }
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 768 / 64) { mName = "Radeon R9 360"; }
                else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 1024 / 64) { mName = "Radeon R7 370"; } 
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1024 / 64) { mName = "Radeon R9 370"; } 
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1280 / 64) { mName = "Radeon R9 370X"; }
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 380"; } 
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 380"; } 
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 2048 / 64) { mName = "Radeon R9 380X"; }
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 2560 / 64) { mName = "Radeon R9 390"; } 
                else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 2816 / 64) { mName = "Radeon R9 390X"; } 
                else if (mName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 3584 / 64) { mName = "Radeon R9 Fury"; } 
                else if (mName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 4096 / 64) { mName = "Radeon R9 Nano"; } 
                else if (mName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 4096 / 64) { mName = "Radeon R9 Fury X"; }
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
            bool doneWithAMD = false;
                
            foreach (var platform in ComputePlatform.Platforms)
            {
                if (platform.Name == "AMD Accelerated Parallel Processing" && doneWithAMD)
                    continue;

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

                if (platform.Name == "AMD Accelerated Parallel Processing")
                    doneWithAMD = true;
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

        static List<OpenCLDummyLbryMiner> dummyMinerList = new List<OpenCLDummyLbryMiner> { }; // Keep everything until the miner quits.
        static IntPtr ADL2Context = IntPtr.Zero;

        public static bool InitializeADL(OpenCLDevice[] mDevices) {
            var ADLRet = -1;
            var NumberOfAdapters = 0;
            if (null == ADL.ADL2_Main_Control_Create
                || null == ADL.ADL_Main_Control_Create
                || null == ADL.ADL_Adapter_NumberOfAdapters_Get)
                return false;
            if (ADL.ADL_SUCCESS == ADL.ADL2_Main_Control_Create(ADL.ADL_Main_Memory_Alloc, 1, ref ADL2Context)
                && ADL.ADL_SUCCESS == ADL.ADL_Main_Control_Create(ADL.ADL_Main_Memory_Alloc, 1)) {
                MainForm.Logger("Successfully initialized AMD Display Library.");
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
                                List<int> taken = new List<int> { };
                                foreach (var device in mDevices) {
                                    var openclDevice = device.GetComputeDevice();
                                    if (device.GetVendor() == "AMD") {
                                        string boardName = (new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]+$")).Replace(System.Text.Encoding.ASCII.GetString(openclDevice.BoardNameAMD), ""); // Drop '\0'
                                        int boardCounter = 0;
                                        for (var i = 0; i < NumberOfAdapters; ++i) {
                                            if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName && !taken.Contains(OSAdapterInfoData.ADLAdapterInfo[i].BusNumber))
                                                boardCounter++;
                                            while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                ++i;
                                        }
                                        if (boardCounter <= 1) {
                                            for (var i = 0; i < NumberOfAdapters; ++i) {
                                                if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName && !taken.Contains(OSAdapterInfoData.ADLAdapterInfo[i].BusNumber)) {
                                                    device.ADLAdapterIndex = i;
                                                    taken.Add(OSAdapterInfoData.ADLAdapterInfo[i].BusNumber);
                                                    break;
                                                }
                                                while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                    ++i;
                                            }
                                        } else {
                                            OpenCLDummyLbryMiner dummyMiner = new OpenCLDummyLbryMiner(device);
                                            dummyMinerList.Add(dummyMiner);
                                            dummyMiner.Start();

                                            int[] activityTotalArray = new int[NumberOfAdapters];
                                            for (var i = 0; i < NumberOfAdapters; ++i)
                                                activityTotalArray[i] = 0;
                                            for (var j = 0; j < 10; ++j) {
                                                for (var i = 0; i < NumberOfAdapters; ++i) {
                                                    if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName && !taken.Contains(OSAdapterInfoData.ADLAdapterInfo[i].BusNumber)) {
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
                                                                activityTotalArray[i] += OSADLPMActivityData.iActivityPercent;
                                                            }
                                                        }
                                                    }
                                                    while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                        ++i;
                                                }
                                                System.Threading.Thread.Sleep(100);
                                            }
                                            int candidate = -1;
                                            int candidateActivity = 0;
                                            for (var i = 0; i < NumberOfAdapters; ++i) {
                                                if (OSAdapterInfoData.ADLAdapterInfo[i].AdapterName == boardName && !taken.Contains(OSAdapterInfoData.ADLAdapterInfo[i].BusNumber)) {
                                                    if (candidate < 0 || activityTotalArray[i] > candidateActivity) {
                                                        candidateActivity = activityTotalArray[i];
                                                        candidate = i;
                                                    }
                                                }
                                                while (i + 1 < NumberOfAdapters && OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == OSAdapterInfoData.ADLAdapterInfo[i + 1].BusNumber)
                                                    ++i;
                                            }
                                            device.ADLAdapterIndex = candidate;
                                            taken.Add(OSAdapterInfoData.ADLAdapterInfo[candidate].BusNumber);

                                            dummyMiner.Stop();
                                            for (int i = 0; i < 50; ++i) {
                                                if (dummyMiner.Stopped)
                                                    break;
                                                System.Threading.Thread.Sleep(100);
                                            }
                                            if (!dummyMiner.Stopped) {
                                                MainForm.Logger("Failed at matching OpenCL devices with ADL devices. Restarting...");
                                                System.Windows.Forms.Application.Exit();
                                            }
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

            set {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_FanSpeed_Set)
                    return;

                if (value < 0) {
                    ADL.ADL_Overdrive5_FanSpeedToDefault_Set(ADLAdapterIndex, 0);
                } else {
                    ADLFanSpeedValue OSADLFanSpeedValueData;
                    OSADLFanSpeedValueData = new ADLFanSpeedValue();
                    var fanSpeedValueBuffer = IntPtr.Zero;
                    var size = Marshal.SizeOf(OSADLFanSpeedValueData);
                    OSADLFanSpeedValueData.iSpeedType = 1;
                    OSADLFanSpeedValueData.iFanSpeed = value;
                    OSADLFanSpeedValueData.iFlags = 0;
                    fanSpeedValueBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(OSADLFanSpeedValueData, fanSpeedValueBuffer, false);
                    ADL.ADL_Overdrive5_FanSpeed_Set(ADLAdapterIndex, 0, fanSpeedValueBuffer);
                }
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

            set {
                bool reset = value < 0;
                int ret;
                
                // OverDrive 5
                ADLODParameters OSADLParametersData;
                OSADLParametersData = new ADLODParameters();
                var parametersBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLParametersData);
                OSADLParametersData.iSize = size;
                parametersBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer)) != ADL.ADL_SUCCESS)
                    return;
                OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                //
                ADLODPerformanceLevels OSADLODPerformanceLevelsData;
                OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                var levelsBuffer = IntPtr.Zero;
                size = Marshal.SizeOf(OSADLODPerformanceLevelsData);
                OSADLODPerformanceLevelsData.iSize = size;
                OSADLODPerformanceLevelsData.iReserved = 0;
                levelsBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, reset ? 1 : 0, levelsBuffer)) != ADL.ADL_SUCCESS)
                    return;
                OSADLODPerformanceLevelsData = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());
                //
                if (!reset) {
                    for (int i = 1; i < ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_OD5; ++i)
                        OSADLODPerformanceLevelsData.aLevels[i].iEngineClock = value * 100;
                }
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODPerformanceLevels_Set(ADLAdapterIndex, levelsBuffer)) != ADL.ADL_SUCCESS)
                    return;

                // OverDrive Next
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData;
                OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                var ODNLevelsBuffer = IntPtr.Zero;
                var ODNSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData);
                OSADLODNPerformanceLevelsData.iSize = ODNSize;
                OSADLODNPerformanceLevelsData.iMode = (int)(reset ? ADLODNControlType.ODNControlType_Default : ADLODNControlType.ODNControlType_Current);
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS;
                ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)ODNSize);
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_SystemClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return;
                OSADLODNPerformanceLevelsData = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
                //
                OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Manual;
                if (!reset) {
                    for (int i = 1; i < OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels; ++i)
                        OSADLODNPerformanceLevelsData.aLevels[i].iClock = value * 100;
                }
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_SystemClocks_Set(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return;
            }
        }

        public int CoreVoltage {
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
                //if (OSADLPMActivityData.iVddc >= ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS)
                    return OSADLPMActivityData.iVddc;
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

            set {
                bool reset = value < 0;
                int ret;

                // OverDrive 5
                ADLODParameters OSADLParametersData;
                OSADLParametersData = new ADLODParameters();
                var parametersBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLParametersData);
                OSADLParametersData.iSize = size;
                parametersBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer)) != ADL.ADL_SUCCESS)
                    return;
                OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                //
                ADLODPerformanceLevels OSADLODPerformanceLevelsData;
                OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                var levelsBuffer = IntPtr.Zero;
                size = Marshal.SizeOf(OSADLODPerformanceLevelsData);
                OSADLODPerformanceLevelsData.iSize = size;
                OSADLODPerformanceLevelsData.iReserved = 0;
                levelsBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, reset ? 1 : 0, levelsBuffer)) != ADL.ADL_SUCCESS)
                    return;
                OSADLODPerformanceLevelsData = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());
                //
                if (!reset) {
                    for (int i = 1; i < ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_OD5; ++i)
                        OSADLODPerformanceLevelsData.aLevels[i].iMemoryClock = value * 100;
                }
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODPerformanceLevels_Set(ADLAdapterIndex, levelsBuffer)) != ADL.ADL_SUCCESS)
                    return;

                // OverDrive Next
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData;
                OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                var ODNLevelsBuffer = IntPtr.Zero;
                var ODNSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData);
                OSADLODNPerformanceLevelsData.iSize = ODNSize;
                OSADLODNPerformanceLevelsData.iMode = (int)(reset ? ADLODNControlType.ODNControlType_Default : ADLODNControlType.ODNControlType_Current);
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS;
                ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)ODNSize);
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_MemoryClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return;
                OSADLODNPerformanceLevelsData = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
                //
                OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Manual;
                if (!reset) {
                    for (int i = 1; i < OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels; ++i)
                        OSADLODNPerformanceLevelsData.aLevels[i].iClock = value * 100;
                }
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_MemoryClocks_Set(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return;
            }
        }

        static ADLODPerformanceLevels[] sODPerformanceLevelsBackups;
        static ADLODNPerformanceLevels[] sODNSystemClocksBackups;
        static ADLODNPerformanceLevels[] sODNMemoryClocksBackups;

        static public void SaveOverclockingSettings() {
            sODPerformanceLevelsBackups = new ADLODPerformanceLevels[MainForm.DeviceCount];
            sODNSystemClocksBackups = new ADLODNPerformanceLevels[MainForm.DeviceCount];
            for (int i = 0; i < MainForm.DeviceCount; ++i) {
                bool reset = true;
                int ret;

                OpenCLDevice device = MainForm.Devices[i];
                int ADLAdapterIndex = device.ADLAdapterIndex;
                if (ADLAdapterIndex < 0)
                    continue;

                // OverDrive 5
                ADLODPerformanceLevels OSADLODPerformanceLevelsData;
                OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                var levelsBuffer = IntPtr.Zero;
                var size = Marshal.SizeOf(OSADLODPerformanceLevelsData);
                OSADLODPerformanceLevelsData.iSize = size;
                OSADLODPerformanceLevelsData.iReserved = 0;
                levelsBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if ((ret = ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, 1, levelsBuffer)) == ADL.ADL_SUCCESS)
                    sODPerformanceLevelsBackups[i] = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());

                // OverDrive Next (System Clocks)
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData;
                OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                var ODNLevelsBuffer = IntPtr.Zero;
                var ODNSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData);
                OSADLODNPerformanceLevelsData.iSize = ODNSize;
                OSADLODNPerformanceLevelsData.iMode = (int)(reset ? ADLODNControlType.ODNControlType_Default : ADLODNControlType.ODNControlType_Current);
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS;
                ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)ODNSize);
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_SystemClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) == ADL.ADL_SUCCESS)
                    sODNSystemClocksBackups[i] = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());

                // OverDrive Next (Memory Clocks)
                OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                ODNLevelsBuffer = IntPtr.Zero;
                ODNSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData);
                OSADLODNPerformanceLevelsData.iSize = ODNSize;
                OSADLODNPerformanceLevelsData.iMode = (int)(reset ? ADLODNControlType.ODNControlType_Default : ADLODNControlType.ODNControlType_Current);
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS;
                ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)ODNSize);
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_MemoryClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) == ADL.ADL_SUCCESS)
                    sODNMemoryClocksBackups[i] = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
            }
        }

        static public void SaveOverclockingSettings() {
            for (int i = 0; i < MainForm.DeviceCount; ++i) {
                int ret;

                OpenCLDevice device = MainForm.Devices[i];
                int ADLAdapterIndex = device.ADLAdapterIndex;
                if (ADLAdapterIndex < 0)
                    continue;

                if (sODPerformanceLevelsBackups != null) {
                    var size = Marshal.SizeOf(sODPerformanceLevelsBackups[i]);
                    var levelsBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(sODPerformanceLevelsBackups[i], levelsBuffer, false);
                    if ((ret = ADL.ADL_Overdrive5_ODPerformanceLevels_Set(ADLAdapterIndex, levelsBuffer)) != ADL.ADL_SUCCESS)
                        return;
                }

                if (sODNSystemClocksBackups != null) {
                    var size = Marshal.SizeOf(sODNSystemClocksBackups[i]);
                    var levelsBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(sODNSystemClocksBackups[i], levelsBuffer, false);
                    if ((ret = ADL.ADL2_OverdriveN_SystemClocks_Set(ADL2Context, ADLAdapterIndex, levelsBuffer)) != ADL.ADL_SUCCESS)
                        return;
                }

                if (sODNMemoryClocksBackups != null) {
                    var size = Marshal.SizeOf(sODNMemoryClocksBackups[i]);
                    var levelsBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(sODNMemoryClocksBackups[i], levelsBuffer, false);
                    if ((ret = ADL.ADL2_OverdriveN_MemoryClocks_Set(ADL2Context, ADLAdapterIndex, levelsBuffer)) != ADL.ADL_SUCCESS)
                        return;
                }
            }
        }
    }
}
