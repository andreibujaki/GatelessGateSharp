using System;
using System.Threading;
using System.Diagnostics;



namespace GatelessGateSharpLauncher
{
    class Program
    {
        static int Main(string[] args)
        {
            try {
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (System.Diagnostics.Process.GetProcessesByName("GatelessGateSharp").Length == 0)
                    Process.Start("GatelessGateSharp.exe");
            } catch (Exception) { }

            return 0;
        }
    }
}
