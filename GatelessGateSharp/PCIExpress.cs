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
        public static extern int ReadAMDGPURegister(int busNum, uint regNo, ref uint ptrValue);
        [DllImport("phymem_wrapper.dll")]
        public static extern int WriteAMDGPURegister(int busNum, uint regNo, uint value, uint mask = 0xffffffff);
        [DllImport("phymem_wrapper.dll")]
        public static extern int UpdateGMC81Registers(int busNum,
            uint reg, uint value, uint mask,
            uint reg1, uint value1, uint mask1,
            uint reg2, uint value2, uint mask2,
            uint reg3, uint value3, uint mask3,
            uint reg4, uint value4, uint mask4,
            uint reg5, uint value5, uint mask5,
            uint reg6, uint value6, uint mask6,
            uint reg7, uint value7, uint mask7,
            uint reg8, uint value8, uint mask8);
        
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

        public static void UpdateMemoryTimings() {
            if (!Available)
                return;
            unchecked {
                int busNumber = Controller.OpenCLDevices[0].GetComputeDevice().PciBusIdAMD;

                //uint MISCData = 0; ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, ref MISCData);
                //BitVector32 MISC = new BitVector32((int)MISCData);
                //MISC[AMDPolaris10.MC_SEQ_MISC_TIMING.TRFC] = 3;
                //WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, MISC.Data, mask);

                AMDPolaris10.MC_ARB_DRAM_TIMING ARBTimings = new AMDPolaris10.MC_ARB_DRAM_TIMING();
                AMDPolaris10.MC_ARB_DRAM_TIMING2 ARBTimings2 = new AMDPolaris10.MC_ARB_DRAM_TIMING2();
                AMDPolaris10.MC_SEQ_RAS_TIMING RASTimings = new AMDPolaris10.MC_SEQ_RAS_TIMING();
                AMDPolaris10.MC_SEQ_CAS_TIMING CASTimings = new AMDPolaris10.MC_SEQ_CAS_TIMING();
                AMDPolaris10.MC_SEQ_MISC_TIMING MiscTimings = new AMDPolaris10.MC_SEQ_MISC_TIMING();
                AMDPolaris10.MC_SEQ_MISC_TIMING2 MiscTimings2 = new AMDPolaris10.MC_SEQ_MISC_TIMING2();

                ARBTimings.ACTRD = 17; // S:16, U:16
                ARBTimings.ACTWR = 10; // S:11, U:18
                ARBTimings.RASMACTRD = 47;
                ARBTimings.RASMACTWR = 63;

                ARBTimings2.RAS2RAS = 186;
                ARBTimings2.RP = 53;
                ARBTimings2.WRPLUSRP = 64;
                ARBTimings2.BUS_TURN = 25;

                RASTimings.TRCDW = 13;
                RASTimings.TRCDWA = 13;
                RASTimings.TRCDR = 24;
                RASTimings.TRCDRA = 24;
                RASTimings.TRRD = 5; // U: 5
                RASTimings.TRC = 65;

                CASTimings.TNOPW = 0;
                CASTimings.TNOPR = 0;
                CASTimings.TR2W = 28;
                CASTimings.TCCDL = 5; // U: 4
                CASTimings.TR2R = 5;
                CASTimings.TW2R = 14;
                //CASTimings.TCL = 21;

                //MiscTimings.TRP_WRA = 46;
                //MiscTimings.TRP_RDA = 24;
                //MiscTimings.TRP = 27;
                MiscTimings.TRFC = 136;

                //MiscTimings2.PA2RDATA = 0;
                //MiscTimings2.PA2WDATA = 0;
                //MiscTimings2.TREDC = 3;
                //MiscTimings2.TWEDC = 7;
                MiscTimings2.FAW = 0;
                MiscTimings2.T32AW = 3; // U: 0

                UpdateGMC81Registers(busNumber,
                    (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ARBTimings.Data, ARBTimings.Mask,
                    (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING2, ARBTimings2.Data, ARBTimings2.Mask,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, RASTimings.Data, RASTimings.Mask,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_CAS_TIMING, CASTimings.Data, CASTimings.Mask,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, MiscTimings.Data, MiscTimings.Mask,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, MiscTimings2.Data, MiscTimings2.Mask,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC1, (uint)0x2014030B, 0xffffffff,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC3, (uint)0xA00089FA, 0xffffffff,
                    (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC8, (uint)0x00000003, 0xffffffff);


                //WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_CAS_TIMING, CAS.Data, mask);

                /*
                777000000000000022CC1C00AD615C41C0590E152ECC8608006007000B031420FA8900A00300000010122F3FBA354019
                TRCDW=13 TRCDWA=13 TRCDR=24 TRCDRA=24 TRRD=5 TRC=65 Pad0=0

                TNOPW=0 TNOPR=0 TR2W=28 TCCDL=4 TR2R=5 TW2R=14 Pad0=0 TCL=21 Pad1=0

                TRP_WRA=46 TRP_RDA=24 TRP=27 TRFC=136 Pad0=0

                PA2RDATA=0 Pad0=0 PA2WDATA=0 Pad1=0 TFAW=0 TCRCRL=3 TCRCWL=7 TFAW32=0

                MC_SEQ_MISC1: 0x2014030B
                MC_SEQ_MISC3: 0xA00089FA
                MC_SEQ_MISC8: 0x00000003

                ACTRD=16 ACTWR=18 RASMACTRD=47 RASMACTWR=63

                RAS2RAS=186 RP=53 WRPLUSRP=64 BUS_TURN=25
                */
            }
        }

        public static void PrintMemoryTimings() {
            if (!Available)
                return;
            unchecked {
                int busNumber = Controller.OpenCLDevices[0].GetComputeDevice().PciBusIdAMD;
                uint ARBData = 0;
                uint ARB2Data = 0;
                uint RASData = 0;
                uint CASData = 0;
                uint MISCData = 0;
                uint MISC2Data = 0;
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ref ARBData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING2, ref ARB2Data);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_CAS_TIMING, ref CASData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, ref RASData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING, ref MISCData);
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, ref MISC2Data);
                AMDPolaris10.MC_ARB_DRAM_TIMING ARBTimings = new AMDPolaris10.MC_ARB_DRAM_TIMING(ARBData);
                AMDPolaris10.MC_ARB_DRAM_TIMING2 ARB2Timings = new AMDPolaris10.MC_ARB_DRAM_TIMING2(ARB2Data);
                AMDPolaris10.MC_SEQ_CAS_TIMING CASTimings = new AMDPolaris10.MC_SEQ_CAS_TIMING(CASData);
                AMDPolaris10.MC_SEQ_RAS_TIMING RASTimings = new AMDPolaris10.MC_SEQ_RAS_TIMING(RASData);
                AMDPolaris10.MC_SEQ_MISC_TIMING MISCTimings = new AMDPolaris10.MC_SEQ_MISC_TIMING(MISCData);
                AMDPolaris10.MC_SEQ_MISC_TIMING2 MISC2Timings = new AMDPolaris10.MC_SEQ_MISC_TIMING2(MISC2Data);
                MainForm.Logger("=============");
                MainForm.Logger("ACTRD:    " + ARBTimings.ACTRD);
                MainForm.Logger("ACTWR:    " + ARBTimings.ACTWR);
                MainForm.Logger("RASMACTRD:" + ARBTimings.RASMACTRD);
                MainForm.Logger("RASMACTWR:" + ARBTimings.RASMACTWR);
                MainForm.Logger("-------------");
                MainForm.Logger("RAS2RAS:  " + ARB2Timings.RAS2RAS);
                MainForm.Logger("RP:       " + ARB2Timings.RP);
                MainForm.Logger("WRPLUSRP: " + ARB2Timings.WRPLUSRP);
                MainForm.Logger("BUS_TURN: " + ARB2Timings.BUS_TURN);
                MainForm.Logger("-------------");
                MainForm.Logger("TNOPW:    " + CASTimings.TNOPW); // 0
                MainForm.Logger("TNOPR:    " + CASTimings.TNOPR); // 0
                MainForm.Logger("TR2W:     " + CASTimings.TR2W); // 31
                MainForm.Logger("TCCDL:    " + CASTimings.TCCDL); // 3
                MainForm.Logger("TR2R:     " + CASTimings.TR2R); // 5
                MainForm.Logger("TW2R:     " + CASTimings.TW2R); // 17
                MainForm.Logger("TCL:      " + CASTimings.TCL); // 0 
                MainForm.Logger("-------------");
                MainForm.Logger("TRCDW:    " + RASTimings.TRCDW);
                MainForm.Logger("TRCDWA:   " + RASTimings.TRCDWA);
                MainForm.Logger("TRCDR:    " + RASTimings.TRCDR);
                MainForm.Logger("TRCDRA:   " + RASTimings.TRCDRA);
                MainForm.Logger("TRRD:     " + RASTimings.TRRD);
                MainForm.Logger("TRC:      " + RASTimings.TRC);
                MainForm.Logger("-------------");
                MainForm.Logger("TRP_WRA:  " + MISCTimings.TRP_WRA);
                MainForm.Logger("TRP_RDA:  " + MISCTimings.TRP_RDA);
                MainForm.Logger("TRP:      " + MISCTimings.TRP);
                MainForm.Logger("TRFC:     " + MISCTimings.TRFC);
                MainForm.Logger("-------------");
                MainForm.Logger("PA2RDATA: " + MISC2Timings.PA2RDATA);
                MainForm.Logger("PA2WDATA: " + MISC2Timings.PA2WDATA);
                MainForm.Logger("FAW:      " + MISC2Timings.FAW);
                MainForm.Logger("TREDC:    " + MISC2Timings.TREDC);
                MainForm.Logger("TWEDC:    " + MISC2Timings.TWEDC);
                MainForm.Logger("T32AW:    " + MISC2Timings.T32AW);
                MainForm.Logger("TWDATATR: " + MISC2Timings.TWDATATR);
                MainForm.Logger("=============");
            }
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
