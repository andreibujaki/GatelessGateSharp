﻿// Copyright 2017 Yurio Miyazawa (a.k.a zawawa) <me@yurio.net>
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
using Cloo;


namespace GatelessGateSharp
{
    public class Miner : IDisposable
    {
        private OpenCLDevice mDevice;
        private bool mStopped = false;
        private bool mDone = false;
        private String mAlgorithmName = "";
        private String mFirstAlgorithmName = "";
        private String mSecondAlgorithmName = "";
        private System.Threading.Thread mMinerThread = null;
        private DateTime mLastAlive = DateTime.Now;
        private System.Diagnostics.Stopwatch mStopwatch = new System.Diagnostics.Stopwatch();
        public OpenCLDevice Device { get { return mDevice; } }
        public int DeviceIndex { get { return mDevice.DeviceIndex; } }
        public bool Stopped { get { return mStopped; } }
        public bool Done { get { return mDone; } }
        public double Speed { get; set; }
        public double SpeedSecondaryAlgorithm { get; set; }
        public double AverageSpeed { get { return Runtime > 0 ? (HashCount / Runtime) : 0; } }
        public double AverageSpeedSecondaryAlgorithm { get { return Runtime > 0 ? (HashCountSecondaryAlgorithm / Runtime) : 0; } }
        public String AlgorithmName { get { return mAlgorithmName; } }
        public String PrimaryAlgorithmName { get { return mFirstAlgorithmName; } }
        public String SecondaryAlgorithmName { get { return mSecondAlgorithmName; } }
        public ComputeContext Context { get { return mDevice.Context; } }
        public UnrecoverableException UnrecoverableException { get; set; }
        public long MemoryUsage { get; set; }
        public double Runtime = 0;
        public double HashCount = 0;
        public double HashCountSecondaryAlgorithm = 0;

        public void ResetHashCount()
        {
            Runtime = 0;
            HashCount = 0;
            HashCountSecondaryAlgorithm = 0;
        }

        public void ReportHashCount(double aHashCount, double aHashCountSecondaryAlgorithm, double aRuntime)
        {
            if (aRuntime > 0) {
                Runtime += aRuntime;
                HashCount += aHashCount;
                HashCountSecondaryAlgorithm += aHashCountSecondaryAlgorithm;
                Speed = aHashCount / aRuntime;
                SpeedSecondaryAlgorithm = aHashCountSecondaryAlgorithm / aRuntime;
            }
        }

        protected Miner(OpenCLDevice aDevice, String aAlgorithmName, String aFirstAlgorithmName = "", String aSecondAlgorithmName = "")
        {
            mDevice = aDevice;
            mAlgorithmName = aAlgorithmName;
            mFirstAlgorithmName = (aFirstAlgorithmName == "") ? aAlgorithmName : aFirstAlgorithmName;
            mSecondAlgorithmName = aSecondAlgorithmName;
            Speed = 0;
            SpeedSecondaryAlgorithm = 0;
            MemoryUsage = 0;
        }

        public void Dispose() {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }

        ~Miner()
        {
            Stop();
            WaitForExit(5000);
            Abort();
        }

        public void Start()
        {
            mStopped = false;
            mDone = false;
            UnrecoverableException = null;
        
            MarkAsAlive();
            mMinerThread = new System.Threading.Thread(MinerThread);
            mMinerThread.IsBackground = true;
            mMinerThread.Start();
            mMinerThread.Priority = Parameters.MinerThreadPriority;
        }

        unsafe protected virtual void MinerThread() { }

        public void Stop()
        {
            mStopped = true;
        }

        public void WaitForExit(int ms)
        {
            while (!Done && ms > 0)
            {
                System.Threading.Thread.Sleep((ms < 10) ? ms : 10);
                ms -= 10;
            }
        }

        public void Abort()
        {
            if (mMinerThread != null)
            {
                try
                {
                    mMinerThread.Abort();
                }
                catch (Exception) { }
                mMinerThread = null;
            }
        }

        protected void MarkAsAlive()
        {
            mLastAlive = DateTime.Now;
        }

        protected void MarkAsDone()
        {
            mDone = true;
            Speed = 0;
            SpeedSecondaryAlgorithm = 0;
        }

        public bool Alive
        {
            get {
                if (mMinerThread != null && (DateTime.Now - mLastAlive).TotalSeconds >= 5)
                    Speed = 0;
                return !(mMinerThread != null && (DateTime.Now - mLastAlive).TotalSeconds >= 60);
            }
        }

        public virtual void SetPrimaryStratum(StratumServer stratum) {
            throw new System.InvalidOperationException();
        }
        public virtual void SetSecondaryStratum(StratumServer stratum) {
            throw new System.InvalidOperationException();
        }
    }
}
