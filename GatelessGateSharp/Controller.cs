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
            Initializing = 3
        };

        public enum ApplicationBenchmarkState
        {
            NotRunning,
            Running,
            Resuming
        };

        [System.SerializableAttribute()]
        public class BenchmarkEntry
        {
            public List<BenchmarkParameter> Parameters = new List<BenchmarkParameter> { };
            public List<BenchmarkResult> Results = new List<BenchmarkResult> { };
            public int Remaining;
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

        private static Controller sInstance = new Controller(); // only for initialization
        public static ApplicationGlobalState AppState { get; set; }
        public static ApplicationBenchmarkState BenchmarkState { get; set; }

        public static OpenCLDevice[] OpenCLDevices { get; set; }
        public static Stratum PrimaryStratum { get; set; }
        public static Stratum PrimaryStratumBackup { get; set; }
        public static Stratum SecondaryStratum { get; set; }
        public static Stratum SecondaryStratumBackup { get; set; }
        public static List<Miner> Miners { get; set; }
        public static List<BenchmarkEntry> BenchmarkEntries { get; set; }
        public static List<BenchmarkEntry> BenchmarkRecords { get; set; }
        public static System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch BenchmarkStopwatch = new System.Diagnostics.Stopwatch();

        private Controller()
        {
            AppState = ApplicationGlobalState.Initializing;
            BenchmarkState = ApplicationBenchmarkState.NotRunning;

            PrimaryStratum = null;
            PrimaryStratumBackup = null;
            SecondaryStratum = null;
            SecondaryStratumBackup = null;
            Miners = new List<Miner>() { };
            BenchmarkEntries = new List<BenchmarkEntry>() { };
            BenchmarkRecords = new List<BenchmarkEntry>() { };
        }

        public static void Task_MemoryTimings(object cancellationToken)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Thread.CurrentThread.Priority = Parameters.MemoryTimingsTaskPriority;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    // memory timings
                    foreach (var device in Controller.OpenCLDevices) {
                        if (device.MemoryTimingModsEnabled) {
                            device.UpdateMemoryTimings();
                            System.Threading.Thread.Sleep(Parameters.MemoryTimingUpdateInterval);
                        }
                    }
                } catch (Exception) { }
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
