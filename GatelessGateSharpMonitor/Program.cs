using System;
using System.Threading;
using System.Diagnostics;



namespace GatelessGateSharpMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--Launch") {
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp").Length == 0)
                    Process.Start("GatelessGateSharp.exe");
            } else {
                // Monitor GGS.
                while (true) {
                    try {
                        int counter = 0;
                        for (counter = 0; counter < 5 * 60; ++counter) {
                            bool unresponsive = false;
                            foreach (var process in System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp"))
                                if (!process.Responding)
                                    unresponsive = true;
                            if (!unresponsive)
                                break;
                            Thread.Sleep(1000);
                        }
                        if (counter >= 5 * 60) {
                            foreach (var process in System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp"))
                                try { process.Kill(); } catch (Exception) { }
                        }
                        if (System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp").Length == 0)
                            Process.Start("GatelessGateSharp.exe");
                    } catch (Exception) { }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
