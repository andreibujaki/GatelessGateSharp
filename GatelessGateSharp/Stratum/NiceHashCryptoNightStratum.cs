using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HashLib;


namespace GatelessGateSharp
{
    class NiceHashCryptoNightStratum : Stratum
    {
        public new class Work : Stratum.Work
        {
            private Job mJob;

            public Job GetJob() { return mJob; }

            public Work(Job aJob)
                : base(aJob)
            {
                mJob = aJob;
            }
        }

        public new class Job : Stratum.Job
        {
            String mID;
            String mBlob;
            String mTarget;

            public String ID { get { return mID; } }
            public String Blob { get { return mBlob; } }
            public String Target { get { return mTarget; } }

            public Job(string aID, string aBlob, string aTarget)
            {
                mID = aID;
                mBlob = aBlob;
                mTarget = aTarget;
            }
        }

        Job mJob;
        String mUserID;
        TcpClient mClient;
        NetworkStream mStream;
        StreamReader mStreamReader;
        StreamWriter mStreamWriter;
        Thread mStreamReaderThread;
        string mSubsciptionID;
        private Mutex mMutex = new Mutex();

        public Job GetJob()
        {
            return mJob;
        }

        private void StreamReaderThread()
        {
            try
            {
                while (!Stopped)
                {
                    string line;
                    if ((line = mStreamReader.ReadLine()) == null)
                        throw new Exception("Disconnected from stratum server.");
                    if (Stopped)
                        break;
                    Dictionary<String, Object> response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(line);
                    if (response.ContainsKey("method") && response.ContainsKey("params"))
                    {
                        string method = (string)response["method"];
                        JContainer parameters = (JContainer)response["params"];
                        if (method.Equals("job") && (mJob == null || mJob.ID != (string)parameters["job_id"]))
                        {
                            mMutex.WaitOne();
                            mJob = new Job((string)parameters["job_id"], (string)parameters["blob"], (string)parameters["target"]);
                            mMutex.ReleaseMutex();
                            MainForm.Logger("Received new job: " + parameters["job_id"]);
                        }
                        else
                        {
                            MainForm.Logger("Unknown stratum method: " + line);
                        }
                    }
                    /*
                    else if (response.ContainsKey("id") && response.ContainsKey("result"))
                    {
                        var ID = response["id"];
                        bool result = (bool)response["result"];

                        if (result) {
                            MainForm.Logger("Share #" + ID + " accepted.");
                        } else {
                            MainForm.Logger("Share #" + ID + " rejected: " + (String)(((JArray)response["error"])[1]));
                        }
                    }*/
                    else
                    {
                        MainForm.Logger("Unknown JSON message: " + line);
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.Logger("Failed to receive data from stratum server: " + ex.Message);
            }

            try
            {
                mClient.Close();
            }
            catch (Exception ex) { }

            if (!Stopped)
            {
                MainForm.Logger("Connection terminated. Reconnecting...");
                Thread reconnectThread = new Thread(new ThreadStart(Connect));
                reconnectThread.IsBackground = true;
                reconnectThread.Start();
            }
        }

        override protected void Connect()
        {
            if (Stopped)
                return;

            MainForm.Logger("Connecting to " + ServerAddress + ":" + ServerPort + " as " + Username + "...");

            mMutex.WaitOne();

            mClient = new TcpClient(ServerAddress, ServerPort);
            mStream = mClient.GetStream();
            mStreamReader = new StreamReader(mStream, System.Text.Encoding.ASCII, false);
            mStreamWriter = new StreamWriter(mStream, System.Text.Encoding.ASCII);

            var line = Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, Object> {
                { "method", "login" },
                { "params", new Dictionary<string, string> {
                    { "login", Username },
                    { "pass", "x" },
                    { "agent", MainForm.shortAppName + "/" + MainForm.appVersion}}},
                { "id", 1 }
            });
            mStreamWriter.Write(line);
            mStreamWriter.Write("\n");
            mStreamWriter.Flush();

            line = mStreamReader.ReadLine();
            Dictionary<String, Object> response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(line);
            var result = ((JContainer)response["result"]);
            var status = (String)(result["status"]);
            if (status != "OK")
            {
                mMutex.ReleaseMutex();
                throw new Exception("Authorization failed.");
            }
            mUserID = (String)(result["id"]);
            mJob = new Job((String)(((JContainer)result["job"])["job_id"]), (String)(((JContainer)result["job"])["blob"]), (String)(((JContainer)result["job"])["target"]));

            mMutex.ReleaseMutex();

            mStreamReaderThread = new Thread(new ThreadStart(StreamReaderThread));
            mStreamReaderThread.IsBackground = true;
            mStreamReaderThread.Start();
        }

        public void Submit(Job job, UInt32 output, String result)
        {
            if (Stopped)
                return;

            mMutex.WaitOne();
            try
            {
                String stringNonce = String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", ((output >> 0) & 0xff), ((output >> 8) & 0xff), ((output >> 16) & 0xff), ((output >> 24) & 0xff));
                String message = JsonConvert.SerializeObject(new Dictionary<string, Object> {
                    { "method", "submit" },
                    { "params", new Dictionary<String, String> {
                        { "id", mUserID },
                        { "job_id", job.ID },
                        { "nonce", stringNonce },
                        { "result", result }}},
                    { "id", 4 }});
                mStreamWriter.Write(message + "\n");
                mStreamWriter.Flush();
                MainForm.Logger("message: " + message);
            }
            catch (Exception ex)
            {
                MainForm.Logger("Failed to submit share: " + ex.Message);
            }
            mMutex.ReleaseMutex();
        }

        public new Work GetWork()
        {
            return new Work(mJob);
        }

        public NiceHashCryptoNightStratum(String aServerAddress, int aServerPort, String aUsername, String aPassword, String aPoolName)
            : base(aServerAddress, aServerPort, aUsername, aPassword, aPoolName)
        {
        }
    }
}
