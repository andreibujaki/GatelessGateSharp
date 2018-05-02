using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloo;



namespace GatelessGateSharp
{
    public class Device : IDisposable
    {
        private int mDeviceIndex;
        private int mAcceptedShares;
        private int mRejectedShares;
        private System.Threading.Mutex mMutex = new System.Threading.Mutex();

        public virtual String GetVendor() { return ""; }
        public virtual String GetName() { return ""; }
        public virtual String GetOpenCLName() { return ""; }
        public virtual long GetMemorySize() { return 0; }
        public virtual long GetMaxComputeUnits() { return 0; }

        public int DeviceIndex { get { return mDeviceIndex; } }
        public string Vendor { get { return GetVendor(); } }
        public string Name { get { return GetName(); } }
        public string OpenCLName { get { return GetOpenCLName(); } }
        public long MemorySize { get { return GetMemorySize(); } }
        public long MaxComputeUnits { get { return GetMaxComputeUnits(); } }
        public int AcceptedShares { get { return mAcceptedShares; } }
        public int RejectedShares { get { return mRejectedShares; } }
        public double TotalHashesPrimaryAlgorithm = 0;
        public double TotalHashesSecondaryAlgorithm = 0;

        public bool OverclockingEnabled { get; set; }
        public int TargetPowerLimit { get; set; }
        public int TargetCoreClock { get; set; }
        public int TargetMemoryClock { get; set; }
        public int TargetCoreVoltage { get; set; }
        public int TargetMemoryVoltage { get; set; }

        public bool FanControlEnabled { get; set; }
        public int TargetMaxFanSpeed { get; set; }
        public int TargetMinFanSpeed { get; set; }
        public int TargetTemperature { get; set; }
        public int TargetMaxTemperature { get; set; }
        
        public int PCIDeviceID = 0;
        public string PNPString;

        public virtual void SaveDefaultMemoryTimings() { }
        public bool MemoryTimingModsEnabled { get; set; }
        public virtual void UpdateMemoryTimings() { }
        public virtual void PrintMemoryTimings() { }
        public virtual void PrepareMemoryTimingMods(string algorithm) { }

        public virtual string GetMemoryType() { return null; }
        public virtual string GetMemoryVendor() { return null; }
        public string MemoryType { get { return GetMemoryType(); } }
        public string MemoryVendor { get { return GetMemoryVendor(); } }

        // https://github.com/CLRX/CLRX-mirror/blob/76a2912097a12f7dd274d7319b2698f88ef6d705/doc/GcnIsa.md
        public bool IsGCN1 { get { return Vendor == "AMD" && (OpenCLName == "Pitcairn" || OpenCLName == "Tahiti" || OpenCLName == "Capeverde" || OpenCLName == "Hainan"); } }
        public bool IsGCN2 { get { return Vendor == "AMD" && (OpenCLName == "Bonaire" || OpenCLName == "Hawaii"); } }
        public bool IsGCN3 { get { return Vendor == "AMD" && (OpenCLName == "Tonga" || OpenCLName == "Fiji" || OpenCLName == "Ellesmere" || OpenCLName == "Baffin"); } }
        public bool IsGCN5 { get { return Vendor == "AMD" && (OpenCLName == "Vega"); } }

        public Device(int aDeviceIndex)
        {
            mDeviceIndex = aDeviceIndex;
            mAcceptedShares = 0;
            mRejectedShares = 0;
            OverclockingEnabled = false;
            FanControlEnabled = false;
            MemoryTimingModsEnabled = false;
        }

        public void Dispose() {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
        }

        public int IncrementAcceptedShares()
        {
            return ++mAcceptedShares;
        }

        public int IncrementRejectedShares()
        {
            return ++mRejectedShares;
        }

        public void ClearShares()
        {
            mAcceptedShares = 0;
            mRejectedShares = 0;
        }
    }
}
