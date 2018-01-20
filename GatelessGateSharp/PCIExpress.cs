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
        public static extern int ReadAMDGPURegister(int busNum, int regNo, ref int ptrValue);
        [DllImport("phymem_wrapper.dll")]
        public static extern int WriteAMDGPURegister(int busNum, int regNo, int value, int mask);

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
                MainForm.Logger("Successfully loaded phymem.");
                sAvailable = true;
            } else {
                MainForm.Logger("Failed to load phymem.");
                return false;
            }

            return true;
        }

        public static void UpdateMemoryTimings() {
            if (!Available)
                return;
            unchecked {
                int busNumber = 5;
                short mask;

                int data = 0;
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ref data);
                BitVector32 ARB = new BitVector32(data);
                mask = 0;
                ARB[AMDPolaris10.MC_ARB_DRAM_TIMING.ACTRD] = 16; mask |= AMDPolaris10.MC_ARB_DRAM_TIMING.ACTRD.Mask;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ARB.Data, mask);

                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, ref data);
                BitVector32 MISC = new BitVector32(data);
                mask = 0;
                //MISC[AMDPolaris10.MC_SEQ_MISC_TIMING.TRFC] = 3; mask |= AMDPolaris10.MC_SEQ_MISC_TIMING.TRFC.Mask;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, MISC.Data, mask);

                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, ref data);
                BitVector32 MISC2 = new BitVector32(data);
                mask = 0;
                MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.FAW] = 16; mask |= AMDPolaris10.MC_SEQ_MISC_TIMING2.FAW.Mask;
                MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.T32AW] = 3; mask |= AMDPolaris10.MC_SEQ_MISC_TIMING2.T32AW.Mask;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, MISC2.Data, mask);

                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, ref data);
                BitVector32 RAS = new BitVector32(data);
                mask = 0;
                RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRRD] = 5; mask |= AMDPolaris10.MC_SEQ_RAS_TIMING.TRRD.Mask;
                RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRC] = 87; mask |= AMDPolaris10.MC_SEQ_RAS_TIMING.TRC.Mask;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, RAS.Data, mask);

                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_CAS_TIMING, ref data);
                BitVector32 CAS = new BitVector32(data);
                mask = 0;
                CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TR2W] = 29; mask |= AMDPolaris10.MC_SEQ_CAS_TIMING.TR2W.Mask;
                CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TR2R] = 2; mask |= AMDPolaris10.MC_SEQ_CAS_TIMING.TR2R.Mask;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_CAS_TIMING, CAS.Data, mask);
            }
        }

        public static void PrintMemoryTimings() {
            if (!Available)
                return;
            unchecked {
                //MainForm.Logger("mmCONFIG_MEMSIZE: " + (uint)Marshal.ReadInt32(configBase, 0x150a * 4));
                //MainForm.Logger("Vendor ID: " + (((uint)Marshal.ReadInt32(configBase, 0xA80 * 4) & 0xf00U) >> 8));
                int busNumber = 5;
                int ARBData = 0;
                int RASData = 0;
                int CASData = 0;
                int MISCData = 0;
                int MISC2Data = 0;
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ref ARBData);
                var ARB = new BitVector32(ARBData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_CAS_TIMING, ref CASData);
                var CAS = new BitVector32(CASData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, ref RASData);
                var RAS = new BitVector32(RASData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, ref MISCData);
                var MISC = new BitVector32(MISCData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, ref MISC2Data);
                var MISC2 = new BitVector32(MISC2Data);
                MainForm.Logger("=============");
                MainForm.Logger("TNOPW:    " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TNOPW]); // 0
                MainForm.Logger("TNOPR:    " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TNOPR]); // 0
                MainForm.Logger("TR2W:     " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TR2W]); // 31
                MainForm.Logger("TCCDL:    " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TCCDL]); // 3
                MainForm.Logger("TR2R:     " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TR2R]); // 5
                MainForm.Logger("TW2R:     " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TW2R]); // 17
                MainForm.Logger("TCL:      " + CAS[AMDPolaris10.MC_SEQ_CAS_TIMING.TCL]); // 0 
                MainForm.Logger("-------------");
                MainForm.Logger("ACTRD:    " + ARB[AMDPolaris10.MC_ARB_DRAM_TIMING.ACTRD]);
                MainForm.Logger("ACTWR:    " + ARB[AMDPolaris10.MC_ARB_DRAM_TIMING.ACTWR]);
                MainForm.Logger("RASMACTRD:" + ARB[AMDPolaris10.MC_ARB_DRAM_TIMING.RASMACTRD]);
                MainForm.Logger("RASMACTWR:" + ARB[AMDPolaris10.MC_ARB_DRAM_TIMING.RASMACTWR]);
                MainForm.Logger("-------------");
                MainForm.Logger("TRCDW:    " + RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRCDW]);
                MainForm.Logger("TRCDWA:   " + RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRCDWA]);
                MainForm.Logger("TRCDR:    " + RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRCDR]);
                MainForm.Logger("TRCDRA:   " + RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRCDRA]);
                MainForm.Logger("TRRD:     " + RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRRD]);
                MainForm.Logger("TRC:      " + RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRC]);
                MainForm.Logger("-------------");
                MainForm.Logger("TRP_WRA:  " + MISC[AMDPolaris10.MC_SEQ_MISC_TIMING.TRP_WRA]);
                MainForm.Logger("TRP_RDA:  " + MISC[AMDPolaris10.MC_SEQ_MISC_TIMING.TRP_RDA]);
                MainForm.Logger("TRP:      " + MISC[AMDPolaris10.MC_SEQ_MISC_TIMING.TRP]);
                MainForm.Logger("TRFC:     " + MISC[AMDPolaris10.MC_SEQ_MISC_TIMING.TRFC]);
                MainForm.Logger("-------------");
                MainForm.Logger("PA2RDATA: " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.PA2RDATA]);
                MainForm.Logger("PA2WDATA: " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.PA2WDATA]);
                MainForm.Logger("FAW:      " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.FAW]);
                MainForm.Logger("TREDC:    " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.TREDC]);
                MainForm.Logger("TWEDC:    " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.TWEDC]);
                MainForm.Logger("T32AW:    " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.T32AW]);
                MainForm.Logger("TWDATATR: " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.TWDATATR]);
                MainForm.Logger("=============");
            }
        }

        public static void UnloadPhyMem() {
            if (!sAvailable)
                return;
            UnloadPhyMemDriver();
            sAvailable = false;
        }
    }
}
