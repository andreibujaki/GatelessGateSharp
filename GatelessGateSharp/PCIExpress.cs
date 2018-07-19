using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Specialized;



namespace GatelessGateSharp {
    class PCIExpress {
        [DllImport("PhyMemWrapper.dll")]
        public static extern int LoadPhyMemDriver();
        [DllImport("PhyMemWrapper.dll")]
        public static extern void UnloadPhyMemDriver();
        [DllImport("PhyMemWrapper.dll")]
        public static extern int ReadPCI(uint busNum, uint devNum, uint funcNum, uint regOff, uint bytes, IntPtr pValue);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int WritePCI(uint busNum, uint devNum, uint funcNum, uint regOff, uint bytes, IntPtr pValue);
        [DllImport("PhyMemWrapper.dll")]
        public static extern IntPtr MapPhyMem(UInt64 phyAddr, uint memSize);
        [DllImport("PhyMemWrapper.dll")]
        public static extern void UnmapPhyMem(IntPtr pVirAddr, uint memSize);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int ReadFromAMDGPURegister(int busNum, uint regNo, out uint ptrValue);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int WriteToAMDGPURegister(int busNum, uint regNo, uint value, uint mask = 0xffffffff);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int UpdateGMC81Registers(
            uint busNum,
            uint arbDramTiming,
            uint arbDramTiming2,
            uint seqRasTiming,
            uint seqCasTiming,
            uint seqMiscTiming,
            uint seqMiscTiming2,
            uint seqMisc1,
            uint seqMisc3,
            uint seqMisc8,
            uint seqWrCtlD0,
            uint seqWrCtlD1,
            uint seqWrCtl2,
            uint arbDramTiming_1,
            uint arbDramTiming2_1,
            uint arbRttCntl0);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int ATOMBIOS_Load(uint busNumber);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int ATOMBIOS_SetOverclockingSettings(uint busNumber, int engineClock, int VDDC, int memoryClock, int VDDCI);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int ATOMBIOS_MemoryTraining(uint busNumber);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int SMU7_ReadDWORD(uint busNum, uint SMCAddress, out uint value);
        [DllImport("PhyMemWrapper.dll")]
        public static extern int SMU7_WriteDWORD(uint busNum, uint SMCAddress, uint value);

        [StructLayout(LayoutKind.Sequential)]
        internal class PCIConfigurationSpace {
            public UInt16 vendorID;
            public UInt16 deviceID;
            public UInt16 command;
            public UInt16 status;
            public UInt32 reserved0;
            public UInt32 reserved1;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 6)]
            internal UInt32[] baseAddressRegisters;
            public UInt32 cardbusCISPointer;
            public UInt16 subsystemVendorID;
            public UInt16 subsystemDeviceID;
            public UInt32 expansionROMBaseAddress;
            public UInt32 reserved2;
            public UInt32 reserved3;
            public UInt32 reserved4;
        }
        
        static PCIConfigurationSpace config = new PCIConfigurationSpace();

        public static bool Available = false;

        public static bool LoadPhyMem() {
            if (Available) {
                return true;
            } else if (LoadPhyMemDriver() != 0) {
                MainForm.Logger("Loaded PhyMem.");
                Available = true;
            } else {
                MainForm.Logger("Failed to load phymem.");
                return false;
            }

            return true;
        }

        public static void UnloadPhyMem() {
            if (!Available)
                return;
            UnloadPhyMemDriver();
            Available = false;
            MainForm.Logger("Unloaded PhyMem.");
        }
    }
}
