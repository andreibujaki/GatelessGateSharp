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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Runtime.ExceptionServices;


namespace GatelessGateSharp
{
    static class Program
    {
        private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            var w = new Form() { Size = new System.Drawing.Size(0, 0) };
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            w.BringToFront(); 
            MessageBox.Show(w, "Unhandled Thread Exception: " + e.Exception.Message + e.Exception.StackTrace, "Gateless Gate Sharp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Environment.Exit(1);
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var w = new Form() { Size = new System.Drawing.Size(0, 0) };
            Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            w.BringToFront();
            MessageBox.Show(w, "Unhandled Exception: " + ((Exception)e.ExceptionObject).Message + ((Exception)e.ExceptionObject).StackTrace, "Gateless Gate Sharp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            if (e.ExceptionObject.GetType() == typeof(DllNotFoundException))
            {
                foreach (var process in Process.GetProcessesByName("GatelessGateSharp"))
                    process.Kill();
            }
            Environment.Exit(1);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory; // for auto-start

            using (var mutex = new Mutex(true, "GatelessGateSharp.exe"))
            {
                Process process = null;
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "GatelessGateSharpMonitor.exe";
                    //startInfo.Arguments = args;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    process = new Process();
                    process.StartInfo = startInfo;
                    process.EnableRaisingEvents = true;
                    process.Start();
                }
                catch (Exception) { }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                
                try { process.Kill(); } catch (Exception) { }
            }
        }
    }
}
