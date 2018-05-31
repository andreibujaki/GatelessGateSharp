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
        public int DeviceIndex;

        private int mAcceptedShares;
        private int mRejectedShares;
        private System.Threading.Mutex mMutex = new System.Threading.Mutex();

        public virtual String GetVendor() { return ""; }
        public virtual String GetName() { return ""; }
        public virtual String GetOpenCLName() { return ""; }
        public virtual long GetMemorySize() { return 0; }
        public virtual long GetMaxComputeUnits() { return 0; }

        public string Vendor { get { return GetVendor(); } }
        public string Name { get { return GetName(); } }
        public string OpenCLName { get { return GetOpenCLName(); } }
        public long MemorySize { get { return GetMemorySize(); } }
        public long MaxComputeUnits { get { return GetMaxComputeUnits(); } }
        public int AcceptedShares { get { return mAcceptedShares; } }
        public int RejectedShares { get { return mRejectedShares; } }

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
        public bool IsAMD { get { return Vendor == "AMD"; } }
        public bool IsNVIDIA { get { return Vendor == "NVIDIA"; } }
        public bool IsGCN1 { get { return Vendor == "AMD" && (OpenCLName == "Pitcairn" || OpenCLName == "Tahiti" || OpenCLName == "Capeverde" || OpenCLName == "Hainan"); } }
        public bool IsGCN2 { get { return Vendor == "AMD" && (OpenCLName == "Bonaire" || OpenCLName == "Hawaii"); } }
        public bool IsGCN3 { get { return Vendor == "AMD" && (OpenCLName == "Tonga" || OpenCLName == "Fiji" || OpenCLName == "Ellesmere" || OpenCLName == "Baffin" || OpenCLName == "gfx804"); } }
        public bool IsGCN5 { get { return Vendor == "AMD" && (OpenCLName == "gfx900"); } }

        public bool IsSpeedStable = false;
        public double prevSpeed = 0;

        public double AverageSpeed {
            get {
                double speedPrimaryAlgorithm = 0;
                foreach (var miner in Controller.Miners) {
                    if (miner.DeviceIndex == DeviceIndex)
                        speedPrimaryAlgorithm += miner.AverageSpeed;
                }
                return speedPrimaryAlgorithm;
            }
        }

        public double AverageSpeedSecondaryAlgorithm {
            get {
                double speedSecondaryAlgorithm = 0;
                foreach (var miner in Controller.Miners) {
                    if (miner.DeviceIndex == DeviceIndex)
                        speedSecondaryAlgorithm += miner.AverageSpeedSecondaryAlgorithm;
                }
                return speedSecondaryAlgorithm;
            }
        }

        public Device(int aDeviceIndex)
        {
            DeviceIndex = aDeviceIndex;
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

        public double PowerConsumption {
            get {
                return GetPowerConsumption();
            }
        }

        public virtual double GetPowerConsumption()
        {
            return 0;
        }

        System.Diagnostics.Stopwatch mAveragePowerConsumptionStopwatch = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch mAveragePowerConsumptionUpdateStopwatch = new System.Diagnostics.Stopwatch();
        double mPowerConsumptionWeighedTotal = 0;

        public void ResetStatistics()
        {
            prevSpeed = 0;
            IsSpeedStable = false;

            mPowerConsumptionWeighedTotal = 0;
            mAveragePowerConsumptionStopwatch.Restart();
            mAveragePowerConsumptionUpdateStopwatch.Restart();
        }

        public double Speed {
            get {
                double currentSpeed = 0;
                foreach (var miner in Controller.Miners)
                    if (miner.DeviceIndex == DeviceIndex)
                        currentSpeed += miner.Speed;
                return currentSpeed;
            }
        }

        public double SpeedSecondaryAlgorithm {
            get {
                double currentSpeed = 0;
                foreach (var miner in Controller.Miners)
                    if (miner.DeviceIndex == DeviceIndex)
                        currentSpeed += miner.SpeedSecondaryAlgorithm;
                return currentSpeed;
            }
        }

        public void UpdateStatistics()
        {
            double currentSpeed = Speed;
            if (prevSpeed > 0 && currentSpeed <= prevSpeed * 1.01)
                IsSpeedStable = true;
            prevSpeed = currentSpeed;
            if (mAveragePowerConsumptionUpdateStopwatch.ElapsedMilliseconds > 0)
                mPowerConsumptionWeighedTotal += PowerConsumption * mAveragePowerConsumptionUpdateStopwatch.ElapsedMilliseconds;
            mAveragePowerConsumptionUpdateStopwatch.Restart();
        }

        public double GetAveragePowerConsumption()
        {
            UpdateStatistics();
            return mPowerConsumptionWeighedTotal / mAveragePowerConsumptionStopwatch.ElapsedMilliseconds;
        }
    }
}
