using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatelessGateSharp {
    public class Parameters
    {
        public static readonly System.Diagnostics.ProcessPriorityClass ProcessPriority = System.Diagnostics.ProcessPriorityClass.High;
        public static readonly System.Threading.ThreadPriority MainThreadPriority = System.Threading.ThreadPriority.AboveNormal;
        public static readonly System.Threading.ThreadPriority MemoryTimingsTaskPriority = System.Threading.ThreadPriority.Highest;

        public static readonly int MemoryTimingUpdateInterval = 10;

        public static readonly int FanControlUpdateInterval = 5000;
        
        public static readonly int WaitTimeForRestartingMinerThreadInMilliseconds = 0;
        public static readonly int TimeoutForFirstJobInMilliseconds = 60000;
        public static readonly int MaxLogFileSize = 1024 * 1024;
        public static readonly int LogMaxNumLines = 256;

        // I really need the DEVFEE to continue to develop this miner and to make my wife happy. Thank you. - zawawa
        public static readonly int DevFeePercentage = 1;
        public static readonly int DevFeeDurationInSeconds = 60;
        public static readonly int DevFeeInitialDelayInSeconds = 15 * 60;
        public static readonly string DevFeeUsernamePostfix = ".DEVFEE";
        public static readonly string DevFeeBitcoinAddress = "1k1WhysGsp7kNRy4atzzr6MaDrBiXw7wm";
        public static readonly string DevFeeEthereumAddress = "0x91fa32e00b0f365d629fb625182a83fed61f0642";
        public static readonly string DevFeeMoneroAddress = "463tWEBn5XZJSxLU6uLQnQ2iY9xuNcDbjLSjkn3XAXHCbLrTTErJrBWYgHJQyrCwkNgYvyV3z8zctJLPCZy24jvb3NiTcTJ.3c33141709b14b9bba1f1d49b39c69f8fb88a4cd571e4e80b3c0682375964a0f";
        public static readonly string DevFeePascalAddress = "86646-64.b7db0252955d6b0f";
        public static readonly string DevFeeLbryAddress = "bEFGDsEnfSzRs1UVKoUqaQfnvWAbPzLiuB";
        public static readonly string DevFeeZcashAddress = "t1NwUDeSKu4BxkD58mtEYKDjzw5toiLfmCu";
        public static readonly string DevFeeFeathercoinAddress = "6evDqvqep9WvRNnm2xV51bFZgwiw6kv7bh";
        public static readonly string DevFeeRavenAddress = @"RNw1EqHTD3bWj6R9dsi3bJe6YJgN55hGxQ";
        public static readonly string DevFeeSumokoinAddress = @"Sumoo78AVSZQKEuRgwfZm94BzCwvTo6LeDksMj2c237hMYDM74epEnbhJLWBdndsBeD4WYhw6GS6yW3vJWCcM7QjGy1AR9tq6ef";
        public static readonly string DevFeeAEONAddress = @"Wms1DnwvmYS2eiAia3W7BRcyehQFVBYEDBQnqUrPtVeaNj6NZM1UZXJf7HU39mfAAn6p8D4jEK6z33Z95nQrHTaL1pcBNGxUh";
        public static readonly string DevFeePigeoncoinAddress = "PMCR5gBQ48DXKykmt1aoULYdnCmCpiknhL";
    }
}
