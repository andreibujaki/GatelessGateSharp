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

        static System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        public static void UpdateMemoryTimings() {
            if (!stopwatch.IsRunning)
                stopwatch.Start();
            if (!Available)
                return;
            unchecked {
                //MainForm.Logger("mmCONFIG_MEMSIZE: " + (uint)Marshal.ReadInt32(configBase, 0x150a * 4));
                //MainForm.Logger("Vendor ID: " + (((uint)Marshal.ReadInt32(configBase, 0xA80 * 4) & 0xf00U) >> 8));
                int busNumber = 5;
                
                int ARBData = 0;
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ref ARBData);
                BitVector32 ARB = new BitVector32(ARBData);
                ARB[AMDPolaris10.MC_ARB_DRAM_TIMING.ACTRD] = 15;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ARB.Data, AMDPolaris10.MC_ARB_DRAM_TIMING.ACTRD.Mask);
                
                int MISC2Data = 0;
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, ref MISC2Data);
                BitVector32 MISC2 = new BitVector32(MISC2Data);
                MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.FAW] = 0;
                MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.T32AW] = 3;
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, MISC2.Data, AMDPolaris10.MC_SEQ_MISC_TIMING2.FAW.Mask | AMDPolaris10.MC_SEQ_MISC_TIMING2.T32AW.Mask);

                int RASData = 0;
                ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, ref RASData);
                BitVector32 RAS = new BitVector32(RASData);
                RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRRD] = 5;
                RAS[AMDPolaris10.MC_SEQ_RAS_TIMING.TRC] = 87; // 87
                WriteAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, RAS.Data, AMDPolaris10.MC_SEQ_RAS_TIMING.TRRD.Mask | AMDPolaris10.MC_SEQ_RAS_TIMING.TRC.Mask);

                if (stopwatch.ElapsedMilliseconds >= 5000) {
                    ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_ARB_DRAM_TIMING, ref ARBData);
                    ARB = new BitVector32(ARBData);
                    ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_MISC_TIMING2, ref MISC2Data);
                    MISC2 = new BitVector32(MISC2Data);
                    ReadAMDGPURegister(busNumber, (int)AMDPolaris10.GMC81Registers.mmMC_SEQ_RAS_TIMING, ref RASData);
                    RAS = new BitVector32(RASData);
                    MainForm.Logger("=============");
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
                    MainForm.Logger("PA2RDATA: " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.PA2RDATA]);
                    MainForm.Logger("PA2WDATA: " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.PA2WDATA]);
                    MainForm.Logger("FAW:      " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.FAW]);
                    MainForm.Logger("TREDC:    " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.TREDC]);
                    MainForm.Logger("TWEDC:    " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.TWEDC]);
                    MainForm.Logger("T32AW:    " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.T32AW]);
                    MainForm.Logger("TWDATATR: " + MISC2[AMDPolaris10.MC_SEQ_MISC_TIMING2.TWDATATR]);
                    MainForm.Logger("=============");
                    stopwatch.Reset();
                }
           }
        }

        public static void UnloadPhyMem() {
            UnloadPhyMemDriver();
            sAvailable = false;
        }
    }
}
