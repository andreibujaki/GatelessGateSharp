// Copyright 2017 Yurio Miyazawa (a.k.a zawawa) <me@yurio.net>
//
// This file is part of Gateless Gate Sharp.
//
// Gateless Gate Sharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Gateless Gate Sharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Gateless Gate Sharp.  If not, see <http://www.gnu.org/licenses/>.



using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;



namespace GatelessGateSharp
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int FreeConsole();

        private const int ATTACH_PARENT_PROCESS = -1;

        private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(Utilities.GetAutoClosingForm(), "Unhandled Thread Exception: " + e.Exception.Message + e.Exception.StackTrace + "\nRestarting the application...", "Gateless Gate Sharp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Program.Exit(false);
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(Utilities.GetAutoClosingForm(), "Unhandled Exception: " + ((Exception)e.ExceptionObject).Message + ((Exception)e.ExceptionObject).StackTrace + "\nRestarting the application...", "Gateless Gate Sharp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            if (e.ExceptionObject.GetType() == typeof(DllNotFoundException))
            {
                foreach (var process in Process.GetProcessesByName("GatelessGateSharp"))
                    process.Kill();
            }
            Program.Exit(false);
        }

        static Mutex sMutex = new Mutex(true, "{1D2A713A-A29C-418C-BC62-2E98BD325490}");
        public static bool KillMonitor { get; set; }
        public static void Exit() { Application.Exit(); }
        public static void Exit(bool killMonitor) { KillMonitor = killMonitor; Application.Exit(); }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        static int Main(string[] args)
        {
           var attachedToConsole = AttachConsole(ATTACH_PARENT_PROCESS);

            bool mutexResult = false;
            try { mutexResult = sMutex.WaitOne(TimeSpan.Zero, true); } catch (Exception) {}
            if (!mutexResult)
                return 1;

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;

            Environment.SetEnvironmentVariable("CUDA_CACHE_DISABLE", "1", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("GPU_MAX_ALLOC_PERCENT", "100", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("GPU_USE_SYNC_OBJECTS", "1", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("GPU_SINGLE_ALLOC_PERCENT", "100", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("GPU_MAX_HEAP_SIZE", "100", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("GPU_FORCE_64BIT_PTR", "0", EnvironmentVariableTarget.Process);

            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory; // for auto-start

#if !COMMAND_LINE_VERSION
            // Launch monitor
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("GatelessGateSharpMonitor"))
                try { process.Kill(); } catch (Exception) { } 
            Process monitor = null;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "GatelessGateSharpMonitor.exe";
                //startInfo.Arguments = args;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                monitor = new Process();
                monitor.StartInfo = startInfo;
                monitor.EnableRaisingEvents = true;
                monitor.Start();
            }
            catch (Exception) { }
#endif
            
            KillMonitor = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            
#if !COMMAND_LINE_VERSION
            if (KillMonitor)
                try { monitor.Kill(); } catch (Exception) { }
#endif

            try { sMutex.ReleaseMutex(); } catch (Exception) { }

            if (attachedToConsole)
                FreeConsole();

            return 0;
        }
    }
}
