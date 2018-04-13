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
        [DllImport("phymem_wrapper.dll")]
        public static extern int LoadPhyMemDriver();
        [DllImport("phymem_wrapper.dll")]
        public static extern void UnloadPhyMemDriver();
        [DllImport("phymem_wrapper.dll")]
        public static extern int ReadPCI(uint busNum, uint devNum, uint funcNum, uint regOff, uint bytes, IntPtr pValue);
        [DllImport("phymem_wrapper.dll")]
        public static extern int WritePCI(uint busNum, uint devNum, uint funcNum, uint regOff, uint bytes, IntPtr pValue);
        [DllImport("phymem_wrapper.dll")]
        public static extern IntPtr MapPhyMem(UInt64 phyAddr, uint memSize);
        [DllImport("phymem_wrapper.dll")]
        public static extern void UnmapPhyMem(IntPtr pVirAddr, uint memSize);
        [DllImport("phymem_wrapper.dll")]
        public static extern int ReadFromAMDGPURegister(int busNum, uint regNo, out uint ptrValue);
        [DllImport("phymem_wrapper.dll")]
        public static extern int WriteToGMC81Register(int busNum, uint regNo, uint value, uint mask = 0xffffffff);
        [DllImport("phymem_wrapper.dll")]
        public static extern int UpdateGMC81Registers(int busNum,
            uint value, uint mask,
            uint value1, uint mask1,
            uint value2, uint mask2,
            uint value3, uint mask3,
            uint value4, uint mask4,
            uint value5, uint mask5,
            uint value6, uint mask6,
            uint value7, uint mask7,
            uint value8, uint mask8,
            uint value9, uint mask9,
            uint value10, uint mask10,
            uint value11, uint mask11,
            uint value12, uint mask12,
            uint value13, uint mask13,
            uint default_value3);
        
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
        
        static bool sAvailable = false;
        static PCIConfigurationSpace config = new PCIConfigurationSpace();

        public static bool Available { get { return sAvailable; } }

        public static bool LoadPhyMem() {
            if (LoadPhyMemDriver() != 0) {
                MainForm.Logger("Loaded PhyMem.");
                sAvailable = true;
            } else {
                MainForm.Logger("Failed to load phymem.");
                return false;
            }

            return true;
        }

        public static void UnloadPhyMem() {
            if (!sAvailable)
                return;
            UnloadPhyMemDriver();
            sAvailable = false;
            MainForm.Logger("Unloaded PhyMem.");
        }
    }
}
