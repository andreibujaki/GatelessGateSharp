using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;



namespace GatelessGateSharp {
    class AMDDevice : OpenCLDevice {
        public AMDDevice(int aDeviceIndex, ComputeDevice aComputeDevice)
            : base(aDeviceIndex, aComputeDevice) {
        }
        public class MC_SEQ_MISC0
        {
            public static readonly UInt32 MT_MASK = 0xf0000000;
            public static readonly Int32 MT__SHIFT = 28;
            public static readonly UInt32 MEMORY_VENDOR_ID_MASK = 0x00000f00;
            public static readonly Int32 MEMORY_VENDOR_ID__SHIFT = 8;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 MT { get { return (Data & MT_MASK) >> MT__SHIFT; } set { Data = (Data & ~MT_MASK) | ((value << MT__SHIFT) & MT_MASK); Mask = Mask | MT_MASK; } }
            public UInt32 MEMORY_VENDOR_ID { get { return (Data & MEMORY_VENDOR_ID_MASK) >> MEMORY_VENDOR_ID__SHIFT; } set { Data = (Data & ~MEMORY_VENDOR_ID_MASK) | ((value << MEMORY_VENDOR_ID__SHIFT) & MEMORY_VENDOR_ID_MASK); Mask = Mask | MEMORY_VENDOR_ID_MASK; } }

            public string MemoryType {
                get {
                    return (MT == 1) ? "GDDR1" :
                           (MT == 2) ? "GDDR2" :
                           (MT == 3) ? "GDDR3" :
                           (MT == 4) ? "GDDR4" :
                           (MT == 5) ? "GDDR5" :
                           (MT == 6) ? "HBM" :
                           (MT == 11) ? "DDR3" :
                                       null;
                }
            }

            public string MemoryVendor {
                get {
                    return (MEMORY_VENDOR_ID == 1) ? "Samsung" :
                           (MEMORY_VENDOR_ID == 2) ? "Infineon" :
                           (MEMORY_VENDOR_ID == 3) ? "Elpida" :
                           (MEMORY_VENDOR_ID == 4) ? "Etron" :
                           (MEMORY_VENDOR_ID == 5) ? "Nanya" :
                           (MEMORY_VENDOR_ID == 6) ? "Hynix" :
                           (MEMORY_VENDOR_ID == 7) ? "Mosel" :
                           (MEMORY_VENDOR_ID == 8) ? "Winbond" :
                           (MEMORY_VENDOR_ID == 9) ? "ESMT" :
                           (MEMORY_VENDOR_ID == 15) ? "Micron" :
                                                      null;
                }
            }

            public MC_SEQ_MISC0(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }
        
        string mMemoryType = null;

        public override string GetMemoryType()
        {
            return mMemoryType;
        }

        string mMemoryVendor = null;

        public override string GetMemoryVendor()
        {
            return mMemoryVendor;
        }
    }
}
