using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace GatelessGateSharp {
    class Controller {
        public enum ApplicationGlobalState {
            Idle = 0,
            Mining = 1,
            Switching = 2
        };



        private static Controller sInstance = new Controller(); // only for initialization
        public static ApplicationGlobalState AppState { get; set; }
        public static OpenCLDevice[] OpenCLDevices { get; set; }
        public static Stratum PrimaryStratum { get; set; }
        public static Stratum PrimaryStratumBackup { get; set; }
        public static Stratum SecondaryStratum { get; set; }
        public static Stratum SecondaryStratumBackup { get; set; }
        public static List<Miner> Miners { get; set; }
        public static System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();



        private Controller() {
            AppState = ApplicationGlobalState.Switching;
            PrimaryStratum = null;
            PrimaryStratumBackup = null;
            SecondaryStratum = null;
            SecondaryStratumBackup = null;
            Miners = new List<Miner>() { };
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
                        if (device.MemoryTimingModsEnabled)
                            device.UpdateMemoryTimings();
                    }
                } catch (Exception) { }
                System.Threading.Thread.Sleep(Parameters.MemoryTimingUpdateInterval);
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
