using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cloo;
using ATI.ADL;
using System.Runtime.InteropServices;



namespace GatelessGateSharp
{
    public class OpenCLDevice : Device, IDisposable
    {
        private ComputeDevice mComputeDevice;
        private ComputeContext mContext = null;
        private System.Threading.Mutex mMutex = new System.Threading.Mutex();
        private List<ComputeDevice> mDeviceList;
        private String mName;
        private string mVendor;
        private bool mCoreVoltageAvailable = true;

        IntPtr mODNLevelsBuffer_SystemClocks = IntPtr.Zero;
        IntPtr mODNLevelsBuffer_MemoryClocks = IntPtr.Zero;
        ADLODNPerformanceLevels mOSADLODNPerformanceLevelsData_SystemClocks;
        ADLODNPerformanceLevels mOSADLODNPerformanceLevelsData_MemoryClocks;

        public int ADLVersion { get; set; }
        public int ADLAdapterIndex { get; set; }

        public override String GetVendor()
        {
            if (mVendor == null) {
                mVendor = (mComputeDevice.Vendor == "Advanced Micro Devices, Inc.") ? "AMD" :
                          (mComputeDevice.Vendor == "NVIDIA Corporation") ? "NVIDIA" :
                          (mComputeDevice.Vendor == "Intel Corporation") ? "Intel" :
                          (mComputeDevice.Vendor == "GenuineIntel") ? "Intel" :
                           mComputeDevice.Vendor;
            }
            return mVendor;
        }

        public override String GetName()
        {
            if (mName == null) {
                if (GetVendor() == "AMD") {
                    mName = System.Text.Encoding.ASCII.GetString(mComputeDevice.BoardNameAMD)
                        .Replace("AMD ", "")
                        .Replace("(TM)", "")
                        .Replace(" Series", "")
                        .Replace(" Graphics", "")
                        .Replace("  ", " ");
                    mName = (new Regex("[^a-zA-Z0-9]+$")).Replace(mName, ""); // Drop '\0'

                    if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 6) { mName = "Radeon HD 7730"; } else if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 8) { mName = "Radeon HD 7750"; } else if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 10) { mName = "Radeon HD 7770"; } else if (mName == "Radeon HD 7700" && mComputeDevice.MaxComputeUnits == 14) { mName = "Radeon HD 7790"; } else if (mName == "Radeon HD 7800" && mComputeDevice.MaxComputeUnits == 16) { mName = "Radeon HD 7850"; } else if (mName == "Radeon HD 7800" && mComputeDevice.MaxComputeUnits == 20) { mName = "Radeon HD 7870"; } else if (mName == "Radeon HD 7800" && mComputeDevice.MaxComputeUnits == 24) { mName = "Radeon HD 7870 XT"; } else if (mName == "Radeon HD 7900" && mComputeDevice.MaxComputeUnits == 28) { mName = "Radeon HD 7950"; } else if (mName == "Radeon HD 7900" && mComputeDevice.MaxComputeUnits == 32 && DefaultCoreClock == 1050) { mName = "Radeon HD 7970 GE"; } else if (mName == "Radeon HD 7900" && mComputeDevice.MaxComputeUnits == 32 && DefaultCoreClock != 1000) { mName = "Radeon HD 7970"; } else if (mName == "Radeon HD 7900" && mComputeDevice.MaxComputeUnits == 32) { mName = "Radeon HD 7990"; } else if (mName == "Radeon R5 200" && mComputeDevice.MaxComputeUnits == 320 / 64) { mName = "Radeon R5 240"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 320 / 64) { mName = "Radeon R7 240"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R7 250"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 512 / 64) { mName = "Radeon R7 250E"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 640 / 64) { mName = "Radeon R7 250X"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 768 / 64) { mName = "Radeon R7 260"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 896 / 64) { mName = "Radeon R7 260X"; } else if (mName == "Radeon R7 200" && mComputeDevice.MaxComputeUnits == 1024 / 64) { mName = "Radeon R7 265"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1280 / 64) { mName = "Radeon R9 270"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1280 / 64) { mName = "Radeon R9 270X"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 280"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 2048 / 64) { mName = "Radeon R9 280X"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 285"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 2560 / 64) { mName = "Radeon R9 290"; } else if (mName == "Radeon R9 200" && mComputeDevice.MaxComputeUnits == 2816 / 64) { mName = "Radeon R9 290X"; } else if (mName == "Radeon R5 300" && mComputeDevice.MaxComputeUnits == 320 / 64) { mName = "Radeon R5 330"; } else if (mName == "Radeon R5 300" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R5 340"; } else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R7 340"; } else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 384 / 64) { mName = "Radeon R7 350"; } else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 512 / 64) { mName = "Radeon R7 350"; } else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 768 / 64) { mName = "Radeon R7 360"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 768 / 64) { mName = "Radeon R9 360"; } else if (mName == "Radeon R7 300" && mComputeDevice.MaxComputeUnits == 1024 / 64) { mName = "Radeon R7 370"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1024 / 64) { mName = "Radeon R9 370"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1280 / 64) { mName = "Radeon R9 370X"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 380"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 1792 / 64) { mName = "Radeon R9 380"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 2048 / 64) { mName = "Radeon R9 380X"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 2560 / 64) { mName = "Radeon R9 390"; } else if (mName == "Radeon R9 300" && mComputeDevice.MaxComputeUnits == 2816 / 64) { mName = "Radeon R9 390X"; } else if (mName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 3584 / 64) { mName = "Radeon R9 Fury"; } else if (mName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 4096 / 64 && DefaultCoreClock == 1000) { mName = "Radeon R9 Nano"; } else if (mName == "Radeon R9 Fury" && mComputeDevice.MaxComputeUnits == 4096 / 64) { mName = "Radeon R9 Fury X"; } else if (mName == "Radeon RX Vega" && mComputeDevice.MaxComputeUnits == 56) { mName = "Radeon RX Vega 56"; } else if (mName == "Radeon RX Vega" && mComputeDevice.MaxComputeUnits == 64 && GetMemorySize() == 16L * 1024 * 1024 * 1024) { mName = "Radeon RX Vega FE"; } else if (mName == "Radeon RX Vega" && mComputeDevice.MaxComputeUnits == 64) { mName = "Radeon RX Vega 64"; }
                } else {
                    mName = mComputeDevice.Name;
                }
            }
            return mName;
        }

        public override String GetOpenCLName()
        {
            return mComputeDevice.Name;
        }

        private System.Threading.Mutex computeByteBufferListMutex = new System.Threading.Mutex();
        List<ComputeBuffer<byte>> computeByteBufferList = new List<ComputeBuffer<byte>> { };

        public ComputeBuffer<byte> RequestComputeByteBuffer(ComputeMemoryFlags flags, long size)
        {
            ComputeBuffer<byte> buffer = null;
            try {
                if (!computeByteBufferListMutex.WaitOne(5000)) throw new Exception("Timed out waiting for mutex.");

                for (int i = 0; i < computeByteBufferList.Count; ++i) {
                    if (size == computeByteBufferList[i].Count && flags == computeByteBufferList[i].Flags) {
                        buffer = computeByteBufferList[i];
                        computeByteBufferList.RemoveAt(i);
                        break;
                    }
                }
            } catch (Exception ex) { MainForm.Logger(ex); } finally { computeByteBufferListMutex.ReleaseMutex(); }

            return (buffer != null) ? buffer : new ComputeBuffer<byte>(Context, flags, size);
        }

        public void ReleaseComputeByteBuffer(ComputeBuffer<byte> buffer)
        {
            try {
                if (!computeByteBufferListMutex.WaitOne(5000)) throw new Exception("Timed out waiting for mutex.");

                computeByteBufferList.Add(buffer);

            } catch (Exception ex) { MainForm.Logger(ex); } finally { computeByteBufferListMutex.ReleaseMutex(); }
        }

        public long MemoryUsage {
            get {
                long size = 0;
                try {
                    if (!computeByteBufferListMutex.WaitOne(5000)) throw new Exception("Timed out waiting for mutex.");

                    for (int i = 0; i < computeByteBufferList.Count; ++i)
                        size += computeByteBufferList[i].Count;

                } catch (Exception ex) { MainForm.Logger(ex); } finally { computeByteBufferListMutex.ReleaseMutex(); }

                return size;
            }
        }

        public void ReleaseAllComputeBuffers()
        {
            try {
                mMutex.WaitOne(100);
                for (int i = 0; i < computeByteBufferList.Count; ++i)
                    computeByteBufferList[i].Dispose();
                computeByteBufferList.Clear();
            } catch (Exception ex) { MainForm.Logger(ex); } finally { mMutex.ReleaseMutex(); }
        }

        public List<ComputeDevice> DeviceList { get { return mDeviceList; } }

        public ComputeContext Context {
            get {
                try { mMutex.WaitOne(5000); } catch (Exception) { }
                if (mContext == null) {
                    mDeviceList = new List<ComputeDevice>();
                    mDeviceList.Add(mComputeDevice);
                    var contextProperties = new ComputeContextPropertyList(mComputeDevice.Platform);
                    mContext = new ComputeContext(mDeviceList, contextProperties, null, IntPtr.Zero);
                }
                try { mMutex.ReleaseMutex(); } catch (Exception) { }
                return mContext;
            }
        }

        public override long GetMaxComputeUnits() { return mComputeDevice.MaxComputeUnits; }
        public ComputeDevice GetComputeDevice() { return mComputeDevice; }
        public override long GetMemorySize() { return mComputeDevice.GlobalMemorySize; }

        public OpenCLDevice(int aDeviceIndex, ComputeDevice aComputeDevice)
            : base(aDeviceIndex)
        {
            ADLAdapterIndex = -1;
            mComputeDevice = aComputeDevice;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                ReleaseAllComputeBuffers();
                if (mContext != null) {
                    mContext.Dispose();
                    mContext = null;
                }
            }
            base.Dispose(disposing);
        }

        public ComputeDevice GetNewComputeDevice()
        {
            var computeDeviceArrayList = new ArrayList();
            foreach (var platform in ComputePlatform.Platforms) {
                IList<ComputeDevice> openclDevices = platform.Devices;
                var properties = new ComputeContextPropertyList(platform);
                using (var context = new ComputeContext(openclDevices, properties, null, IntPtr.Zero)) {
                    foreach (var openclDevice in context.Devices) {
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

            foreach (var platform in ComputePlatform.Platforms) {
                if (platform.Name == "AMD Accelerated Parallel Processing" && doneWithAMD)
                    continue;

                IList<ComputeDevice> openclDevices = platform.Devices;
                var properties = new ComputeContextPropertyList(platform);
                using (var context = new ComputeContext(openclDevices, properties, null, IntPtr.Zero)) {
                    foreach (var openclDevice in context.Devices) {
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
            foreach (var computeDevice in computeDevices) {
                if (computeDevice.Vendor == "Advanced Micro Devices, Inc." && computeDevice.Name == "Ellesmere") {
                    devices[deviceIndex] = new AMDPolaris10(deviceIndex, computeDevice);
                } else {
                    devices[deviceIndex] = new OpenCLDevice(deviceIndex, computeDevice);
                }
                deviceIndex++;
            }

            return devices;
        }

        public static IntPtr ADL2Context = IntPtr.Zero;

        public static bool InitializeADL(OpenCLDevice[] mDevices)
        {
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

                if (!CheckAMDDriverVersion()
                    && (MessageBox.Show(Utilities.GetAutoClosingForm(10),
                        "The installed AMD driver is not compatible with this software.\nWould you like to download the recommended driver?",
                        "Gateless Gate Sharp", MessageBoxButtons.YesNo) == DialogResult.Yes)) {
                    MainForm.DownloadRecommendedAMDDriver();
                    Program.Kill();
                }

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
                                            using (OpenCLDummyLbryMiner dummyMiner = new OpenCLDummyLbryMiner(device)) {
                                                dummyMiner.Start();

                                                int[] activityTotalArray = new int[NumberOfAdapters];
                                                for (var i = 0; i < NumberOfAdapters; ++i)
                                                    activityTotalArray[i] = 0;
                                                for (var j = 0; j < 200; ++j) {
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
                                                    System.Windows.Forms.Application.DoEvents();
                                                    System.Threading.Thread.Sleep(10);
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
                                                if (!dummyMiner.Stopped)
                                                    MainForm.Logger("Failed to match OpenCL devices with ADL devices. The application may become unstable.");
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
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

                            for (int i = 0; i < mDevices.Length; ++i) {
                                if (mDevices[i].ADLAdapterIndex < 0)
                                    continue;
                                int available = 0, enabled = 0, ADLVersion = 0;
                                mDevices[i].PNPString = OSAdapterInfoData.ADLAdapterInfo[mDevices[i].ADLAdapterIndex].PNPString;
                                mDevices[i].ADLVersion = -1;
                                if (ADL.ADL2_Overdrive_Caps != null && ADL.ADL2_Overdrive_Caps(ADL2Context, mDevices[i].ADLAdapterIndex, ref available, ref enabled, ref ADLVersion) == ADL.ADL_SUCCESS
                                    && available != 0
                                    //&& enabled != 0
                                    ) {
                                    mDevices[i].ADLVersion = ADLVersion;
                                }
                                if (ADL.ADL2_OverdriveN_SystemClocks_Get != null && ADL.ADL2_OverdriveN_SystemClocks_Set != null) {
                                    ADLODNPerformanceLevels OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                                    OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Current;
                                    OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_ODN;
                                    mDevices[i].mODNLevelsBuffer_SystemClocks = Marshal.AllocCoTaskMem((int)(OSADLODNPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData)));
                                    Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, mDevices[i].mODNLevelsBuffer_SystemClocks, false);
                                    ADL.ADL2_OverdriveN_SystemClocks_Get(ADL2Context, mDevices[i].ADLAdapterIndex, mDevices[i].mODNLevelsBuffer_SystemClocks);
                                    mDevices[i].mOSADLODNPerformanceLevelsData_SystemClocks = (ADLODNPerformanceLevels)Marshal.PtrToStructure(mDevices[i].mODNLevelsBuffer_SystemClocks, OSADLODNPerformanceLevelsData.GetType());
                                }
                                if (ADL.ADL2_OverdriveN_MemoryClocks_Get != null && ADL.ADL2_OverdriveN_MemoryClocks_Set != null) {
                                    ADLODNPerformanceLevels OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                                    OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Current;
                                    OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_ODN;
                                    mDevices[i].mODNLevelsBuffer_MemoryClocks = Marshal.AllocCoTaskMem((int)(OSADLODNPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData)));
                                    Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, mDevices[i].mODNLevelsBuffer_MemoryClocks, false);
                                    ADL.ADL2_OverdriveN_MemoryClocks_Get(ADL2Context, mDevices[i].ADLAdapterIndex, mDevices[i].mODNLevelsBuffer_MemoryClocks);
                                    mDevices[i].mOSADLODNPerformanceLevelsData_MemoryClocks = (ADLODNPerformanceLevels)Marshal.PtrToStructure(mDevices[i].mODNLevelsBuffer_MemoryClocks, OSADLODNPerformanceLevelsData.GetType());
                                }
                            }
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

        private static bool CheckAMDDriverVersion()
        {
            try {
                bool debug = false;

                // Don't do anything if no AMD drivers are present in the system.
                if (OpenCLDevice.ADL2Context == IntPtr.Zero) {
                    if (debug) MainForm.Logger("OpenCLDevice.ADL2Context == IntPtr.Zero");
                    return true;
                }
                if (ADL.ADL2_Graphics_Versions_Get == null) {
                    if (debug) MainForm.Logger("ADL.ADL2_Graphics_Versions_Get == null");
                    return true;
                }

                ADLVersionsInfo info = new ADLVersionsInfo();
                var infoBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(info));
                Marshal.StructureToPtr(info, infoBuffer, false);
                var ret = ADL.ADL2_Graphics_Versions_Get(OpenCLDevice.ADL2Context, infoBuffer);
                if (ret == ADL.ADL_SUCCESS || ret == ADL.ADL_OK_WARNING) {
                    info = (ADLVersionsInfo)Marshal.PtrToStructure(infoBuffer, info.GetType());
                    // 18.2.6 -> "17.50.17.04-180206a-324065E-RadeonSoftwareAdrenalin"
                    MainForm.Logger("AMD Driver Version: " + info.DriverVer);
                    var match = (new Regex(@"^([0-9.]+)-([0-9][0-9])([0-9][0-9])([0-9][0-9])([a-z])?-([0-9A-F]+)-([a-zA-Z]+)$").Match(info.DriverVer));
                    if (match.Success) {
                        int driverVersionYear = int.Parse(match.Groups[2].Value);
                        int driverVersionMonth = int.Parse(match.Groups[3].Value);
                        int driverVersionDay = int.Parse(match.Groups[4].Value);
                        if (debug) MainForm.Logger("Driver Version Year:  " + driverVersionYear);
                        if (debug) MainForm.Logger("Driver Version Month: " + driverVersionMonth);
                        if (debug) MainForm.Logger("Driver Version Day:   " + driverVersionDay);
                        return driverVersionYear >= 18; // 2018
                    }
                }
            } catch (Exception ex) {
                MainForm.Logger(ex);
            }
            return false;
        }

        public int Temperature {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL_Overdrive5_Temperature_Get)
                    return -1;

                ADLTemperature OSADLTemperatureData = new ADLTemperature();
                var tempBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSADLTemperatureData));
                Marshal.StructureToPtr(OSADLTemperatureData, tempBuffer, false);
                if (ADL.ADL_Overdrive5_Temperature_Get != null && ADL.ADL_Overdrive5_Temperature_Get(ADLAdapterIndex, 0, tempBuffer) == ADL.ADL_SUCCESS) {
                    OSADLTemperatureData = (ADLTemperature)Marshal.PtrToStructure(tempBuffer, OSADLTemperatureData.GetType());
                    return (OSADLTemperatureData.Temperature / 1000);
                }
                return -1;
            }
        }

        public int FanSpeed {
            get {
                if (ADLAdapterIndex < 0)
                    return -1;

                ADLFanSpeedValue OSADLFanSpeedValueData = new ADLFanSpeedValue();
                OSADLFanSpeedValueData.iSpeedType = 1;
                var fanSpeedValueBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSADLFanSpeedValueData));
                Marshal.StructureToPtr(OSADLFanSpeedValueData, fanSpeedValueBuffer, false);
                if (ADL.ADL_Overdrive5_FanSpeed_Get != null && ADL.ADL_Overdrive5_FanSpeed_Get(ADLAdapterIndex, 0, fanSpeedValueBuffer) == ADL.ADL_SUCCESS) {
                    OSADLFanSpeedValueData = (ADLFanSpeedValue)Marshal.PtrToStructure(fanSpeedValueBuffer, OSADLFanSpeedValueData.GetType());
                    return OSADLFanSpeedValueData.iFanSpeed;
                }
                return -1;
            }

            set {
                if (ADLAdapterIndex < 0)
                    return;

                if (value < 0 && ADL.ADL_Overdrive5_FanSpeedToDefault_Set != null) {
                    ADL.ADL_Overdrive5_FanSpeedToDefault_Set(ADLAdapterIndex, 0);
                } else if (value >= 0 && ADL.ADL_Overdrive5_FanSpeed_Set != null) {
                    ADLFanSpeedValue OSADLFanSpeedValueData = new ADLFanSpeedValue();
                    OSADLFanSpeedValueData.iSpeedType = 1;
                    OSADLFanSpeedValueData.iFanSpeed = value;
                    OSADLFanSpeedValueData.iFlags = 0;
                    var fanSpeedValueBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSADLFanSpeedValueData));
                    Marshal.StructureToPtr(OSADLFanSpeedValueData, fanSpeedValueBuffer, false);
                    ADL.ADL_Overdrive5_FanSpeed_Set(ADLAdapterIndex, 0, fanSpeedValueBuffer);
                }
            }
        }

        public int Activity {
            get {
                if (ADLAdapterIndex < 0)
                    return -1;

                ADLPMActivity OSADLPMActivityData = new ADLPMActivity();
                var activityBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSADLPMActivityData));
                Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);
                if (ADL.ADL_Overdrive5_CurrentActivity_Get != null && ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) == ADL.ADL_SUCCESS) {
                    OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                    return OSADLPMActivityData.iActivityPercent;
                }
                return -1;
            }
        }

        public int PowerLimit {
            get {
                if (ADLAdapterIndex < 0)
                    return -1;

                int currentValue = 0, defaultValue = 0;
                if (ADL.ADL_Overdrive5_PowerControl_Get != null && ADL.ADL_Overdrive5_PowerControl_Get(ADLAdapterIndex, ref currentValue, ref defaultValue) == ADL.ADL_SUCCESS)
                    return 100 + currentValue;
                return -1;
            }

            set {
                if (ADLAdapterIndex < 0)
                    return;

                if (ADL.ADL_Overdrive5_PowerControl_Set != null && ADL.ADL_Overdrive5_PowerControl_Set(ADLAdapterIndex, value < 0 ? 0 : value - 100) == ADL.ADL_SUCCESS)
                    return;
                MainForm.Logger("ADL.ADL_Overdrive5_PowerControl_Set() failed with Device #" + DeviceIndex + ".");
            }
        }

        public void ResetOverclockingSettings()
        {
            if (ADLAdapterIndex < 0)
                return;

            var enabled = OverclockingEnabled;
            OverclockingEnabled = false;
            TargetPowerLimit = 100;

            if (ADLVersion >= 7
                && ADL.ADL2_OverdriveN_SystemClocks_Get != null 
                && ADL.ADL2_OverdriveN_SystemClocks_Set != null
                && ADL.ADL2_OverdriveN_MemoryClocks_Get != null
                && ADL.ADL2_OverdriveN_MemoryClocks_Set != null) {

                // OverDrive Next
                var systemData = mOSADLODNPerformanceLevelsData_SystemClocks;
                systemData.iMode = (int)ADLODNControlType.ODNControlType_Default;
                Marshal.StructureToPtr(systemData, mODNLevelsBuffer_SystemClocks, false);

                var memoryData = mOSADLODNPerformanceLevelsData_MemoryClocks;
                memoryData.iMode = (int)ADLODNControlType.ODNControlType_Default;
                Marshal.StructureToPtr(memoryData, mODNLevelsBuffer_MemoryClocks, false);

                //var s = "";
                //for (int i = 0; i < systemData.iNumberOfPerformanceLevels; ++i)
                //    s += systemData.aLevels[i].iClock + " " + systemData.aLevels[i].iVddc + " " + systemData.aLevels[i].iEnabled + "\n";
                //for (int i = 0; i < memoryData.iNumberOfPerformanceLevels; ++i)
                //    s += memoryData.aLevels[i].iClock + " " + memoryData.aLevels[i].iVddc + " " + memoryData.aLevels[i].iEnabled + "\n";
                //MessageBox.Show(s);

                ADL.ADL2_OverdriveN_MemoryClocks_Set(ADL2Context, ADLAdapterIndex, mODNLevelsBuffer_MemoryClocks);
                ADL.ADL2_OverdriveN_SystemClocks_Set(ADL2Context, ADLAdapterIndex, mODNLevelsBuffer_SystemClocks);

            } else if (ADLVersion >= 5
                && ADL.ADL_Overdrive5_ODPerformanceLevels_Get != null
                && ADL.ADL_Overdrive5_ODPerformanceLevels_Set != null) {
                
                // OverDrive 5
                ADLODPerformanceLevels OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                OSADLODPerformanceLevelsData.iReserved = 0;
                var levelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if (ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, 1, levelsBuffer) == ADL.ADL_SUCCESS)
                    ADL.ADL_Overdrive5_ODPerformanceLevels_Set(ADLAdapterIndex, levelsBuffer);
            }

            OverclockingEnabled = enabled;
        }


        public int CoreClock {
            get {
                if (ADLAdapterIndex < 0)
                    return -1;

                ADLPMActivity OSADLPMActivityData = new ADLPMActivity();
                var activityBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSADLPMActivityData));
                Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);
                if (ADL.ADL_Overdrive5_CurrentActivity_Get != null && ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) == ADL.ADL_SUCCESS) {
                    OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                    return OSADLPMActivityData.iEngineClock / 100;
                }

                return -1;
            }
        }

        public int CoreVoltage {
            get {
                if (ADLAdapterIndex < 0)
                    return -1;

                if (null != ADL.ADL_Overdrive5_CurrentActivity_Get) {
                    ADLPMActivity OSADLPMActivityData = new ADLPMActivity();
                    var activityBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSADLPMActivityData));
                    Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);
                    if (ADL.ADL_Overdrive5_CurrentActivity_Get != null && ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) == ADL.ADL_SUCCESS) {
                        OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                        mCoreVoltageAvailable = mCoreVoltageAvailable && !(OSADLPMActivityData.iVddc <= 800 || OSADLPMActivityData.iVddc > 2000); // The driver may return garbage.
                        if (mCoreVoltageAvailable)
                            return OSADLPMActivityData.iVddc;
                    }
                }

                if (ADLVersion < 6)
                    return -1;

                if (ADL.ADL_Overdrive6_VoltageControl_Get != null) {
                    int defaultValue = 0, currentValue = 0;
                    if (ADL.ADL_Overdrive6_VoltageControl_Get != null && ADL.ADL_Overdrive6_VoltageControl_Get(ADLAdapterIndex, ref currentValue, ref defaultValue) == ADL.ADL_SUCCESS)
                        return currentValue;
                }

                if (ADLVersion < 7)
                    return -1;

                ADLODNPerformanceStatus OSODNPerformanceStatusData = new ADLODNPerformanceStatus();
                var statusBuffer = Marshal.AllocCoTaskMem((int)Marshal.SizeOf(OSODNPerformanceStatusData));
                Marshal.StructureToPtr(OSODNPerformanceStatusData, statusBuffer, false);
                if (ADL.ADL2_OverdriveN_PerformanceStatus_Get != null && ADL.ADL2_OverdriveN_PerformanceStatus_Get(ADL2Context, ADLAdapterIndex, statusBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSODNPerformanceStatusData = (ADLODNPerformanceStatus)Marshal.PtrToStructure(statusBuffer, OSODNPerformanceStatusData.GetType());
                mCoreVoltageAvailable = mCoreVoltageAvailable && !(OSODNPerformanceStatusData.iVDDC <= 800 || OSODNPerformanceStatusData.iVDDC > 2000); // The driver may return garbage.
                return (!mCoreVoltageAvailable) ? -1 : OSODNPerformanceStatusData.iVDDC;
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
                if (ADL.ADL_Overdrive5_CurrentActivity_Get != null && ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndex, activityBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                return OSADLPMActivityData.iMemoryClock / 100;
            }
        }

        public int MemoryVoltage {
            get {
                if (ADLAdapterIndex < 0 || null == ADL.ADL2_OverdriveN_PerformanceStatus_Get)
                    return -1;

                ADLODNPerformanceStatus OSODNPerformanceStatusData = new ADLODNPerformanceStatus();
                var statusBuffer = Marshal.AllocCoTaskMem((int)(Marshal.SizeOf(OSODNPerformanceStatusData)));
                Marshal.StructureToPtr(OSODNPerformanceStatusData, statusBuffer, false);
                if (ADL.ADL2_OverdriveN_PerformanceStatus_Get != null && ADL.ADL2_OverdriveN_PerformanceStatus_Get(ADL2Context, ADLAdapterIndex, statusBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSODNPerformanceStatusData = (ADLODNPerformanceStatus)Marshal.PtrToStructure(statusBuffer, OSODNPerformanceStatusData.GetType());
                return (OSODNPerformanceStatusData.iVDDC < 800 || OSODNPerformanceStatusData.iVDDC > 2000) ? -1 : OSODNPerformanceStatusData.iVDDC; // The driver may return garbage.
            }
        }

        public int MaxCoreClock {
            get {
                // OverDrive 5
                var OSADLParametersData = new ADLODParameters();
                var parametersBuffer = Marshal.AllocCoTaskMem((int)(OSADLParametersData.iSize = Marshal.SizeOf(OSADLParametersData)));
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if (ADL.ADL_Overdrive5_ODParameters_Get != null && ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer) == ADL.ADL_SUCCESS) {
                    OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                    return OSADLParametersData.sEngineClockRange.iMax / 100;
                }

                return -1;
            }
        }

        public int MinCoreClock {
            get {
                // OverDrive 5
                var OSADLParametersData = new ADLODParameters();
                var parametersBuffer = Marshal.AllocCoTaskMem((int)(OSADLParametersData.iSize = Marshal.SizeOf(OSADLParametersData)));
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if (ADL.ADL_Overdrive5_ODParameters_Get != null && ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer) == ADL.ADL_SUCCESS) {
                    OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                    return OSADLParametersData.sEngineClockRange.iMin / 100;
                }

                return -1;
            }
        }

        public int CoreClockStep {
            get {
                // OverDrive 5
                var OSADLParametersData = new ADLODParameters();
                var parametersBuffer = Marshal.AllocCoTaskMem((int)(OSADLParametersData.iSize = Marshal.SizeOf(OSADLParametersData)));
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if (ADL.ADL_Overdrive5_ODParameters_Get != null && ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer) == ADL.ADL_SUCCESS) {
                    OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                    return OSADLParametersData.sEngineClockRange.iStep / 100;
                }

                return -1;
            }
        }


        public int MaxMemoryClock {
            get {
                // OverDrive 5
                var OSADLParametersData = new ADLODParameters();
                var parametersBuffer = Marshal.AllocCoTaskMem((int)(OSADLParametersData.iSize = Marshal.SizeOf(OSADLParametersData)));
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if (ADL.ADL_Overdrive5_ODParameters_Get != null && ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer) == ADL.ADL_SUCCESS) {
                    OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                    return OSADLParametersData.sMemoryClockRange.iMax / 100;
                }

                return -1;
            }
        }

        public int MinMemoryClock {
            get {
                // OverDrive 5
                var OSADLParametersData = new ADLODParameters();
                var parametersBuffer = Marshal.AllocCoTaskMem((int)(OSADLParametersData.iSize = Marshal.SizeOf(OSADLParametersData)));
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if (ADL.ADL_Overdrive5_ODParameters_Get != null && ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer) == ADL.ADL_SUCCESS) {
                    OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                    return OSADLParametersData.sMemoryClockRange.iMin / 100;
                }

                return -1;
            }
        }

        public int MemoryClockStep {
            get {
                // OverDrive 5
                var OSADLParametersData = new ADLODParameters();
                var parametersBuffer = Marshal.AllocCoTaskMem((int)(OSADLParametersData.iSize = Marshal.SizeOf(OSADLParametersData)));
                Marshal.StructureToPtr(OSADLParametersData, parametersBuffer, false);
                if (ADL.ADL_Overdrive5_ODParameters_Get != null && ADL.ADL_Overdrive5_ODParameters_Get(ADLAdapterIndex, parametersBuffer) == ADL.ADL_SUCCESS) {
                    OSADLParametersData = (ADLODParameters)Marshal.PtrToStructure(parametersBuffer, OSADLParametersData.GetType());
                    return OSADLParametersData.sMemoryClockRange.iStep / 100;
                }

                return -1;
            }
        }

        public int DefaultCoreClock {
            get {
                // OverDrive 5
                ADLODPerformanceLevels OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                OSADLODPerformanceLevelsData.iReserved = 0;
                var levelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if (ADL.ADL_Overdrive5_ODPerformanceLevels_Get != null && ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, 1, levelsBuffer) == ADL.ADL_SUCCESS) {
                    OSADLODPerformanceLevelsData = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());
                    return OSADLODPerformanceLevelsData.aLevels[ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_OD5 - 1].iEngineClock / 100;
                }

                if (ADLVersion < 7)
                    return -1;

                // OverDrive Next
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Default;
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_ODN;
                var ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODNPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_SystemClocks_Get != null && ADL.ADL2_OverdriveN_SystemClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLODNPerformanceLevelsData = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
                //
                int sourceIndex = -1;
                for (int i = 0; i < OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels; ++i)
                    if (OSADLODNPerformanceLevelsData.aLevels[i].iEnabled != 0)
                        sourceIndex = i;
                return (sourceIndex < 0) ? -1 : OSADLODNPerformanceLevelsData.aLevels[sourceIndex].iClock / 100;
            }
        }

        public int DefaultMemoryClock {
            get {
                // OverDrive 5
                ADLODPerformanceLevels OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                var levelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if (ADL.ADL_Overdrive5_ODPerformanceLevels_Get != null && ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, 1, levelsBuffer) == ADL.ADL_SUCCESS) {
                    OSADLODPerformanceLevelsData = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());
                    return OSADLODPerformanceLevelsData.aLevels[ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_OD5 - 1].iMemoryClock / 100;
                }

                if (ADLVersion < 7)
                    return -1;

                // OverDrive Next
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Default;
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_ODN;
                var ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODNPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_MemoryClocks_Get != null && ADL.ADL2_OverdriveN_MemoryClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLODNPerformanceLevelsData = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
                //
                int sourceIndex = -1;
                for (int i = 0; i < OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels; ++i)
                    if (OSADLODNPerformanceLevelsData.aLevels[i].iEnabled != 0)
                        sourceIndex = i;
                return (sourceIndex < 0) ? -1 : OSADLODNPerformanceLevelsData.aLevels[sourceIndex].iClock / 100;
            }
        }

        public int DefaultCoreVoltage {
            get {
                // OverDrive 5
                ADLODPerformanceLevels OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                OSADLODPerformanceLevelsData.iReserved = 0;
                var levelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if (ADL.ADL_Overdrive5_ODPerformanceLevels_Get != null && ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, 1, levelsBuffer) == ADL.ADL_SUCCESS) {
                    OSADLODPerformanceLevelsData = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());
                    return OSADLODPerformanceLevelsData.aLevels[ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_OD5 - 1].iVddc;
                }

                if (ADLVersion < 7)
                    return -1;

                // OverDrive Next
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Default;
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_ODN;
                var ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODNPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_SystemClocks_Get != null && ADL.ADL2_OverdriveN_SystemClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLODNPerformanceLevelsData = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
                //
                int sourceIndex = -1;
                for (int i = 0; i < OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels; ++i)
                    if (OSADLODNPerformanceLevelsData.aLevels[i].iEnabled != 0)
                        sourceIndex = i;
                return (sourceIndex < 0) ? -1 : OSADLODNPerformanceLevelsData.aLevels[sourceIndex].iVddc;
            }
        }

        public int DefaultMemoryVoltage {
            get {
                if (ADLVersion < 7)
                    return -1;

                // OverDrive Next
                ADLODNPerformanceLevels OSADLODNPerformanceLevelsData = new ADLODNPerformanceLevels();
                OSADLODNPerformanceLevelsData.iMode = (int)ADLODNControlType.ODNControlType_Default;
                OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels = ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_ODN;
                var ODNLevelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODNPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODNPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODNPerformanceLevelsData, ODNLevelsBuffer, false);
                if (ADL.ADL2_OverdriveN_MemoryClocks_Get != null && ADL.ADL2_OverdriveN_MemoryClocks_Get(ADL2Context, ADLAdapterIndex, ODNLevelsBuffer) != ADL.ADL_SUCCESS)
                    return -1;
                OSADLODNPerformanceLevelsData = (ADLODNPerformanceLevels)Marshal.PtrToStructure(ODNLevelsBuffer, OSADLODNPerformanceLevelsData.GetType());
                //
                int sourceIndex = -1;
                for (int i = 0; i < OSADLODNPerformanceLevelsData.iNumberOfPerformanceLevels; ++i)
                    if (OSADLODNPerformanceLevelsData.aLevels[i].iEnabled != 0)
                        sourceIndex = i;
                return (sourceIndex < 0) ? -1 : OSADLODNPerformanceLevelsData.aLevels[sourceIndex].iVddc;
            }
        }

        public bool GetECCErrorCounts(out int SEC, out int DED) {
            int supported;

            SEC = DED = 0;
            if (ADL.ADL_Workstation_ECC_Caps == null || ADL.ADL_Workstation_ECCData_Get == null)
                return false;
            var ret = ADL.ADL_Workstation_ECC_Caps(ADLAdapterIndex, out supported);
            if (ret != ADL.ADL_SUCCESS || supported == 0)
                return false;
            ADLECCData data = new ADLECCData();
            var buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(data));
            ret = ADL.ADL_Workstation_ECCData_Get(ADLAdapterIndex, buffer);
            data = (ADLECCData)Marshal.PtrToStructure(buffer, data.GetType());
            SEC = data.Sec;
            DED = data.Ded;
            return (ret == ADL.ADL_SUCCESS);
        }

        public int Power {
            get {
                if (ADLVersion < 6)
                    return -1;

                int power = 0;
                if (ADL.ADL_Overdrive6_CurrentPower_Get != null
                    && ADL.ADL_Overdrive6_CurrentPower_Get(ADLAdapterIndex, 0, ref power) == ADL.ADL_SUCCESS)
                    return power >> 8;
                if (ADL.ADL2_Overdrive6_CurrentPower_Get != null
                    && ADL.ADL2_Overdrive6_CurrentPower_Get(ADL2Context, ADLAdapterIndex, 0, ref power) == ADL.ADL_SUCCESS)
                    return power >> 8;
                return -1;
            }
        }

        public void UpdateOverclockingSettings()
        {
            if (ADLAdapterIndex < 0)
                return;
            //if (!OverclockingEnabled)
            //    return;

            PowerLimit = TargetPowerLimit;

            if (ADLVersion >= 7
                && mODNLevelsBuffer_SystemClocks != IntPtr.Zero
                && mODNLevelsBuffer_MemoryClocks != IntPtr.Zero
                && TargetCoreClock >= 0
                && TargetCoreVoltage >= 0
                && TargetMemoryClock >= 0
                && TargetMemoryVoltage >= 0) {

                // OverDrive Next
                var systemData = mOSADLODNPerformanceLevelsData_SystemClocks;
                systemData.iMode = (int)ADLODNControlType.ODNControlType_Manual;
                for (int i = systemData.iNumberOfPerformanceLevels - 1; i >= 0; --i) {
                    systemData.aLevels[i].iClock = TargetCoreClock * 100;
                    systemData.aLevels[i].iVddc = TargetCoreVoltage;
                }
                Marshal.StructureToPtr(systemData, mODNLevelsBuffer_SystemClocks, false);

                var memoryData = mOSADLODNPerformanceLevelsData_MemoryClocks;
                mOSADLODNPerformanceLevelsData_MemoryClocks.iMode = (int)ADLODNControlType.ODNControlType_Manual;
                for (int i = memoryData.iNumberOfPerformanceLevels - 1; i >= 0; --i) {
                    memoryData.aLevels[i].iClock = TargetMemoryClock * 100;
                    memoryData.aLevels[i].iVddc = TargetMemoryVoltage;
                }
                Marshal.StructureToPtr(mOSADLODNPerformanceLevelsData_MemoryClocks, mODNLevelsBuffer_MemoryClocks, false);

                ADL.ADL2_OverdriveN_SystemClocks_Set(ADL2Context, ADLAdapterIndex, mODNLevelsBuffer_SystemClocks);
                ADL.ADL2_OverdriveN_MemoryClocks_Set(ADL2Context, ADLAdapterIndex, mODNLevelsBuffer_MemoryClocks);

            } else if (ADLVersion >= 5 && TargetCoreClock >= 0 && TargetMemoryClock >= 0 && TargetCoreVoltage >= 0
                && ADL.ADL_Overdrive5_ODPerformanceLevels_Get != null
                && ADL.ADL_Overdrive5_ODPerformanceLevels_Set != null) {

                // OverDrive 5
                ADLODPerformanceLevels OSADLODPerformanceLevelsData = new ADLODPerformanceLevels();
                OSADLODPerformanceLevelsData.iReserved = 0;
                var levelsBuffer = Marshal.AllocCoTaskMem((int)(OSADLODPerformanceLevelsData.iSize = Marshal.SizeOf(OSADLODPerformanceLevelsData)));
                Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                if (ADL.ADL_Overdrive5_ODPerformanceLevels_Get(ADLAdapterIndex, 0, levelsBuffer) == ADL.ADL_SUCCESS) {
                    OSADLODPerformanceLevelsData = (ADLODPerformanceLevels)Marshal.PtrToStructure(levelsBuffer, OSADLODPerformanceLevelsData.GetType());
                    //
                    OSADLODPerformanceLevelsData.iReserved = 0;
                    for (int i = 0; i < ADL.ADL_MAX_NUM_PERFORMANCE_LEVELS_OD5; ++i) {
                        OSADLODPerformanceLevelsData.aLevels[i].iEngineClock = TargetCoreClock * 100;
                        OSADLODPerformanceLevelsData.aLevels[i].iMemoryClock = TargetMemoryClock * 100;
                        OSADLODPerformanceLevelsData.aLevels[i].iVddc = TargetCoreVoltage;
                    }
                    Marshal.StructureToPtr(OSADLODPerformanceLevelsData, levelsBuffer, false);
                    ADL.ADL_Overdrive5_ODPerformanceLevels_Set(ADLAdapterIndex, levelsBuffer);
                }
            }
        }
    }
}
