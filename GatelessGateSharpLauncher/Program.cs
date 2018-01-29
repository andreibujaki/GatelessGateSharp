using System;
using System.Threading;
using System.Diagnostics;



namespace GatelessGateSharpLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var proc in System.Diagnostics.Process.GetProcessesByName("msiexec"))
                    proc.Kill();
                if (System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp").Length == 0)
                    Process.Start("GatelessGateSharp.exe");
            } catch (Exception) { }
        }
    }
}
