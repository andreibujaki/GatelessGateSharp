           using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace GatelessGateSharp {
    public class Controller {
        public enum ApplicationGlobalState {
            Idle = 0,
            Mining = 1,
            Switching = 2,
            Initializing = 3,
            Relaunching = 4,
        };

        public enum ApplicationBenchmarkState
        {
            NotRunning,
            Running,
            Resuming,
            CoolingDown
        };

        public enum ApplicationOptimizerState
        {
            NotRunning,
            Running,
        };

        [System.SerializableAttribute()]
        public class BenchmarkEntry
        {
            public int ID;
            public List<BenchmarkParameter> Parameters = new List<BenchmarkParameter> { };
            public List<BenchmarkResult> Results = new List<BenchmarkResult> { };
            public int Remaining;
            public int SuccessCount;
            public int StabilityScore;
            public double SpeedPrimaryAlgorithm;
            public double SpeedSecondaryAlgorithm;
            public bool Rebooted;
        }

        [System.SerializableAttribute()]
        public class OptimizerEntry
        {
            public int ID;
            public int DeviceIndex;
            public string Algorithm = null;
            public string Parameter = null;
            public string ParameterWithValues = null;

            public int SuccessCount;
            public int ResultCount;
            public double SpeedPrimaryAlgorithm;
            public double SpeedSecondaryAlgorithm;

            public OptimizerEntry() { }
            public OptimizerEntry(OptimizerEntry aEntry) { DeviceIndex = aEntry.DeviceIndex; Algorithm = aEntry.Algorithm; Parameter = aEntry.Parameter;}
            public OptimizerEntry(int aDeviceIndex, string aAlgorithm, string aFirstParameter) { DeviceIndex = aDeviceIndex; Algorithm = aAlgorithm; Parameter = aFirstParameter; }
        }

        [System.SerializableAttribute()]
        public class BenchmarkParameter
        {
            public string Name;
            public string Value;
            public List<string> OriginalValues = new List<string> { };

            public BenchmarkParameter(string aName, string aValue, string aOriginalValue)
            {
                Name = aName;
                Value = aValue;
                foreach (var device in Controller.OpenCLDevices)
                    OriginalValues.Add(aOriginalValue);
            }

            public BenchmarkParameter(string aName, string aValue, List<string> aOriginalValues)
            {
                Name = aName;
                Value = aValue;
                OriginalValues = aOriginalValues;
            }

            public BenchmarkParameter()
            {
                Name = "";
                Value = "";
            }
        }

        [System.SerializableAttribute()]
        public class BenchmarkResult
        {
            public bool Success;
            public double SpeedPrimaryAlgorithm;
            public double SpeedSecondaryAlgorithm;
        }

        public static ApplicationGlobalState AppState = ApplicationGlobalState.Initializing;
        public static ApplicationBenchmarkState BenchmarkState = ApplicationBenchmarkState.NotRunning;
        public static List<BenchmarkEntry> BenchmarkEntries = new List<BenchmarkEntry>() { };
        public static List<BenchmarkEntry> BenchmarkRecords = new List<BenchmarkEntry>() { };
        public static ApplicationOptimizerState OptimizerState = ApplicationOptimizerState.NotRunning;
        public static List<OptimizerEntry> OptimizerEntries = new List<OptimizerEntry>() { };
        public static List<OptimizerEntry> OptimizerRecords = new List<OptimizerEntry>() { };
        public static Mutex OpenCLBinaryMutex = new Mutex();

        public static OpenCLDevice[] OpenCLDevices;
        public static Stratum PrimaryStratum;
        public static Stratum PrimaryStratumBackup;
        public static Stratum SecondaryStratum;
        public static Stratum SecondaryStratumBackup;
        public static List<Miner> Miners = new List<Miner>() { };
        public static System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch BenchmarkStopwatch = new System.Diagnostics.Stopwatch();

        public static void Task_MemoryTimings(object cancellationToken)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Thread.CurrentThread.Priority = Parameters.MemoryTimingsTaskPriority;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                int interval = Parameters.OverclockingSettingsUpdateInterval;
                foreach (var device in Controller.OpenCLDevices) {
                    try {
                        if (device.OverclockingEnabled)
                            device.UpdateOverclockingSettings();
                        if (device.MemoryTimingModsEnabled && device.MemoryClock == device.TargetMemoryClock) {
                            interval = Parameters.MemoryTimingUpdateInterval;
                            device.UpdateMemoryTimings();
                        }
                    } catch (Exception ex) { MainForm.Logger(ex); }
                }
                System.Threading.Thread.Sleep(interval);
            }
        }

        public static void Task_FanControl(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    if (AppState == ApplicationGlobalState.Mining) {
                        foreach (var device in Controller.OpenCLDevices) {
                            if (!device.FanControlEnabled)
                                continue;

                            int currentTemperature = device.Temperature;
                            int targetTemperature = device.TargetTemperature;
                            int maxTemperature = device.TargetMaxTemperature;
                            int currentFanSpeed = device.FanSpeed;
                            int maxFanSpeed = device.TargetMaxFanSpeed;
                            int minFanSpeed = device.TargetMinFanSpeed;
                            int newFanSpeed = currentFanSpeed;

                            if (currentTemperature > targetTemperature + 5)
                                newFanSpeed += 5;
                            else if (currentTemperature > targetTemperature)
                                ++newFanSpeed;
                            else if (currentTemperature < targetTemperature - 5)
                                newFanSpeed -= 5;
                            else if (currentTemperature < targetTemperature)
                                --newFanSpeed;
                            if (currentTemperature > maxTemperature)
                                newFanSpeed = maxFanSpeed;
                            if (newFanSpeed < minFanSpeed)
                                newFanSpeed = minFanSpeed;
                            device.FanSpeed = newFanSpeed;
                        }
                    }
                } catch (Exception) { }
                System.Threading.Thread.Sleep(Parameters.FanControlUpdateInterval);
            }
        }
    }
}
