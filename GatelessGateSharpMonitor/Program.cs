using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;



namespace GatelessGateSharpMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var handle = new Mutex(true, "GatelessGateSharp.exe"))
            {
                try { handle.WaitOne(); } catch (Exception) {}
                Thread.Sleep(5000);
                Process.Start("GatelessGateSharp.exe");
            }
        }
    }
}