using System;
using System.Threading;
using System.Diagnostics;



namespace GatelessGateSharpMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                try {
                    int counter = 0;
                    for (counter = 0; counter < 600; ++counter) {
                        bool unresponsive = false;
                        foreach (var process in System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp"))
                            if (!process.Responding)
                                unresponsive = true;
                        if (!unresponsive)
                            break;
                        Thread.Sleep(100);
                    }
                    if (counter >= 600) {
                        foreach (var process in System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp"))
                            try { process.Kill(); } catch (Exception) { }
                    }
                    if (System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp").Length == 0)
                        Process.Start("GatelessGateSharp.exe");
                    Thread.Sleep(100);
                } catch (Exception) { }
            }
        }
    }
}