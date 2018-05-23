using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography.X509Certificates;



namespace GatelessGateSharp {
    public class StratumServer {
        public class Job {
            private Mutex mMutex = new Mutex();
            static Random r = new Random();
            UInt64 nextLocalExtranonce;
            private StratumServer mStratum;

            public StratumServer Stratum { get { return mStratum; } }

            public Job(StratumServer aStratum) {
                mStratum = aStratum;
                try { mMutex.WaitOne(5000); } catch (Exception) { }
                nextLocalExtranonce = 0;
                for (int i = 0; i < mStratum.LocalExtranonceSize; ++i)
                    nextLocalExtranonce |= (UInt64)r.Next(32, 255) << (i * 8); // TODO
                try { mMutex.ReleaseMutex(); } catch (Exception) { }
            }

            public UInt64 GetNewLocalExtranonce() {
                UInt64 ret;
                try { mMutex.WaitOne(5000); } catch (Exception) { }
                if (mStratum.LocalExtranonceSize == 1) {
                    // Ethash
                    ret = nextLocalExtranonce++;
                } else {
                    // The following restrictions are for Pascal.
                    ret = 0;
                    for (int i = 0; i < mStratum.LocalExtranonceSize; ++i)
                        ret |= (UInt64)r.Next(32, 255) << (i * 8); // TODO
                }
                try { mMutex.ReleaseMutex(); } catch (Exception) { }
                return ret;
            }
        }

        public class Work {
            readonly private Job mJob;
            readonly private UInt64 mLocalExtranonce;

            public UInt64 LocalExtranonce {
                get {
                    return mJob == null ? 0 : // dummy
                           (mJob.Stratum.LocalExtranonceSize == 1) ? (mLocalExtranonce & 0xffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 2) ? (mLocalExtranonce & 0xffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 3) ? (mLocalExtranonce & 0xffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 4) ? (mLocalExtranonce & 0xffffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 5) ? (mLocalExtranonce & 0xffffffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 6) ? (mLocalExtranonce & 0xffffffffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 7) ? (mLocalExtranonce & 0xffffffffffffffUL) :
                                                                     (mLocalExtranonce);
                }
            }
            public string LocalExtranonceString {
                get {
                    return (mJob.Stratum.LocalExtranonceSize == 1) ? String.Format("{0:x2}", LocalExtranonce & 0xffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 2) ? String.Format("{0:x4}", LocalExtranonce & 0xffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 3) ? String.Format("{0:x6}", LocalExtranonce & 0xffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 4) ? String.Format("{0:x8}", LocalExtranonce & 0xffffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 5) ? String.Format("{0:x10}", LocalExtranonce & 0xffffffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 6) ? String.Format("{0:x12}", LocalExtranonce & 0xffffffffffffUL) :
                           (mJob.Stratum.LocalExtranonceSize == 7) ? String.Format("{0:x14}", LocalExtranonce & 0xffffffffffffffUL) :
                                                                     String.Format("{0:x16}", LocalExtranonce);
                }
            }
            public Job GetJob() { return mJob; }

            protected Work(Job aJob) {
                mJob = aJob;
                mLocalExtranonce = (mJob == null) ? 0 : aJob.GetNewLocalExtranonce();
            }
        }

        protected Work GetWork() {
            return null;
        }

        private Mutex mMutex = new Mutex();
        private bool mStopped = false;
        String mServerAddress;
        int mServerPort;
        String mUsername;
        String mPassword;
        protected double mDifficulty = 1.0;
        protected String mPoolExtranonce = "";
        protected String mPoolName = "";
        protected String mAlgorithm = "";
        TcpClient mClient;
        NetworkStream mStream;
        bool mSecureConnection = false;
        SslStream mSecureStream;
        StreamReader mStreamReader;
        StreamWriter mStreamWriter;
        Thread mStreamReaderThread;
        private List<OpenCLDevice> mDevicesWithShare = new List<OpenCLDevice>();
        private List<int> mShareIDs = new List<int>();
        private int mLocalExtranonceSize = 1;
        private bool mReconnectionRequested = false;
        private int mShareCounter = 0;

        public int LocalExtranonceSize {
            get {
                return mLocalExtranonceSize;
            }
            set {
                mLocalExtranonceSize = value;
            }
        }
        public bool Stopped { get { return mStopped; } }
        public String ServerAddress { get { return mServerAddress; } }
        public int ServerPort { get { return mServerPort; } }
        public String Username { get { return mUsername; } }
        public String Password { get { return mPassword; } }
        public String PoolExtranonce { get { return mPoolExtranonce; } }
        public String PoolName { get { return mPoolName; } }
        public String AlgorithmName { get { return mAlgorithm; } }
        public double Difficulty { get { return mDifficulty; } }
        public UnrecoverableException UnrecoverableException { get; set; }
        public bool SilentMode { get; set; }

        protected void ReportSubmittedShare(OpenCLDevice aDevice) {
            int shareID = -1;

            try { mMutex.WaitOne(5000); } catch (Exception) { }
            mDevicesWithShare.Add(aDevice);
            shareID = mShareCounter;
            mShareIDs.Add(mShareCounter++);
            try { mMutex.ReleaseMutex(); } catch (Exception) { }

            if (shareID >= 0 && !MainForm.DevFeeMode) {
                MainForm.Logger("Device #" + aDevice.DeviceIndex + " submitted Share #" + shareID + " to " + ServerAddress + " as " + (Utilities.IsDevFeeAddress(Username) ? "a DEVFEE" : Username) + ".");
            } else {
                MainForm.Logger("Device #" + aDevice.DeviceIndex + " submitted a share to " + ServerAddress + " as " + (Utilities.IsDevFeeAddress(Username) ? "a DEVFEE" : Username) + ".");
            }
        }

        protected void ReportAcceptedShare() {
            int shareID = -1;
            OpenCLDevice device = null;
            
            try { mMutex.WaitOne(5000); } catch (Exception) { }
            if (mDevicesWithShare.Count > 0) {
                device = mDevicesWithShare[0];
                mDevicesWithShare.RemoveAt(0);
                shareID = mShareIDs[0];
                mShareIDs.RemoveAt(0);
            }
            try { mMutex.ReleaseMutex(); } catch (Exception) { }

            if (!MainForm.DevFeeMode) {
                device.IncrementAcceptedShares();
                MainForm.Instance.ReportAcceptedShare();
            }

            if (shareID >= 0 && !MainForm.DevFeeMode) {
                MainForm.Logger("Share #" + shareID + " accepted.");
            } else {
                MainForm.Logger("Share accepted.");
            }
        }

        protected void ReportRejectedShare(string reason = null) {
            int shareID = -1;
            OpenCLDevice device = null;

            try { mMutex.WaitOne(5000); } catch (Exception) { }
            if (mDevicesWithShare.Count > 0) {
                device = mDevicesWithShare[0];
                mDevicesWithShare.RemoveAt(0);
                shareID = mShareIDs[0];
                mShareIDs.RemoveAt(0);
            }
            try { mMutex.ReleaseMutex(); } catch (Exception) { }

            if (!MainForm.DevFeeMode) {
                device.IncrementRejectedShares();
                MainForm.Instance.ReportRejectedShare();
            }

            String message = "Share rejected" + (reason == null ? "." : ": " + reason);
            if (shareID >= 0 && !MainForm.DevFeeMode) {
                 message = "Share #" + shareID + " rejected" + (reason == null ? "." : ": " + reason);

            }
            MainForm.Logger(message);
            if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running)
                throw new UnrecoverableException(message);
        }

        public void Stop() {
            mStopped = true;
        }

        public StratumServer(String aServerAddress, int aServerPort, String aUsername, String aPassword, String aPoolName, String aAlgorithm, bool secure = false) {
            mServerAddress = aServerAddress;
            mServerPort = aServerPort;
            mUsername = aUsername;
            mPassword = aPassword;
            mPoolName = aPoolName + (secure ? " [SSL]" : "");
            mAlgorithm = aAlgorithm;
            SilentMode = false;
            mSecureConnection = secure;

            mStreamReaderThread = new Thread(new ThreadStart(StreamReaderThread));
            mStreamReaderThread.IsBackground = true;
            mStreamReaderThread.Start();
        }

        protected void WriteLine(String line) {
            try { mMutex.WaitOne(5000); } catch (Exception) { }
            mStreamWriter.Write(line);
            mStreamWriter.Write("\n");
            mStreamWriter.Flush();
            try { mMutex.ReleaseMutex(); } catch (Exception) { }
        }

        protected String ReadLine() {
            return mStreamReader.ReadLine();
        }

        protected virtual void Authorize() { }
        protected virtual void ProcessLine(String line) { }

        public void Reconnect() {
            mReconnectionRequested = true;
        }

        private void StreamReaderThread() {
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
            int errorCount = 0;

            UnrecoverableException = null; 
            
            do {
                try {
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    try { mMutex.WaitOne(5000); } catch (Exception) { }

                    mClient = new TcpClient(ServerAddress, ServerPort);
                    mStream = mClient.GetStream();
                    if (mSecureConnection) {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        mSecureStream = new SslStream(
                            mClient.GetStream(), 
                            false, 
                            new RemoteCertificateValidationCallback(
                                (sender, certificate, chain, policyErrors) => {
                                    if ((policyErrors == SslPolicyErrors.None) || (policyErrors == SslPolicyErrors.RemoteCertificateNameMismatch))
                                        return true;
                                    MainForm.Logger(policyErrors.ToString());
                                    return false;
                                }),
                            null,
                            EncryptionPolicy.RequireEncryption);
                        mSecureStream.AuthenticateAsClient("", null, System.Security.Authentication.SslProtocols.Tls12, false);
                        mStreamReader = new StreamReader(mSecureStream, System.Text.Encoding.ASCII, false, 65536);
                        mStreamWriter = new StreamWriter(mSecureStream, System.Text.Encoding.ASCII, 65536);
                    } else {
                        mStreamReader = new StreamReader(mStream, System.Text.Encoding.ASCII, false, 65536);
                        mStreamWriter = new StreamWriter(mStream, System.Text.Encoding.ASCII, 65536);
                    }
                    mStreamReader.BaseStream.ReadTimeout = 3 * 60 * 1000;
                    mStreamWriter.BaseStream.WriteTimeout = 10 * 1000;
                    mReconnectionRequested = false;

                    try { mMutex.ReleaseMutex(); } catch (Exception) { }

                    Authorize();

                    while (!Stopped  && !mReconnectionRequested) {
                        string line;
                        try {
                            line = mStreamReader.ReadLine();
                        } catch (Exception) {
                            throw new StratumServerUnavailableException();
                        }
                        if (line == null)
                            throw new StratumServerUnavailableException();

                        if (Stopped)
                            break;
                        if (line != "")
                            ProcessLine(line);
                        if (sw.ElapsedMilliseconds >= 60 * 60 * 1000)
                            mReconnectionRequested = true;
                    }
                } catch (UnrecoverableException ex) {
                    this.UnrecoverableException = ex;
                } catch (Exception ex) {
                    MainForm.Logger("Exception in Stratum.StreamReaderThread(): " + ex.ToString());
                    if (UnrecoverableException.IsUnrecoverableException(ex)) {
                        this.UnrecoverableException = new UnrecoverableException(ex.Message);
                    } else if (++errorCount < 4) {
                        MainForm.Logger("Reconnecting to the server...");
                        System.Threading.Thread.Sleep(5000);
                    } else {
                        this.UnrecoverableException = new StratumServerUnavailableException();
                    }
                }

                try {
                    mClient.Close();
                    mClient = null;
                } catch (Exception) { }
            } while (!Stopped && UnrecoverableException == null);
        }

        ~StratumServer() {
            if (mStreamReaderThread != null)
                mStreamReaderThread.Abort();
        }
    }
}
