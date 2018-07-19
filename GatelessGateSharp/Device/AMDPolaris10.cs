using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Cloo;



namespace GatelessGateSharp {
    class AMDGMC81 : AMDDevice {
        public enum GMC81Registers : uint {
            ixMC_TSM_DEBUG_GCNT = 0x0,
            ixMC_IO_DEBUG_UP_0 = 0x0,
            ixMC_TSM_DEBUG_FLAG = 0x1,
            ixMC_IO_DEBUG_UP_1 = 0x1,
            ixMC_TSM_DEBUG_ST01 = 0x10,
            ixMC_IO_DEBUG_DQB0L_RXPHASE_D0 = 0x100,
            ixMC_IO_DEBUG_DQB0H_RXPHASE_D0 = 0x101,
            ixMC_IO_DEBUG_DQB1L_RXPHASE_D0 = 0x102,
            ixMC_IO_DEBUG_DQB1H_RXPHASE_D0 = 0x103,
            ixMC_IO_DEBUG_DQB2L_RXPHASE_D0 = 0x104,
            ixMC_IO_DEBUG_DQB2H_RXPHASE_D0 = 0x105,
            ixMC_IO_DEBUG_DQB3L_RXPHASE_D0 = 0x106,
            ixMC_IO_DEBUG_DQB3H_RXPHASE_D0 = 0x107,
            ixMC_IO_DEBUG_DBI_RXPHASE_D0 = 0x108,
            ixMC_IO_DEBUG_EDC_RXPHASE_D0 = 0x109,
            ixMC_IO_DEBUG_WCK_RXPHASE_D0 = 0x10a,
            ixMC_IO_DEBUG_CK_RXPHASE_D0 = 0x10b,
            ixMC_IO_DEBUG_ADDRL_RXPHASE_D0 = 0x10c,
            ixMC_IO_DEBUG_ADDRH_RXPHASE_D0 = 0x10d,
            ixMC_IO_DEBUG_ACMD_RXPHASE_D0 = 0x10e,
            ixMC_IO_DEBUG_CMD_RXPHASE_D0 = 0x10f,
            ixMC_TSM_DEBUG_ST23 = 0x11,
            ixMC_IO_DEBUG_DQB0L_RXPHASE_D1 = 0x110,
            ixMC_IO_DEBUG_DQB0H_RXPHASE_D1 = 0x111,
            ixMC_IO_DEBUG_DQB1L_RXPHASE_D1 = 0x112,
            ixMC_IO_DEBUG_DQB1H_RXPHASE_D1 = 0x113,
            ixMC_IO_DEBUG_DQB2L_RXPHASE_D1 = 0x114,
            ixMC_IO_DEBUG_DQB2H_RXPHASE_D1 = 0x115,
            ixMC_IO_DEBUG_DQB3L_RXPHASE_D1 = 0x116,
            ixMC_IO_DEBUG_DQB3H_RXPHASE_D1 = 0x117,
            ixMC_IO_DEBUG_DBI_RXPHASE_D1 = 0x118,
            ixMC_IO_DEBUG_EDC_RXPHASE_D1 = 0x119,
            ixMC_IO_DEBUG_WCK_RXPHASE_D1 = 0x11a,
            ixMC_IO_DEBUG_CK_RXPHASE_D1 = 0x11b,
            ixMC_IO_DEBUG_ADDRL_RXPHASE_D1 = 0x11c,
            ixMC_IO_DEBUG_ADDRH_RXPHASE_D1 = 0x11d,
            ixMC_IO_DEBUG_ACMD_RXPHASE_D1 = 0x11e,
            ixMC_IO_DEBUG_CMD_RXPHASE_D1 = 0x11f,
            ixMC_TSM_DEBUG_ST45 = 0x12,
            ixMC_IO_DEBUG_DQB0L_TXPHASE_D0 = 0x120,
            ixMC_IO_DEBUG_DQB0H_TXPHASE_D0 = 0x121,
            ixMC_IO_DEBUG_DQB1L_TXPHASE_D0 = 0x122,
            ixMC_IO_DEBUG_DQB1H_TXPHASE_D0 = 0x123,
            ixMC_IO_DEBUG_DQB2L_TXPHASE_D0 = 0x124,
            ixMC_IO_DEBUG_DQB2H_TXPHASE_D0 = 0x125,
            ixMC_IO_DEBUG_DQB3L_TXPHASE_D0 = 0x126,
            ixMC_IO_DEBUG_DQB3H_TXPHASE_D0 = 0x127,
            ixMC_IO_DEBUG_DBI_TXPHASE_D0 = 0x128,
            ixMC_IO_DEBUG_EDC_TXPHASE_D0 = 0x129,
            ixMC_IO_DEBUG_WCK_TXPHASE_D0 = 0x12a,
            ixMC_IO_DEBUG_CK_TXPHASE_D0 = 0x12b,
            ixMC_IO_DEBUG_ADDRL_TXPHASE_D0 = 0x12c,
            ixMC_IO_DEBUG_ADDRH_TXPHASE_D0 = 0x12d,
            ixMC_IO_DEBUG_ACMD_TXPHASE_D0 = 0x12e,
            ixMC_IO_DEBUG_CMD_TXPHASE_D0 = 0x12f,
            ixMC_TSM_DEBUG_BKPT = 0x13,
            ixMC_IO_DEBUG_DQB0L_TXPHASE_D1 = 0x130,
            ixMC_IO_DEBUG_DQB0H_TXPHASE_D1 = 0x131,
            ixMC_IO_DEBUG_DQB1L_TXPHASE_D1 = 0x132,
            ixMC_IO_DEBUG_DQB1H_TXPHASE_D1 = 0x133,
            ixMC_IO_DEBUG_DQB2L_TXPHASE_D1 = 0x134,
            ixMC_IO_DEBUG_DQB2H_TXPHASE_D1 = 0x135,
            ixMC_IO_DEBUG_DQB3L_TXPHASE_D1 = 0x136,
            ixMC_IO_DEBUG_DQB3H_TXPHASE_D1 = 0x137,
            ixMC_IO_DEBUG_DBI_TXPHASE_D1 = 0x138,
            ixMC_IO_DEBUG_EDC_TXPHASE_D1 = 0x139,
            ixMC_IO_DEBUG_WCK_TXPHASE_D1 = 0x13a,
            ixMC_IO_DEBUG_CK_TXPHASE_D1 = 0x13b,
            ixMC_IO_DEBUG_ADDRL_TXPHASE_D1 = 0x13c,
            ixMC_IO_DEBUG_ADDRH_TXPHASE_D1 = 0x13d,
            ixMC_IO_DEBUG_ACMD_TXPHASE_D1 = 0x13e,
            ixMC_IO_DEBUG_CMD_TXPHASE_D1 = 0x13f,
            ixMC_IO_DEBUG_UP_20 = 0x14,
            ixMC_IO_DEBUG_DQB0L_RX_VREF_CAL_D0 = 0x140,
            ixMC_IO_DEBUG_DQB0H_RX_VREF_CAL_D0 = 0x141,
            ixMC_IO_DEBUG_DQB1L_RX_VREF_CAL_D0 = 0x142,
            ixMC_IO_DEBUG_DQB1H_RX_VREF_CAL_D0 = 0x143,
            ixMC_IO_DEBUG_DQB2L_RX_VREF_CAL_D0 = 0x144,
            ixMC_IO_DEBUG_DQB2H_RX_VREF_CAL_D0 = 0x145,
            ixMC_IO_DEBUG_DQB3L_RX_VREF_CAL_D0 = 0x146,
            ixMC_IO_DEBUG_DQB3H_RX_VREF_CAL_D0 = 0x147,
            ixMC_IO_DEBUG_DBI_RX_VREF_CAL_D0 = 0x148,
            ixMC_IO_DEBUG_EDC_RX_VREF_CAL_D0 = 0x149,
            ixMC_IO_DEBUG_WCK_RX_VREF_CAL_D0 = 0x14a,
            ixMC_IO_DEBUG_DQB0_CDR_PHSIZE_D0 = 0x14b,
            ixMC_IO_DEBUG_DQB1_CDR_PHSIZE_D0 = 0x14c,
            ixMC_IO_DEBUG_DQB2_CDR_PHSIZE_D0 = 0x14d,
            ixMC_IO_DEBUG_DQB3_CDR_PHSIZE_D0 = 0x14e,
            ixMC_IO_DEBUG_DBI_CDR_PHSIZE_D0 = 0x14f,
            ixMC_IO_DEBUG_UP_21 = 0x15,
            ixMC_IO_DEBUG_DQB0L_RX_VREF_CAL_D1 = 0x150,
            ixMC_IO_DEBUG_DQB0H_RX_VREF_CAL_D1 = 0x151,
            ixMC_IO_DEBUG_DQB1L_RX_VREF_CAL_D1 = 0x152,
            ixMC_IO_DEBUG_DQB1H_RX_VREF_CAL_D1 = 0x153,
            ixMC_IO_DEBUG_DQB2L_RX_VREF_CAL_D1 = 0x154,
            ixMC_IO_DEBUG_DQB2H_RX_VREF_CAL_D1 = 0x155,
            ixMC_IO_DEBUG_DQB3L_RX_VREF_CAL_D1 = 0x156,
            ixMC_IO_DEBUG_DQB3H_RX_VREF_CAL_D1 = 0x157,
            ixMC_IO_DEBUG_DBI_RX_VREF_CAL_D1 = 0x158,
            ixMC_IO_DEBUG_EDC_RX_VREF_CAL_D1 = 0x159,
            ixMC_IO_DEBUG_WCK_RX_VREF_CAL_D1 = 0x15a,
            ixMC_IO_DEBUG_DQB0_CDR_PHSIZE_D1 = 0x15b,
            ixMC_IO_DEBUG_DQB1_CDR_PHSIZE_D1 = 0x15c,
            ixMC_IO_DEBUG_DQB2_CDR_PHSIZE_D1 = 0x15d,
            ixMC_IO_DEBUG_DQB3_CDR_PHSIZE_D1 = 0x15e,
            ixMC_IO_DEBUG_DBI_CDR_PHSIZE_D1 = 0x15f,
            ixMC_IO_DEBUG_UP_22 = 0x16,
            ixMC_IO_DEBUG_DQB0L_TXSLF_D0 = 0x160,
            ixMC_IO_DEBUG_DQB0H_TXSLF_D0 = 0x161,
            ixMC_IO_DEBUG_DQB1L_TXSLF_D0 = 0x162,
            ixMC_IO_DEBUG_DQB1H_TXSLF_D0 = 0x163,
            ixMC_IO_DEBUG_DQB2L_TXSLF_D0 = 0x164,
            ixMC_IO_DEBUG_DQB2H_TXSLF_D0 = 0x165,
            ixMC_IO_DEBUG_DQB3L_TXSLF_D0 = 0x166,
            ixMC_IO_DEBUG_DQB3H_TXSLF_D0 = 0x167,
            ixMC_IO_DEBUG_DBI_TXSLF_D0 = 0x168,
            ixMC_IO_DEBUG_EDC_TXSLF_D0 = 0x169,
            ixMC_IO_DEBUG_WCK_TXSLF_D0 = 0x16a,
            ixMC_IO_DEBUG_CK_TXSLF_D0 = 0x16b,
            ixMC_IO_DEBUG_ADDRL_TXSLF_D0 = 0x16c,
            ixMC_IO_DEBUG_ADDRH_TXSLF_D0 = 0x16d,
            ixMC_IO_DEBUG_ACMD_TXSLF_D0 = 0x16e,
            ixMC_IO_DEBUG_CMD_TXSLF_D0 = 0x16f,
            ixMC_IO_DEBUG_UP_23 = 0x17,
            ixMC_IO_DEBUG_DQB0L_TXSLF_D1 = 0x170,
            ixMC_IO_DEBUG_DQB0H_TXSLF_D1 = 0x171,
            ixMC_IO_DEBUG_DQB1L_TXSLF_D1 = 0x172,
            ixMC_IO_DEBUG_DQB1H_TXSLF_D1 = 0x173,
            ixMC_IO_DEBUG_DQB2L_TXSLF_D1 = 0x174,
            ixMC_IO_DEBUG_DQB2H_TXSLF_D1 = 0x175,
            ixMC_IO_DEBUG_DQB3L_TXSLF_D1 = 0x176,
            ixMC_IO_DEBUG_DQB3H_TXSLF_D1 = 0x177,
            ixMC_IO_DEBUG_DBI_TXSLF_D1 = 0x178,
            ixMC_IO_DEBUG_EDC_TXSLF_D1 = 0x179,
            ixMC_IO_DEBUG_WCK_TXSLF_D1 = 0x17a,
            ixMC_IO_DEBUG_CK_TXSLF_D1 = 0x17b,
            ixMC_IO_DEBUG_ADDRL_TXSLF_D1 = 0x17c,
            ixMC_IO_DEBUG_ADDRH_TXSLF_D1 = 0x17d,
            ixMC_IO_DEBUG_ACMD_TXSLF_D1 = 0x17e,
            ixMC_IO_DEBUG_CMD_TXSLF_D1 = 0x17f,
            ixMC_IO_DEBUG_UP_24 = 0x18,
            ixMC_IO_DEBUG_DQB0L_TXBST_PD_D0 = 0x180,
            ixMC_IO_DEBUG_DQB0H_TXBST_PD_D0 = 0x181,
            ixMC_IO_DEBUG_DQB1L_TXBST_PD_D0 = 0x182,
            ixMC_IO_DEBUG_DQB1H_TXBST_PD_D0 = 0x183,
            ixMC_IO_DEBUG_DQB2L_TXBST_PD_D0 = 0x184,
            ixMC_IO_DEBUG_DQB2H_TXBST_PD_D0 = 0x185,
            ixMC_IO_DEBUG_DQB3L_TXBST_PD_D0 = 0x186,
            ixMC_IO_DEBUG_DQB3H_TXBST_PD_D0 = 0x187,
            ixMC_IO_DEBUG_DBI_TXBST_PD_D0 = 0x188,
            ixMC_IO_DEBUG_EDC_TXBST_PD_D0 = 0x189,
            ixMC_IO_DEBUG_WCK_TXBST_PD_D0 = 0x18a,
            ixMC_IO_DEBUG_CK_TXBST_PD_D0 = 0x18b,
            ixMC_IO_DEBUG_ADDRL_TXBST_PD_D0 = 0x18c,
            ixMC_IO_DEBUG_ADDRH_TXBST_PD_D0 = 0x18d,
            ixMC_IO_DEBUG_ACMD_TXBST_PD_D0 = 0x18e,
            ixMC_IO_DEBUG_CMD_TXBST_PD_D0 = 0x18f,
            ixMC_IO_DEBUG_UP_25 = 0x19,
            ixMC_IO_DEBUG_DQB0L_TXBST_PD_D1 = 0x190,
            ixMC_IO_DEBUG_DQB0H_TXBST_PD_D1 = 0x191,
            ixMC_IO_DEBUG_DQB1L_TXBST_PD_D1 = 0x192,
            ixMC_IO_DEBUG_DQB1H_TXBST_PD_D1 = 0x193,
            ixMC_IO_DEBUG_DQB2L_TXBST_PD_D1 = 0x194,
            ixMC_IO_DEBUG_DQB2H_TXBST_PD_D1 = 0x195,
            ixMC_IO_DEBUG_DQB3L_TXBST_PD_D1 = 0x196,
            ixMC_IO_DEBUG_DQB3H_TXBST_PD_D1 = 0x197,
            ixMC_IO_DEBUG_DBI_TXBST_PD_D1 = 0x198,
            ixMC_IO_DEBUG_EDC_TXBST_PD_D1 = 0x199,
            ixMC_IO_DEBUG_WCK_TXBST_PD_D1 = 0x19a,
            ixMC_IO_DEBUG_CK_TXBST_PD_D1 = 0x19b,
            ixMC_IO_DEBUG_ADDRL_TXBST_PD_D1 = 0x19c,
            ixMC_IO_DEBUG_ADDRH_TXBST_PD_D1 = 0x19d,
            ixMC_IO_DEBUG_ACMD_TXBST_PD_D1 = 0x19e,
            ixMC_IO_DEBUG_CMD_TXBST_PD_D1 = 0x19f,
            ixMC_IO_DEBUG_UP_26 = 0x1a,
            ixMC_IO_DEBUG_DQB0L_TXBST_PU_D0 = 0x1a0,
            ixMC_IO_DEBUG_DQB0H_TXBST_PU_D0 = 0x1a1,
            ixMC_IO_DEBUG_DQB1L_TXBST_PU_D0 = 0x1a2,
            ixMC_IO_DEBUG_DQB1H_TXBST_PU_D0 = 0x1a3,
            ixMC_IO_DEBUG_DQB2L_TXBST_PU_D0 = 0x1a4,
            ixMC_IO_DEBUG_DQB2H_TXBST_PU_D0 = 0x1a5,
            ixMC_IO_DEBUG_DQB3L_TXBST_PU_D0 = 0x1a6,
            ixMC_IO_DEBUG_DQB3H_TXBST_PU_D0 = 0x1a7,
            ixMC_IO_DEBUG_DBI_TXBST_PU_D0 = 0x1a8,
            ixMC_IO_DEBUG_EDC_TXBST_PU_D0 = 0x1a9,
            ixMC_IO_DEBUG_WCK_TXBST_PU_D0 = 0x1aa,
            ixMC_IO_DEBUG_CK_TXBST_PU_D0 = 0x1ab,
            ixMC_IO_DEBUG_ADDRL_TXBST_PU_D0 = 0x1ac,
            ixMC_IO_DEBUG_ADDRH_TXBST_PU_D0 = 0x1ad,
            ixMC_IO_DEBUG_ACMD_TXBST_PU_D0 = 0x1ae,
            ixMC_IO_DEBUG_CMD_TXBST_PU_D0 = 0x1af,
            ixMC_IO_DEBUG_UP_27 = 0x1b,
            ixMC_IO_DEBUG_DQB0L_TXBST_PU_D1 = 0x1b0,
            ixMC_IO_DEBUG_DQB0H_TXBST_PU_D1 = 0x1b1,
            ixMC_IO_DEBUG_DQB1L_TXBST_PU_D1 = 0x1b2,
            ixMC_IO_DEBUG_DQB1H_TXBST_PU_D1 = 0x1b3,
            ixMC_IO_DEBUG_DQB2L_TXBST_PU_D1 = 0x1b4,
            ixMC_IO_DEBUG_DQB2H_TXBST_PU_D1 = 0x1b5,
            ixMC_IO_DEBUG_DQB3L_TXBST_PU_D1 = 0x1b6,
            ixMC_IO_DEBUG_DQB3H_TXBST_PU_D1 = 0x1b7,
            ixMC_IO_DEBUG_DBI_TXBST_PU_D1 = 0x1b8,
            ixMC_IO_DEBUG_EDC_TXBST_PU_D1 = 0x1b9,
            ixMC_IO_DEBUG_WCK_TXBST_PU_D1 = 0x1ba,
            ixMC_IO_DEBUG_CK_TXBST_PU_D1 = 0x1bb,
            ixMC_IO_DEBUG_ADDRL_TXBST_PU_D1 = 0x1bc,
            ixMC_IO_DEBUG_ADDRH_TXBST_PU_D1 = 0x1bd,
            ixMC_IO_DEBUG_ACMD_TXBST_PU_D1 = 0x1be,
            ixMC_IO_DEBUG_CMD_TXBST_PU_D1 = 0x1bf,
            ixMC_IO_DEBUG_UP_28 = 0x1c,
            ixMC_IO_DEBUG_DQB0L_RX_EQ_D0 = 0x1c0,
            ixMC_IO_DEBUG_DQB0H_RX_EQ_D0 = 0x1c1,
            ixMC_IO_DEBUG_DQB1L_RX_EQ_D0 = 0x1c2,
            ixMC_IO_DEBUG_DQB1H_RX_EQ_D0 = 0x1c3,
            ixMC_IO_DEBUG_DQB2L_RX_EQ_D0 = 0x1c4,
            ixMC_IO_DEBUG_DQB2H_RX_EQ_D0 = 0x1c5,
            ixMC_IO_DEBUG_DQB3L_RX_EQ_D0 = 0x1c6,
            ixMC_IO_DEBUG_DQB3H_RX_EQ_D0 = 0x1c7,
            ixMC_IO_DEBUG_DBI_RX_EQ_D0 = 0x1c8,
            ixMC_IO_DEBUG_EDC_RX_EQ_D0 = 0x1c9,
            ixMC_IO_DEBUG_WCK_RX_EQ_D0 = 0x1ca,
            ixMC_IO_DEBUG_DQ0_RX_EQ_PM_D0 = 0x1cb,
            ixMC_IO_DEBUG_DQ1_RX_EQ_PM_D0 = 0x1cc,
            ixMC_IO_DEBUG_DQ0_RX_DYN_PM_D0 = 0x1cd,
            ixMC_IO_DEBUG_DQ1_RX_DYN_PM_D0 = 0x1ce,
            ixMC_IO_DEBUG_CMD_RX_EQ_D0 = 0x1cf,
            ixMC_IO_DEBUG_UP_29 = 0x1d,
            ixMC_IO_DEBUG_DQB0L_RX_EQ_D1 = 0x1d0,
            ixMC_IO_DEBUG_DQB0H_RX_EQ_D1 = 0x1d1,
            ixMC_IO_DEBUG_DQB1L_RX_EQ_D1 = 0x1d2,
            ixMC_IO_DEBUG_DQB1H_RX_EQ_D1 = 0x1d3,
            ixMC_IO_DEBUG_DQB2L_RX_EQ_D1 = 0x1d4,
            ixMC_IO_DEBUG_DQB2H_RX_EQ_D1 = 0x1d5,
            ixMC_IO_DEBUG_DQB3L_RX_EQ_D1 = 0x1d6,
            ixMC_IO_DEBUG_DQB3H_RX_EQ_D1 = 0x1d7,
            ixMC_IO_DEBUG_DBI_RX_EQ_D1 = 0x1d8,
            ixMC_IO_DEBUG_EDC_RX_EQ_D1 = 0x1d9,
            ixMC_IO_DEBUG_WCK_RX_EQ_D1 = 0x1da,
            ixMC_IO_DEBUG_DQ0_RX_EQ_PM_D1 = 0x1db,
            ixMC_IO_DEBUG_DQ1_RX_EQ_PM_D1 = 0x1dc,
            ixMC_IO_DEBUG_DQ0_RX_DYN_PM_D1 = 0x1dd,
            ixMC_IO_DEBUG_DQ1_RX_DYN_PM_D1 = 0x1de,
            ixMC_IO_DEBUG_CMD_RX_EQ_D1 = 0x1df,
            ixMC_IO_DEBUG_UP_30 = 0x1e,
            ixMC_IO_DEBUG_WCDR_MISC_D0 = 0x1e0,
            ixMC_IO_DEBUG_WCDR_CLKSEL_D0 = 0x1e1,
            ixMC_IO_DEBUG_WCDR_OFSCAL_D0 = 0x1e2,
            ixMC_IO_DEBUG_WCDR_RXPHASE_D0 = 0x1e3,
            ixMC_IO_DEBUG_WCDR_TXPHASE_D0 = 0x1e4,
            ixMC_IO_DEBUG_WCDR_RX_VREF_CAL_D0 = 0x1e5,
            ixMC_IO_DEBUG_WCDR_TXSLF_D0 = 0x1e6,
            ixMC_IO_DEBUG_WCDR_TXBST_PD_D0 = 0x1e7,
            ixMC_IO_DEBUG_WCDR_TXBST_PU_D0 = 0x1e8,
            ixMC_IO_DEBUG_WCDR_RX_EQ_D0 = 0x1e9,
            ixMC_IO_DEBUG_WCDR_CDR_PHSIZE_D0 = 0x1ea,
            ixMC_IO_DEBUG_WCDR_RX_EQ_PM_D0 = 0x1eb,
            ixMC_IO_DEBUG_WCDR_RX_DYN_PM_D0 = 0x1ec,
            ixMC_IO_DEBUG_UP_31 = 0x1f,
            ixMC_IO_DEBUG_WCDR_MISC_D1 = 0x1f0,
            ixMC_IO_DEBUG_WCDR_CLKSEL_D1 = 0x1f1,
            ixMC_IO_DEBUG_WCDR_OFSCAL_D1 = 0x1f2,
            ixMC_IO_DEBUG_WCDR_RXPHASE_D1 = 0x1f3,
            ixMC_IO_DEBUG_WCDR_TXPHASE_D1 = 0x1f4,
            ixMC_IO_DEBUG_WCDR_RX_VREF_CAL_D1 = 0x1f5,
            ixMC_IO_DEBUG_WCDR_TXSLF_D1 = 0x1f6,
            ixMC_IO_DEBUG_WCDR_TXBST_PD_D1 = 0x1f7,
            ixMC_IO_DEBUG_WCDR_TXBST_PU_D1 = 0x1f8,
            ixMC_IO_DEBUG_WCDR_RX_EQ_D1 = 0x1f9,
            ixMC_IO_DEBUG_WCDR_CDR_PHSIZE_D1 = 0x1fa,
            ixMC_IO_DEBUG_WCDR_RX_EQ_PM_D1 = 0x1fb,
            ixMC_IO_DEBUG_WCDR_RX_DYN_PM_D1 = 0x1fc,
            ixMC_TSM_DEBUG_MISC = 0x2,
            ixMC_IO_DEBUG_UP_2 = 0x2,
            ixMC_IO_DEBUG_UP_32 = 0x20,
            ixMC_IO_DEBUG_UP_33 = 0x21,
            ixMC_IO_DEBUG_UP_34 = 0x22,
            ixMC_IO_DEBUG_UP_35 = 0x23,
            ixMC_IO_DEBUG_UP_36 = 0x24,
            ixMC_IO_DEBUG_UP_37 = 0x25,
            ixMC_IO_DEBUG_UP_38 = 0x26,
            ixMC_IO_DEBUG_UP_39 = 0x27,
            ixMC_IO_DEBUG_UP_40 = 0x28,
            ixMC_IO_DEBUG_UP_41 = 0x29,
            ixMC_IO_DEBUG_UP_42 = 0x2a,
            ixMC_IO_DEBUG_UP_43 = 0x2b,
            ixMC_IO_DEBUG_UP_44 = 0x2c,
            ixMC_IO_DEBUG_UP_45 = 0x2d,
            ixMC_IO_DEBUG_UP_46 = 0x2e,
            ixMC_IO_DEBUG_UP_47 = 0x2f,
            ixMC_TSM_DEBUG_BCNT0 = 0x3,
            ixMC_IO_DEBUG_UP_3 = 0x3,
            ixMC_IO_DEBUG_UP_48 = 0x30,
            ixMC_IO_DEBUG_UP_49 = 0x31,
            ixMC_IO_DEBUG_UP_50 = 0x32,
            ixMC_IO_DEBUG_UP_51 = 0x33,
            ixMC_IO_DEBUG_UP_52 = 0x34,
            ixMC_IO_DEBUG_UP_53 = 0x35,
            ixMC_IO_DEBUG_UP_54 = 0x36,
            ixMC_IO_DEBUG_UP_55 = 0x37,
            ixMC_IO_DEBUG_UP_56 = 0x38,
            ixMC_IO_DEBUG_UP_57 = 0x39,
            ixMC_IO_DEBUG_UP_58 = 0x3a,
            ixMC_IO_DEBUG_UP_59 = 0x3b,
            ixMC_IO_DEBUG_UP_60 = 0x3c,
            ixMC_IO_DEBUG_UP_61 = 0x3d,
            ixMC_IO_DEBUG_UP_62 = 0x3e,
            ixMC_IO_DEBUG_UP_63 = 0x3f,
            ixMC_TSM_DEBUG_BCNT1 = 0x4,
            ixMC_IO_DEBUG_UP_4 = 0x4,
            ixMC_IO_DEBUG_UP_64 = 0x40,
            ixMC_IO_DEBUG_UP_65 = 0x41,
            ixMC_IO_DEBUG_UP_66 = 0x42,
            ixMC_IO_DEBUG_UP_67 = 0x43,
            ixMC_IO_DEBUG_UP_68 = 0x44,
            ixMC_IO_DEBUG_UP_69 = 0x45,
            ixMC_IO_DEBUG_UP_70 = 0x46,
            ixMC_IO_DEBUG_UP_71 = 0x47,
            ixMC_IO_DEBUG_UP_72 = 0x48,
            ixMC_IO_DEBUG_UP_73 = 0x49,
            ixMC_IO_DEBUG_UP_74 = 0x4a,
            ixMC_IO_DEBUG_UP_75 = 0x4b,
            ixMC_IO_DEBUG_UP_76 = 0x4c,
            ixMC_IO_DEBUG_UP_77 = 0x4d,
            ixMC_IO_DEBUG_UP_78 = 0x4e,
            ixMC_IO_DEBUG_UP_79 = 0x4f,
            ixMC_TSM_DEBUG_BCNT2 = 0x5,
            ixMC_IO_DEBUG_UP_5 = 0x5,
            ixMC_IO_DEBUG_UP_80 = 0x50,
            mmVM_L2_CNTL = 0x500,
            mmVM_L2_CNTL2 = 0x501,
            mmVM_L2_CNTL3 = 0x502,
            mmVM_L2_STATUS = 0x503,
            mmVM_CONTEXT0_CNTL = 0x504,
            mmVM_CONTEXT1_CNTL = 0x505,
            mmVM_DUMMY_PAGE_FAULT_CNTL = 0x506,
            mmVM_DUMMY_PAGE_FAULT_ADDR = 0x507,
            mmVM_CONTEXT0_CNTL2 = 0x50c,
            mmVM_CONTEXT1_CNTL2 = 0x50d,
            mmVM_CONTEXT8_PAGE_TABLE_BASE_ADDR = 0x50e,
            mmVM_CONTEXT9_PAGE_TABLE_BASE_ADDR = 0x50f,
            ixMC_IO_DEBUG_UP_81 = 0x51,
            mmVM_CONTEXT10_PAGE_TABLE_BASE_ADDR = 0x510,
            mmVM_CONTEXT11_PAGE_TABLE_BASE_ADDR = 0x511,
            mmVM_CONTEXT12_PAGE_TABLE_BASE_ADDR = 0x512,
            mmVM_CONTEXT13_PAGE_TABLE_BASE_ADDR = 0x513,
            mmVM_CONTEXT14_PAGE_TABLE_BASE_ADDR = 0x514,
            mmVM_CONTEXT15_PAGE_TABLE_BASE_ADDR = 0x515,
            mmVM_INVALIDATE_REQUEST = 0x51e,
            mmVM_INVALIDATE_RESPONSE = 0x51f,
            ixMC_IO_DEBUG_UP_82 = 0x52,
            mmVM_PRT_APERTURE0_LOW_ADDR = 0x52c,
            mmVM_PRT_APERTURE1_LOW_ADDR = 0x52d,
            mmVM_PRT_APERTURE2_LOW_ADDR = 0x52e,
            mmVM_PRT_APERTURE3_LOW_ADDR = 0x52f,
            ixMC_IO_DEBUG_UP_83 = 0x53,
            mmVM_PRT_APERTURE0_HIGH_ADDR = 0x530,
            mmVM_PRT_APERTURE1_HIGH_ADDR = 0x531,
            mmVM_PRT_APERTURE2_HIGH_ADDR = 0x532,
            mmVM_PRT_APERTURE3_HIGH_ADDR = 0x533,
            mmVM_PRT_CNTL = 0x534,
            mmVM_CONTEXTS_DISABLE = 0x535,
            mmVM_CONTEXT0_PROTECTION_FAULT_STATUS = 0x536,
            mmVM_CONTEXT1_PROTECTION_FAULT_STATUS = 0x537,
            mmVM_CONTEXT0_PROTECTION_FAULT_MCCLIENT = 0x538,
            mmVM_CONTEXT1_PROTECTION_FAULT_MCCLIENT = 0x539,
            mmVM_CONTEXT0_PROTECTION_FAULT_ADDR = 0x53e,
            mmVM_CONTEXT1_PROTECTION_FAULT_ADDR = 0x53f,
            ixMC_IO_DEBUG_UP_84 = 0x54,
            mmVM_CONTEXT0_PROTECTION_FAULT_DEFAULT_ADDR = 0x546,
            mmVM_CONTEXT1_PROTECTION_FAULT_DEFAULT_ADDR = 0x547,
            mmVM_FAULT_CLIENT_ID = 0x54e,
            mmVM_CONTEXT0_PAGE_TABLE_BASE_ADDR = 0x54f,
            ixMC_IO_DEBUG_UP_85 = 0x55,
            mmVM_CONTEXT1_PAGE_TABLE_BASE_ADDR = 0x550,
            mmVM_CONTEXT2_PAGE_TABLE_BASE_ADDR = 0x551,
            mmVM_CONTEXT3_PAGE_TABLE_BASE_ADDR = 0x552,
            mmVM_CONTEXT4_PAGE_TABLE_BASE_ADDR = 0x553,
            mmVM_CONTEXT5_PAGE_TABLE_BASE_ADDR = 0x554,
            mmVM_CONTEXT6_PAGE_TABLE_BASE_ADDR = 0x555,
            mmVM_CONTEXT7_PAGE_TABLE_BASE_ADDR = 0x556,
            mmVM_CONTEXT0_PAGE_TABLE_START_ADDR = 0x557,
            mmVM_CONTEXT1_PAGE_TABLE_START_ADDR = 0x558,
            mmVM_CONTEXT0_PAGE_TABLE_END_ADDR = 0x55f,
            ixMC_IO_DEBUG_UP_86 = 0x56,
            mmVM_CONTEXT1_PAGE_TABLE_END_ADDR = 0x560,
            mmVM_DEBUG = 0x56f,
            ixMC_IO_DEBUG_UP_87 = 0x57,
            mmVM_L2_CG = 0x570,
            mmVM_L2_BANK_SELECT_MASKA = 0x572,
            mmVM_L2_BANK_SELECT_MASKB = 0x573,
            mmVM_L2_CONTEXT1_IDENTITY_APERTURE_LOW_ADDR = 0x575,
            mmVM_L2_CONTEXT1_IDENTITY_APERTURE_HIGH_ADDR = 0x576,
            mmVM_L2_CONTEXT_IDENTITY_PHYSICAL_OFFSET = 0x577,
            mmVM_L2_CNTL4 = 0x578,
            mmVM_L2_BANK_SELECT_RESERVED_CID = 0x579,
            mmVM_L2_BANK_SELECT_RESERVED_CID2 = 0x57a,
            ixMC_IO_DEBUG_UP_88 = 0x58,
            ixMC_IO_DEBUG_UP_89 = 0x59,
            ixMC_IO_DEBUG_UP_90 = 0x5a,
            ixMC_IO_DEBUG_UP_91 = 0x5b,
            ixMC_IO_DEBUG_UP_92 = 0x5c,
            ixMC_IO_DEBUG_UP_93 = 0x5d,
            ixMC_IO_DEBUG_UP_94 = 0x5e,
            mmMCIF_WB0_MCIF_WB_BUFMGR_SW_CONTROL = 0x5e78,
            mmMCIF_WB_BUFMGR_SW_CONTROL = 0x5e78,
            mmMCIF_WB0_MCIF_WB_BUFMGR_CUR_LINE_R = 0x5e79,
            mmMCIF_WB_BUFMGR_CUR_LINE_R = 0x5e79,
            mmMCIF_WB0_MCIF_WB_BUFMGR_STATUS = 0x5e7a,
            mmMCIF_WB_BUFMGR_STATUS = 0x5e7a,
            mmMCIF_WB0_MCIF_WB_BUF_PITCH = 0x5e7b,
            mmMCIF_WB_BUF_PITCH = 0x5e7b,
            mmMCIF_WB0_MCIF_WB_BUF_1_STATUS = 0x5e7c,
            mmMCIF_WB_BUF_1_STATUS = 0x5e7c,
            mmMCIF_WB0_MCIF_WB_BUF_1_STATUS2 = 0x5e7d,
            mmMCIF_WB_BUF_1_STATUS2 = 0x5e7d,
            mmMCIF_WB0_MCIF_WB_BUF_2_STATUS = 0x5e7e,
            mmMCIF_WB_BUF_2_STATUS = 0x5e7e,
            mmMCIF_WB0_MCIF_WB_BUF_2_STATUS2 = 0x5e7f,
            mmMCIF_WB_BUF_2_STATUS2 = 0x5e7f,
            mmMCIF_WB0_MCIF_WB_BUF_3_STATUS = 0x5e80,
            mmMCIF_WB_BUF_3_STATUS = 0x5e80,
            mmMCIF_WB0_MCIF_WB_BUF_3_STATUS2 = 0x5e81,
            mmMCIF_WB_BUF_3_STATUS2 = 0x5e81,
            mmMCIF_WB0_MCIF_WB_BUF_4_STATUS = 0x5e82,
            mmMCIF_WB_BUF_4_STATUS = 0x5e82,
            mmMCIF_WB0_MCIF_WB_BUF_4_STATUS2 = 0x5e83,
            mmMCIF_WB_BUF_4_STATUS2 = 0x5e83,
            mmMCIF_WB0_MCIF_WB_ARBITRATION_CONTROL = 0x5e84,
            mmMCIF_WB_ARBITRATION_CONTROL = 0x5e84,
            mmMCIF_WB0_MCIF_WB_URGENCY_WATERMARK = 0x5e85,
            mmMCIF_WB_URGENCY_WATERMARK = 0x5e85,
            mmMCIF_WB0_MCIF_WB_TEST_DEBUG_INDEX = 0x5e86,
            mmMCIF_WB_TEST_DEBUG_INDEX = 0x5e86,
            mmMCIF_WB0_MCIF_WB_TEST_DEBUG_DATA = 0x5e87,
            mmMCIF_WB_TEST_DEBUG_DATA = 0x5e87,
            mmMCIF_WB0_MCIF_WB_BUF_1_ADDR_Y = 0x5e88,
            mmMCIF_WB_BUF_1_ADDR_Y = 0x5e88,
            mmMCIF_WB0_MCIF_WB_BUF_1_ADDR_Y_OFFSET = 0x5e89,
            mmMCIF_WB_BUF_1_ADDR_Y_OFFSET = 0x5e89,
            mmMCIF_WB0_MCIF_WB_BUF_1_ADDR_C = 0x5e8a,
            mmMCIF_WB_BUF_1_ADDR_C = 0x5e8a,
            mmMCIF_WB0_MCIF_WB_BUF_1_ADDR_C_OFFSET = 0x5e8b,
            mmMCIF_WB_BUF_1_ADDR_C_OFFSET = 0x5e8b,
            mmMCIF_WB0_MCIF_WB_BUF_2_ADDR_Y = 0x5e8c,
            mmMCIF_WB_BUF_2_ADDR_Y = 0x5e8c,
            mmMCIF_WB0_MCIF_WB_BUF_2_ADDR_Y_OFFSET = 0x5e8d,
            mmMCIF_WB_BUF_2_ADDR_Y_OFFSET = 0x5e8d,
            mmMCIF_WB0_MCIF_WB_BUF_2_ADDR_C = 0x5e8e,
            mmMCIF_WB_BUF_2_ADDR_C = 0x5e8e,
            mmMCIF_WB0_MCIF_WB_BUF_2_ADDR_C_OFFSET = 0x5e8f,
            mmMCIF_WB_BUF_2_ADDR_C_OFFSET = 0x5e8f,
            mmMCIF_WB0_MCIF_WB_BUF_3_ADDR_Y = 0x5e90,
            mmMCIF_WB_BUF_3_ADDR_Y = 0x5e90,
            mmMCIF_WB0_MCIF_WB_BUF_3_ADDR_Y_OFFSET = 0x5e91,
            mmMCIF_WB_BUF_3_ADDR_Y_OFFSET = 0x5e91,
            mmMCIF_WB0_MCIF_WB_BUF_3_ADDR_C = 0x5e92,
            mmMCIF_WB_BUF_3_ADDR_C = 0x5e92,
            mmMCIF_WB0_MCIF_WB_BUF_3_ADDR_C_OFFSET = 0x5e93,
            mmMCIF_WB_BUF_3_ADDR_C_OFFSET = 0x5e93,
            mmMCIF_WB0_MCIF_WB_BUF_4_ADDR_Y = 0x5e94,
            mmMCIF_WB_BUF_4_ADDR_Y = 0x5e94,
            mmMCIF_WB0_MCIF_WB_BUF_4_ADDR_Y_OFFSET = 0x5e95,
            mmMCIF_WB_BUF_4_ADDR_Y_OFFSET = 0x5e95,
            mmMCIF_WB0_MCIF_WB_BUF_4_ADDR_C = 0x5e96,
            mmMCIF_WB_BUF_4_ADDR_C = 0x5e96,
            mmMCIF_WB0_MCIF_WB_BUF_4_ADDR_C_OFFSET = 0x5e97,
            mmMCIF_WB_BUF_4_ADDR_C_OFFSET = 0x5e97,
            mmMCIF_WB0_MCIF_WB_BUFMGR_VCE_CONTROL = 0x5e98,
            mmMCIF_WB_BUFMGR_VCE_CONTROL = 0x5e98,
            mmMCIF_WB0_MCIF_WB_HVVMID_CONTROL = 0x5e99,
            mmMCIF_WB_HVVMID_CONTROL = 0x5e99,
            mmMCIF_WB1_MCIF_WB_BUFMGR_SW_CONTROL = 0x5eb8,
            mmMCIF_WB1_MCIF_WB_BUFMGR_CUR_LINE_R = 0x5eb9,
            mmMCIF_WB1_MCIF_WB_BUFMGR_STATUS = 0x5eba,
            mmMCIF_WB1_MCIF_WB_BUF_PITCH = 0x5ebb,
            mmMCIF_WB1_MCIF_WB_BUF_1_STATUS = 0x5ebc,
            mmMCIF_WB1_MCIF_WB_BUF_1_STATUS2 = 0x5ebd,
            mmMCIF_WB1_MCIF_WB_BUF_2_STATUS = 0x5ebe,
            mmMCIF_WB1_MCIF_WB_BUF_2_STATUS2 = 0x5ebf,
            mmMCIF_WB1_MCIF_WB_BUF_3_STATUS = 0x5ec0,
            mmMCIF_WB1_MCIF_WB_BUF_3_STATUS2 = 0x5ec1,
            mmMCIF_WB1_MCIF_WB_BUF_4_STATUS = 0x5ec2,
            mmMCIF_WB1_MCIF_WB_BUF_4_STATUS2 = 0x5ec3,
            mmMCIF_WB1_MCIF_WB_ARBITRATION_CONTROL = 0x5ec4,
            mmMCIF_WB1_MCIF_WB_URGENCY_WATERMARK = 0x5ec5,
            mmMCIF_WB1_MCIF_WB_TEST_DEBUG_INDEX = 0x5ec6,
            mmMCIF_WB1_MCIF_WB_TEST_DEBUG_DATA = 0x5ec7,
            mmMCIF_WB1_MCIF_WB_BUF_1_ADDR_Y = 0x5ec8,
            mmMCIF_WB1_MCIF_WB_BUF_1_ADDR_Y_OFFSET = 0x5ec9,
            mmMCIF_WB1_MCIF_WB_BUF_1_ADDR_C = 0x5eca,
            mmMCIF_WB1_MCIF_WB_BUF_1_ADDR_C_OFFSET = 0x5ecb,
            mmMCIF_WB1_MCIF_WB_BUF_2_ADDR_Y = 0x5ecc,
            mmMCIF_WB1_MCIF_WB_BUF_2_ADDR_Y_OFFSET = 0x5ecd,
            mmMCIF_WB1_MCIF_WB_BUF_2_ADDR_C = 0x5ece,
            mmMCIF_WB1_MCIF_WB_BUF_2_ADDR_C_OFFSET = 0x5ecf,
            mmMCIF_WB1_MCIF_WB_BUF_3_ADDR_Y = 0x5ed0,
            mmMCIF_WB1_MCIF_WB_BUF_3_ADDR_Y_OFFSET = 0x5ed1,
            mmMCIF_WB1_MCIF_WB_BUF_3_ADDR_C = 0x5ed2,
            mmMCIF_WB1_MCIF_WB_BUF_3_ADDR_C_OFFSET = 0x5ed3,
            mmMCIF_WB1_MCIF_WB_BUF_4_ADDR_Y = 0x5ed4,
            mmMCIF_WB1_MCIF_WB_BUF_4_ADDR_Y_OFFSET = 0x5ed5,
            mmMCIF_WB1_MCIF_WB_BUF_4_ADDR_C = 0x5ed6,
            mmMCIF_WB1_MCIF_WB_BUF_4_ADDR_C_OFFSET = 0x5ed7,
            mmMCIF_WB1_MCIF_WB_BUFMGR_VCE_CONTROL = 0x5ed8,
            mmMCIF_WB1_MCIF_WB_HVVMID_CONTROL = 0x5ed9,
            mmMCIF_WB2_MCIF_WB_BUFMGR_SW_CONTROL = 0x5ef8,
            mmMCIF_WB2_MCIF_WB_BUFMGR_CUR_LINE_R = 0x5ef9,
            mmMCIF_WB2_MCIF_WB_BUFMGR_STATUS = 0x5efa,
            mmMCIF_WB2_MCIF_WB_BUF_PITCH = 0x5efb,
            mmMCIF_WB2_MCIF_WB_BUF_1_STATUS = 0x5efc,
            mmMCIF_WB2_MCIF_WB_BUF_1_STATUS2 = 0x5efd,
            mmMCIF_WB2_MCIF_WB_BUF_2_STATUS = 0x5efe,
            mmMCIF_WB2_MCIF_WB_BUF_2_STATUS2 = 0x5eff,
            ixMC_IO_DEBUG_UP_95 = 0x5f,
            mmMCIF_WB2_MCIF_WB_BUF_3_STATUS = 0x5f00,
            mmMCIF_WB2_MCIF_WB_BUF_3_STATUS2 = 0x5f01,
            mmMCIF_WB2_MCIF_WB_BUF_4_STATUS = 0x5f02,
            mmMCIF_WB2_MCIF_WB_BUF_4_STATUS2 = 0x5f03,
            mmMCIF_WB2_MCIF_WB_ARBITRATION_CONTROL = 0x5f04,
            mmMCIF_WB2_MCIF_WB_URGENCY_WATERMARK = 0x5f05,
            mmMCIF_WB2_MCIF_WB_TEST_DEBUG_INDEX = 0x5f06,
            mmMCIF_WB2_MCIF_WB_TEST_DEBUG_DATA = 0x5f07,
            mmMCIF_WB2_MCIF_WB_BUF_1_ADDR_Y = 0x5f08,
            mmMCIF_WB2_MCIF_WB_BUF_1_ADDR_Y_OFFSET = 0x5f09,
            mmMCIF_WB2_MCIF_WB_BUF_1_ADDR_C = 0x5f0a,
            mmMCIF_WB2_MCIF_WB_BUF_1_ADDR_C_OFFSET = 0x5f0b,
            mmMCIF_WB2_MCIF_WB_BUF_2_ADDR_Y = 0x5f0c,
            mmMCIF_WB2_MCIF_WB_BUF_2_ADDR_Y_OFFSET = 0x5f0d,
            mmMCIF_WB2_MCIF_WB_BUF_2_ADDR_C = 0x5f0e,
            mmMCIF_WB2_MCIF_WB_BUF_2_ADDR_C_OFFSET = 0x5f0f,
            mmMCIF_WB2_MCIF_WB_BUF_3_ADDR_Y = 0x5f10,
            mmMCIF_WB2_MCIF_WB_BUF_3_ADDR_Y_OFFSET = 0x5f11,
            mmMCIF_WB2_MCIF_WB_BUF_3_ADDR_C = 0x5f12,
            mmMCIF_WB2_MCIF_WB_BUF_3_ADDR_C_OFFSET = 0x5f13,
            mmMCIF_WB2_MCIF_WB_BUF_4_ADDR_Y = 0x5f14,
            mmMCIF_WB2_MCIF_WB_BUF_4_ADDR_Y_OFFSET = 0x5f15,
            mmMCIF_WB2_MCIF_WB_BUF_4_ADDR_C = 0x5f16,
            mmMCIF_WB2_MCIF_WB_BUF_4_ADDR_C_OFFSET = 0x5f17,
            mmMCIF_WB2_MCIF_WB_BUFMGR_VCE_CONTROL = 0x5f18,
            mmMCIF_WB2_MCIF_WB_HVVMID_CONTROL = 0x5f19,
            ixMC_TSM_DEBUG_BCNT3 = 0x6,
            ixMC_IO_DEBUG_UP_6 = 0x6,
            ixMC_IO_DEBUG_UP_96 = 0x60,
            ixMC_IO_DEBUG_UP_97 = 0x61,
            ixMC_IO_DEBUG_UP_98 = 0x62,
            ixMC_IO_DEBUG_UP_99 = 0x63,
            ixMC_IO_DEBUG_UP_100 = 0x64,
            ixMC_IO_DEBUG_UP_101 = 0x65,
            ixMC_IO_DEBUG_UP_102 = 0x66,
            ixMC_IO_DEBUG_UP_103 = 0x67,
            ixMC_IO_DEBUG_UP_104 = 0x68,
            ixMC_IO_DEBUG_UP_105 = 0x69,
            ixMC_IO_DEBUG_UP_106 = 0x6a,
            ixMC_IO_DEBUG_UP_107 = 0x6b,
            ixMC_IO_DEBUG_UP_108 = 0x6c,
            ixMC_IO_DEBUG_UP_109 = 0x6d,
            ixMC_IO_DEBUG_UP_110 = 0x6e,
            ixMC_IO_DEBUG_UP_111 = 0x6f,
            ixMC_TSM_DEBUG_BCNT4 = 0x7,
            ixMC_IO_DEBUG_UP_7 = 0x7,
            ixMC_IO_DEBUG_UP_112 = 0x70,
            ixMC_IO_DEBUG_UP_113 = 0x71,
            ixMC_IO_DEBUG_UP_114 = 0x72,
            ixMC_IO_DEBUG_UP_115 = 0x73,
            ixMC_IO_DEBUG_UP_116 = 0x74,
            ixMC_IO_DEBUG_UP_117 = 0x75,
            ixMC_IO_DEBUG_UP_118 = 0x76,
            ixMC_IO_DEBUG_UP_119 = 0x77,
            ixMC_IO_DEBUG_UP_120 = 0x78,
            ixMC_IO_DEBUG_UP_121 = 0x79,
            ixMC_IO_DEBUG_UP_122 = 0x7a,
            mmMC_CITF_PERFCOUNTER_LO = 0x7a0,
            mmMC_HUB_PERFCOUNTER_LO = 0x7a1,
            mmMC_RPB_PERFCOUNTER_LO = 0x7a2,
            mmMC_MCBVM_PERFCOUNTER_LO = 0x7a3,
            mmMC_MCDVM_PERFCOUNTER_LO = 0x7a4,
            mmMC_VM_L2_PERFCOUNTER_LO = 0x7a5,
            mmMC_ARB_PERFCOUNTER_LO = 0x7a6,
            mmATC_PERFCOUNTER_LO = 0x7a7,
            mmMC_CITF_PERFCOUNTER_HI = 0x7a8,
            mmMC_HUB_PERFCOUNTER_HI = 0x7a9,
            mmMC_MCBVM_PERFCOUNTER_HI = 0x7aa,
            mmMC_MCDVM_PERFCOUNTER_HI = 0x7ab,
            mmMC_RPB_PERFCOUNTER_HI = 0x7ac,
            mmMC_VM_L2_PERFCOUNTER_HI = 0x7ad,
            mmMC_ARB_PERFCOUNTER_HI = 0x7ae,
            mmATC_PERFCOUNTER_HI = 0x7af,
            ixMC_IO_DEBUG_UP_123 = 0x7b,
            mmMC_CITF_PERFCOUNTER0_CFG = 0x7b0,
            mmMC_CITF_PERFCOUNTER1_CFG = 0x7b1,
            mmMC_CITF_PERFCOUNTER2_CFG = 0x7b2,
            mmMC_CITF_PERFCOUNTER3_CFG = 0x7b3,
            mmMC_HUB_PERFCOUNTER0_CFG = 0x7b4,
            mmMC_HUB_PERFCOUNTER1_CFG = 0x7b5,
            mmMC_HUB_PERFCOUNTER2_CFG = 0x7b6,
            mmMC_HUB_PERFCOUNTER3_CFG = 0x7b7,
            mmMC_RPB_PERFCOUNTER0_CFG = 0x7b8,
            mmMC_RPB_PERFCOUNTER1_CFG = 0x7b9,
            mmMC_RPB_PERFCOUNTER2_CFG = 0x7ba,
            mmMC_RPB_PERFCOUNTER3_CFG = 0x7bb,
            mmMC_ARB_PERFCOUNTER0_CFG = 0x7bc,
            mmMC_ARB_PERFCOUNTER1_CFG = 0x7bd,
            mmMC_ARB_PERFCOUNTER2_CFG = 0x7be,
            mmMC_ARB_PERFCOUNTER3_CFG = 0x7bf,
            ixMC_IO_DEBUG_UP_124 = 0x7c,
            mmMC_MCBVM_PERFCOUNTER0_CFG = 0x7c0,
            mmMC_MCBVM_PERFCOUNTER1_CFG = 0x7c1,
            mmMC_MCBVM_PERFCOUNTER2_CFG = 0x7c2,
            mmMC_MCBVM_PERFCOUNTER3_CFG = 0x7c3,
            mmMC_MCDVM_PERFCOUNTER0_CFG = 0x7c4,
            mmMC_MCDVM_PERFCOUNTER1_CFG = 0x7c5,
            mmMC_MCDVM_PERFCOUNTER2_CFG = 0x7c6,
            mmMC_MCDVM_PERFCOUNTER3_CFG = 0x7c7,
            mmATC_PERFCOUNTER0_CFG = 0x7c8,
            mmATC_PERFCOUNTER1_CFG = 0x7c9,
            mmATC_PERFCOUNTER2_CFG = 0x7ca,
            mmATC_PERFCOUNTER3_CFG = 0x7cb,
            mmMC_VM_L2_PERFCOUNTER0_CFG = 0x7cc,
            mmMC_VM_L2_PERFCOUNTER1_CFG = 0x7cd,
            mmMC_CITF_PERFCOUNTER_RSLT_CNTL = 0x7ce,
            mmMC_HUB_PERFCOUNTER_RSLT_CNTL = 0x7cf,
            ixMC_IO_DEBUG_UP_125 = 0x7d,
            mmMC_RPB_PERFCOUNTER_RSLT_CNTL = 0x7d0,
            mmMC_MCBVM_PERFCOUNTER_RSLT_CNTL = 0x7d1,
            mmMC_MCDVM_PERFCOUNTER_RSLT_CNTL = 0x7d2,
            mmMC_VM_L2_PERFCOUNTER_RSLT_CNTL = 0x7d3,
            mmMC_ARB_PERFCOUNTER_RSLT_CNTL = 0x7d4,
            mmATC_PERFCOUNTER_RSLT_CNTL = 0x7d5,
            mmCHUB_ATC_PERFCOUNTER_LO = 0x7d6,
            mmCHUB_ATC_PERFCOUNTER_HI = 0x7d7,
            mmCHUB_ATC_PERFCOUNTER0_CFG = 0x7d8,
            mmCHUB_ATC_PERFCOUNTER1_CFG = 0x7d9,
            mmCHUB_ATC_PERFCOUNTER_RSLT_CNTL = 0x7da,
            ixMC_IO_DEBUG_UP_126 = 0x7e,
            ixMC_IO_DEBUG_UP_127 = 0x7f,
            ixMC_TSM_DEBUG_BCNT5 = 0x8,
            ixMC_IO_DEBUG_UP_8 = 0x8,
            ixMC_IO_DEBUG_UP_128 = 0x80,
            mmMC_CONFIG = 0x800,
            mmMC_SHARED_CHMAP = 0x801,
            mmMC_SHARED_CHREMAP = 0x802,
            mmMC_RD_GRP_GFX = 0x803,
            mmMC_WR_GRP_GFX = 0x804,
            mmMC_RD_GRP_SYS = 0x805,
            mmMC_WR_GRP_SYS = 0x806,
            mmMC_RD_GRP_OTH = 0x807,
            mmMC_WR_GRP_OTH = 0x808,
            mmMC_VM_FB_LOCATION = 0x809,
            mmMC_VM_AGP_TOP = 0x80a,
            mmMC_VM_AGP_BOT = 0x80b,
            mmMC_VM_AGP_BASE = 0x80c,
            mmMC_VM_SYSTEM_APERTURE_LOW_ADDR = 0x80d,
            mmMC_VM_SYSTEM_APERTURE_HIGH_ADDR = 0x80e,
            mmMC_VM_SYSTEM_APERTURE_DEFAULT_ADDR = 0x80f,
            ixMC_IO_DEBUG_UP_129 = 0x81,
            mmMC_VM_DC_WRITE_CNTL = 0x810,
            mmMC_VM_DC_WRITE_HIT_REGION_0_LOW_ADDR = 0x811,
            mmMC_VM_DC_WRITE_HIT_REGION_1_LOW_ADDR = 0x812,
            mmMC_VM_DC_WRITE_HIT_REGION_2_LOW_ADDR = 0x813,
            mmMC_VM_DC_WRITE_HIT_REGION_3_LOW_ADDR = 0x814,
            mmMC_VM_DC_WRITE_HIT_REGION_0_HIGH_ADDR = 0x815,
            mmMC_VM_DC_WRITE_HIT_REGION_1_HIGH_ADDR = 0x816,
            mmMC_VM_DC_WRITE_HIT_REGION_2_HIGH_ADDR = 0x817,
            mmMC_VM_DC_WRITE_HIT_REGION_3_HIGH_ADDR = 0x818,
            mmMC_VM_MX_L1_TLB_CNTL = 0x819,
            mmMC_VM_FB_OFFSET = 0x81a,
            mmMC_VM_STEERING = 0x81b,
            mmMC_SHARED_CHREMAP2 = 0x81c,
            mmMC_SHARED_VF_ENABLE = 0x81d,
            mmMC_SHARED_VIRT_RESET_REQ = 0x81e,
            mmMC_SHARED_ACTIVE_FCN_ID = 0x81f,
            ixMC_IO_DEBUG_UP_130 = 0x82,
            mmMC_CONFIG_MCD = 0x828,
            mmMC_CG_CONFIG_MCD = 0x829,
            mmMC_MEM_POWER_LS = 0x82a,
            mmMC_SHARED_BLACKOUT_CNTL = 0x82b,
            mmMC_HUB_MISC_POWER = 0x82d,
            mmMC_HUB_MISC_HUB_CG = 0x82e,
            mmMC_HUB_MISC_VM_CG = 0x82f,
            ixMC_IO_DEBUG_UP_131 = 0x83,
            mmMC_HUB_MISC_SIP_CG = 0x830,
            mmMC_HUB_MISC_STATUS = 0x832,
            mmMC_HUB_MISC_OVERRIDE = 0x833,
            mmMC_HUB_MISC_FRAMING = 0x834,
            mmMC_HUB_WDP_CNTL = 0x835,
            mmMC_HUB_WDP_ERR = 0x836,
            mmMC_HUB_WDP_BP = 0x837,
            mmMC_HUB_WDP_STATUS = 0x838,
            mmMC_HUB_RDREQ_STATUS = 0x839,
            mmMC_HUB_WRRET_STATUS = 0x83a,
            mmMC_HUB_RDREQ_CNTL = 0x83b,
            mmMC_HUB_WRRET_CNTL = 0x83c,
            mmMC_HUB_RDREQ_WTM_CNTL = 0x83d,
            mmMC_HUB_WDP_WTM_CNTL = 0x83e,
            mmMC_HUB_WDP_CREDITS = 0x83f,
            ixMC_IO_DEBUG_UP_132 = 0x84,
            mmMC_HUB_WDP_CREDITS2 = 0x840,
            mmMC_HUB_WDP_GBL0 = 0x841,
            mmMC_HUB_WDP_GBL1 = 0x842,
            mmMC_HUB_WDP_CREDITS3 = 0x843,
            mmMC_HUB_RDREQ_CREDITS = 0x844,
            mmMC_HUB_RDREQ_CREDITS2 = 0x845,
            mmMC_HUB_SHARED_DAGB_DLY = 0x846,
            mmMC_HUB_MISC_IDLE_STATUS = 0x847,
            mmMC_HUB_RDREQ_DMIF_LIMIT = 0x848,
            mmMC_HUB_RDREQ_ACPG_LIMIT = 0x849,
            mmMC_HUB_WDP_BYPASS_GBL0 = 0x84a,
            mmMC_HUB_WDP_BYPASS_GBL1 = 0x84b,
            mmMC_HUB_RDREQ_BYPASS_GBL0 = 0x84c,
            mmMC_HUB_WDP_SH2 = 0x84d,
            mmMC_HUB_WDP_SH3 = 0x84e,
            mmMC_HUB_MISC_ATOMIC_IDLE_STATUS = 0x84f,
            ixMC_IO_DEBUG_UP_133 = 0x85,
            mmMC_HUB_WDP_VIN0 = 0x850,
            mmMC_HUB_RDREQ_MCDW = 0x851,
            mmMC_HUB_RDREQ_MCDX = 0x852,
            mmMC_HUB_RDREQ_MCDY = 0x853,
            mmMC_HUB_RDREQ_MCDZ = 0x854,
            mmMC_HUB_RDREQ_SIP = 0x855,
            mmMC_HUB_RDREQ_GBL0 = 0x856,
            mmMC_HUB_RDREQ_GBL1 = 0x857,
            mmMC_HUB_RDREQ_SMU = 0x858,
            mmMC_HUB_RDREQ_SDMA0 = 0x859,
            mmMC_HUB_RDREQ_HDP = 0x85a,
            mmMC_HUB_RDREQ_SDMA1 = 0x85b,
            mmMC_HUB_RDREQ_RLC = 0x85c,
            mmMC_HUB_RDREQ_SEM = 0x85d,
            mmMC_HUB_RDREQ_VCE0 = 0x85e,
            mmMC_HUB_RDREQ_UMC = 0x85f,
            ixMC_IO_DEBUG_UP_134 = 0x86,
            mmMC_HUB_RDREQ_UVD = 0x860,
            mmMC_HUB_RDREQ_TLS = 0x861,
            mmMC_HUB_RDREQ_DMIF = 0x862,
            mmMC_HUB_RDREQ_MCIF = 0x863,
            mmMC_HUB_RDREQ_VMC = 0x864,
            mmMC_HUB_RDREQ_VCEU0 = 0x865,
            mmMC_HUB_WDP_MCDW = 0x866,
            mmMC_HUB_WDP_MCDX = 0x867,
            mmMC_HUB_WDP_MCDY = 0x868,
            mmMC_HUB_WDP_MCDZ = 0x869,
            mmMC_HUB_WDP_SIP = 0x86a,
            mmMC_HUB_WDP_SDMA1 = 0x86b,
            mmMC_HUB_WDP_SH0 = 0x86c,
            mmMC_HUB_WDP_MCIF = 0x86d,
            mmMC_HUB_WDP_VCE0 = 0x86e,
            mmMC_HUB_WDP_XDP = 0x86f,
            ixMC_IO_DEBUG_UP_135 = 0x87,
            mmMC_HUB_WDP_IH = 0x870,
            mmMC_HUB_WDP_RLC = 0x871,
            mmMC_HUB_WDP_SEM = 0x872,
            mmMC_HUB_WDP_SMU = 0x873,
            mmMC_HUB_WDP_SH1 = 0x874,
            mmMC_HUB_WDP_UMC = 0x875,
            mmMC_HUB_WDP_UVD = 0x876,
            mmMC_HUB_WDP_HDP = 0x877,
            mmMC_HUB_WDP_SDMA0 = 0x878,
            mmMC_HUB_WRRET_MCDW = 0x879,
            mmMC_HUB_WRRET_MCDX = 0x87a,
            mmMC_HUB_WRRET_MCDY = 0x87b,
            mmMC_HUB_WRRET_MCDZ = 0x87c,
            mmMC_HUB_WDP_VCEU0 = 0x87d,
            mmMC_HUB_WDP_XDMAM = 0x87e,
            mmMC_HUB_WDP_XDMA = 0x87f,
            ixMC_IO_DEBUG_UP_136 = 0x88,
            mmMC_HUB_RDREQ_XDMAM = 0x880,
            mmMC_HUB_RDREQ_ACPG = 0x881,
            mmMC_HUB_RDREQ_ACPO = 0x882,
            mmMC_HUB_RDREQ_SAMMSP = 0x883,
            mmMC_HUB_RDREQ_VP8 = 0x884,
            mmMC_HUB_RDREQ_VP8U = 0x885,
            mmMC_HUB_WDP_ACPG = 0x886,
            mmMC_HUB_WDP_ACPO = 0x887,
            mmMC_HUB_WDP_SAMMSP = 0x888,
            mmMC_HUB_WDP_VP8 = 0x889,
            mmMC_HUB_WDP_VP8U = 0x88a,
            ixMC_IO_DEBUG_UP_137 = 0x89,
            mmMC_VM_MB_L1_TLB0_DEBUG = 0x891,
            mmMC_VM_MB_L1_TLB1_DEBUG = 0x892,
            mmMC_VM_MB_L1_TLB2_DEBUG = 0x893,
            mmMC_VM_MB_L1_TLB0_STATUS = 0x895,
            mmMC_VM_MB_L1_TLB1_STATUS = 0x896,
            mmMC_VM_MB_L1_TLB2_STATUS = 0x897,
            ixMC_IO_DEBUG_UP_138 = 0x8a,
            mmMC_VM_MB_L2ARBITER_L2_CREDITS = 0x8a1,
            mmMC_VM_MB_L1_TLB3_DEBUG = 0x8a5,
            mmMC_VM_MB_L1_TLB3_STATUS = 0x8a6,
            ixMC_IO_DEBUG_UP_139 = 0x8b,
            ixMC_IO_DEBUG_UP_140 = 0x8c,
            mmMC_XPB_RTR_SRC_APRTR0 = 0x8cd,
            mmMC_XPB_RTR_SRC_APRTR1 = 0x8ce,
            mmMC_XPB_RTR_SRC_APRTR2 = 0x8cf,
            ixMC_IO_DEBUG_UP_141 = 0x8d,
            mmMC_XPB_RTR_SRC_APRTR3 = 0x8d0,
            mmMC_XPB_RTR_SRC_APRTR4 = 0x8d1,
            mmMC_XPB_RTR_SRC_APRTR5 = 0x8d2,
            mmMC_XPB_RTR_SRC_APRTR6 = 0x8d3,
            mmMC_XPB_RTR_SRC_APRTR7 = 0x8d4,
            mmMC_XPB_RTR_SRC_APRTR8 = 0x8d5,
            mmMC_XPB_RTR_SRC_APRTR9 = 0x8d6,
            mmMC_XPB_XDMA_RTR_SRC_APRTR0 = 0x8d7,
            mmMC_XPB_XDMA_RTR_SRC_APRTR1 = 0x8d8,
            mmMC_XPB_XDMA_RTR_SRC_APRTR2 = 0x8d9,
            mmMC_XPB_XDMA_RTR_SRC_APRTR3 = 0x8da,
            mmMC_XPB_RTR_DEST_MAP0 = 0x8db,
            mmMC_XPB_RTR_DEST_MAP1 = 0x8dc,
            mmMC_XPB_RTR_DEST_MAP2 = 0x8dd,
            mmMC_XPB_RTR_DEST_MAP3 = 0x8de,
            mmMC_XPB_RTR_DEST_MAP4 = 0x8df,
            ixMC_IO_DEBUG_UP_142 = 0x8e,
            mmMC_XPB_RTR_DEST_MAP5 = 0x8e0,
            mmMC_XPB_RTR_DEST_MAP6 = 0x8e1,
            mmMC_XPB_RTR_DEST_MAP7 = 0x8e2,
            mmMC_XPB_RTR_DEST_MAP8 = 0x8e3,
            mmMC_XPB_RTR_DEST_MAP9 = 0x8e4,
            mmMC_XPB_XDMA_RTR_DEST_MAP0 = 0x8e5,
            mmMC_XPB_XDMA_RTR_DEST_MAP1 = 0x8e6,
            mmMC_XPB_XDMA_RTR_DEST_MAP2 = 0x8e7,
            mmMC_XPB_XDMA_RTR_DEST_MAP3 = 0x8e8,
            mmMC_XPB_CLG_CFG0 = 0x8e9,
            mmMC_XPB_CLG_CFG1 = 0x8ea,
            mmMC_XPB_CLG_CFG2 = 0x8eb,
            mmMC_XPB_CLG_CFG3 = 0x8ec,
            mmMC_XPB_CLG_CFG4 = 0x8ed,
            mmMC_XPB_CLG_CFG5 = 0x8ee,
            mmMC_XPB_CLG_CFG6 = 0x8ef,
            ixMC_IO_DEBUG_UP_143 = 0x8f,
            mmMC_XPB_CLG_CFG7 = 0x8f0,
            mmMC_XPB_CLG_CFG8 = 0x8f1,
            mmMC_XPB_CLG_CFG9 = 0x8f2,
            mmMC_XPB_CLG_CFG10 = 0x8f3,
            mmMC_XPB_CLG_CFG11 = 0x8f4,
            mmMC_XPB_CLG_CFG12 = 0x8f5,
            mmMC_XPB_CLG_CFG13 = 0x8f6,
            mmMC_XPB_CLG_CFG14 = 0x8f7,
            mmMC_XPB_CLG_CFG15 = 0x8f8,
            mmMC_XPB_CLG_CFG16 = 0x8f9,
            mmMC_XPB_CLG_CFG17 = 0x8fa,
            mmMC_XPB_CLG_CFG18 = 0x8fb,
            mmMC_XPB_CLG_CFG19 = 0x8fc,
            mmMC_XPB_CLG_EXTRA = 0x8fd,
            mmMC_XPB_LB_ADDR = 0x8fe,
            mmMC_XPB_UNC_THRESH_HST = 0x8ff,
            ixMC_TSM_DEBUG_BCNT6 = 0x9,
            ixMC_IO_DEBUG_UP_9 = 0x9,
            ixMC_IO_DEBUG_UP_144 = 0x90,
            mmMC_XPB_UNC_THRESH_SID = 0x900,
            mmMC_XPB_WCB_STS = 0x901,
            mmMC_XPB_WCB_CFG = 0x902,
            mmMC_XPB_P2P_BAR_CFG = 0x903,
            mmMC_XPB_P2P_BAR0 = 0x904,
            mmMC_XPB_P2P_BAR1 = 0x905,
            mmMC_XPB_P2P_BAR2 = 0x906,
            mmMC_XPB_P2P_BAR3 = 0x907,
            mmMC_XPB_P2P_BAR4 = 0x908,
            mmMC_XPB_P2P_BAR5 = 0x909,
            mmMC_XPB_P2P_BAR6 = 0x90a,
            mmMC_XPB_P2P_BAR7 = 0x90b,
            mmMC_XPB_P2P_BAR_SETUP = 0x90c,
            mmMC_XPB_P2P_BAR_DEBUG = 0x90d,
            mmMC_XPB_P2P_BAR_DELTA_ABOVE = 0x90e,
            mmMC_XPB_P2P_BAR_DELTA_BELOW = 0x90f,
            ixMC_IO_DEBUG_UP_145 = 0x91,
            mmMC_XPB_PEER_SYS_BAR0 = 0x910,
            mmMC_XPB_PEER_SYS_BAR1 = 0x911,
            mmMC_XPB_PEER_SYS_BAR2 = 0x912,
            mmMC_XPB_PEER_SYS_BAR3 = 0x913,
            mmMC_XPB_PEER_SYS_BAR4 = 0x914,
            mmMC_XPB_PEER_SYS_BAR5 = 0x915,
            mmMC_XPB_PEER_SYS_BAR6 = 0x916,
            mmMC_XPB_PEER_SYS_BAR7 = 0x917,
            mmMC_XPB_PEER_SYS_BAR8 = 0x918,
            mmMC_XPB_PEER_SYS_BAR9 = 0x919,
            mmMC_XPB_XDMA_PEER_SYS_BAR0 = 0x91a,
            mmMC_XPB_XDMA_PEER_SYS_BAR1 = 0x91b,
            mmMC_XPB_XDMA_PEER_SYS_BAR2 = 0x91c,
            mmMC_XPB_XDMA_PEER_SYS_BAR3 = 0x91d,
            mmMC_XPB_CLK_GAT = 0x91e,
            mmMC_XPB_INTF_CFG = 0x91f,
            ixMC_IO_DEBUG_UP_146 = 0x92,
            mmMC_XPB_INTF_STS = 0x920,
            mmMC_XPB_PIPE_STS = 0x921,
            mmMC_XPB_SUB_CTRL = 0x922,
            mmMC_XPB_MAP_INVERT_FLUSH_NUM_LSB = 0x923,
            mmMC_XPB_PERF_KNOBS = 0x924,
            mmMC_XPB_STICKY = 0x925,
            mmMC_XPB_STICKY_W1C = 0x926,
            mmMC_XPB_MISC_CFG = 0x927,
            mmMC_XPB_CLG_CFG20 = 0x928,
            mmMC_XPB_CLG_CFG21 = 0x929,
            mmMC_XPB_CLG_CFG22 = 0x92a,
            mmMC_XPB_CLG_CFG23 = 0x92b,
            mmMC_XPB_CLG_CFG24 = 0x92c,
            mmMC_XPB_CLG_CFG25 = 0x92d,
            mmMC_XPB_CLG_CFG26 = 0x92e,
            mmMC_XPB_CLG_CFG27 = 0x92f,
            ixMC_IO_DEBUG_UP_147 = 0x93,
            mmMC_XPB_CLG_CFG28 = 0x930,
            mmMC_XPB_CLG_CFG29 = 0x931,
            mmMC_XPB_CLG_CFG30 = 0x932,
            mmMC_XPB_CLG_CFG31 = 0x933,
            mmMC_XPB_INTF_CFG2 = 0x934,
            mmMC_XPB_CLG_EXTRA_RD = 0x935,
            mmMC_XPB_CLG_CFG32 = 0x936,
            mmMC_XPB_CLG_CFG33 = 0x937,
            mmMC_XPB_CLG_CFG34 = 0x938,
            mmMC_XPB_CLG_CFG35 = 0x939,
            mmMC_XPB_CLG_CFG36 = 0x93a,
            ixMC_IO_DEBUG_UP_148 = 0x94,
            mmMC_RPB_CONF = 0x94d,
            mmMC_RPB_IF_CONF = 0x94e,
            mmMC_RPB_DBG1 = 0x94f,
            ixMC_IO_DEBUG_UP_149 = 0x95,
            mmMC_RPB_EFF_CNTL = 0x950,
            mmMC_RPB_ARB_CNTL = 0x951,
            mmMC_RPB_BIF_CNTL = 0x952,
            mmMC_RPB_WR_SWITCH_CNTL = 0x953,
            mmMC_RPB_WR_COMBINE_CNTL = 0x954,
            mmMC_RPB_RD_SWITCH_CNTL = 0x955,
            mmMC_RPB_CID_QUEUE_WR = 0x956,
            mmMC_RPB_CID_QUEUE_RD = 0x957,
            mmMC_RPB_PERF_COUNTER_CNTL = 0x958,
            mmMC_RPB_PERF_COUNTER_STATUS = 0x959,
            mmMC_RPB_CID_QUEUE_EX = 0x95a,
            mmMC_RPB_CID_QUEUE_EX_DATA = 0x95b,
            mmMC_RPB_TCI_CNTL = 0x95c,
            mmMC_RPB_TCI_CNTL2 = 0x95d,
            ixMC_IO_DEBUG_UP_150 = 0x96,
            mmMC_CITF_XTRA_ENABLE = 0x96d,
            mmCC_MC_MAX_CHANNEL = 0x96e,
            mmMC_CG_CONFIG = 0x96f,
            ixMC_IO_DEBUG_UP_151 = 0x97,
            mmMC_CITF_CNTL = 0x970,
            mmMC_CITF_CREDITS_VM = 0x971,
            mmMC_CITF_CREDITS_ARB_RD = 0x972,
            mmMC_CITF_CREDITS_ARB_WR = 0x973,
            mmMC_CITF_DAGB_CNTL = 0x974,
            mmMC_CITF_INT_CREDITS = 0x975,
            mmMC_CITF_RET_MODE = 0x976,
            mmMC_CITF_DAGB_DLY = 0x977,
            mmMC_RD_GRP_EXT = 0x978,
            mmMC_WR_GRP_EXT = 0x979,
            mmMC_CITF_REMREQ = 0x97a,
            mmMC_WR_TC0 = 0x97b,
            mmMC_WR_TC1 = 0x97c,
            mmMC_CITF_INT_CREDITS_WR = 0x97d,
            mmMC_CITF_CREDITS_ARB_RD2 = 0x97e,
            mmMC_CITF_WTM_RD_CNTL = 0x97f,
            ixMC_IO_DEBUG_UP_152 = 0x98,
            mmMC_CITF_WTM_WR_CNTL = 0x980,
            mmMC_RD_CB = 0x981,
            mmMC_RD_DB = 0x982,
            mmMC_RD_TC0 = 0x983,
            mmMC_RD_TC1 = 0x984,
            mmMC_RD_HUB = 0x985,
            mmMC_WR_CB = 0x986,
            mmMC_WR_DB = 0x987,
            mmMC_WR_HUB = 0x988,
            mmMC_CITF_CREDITS_XBAR = 0x989,
            mmMC_RD_GRP_LCL = 0x98a,
            mmMC_WR_GRP_LCL = 0x98b,
            mmMC_CITF_PERF_MON_CNTL2 = 0x98e,
            ixMC_IO_DEBUG_UP_153 = 0x99,
            mmMC_CITF_PERF_MON_RSLT2 = 0x991,
            mmMC_CITF_MISC_RD_CG = 0x992,
            mmMC_CITF_MISC_WR_CG = 0x993,
            mmMC_CITF_MISC_VM_CG = 0x994,
            mmMC_VM_MD_L1_TLB0_DEBUG = 0x998,
            mmMC_VM_MD_L1_TLB1_DEBUG = 0x999,
            mmMC_VM_MD_L1_TLB2_DEBUG = 0x99a,
            mmMC_VM_MD_L1_TLB0_STATUS = 0x99b,
            mmMC_VM_MD_L1_TLB1_STATUS = 0x99c,
            mmMC_VM_MD_L1_TLB2_STATUS = 0x99d,
            ixMC_IO_DEBUG_UP_154 = 0x9a,
            mmMC_VM_MD_L2ARBITER_L2_CREDITS = 0x9a4,
            mmMC_VM_MD_L1_TLB3_DEBUG = 0x9a7,
            mmMC_VM_MD_L1_TLB3_STATUS = 0x9a8,
            ixMC_IO_DEBUG_UP_155 = 0x9b,
            mmMC_ARB_ATOMIC = 0x9be,
            mmMC_ARB_AGE_CNTL = 0x9bf,
            ixMC_IO_DEBUG_UP_156 = 0x9c,
            mmMC_ARB_RET_CREDITS2 = 0x9c0,
            mmMC_ARB_FED_CNTL = 0x9c1,
            mmMC_ARB_GECC2_STATUS = 0x9c2,
            mmMC_ARB_GECC2_MISC = 0x9c3,
            mmMC_ARB_GECC2_DEBUG = 0x9c4,
            mmMC_ARB_GECC2_DEBUG2 = 0x9c5,
            mmMC_ARB_PERF_CID = 0x9c6,
            mmMC_ARB_SNOOP = 0x9c7,
            mmMC_ARB_GRUB = 0x9c8,
            mmMC_ARB_GECC2 = 0x9c9,
            mmMC_ARB_GECC2_CLI = 0x9ca,
            mmMC_ARB_ADDR_SWIZ0 = 0x9cb,
            mmMC_ARB_ADDR_SWIZ1 = 0x9cc,
            mmMC_ARB_MISC3 = 0x9cd,
            mmMC_ARB_GRUB_PROMOTE = 0x9ce,
            mmMC_ARB_RTT_DATA = 0x9cf,
            ixMC_IO_DEBUG_UP_157 = 0x9d,
            mmMC_ARB_RTT_CNTL0 = 0x9d0,
            mmMC_ARB_RTT_CNTL1 = 0x9d1,
            mmMC_ARB_RTT_CNTL2 = 0x9d2,
            mmMC_ARB_RTT_DEBUG = 0x9d3,
            mmMC_ARB_CAC_CNTL = 0x9d4,
            mmMC_ARB_MISC2 = 0x9d5,
            mmMC_ARB_MISC = 0x9d6,
            mmMC_ARB_BANKMAP = 0x9d7,
            mmMC_ARB_RAMCFG = 0x9d8,
            mmMC_ARB_POP = 0x9d9,
            mmMC_ARB_MINCLKS = 0x9da,
            mmMC_ARB_SQM_CNTL = 0x9db,
            mmMC_ARB_ADDR_HASH = 0x9dc,
            mmMC_ARB_DRAM_TIMING = 0x9dd,
            mmMC_ARB_DRAM_TIMING2 = 0x9de,
            mmMC_ARB_WTM_CNTL_RD = 0x9df,
            ixMC_IO_DEBUG_UP_158 = 0x9e,
            mmMC_ARB_WTM_CNTL_WR = 0x9e0,
            mmMC_ARB_WTM_GRPWT_RD = 0x9e1,
            mmMC_ARB_WTM_GRPWT_WR = 0x9e2,
            mmMC_ARB_TM_CNTL_RD = 0x9e3,
            mmMC_ARB_TM_CNTL_WR = 0x9e4,
            mmMC_ARB_LAZY0_RD = 0x9e5,
            mmMC_ARB_LAZY0_WR = 0x9e6,
            mmMC_ARB_LAZY1_RD = 0x9e7,
            mmMC_ARB_LAZY1_WR = 0x9e8,
            mmMC_ARB_AGE_RD = 0x9e9,
            mmMC_ARB_AGE_WR = 0x9ea,
            mmMC_ARB_RFSH_CNTL = 0x9eb,
            mmMC_ARB_RFSH_RATE = 0x9ec,
            mmMC_ARB_PM_CNTL = 0x9ed,
            mmMC_ARB_GDEC_RD_CNTL = 0x9ee,
            mmMC_ARB_GDEC_WR_CNTL = 0x9ef,
            ixMC_IO_DEBUG_UP_159 = 0x9f,
            mmMC_ARB_LM_RD = 0x9f0,
            mmMC_ARB_LM_WR = 0x9f1,
            mmMC_ARB_REMREQ = 0x9f2,
            mmMC_ARB_REPLAY = 0x9f3,
            mmMC_ARB_RET_CREDITS_RD = 0x9f4,
            mmMC_ARB_RET_CREDITS_WR = 0x9f5,
            mmMC_ARB_MAX_LAT_CID = 0x9f6,
            mmMC_ARB_MAX_LAT_RSLT0 = 0x9f7,
            mmMC_ARB_MAX_LAT_RSLT1 = 0x9f8,
            mmMC_ARB_GRUB_REALTIME_RD = 0x9f9,
            mmMC_ARB_CG = 0x9fa,
            mmMC_ARB_GRUB_REALTIME_WR = 0x9fb,
            mmMC_ARB_DRAM_TIMING_1 = 0x9fc,
            mmMC_ARB_BUSY_STATUS = 0x9fd,
            mmMC_ARB_DRAM_TIMING2_1 = 0x9ff,
            ixMC_TSM_DEBUG_BCNT7 = 0xa,
            ixMC_IO_DEBUG_UP_10 = 0xa,
            ixMC_IO_DEBUG_DQB0L_MISC_D0 = 0xa0,
            mmMC_ARB_GRUB2 = 0xa01,
            mmMC_ARB_BURST_TIME = 0xa02,
            mmMC_BIST_CNTL = 0xa05,
            mmMC_BIST_AUTO_CNTL = 0xa06,
            mmMC_BIST_DIR_CNTL = 0xa07,
            mmMC_BIST_SADDR = 0xa08,
            mmMC_BIST_EADDR = 0xa09,
            mmMC_BIST_DATA_WORD0 = 0xa0a,
            mmMC_BIST_DATA_WORD1 = 0xa0b,
            mmMC_BIST_DATA_WORD2 = 0xa0c,
            mmMC_BIST_DATA_WORD3 = 0xa0d,
            mmMC_BIST_DATA_WORD4 = 0xa0e,
            mmMC_BIST_DATA_WORD5 = 0xa0f,
            ixMC_IO_DEBUG_DQB0H_MISC_D0 = 0xa1,
            mmMC_BIST_DATA_WORD6 = 0xa10,
            mmMC_BIST_DATA_WORD7 = 0xa11,
            mmMC_BIST_DATA_MASK = 0xa12,
            mmMC_BIST_MISMATCH_ADDR = 0xa13,
            mmMC_BIST_RDATA_WORD0 = 0xa14,
            mmMC_BIST_RDATA_WORD1 = 0xa15,
            mmMC_BIST_RDATA_WORD2 = 0xa16,
            mmMC_BIST_RDATA_WORD3 = 0xa17,
            mmMC_BIST_RDATA_WORD4 = 0xa18,
            mmMC_BIST_RDATA_WORD5 = 0xa19,
            mmMC_BIST_RDATA_WORD6 = 0xa1a,
            mmMC_BIST_RDATA_WORD7 = 0xa1b,
            mmMC_BIST_RDATA_MASK = 0xa1c,
            mmMC_BIST_RDATA_EDC = 0xa1d,
            mmMC_SEQ_RESERVE_0_S = 0xa1e,
            mmMC_SEQ_RESERVE_1_S = 0xa1f,
            ixMC_IO_DEBUG_DQB1L_MISC_D0 = 0xa2,
            mmMC_SEQ_STATUS_S = 0xa20,
            mmMC_CG_DATAPORT = 0xa21,
            mmMC_SEQ_MPLL_OVERRIDE = 0xa22,
            mmMC_SEQ_CNTL = 0xa25,
            mmMC_SEQ_DRAM = 0xa26,
            mmMC_SEQ_DRAM_2 = 0xa27,
            mmMC_SEQ_RAS_TIMING = 0xa28,
            mmMC_SEQ_CAS_TIMING = 0xa29,
            mmMC_SEQ_MISC_TIMING = 0xa2a,
            mmMC_SEQ_MISC_TIMING2 = 0xa2b,
            mmMC_SEQ_PMG_TIMING = 0xa2c,
            mmMC_SEQ_RD_CTL_D0 = 0xa2d,
            mmMC_SEQ_RD_CTL_D1 = 0xa2e,
            mmMC_SEQ_WR_CTL_D0 = 0xa2f,
            ixMC_IO_DEBUG_DQB1H_MISC_D0 = 0xa3,
            mmMC_SEQ_WR_CTL_D1 = 0xa30,
            mmMC_SEQ_CMD = 0xa31,
            mmMC_SEQ_SUP_CNTL = 0xa32,
            mmMC_SEQ_SUP_PGM = 0xa33,
            mmMC_PMG_AUTO_CMD = 0xa34,
            mmMC_PMG_AUTO_CFG = 0xa35,
            mmMC_IMP_CNTL = 0xa36,
            mmMC_IMP_DEBUG = 0xa37,
            mmMC_IMP_STATUS = 0xa38,
            mmMC_SEQ_WCDR_CTRL = 0xa39,
            mmMC_SEQ_TRAIN_WAKEUP_CNTL = 0xa3a,
            mmMC_SEQ_TRAIN_EDC_THRESHOLD = 0xa3b,
            mmMC_SEQ_TRAIN_WAKEUP_EDGE = 0xa3c,
            mmMC_SEQ_TRAIN_WAKEUP_MASK = 0xa3d,
            mmMC_SEQ_TRAIN_CAPTURE = 0xa3e,
            mmMC_SEQ_TRAIN_WAKEUP_CLEAR = 0xa3f,
            ixMC_IO_DEBUG_DQB2L_MISC_D0 = 0xa4,
            mmMC_SEQ_TRAIN_TIMING = 0xa40,
            mmMC_TRAIN_EDCCDR_R_D0 = 0xa41,
            mmMC_TRAIN_EDCCDR_R_D1 = 0xa42,
            mmMC_TRAIN_PRBSERR_0_D0 = 0xa43,
            mmMC_TRAIN_PRBSERR_1_D0 = 0xa44,
            mmMC_TRAIN_EDC_STATUS_D0 = 0xa45,
            mmMC_TRAIN_PRBSERR_0_D1 = 0xa46,
            mmMC_TRAIN_PRBSERR_1_D1 = 0xa47,
            mmMC_TRAIN_EDC_STATUS_D1 = 0xa48,
            mmMC_IO_TXCNTL_DPHY0_D0 = 0xa49,
            mmMC_IO_TXCNTL_DPHY1_D0 = 0xa4a,
            mmMC_IO_TXCNTL_APHY_D0 = 0xa4b,
            mmMC_IO_RXCNTL_DPHY0_D0 = 0xa4c,
            mmMC_IO_RXCNTL_DPHY1_D0 = 0xa4d,
            mmMC_IO_DPHY_STR_CNTL_D0 = 0xa4e,
            mmMC_IO_TXCNTL_DPHY0_D1 = 0xa4f,
            ixMC_IO_DEBUG_DQB2H_MISC_D0 = 0xa5,
            mmMC_IO_TXCNTL_DPHY1_D1 = 0xa50,
            mmMC_IO_TXCNTL_APHY_D1 = 0xa51,
            mmMC_IO_RXCNTL_DPHY0_D1 = 0xa52,
            mmMC_IO_RXCNTL_DPHY1_D1 = 0xa53,
            mmMC_IO_DPHY_STR_CNTL_D1 = 0xa54,
            mmMC_IO_CDRCNTL_D0 = 0xa55,
            mmMC_IO_CDRCNTL_D1 = 0xa56,
            mmMC_SEQ_FIFO_CTL = 0xa57,
            mmMC_SEQ_TXFRAMING_BYTE0_D0 = 0xa58,
            mmMC_SEQ_TXFRAMING_BYTE1_D0 = 0xa59,
            mmMC_SEQ_TXFRAMING_BYTE2_D0 = 0xa5a,
            mmMC_SEQ_TXFRAMING_BYTE3_D0 = 0xa5b,
            mmMC_SEQ_TXFRAMING_DBI_D0 = 0xa5c,
            mmMC_SEQ_TXFRAMING_EDC_D0 = 0xa5d,
            mmMC_SEQ_TXFRAMING_FCK_D0 = 0xa5e,
            mmMC_SEQ_MISC8 = 0xa5f,
            ixMC_IO_DEBUG_DQB3L_MISC_D0 = 0xa6,
            mmMC_SEQ_TXFRAMING_BYTE0_D1 = 0xa60,
            mmMC_SEQ_TXFRAMING_BYTE1_D1 = 0xa61,
            mmMC_SEQ_TXFRAMING_BYTE2_D1 = 0xa62,
            mmMC_SEQ_TXFRAMING_BYTE3_D1 = 0xa63,
            mmMC_SEQ_TXFRAMING_DBI_D1 = 0xa64,
            mmMC_SEQ_TXFRAMING_EDC_D1 = 0xa65,
            mmMC_SEQ_TXFRAMING_FCK_D1 = 0xa66,
            mmMC_SEQ_RXFRAMING_BYTE0_D0 = 0xa67,
            mmMC_SEQ_RXFRAMING_BYTE1_D0 = 0xa68,
            mmMC_SEQ_RXFRAMING_BYTE2_D0 = 0xa69,
            mmMC_SEQ_RXFRAMING_BYTE3_D0 = 0xa6a,
            mmMC_SEQ_RXFRAMING_DBI_D0 = 0xa6b,
            mmMC_SEQ_RXFRAMING_EDC_D0 = 0xa6c,
            mmMC_SEQ_RXFRAMING_BYTE0_D1 = 0xa6d,
            mmMC_SEQ_RXFRAMING_BYTE1_D1 = 0xa6e,
            mmMC_SEQ_RXFRAMING_BYTE2_D1 = 0xa6f,
            ixMC_IO_DEBUG_DQB3H_MISC_D0 = 0xa7,
            mmMC_SEQ_RXFRAMING_BYTE3_D1 = 0xa70,
            mmMC_SEQ_RXFRAMING_DBI_D1 = 0xa71,
            mmMC_SEQ_RXFRAMING_EDC_D1 = 0xa72,
            mmMC_IO_PAD_CNTL = 0xa73,
            mmMC_IO_PAD_CNTL_D0 = 0xa74,
            mmMC_IO_PAD_CNTL_D1 = 0xa75,
            mmMC_NPL_STATUS = 0xa76,
            mmMC_SEQ_PERF_CNTL = 0xa77,
            mmMC_SEQ_PERF_SEQ_CTL = 0xa78,
            mmMC_SEQ_PERF_SEQ_CNT_A_I0 = 0xa79,
            mmMC_SEQ_PERF_SEQ_CNT_A_I1 = 0xa7a,
            mmMC_SEQ_PERF_SEQ_CNT_B_I0 = 0xa7b,
            mmMC_SEQ_PERF_SEQ_CNT_B_I1 = 0xa7c,
            mmMC_SEQ_STATUS_M = 0xa7d,
            mmMC_SEQ_VENDOR_ID_I0 = 0xa7e,
            mmMC_SEQ_VENDOR_ID_I1 = 0xa7f,
            ixMC_IO_DEBUG_DBI_MISC_D0 = 0xa8,
            mmMC_SEQ_MISC0 = 0xa80,
            mmMC_SEQ_MISC1 = 0xa81,
            mmMC_SEQ_RESERVE_M = 0xa82,
            mmMC_PMG_CMD_EMRS = 0xa83,
            mmMC_PMG_CFG = 0xa84,
            mmMC_SEQ_SUP_GP2_STAT = 0xa85,
            mmMC_SEQ_SUP_GP3_STAT = 0xa86,
            mmMC_SEQ_SUP_IR_STAT = 0xa87,
            mmMC_SEQ_SUP_DEC_STAT = 0xa88,
            mmMC_SEQ_SUP_PGM_STAT = 0xa89,
            mmMC_SEQ_SUP_R_PGM = 0xa8a,
            mmMC_SEQ_MISC3 = 0xa8b,
            mmMC_SEQ_MISC4 = 0xa8c,
            mmMC_BIST_CMP_CNTL = 0xa8d,
            mmMC_BIST_CMD_CNTL = 0xa8e,
            mmMC_SEQ_SUP_GP0_STAT = 0xa8f,
            ixMC_IO_DEBUG_EDC_MISC_D0 = 0xa9,
            mmMC_SEQ_SUP_GP1_STAT = 0xa90,
            mmMC_SEQ_IO_DEBUG_INDEX = 0xa91,
            mmMC_SEQ_IO_DEBUG_DATA = 0xa92,
            mmMC_SEQ_BYTE_REMAP_D0 = 0xa93,
            mmMC_SEQ_BYTE_REMAP_D1 = 0xa94,
            mmMC_SEQ_MISC5 = 0xa95,
            mmMC_SEQ_MISC6 = 0xa96,
            mmMC_IO_APHY_STR_CNTL_D0 = 0xa97,
            mmMC_IO_APHY_STR_CNTL_D1 = 0xa98,
            mmMC_SEQ_MISC7 = 0xa99,
            mmMC_SEQ_CG = 0xa9a,
            mmMC_SEQ_RAS_TIMING_LP = 0xa9b,
            mmMC_SEQ_CAS_TIMING_LP = 0xa9c,
            mmMC_SEQ_MISC_TIMING_LP = 0xa9d,
            mmMC_SEQ_MISC_TIMING2_LP = 0xa9e,
            mmMC_SEQ_WR_CTL_D0_LP = 0xa9f,
            ixMC_IO_DEBUG_WCK_MISC_D0 = 0xaa,
            mmMC_SEQ_WR_CTL_D1_LP = 0xaa0,
            mmMC_SEQ_PMG_CMD_EMRS_LP = 0xaa1,
            mmMC_SEQ_PMG_CMD_MRS_LP = 0xaa2,
            mmMC_SEQ_BIT_REMAP_B0_D0 = 0xaa3,
            mmMC_SEQ_BIT_REMAP_B1_D0 = 0xaa4,
            mmMC_SEQ_BIT_REMAP_B2_D0 = 0xaa5,
            mmMC_SEQ_BIT_REMAP_B3_D0 = 0xaa6,
            mmMC_SEQ_BIT_REMAP_B0_D1 = 0xaa7,
            mmMC_SEQ_BIT_REMAP_B1_D1 = 0xaa8,
            mmMC_SEQ_BIT_REMAP_B2_D1 = 0xaa9,
            mmMC_SEQ_BIT_REMAP_B3_D1 = 0xaaa,
            mmMC_PMG_CMD_MRS = 0xaab,
            mmMC_SEQ_IO_RWORD0 = 0xaac,
            mmMC_SEQ_IO_RWORD1 = 0xaad,
            mmMC_SEQ_IO_RWORD2 = 0xaae,
            mmMC_SEQ_IO_RWORD3 = 0xaaf,
            ixMC_IO_DEBUG_CK_MISC_D0 = 0xab,
            mmMC_SEQ_IO_RWORD4 = 0xab0,
            mmMC_SEQ_IO_RWORD5 = 0xab1,
            mmMC_SEQ_IO_RWORD6 = 0xab2,
            mmMC_SEQ_IO_RWORD7 = 0xab3,
            mmMC_SEQ_IO_RDBI = 0xab4,
            mmMC_SEQ_IO_REDC = 0xab5,
            mmMC_BIST_CMP_CNTL_2 = 0xab6,
            mmMC_SEQ_IO_RESERVE_D0 = 0xab7,
            mmMC_SEQ_IO_RESERVE_D1 = 0xab8,
            mmMC_SEQ_PMG_PG_HWCNTL = 0xab9,
            mmMC_SEQ_PMG_PG_SWCNTL_0 = 0xaba,
            mmMC_SEQ_PMG_PG_SWCNTL_1 = 0xabb,
            mmMC_IMP_DQ_STATUS = 0xabc,
            mmMC_SEQ_TCG_CNTL = 0xabd,
            mmMC_SEQ_TSM_CTRL = 0xabe,
            mmMC_SEQ_TSM_GCNT = 0xabf,
            ixMC_IO_DEBUG_ADDRL_MISC_D0 = 0xac,
            mmMC_SEQ_TSM_OCNT = 0xac0,
            mmMC_SEQ_TSM_NCNT = 0xac1,
            mmMC_SEQ_TSM_BCNT = 0xac2,
            mmMC_SEQ_TSM_FLAG = 0xac3,
            mmMC_SEQ_TSM_UPDATE = 0xac4,
            mmMC_SEQ_TSM_EDC = 0xac5,
            mmMC_SEQ_TSM_DBI = 0xac6,
            mmMC_SEQ_RD_CTL_D0_LP = 0xac7,
            mmMC_SEQ_RD_CTL_D1_LP = 0xac8,
            mmMC_SEQ_TIMER_WR = 0xac9,
            mmMC_SEQ_TIMER_RD = 0xaca,
            mmMC_SEQ_DRAM_ERROR_INSERTION = 0xacb,
            mmMC_PHY_TIMING_D0 = 0xacc,
            mmMC_PHY_TIMING_D1 = 0xacd,
            mmMC_PHY_TIMING_2 = 0xace,
            mmMC_SEQ_TSM_DEBUG_INDEX = 0xacf,
            ixMC_IO_DEBUG_ADDRH_MISC_D0 = 0xad,
            mmMC_SEQ_TSM_DEBUG_DATA = 0xad0,
            mmMC_PMG_CMD_MRS1 = 0xad1,
            mmMC_SEQ_PMG_CMD_MRS1_LP = 0xad2,
            mmMC_SEQ_PMG_TIMING_LP = 0xad3,
            mmMC_SEQ_CNTL_2 = 0xad4,
            mmMC_SEQ_WR_CTL_2 = 0xad5,
            mmMC_SEQ_WR_CTL_2_LP = 0xad6,
            mmMC_PMG_CMD_MRS2 = 0xad7,
            mmMC_SEQ_PMG_CMD_MRS2_LP = 0xad8,
            mmMC_SEQ_PERF_SEQ_CNT_C_I0 = 0xad9,
            mmMC_SEQ_PERF_SEQ_CNT_C_I1 = 0xada,
            mmMC_SEQ_PERF_SEQ_CNT_D_I0 = 0xadb,
            mmMC_SEQ_PERF_SEQ_CNT_D_I1 = 0xadc,
            mmMC_IO_CDRCNTL1_D0 = 0xadd,
            mmMC_IO_CDRCNTL1_D1 = 0xade,
            mmMC_IO_RXCNTL1_DPHY0_D0 = 0xadf,
            ixMC_IO_DEBUG_ACMD_MISC_D0 = 0xae,
            mmMC_IO_RXCNTL1_DPHY1_D0 = 0xae0,
            mmMC_IO_RXCNTL1_DPHY0_D1 = 0xae1,
            mmMC_IO_RXCNTL1_DPHY1_D1 = 0xae2,
            mmMC_SEQ_TSM_WCDR = 0xae3,
            mmMC_IO_CDRCNTL2_D0 = 0xae4,
            mmMC_IO_CDRCNTL2_D1 = 0xae5,
            mmMC_SEQ_TSM_MISC = 0xae6,
            mmMC_SEQ_MISC9 = 0xae7,
            mmMCLK_PWRMGT_CNTL = 0xae8,
            mmDLL_CNTL = 0xae9,
            mmMPLL_SEQ_UCODE_1 = 0xaea,
            mmMPLL_SEQ_UCODE_2 = 0xaeb,
            mmMPLL_CNTL_MODE = 0xaec,
            mmMPLL_FUNC_CNTL = 0xaed,
            mmMPLL_FUNC_CNTL_1 = 0xaee,
            mmMPLL_FUNC_CNTL_2 = 0xaef,
            ixMC_IO_DEBUG_CMD_MISC_D0 = 0xaf,
            mmMPLL_AD_FUNC_CNTL = 0xaf0,
            mmMPLL_DQ_FUNC_CNTL = 0xaf1,
            mmMPLL_TIME = 0xaf2,
            mmMPLL_SS1 = 0xaf3,
            mmMPLL_SS2 = 0xaf4,
            mmMPLL_CONTROL = 0xaf5,
            mmMPLL_AD_STATUS = 0xaf6,
            mmMPLL_DQ_0_0_STATUS = 0xaf7,
            mmMPLL_DQ_0_1_STATUS = 0xaf8,
            mmMPLL_DQ_1_0_STATUS = 0xaf9,
            mmMPLL_DQ_1_1_STATUS = 0xafa,
            mmMC_TRAIN_PRBSERR_2_D0 = 0xafb,
            mmMC_TRAIN_PRBSERR_2_D1 = 0xafc,
            mmMC_SEQ_PERF_CNTL_1 = 0xafd,
            mmMC_SEQ_TRAIN_EDC_THRESHOLD2 = 0xafe,
            mmMC_SEQ_TRAIN_EDC_THRESHOLD3 = 0xaff,
            ixMC_TSM_DEBUG_BCNT8 = 0xb,
            ixMC_IO_DEBUG_UP_11 = 0xb,
            ixMC_IO_DEBUG_DQB0L_MISC_D1 = 0xb0,
            ixMC_IO_DEBUG_DQB0H_MISC_D1 = 0xb1,
            ixMC_IO_DEBUG_DQB1L_MISC_D1 = 0xb2,
            ixMC_IO_DEBUG_DQB1H_MISC_D1 = 0xb3,
            ixMC_IO_DEBUG_DQB2L_MISC_D1 = 0xb4,
            ixMC_IO_DEBUG_DQB2H_MISC_D1 = 0xb5,
            ixMC_IO_DEBUG_DQB3L_MISC_D1 = 0xb6,
            ixMC_IO_DEBUG_DQB3H_MISC_D1 = 0xb7,
            ixMC_IO_DEBUG_DBI_MISC_D1 = 0xb8,
            ixMC_IO_DEBUG_EDC_MISC_D1 = 0xb9,
            ixMC_IO_DEBUG_WCK_MISC_D1 = 0xba,
            ixMC_IO_DEBUG_CK_MISC_D1 = 0xbb,
            ixMC_IO_DEBUG_ADDRL_MISC_D1 = 0xbc,
            ixMC_IO_DEBUG_ADDRH_MISC_D1 = 0xbd,
            ixMC_IO_DEBUG_ACMD_MISC_D1 = 0xbe,
            ixMC_IO_DEBUG_CMD_MISC_D1 = 0xbf,
            ixMC_TSM_DEBUG_BCNT9 = 0xc,
            ixMC_IO_DEBUG_UP_12 = 0xc,
            ixMC_IO_DEBUG_DQB0L_CLKSEL_D0 = 0xc0,
            ixMC_IO_DEBUG_DQB0H_CLKSEL_D0 = 0xc1,
            ixMC_IO_DEBUG_DQB1L_CLKSEL_D0 = 0xc2,
            ixMC_IO_DEBUG_DQB1H_CLKSEL_D0 = 0xc3,
            ixMC_IO_DEBUG_DQB2L_CLKSEL_D0 = 0xc4,
            ixMC_IO_DEBUG_DQB2H_CLKSEL_D0 = 0xc5,
            ixMC_IO_DEBUG_DQB3L_CLKSEL_D0 = 0xc6,
            ixMC_IO_DEBUG_DQB3H_CLKSEL_D0 = 0xc7,
            ixMC_IO_DEBUG_DBI_CLKSEL_D0 = 0xc8,
            mmMC_XBAR_ADDR_DEC = 0xc80,
            mmMC_XBAR_REMOTE = 0xc81,
            mmMC_XBAR_WRREQ_CREDIT = 0xc82,
            mmMC_XBAR_RDREQ_CREDIT = 0xc83,
            mmMC_XBAR_RDREQ_PRI_CREDIT = 0xc84,
            mmMC_XBAR_WRRET_CREDIT1 = 0xc85,
            mmMC_XBAR_WRRET_CREDIT2 = 0xc86,
            mmMC_XBAR_RDRET_CREDIT1 = 0xc87,
            mmMC_XBAR_RDRET_CREDIT2 = 0xc88,
            mmMC_XBAR_RDRET_PRI_CREDIT1 = 0xc89,
            mmMC_XBAR_RDRET_PRI_CREDIT2 = 0xc8a,
            mmMC_XBAR_CHTRIREMAP = 0xc8b,
            mmMC_XBAR_TWOCHAN = 0xc8c,
            mmMC_XBAR_ARB = 0xc8d,
            mmMC_XBAR_ARB_MAX_BURST = 0xc8e,
            mmMC_XBAR_FIFO_MON_CNTL0 = 0xc8f,
            ixMC_IO_DEBUG_EDC_CLKSEL_D0 = 0xc9,
            mmMC_XBAR_FIFO_MON_CNTL1 = 0xc90,
            mmMC_XBAR_FIFO_MON_CNTL2 = 0xc91,
            mmMC_XBAR_FIFO_MON_RSLT0 = 0xc92,
            mmMC_XBAR_FIFO_MON_RSLT1 = 0xc93,
            mmMC_XBAR_FIFO_MON_RSLT2 = 0xc94,
            mmMC_XBAR_FIFO_MON_RSLT3 = 0xc95,
            mmMC_XBAR_FIFO_MON_MAX_THSH = 0xc96,
            mmMC_XBAR_SPARE0 = 0xc97,
            mmMC_XBAR_SPARE1 = 0xc98,
            ixMC_IO_DEBUG_WCK_CLKSEL_D0 = 0xca,
            ixMC_IO_DEBUG_CK_CLKSEL_D0 = 0xcb,
            ixMC_IO_DEBUG_ADDRL_CLKSEL_D0 = 0xcc,
            mmATC_VM_APERTURE0_LOW_ADDR = 0xcc0,
            mmATC_VM_APERTURE1_LOW_ADDR = 0xcc1,
            mmATC_VM_APERTURE0_HIGH_ADDR = 0xcc2,
            mmATC_VM_APERTURE1_HIGH_ADDR = 0xcc3,
            mmATC_VM_APERTURE0_CNTL = 0xcc4,
            mmATC_VM_APERTURE1_CNTL = 0xcc5,
            mmATC_VM_APERTURE0_CNTL2 = 0xcc6,
            mmATC_VM_APERTURE1_CNTL2 = 0xcc7,
            mmATC_ATS_CNTL = 0xcc9,
            mmATC_ATS_DEBUG = 0xcca,
            mmATC_ATS_FAULT_DEBUG = 0xccb,
            mmATC_ATS_STATUS = 0xccc,
            mmATC_ATS_FAULT_CNTL = 0xccd,
            mmATC_ATS_FAULT_STATUS_INFO = 0xcce,
            mmATC_ATS_FAULT_STATUS_ADDR = 0xccf,
            ixMC_IO_DEBUG_ADDRH_CLKSEL_D0 = 0xcd,
            mmATC_ATS_DEFAULT_PAGE_LOW = 0xcd0,
            mmATC_ATS_DEFAULT_PAGE_CNTL = 0xcd1,
            mmATC_ATS_FAULT_STATUS_INFO2 = 0xcd2,
            mmATC_MISC_CG = 0xcd4,
            mmATC_L2_CNTL = 0xcd5,
            mmATC_L2_CNTL2 = 0xcd6,
            mmATC_L2_DEBUG = 0xcd7,
            mmATC_L2_DEBUG2 = 0xcd8,
            mmATC_L2_CACHE_DATA0 = 0xcd9,
            mmATC_L2_CACHE_DATA1 = 0xcda,
            mmATC_L2_CACHE_DATA2 = 0xcdb,
            mmATC_L1_CNTL = 0xcdc,
            mmATC_L1_ADDRESS_OFFSET = 0xcdd,
            mmATC_L1RD_DEBUG_TLB = 0xcde,
            mmATC_L1WR_DEBUG_TLB = 0xcdf,
            ixMC_IO_DEBUG_ACMD_CLKSEL_D0 = 0xce,
            mmATC_L1RD_STATUS = 0xce0,
            mmATC_L1WR_STATUS = 0xce1,
            mmATC_L1RD_DEBUG2_TLB = 0xce2,
            mmATC_L1WR_DEBUG2_TLB = 0xce3,
            mmATC_VMID_PASID_MAPPING_UPDATE_STATUS = 0xce6,
            mmATC_VMID0_PASID_MAPPING = 0xce7,
            mmATC_VMID1_PASID_MAPPING = 0xce8,
            mmATC_VMID2_PASID_MAPPING = 0xce9,
            mmATC_VMID3_PASID_MAPPING = 0xcea,
            mmATC_VMID4_PASID_MAPPING = 0xceb,
            mmATC_VMID5_PASID_MAPPING = 0xcec,
            mmATC_VMID6_PASID_MAPPING = 0xced,
            mmATC_VMID7_PASID_MAPPING = 0xcee,
            mmATC_VMID8_PASID_MAPPING = 0xcef,
            ixMC_IO_DEBUG_CMD_CLKSEL_D0 = 0xcf,
            mmATC_VMID9_PASID_MAPPING = 0xcf0,
            mmATC_VMID10_PASID_MAPPING = 0xcf1,
            mmATC_VMID11_PASID_MAPPING = 0xcf2,
            mmATC_VMID12_PASID_MAPPING = 0xcf3,
            mmATC_VMID13_PASID_MAPPING = 0xcf4,
            mmATC_VMID14_PASID_MAPPING = 0xcf5,
            mmATC_VMID15_PASID_MAPPING = 0xcf6,
            ixMC_TSM_DEBUG_BCNT10 = 0xd,
            ixMC_IO_DEBUG_UP_13 = 0xd,
            ixMC_IO_DEBUG_DQB0L_CLKSEL_D1 = 0xd0,
            mmATC_ATS_VMID_STATUS = 0xd07,
            mmATC_ATS_SMU_STATUS = 0xd08,
            mmATC_L2_CNTL3 = 0xd09,
            mmATC_L2_STATUS = 0xd0a,
            mmATC_L2_STATUS2 = 0xd0b,
            ixMC_IO_DEBUG_DQB0H_CLKSEL_D1 = 0xd1,
            ixMC_IO_DEBUG_DQB1L_CLKSEL_D1 = 0xd2,
            ixMC_IO_DEBUG_DQB1H_CLKSEL_D1 = 0xd3,
            ixMC_IO_DEBUG_DQB2L_CLKSEL_D1 = 0xd4,
            mmGMCON_RENG_RAM_INDEX = 0xd40,
            mmGMCON_RENG_RAM_DATA = 0xd41,
            mmGMCON_RENG_EXECUTE = 0xd42,
            mmGMCON_MISC = 0xd43,
            mmGMCON_MISC2 = 0xd44,
            mmGMCON_STCTRL_REGISTER_SAVE_RANGE0 = 0xd45,
            mmGMCON_STCTRL_REGISTER_SAVE_RANGE1 = 0xd46,
            mmGMCON_STCTRL_REGISTER_SAVE_RANGE2 = 0xd47,
            mmGMCON_STCTRL_REGISTER_SAVE_EXCL_SET0 = 0xd48,
            mmGMCON_STCTRL_REGISTER_SAVE_EXCL_SET1 = 0xd49,
            mmGMCON_PERF_MON_CNTL0 = 0xd4a,
            mmGMCON_PERF_MON_CNTL1 = 0xd4b,
            mmGMCON_PERF_MON_RSLT0 = 0xd4c,
            mmGMCON_PERF_MON_RSLT1 = 0xd4d,
            mmGMCON_PGFSM_CONFIG = 0xd4e,
            mmGMCON_PGFSM_WRITE = 0xd4f,
            ixMC_IO_DEBUG_DQB2H_CLKSEL_D1 = 0xd5,
            mmGMCON_PGFSM_READ = 0xd50,
            mmGMCON_MISC3 = 0xd51,
            mmGMCON_MASK = 0xd52,
            mmGMCON_LPT_TARGET = 0xd53,
            mmGMCON_DEBUG = 0xd5f,
            ixMC_IO_DEBUG_DQB3L_CLKSEL_D1 = 0xd6,
            ixMC_IO_DEBUG_DQB3H_CLKSEL_D1 = 0xd7,
            ixMC_IO_DEBUG_DBI_CLKSEL_D1 = 0xd8,
            mmMC_SEQ_CNTL_3 = 0xd80,
            mmMC_SEQ_G5PDX_CTRL = 0xd81,
            mmMC_SEQ_G5PDX_CTRL_LP = 0xd82,
            mmMC_SEQ_G5PDX_CMD0 = 0xd83,
            mmMC_SEQ_G5PDX_CMD0_LP = 0xd84,
            mmMC_SEQ_G5PDX_CMD1 = 0xd85,
            mmMC_SEQ_G5PDX_CMD1_LP = 0xd86,
            mmMC_SEQ_SREG_READ = 0xd87,
            mmMC_SEQ_SREG_STATUS = 0xd88,
            mmMC_SEQ_PHYREG_BCAST = 0xd89,
            mmMC_SEQ_PMG_DVS_CTL = 0xd8a,
            mmMC_SEQ_PMG_DVS_CTL_LP = 0xd8b,
            mmMC_SEQ_PMG_DVS_CMD = 0xd8c,
            mmMC_SEQ_PMG_DVS_CMD_LP = 0xd8d,
            mmMC_SEQ_DLL_STBY = 0xd8e,
            mmMC_SEQ_DLL_STBY_LP = 0xd8f,
            ixMC_IO_DEBUG_EDC_CLKSEL_D1 = 0xd9,
            mmMC_DLB_MISCCTRL0 = 0xd90,
            mmMC_DLB_MISCCTRL1 = 0xd91,
            mmMC_DLB_MISCCTRL2 = 0xd92,
            mmMC_DLB_CONFIG0 = 0xd93,
            mmMC_DLB_CONFIG1 = 0xd94,
            mmMC_DLB_SETUP = 0xd95,
            mmMC_DLB_SETUPSWEEP = 0xd96,
            mmMC_DLB_SETUPFIFO = 0xd97,
            mmMC_DLB_WRITE_MASK = 0xd98,
            mmMC_DLB_STATUS = 0xd99,
            mmMC_DLB_STATUS_MISC0 = 0xd9a,
            mmMC_DLB_STATUS_MISC1 = 0xd9b,
            mmMC_DLB_STATUS_MISC2 = 0xd9c,
            mmMC_DLB_STATUS_MISC3 = 0xd9d,
            mmMC_DLB_STATUS_MISC4 = 0xd9e,
            mmMC_DLB_STATUS_MISC5 = 0xd9f,
            ixMC_IO_DEBUG_WCK_CLKSEL_D1 = 0xda,
            mmMC_DLB_STATUS_MISC6 = 0xda0,
            mmMC_DLB_STATUS_MISC7 = 0xda1,
            ixMC_IO_DEBUG_CK_CLKSEL_D1 = 0xdb,
            ixMC_IO_DEBUG_ADDRL_CLKSEL_D1 = 0xdc,
            mmMC_ARB_HARSH_EN_RD = 0xdc0,
            mmMC_ARB_HARSH_EN_WR = 0xdc1,
            mmMC_ARB_HARSH_TX_HI0_RD = 0xdc2,
            mmMC_ARB_HARSH_TX_HI0_WR = 0xdc3,
            mmMC_ARB_HARSH_TX_HI1_RD = 0xdc4,
            mmMC_ARB_HARSH_TX_HI1_WR = 0xdc5,
            mmMC_ARB_HARSH_TX_LO0_RD = 0xdc6,
            mmMC_ARB_HARSH_TX_LO0_WR = 0xdc7,
            mmMC_ARB_HARSH_TX_LO1_RD = 0xdc8,
            mmMC_ARB_HARSH_TX_LO1_WR = 0xdc9,
            mmMC_ARB_HARSH_BWPERIOD0_RD = 0xdca,
            mmMC_ARB_HARSH_BWPERIOD0_WR = 0xdcb,
            mmMC_ARB_HARSH_BWPERIOD1_RD = 0xdcc,
            mmMC_ARB_HARSH_BWPERIOD1_WR = 0xdcd,
            mmMC_ARB_HARSH_BWCNT0_RD = 0xdce,
            mmMC_ARB_HARSH_BWCNT0_WR = 0xdcf,
            ixMC_IO_DEBUG_ADDRH_CLKSEL_D1 = 0xdd,
            mmMC_ARB_HARSH_BWCNT1_RD = 0xdd0,
            mmMC_ARB_HARSH_BWCNT1_WR = 0xdd1,
            mmMC_ARB_HARSH_SAT0_RD = 0xdd2,
            mmMC_ARB_HARSH_SAT0_WR = 0xdd3,
            mmMC_ARB_HARSH_SAT1_RD = 0xdd4,
            mmMC_ARB_HARSH_SAT1_WR = 0xdd5,
            mmMC_ARB_HARSH_CTL_RD = 0xdd6,
            mmMC_ARB_HARSH_CTL_WR = 0xdd7,
            mmMC_ARB_GRUB_PRIORITY1_RD = 0xdd8,
            mmMC_ARB_GRUB_PRIORITY1_WR = 0xdd9,
            mmMC_ARB_GRUB_PRIORITY2_RD = 0xdda,
            mmMC_ARB_GRUB_PRIORITY2_WR = 0xddb,
            ixMC_IO_DEBUG_ACMD_CLKSEL_D1 = 0xde,
            mmMC_HUB_RDREQ_ISP_SPM = 0xde0,
            mmMC_HUB_RDREQ_ISP_MPM = 0xde1,
            mmMC_HUB_RDREQ_ISP_CCPU = 0xde2,
            mmMC_HUB_WDP_ISP_SPM = 0xde3,
            mmMC_HUB_WDP_ISP_MPS = 0xde4,
            mmMC_HUB_WDP_ISP_MPM = 0xde5,
            mmMC_HUB_WDP_ISP_CCPU = 0xde6,
            mmMC_HUB_RDREQ_MCDS = 0xde7,
            mmMC_HUB_RDREQ_MCDT = 0xde8,
            mmMC_HUB_RDREQ_MCDU = 0xde9,
            mmMC_HUB_RDREQ_MCDV = 0xdea,
            mmMC_HUB_WDP_MCDS = 0xdeb,
            mmMC_HUB_WDP_MCDT = 0xdec,
            mmMC_HUB_WDP_MCDU = 0xded,
            mmMC_HUB_WDP_MCDV = 0xdee,
            mmMC_HUB_WRRET_MCDS = 0xdef,
            ixMC_IO_DEBUG_CMD_CLKSEL_D1 = 0xdf,
            mmMC_HUB_WRRET_MCDT = 0xdf0,
            mmMC_HUB_WRRET_MCDU = 0xdf1,
            mmMC_HUB_WRRET_MCDV = 0xdf2,
            mmMC_HUB_WDP_CREDITS_MCDW = 0xdf3,
            mmMC_HUB_WDP_CREDITS_MCDX = 0xdf4,
            mmMC_HUB_WDP_CREDITS_MCDY = 0xdf5,
            mmMC_HUB_WDP_CREDITS_MCDZ = 0xdf6,
            mmMC_HUB_WDP_CREDITS_MCDS = 0xdf7,
            mmMC_HUB_WDP_CREDITS_MCDT = 0xdf8,
            mmMC_HUB_WDP_CREDITS_MCDU = 0xdf9,
            mmMC_HUB_WDP_CREDITS_MCDV = 0xdfa,
            mmMC_HUB_WDP_BP2 = 0xdfb,
            mmMC_HUB_RDREQ_VCE1 = 0xdfc,
            mmMC_HUB_RDREQ_VCEU1 = 0xdfd,
            mmMC_HUB_WDP_VCE1 = 0xdfe,
            mmMC_HUB_WDP_VCEU1 = 0xdff,
            ixMC_IO_DEBUG_UP_14 = 0xe,
            ixMC_IO_DEBUG_DQB0L_OFSCAL_D0 = 0xe0,
            ixMC_IO_DEBUG_DQB0H_OFSCAL_D0 = 0xe1,
            ixMC_IO_DEBUG_DQB1L_OFSCAL_D0 = 0xe2,
            ixMC_IO_DEBUG_DQB1H_OFSCAL_D0 = 0xe3,
            ixMC_IO_DEBUG_DQB2L_OFSCAL_D0 = 0xe4,
            ixMC_IO_DEBUG_DQB2H_OFSCAL_D0 = 0xe5,
            ixMC_IO_DEBUG_DQB3L_OFSCAL_D0 = 0xe6,
            ixMC_IO_DEBUG_DQB3H_OFSCAL_D0 = 0xe7,
            ixMC_IO_DEBUG_DBI_OFSCAL_D0 = 0xe8,
            ixMC_IO_DEBUG_EDC_OFSCAL_D0 = 0xe9,
            ixMC_IO_DEBUG_WCK_OFSCAL_D0 = 0xea,
            ixMC_IO_DEBUG_EDC_RX_EQ_PM_D0 = 0xeb,
            ixMC_IO_DEBUG_EDC_RX_DYN_PM_D0 = 0xec,
            ixMC_IO_DEBUG_EDC_CDR_PHSIZE_D0 = 0xed,
            ixMC_IO_DEBUG_ACMD_OFSCAL_D0 = 0xee,
            ixMC_IO_DEBUG_CMD_OFSCAL_D0 = 0xef,
            ixMC_IO_DEBUG_UP_15 = 0xf,
            ixMC_IO_DEBUG_DQB0L_OFSCAL_D1 = 0xf0,
            ixMC_IO_DEBUG_DQB0H_OFSCAL_D1 = 0xf1,
            ixMC_IO_DEBUG_DQB1L_OFSCAL_D1 = 0xf2,
            ixMC_IO_DEBUG_DQB1H_OFSCAL_D1 = 0xf3,
            ixMC_IO_DEBUG_DQB2L_OFSCAL_D1 = 0xf4,
            ixMC_IO_DEBUG_DQB2H_OFSCAL_D1 = 0xf5,
            ixMC_IO_DEBUG_DQB3L_OFSCAL_D1 = 0xf6,
            ixMC_IO_DEBUG_DQB3H_OFSCAL_D1 = 0xf7,
            ixMC_IO_DEBUG_DBI_OFSCAL_D1 = 0xf8,
            ixMC_IO_DEBUG_EDC_OFSCAL_D1 = 0xf9,
            mmMC_VM_FB_SIZE_OFFSET_VF0 = 0xf980,
            mmMC_VM_FB_SIZE_OFFSET_VF1 = 0xf981,
            mmMC_VM_FB_SIZE_OFFSET_VF2 = 0xf982,
            mmMC_VM_FB_SIZE_OFFSET_VF3 = 0xf983,
            mmMC_VM_FB_SIZE_OFFSET_VF4 = 0xf984,
            mmMC_VM_FB_SIZE_OFFSET_VF5 = 0xf985,
            mmMC_VM_FB_SIZE_OFFSET_VF6 = 0xf986,
            mmMC_VM_FB_SIZE_OFFSET_VF7 = 0xf987,
            mmMC_VM_FB_SIZE_OFFSET_VF8 = 0xf988,
            mmMC_VM_FB_SIZE_OFFSET_VF9 = 0xf989,
            mmMC_VM_FB_SIZE_OFFSET_VF10 = 0xf98a,
            mmMC_VM_FB_SIZE_OFFSET_VF11 = 0xf98b,
            mmMC_VM_FB_SIZE_OFFSET_VF12 = 0xf98c,
            mmMC_VM_FB_SIZE_OFFSET_VF13 = 0xf98d,
            mmMC_VM_FB_SIZE_OFFSET_VF14 = 0xf98e,
            mmMC_VM_FB_SIZE_OFFSET_VF15 = 0xf98f,
            mmMC_VM_NB_MMIOBASE = 0xf990,
            mmMC_VM_NB_MMIOLIMIT = 0xf991,
            mmMC_VM_NB_PCI_CTRL = 0xf992,
            mmMC_VM_NB_PCI_ARB = 0xf993,
            mmMC_VM_NB_TOP_OF_DRAM_SLOT1 = 0xf994,
            mmMC_VM_NB_LOWER_TOP_OF_DRAM2 = 0xf995,
            mmMC_VM_NB_UPPER_TOP_OF_DRAM2 = 0xf996,
            mmMC_VM_NB_TOP_OF_DRAM3 = 0xf997,
            mmMC_VM_MARC_BASE_LO_0 = 0xf998,
            mmMC_VM_MARC_BASE_HI_0 = 0xf999,
            mmMC_VM_MARC_RELOC_LO_0 = 0xf99a,
            mmMC_VM_MARC_RELOC_HI_0 = 0xf99b,
            mmMC_VM_MARC_LEN_LO_0 = 0xf99c,
            mmMC_VM_MARC_LEN_HI_0 = 0xf99d,
            mmMC_VM_MARC_BASE_LO_1 = 0xf99e,
            mmMC_VM_MARC_BASE_HI_1 = 0xf99f,
            mmMC_VM_MARC_RELOC_LO_1 = 0xf9a0,
            mmMC_VM_MARC_RELOC_HI_1 = 0xf9a1,
            mmMC_VM_MARC_LEN_LO_1 = 0xf9a2,
            mmMC_VM_MARC_LEN_HI_1 = 0xf9a3,
            mmMC_VM_MARC_BASE_LO_2 = 0xf9a4,
            mmMC_VM_MARC_BASE_HI_2 = 0xf9a5,
            mmMC_VM_MARC_RELOC_LO_2 = 0xf9a6,
            mmMC_VM_MARC_RELOC_HI_2 = 0xf9a7,
            mmMC_VM_MARC_LEN_LO_2 = 0xf9a8,
            mmMC_VM_MARC_LEN_HI_2 = 0xf9a9,
            mmMC_VM_MARC_BASE_LO_3 = 0xf9aa,
            mmMC_VM_MARC_BASE_HI_3 = 0xf9ab,
            mmMC_VM_MARC_RELOC_LO_3 = 0xf9ac,
            mmMC_VM_MARC_RELOC_HI_3 = 0xf9ad,
            mmMC_VM_MARC_LEN_LO_3 = 0xf9ae,
            mmMC_VM_MARC_LEN_HI_3 = 0xf9af,
            mmMC_VM_MARC_CNTL = 0xf9b0,
            mmMC_VM_MB_L1_TLS0_CNTL0 = 0xf9b1,
            mmMC_VM_MB_L1_TLS0_START_ADDR0 = 0xf9b2,
            mmMC_VM_MB_L1_TLS0_END_ADDR0 = 0xf9b3,
            mmMC_VM_MB_L1_TLS0_CNTL1 = 0xf9b4,
            mmMC_VM_MB_L1_TLS0_START_ADDR1 = 0xf9b5,
            mmMC_VM_MB_L1_TLS0_END_ADDR1 = 0xf9b6,
            mmMC_VM_MB_L1_TLS0_CNTL2 = 0xf9b7,
            mmMC_VM_MB_L1_TLS0_START_ADDR2 = 0xf9b8,
            mmMC_VM_MB_L1_TLS0_END_ADDR2 = 0xf9b9,
            mmMC_VM_MB_L1_TLS0_CNTL3 = 0xf9ba,
            mmMC_VM_MB_L1_TLS0_START_ADDR3 = 0xf9bb,
            mmMC_VM_MB_L1_TLS0_END_ADDR3 = 0xf9bc,
            mmMC_VM_MB_L1_TLS0_CNTL4 = 0xf9bd,
            mmMC_VM_MB_L1_TLS0_START_ADDR4 = 0xf9be,
            mmMC_VM_MB_L1_TLS0_END_ADDR4 = 0xf9bf,
            mmMC_VM_MB_L1_TLS0_CNTL5 = 0xf9c0,
            mmMC_VM_MB_L1_TLS0_START_ADDR5 = 0xf9c1,
            mmMC_VM_MB_L1_TLS0_END_ADDR5 = 0xf9c2,
            mmMC_VM_MB_L1_TLS0_CNTL6 = 0xf9c3,
            mmMC_VM_MB_L1_TLS0_START_ADDR6 = 0xf9c4,
            mmMC_VM_MB_L1_TLS0_END_ADDR6 = 0xf9c5,
            mmMC_VM_MB_L1_TLS0_CNTL7 = 0xf9c6,
            mmMC_VM_MB_L1_TLS0_START_ADDR7 = 0xf9c7,
            mmMC_VM_MB_L1_TLS0_END_ADDR7 = 0xf9c8,
            mmMC_VM_MB_L1_TLS0_CNTL8 = 0xf9c9,
            mmMC_VM_MB_L1_TLS0_START_ADDR8 = 0xf9ca,
            mmMC_VM_MB_L1_TLS0_END_ADDR8 = 0xf9cb,
            mmMC_VM_MB_L1_TLS0_PROTECTION_FAULT_STATUS = 0xf9cc,
            mmMC_VM_MB_L1_TLS0_PROTECTION_FAULT_ADDR = 0xf9cd,
            ixMC_IO_DEBUG_WCK_OFSCAL_D1 = 0xfa,
            ixMC_IO_DEBUG_EDC_RX_EQ_PM_D1 = 0xfb,
            ixMC_IO_DEBUG_EDC_RX_DYN_PM_D1 = 0xfc,
            ixMC_IO_DEBUG_EDC_CDR_PHSIZE_D1 = 0xfd,
            ixMC_IO_DEBUG_ACMD_OFSCAL_D1 = 0xfe,
            ixMC_IO_DEBUG_CMD_OFSCAL_D1 = 0xff,
        }

        public class MC_SEQ_WR_CTL_D1
        {
            public static readonly UInt32 DAT_DLY_MASK = 0xf;
            public static readonly Int32 DAT_DLY__SHIFT = 0x0;
            public static readonly UInt32 DQS_DLY_MASK = 0xf0;
            public static readonly Int32 DQS_DLY__SHIFT = 0x4;
            public static readonly UInt32 DQS_XTR_MASK = 0x100;
            public static readonly Int32 DQS_XTR__SHIFT = 0x8;
            public static readonly UInt32 DAT_2Y_DLY_MASK = 0x200;
            public static readonly Int32 DAT_2Y_DLY__SHIFT = 0x9;
            public static readonly UInt32 ADR_2Y_DLY_MASK = 0x400;
            public static readonly Int32 ADR_2Y_DLY__SHIFT = 0xa;
            public static readonly UInt32 CMD_2Y_DLY_MASK = 0x800;
            public static readonly Int32 CMD_2Y_DLY__SHIFT = 0xb;
            public static readonly UInt32 OEN_DLY_MASK = 0xf000;
            public static readonly Int32 OEN_DLY__SHIFT = 0xc;
            public static readonly UInt32 OEN_EXT_MASK = 0xf0000;
            public static readonly Int32 OEN_EXT__SHIFT = 0x10;
            public static readonly UInt32 OEN_SEL_MASK = 0x300000;
            public static readonly Int32 OEN_SEL__SHIFT = 0x14;
            public static readonly UInt32 ODT_DLY_MASK = 0xf000000;
            public static readonly Int32 ODT_DLY__SHIFT = 0x18;
            public static readonly UInt32 ODT_EXT_MASK = 0x10000000;
            public static readonly Int32 ODT_EXT__SHIFT = 0x1c;
            public static readonly UInt32 ADR_DLY_MASK = 0x20000000;
            public static readonly Int32 ADR_DLY__SHIFT = 0x1d;
            public static readonly UInt32 CMD_DLY_MASK = 0x40000000;
            public static readonly Int32 CMD_DLY__SHIFT = 0x1e;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 DAT_DLY { get { return (Data & DAT_DLY_MASK) >> DAT_DLY__SHIFT; } set { Data = (Data & ~DAT_DLY_MASK) | ((value << DAT_DLY__SHIFT) & DAT_DLY_MASK); Mask = Mask | DAT_DLY_MASK; } }
            public UInt32 DQS_DLY { get { return (Data & DQS_DLY_MASK) >> DQS_DLY__SHIFT; } set { Data = (Data & ~DQS_DLY_MASK) | ((value << DQS_DLY__SHIFT) & DQS_DLY_MASK); Mask = Mask | DQS_DLY_MASK; } }
            public UInt32 DQS_XTR { get { return (Data & DQS_XTR_MASK) >> DQS_XTR__SHIFT; } set { Data = (Data & ~DQS_XTR_MASK) | ((value << DQS_XTR__SHIFT) & DQS_XTR_MASK); Mask = Mask | DQS_XTR_MASK; } }
            public UInt32 DAT_2Y_DLY { get { return (Data & DAT_2Y_DLY_MASK) >> DAT_2Y_DLY__SHIFT; } set { Data = (Data & ~DAT_2Y_DLY_MASK) | ((value << DAT_2Y_DLY__SHIFT) & DAT_2Y_DLY_MASK); Mask = Mask | DAT_2Y_DLY_MASK; } }
            public UInt32 ADR_2Y_DLY { get { return (Data & ADR_2Y_DLY_MASK) >> ADR_2Y_DLY__SHIFT; } set { Data = (Data & ~ADR_2Y_DLY_MASK) | ((value << ADR_2Y_DLY__SHIFT) & ADR_2Y_DLY_MASK); Mask = Mask | ADR_2Y_DLY_MASK; } }
            public UInt32 CMD_2Y_DLY { get { return (Data & CMD_2Y_DLY_MASK) >> CMD_2Y_DLY__SHIFT; } set { Data = (Data & ~CMD_2Y_DLY_MASK) | ((value << CMD_2Y_DLY__SHIFT) & CMD_2Y_DLY_MASK); Mask = Mask | CMD_2Y_DLY_MASK; } }
            public UInt32 OEN_DLY { get { return (Data & OEN_DLY_MASK) >> OEN_DLY__SHIFT; } set { Data = (Data & ~OEN_DLY_MASK) | ((value << OEN_DLY__SHIFT) & OEN_DLY_MASK); Mask = Mask | OEN_DLY_MASK; } }
            public UInt32 OEN_EXT { get { return (Data & OEN_EXT_MASK) >> OEN_EXT__SHIFT; } set { Data = (Data & ~OEN_EXT_MASK) | ((value << OEN_EXT__SHIFT) & OEN_EXT_MASK); Mask = Mask | OEN_EXT_MASK; } }
            public UInt32 OEN_SEL { get { return (Data & OEN_SEL_MASK) >> OEN_SEL__SHIFT; } set { Data = (Data & ~OEN_SEL_MASK) | ((value << OEN_SEL__SHIFT) & OEN_SEL_MASK); Mask = Mask | OEN_SEL_MASK; } }
            public UInt32 ODT_DLY { get { return (Data & ODT_DLY_MASK) >> ODT_DLY__SHIFT; } set { Data = (Data & ~ODT_DLY_MASK) | ((value << ODT_DLY__SHIFT) & ODT_DLY_MASK); Mask = Mask | ODT_DLY_MASK; } }
            public UInt32 ODT_EXT { get { return (Data & ODT_EXT_MASK) >> ODT_EXT__SHIFT; } set { Data = (Data & ~ODT_EXT_MASK) | ((value << ODT_EXT__SHIFT) & ODT_EXT_MASK); Mask = Mask | ODT_EXT_MASK; } }
            public UInt32 ADR_DLY { get { return (Data & ADR_DLY_MASK) >> ADR_DLY__SHIFT; } set { Data = (Data & ~ADR_DLY_MASK) | ((value << ADR_DLY__SHIFT) & ADR_DLY_MASK); Mask = Mask | ADR_DLY_MASK; } }
            public UInt32 CMD_DLY { get { return (Data & CMD_DLY_MASK) >> CMD_DLY__SHIFT; } set { Data = (Data & ~CMD_DLY_MASK) | ((value << CMD_DLY__SHIFT) & CMD_DLY_MASK); Mask = Mask | CMD_DLY_MASK; } }

            public MC_SEQ_WR_CTL_D1(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_WR_CTL_2
        {
            public static readonly UInt32 DAT_DLY_H_D0_MASK = 0x1;
            public static readonly Int32 DAT_DLY_H_D0__SHIFT = 0x0;
            public static readonly UInt32 DQS_DLY_H_D0_MASK = 0x2;
            public static readonly Int32 DQS_DLY_H_D0__SHIFT = 0x1;
            public static readonly UInt32 OEN_DLY_H_D0_MASK = 0x4;
            public static readonly Int32 OEN_DLY_H_D0__SHIFT = 0x2;
            public static readonly UInt32 DAT_DLY_H_D1_MASK = 0x8;
            public static readonly Int32 DAT_DLY_H_D1__SHIFT = 0x3;
            public static readonly UInt32 DQS_DLY_H_D1_MASK = 0x10;
            public static readonly Int32 DQS_DLY_H_D1__SHIFT = 0x4;
            public static readonly UInt32 OEN_DLY_H_D1_MASK = 0x20;
            public static readonly Int32 OEN_DLY_H_D1__SHIFT = 0x5;
            public static readonly UInt32 WCDR_EN_MASK = 0x40;
            public static readonly Int32 WCDR_EN__SHIFT = 0x6;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 DAT_DLY_H_D0 { get { return (Data & DAT_DLY_H_D0_MASK) >> DAT_DLY_H_D0__SHIFT; } set { Data = (Data & ~DAT_DLY_H_D0_MASK) | ((value << DAT_DLY_H_D0__SHIFT) & DAT_DLY_H_D0_MASK); Mask = Mask | DAT_DLY_H_D0_MASK; } }
            public UInt32 DQS_DLY_H_D0 { get { return (Data & DQS_DLY_H_D0_MASK) >> DQS_DLY_H_D0__SHIFT; } set { Data = (Data & ~DQS_DLY_H_D0_MASK) | ((value << DQS_DLY_H_D0__SHIFT) & DQS_DLY_H_D0_MASK); Mask = Mask | DQS_DLY_H_D0_MASK; } }
            public UInt32 OEN_DLY_H_D0 { get { return (Data & OEN_DLY_H_D0_MASK) >> OEN_DLY_H_D0__SHIFT; } set { Data = (Data & ~OEN_DLY_H_D0_MASK) | ((value << OEN_DLY_H_D0__SHIFT) & OEN_DLY_H_D0_MASK); Mask = Mask | OEN_DLY_H_D0_MASK; } }
            public UInt32 DAT_DLY_H_D1 { get { return (Data & DAT_DLY_H_D1_MASK) >> DAT_DLY_H_D1__SHIFT; } set { Data = (Data & ~DAT_DLY_H_D1_MASK) | ((value << DAT_DLY_H_D1__SHIFT) & DAT_DLY_H_D1_MASK); Mask = Mask | DAT_DLY_H_D1_MASK; } }
            public UInt32 DQS_DLY_H_D1 { get { return (Data & DQS_DLY_H_D1_MASK) >> DQS_DLY_H_D1__SHIFT; } set { Data = (Data & ~DQS_DLY_H_D1_MASK) | ((value << DQS_DLY_H_D1__SHIFT) & DQS_DLY_H_D1_MASK); Mask = Mask | DQS_DLY_H_D1_MASK; } }
            public UInt32 OEN_DLY_H_D1 { get { return (Data & OEN_DLY_H_D1_MASK) >> OEN_DLY_H_D1__SHIFT; } set { Data = (Data & ~OEN_DLY_H_D1_MASK) | ((value << OEN_DLY_H_D1__SHIFT) & OEN_DLY_H_D1_MASK); Mask = Mask | OEN_DLY_H_D1_MASK; } }
            public UInt32 WCDR_EN { get { return (Data & WCDR_EN_MASK) >> WCDR_EN__SHIFT; } set { Data = (Data & ~WCDR_EN_MASK) | ((value << WCDR_EN__SHIFT) & WCDR_EN_MASK); Mask = Mask | WCDR_EN_MASK; } }

            public MC_SEQ_WR_CTL_2(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_RAS_TIMING
        {
            // based on umr
            public static readonly UInt32 TRCDW_MASK  =       0x1f; public static readonly Int32 TRCDW__SHIFT  = 0;
            public static readonly UInt32 TRCDWA_MASK =      0x3e0; public static readonly Int32 TRCDWA__SHIFT = 5;
            public static readonly UInt32 TRCDR_MASK  =     0x7c00; public static readonly Int32 TRCDR__SHIFT  = 10;
            public static readonly UInt32 TRCDRA_MASK =    0xf8000; public static readonly Int32 TRCDRA__SHIFT = 15;
            public static readonly UInt32 TRRD_MASK   =   0xf00000; public static readonly Int32 TRRD__SHIFT   = 20;
            public static readonly UInt32 TRC_MASK    = 0xff000000u; public static readonly Int32 TRC__SHIFT    = 24; // 0x7f000000

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 TRCDW { get { return (Data & TRCDW_MASK) >> TRCDW__SHIFT; } set { Data = (Data & ~TRCDW_MASK) | ((value << TRCDW__SHIFT) & TRCDW_MASK); Mask = Mask | TRCDW_MASK; } }
            public UInt32 TRCDWA { get { return (Data & TRCDWA_MASK) >> TRCDWA__SHIFT; } set { Data = (Data & ~TRCDWA_MASK) | ((value << TRCDWA__SHIFT) & TRCDWA_MASK); Mask = Mask | TRCDWA_MASK; } }
            public UInt32 TRCDR { get { return (Data & TRCDR_MASK) >> TRCDR__SHIFT; } set { Data = (Data & ~TRCDR_MASK) | ((value << TRCDR__SHIFT) & TRCDR_MASK); Mask = Mask | TRCDR_MASK; } }
            public UInt32 TRCDRA { get { return (Data & TRCDRA_MASK) >> TRCDRA__SHIFT; } set { Data = (Data & ~TRCDRA_MASK) | ((value << TRCDRA__SHIFT) & TRCDRA_MASK); Mask = Mask | TRCDRA_MASK; } }
            public UInt32 TRRD { get { return (Data & TRRD_MASK) >> TRRD__SHIFT; } set { Data = (Data & ~TRRD_MASK) | ((value << TRRD__SHIFT) & TRRD_MASK); Mask = Mask | TRRD_MASK; } }
            public UInt32 TRC { get { return (Data & TRC_MASK) >> TRC__SHIFT; } set { Data = (Data & ~TRC_MASK) | ((value << TRC__SHIFT) & TRC_MASK); Mask = Mask | TRC_MASK; } }

            public MC_SEQ_RAS_TIMING(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_PMG_TIMING
        {
            // based on umr
            public static readonly UInt32 TCKSRE_MASK         =        0xf; public static readonly Int32 TCKSRE__SHIFT         = 0;  // 0x7
            public static readonly UInt32 TCKSRX_MASK         =       0xf0; public static readonly Int32 TCKSRX__SHIFT         = 4;  // 0x70
            public static readonly UInt32 TCKE_PULSE_MASK     =      0xf00; public static readonly Int32 TCKE_PULSE__SHIFT     = 8;
            public static readonly UInt32 TCKE_MASK           =    0x3f000; public static readonly Int32 TCKE__SHIFT           = 12;
            public static readonly UInt32 SEQ_IDLE_MASK       =   0x7c0000; public static readonly Int32 SEQ_IDLE__SHIFT       = 18; // 0x1c0000
            public static readonly UInt32 TCKE_PULSE_MSB_MASK =   0x800000; public static readonly Int32 TCKE_PULSE_MSB__SHIFT = 23;
            public static readonly UInt32 SEQ_IDLE_SS_MASK    = 0xff000000u; public static readonly Int32 SEQ_IDLE_SS__SHIFT    = 24;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 TCKSRE { get { return (Data & TCKSRE_MASK) >> TCKSRE__SHIFT; } set { Data = (Data & ~TCKSRE_MASK) | ((value << TCKSRE__SHIFT) & TCKSRE_MASK); Mask = Mask | TCKSRE_MASK; } }
            public UInt32 TCKSRX { get { return (Data & TCKSRX_MASK) >> TCKSRX__SHIFT; } set { Data = (Data & ~TCKSRX_MASK) | ((value << TCKSRX__SHIFT) & TCKSRX_MASK); Mask = Mask | TCKSRX_MASK; } }
            public UInt32 TCKE { get { return (Data & TCKE_MASK) >> TCKE__SHIFT; } set { Data = (Data & ~TCKE_MASK) | ((value << TCKE__SHIFT) & TCKE_MASK); Mask = Mask | TCKE_MASK; } }
            public UInt32 SEQ_IDLE { get { return (Data & SEQ_IDLE_MASK) >> SEQ_IDLE__SHIFT; } set { Data = (Data & ~SEQ_IDLE_MASK) | ((value << SEQ_IDLE__SHIFT) & SEQ_IDLE_MASK); Mask = Mask | SEQ_IDLE_MASK; } }
            public UInt32 SEQ_IDLE_SS { get { return (Data & SEQ_IDLE_SS_MASK) >> SEQ_IDLE_SS__SHIFT; } set { Data = (Data & ~SEQ_IDLE_SS_MASK) | ((value << SEQ_IDLE_SS__SHIFT) & SEQ_IDLE_SS_MASK); Mask = Mask | SEQ_IDLE_SS_MASK; } }
            public UInt32 TCKE_PULSE { get { return (Data & TCKE_PULSE_MASK) >> TCKE_PULSE__SHIFT; } set { Data = (Data & ~TCKE_PULSE_MASK) | ((value << TCKE_PULSE__SHIFT) & TCKE_PULSE_MASK); Mask = Mask | TCKE_PULSE_MASK; } }
            public UInt32 TCKE_PULSE_MSB { get { return (Data & TCKE_PULSE_MSB_MASK) >> TCKE_PULSE_MSB__SHIFT; } set { Data = (Data & ~TCKE_PULSE_MSB_MASK) | ((value << TCKE_PULSE_MSB__SHIFT) & TCKE_PULSE_MSB_MASK); Mask = Mask | TCKE_PULSE_MSB_MASK; } }

            public MC_SEQ_PMG_TIMING(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_CAS_TIMING
        {
            public static readonly UInt32 TNOPW_MASK =        0x3; public static readonly Int32 TNOPW__SHIFT = 0x0;
            public static readonly UInt32 TNOPR_MASK =        0xc; public static readonly Int32 TNOPR__SHIFT = 0x2;
            public static readonly UInt32 TR2W_MASK  =      0x1f0; public static readonly Int32 TR2W__SHIFT = 0x4;
            public static readonly UInt32 TCCDL_MASK =      0xe00; public static readonly Int32 TCCDL__SHIFT = 0x9;
            public static readonly UInt32 TR2R_MASK  =     0xf000; public static readonly Int32 TR2R__SHIFT = 0xc;
            public static readonly UInt32 TW2R_MASK  =   0xff0000; public static readonly Int32 TW2R__SHIFT = 0x10; // 0x1f0000
            public static readonly UInt32 TCL_MASK   = 0x1f000000u; public static readonly Int32 TCL__SHIFT = 0x18; // 0x1f000000

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 TNOPW { get { return (Data & TNOPW_MASK) >> TNOPW__SHIFT; } set { Data = (Data & ~TNOPW_MASK) | ((value << TNOPW__SHIFT) & TNOPW_MASK); Mask = Mask | TNOPW_MASK; } }
            public UInt32 TNOPR { get { return (Data & TNOPR_MASK) >> TNOPR__SHIFT; } set { Data = (Data & ~TNOPR_MASK) | ((value << TNOPR__SHIFT) & TNOPR_MASK); Mask = Mask | TNOPR_MASK; } }
            public UInt32 TR2W { get { return (Data & TR2W_MASK) >> TR2W__SHIFT; } set { Data = (Data & ~TR2W_MASK) | ((value << TR2W__SHIFT) & TR2W_MASK); Mask = Mask | TR2W_MASK; } }
            public UInt32 TCCDL { get { return (Data & TCCDL_MASK) >> TCCDL__SHIFT; } set { Data = (Data & ~TCCDL_MASK) | ((value << TCCDL__SHIFT) & TCCDL_MASK); Mask = Mask | TCCDL_MASK; } }
            public UInt32 TR2R { get { return (Data & TR2R_MASK) >> TR2R__SHIFT; } set { Data = (Data & ~TR2R_MASK) | ((value << TR2R__SHIFT) & TR2R_MASK); Mask = Mask | TR2R_MASK; } }
            public UInt32 TW2R { get { return (Data & TW2R_MASK) >> TW2R__SHIFT; } set { Data = (Data & ~TW2R_MASK) | ((value << TW2R__SHIFT) & TW2R_MASK); Mask = Mask | TW2R_MASK; } }
            public UInt32 TCL { get { return (Data & TCL_MASK) >> TCL__SHIFT; } set { Data = (Data & ~TCL_MASK) | ((value << TCL__SHIFT) & TCL_MASK); Mask = Mask | TCL_MASK; } }

            public MC_SEQ_CAS_TIMING(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_MISC_TIMING2
        {
            // based on umr
            public static readonly UInt32 PA2RDATA_MASK =        0xf; public static readonly Int32 PA2RDATA__SHIFT = 0; // 0x7
            public static readonly UInt32 PA2WDATA_MASK =       0xf0; public static readonly Int32 PA2WDATA__SHIFT = 4; // 0x70
            public static readonly UInt32 FAW_MASK      =     0x1f00; public static readonly Int32 FAW__SHIFT      = 8;
            public static readonly UInt32 TREDC_MASK    =     0xe000; public static readonly Int32 TREDC__SHIFT    = 13;
            public static readonly UInt32 TWEDC_MASK    =   0x1f0000; public static readonly Int32 TWEDC__SHIFT    = 16;
            public static readonly UInt32 T32AW_MASK    =  0xfe00000; public static readonly Int32 T32AW__SHIFT    = 21; // 0x1e00000
            public static readonly UInt32 TWDATATR_MASK = 0xf0000000u; public static readonly Int32 TWDATATR__SHIFT = 28;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 PA2WDATA { get { return (Data & PA2WDATA_MASK) >> PA2WDATA__SHIFT; } set { Data = (Data & ~PA2WDATA_MASK) | ((value << PA2WDATA__SHIFT) & PA2WDATA_MASK); Mask = Mask | PA2WDATA_MASK; } }
            public UInt32 PA2RDATA { get { return (Data & PA2RDATA_MASK) >> PA2RDATA__SHIFT; } set { Data = (Data & ~PA2RDATA_MASK) | ((value << PA2RDATA__SHIFT) & PA2RDATA_MASK); Mask = Mask | PA2RDATA_MASK; } }
            public UInt32 FAW { get { return (Data & FAW_MASK) >> FAW__SHIFT; } set { Data = (Data & ~FAW_MASK) | ((value << FAW__SHIFT) & FAW_MASK); Mask = Mask | FAW_MASK; } }
            public UInt32 TREDC { get { return (Data & TREDC_MASK) >> TREDC__SHIFT; } set { Data = (Data & ~TREDC_MASK) | ((value << TREDC__SHIFT) & TREDC_MASK); Mask = Mask | TREDC_MASK; } }
            public UInt32 TWEDC { get { return (Data & TWEDC_MASK) >> TWEDC__SHIFT; } set { Data = (Data & ~TWEDC_MASK) | ((value << TWEDC__SHIFT) & TWEDC_MASK); Mask = Mask | TWEDC_MASK; } }
            public UInt32 T32AW { get { return (Data & T32AW_MASK) >> T32AW__SHIFT; } set { Data = (Data & ~T32AW_MASK) | ((value << T32AW__SHIFT) & T32AW_MASK); Mask = Mask | T32AW_MASK; } }
            public UInt32 TWDATATR { get { return (Data & TWDATATR_MASK) >> TWDATATR__SHIFT; } set { Data = (Data & ~TWDATATR_MASK) | ((value << TWDATATR__SHIFT) & TWDATATR_MASK); Mask = Mask | TWDATATR_MASK; } }

            public MC_SEQ_MISC_TIMING2(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_MISC_TIMING
        {
            // based on umr
            public static readonly UInt32 TRP_WRA_MASK =       0xff; public static readonly Int32 TRP_WRA__SHIFT = 0x0; // 0x3f
            public static readonly UInt32 TRP_RDA_MASK =     0x7f00; public static readonly Int32 TRP_RDA__SHIFT = 0x8; // 0x3f00
            public static readonly UInt32 TRP_MASK     =    0xf8000; public static readonly Int32 TRP__SHIFT = 0xf;
            public static readonly UInt32 TRFC_MASK    = 0xfff00000u; public static readonly Int32 TRFC__SHIFT = 0x14; // 0x1ff00000

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 TRP_WRA { get { return (Data & TRP_WRA_MASK) >> TRP_WRA__SHIFT; } set { Data = (Data & ~TRP_WRA_MASK) | ((value << TRP_WRA__SHIFT) & TRP_WRA_MASK); Mask = Mask | TRP_WRA_MASK; } }
            public UInt32 TRP_RDA { get { return (Data & TRP_RDA_MASK) >> TRP_RDA__SHIFT; } set { Data = (Data & ~TRP_RDA_MASK) | ((value << TRP_RDA__SHIFT) & TRP_RDA_MASK); Mask = Mask | TRP_RDA_MASK; } }
            public UInt32 TRP { get { return (Data & TRP_MASK) >> TRP__SHIFT; } set { Data = (Data & ~TRP_MASK) | ((value << TRP__SHIFT) & TRP_MASK); Mask = Mask | TRP_MASK; } }
            public UInt32 TRFC { get { return (Data & TRFC_MASK) >> TRFC__SHIFT; } set { Data = (Data & ~TRFC_MASK) | ((value << TRFC__SHIFT) & TRFC_MASK); Mask = Mask | TRFC_MASK; } }

            public MC_SEQ_MISC_TIMING(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }
        
        public class MC_ARB_DRAM_TIMING
        {
            public static readonly UInt32 ACTRD_MASK = 0xff;
            public static readonly Int32 ACTRD__SHIFT = 0x0;
            public static readonly UInt32 ACTWR_MASK = 0xff00;
            public static readonly Int32 ACTWR__SHIFT = 0x8;
            public static readonly UInt32 RASMACTRD_MASK = 0xff0000;
            public static readonly Int32 RASMACTRD__SHIFT = 0x10;
            public static readonly UInt32 RASMACTWR_MASK = 0xff000000u;
            public static readonly Int32 RASMACTWR__SHIFT = 0x18;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 ACTRD { get { return (Data & ACTRD_MASK) >> ACTRD__SHIFT; } set { Data = (Data & ~ACTRD_MASK) | ((value << ACTRD__SHIFT) & ACTRD_MASK); Mask = Mask | ACTRD_MASK; } }
            public UInt32 ACTWR { get { return (Data & ACTWR_MASK) >> ACTWR__SHIFT; } set { Data = (Data & ~ACTWR_MASK) | ((value << ACTWR__SHIFT) & ACTWR_MASK); Mask = Mask | ACTWR_MASK; } }
            public UInt32 RASMACTRD { get { return (Data & RASMACTRD_MASK) >> RASMACTRD__SHIFT; } set { Data = (Data & ~RASMACTRD_MASK) | ((value << RASMACTRD__SHIFT) & RASMACTRD_MASK); Mask = Mask | RASMACTRD_MASK; } }
            public UInt32 RASMACTWR { get { return (Data & RASMACTWR_MASK) >> RASMACTWR__SHIFT; } set { Data = (Data & ~RASMACTWR_MASK) | ((value << RASMACTWR__SHIFT) & RASMACTWR_MASK); Mask = Mask | RASMACTWR_MASK; } }

            public MC_ARB_DRAM_TIMING(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_ARB_DRAM_TIMING2
        {
            public static readonly UInt32 RAS2RAS_MASK = 0xff;
            public static readonly Int32 RAS2RAS__SHIFT = 0x0;
            public static readonly UInt32 RP_MASK = 0xff00;
            public static readonly Int32 RP__SHIFT = 0x8;
            public static readonly UInt32 WRPLUSRP_MASK = 0xff0000;
            public static readonly Int32 WRPLUSRP__SHIFT = 0x10;
            public static readonly UInt32 BUS_TURN_MASK = 0xff000000u; // 0x1f000000
            public static readonly Int32 BUS_TURN__SHIFT = 0x18;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 RAS2RAS { get { return (Data & RAS2RAS_MASK) >> RAS2RAS__SHIFT; } set { Data = (Data & ~RAS2RAS_MASK) | ((value << RAS2RAS__SHIFT) & RAS2RAS_MASK); Mask = Mask | RAS2RAS_MASK; } }
            public UInt32 RP { get { return (Data & RP_MASK) >> RP__SHIFT; } set { Data = (Data & ~RP_MASK) | ((value << RP__SHIFT) & RP_MASK); Mask = Mask | RP_MASK; } }
            public UInt32 WRPLUSRP { get { return (Data & WRPLUSRP_MASK) >> WRPLUSRP__SHIFT; } set { Data = (Data & ~WRPLUSRP_MASK) | ((value << WRPLUSRP__SHIFT) & WRPLUSRP_MASK); Mask = Mask | WRPLUSRP_MASK; } }
            public UInt32 BUS_TURN { get { return (Data & BUS_TURN_MASK) >> BUS_TURN__SHIFT; } set { Data = (Data & ~BUS_TURN_MASK) | ((value << BUS_TURN__SHIFT) & BUS_TURN_MASK); Mask = Mask | BUS_TURN_MASK; } }

            public MC_ARB_DRAM_TIMING2(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_ARB_BURST_TIME
        {

            public static readonly UInt32 STATE0_MASK   = 0x1f;
            public static readonly Int32  STATE0__SHIFT = 0x0;
            public static readonly UInt32 STATE1_MASK = 0x3e0;
            public static readonly Int32 STATE1__SHIFT = 0x5;
            public static readonly UInt32 TRRDS0_MASK = 0x7c00;
            public static readonly Int32 TRRDS0__SHIFT = 0xa;
            public static readonly UInt32 TRRDS1_MASK = 0xf8000;
            public static readonly Int32 TRRDS1__SHIFT = 0xf;
            public static readonly UInt32 TRRDL0_MASK = 0x1f00000;
            public static readonly Int32 TRRDL0__SHIFT = 0x14;
            public static readonly UInt32 TRRDL1_MASK = 0x3e000000;
            public static readonly Int32 TRRDL1__SHIFT = 0x19;

            public UInt32 Data { get; set; }
            public UInt32 Mask { get; set; }
            public UInt32 STATE0 { get { return (Data & STATE0_MASK) >> STATE0__SHIFT; } set { Data = (Data & ~STATE0_MASK) | ((value << STATE0__SHIFT) & STATE0_MASK); Mask = Mask | STATE0_MASK; } }
            public UInt32 STATE1 { get { return (Data & STATE1_MASK) >> STATE1__SHIFT; } set { Data = (Data & ~STATE1_MASK) | ((value << STATE1__SHIFT) & STATE1_MASK); Mask = Mask | STATE1_MASK; } }
            public UInt32 TRRDS0 { get { return (Data & TRRDS0_MASK) >> TRRDS0__SHIFT; } set { Data = (Data & ~TRRDS0_MASK) | ((value << TRRDS0__SHIFT) & TRRDS0_MASK); Mask = Mask | TRRDS0_MASK; } }
            public UInt32 TRRDS1 { get { return (Data & TRRDS1_MASK) >> TRRDS1__SHIFT; } set { Data = (Data & ~TRRDS1_MASK) | ((value << TRRDS1__SHIFT) & TRRDS1_MASK); Mask = Mask | TRRDS1_MASK; } }
            public UInt32 TRRDL0 { get { return (Data & TRRDL0_MASK) >> TRRDL0__SHIFT; } set { Data = (Data & ~TRRDL0_MASK) | ((value << TRRDL0__SHIFT) & TRRDL0_MASK); Mask = Mask | TRRDL0_MASK; } }
            public UInt32 TRRDL1 { get { return (Data & TRRDL1_MASK) >> TRRDL1__SHIFT; } set { Data = (Data & ~TRRDL1_MASK) | ((value << TRRDL1__SHIFT) & TRRDL1_MASK); Mask = Mask | TRRDL1_MASK; } }

            public MC_ARB_BURST_TIME(uint aData = 0x0)
            {
                Data = aData;
                Mask = 0x0;
            }
        }

        public class MC_SEQ_MISC0
        {
            public static readonly UInt32 MT_MASK	 = 0xf0000000u;
            public static readonly Int32 MT__SHIFT	 = 28;
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
                    return (MEMORY_VENDOR_ID == 1)  ? "Samsung" :
                           (MEMORY_VENDOR_ID == 2)  ? "Infineon" :
                           (MEMORY_VENDOR_ID == 3)  ? "Elpida" :
                           (MEMORY_VENDOR_ID == 4)  ? "Etron" :
                           (MEMORY_VENDOR_ID == 5)  ? "Nanya" :
                           (MEMORY_VENDOR_ID == 6)  ? "Hynix" :
                           (MEMORY_VENDOR_ID == 7)  ? "Mosel" :
                           (MEMORY_VENDOR_ID == 8)  ? "Winbond" :
                           (MEMORY_VENDOR_ID == 9)  ? "ESMT" :
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
        
        public AMDGMC81(int aDeviceIndex, ComputeDevice aComputeDevice)
            : base(aDeviceIndex, aComputeDevice) {
        }

        string mMemoryType = null;
        string mMemoryVendor = null;

        private void UpdateMemoryInfo()
        {
            if (!PCIExpress.Available)
                return;
            int busNumber = GetComputeDevice().PciBusIdAMD;
            if (busNumber <= 0)
                return;

            uint misc0Data = 0;
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC0, out misc0Data);
            MC_SEQ_MISC0 misc0 = new MC_SEQ_MISC0(misc0Data);
            mMemoryType = misc0.MemoryType;
            mMemoryVendor = misc0.MemoryVendor;
        }

        public override string GetMemoryType()
        {
            if (mMemoryType == null)
                UpdateMemoryInfo();
            return mMemoryType;
        }

        public override string GetMemoryVendor()
        {
            if (mMemoryVendor == null)
                UpdateMemoryInfo();
            return mMemoryVendor;
        }

        MC_ARB_DRAM_TIMING  arbDramTiming  = new MC_ARB_DRAM_TIMING();
        MC_ARB_DRAM_TIMING2 arbDramTiming2 = new MC_ARB_DRAM_TIMING2();
        MC_SEQ_RAS_TIMING   seqRasTiming   = new MC_SEQ_RAS_TIMING();
        MC_SEQ_CAS_TIMING   seqCasTiming   = new MC_SEQ_CAS_TIMING();
        MC_SEQ_MISC_TIMING  seqMiscTiming  = new MC_SEQ_MISC_TIMING();
        MC_SEQ_MISC_TIMING2 seqMiscTiming2 = new MC_SEQ_MISC_TIMING2();
        UInt32              seqMisc1;
        UInt32              seqMisc3;
        UInt32              seqMisc8;
        UInt32              seqWrCtlD0;
        UInt32              seqWrCtlD1;
        UInt32              seqWrCtl2;
        UInt32              arbDramTiming_1;
        UInt32              arbDramTiming2_1;
        UInt32              arbRttCntl0;

        public override void PrepareMemoryTimingMods(string algorithm)
        {
            if (!PCIExpress.Available)
                return;
            int busNumber = GetComputeDevice().PciBusIdAMD;
            if (busNumber <= 0)
                return;

            uint misc0Data = 0;
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC0, out misc0Data);
            MC_SEQ_MISC0 misc0 = new MC_SEQ_MISC0(misc0Data);
            mMemoryType = misc0.MemoryType;
            mMemoryVendor = misc0.MemoryVendor;

            arbDramTiming = new MC_ARB_DRAM_TIMING();
            arbDramTiming2 = new MC_ARB_DRAM_TIMING2();
            seqRasTiming = new MC_SEQ_RAS_TIMING();
            seqCasTiming = new MC_SEQ_CAS_TIMING();
            seqMiscTiming = new MC_SEQ_MISC_TIMING();
            seqMiscTiming2 = new MC_SEQ_MISC_TIMING2();

            uint value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_actrd", out value); arbDramTiming.ACTRD = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_actwr", out value); arbDramTiming.ACTWR = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_rasmactrd", out value); arbDramTiming.RASMACTRD = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_rasmactwr", out value); arbDramTiming.RASMACTWR = value;

            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_ras2ras", out value); arbDramTiming2.RAS2RAS = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_rp", out value); arbDramTiming2.RP = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_wrplusrp", out value); arbDramTiming2.WRPLUSRP = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_bus_turn", out value); arbDramTiming2.BUS_TURN = value;

            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trcdw", out value); seqRasTiming.TRCDW = seqRasTiming.TRCDWA = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trcdr", out value); seqRasTiming.TRCDR = seqRasTiming.TRCDRA = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trrd", out value); seqRasTiming.TRRD = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trc", out value); seqRasTiming.TRC = value;

            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_tr2w", out value); seqCasTiming.TR2W = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_tr2r", out value); seqCasTiming.TR2R = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_tw2r", out value); seqCasTiming.TW2R = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_tccdl", out value); seqCasTiming.TCCDL = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_tcl", out value); seqCasTiming.TCL = value;

            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trp_wra", out value); seqMiscTiming.TRP_WRA = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trp_rda", out value); seqMiscTiming.TRP_RDA = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trp", out value); seqMiscTiming.TRP = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_trfc", out value); seqMiscTiming.TRFC = value;

            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_pa2rdata", out value); seqMiscTiming2.PA2RDATA = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_pa2wdata", out value); seqMiscTiming2.PA2WDATA = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_faw", out value); seqMiscTiming2.FAW = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_t32aw", out value); seqMiscTiming2.T32AW = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_tredc", out value); seqMiscTiming2.TREDC = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_twedc", out value); seqMiscTiming2.TWEDC = value;
            MainForm.GetMemoryTimingsParameterValue(DeviceIndex, algorithm, "polaris10_twdatatr", out value); seqMiscTiming2.TWDATATR = value;

            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_seq_misc1", out seqMisc1);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_seq_misc3", out seqMisc3);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_seq_misc8", out seqMisc8);

            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_seq_wr_ctl_d0", out seqWrCtlD0);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_seq_wr_ctl_d1", out seqWrCtlD1);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_seq_wr_ctl_2",  out seqWrCtl2);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_arb_dram_timing_1", out arbDramTiming_1);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_arb_dram_timing2_1", out arbDramTiming2_1);
            MainForm.GetMemoryTimingsRegisterValue(DeviceIndex, algorithm, "polaris10_arb_rtt_cntl0",  out arbRttCntl0);
        }

        public override void UpdateMemoryTimings()
        {
            if (!PCIExpress.Available)
                return;
            int busNumber = GetComputeDevice().PciBusIdAMD;
            if (busNumber <= 0)
                return;

            if (PCIExpress.UpdateGMC81Registers(
                (uint)busNumber,
                arbDramTiming.Data,
                arbDramTiming2.Data,
                seqRasTiming.Data,
                seqCasTiming.Data,  
                seqMiscTiming.Data,  
                seqMiscTiming2.Data, 
                seqMisc1, 
                seqMisc3,
                seqMisc8,
                seqWrCtlD0,
                seqWrCtlD1,
                seqWrCtl2,
                arbDramTiming_1,
                arbDramTiming2_1,
                arbRttCntl0) == 0)
                Environment.Exit(1);
        }

        public override void PrintMemoryTimings()
        {
            if (!PCIExpress.Available && !PCIExpress.LoadPhyMem())
                return;
            int busNumber = GetComputeDevice().PciBusIdAMD;
            if (busNumber <= 0)
                return;

            uint ARBData = 0;
            uint ARB2Data = 0;
            uint RASData = 0;
            uint CASData = 0;
            uint MISCData = 0;
            uint MISC2Data = 0;
            uint PMGData = 0;
            uint burstTimeData = 0;
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_ARB_DRAM_TIMING, out ARBData);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_ARB_DRAM_TIMING2, out ARB2Data);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_CAS_TIMING, out CASData);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_RAS_TIMING, out RASData);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC_TIMING, out MISCData);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC_TIMING2, out MISC2Data);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_PMG_TIMING, out PMGData);
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_ARB_BURST_TIME, out burstTimeData);
            MC_ARB_DRAM_TIMING ARBTimings = new MC_ARB_DRAM_TIMING(ARBData);
            MC_ARB_DRAM_TIMING2 ARB2Timings = new MC_ARB_DRAM_TIMING2(ARB2Data);
            MC_SEQ_CAS_TIMING CASTimings = new MC_SEQ_CAS_TIMING(CASData);
            MC_SEQ_RAS_TIMING RASTimings = new MC_SEQ_RAS_TIMING(RASData);
            MC_SEQ_MISC_TIMING MISCTimings = new MC_SEQ_MISC_TIMING(MISCData);
            MC_SEQ_MISC_TIMING2 MISC2Timings = new MC_SEQ_MISC_TIMING2(MISC2Data);
            MC_SEQ_PMG_TIMING PMGTimings = new MC_SEQ_PMG_TIMING(PMGData);
            MC_ARB_BURST_TIME burstTime = new MC_ARB_BURST_TIME(burstTimeData);
            //if (CASTimings.TCL == 24)
            //    return;
            MainForm.Logger("=============");
            MainForm.Logger("Device Index: " + DeviceIndex);
            MainForm.Logger("PCIe Bus #: " + busNumber);
            uint MC_SEQ_RD_CTL_D0Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_RD_CTL_D0, out MC_SEQ_RD_CTL_D0Data); MainForm.Logger("MC_SEQ_RD_CTL_D0: 0x" + String.Format("{0:x8}", MC_SEQ_RD_CTL_D0Data));
            uint MC_SEQ_RD_CTL_D1Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_RD_CTL_D1, out MC_SEQ_RD_CTL_D1Data); MainForm.Logger("MC_SEQ_RD_CTL_D1: 0x" + String.Format("{0:x8}", MC_SEQ_RD_CTL_D1Data));
            uint MC_SEQ_WR_CTL_D0Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_WR_CTL_D0, out MC_SEQ_WR_CTL_D0Data); MainForm.Logger("MC_SEQ_WR_CTL_D0: 0x" + String.Format("{0:x8}", MC_SEQ_WR_CTL_D0Data));
            uint MC_SEQ_WR_CTL_D1Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_WR_CTL_D1, out MC_SEQ_WR_CTL_D1Data); MainForm.Logger("MC_SEQ_WR_CTL_D1: 0x" + String.Format("{0:x8}", MC_SEQ_WR_CTL_D1Data));
            uint MC_SEQ_WR_CTL_2Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_WR_CTL_2, out MC_SEQ_WR_CTL_2Data); MainForm.Logger("MC_SEQ_WR_CTL_2: 0x" + String.Format("{0:x8}", MC_SEQ_WR_CTL_2Data));
            uint MC_PHY_TIMING_D0Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_PHY_TIMING_D0, out MC_PHY_TIMING_D0Data); MainForm.Logger("MC_PHY_TIMING_D0: 0x" + String.Format("{0:x8}", MC_PHY_TIMING_D0Data));
            uint MC_PHY_TIMING_D1Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_PHY_TIMING_D1, out MC_PHY_TIMING_D1Data); MainForm.Logger("MC_PHY_TIMING_D1: 0x" + String.Format("{0:x8}", MC_PHY_TIMING_D1Data));
            uint MC_PHY_TIMING_2Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_PHY_TIMING_2, out MC_PHY_TIMING_2Data); MainForm.Logger("MC_PHY_TIMING_2: 0x" + String.Format("{0:x8}", MC_PHY_TIMING_2Data));
            uint MC_SEQ_MISC1Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC1, out MC_SEQ_MISC1Data); MainForm.Logger("MC_SEQ_MISC1: 0x" + String.Format("{0:x8}", MC_SEQ_MISC1Data));
            uint MC_SEQ_MISC2Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_RESERVE_M, out MC_SEQ_MISC2Data); MainForm.Logger("MC_SEQ_MISC2: 0x" + String.Format("{0:x8}", MC_SEQ_MISC2Data));
            uint MC_SEQ_MISC3Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC3, out MC_SEQ_MISC3Data); MainForm.Logger("MC_SEQ_MISC3: 0x" + String.Format("{0:x8}", MC_SEQ_MISC3Data));
            uint MC_SEQ_MISC4Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC4, out MC_SEQ_MISC4Data); MainForm.Logger("MC_SEQ_MISC4: 0x" + String.Format("{0:x8}", MC_SEQ_MISC4Data));
            uint MC_SEQ_MISC5Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC5, out MC_SEQ_MISC5Data); MainForm.Logger("MC_SEQ_MISC5: 0x" + String.Format("{0:x8}", MC_SEQ_MISC5Data));
            uint MC_SEQ_MISC6Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC6, out MC_SEQ_MISC6Data); MainForm.Logger("MC_SEQ_MISC6: 0x" + String.Format("{0:x8}", MC_SEQ_MISC6Data));
            uint MC_SEQ_MISC7Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC7, out MC_SEQ_MISC7Data); MainForm.Logger("MC_SEQ_MISC7: 0x" + String.Format("{0:x8}", MC_SEQ_MISC7Data));
            uint MC_SEQ_MISC8Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC8, out MC_SEQ_MISC8Data); MainForm.Logger("MC_SEQ_MISC8: 0x" + String.Format("{0:x8}", MC_SEQ_MISC8Data));
            uint MC_SEQ_MISC9Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_MISC9, out MC_SEQ_MISC9Data); MainForm.Logger("MC_SEQ_MISC9: 0x" + String.Format("{0:x8}", MC_SEQ_MISC9Data));
            uint MC_SEQ_DRAMData = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_DRAM, out MC_SEQ_DRAMData); MainForm.Logger("MC_SEQ_DRAM: 0x" + String.Format("{0:x8}", MC_SEQ_DRAMData));
            uint MC_SEQ_DRAM2Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_DRAM_2, out MC_SEQ_DRAM2Data); MainForm.Logger("MC_SEQ_DRAM_2: 0x" + String.Format("{0:x8}", MC_SEQ_DRAM2Data));
            uint MC_SEQ_CMDData = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_SEQ_CMD, out MC_SEQ_CMDData); MainForm.Logger("MC_SEQ_CMD: 0x" + String.Format("{0:x8}", MC_SEQ_CMDData));
            uint MC_CONFIGData = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_CONFIG, out MC_CONFIGData); MainForm.Logger("MC_CONFIG: 0x" + String.Format("{0:x8}", MC_CONFIGData));
            uint MC_IO_PAD_CNTL_D0Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_IO_PAD_CNTL_D0, out MC_IO_PAD_CNTL_D0Data); MainForm.Logger("MC_IO_PAD_CNTL_D0: 0x" + String.Format("{0:x8}", MC_IO_PAD_CNTL_D0Data));
            uint MC_IO_PAD_CNTL_D1Data = 0; PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_IO_PAD_CNTL_D1, out MC_IO_PAD_CNTL_D1Data); MainForm.Logger("MC_IO_PAD_CNTL_D1: 0x" + String.Format("{0:x8}", MC_IO_PAD_CNTL_D1Data));
            uint data = 0;
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_ARB_DRAM_TIMING, out data); MainForm.Logger("mmMC_ARB_DRAM_TIMING: 0x" + String.Format("{0:x8}", ARBTimings.Data));
            PCIExpress.ReadFromAMDGPURegister(busNumber, (int)GMC81Registers.mmMC_ARB_DRAM_TIMING2, out data); MainForm.Logger("mmMC_ARB_DRAM_TIMING2: 0x" + String.Format("{0:x8}", arbDramTiming2.Data));
            MainForm.Logger("MC_SEQ_PMG: 0x" + String.Format("{0:x8}", PMGData));
            MainForm.Logger("-------------");
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
            MainForm.Logger("STATE0:   " + burstTime.STATE0);
            MainForm.Logger("STATE1:   " + burstTime.STATE1);
            MainForm.Logger("TRRDS0:   " + burstTime.TRRDS0);
            MainForm.Logger("TRRDS1:   " + burstTime.TRRDS1);
            MainForm.Logger("TRRDL0:   " + burstTime.TRRDL0);
            MainForm.Logger("TRRDL1:   " + burstTime.TRRDL1);
            MainForm.Logger("-------------");
            MainForm.Logger("TRCDW:    " + RASTimings.TRCDW);
            MainForm.Logger("TRCDWA:   " + RASTimings.TRCDWA);
            MainForm.Logger("TRCDR:    " + RASTimings.TRCDR);
            MainForm.Logger("TRCDRA:   " + RASTimings.TRCDRA);
            MainForm.Logger("TRRD:     " + RASTimings.TRRD);
            MainForm.Logger("TRC:      " + RASTimings.TRC);
            MainForm.Logger("-------------");
            MainForm.Logger("TNOPW:    " + CASTimings.TNOPW);
            MainForm.Logger("TNOPR:    " + CASTimings.TNOPR);
            MainForm.Logger("TR2W:     " + CASTimings.TR2W);
            MainForm.Logger("TCCDL:    " + CASTimings.TCCDL);
            MainForm.Logger("TR2R:     " + CASTimings.TR2R);
            MainForm.Logger("TW2R:     " + CASTimings.TW2R);
            MainForm.Logger("TCL:      " + CASTimings.TCL);
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
            MainForm.Logger("-------------");
            MainForm.Logger("TCKSRE:   " + PMGTimings.TCKSRE);
            MainForm.Logger("TCKSRX:   " + PMGTimings.TCKSRX);
            MainForm.Logger("TCKE_PULSE:" + PMGTimings.TCKE_PULSE);
            MainForm.Logger("TCKE:     " + PMGTimings.TCKE);
            MainForm.Logger("SEQ_IDLE: " + PMGTimings.SEQ_IDLE);
            MainForm.Logger("TCKE_PULSE_MSB:" + PMGTimings.TCKE_PULSE_MSB);
            MainForm.Logger("SEQ_IDLE_SS:" + PMGTimings.SEQ_IDLE_SS);
            MainForm.Logger("-------------");
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000,         out data); MainForm.Logger("SMC74_Firmware_Header.Digest[0]: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 1 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.Digest[1]: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 2 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.Digest[2]: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 3 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.Digest[3]: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 4 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.Digest[4]: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 5 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.Version: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 6 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.HeaderSize: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 7 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.Flags: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 8 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.EntryPoint: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 9 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.CodeSize: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 10 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.ImageSize: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 17 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.mcRegisterTable: 0x" + String.Format("{0:x8}", data));
            PCIExpress.SMU7_ReadDWORD((uint)busNumber, 0x20000 + 18 * 4, out data); MainForm.Logger("SMC74_Firmware_Header.mcArbDramTimingTable: 0x" + String.Format("{0:x8}", data));
        }
    }
}
