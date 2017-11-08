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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
using System.Runtime.InteropServices;
using Cloo;
using ATI.ADL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace GatelessGateSharp
{
    unsafe public partial class MainForm : Form
    {
        [DllImport("phymem_wrapper.dll")]
        extern public static int LoadPhyMemDriver();
        [DllImport("phymem_wrapper.dll")]
        extern public static void UnloadPhyMemDriver();

        private static MainForm instance;
        public static String shortAppName = "Gateless Gate Sharp";
        public static String appVersion = "0.0.9";
        public static String appName = shortAppName + " " + appVersion + " alpha";
        private static String databaseFileName = "GatelessGateSharp.sqlite";
        private static String logFileName = "GatelessGateSharp.log";
        private static int mLaunchInterval = 500;

        private System.Threading.Mutex loggerMutex = new System.Threading.Mutex();
        private Control[] labelGPUVendorArray;
        private Control[] labelGPUNameArray;
        private Control[] labelGPUIDArray;
        private Control[] labelGPUSpeedArray;
        private Control[] labelGPUTempArray;
        private Control[] labelGPUActivityArray;
        private Control[] labelGPUFanArray;
        private Control[] labelGPUCoreClockArray;
        private Control[] labelGPUMemoryClockArray;
        private Control[] labelGPUSharesArray;
        private CheckBox[] checkBoxGPUEnableArray;
        private TabPage[] tabPageDeviceArray;
        private NumericUpDown[] numericUpDownDeviceEthashIntensityArray;
        private NumericUpDown[] numericUpDownDeviceCryptoNightThreadsArray;
        private NumericUpDown[] numericUpDownDeviceCryptoNightIntensityArray;
        private NumericUpDown[] numericUpDownDeviceCryptoNightLocalWorkSizeArray;

        private Device[] mDevices;
        private const int maxNumDevices = 8; // This depends on MainForm.
        private bool ADLInitialized = false;
        private bool NVMLInitialized = false;
        private Int32[] ADLAdapterIndexArray;
        private System.Threading.Mutex DeviceManagementLibrariesMutex = new System.Threading.Mutex();
        private ManagedCuda.Nvml.nvmlDevice[] nvmlDeviceArray;

        private bool mDevFeeMode = true;
        private int mDevFeePercentage = 1;
        private int mDevFeeDurationInSeconds = 60;
        private String mDevFeeBitcoinAddress = "1BHwDWVerUTiKxhHPf2ubqKKiBMiKQGomZ";
        private DateTime mDevFeeModeStartTime = DateTime.Now; // dummy

        private DateTime mStartTime = DateTime.Now;
        private String mCurrentPool = "NiceHash";

        public static MainForm Instance { get { return instance; } }
        public static bool DevFeeMode { get { return Instance.mDevFeeMode; } }

        private static String sLoggerBuffer = "";

        public static void Logger(String lines)
        {
            lines = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " [" + System.Threading.Thread.CurrentThread.ManagedThreadId + "] " + lines + "\r\n";
            System.Console.Write(lines);
            Instance.loggerMutex.WaitOne();
            sLoggerBuffer += lines;
            Instance.loggerMutex.ReleaseMutex();
        }

        public static void UpdateLog()
        {
            Instance.loggerMutex.WaitOne();
            String loggerBuffer = sLoggerBuffer;
            sLoggerBuffer = "";
            Instance.loggerMutex.ReleaseMutex();

            if (loggerBuffer == "")
                return;

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFileName, true))
                    file.Write(loggerBuffer);

                Instance.richTextBoxLog.SelectionLength = 0;
                Instance.richTextBoxLog.SelectionStart = Instance.richTextBoxLog.Text.Length;
                Instance.richTextBoxLog.ScrollToCaret();
                Instance.richTextBoxLog.Text += loggerBuffer;
                Instance.richTextBoxLog.SelectionLength = 0;
                Instance.richTextBoxLog.SelectionStart = Instance.richTextBoxLog.Text.Length;
                Instance.richTextBoxLog.ScrollToCaret();
            }
            catch (Exception ex) { }
        }

        unsafe public MainForm()
        {
            instance = this;

            InitializeComponent();
        }

        private void CreateNewDatabase()
        {
            SQLiteConnection.CreateFile(databaseFileName);
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + databaseFileName + ";Version=3;"))
            {
                conn.Open();
                String sql = "create table wallet_addresses (coin varchar(128), address varchar(128));";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                sql = "create table pools (name varchar(128));";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                sql = "create table properties (name varchar(128), value varchar(128));";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                sql = "create table device_parameters (device_id int, device_vendor varchar(128), device_name varchar(128), parameter_name varchar(128), parameter_value varchar(128));";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }
                conn.Close();
            }
        }

        private void LoadDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + databaseFileName + ";Version=3;"))
            {
                conn.Open();
                String sql = "select * from wallet_addresses";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            if ((String)reader["coin"] == "bitcoin")
                            {
                                textBoxBitcoinAddress.Text = (String)reader["address"];
                            }
                            else if ((String)reader["coin"] == "ethereum")
                            {
                                textBoxEthereumAddress.Text = (String)reader["address"];
                            }
                            else if ((String)reader["coin"] == "monero")
                            {
                                textBoxMoneroAddress.Text = (String)reader["address"];
                            }
                            else if ((String)reader["coin"] == "zcash")
                            {
                                textBoxZcashAddress.Text = (String)reader["address"];
                            }
                        }
                    }
                }



                try
                {
                    sql = "select * from pools";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            List<String> oldItems = new List<string>();
                            foreach (String poolName in listBoxPoolPriorities.Items)
                                oldItems.Add(poolName);
                            listBoxPoolPriorities.Items.Clear();
                            while (reader.Read())
                                listBoxPoolPriorities.Items.Add((String)reader["name"]);
                            foreach (String poolName in oldItems)
                                if (!listBoxPoolPriorities.Items.Contains(poolName))
                                    listBoxPoolPriorities.Items.Add(poolName);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                }

                try
                {
                    sql = "select * from properties";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                String propertyName = (String)reader["name"];
                                if (propertyName == "coin_to_mine")
                                {
                                    String coinToMine = (String)reader["value"];
                                    if (coinToMine == "ethereum")
                                    {
                                        radioButtonEthereum.Checked = true;
                                        radioButtonMonero.Checked = false;
                                        radioButtonZcash.Checked = false;
                                    }
                                    else if (coinToMine == "monero")
                                    {
                                        radioButtonEthereum.Checked = false;
                                        radioButtonMonero.Checked = true;
                                        radioButtonZcash.Checked = false;
                                    }
                                    else if (coinToMine == "zcash")
                                    {
                                        radioButtonEthereum.Checked = false;
                                        radioButtonMonero.Checked = false;
                                        radioButtonZcash.Checked = true;
                                    }
                                    else
                                    {
                                        radioButtonEthereum.Checked = true;
                                        radioButtonMonero.Checked = false;
                                        radioButtonZcash.Checked = false;
                                    }
                                }
                                else if (propertyName == "pool_rig_id")
                                {
                                    textBoxRigID.Text = (String)reader["value"];
                                }
                                else if (propertyName == "pool_email")
                                {
                                    textBoxEmail.Text = (String)reader["value"];
                                }
                                else if (propertyName == "pool_login")
                                {
                                    textBoxLogin.Text = (String)reader["value"];
                                }
                                else if (propertyName == "pool_password")
                                {
                                    textBoxPassword.Text = (String)reader["value"];
                                }
                                else if (propertyName == "auto_start")
                                {
                                    checkBoxAutoStart.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "launch_at_startup")
                                {
                                    checkBoxLaunchAtStartup.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu0")
                                {
                                    checkBoxGPU0Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu1")
                                {
                                    checkBoxGPU1Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu2")
                                {
                                    checkBoxGPU2Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu3")
                                {
                                    checkBoxGPU3Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu4")
                                {
                                    checkBoxGPU4Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu5")
                                {
                                    checkBoxGPU5Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu6")
                                {
                                    checkBoxGPU6Enable.Checked = ((String)reader["value"] == "true");
                                }
                                else if (propertyName == "enable_gpu7")
                                {
                                    checkBoxGPU7Enable.Checked = ((String)reader["value"] == "true");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                }

                try
                {
                    sql = "select * from device_parameters";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int deviceID = (int)reader["device_id"];
                                String deviceVendor = (String)reader["device_vendor"];
                                String deviceName = (String)reader["device_name"];
                                String name = (String)reader["parameter_name"];
                                String value = (String)reader["parameter_value"];
                                if (deviceID >= mDevices.Length || deviceVendor != mDevices[deviceID].Vendor || deviceName != mDevices[deviceID].Name)
                                    continue;
                                if (name == "ethash_intensity")
                                {
                                    numericUpDownDeviceEthashIntensityArray[deviceID].Value = Decimal.Parse(value);
                                }
                                else if (name == "cryptonight_threads")
                                {
                                    numericUpDownDeviceCryptoNightThreadsArray[deviceID].Value = Decimal.Parse(value);
                                }
                                else if (name == "cryptonight_intensity")
                                {
                                    numericUpDownDeviceCryptoNightIntensityArray[deviceID].Value = Decimal.Parse(value);
                                }
                                else if (name == "cryptonight_local_work_size")
                                {
                                    numericUpDownDeviceCryptoNightLocalWorkSizeArray[deviceID].Value = Decimal.Parse(value);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                }

                conn.Close();
            }
        }

        private void UpdateDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + databaseFileName + ";Version=3;"))
            {
                conn.Open();
                String sql = "delete from wallet_addresses";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    command.ExecuteNonQuery();

                sql = "insert into wallet_addresses (coin, address) values (@coin, @address)";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@coin", "bitcoin");
                    command.Parameters.AddWithValue("@address", textBoxBitcoinAddress.Text);
                    command.ExecuteNonQuery();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@coin", "ethereum");
                    command.Parameters.AddWithValue("@address", textBoxEthereumAddress.Text);
                    command.ExecuteNonQuery();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@coin", "monero");
                    command.Parameters.AddWithValue("@address", textBoxMoneroAddress.Text);
                    command.ExecuteNonQuery();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@coin", "zcash");
                    command.Parameters.AddWithValue("@address", textBoxZcashAddress.Text);
                    command.ExecuteNonQuery();
                }

                try
                {
                    sql = "delete from pools";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                        command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                    sql = "create table pools (name varchar(128));";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                        command.ExecuteNonQuery();
                }

                sql = "insert into pools (name) values (@name)";
                foreach (String poolName in listBoxPoolPriorities.Items)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@name", poolName);
                        command.ExecuteNonQuery();
                    }
                }

                try
                {
                    sql = "delete from properties";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                        command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                    sql = "create table properties (name varchar(128), value varchar(128));";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                        command.ExecuteNonQuery();
                }

                sql = "insert into properties (name, value) values (@name, @value)";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "coin_to_mine");
                    command.Parameters.AddWithValue("@value",
                                                    (radioButtonEthereum.Checked) ? "ethereum" :
                                                    (radioButtonMonero.Checked) ? "monero" :
                                                    (radioButtonMonero.Checked) ? "zcash" :
                                                                                    "most_profitable");
                    command.ExecuteNonQuery();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "pool_rig_id");
                    command.Parameters.AddWithValue("@value", textBoxRigID.Text);
                    command.ExecuteNonQuery();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "pool_email");
                    command.Parameters.AddWithValue("@value", textBoxEmail.Text);
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "pool_login");
                    command.Parameters.AddWithValue("@value", textBoxLogin.Text);
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "auto_start");
                    command.Parameters.AddWithValue("@value", (checkBoxAutoStart.Checked ? "true" : "false"));
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "launch_at_startup");
                    command.Parameters.AddWithValue("@value", (checkBoxLaunchAtStartup.Checked ? "true" : "false"));
                    command.ExecuteNonQuery();
                }

                for (int i = 0; i < mDevices.Length; ++i)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@name", "enable_gpu" + i);
                        command.Parameters.AddWithValue("@value", (checkBoxGPUEnableArray[i].Checked ? "true" : "false"));
                        command.ExecuteNonQuery();
                    }

                }

                sql = "insert into properties (name, value) values (@name, @value)";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "coin_to_mine");
                    command.Parameters.AddWithValue("@value",
                                                    (radioButtonEthereum.Checked) ? "ethereum" :
                                                    (radioButtonMonero.Checked) ? "monero" :
                                                    (radioButtonMonero.Checked) ? "zcash" :
                                                                                    "most_profitable");
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "pool_rig_id");
                    command.Parameters.AddWithValue("@value", textBoxRigID.Text);
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "pool_email");
                    command.Parameters.AddWithValue("@value", textBoxEmail.Text);
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "pool_login");
                    command.Parameters.AddWithValue("@value", textBoxLogin.Text);
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "auto_start");
                    command.Parameters.AddWithValue("@value", (checkBoxAutoStart.Checked ? "true" : "false"));
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", "launch_at_startup");
                    command.Parameters.AddWithValue("@value", (checkBoxLaunchAtStartup.Checked ? "true" : "false"));
                    command.ExecuteNonQuery();
                }

                for (int i = 0; i < mDevices.Length; ++i)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@name", "enable_gpu" + i);
                        command.Parameters.AddWithValue("@value", (checkBoxGPUEnableArray[i].Checked ? "true" : "false"));
                        command.ExecuteNonQuery();
                    }

                }

                try
                {
                    sql = "delete from device_parameters";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                        command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                    sql = "create table device_parameters (device_id int, device_vendor varchar(128), device_name varchar(128), parameter_name varchar(128), parameter_value varchar(128));";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                        command.ExecuteNonQuery();
                }

                sql = "insert into device_parameters (device_id, device_vendor, device_name, parameter_name, parameter_value) values (@device_id, @device_vendor, @device_name, @parameter_name, @parameter_value)";
                for (int i = 0; i < mDevices.Length; ++i)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@device_id", i);
                        command.Parameters.AddWithValue("@device_vendor", mDevices[i].Vendor);
                        command.Parameters.AddWithValue("@device_name", mDevices[i].Name);
                        command.Parameters.AddWithValue("@parameter_name", "ethash_intensity");
                        command.Parameters.AddWithValue("@parameter_value", numericUpDownDeviceEthashIntensityArray[i].Value.ToString());
                        command.ExecuteNonQuery();
                    }


                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@device_id", i);
                        command.Parameters.AddWithValue("@device_vendor", mDevices[i].Vendor);
                        command.Parameters.AddWithValue("@device_name", mDevices[i].Name);
                        command.Parameters.AddWithValue("@parameter_name", "cryptonight_threads");
                        command.Parameters.AddWithValue("@parameter_value", numericUpDownDeviceCryptoNightThreadsArray[i].Value.ToString());
                        command.ExecuteNonQuery();
                    }


                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@device_id", i);
                        command.Parameters.AddWithValue("@device_vendor", mDevices[i].Vendor);
                        command.Parameters.AddWithValue("@device_name", mDevices[i].Name);
                        command.Parameters.AddWithValue("@parameter_name", "cryptonight_intensity");
                        command.Parameters.AddWithValue("@parameter_value", numericUpDownDeviceCryptoNightIntensityArray[i].Value.ToString());
                        command.ExecuteNonQuery();
                    }


                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@device_id", i);
                        command.Parameters.AddWithValue("@device_vendor", mDevices[i].Vendor);
                        command.Parameters.AddWithValue("@device_name", mDevices[i].Name);
                        command.Parameters.AddWithValue("@parameter_name", "cryptonight_local_work_size");
                        command.Parameters.AddWithValue("@parameter_value", numericUpDownDeviceCryptoNightLocalWorkSizeArray[i].Value.ToString());
                        command.ExecuteNonQuery();
                    }

                }

                conn.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Logger(appName + " started.");
            labelGPUVendorArray = new Control[] { labelGPU0Vendor, labelGPU1Vendor, labelGPU2Vendor, labelGPU3Vendor, labelGPU4Vendor, labelGPU5Vendor, labelGPU6Vendor, labelGPU7Vendor };
            labelGPUNameArray = new Control[] { labelGPU0Name, labelGPU1Name, labelGPU2Name, labelGPU3Name, labelGPU4Name, labelGPU5Name, labelGPU6Name, labelGPU7Name };
            labelGPUIDArray = new Control[] { labelGPU0ID, labelGPU1ID, labelGPU2ID, labelGPU3ID, labelGPU4ID, labelGPU5ID, labelGPU6ID, labelGPU7ID };
            labelGPUTempArray = new Control[] { labelGPU0Temp, labelGPU1Temp, labelGPU2Temp, labelGPU3Temp, labelGPU4Temp, labelGPU5Temp, labelGPU6Temp, labelGPU7Temp };
            labelGPUActivityArray = new Control[] { labelGPU0Activity, labelGPU1Activity, labelGPU2Activity, labelGPU3Activity, labelGPU4Activity, labelGPU5Activity, labelGPU6Activity, labelGPU7Activity };
            labelGPUFanArray = new Control[] { labelGPU0Fan, labelGPU1Fan, labelGPU2Fan, labelGPU3Fan, labelGPU4Fan, labelGPU5Fan, labelGPU6Fan, labelGPU7Fan };
            labelGPUSpeedArray = new Control[] { labelGPU0Speed, labelGPU1Speed, labelGPU2Speed, labelGPU3Speed, labelGPU4Speed, labelGPU5Speed, labelGPU6Speed, labelGPU7Speed };
            labelGPUCoreClockArray = new Control[] { labelGPU0CoreClock, labelGPU1CoreClock, labelGPU2CoreClock, labelGPU3CoreClock, labelGPU4CoreClock, labelGPU5CoreClock, labelGPU6CoreClock, labelGPU7CoreClock };
            labelGPUMemoryClockArray = new Control[] { labelGPU0MemoryClock, labelGPU1MemoryClock, labelGPU2MemoryClock, labelGPU3MemoryClock, labelGPU4MemoryClock, labelGPU5MemoryClock, labelGPU6MemoryClock, labelGPU7MemoryClock };
            labelGPUSharesArray = new Control[] { labelGPU0Shares, labelGPU1Shares, labelGPU2Shares, labelGPU3Shares, labelGPU4Shares, labelGPU5Shares, labelGPU6Shares, labelGPU7Shares };
            checkBoxGPUEnableArray = new CheckBox[] { checkBoxGPU0Enable, checkBoxGPU1Enable, checkBoxGPU2Enable, checkBoxGPU3Enable, checkBoxGPU4Enable, checkBoxGPU5Enable, checkBoxGPU6Enable, checkBoxGPU7Enable };
            tabPageDeviceArray = new TabPage[] { tabPageDevice0, tabPageDevice1, tabPageDevice2, tabPageDevice3, tabPageDevice4, tabPageDevice5, tabPageDevice6, tabPageDevice7 };
            numericUpDownDeviceEthashIntensityArray = new NumericUpDown[] { numericUpDownDevice0EthashIntensity, numericUpDownDevice1EthashIntensity, numericUpDownDevice2EthashIntensity, numericUpDownDevice3EthashIntensity, numericUpDownDevice4EthashIntensity, numericUpDownDevice5EthashIntensity, numericUpDownDevice6EthashIntensity, numericUpDownDevice7EthashIntensity };
            numericUpDownDeviceCryptoNightThreadsArray = new NumericUpDown[] { numericUpDownDevice0CryptoNightThreads, numericUpDownDevice1CryptoNightThreads, numericUpDownDevice2CryptoNightThreads, numericUpDownDevice3CryptoNightThreads, numericUpDownDevice4CryptoNightThreads, numericUpDownDevice5CryptoNightThreads, numericUpDownDevice6CryptoNightThreads, numericUpDownDevice7CryptoNightThreads };
            numericUpDownDeviceCryptoNightIntensityArray = new NumericUpDown[] { numericUpDownDevice0CryptoNightIntensity, numericUpDownDevice1CryptoNightIntensity, numericUpDownDevice2CryptoNightIntensity, numericUpDownDevice3CryptoNightIntensity, numericUpDownDevice4CryptoNightIntensity, numericUpDownDevice5CryptoNightIntensity, numericUpDownDevice6CryptoNightIntensity, numericUpDownDevice7CryptoNightIntensity };
            numericUpDownDeviceCryptoNightLocalWorkSizeArray = new NumericUpDown[] { numericUpDownDevice0CryptoNightLocalWorkSize, numericUpDownDevice1CryptoNightLocalWorkSize, numericUpDownDevice2CryptoNightLocalWorkSize, numericUpDownDevice3CryptoNightLocalWorkSize, numericUpDownDevice4CryptoNightLocalWorkSize, numericUpDownDevice5CryptoNightLocalWorkSize, numericUpDownDevice6CryptoNightLocalWorkSize, numericUpDownDevice7CryptoNightLocalWorkSize };

            if (LoadPhyMemDriver() != 0)
            {
                Logger("Successfully loaded phymem.");
            }
            else
            {
                Logger("Failed to load phymem.");
                MessageBox.Show("Failed to load phymem.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }

            InitializeDevices();
            if (!System.IO.File.Exists(databaseFileName))
                CreateNewDatabase();
            LoadDatabase();

            Text = appName; // Set the window title.

            // Do everything to turn off TDR.
            foreach (String controlSet in new String[] { "CurrentControlSet", "ControlSet001" })
            { // This shouldn't be necessary but it doesn't work without this.
                foreach (String path in new String[] { 
                    @"HKEY_LOCAL_MACHINE\System\" + controlSet + @"\Control\GraphicsDrivers",
                    @"HKEY_LOCAL_MACHINE\System\" + controlSet + @"\Control\GraphicsDrivers\TdrWatch"
                })
                {
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrLevel", 0); }
                    catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrDelay", 60); }
                    catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrDdiDelay", 60); }
                    catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrLimitTime", 60); }
                    catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrLimitCount", 256); }
                    catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TDR_RECOVERY", 0); }
                    catch (Exception) { } // Undocumented but found on Windows 10.
                }
            }

            if (checkBoxAutoStart.Checked)
                buttonStart_Click(null, null);
        }

        private void InitializeDevices()
        {
            ArrayList computeDeviceArrayList = new ArrayList();

            foreach (ComputePlatform platform in ComputePlatform.Platforms)
            {
                IList<ComputeDevice> openclDevices = platform.Devices;
                ComputeContextPropertyList properties = new ComputeContextPropertyList(platform);
                using (ComputeContext context = new ComputeContext(openclDevices, properties, null, IntPtr.Zero))
                {
                    foreach (ComputeDevice openclDevice in context.Devices)
                    {
                        if (openclDevice.Vendor == "Intel(R) Corporation"
                            || openclDevice.Vendor == "GenuineIntel"
                            || openclDevice.Type == ComputeDeviceTypes.Cpu)
                            continue;
                        computeDeviceArrayList.Add(openclDevice);
                    }
                }

            }
            ComputeDevice[] computeDevices = Array.ConvertAll(computeDeviceArrayList.ToArray(), item => (Cloo.ComputeDevice)item);
            mDevices = new Device[computeDevices.Length];
            int deviceIndex = 0;
            foreach (var computeDevice in computeDevices)
            {
                mDevices[deviceIndex] = new Device(deviceIndex, computeDevice);
                deviceIndex++;
            }
            Logger("Number of Devices: " + mDevices.Length);

            foreach (Device device in mDevices)
            {
                ComputeDevice openclDevice = device.GetComputeDevice();
                int index = device.DeviceIndex;
                labelGPUVendorArray[index].Text = device.Vendor;
                labelGPUNameArray[index].Text = openclDevice.Name;

                labelGPUSpeedArray[index].Text = "-";
                labelGPUActivityArray[index].Text = "-";
                labelGPUTempArray[index].Text = "-";
                labelGPUFanArray[index].Text = "-";
                labelGPUSharesArray[index].Text = "-";
            }

            for (int index = mDevices.Length; index < maxNumDevices; ++index)
            {
                labelGPUVendorArray[index].Visible = false;
                labelGPUNameArray[index].Visible = false;
                labelGPUIDArray[index].Visible = false;
                labelGPUSpeedArray[index].Visible = false;
                labelGPUActivityArray[index].Visible = false;
                labelGPUTempArray[index].Visible = false;
                labelGPUFanArray[index].Visible = false;
                labelGPUCoreClockArray[index].Visible = false;
                labelGPUMemoryClockArray[index].Visible = false;
                labelGPUSharesArray[index].Visible = false;
                checkBoxGPUEnableArray[index].Visible = false;
            }

            int ADLRet = -1;
            int NumberOfAdapters = 0;
            ADLAdapterIndexArray = new Int32[mDevices.Length];
            for (int i = 0; i < mDevices.Length; i++)
                ADLAdapterIndexArray[i] = -1;
            if (null != ADL.ADL_Main_Control_Create)
                ADLRet = ADL.ADL_Main_Control_Create(ADL.ADL_Main_Memory_Alloc, 1);
            if (ADL.ADL_SUCCESS == ADLRet)
            {
                Logger("Successfully initialized AMD Display Library.");
                ADLInitialized = true;
                if (null != ADL.ADL_Adapter_NumberOfAdapters_Get)
                {
                    ADL.ADL_Adapter_NumberOfAdapters_Get(ref NumberOfAdapters);
                }
                Logger("Number of ADL Adapters: " + NumberOfAdapters.ToString());

                if (0 < NumberOfAdapters)
                {
                    ADLAdapterInfoArray OSAdapterInfoData;
                    OSAdapterInfoData = new ADLAdapterInfoArray();

                    if (null != ADL.ADL_Adapter_AdapterInfo_Get)
                    {
                        IntPtr AdapterBuffer = IntPtr.Zero;
                        int size = Marshal.SizeOf(OSAdapterInfoData);
                        AdapterBuffer = Marshal.AllocCoTaskMem((int)size);
                        Marshal.StructureToPtr(OSAdapterInfoData, AdapterBuffer, false);

                        if (null != ADL.ADL_Adapter_AdapterInfo_Get)
                        {
                            ADLRet = ADL.ADL_Adapter_AdapterInfo_Get(AdapterBuffer, size);
                            if (ADL.ADL_SUCCESS == ADLRet)
                            {
                                OSAdapterInfoData = (ADLAdapterInfoArray)Marshal.PtrToStructure(AdapterBuffer, OSAdapterInfoData.GetType());
                                int IsActive = 0;

                                //int deviceIndex = 0;
                                deviceIndex = 0;
                                foreach (var device in mDevices)
                                {
                                    ComputeDevice openclDevice = device.GetComputeDevice();
                                    if (openclDevice.Vendor == "Advanced Micro Devices, Inc.")
                                    {
                                        ComputeDevice.cl_device_topology_amd topology = openclDevice.TopologyAMD;
                                        for (int i = 0; i < NumberOfAdapters; i++)
                                        {
                                            if (null != ADL.ADL_Adapter_Active_Get)
                                                ADLRet = ADL.ADL_Adapter_Active_Get(OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex, ref IsActive);
                                            if (OSAdapterInfoData.ADLAdapterInfo[i].BusNumber == topology.bus
                                                && (ADLAdapterIndexArray[deviceIndex] < 0 || IsActive != 0))
                                            {
                                                ADLAdapterIndexArray[deviceIndex] = OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex;
                                                device.SetADLName(OSAdapterInfoData.ADLAdapterInfo[i].AdapterName);
                                                labelGPUNameArray[deviceIndex].Text = device.Name;
                                            }
                                        }
                                    }
                                    ++deviceIndex;
                                }
                            }
                            else
                            {
                                Logger("ADL_Adapter_AdapterInfo_Get() returned error code " + ADLRet.ToString());
                            }
                        }
                        // Release the memory for the AdapterInfo structure
                        if (IntPtr.Zero != AdapterBuffer)
                            Marshal.FreeCoTaskMem(AdapterBuffer);
                    }
                }
            }
            else
            {
                Logger("Failed to initialize AMD Display Library.");
            }

            try
            {
                if (ManagedCuda.Nvml.NvmlNativeMethods.nvmlInit() == 0)
                {
                    Logger("Successfully initialized NVIDIA Management Library.");
                    uint nvmlDeviceCount = 0;
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetCount(ref nvmlDeviceCount);
                    Logger("NVML Device Count: " + nvmlDeviceCount);

                    nvmlDeviceArray = new ManagedCuda.Nvml.nvmlDevice[mDevices.Length];
                    for (uint i = 0; i < nvmlDeviceCount; ++i)
                    {
                        ManagedCuda.Nvml.nvmlDevice nvmlDevice = new ManagedCuda.Nvml.nvmlDevice();
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetHandleByIndex(i, ref nvmlDevice);
                        ManagedCuda.Nvml.nvmlPciInfo info = new ManagedCuda.Nvml.nvmlPciInfo();
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetPciInfo(nvmlDevice, ref info);

                        uint j;
                        for (j = 0; j < mDevices.Length; ++j)
                        {
                            if (mDevices[j].GetComputeDevice().Vendor == "NVIDIA Corporation" && mDevices[j].GetComputeDevice().PciBusIdNV == info.bus)
                            {
                                nvmlDeviceArray[j] = nvmlDevice;
                                break;
                            }
                        }
                        if (j >= mDevices.Length)
                            throw new Exception();
                    }

                    NVMLInitialized = true;
                }
            }
            catch (Exception ex)
            {
            }
            if (!NVMLInitialized)
            {
                Logger("Failed to initialize NVIDIA Management Library.");
            }
            else
            {
            }

            foreach (Device device in mDevices)
            {
                tabPageDeviceArray[device.DeviceIndex].Text = "#" + device.DeviceIndex + ": " + device.Vendor + " " + device.Name;

                // Ethash
                numericUpDownDeviceEthashIntensityArray[device.DeviceIndex].Value = (decimal)2000;

                // CryptoNight
                numericUpDownDeviceCryptoNightThreadsArray[device.DeviceIndex].Value = (decimal)2;
                numericUpDownDeviceCryptoNightLocalWorkSizeArray[device.DeviceIndex].Value = (decimal)(device.Vendor == "AMD" ? 8 : 4);
                numericUpDownDeviceCryptoNightIntensityArray[device.DeviceIndex].Value
                    = (decimal)((device.Vendor == "AMD" && device.Name == "Radeon RX 470") ? (24) :
                                (device.Vendor == "AMD" && device.Name == "Radeon RX 570") ? (24) :
                                (device.Vendor == "AMD" && device.Name == "Radeon RX 480") ? (32) :
                                (device.Vendor == "AMD" && device.Name == "Radeon RX 580") ? (32) :
                                (device.Vendor == "AMD" && device.Name == "Radeon R9 Fury X/Nano") ? (14) :
                                (device.Vendor == "AMD") ? (16) :
                                (device.Vendor == "NVIDIA" && device.Name == "GeForce GTX 1080 Ti") ? (64) :
                                                                                                      (32));
            }

            UpdateStatsWithShortPolling();
            timerDeviceStatusUpdates.Enabled = true;
            UpdateStatsWithLongPolling();
            timerCurrencyStatUpdates.Enabled = true;
        }

        private class CustomWebClient : System.Net.WebClient
        {
            protected override System.Net.WebRequest GetWebRequest(Uri uri)
            {
                System.Net.WebRequest request = base.GetWebRequest(uri);
                request.Timeout = 1 * 1000;
                return request;
            }
        }

        private void UpdateStatsWithLongPolling()
        {
            try
            {
                double totalSpeed = 0;
                if (mMiners != null)
                    foreach (Miner miner in mMiners)
                        totalSpeed += miner.Speed;
                labelCurrentSpeed.Text = (appState != ApplicationGlobalState.Mining) ? "-" : String.Format("{0:N2} Mh/s", totalSpeed / 1000000);

                var client = new CustomWebClient();
                double USDBTC = 0;
                {
                    String jsonString = client.DownloadString("https://blockchain.info/ticker");
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var USD = (JContainer)(response["USD"]);
                    USDBTC = (double)(USD["15m"]);
                }

                double USDETH = 0.0;
                double USDXMR = 0.0;
                {
                    String jsonString = client.DownloadString("https://api.coinmarketcap.com/v1/ticker/?convert=USD");
                    var responseArray = JsonConvert.DeserializeObject<JArray>(jsonString);
                    foreach (JContainer currency in responseArray)
                    {
                        if ((String)currency["id"] == "ethereum")
                            USDETH = Double.Parse((String)currency["price_usd"]);
                        if ((String)currency["id"] == "monero")
                            USDXMR = Double.Parse((String)currency["price_usd"]);
                    }
                }

                if (mCurrentPool == "NiceHash" && radioButtonEthereum.Checked && textBoxBitcoinAddress.Text != "")
                {
                    double balance = 0;
                    String jsonString = client.DownloadString("https://api.nicehash.com/api?method=stats.provider&addr=" + textBoxBitcoinAddress.Text);
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var result = (JContainer)(response["result"]);
                    var stats = (JArray)(result["stats"]);
                    foreach (JContainer item in stats)
                        balance += Double.Parse((String)item["balance"]);
                    labelBalance.Text = String.Format("{0:N6}", balance) + " BTC (" + String.Format("{0:N2}", (balance * USDBTC)) + " USD)";

                    if (appState == ApplicationGlobalState.Mining && textBoxBitcoinAddress.Text != "")
                    {
                        double price = 0;
                        jsonString = client.DownloadString("https://api.nicehash.com/api?method=stats.global.current");
                        response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                        result = (JContainer)(response["result"]);
                        stats = (JArray)(result["stats"]);
                        foreach (JContainer item in stats)
                            if ((double)item["algo"] == 20)
                                price = Double.Parse((String)item["price"]) * totalSpeed / 1000000000.0;

                        labelPriceDay.Text = String.Format("{0:N6}", price) + " BTC/Day (" + String.Format("{0:N2}", (price * USDBTC)) + " USD/Day)";
                        labelPriceWeek.Text = String.Format("{0:N6}", price * 7) + " BTC/Week (" + String.Format("{0:N2}", (price * 7 * USDBTC)) + " USD/Week)";
                        labelPriceMonth.Text = String.Format("{0:N6}", price * (365.25 / 12)) + " BTC/Month (" + String.Format("{0:N2}", (price * (365.25 / 12) * USDBTC)) + " USD/Month)";
                    }
                    else
                    {
                        labelPriceDay.Text = "-";
                        labelPriceWeek.Text = "-";
                        labelPriceMonth.Text = "-";
                    }
                }
                if (mCurrentPool == "NiceHash" && radioButtonMonero.Checked && textBoxBitcoinAddress.Text != "")
                {
                    double balance = 0;
                    String jsonString = client.DownloadString("https://api.nicehash.com/api?method=stats.provider&addr=" + textBoxBitcoinAddress.Text);
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var result = (JContainer)(response["result"]);
                    var stats = (JArray)(result["stats"]);
                    foreach (JContainer item in stats)
                        balance += Double.Parse((String)item["balance"]);
                    labelBalance.Text = String.Format("{0:N6}", balance) + " BTC (" + String.Format("{0:N2}", (balance * USDBTC)) + " USD)";

                    if (appState == ApplicationGlobalState.Mining && textBoxBitcoinAddress.Text != "")
                    {
                        double price = 0;
                        jsonString = client.DownloadString("https://api.nicehash.com/api?method=stats.global.current");
                        response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                        result = (JContainer)(response["result"]);
                        stats = (JArray)(result["stats"]);
                        foreach (JContainer item in stats)
                            if ((double)item["algo"] == 22)
                                price = Double.Parse((String)item["price"]) * totalSpeed / 1000000.0;

                        labelPriceDay.Text = String.Format("{0:N6}", price) + " BTC/Day (" + String.Format("{0:N2}", (price * USDBTC)) + " USD/Day)";
                        labelPriceWeek.Text = String.Format("{0:N6}", price * 7) + " BTC/Week (" + String.Format("{0:N2}", (price * 7 * USDBTC)) + " USD/Week)";
                        labelPriceMonth.Text = String.Format("{0:N6}", price * (365.25 / 12)) + " BTC/Month (" + String.Format("{0:N2}", (price * (365.25 / 12) * USDBTC)) + " USD/Month)";
                    }
                    else
                    {
                        labelPriceDay.Text = "-";
                        labelPriceWeek.Text = "-";
                        labelPriceMonth.Text = "-";
                    }
                }
                else if (mCurrentPool == "ethermine.org" && radioButtonEthereum.Checked && textBoxEthereumAddress.Text != "")
                {
                    String jsonString = client.DownloadString("https://api.ethermine.org/miner/" + textBoxEthereumAddress.Text + "/currentStats");
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var data = (JContainer)(response["data"]);
                    double balance = (double)data["unpaid"] * 1e-18;
                    double averageHashrate = (double)data["averageHashrate"];
                    double coinsPerMin = (double)data["coinsPerMin"];
                    labelBalance.Text = String.Format("{0:N6}", balance) + " ETH (" + String.Format("{0:N2}", (balance * USDETH)) + " USD)";

                    if (appState == ApplicationGlobalState.Mining && averageHashrate != 0)
                    {
                        double price = (coinsPerMin * 60 * 24) * (totalSpeed / averageHashrate);

                        labelPriceDay.Text = String.Format("{0:N6}", price) + " ETH/Day (" + String.Format("{0:N2}", (price * USDETH)) + " USD/Day)";
                        labelPriceWeek.Text = String.Format("{0:N6}", price * 7) + " ETH/Week (" + String.Format("{0:N2}", (price * 7 * USDETH)) + " USD/Week)";
                        labelPriceMonth.Text = String.Format("{0:N6}", price * (365.25 / 12)) + " ETH/Month (" + String.Format("{0:N2}", (price * (365.25 / 12) * USDETH)) + " USD/Month)";
                    }
                    else
                    {
                        labelPriceDay.Text = "-";
                        labelPriceWeek.Text = "-";
                        labelPriceMonth.Text = "-";
                    }
                }
                else if (mCurrentPool == "ethpool.org" && radioButtonEthereum.Checked && textBoxEthereumAddress.Text != "")
                {
                    String jsonString = client.DownloadString("http://api.ethpool.org/miner/" + textBoxEthereumAddress.Text + "/currentStats");
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var data = (JContainer)(response["data"]);
                    double balance = 0;
                    try
                    {
                        balance = (double)data["unpaid"] * 1e-18;
                    }
                    catch (Exception ex) { }
                    double averageHashrate = (double)data["averageHashrate"];
                    double coinsPerMin = (double)data["coinsPerMin"];
                    labelBalance.Text = String.Format("{0:N6}", balance) + " ETH (" + String.Format("{0:N2}", (balance * USDETH)) + " USD)";

                    if (appState == ApplicationGlobalState.Mining && averageHashrate != 0)
                    {
                        double price = (coinsPerMin * 60 * 24) * (totalSpeed / averageHashrate);

                        labelPriceDay.Text = String.Format("{0:N6}", price) + " ETH/Day (" + String.Format("{0:N2}", (price * USDETH)) + " USD/Day)";
                        labelPriceWeek.Text = String.Format("{0:N6}", price * 7) + " ETH/Week (" + String.Format("{0:N2}", (price * 7 * USDETH)) + " USD/Week)";
                        labelPriceMonth.Text = String.Format("{0:N6}", price * (365.25 / 12)) + " ETH/Month (" + String.Format("{0:N2}", (price * (365.25 / 12) * USDETH)) + " USD/Month)";
                    }
                    else
                    {
                        labelPriceDay.Text = "-";
                        labelPriceWeek.Text = "-";
                        labelPriceMonth.Text = "-";
                    }
                }
                else if (mCurrentPool == "Nanopool" && radioButtonEthereum.Checked && textBoxEthereumAddress.Text != "")
                {
                    String jsonString = client.DownloadString("https://api.nanopool.org/v1/eth/user/" + textBoxEthereumAddress.Text);
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var data = (JContainer)(response["data"]);
                    double balance = 0;
                    try
                    {
                        balance = (double)data["balance"];
                    }
                    catch (Exception ex) { }
                    labelBalance.Text = String.Format("{0:N6}", balance) + " ETH (" + String.Format("{0:N2}", (balance * USDETH)) + " USD)";

                    labelPriceDay.Text = "-";
                    labelPriceWeek.Text = "-";
                    labelPriceMonth.Text = "-";
                }
                else if (mCurrentPool == "Nanopool" && radioButtonMonero.Checked && textBoxMoneroAddress.Text != "")
                {
                    String jsonString = client.DownloadString("https://api.nanopool.org/v1/xmr/user/" + textBoxMoneroAddress.Text);
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    var data = (JContainer)(response["data"]);
                    double balance = 0;
                    try
                    {
                        balance = (double)data["balance"];
                    }
                    catch (Exception ex) { }
                    labelBalance.Text = String.Format("{0:N6}", balance) + " XMR (" + String.Format("{0:N2}", (balance * USDXMR)) + " USD)";

                    labelPriceDay.Text = "-";
                    labelPriceWeek.Text = "-";
                    labelPriceMonth.Text = "-";
                }
                else if (mCurrentPool == "DwarfPool" && radioButtonEthereum.Checked && textBoxEthereumAddress.Text != "")
                {
                    String jsonString = client.DownloadString("http://dwarfpool.com/eth/api?wallet=" + textBoxEthereumAddress.Text);
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    double balance = 0;
                    try
                    {
                        balance = Double.Parse((String)response["wallet_balance"]);
                    }
                    catch (Exception ex) { }
                    labelBalance.Text = String.Format("{0:N6}", balance) + " ETH (" + String.Format("{0:N2}", (balance * USDETH)) + " USD)";

                    labelPriceDay.Text = "-";
                    labelPriceWeek.Text = "-";
                    labelPriceMonth.Text = "-";
                }
                else if (mCurrentPool == "DwarfPool" && radioButtonMonero.Checked && textBoxMoneroAddress.Text != "")
                {
                    String jsonString = client.DownloadString("http://dwarfpool.com/xmr/api?wallet=" + textBoxMoneroAddress.Text);
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
                    double balance = 0;
                    try
                    {
                        balance = Double.Parse((String)response["wallet_balance"]);
                    }
                    catch (Exception ex) { }
                    labelBalance.Text = String.Format("{0:N6}", balance) + " XMR (" + String.Format("{0:N2}", (balance * USDXMR)) + " USD)";

                    labelPriceDay.Text = "-";
                    labelPriceWeek.Text = "-";
                    labelPriceMonth.Text = "-";
                }
                else
                {
                    labelPriceDay.Text = "-";
                    labelPriceWeek.Text = "-";
                    labelPriceMonth.Text = "-";
                    labelBalance.Text = "-";
                }
            }
            catch (Exception ex)
            {
                MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
            }
        }

        private String ConvertHashRateToString(double totalSpeed)
        {
            if (totalSpeed < 1000)
            {
                return String.Format("{0:N1} h/s", totalSpeed);
            }
            else if (totalSpeed < 10000)
            {
                return String.Format("{0:N0} h/s", totalSpeed);
            }
            else if (totalSpeed < 100000)
            {
                return String.Format("{0:N2} Kh/s", totalSpeed / 1000);
            }
            else if (totalSpeed < 1000000)
            {
                return String.Format("{0:N1} Kh/s", totalSpeed / 1000);
            }
            else if (totalSpeed < 10000000)
            {
                return String.Format("{0:N0} Kh/s", totalSpeed / 1000);
            }
            else if (totalSpeed < 100000000)
            {
                return String.Format("{0:N2} Mh/s", totalSpeed / 1000000);
            }
            else if (totalSpeed < 1000000000)
            {
                return String.Format("{0:N1} Mh/s", totalSpeed / 1000000);
            }
            else
            {
                return String.Format("{0:N0} Mh/s", totalSpeed / 1000000);
            }
        }

        private void UpdateStatsWithShortPolling()
        {
            mCurrentPool = (string)listBoxPoolPriorities.Items[0];
            if (appState == ApplicationGlobalState.Mining && mDevFeeMode)
            {
                labelCurrentPool.Text = "DEVFEE(" + mDevFeePercentage + "%; " + String.Format("{0:N0}", mDevFeeDurationInSeconds - (DateTime.Now - mDevFeeModeStartTime).TotalSeconds) + " seconds remaining...)";
                mCurrentPool = mStratum.PoolName;
            }
            else if (appState == ApplicationGlobalState.Mining && mStratum != null)
            {
                labelCurrentPool.Text = mStratum.PoolName + " (" + mStratum.ServerAddress + ")";
                mCurrentPool = mStratum.PoolName;
            }
            else
            {
                labelCurrentPool.Text = (string)listBoxPoolPriorities.Items[0];
            }

            for (int i = 0; i < mDevices.Length; ++i)
            {
                Color labelColor = (checkBoxGPUEnableArray[i].Checked ? Color.Black : Color.Gray);
                labelGPUNameArray[i].ForeColor = labelColor;
                labelGPUVendorArray[i].ForeColor = labelColor;
                labelGPUIDArray[i].ForeColor = labelColor;
                labelGPUSpeedArray[i].ForeColor = labelColor;
                labelGPUActivityArray[i].ForeColor = labelColor;
                labelGPUFanArray[i].ForeColor = labelColor;
                labelGPUCoreClockArray[i].ForeColor = labelColor;
                labelGPUMemoryClockArray[i].ForeColor = labelColor;
                labelGPUSharesArray[i].ForeColor = labelColor;
            }

            long elapsedTimeInSeconds = (long)((DateTime.Now - mStartTime).TotalSeconds);
            if (appState != ApplicationGlobalState.Mining)
            {
                labelElapsedTime.Text = "-";
            }
            else if (elapsedTimeInSeconds >= 24 * 60 * 60)
            {
                labelElapsedTime.Text = String.Format("{3} Days {2:00}:{1:00}:{0:00}", elapsedTimeInSeconds % 60, (elapsedTimeInSeconds / 60) % 60, (elapsedTimeInSeconds / 60 / 60) % 24, (elapsedTimeInSeconds / 60 / 60 / 24));
            }
            else
            {
                labelElapsedTime.Text = String.Format("{2:00}:{1:00}:{0:00}", elapsedTimeInSeconds % 60, (elapsedTimeInSeconds / 60) % 60, (elapsedTimeInSeconds / 60 / 60) % 24);
            }

            double totalSpeed = 0;
            if (mMiners != null)
                foreach (Miner miner in mMiners)
                    totalSpeed += miner.Speed;
            labelCurrentSpeed.Text = (appState != ApplicationGlobalState.Mining) ? "-" : ConvertHashRateToString(totalSpeed);
            DeviceManagementLibrariesMutex.WaitOne();
            foreach (var device in mDevices)
            {
                ComputeDevice computeDevice = device.GetComputeDevice();
                int deviceIndex = device.DeviceIndex;
                double speed = 0;
                if (mMiners != null)
                    foreach (Miner miner in mMiners)
                        if (miner.DeviceIndex == deviceIndex)
                            speed += miner.Speed;
                labelGPUSpeedArray[deviceIndex].Text = (appState != ApplicationGlobalState.Mining) ? "-" : ConvertHashRateToString(speed);

                if (device.AcceptedShares + device.RejectedShares == 0)
                {
                    labelGPUSharesArray[deviceIndex].ForeColor = Color.Black;
                    labelGPUSharesArray[deviceIndex].Text = (appState == ApplicationGlobalState.Mining) ? "0" : "-";
                }
                else
                {
                    double acceptanceRate = (double)device.AcceptedShares / (device.AcceptedShares + device.RejectedShares);
                    labelGPUSharesArray[deviceIndex].Text = device.AcceptedShares.ToString() + "/" + (device.AcceptedShares + device.RejectedShares).ToString() + " (" + String.Format("{0:N1}", (acceptanceRate) * 100) + "%)";
                    labelGPUSharesArray[deviceIndex].ForeColor = (acceptanceRate >= 0.95 ? Color.Green : Color.Red); // TODO
                }

                if (ADLAdapterIndexArray[deviceIndex] >= 0)
                {
                    // temperature
                    ADLTemperature OSADLTemperatureData;
                    OSADLTemperatureData = new ADLTemperature();
                    IntPtr tempBuffer = IntPtr.Zero;
                    int size = Marshal.SizeOf(OSADLTemperatureData);
                    tempBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(OSADLTemperatureData, tempBuffer, false);

                    if (null != ADL.ADL_Overdrive5_Temperature_Get)
                    {
                        int ADLRet = ADL.ADL_Overdrive5_Temperature_Get(ADLAdapterIndexArray[deviceIndex], 0, tempBuffer);
                        if (ADL.ADL_SUCCESS == ADLRet)
                        {
                            OSADLTemperatureData = (ADLTemperature)Marshal.PtrToStructure(tempBuffer, OSADLTemperatureData.GetType());
                            labelGPUTempArray[deviceIndex].Text = (OSADLTemperatureData.Temperature / 1000).ToString() + "℃";
                            labelGPUTempArray[deviceIndex].ForeColor = (OSADLTemperatureData.Temperature >= 80000) ? Color.Red :
                                                                       (OSADLTemperatureData.Temperature >= 60000) ? Color.Purple :
                                                                                                                     Color.Blue;
                        }
                    }

                    // activity
                    ADLPMActivity OSADLPMActivityData;
                    OSADLPMActivityData = new ADLPMActivity();
                    IntPtr activityBuffer = IntPtr.Zero;
                    size = Marshal.SizeOf(OSADLPMActivityData);
                    activityBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(OSADLPMActivityData, activityBuffer, false);

                    if (null != ADL.ADL_Overdrive5_CurrentActivity_Get)
                    {
                        int ADLRet = ADL.ADL_Overdrive5_CurrentActivity_Get(ADLAdapterIndexArray[deviceIndex], activityBuffer);
                        if (ADL.ADL_SUCCESS == ADLRet)
                        {
                            OSADLPMActivityData = (ADLPMActivity)Marshal.PtrToStructure(activityBuffer, OSADLPMActivityData.GetType());
                            labelGPUActivityArray[deviceIndex].Text = OSADLPMActivityData.iActivityPercent.ToString() + "%";
                            labelGPUCoreClockArray[deviceIndex].Text = (OSADLPMActivityData.iEngineClock / 100).ToString() + " MHz";
                            labelGPUMemoryClockArray[deviceIndex].Text = (OSADLPMActivityData.iMemoryClock / 100).ToString() + " MHz";
                        }
                    }

                    // fan speed
                    ADLFanSpeedValue OSADLFanSpeedValueData;
                    OSADLFanSpeedValueData = new ADLFanSpeedValue();
                    IntPtr fanSpeedValueBuffer = IntPtr.Zero;
                    size = Marshal.SizeOf(OSADLFanSpeedValueData);
                    OSADLFanSpeedValueData.iSpeedType = 1;
                    fanSpeedValueBuffer = Marshal.AllocCoTaskMem((int)size);
                    Marshal.StructureToPtr(OSADLFanSpeedValueData, fanSpeedValueBuffer, false);

                    if (null != ADL.ADL_Overdrive5_FanSpeed_Get)
                    {
                        int ADLRet = ADL.ADL_Overdrive5_FanSpeed_Get(ADLAdapterIndexArray[deviceIndex], 0, fanSpeedValueBuffer);
                        if (ADL.ADL_SUCCESS == ADLRet)
                        {
                            OSADLFanSpeedValueData = (ADLFanSpeedValue)Marshal.PtrToStructure(fanSpeedValueBuffer, OSADLFanSpeedValueData.GetType());
                            labelGPUFanArray[deviceIndex].Text = OSADLFanSpeedValueData.iFanSpeed.ToString() + "%";
                        }
                    }
                }
                else if (NVMLInitialized && device.GetComputeDevice().Vendor.Equals("NVIDIA Corporation"))
                {
                    uint temp = 0;
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetTemperature(nvmlDeviceArray[deviceIndex], ManagedCuda.Nvml.nvmlTemperatureSensors.Gpu, ref temp);
                    labelGPUTempArray[deviceIndex].Text = temp.ToString() + "℃";
                    labelGPUTempArray[deviceIndex].ForeColor = (temp >= 80) ? Color.Red :
                                                               (temp >= 60) ? Color.Purple :
                                                                              Color.Blue;

                    uint fanSpeed = 0;
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetFanSpeed(nvmlDeviceArray[deviceIndex], ref fanSpeed);
                    labelGPUFanArray[deviceIndex].Text = fanSpeed.ToString() + "%";

                    ManagedCuda.Nvml.nvmlUtilization utilization = new ManagedCuda.Nvml.nvmlUtilization();
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetUtilizationRates(nvmlDeviceArray[deviceIndex], ref utilization);
                    labelGPUActivityArray[deviceIndex].Text = utilization.gpu.ToString() + "%";

                    uint clock = 0;
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetClockInfo(nvmlDeviceArray[deviceIndex], ManagedCuda.Nvml.nvmlClockType.Graphics, ref clock);
                    labelGPUCoreClockArray[deviceIndex].Text = clock.ToString() + " MHz";
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetClockInfo(nvmlDeviceArray[deviceIndex], ManagedCuda.Nvml.nvmlClockType.Mem, ref clock);
                    labelGPUMemoryClockArray[deviceIndex].Text = clock.ToString() + " MHz";
                }
            }
            DeviceManagementLibrariesMutex.ReleaseMutex();
        }

        private void DumpADLInfo()
        {
            int ADLRet = -1;
            int NumberOfAdapters = 0;
            int NumberOfDisplays = 0;

            if (null != ADL.ADL_Adapter_NumberOfAdapters_Get)
            {
                ADL.ADL_Adapter_NumberOfAdapters_Get(ref NumberOfAdapters);
            }

            // Get OS adpater info from ADL
            ADLAdapterInfoArray OSAdapterInfoData;
            OSAdapterInfoData = new ADLAdapterInfoArray();

            if (null != ADL.ADL_Adapter_AdapterInfo_Get)
            {
                IntPtr AdapterBuffer = IntPtr.Zero;
                int size = Marshal.SizeOf(OSAdapterInfoData);
                AdapterBuffer = Marshal.AllocCoTaskMem((int)size);
                Marshal.StructureToPtr(OSAdapterInfoData, AdapterBuffer, false);

                if (null != ADL.ADL_Adapter_AdapterInfo_Get)
                {
                    ADLRet = ADL.ADL_Adapter_AdapterInfo_Get(AdapterBuffer, size);
                    if (ADL.ADL_SUCCESS == ADLRet)
                    {
                        OSAdapterInfoData = (ADLAdapterInfoArray)Marshal.PtrToStructure(AdapterBuffer, OSAdapterInfoData.GetType());
                        int IsActive = 0;

                        for (int i = 0; i < NumberOfAdapters; i++)
                        {
                            // Check if the adapter is active
                            if (null != ADL.ADL_Adapter_Active_Get)
                                ADLRet = ADL.ADL_Adapter_Active_Get(OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex, ref IsActive);

                            if (ADL.ADL_SUCCESS == ADLRet)
                            {
                                Logger("Adapter is   : " + (0 == IsActive ? "DISABLED" : "ENABLED"));
                                Logger("Adapter Index: " + OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex.ToString());
                                Logger("Adapter UDID : " + OSAdapterInfoData.ADLAdapterInfo[i].UDID);
                                Logger("Bus No       : " + OSAdapterInfoData.ADLAdapterInfo[i].BusNumber.ToString());
                                Logger("Driver No    : " + OSAdapterInfoData.ADLAdapterInfo[i].DriverNumber.ToString());
                                Logger("Function No  : " + OSAdapterInfoData.ADLAdapterInfo[i].FunctionNumber.ToString());
                                Logger("Vendor ID    : " + OSAdapterInfoData.ADLAdapterInfo[i].VendorID.ToString());
                                Logger("Adapter Name : " + OSAdapterInfoData.ADLAdapterInfo[i].AdapterName);
                                Logger("Display Name : " + OSAdapterInfoData.ADLAdapterInfo[i].DisplayName);
                                Logger("Present      : " + (0 == OSAdapterInfoData.ADLAdapterInfo[i].Present ? "No" : "Yes"));
                                Logger("Exist        : " + (0 == OSAdapterInfoData.ADLAdapterInfo[i].Exist ? "No" : "Yes"));
                                Logger("Driver Path  : " + OSAdapterInfoData.ADLAdapterInfo[i].DriverPath);
                                Logger("Driver Path X: " + OSAdapterInfoData.ADLAdapterInfo[i].DriverPathExt);
                                Logger("PNP String   : " + OSAdapterInfoData.ADLAdapterInfo[i].PNPString);

                                // Obtain information about displays
                                ADLDisplayInfo oneDisplayInfo = new ADLDisplayInfo();

                                if (null != ADL.ADL_Display_DisplayInfo_Get)
                                {
                                    IntPtr DisplayBuffer = IntPtr.Zero;
                                    int j = 0;

                                    // Force the display detection and get the Display Info. Use 0 as last parameter to NOT force detection
                                    ADLRet = ADL.ADL_Display_DisplayInfo_Get(OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex, ref NumberOfDisplays, out DisplayBuffer, 1);
                                    if (ADL.ADL_SUCCESS == ADLRet)
                                    {
                                        List<ADLDisplayInfo> DisplayInfoData = new List<ADLDisplayInfo>();
                                        for (j = 0; j < NumberOfDisplays; j++)
                                        {
                                            oneDisplayInfo = (ADLDisplayInfo)Marshal.PtrToStructure(new IntPtr(DisplayBuffer.ToInt64() + j * Marshal.SizeOf(oneDisplayInfo)), oneDisplayInfo.GetType());
                                            DisplayInfoData.Add(oneDisplayInfo);
                                        }
                                        Logger("\nTotal Number of Displays supported: " + NumberOfDisplays.ToString());
                                        Logger("\nDispID  AdpID  Type OutType  CnctType Connected  Mapped  InfoValue DisplayName ");

                                        for (j = 0; j < NumberOfDisplays; j++)
                                        {
                                            int InfoValue = DisplayInfoData[j].DisplayInfoValue;
                                            string StrConnected = (1 == (InfoValue & 1)) ? "Yes" : "No ";
                                            string StrMapped = (2 == (InfoValue & 2)) ? "Yes" : "No ";
                                            int AdpID = DisplayInfoData[j].DisplayID.DisplayLogicalAdapterIndex;
                                            string StrAdpID = (AdpID < 0) ? "--" : AdpID.ToString("d2");

                                            Logger(DisplayInfoData[j].DisplayID.DisplayLogicalIndex.ToString() + "        " +
                                                                 StrAdpID + "      " +
                                                                 DisplayInfoData[j].DisplayType.ToString() + "      " +
                                                                 DisplayInfoData[j].DisplayOutputType.ToString() + "      " +
                                                                 DisplayInfoData[j].DisplayConnector.ToString() + "        " +
                                                                 StrConnected + "        " +
                                                                 StrMapped + "      " +
                                                                 InfoValue.ToString("x4") + "   " +
                                                                 DisplayInfoData[j].DisplayName.ToString());
                                        }
                                        Logger("");
                                    }
                                    else
                                    {
                                        Logger("ADL_Display_DisplayInfo_Get() returned error code " + ADLRet.ToString());
                                    }
                                    // Release the memory for the DisplayInfo structure
                                    if (IntPtr.Zero != DisplayBuffer)
                                        Marshal.FreeCoTaskMem(DisplayBuffer);
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger("ADL_Adapter_AdapterInfo_Get() returned error code " + ADLRet.ToString());
                    }
                }
                // Release the memory for the AdapterInfo structure
                if (IntPtr.Zero != AdapterBuffer)
                    Marshal.FreeCoTaskMem(AdapterBuffer);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateDatabase();
            UnloadPhyMemDriver();
            if (ADLInitialized && null != ADL.ADL_Main_Control_Destroy)
                ADL.ADL_Main_Control_Destroy();
        }

        private void timerDeviceStatusUpdates_Tick(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        Stratum mStratum;
        List<Miner> mMiners = null;
        enum ApplicationGlobalState
        {
            Idle = 0,
            Mining = 1,
            Benchmarking = 2
        };
        ApplicationGlobalState appState = ApplicationGlobalState.Idle;

        public bool ValidateBitcoinAddress()
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[13][a-km-zA-HJ-NP-Z1-9]{25,34}$");
            var match = regex.Match(textBoxBitcoinAddress.Text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter a valid Bitcoin address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidateEthereumAddress()
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^0x[a-fA-Z0-9]{40}$");
            var match = regex.Match(textBoxEthereumAddress.Text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter a valid Ethereum address starting with \"0x\".", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidateMoneroAddress()
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^4[0-9AB][123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz]{93}$");
            var match = regex.Match(textBoxMoneroAddress.Text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter a valid Monero address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        struct StratumServerInfo : IComparable<StratumServerInfo>
        {
            public String name;
            public long delay;
            public long time;

            public StratumServerInfo(String aName, long aDelay)
            {
                name = aName;
                delay = aDelay;
                try
                {
                    time = Utilities.MeasurePingRoundtripTime(aName);
                }
                catch (Exception ex)
                {
                    time = -1;
                }
                if (time >= 0)
                    time += delay;
            }

            public int CompareTo(StratumServerInfo other)
            {
                if (time == other.time)
                {
                    return 0;
                }
                else if (other.time < 0 && time >= 0)
                {
                    return -1;
                }
                else if (other.time >= 0 && time < 0)
                {
                    return 1;
                }
                else if (other.time > time)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        };

        public void LaunchCryptoNightMiners(String pool)
        {
            CryptoNightStratum stratum = null;
            bool niceHashMode = false;

            if (pool == "NiceHash" || mDevFeeMode)
            {
                var hosts = new List<StratumServerInfo> {
                    new StratumServerInfo("cryptonight.usa.nicehash.com", 0),   
                    new StratumServerInfo("cryptonight.eu.nicehash.com", 0),
                    new StratumServerInfo("cryptonight.hk.nicehash.com", 150),
                    new StratumServerInfo("cryptonight.jp.nicehash.com", 100),
                    new StratumServerInfo("cryptonight.in.nicehash.com", 200),
                    new StratumServerInfo("cryptonight.br.nicehash.com", 180)
                };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            stratum = (new CryptoNightStratum(host.name, 3355, (mDevFeeMode ? mDevFeeBitcoinAddress : textBoxBitcoinAddress.Text), "x", pool));
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
                niceHashMode = true;
            }
            else if (pool == "DwarfPool")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("xmr-eu.dwarfpool.com", 0),
                                new StratumServerInfo("xmr-usa.dwarfpool.com", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            String username = textBoxMoneroAddress.Text;
                            if (textBoxRigID.Text != "")
                                username += "." + textBoxRigID.Text; // TODO
                            stratum = new CryptoNightStratum(host.name, 8005, username, (textBoxEmail.Text != "" ? textBoxEmail.Text : "x"), pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "Nanopool")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("xmr-eu1.nanopool.org", 0),
                                new StratumServerInfo("xmr-eu2.nanopool.org", 0),
                                new StratumServerInfo("xmr-us-east1.nanopool.org", 0),
                                new StratumServerInfo("xmr-us-west1.nanopool.org", 0),
                                new StratumServerInfo("xmr-asia1.nanopool.org", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            String username = textBoxMoneroAddress.Text;
                            if (textBoxRigID.Text != "")
                            {
                                username += "." + textBoxRigID.Text; // TODO
                                if (textBoxEmail.Text != "")
                                    username += "/" + textBoxEmail.Text;
                            }
                            stratum = new CryptoNightStratum(host.name, 14444, username, "x", pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "mineXMR.com")
            {
                String username = textBoxMoneroAddress.Text;
                if (textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text; // TODO
                stratum = new CryptoNightStratum("pool.minexmr.com", 7777, username, "x", pool);
            }
            mStratum = (Stratum)stratum;
            mMiners = new List<Miner>();
            for (int deviceIndex = 0; deviceIndex < mDevices.Length; ++deviceIndex)
            {
                if (checkBoxGPUEnableArray[deviceIndex].Checked)
                {
                    for (int i = 0; i < numericUpDownDeviceCryptoNightThreadsArray[deviceIndex].Value; ++i)
                        mMiners.Add(new OpenCLCryptoNightMiner(mDevices[deviceIndex], stratum, Convert.ToInt32(Math.Round(numericUpDownDeviceCryptoNightIntensityArray[deviceIndex].Value)), Convert.ToInt32(Math.Round(numericUpDownDeviceCryptoNightLocalWorkSizeArray[deviceIndex].Value)), niceHashMode));
                    System.Threading.Thread.Sleep(mLaunchInterval);
                }
            }
        }

        public void LaunchEthashMiners(String pool)
        {
            EthashStratum stratum = null;

            if (pool == "NiceHash" || mDevFeeMode)
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("daggerhashimoto.usa.nicehash.com", 0),   
                                new StratumServerInfo("daggerhashimoto.eu.nicehash.com", 0),
                                new StratumServerInfo("daggerhashimoto.hk.nicehash.com", 150),
                                new StratumServerInfo("daggerhashimoto.jp.nicehash.com", 100),
                                new StratumServerInfo("daggerhashimoto.in.nicehash.com", 200),
                                new StratumServerInfo("daggerhashimoto.br.nicehash.com", 180)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            stratum = new NiceHashEthashStratum(host.name, 3353, (mDevFeeMode ? mDevFeeBitcoinAddress : textBoxBitcoinAddress.Text), "x", pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "zawawa.net")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("eth-uswest.zawawa.net", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 4000, textBoxEthereumAddress.Text, "x", pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "DwarfPool")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("eth-eu.dwarfpool.com", 0),
                                new StratumServerInfo("eth-us.dwarfpool.com", 0),
                                new StratumServerInfo("eth-us2.dwarfpool.com", 0),
                                new StratumServerInfo("eth-ru.dwarfpool.com", 0),
                                new StratumServerInfo("eth-asia.dwarfpool.com", 0),
                                new StratumServerInfo("eth-cn.dwarfpool.com", 0),
                                new StratumServerInfo("eth-cn2.dwarfpool.com", 0),
                                new StratumServerInfo("eth-sg.dwarfpool.com", 0),
                                new StratumServerInfo("eth-au.dwarfpool.com", 0),
                                new StratumServerInfo("eth-ru2.dwarfpool.com", 0),
                                new StratumServerInfo("eth-hk.dwarfpool.com", 0),
                                new StratumServerInfo("eth-br.dwarfpool.com", 0),
                                new StratumServerInfo("eth-ar.dwarfpool.com", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            String username = textBoxEthereumAddress.Text;
                            if (textBoxRigID.Text != "")
                                username += "." + textBoxRigID.Text; // TODO
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 8008, username, (textBoxEmail.Text != "" ? textBoxEmail.Text : "x"), pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "ethermine.org")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("us1.ethermine.org", 0),
                                new StratumServerInfo("us2.ethermine.org", 0),
                                new StratumServerInfo("eu1.ethermine.org", 0),
                                new StratumServerInfo("asia1.ethermine.org", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            String username = textBoxEthereumAddress.Text;
                            if (textBoxRigID.Text != "")
                                username += "." + textBoxRigID.Text; // TODO
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 4444, username, "x", pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "ethpool.org")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("us1.ethpool.org", 0),
                                new StratumServerInfo("us2.ethpool.org", 0),
                                new StratumServerInfo("eu1.ethpool.org", 0),
                                new StratumServerInfo("asia1.ethpool.org", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            String username = textBoxEthereumAddress.Text;
                            if (textBoxRigID.Text != "")
                                username += "." + textBoxRigID.Text; // TODO
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 3333, username, "x", pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else if (pool == "Nanopool")
            {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("eth-eu1.nanopool.org", 0),
                                new StratumServerInfo("eth-eu2.nanopool.org", 0),
                                new StratumServerInfo("eth-asia1.nanopool.org", 0),
                                new StratumServerInfo("eth-us-east1.nanopool.org", 0),
                                new StratumServerInfo("eth-us-west1.nanopool.org", 0)
                            };
                hosts.Sort();
                foreach (StratumServerInfo host in hosts)
                {
                    if (host.time >= 0)
                    {
                        try
                        {
                            String username = textBoxEthereumAddress.Text;
                            if (textBoxRigID.Text != "")
                            {
                                username += "." + textBoxRigID.Text; // TODO
                                if (textBoxEmail.Text != "")
                                    username += "/" + textBoxEmail.Text;
                            }
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 9999, username, "x", pool);
                            break;
                        }
                        catch (Exception ex)
                        {
                            MainForm.Logger("Exception: " + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            else
            {
                stratum = new OpenEthereumPoolEthashStratum("eth-uswest.zawawa.net", 4000, textBoxEthereumAddress.Text, "x", pool);
            }

            mStratum = (Stratum)stratum;
            mMiners = new List<Miner>();
            for (int deviceIndex = 0; deviceIndex < mDevices.Length; ++deviceIndex)
            {
                if (checkBoxGPUEnableArray[deviceIndex].Checked)
                {
                    mMiners.Add(new OpenCLEthashMiner(mDevices[deviceIndex], stratum, Convert.ToInt32(Math.Round(numericUpDownDeviceCryptoNightIntensityArray[deviceIndex].Value))));
                    System.Threading.Thread.Sleep(mLaunchInterval);
                }
            }
        }

        private void LaunchMiners()
        {
            foreach (String pool in listBoxPoolPriorities.Items)
            {
                try
                {
                    if (radioButtonEthereum.Checked)
                    {
                        Logger("Launching Ethash miners...");
                        LaunchEthashMiners(pool);
                        break;
                    }
                    else if (radioButtonMonero.Checked)
                    {
                        Logger("Launching CryptoNight miners...");
                        LaunchCryptoNightMiners(pool);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Logger("Exception: " + ex.Message + ex.StackTrace);
                    //if (mStratum != null)
                    //    mStratum.Stop();
                    //if (mMiners != null)
                    //    foreach (Miner miner in mMiners)
                    //        miner.Stop();
                    mStratum = null;
                    mMiners = null;
                }
            }
        }

        private void StopMiners()
        {
            try
            {
                Logger("Stopping miners...");
                foreach (Miner miner in mMiners)
                {
                    miner.Stop();
                    miner.WaitForExit(60000);
                    if (!miner.Done)
                        miner.Abort(); // Not good at all. Avoid this at all costs.
                }
                mStratum.Stop();
            }
            catch (Exception ex)
            {
                Logger("Exception: " + ex.Message + ex.StackTrace);
            }
            mMiners = null;
            mStratum = null;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            UpdateDatabase();

            if (textBoxBitcoinAddress.Text != "" && !ValidateBitcoinAddress())
                return;
            if (textBoxEthereumAddress.Text != "" && !ValidateEthereumAddress())
                return;
            if (textBoxMoneroAddress.Text != "" && !ValidateMoneroAddress())
                return;
            if (textBoxBitcoinAddress.Text == "" && textBoxEthereumAddress.Text == "" && textBoxMoneroAddress.Text == "")
            {
                MessageBox.Show("Please enter at least one valid wallet address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlMainForm.TabIndex = 1;
                return;
            }
            bool enabled = false;
            foreach (var control in checkBoxGPUEnableArray)
                enabled = enabled || control.Checked;
            if (!enabled)
            {
                MessageBox.Show("Please enable at least one device.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlMainForm.TabIndex = 0;
                return;
            }

            this.Enabled = false;

            if (appState == ApplicationGlobalState.Idle)
            {
                foreach (var device in mDevices)
                {
                    device.ClearShares();
                    labelGPUSharesArray[device.DeviceIndex].Text = "0";
                }

                mStratum = null;
                mMiners = null;

                mDevFeeMode = true;
                LaunchMiners();
                if (mStratum == null || mMiners == null)
                {
                    MessageBox.Show("Failed to launch miner.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    appState = ApplicationGlobalState.Mining;
                    tabControlMainForm.SelectedIndex = 0;
                    timerDevFee.Interval = mDevFeeDurationInSeconds * 1000;
                    timerDevFee.Enabled = true;
                    mStartTime = DateTime.Now;
                    mDevFeeModeStartTime = DateTime.Now;
                }
            }
            else if (appState == ApplicationGlobalState.Mining)
            {
                timerDevFee.Enabled = false;
                StopMiners();
                appState = ApplicationGlobalState.Idle;
            }

            UpdateStatsWithShortPolling();
            UpdateStatsWithLongPolling();
            UpdateControls();
        }

        private void UpdateControls()
        {
            buttonStart.Text = (appState == ApplicationGlobalState.Mining) ? "Stop" : "Start";
            buttonBenchmark.Enabled = false;

            groupBoxCoinsToMine.Enabled = (appState == ApplicationGlobalState.Idle);
            groupBoxPoolPriorities.Enabled = (appState == ApplicationGlobalState.Idle);
            groupBoxPoolParameters.Enabled = (appState == ApplicationGlobalState.Idle);
            groupBoxWalletAddresses.Enabled = (appState == ApplicationGlobalState.Idle);
            groupBoxAutomation.Enabled = (appState == ApplicationGlobalState.Idle);
            foreach (var control in checkBoxGPUEnableArray)
                control.Enabled = (appState == ApplicationGlobalState.Idle);

            this.Enabled = true;
        }

        private void timerCurrencyStatUpdates_Tick(object sender, EventArgs e)
        {
            UpdateStatsWithLongPolling();
        }

        private void buttonPoolPrioritiesUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxPoolPriorities.SelectedIndex;
            if (selectedIndex > 0)
            {
                listBoxPoolPriorities.Items.Insert(selectedIndex - 1, listBoxPoolPriorities.Items[selectedIndex]);
                listBoxPoolPriorities.Items.RemoveAt(selectedIndex + 1);
                listBoxPoolPriorities.SelectedIndex = selectedIndex - 1;
                UpdateStatsWithLongPolling();
            }
        }

        private void buttonPoolPrioritiesDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxPoolPriorities.SelectedIndex;
            if (selectedIndex < listBoxPoolPriorities.Items.Count - 1 & selectedIndex != -1)
            {
                listBoxPoolPriorities.Items.Insert(selectedIndex + 2, listBoxPoolPriorities.Items[selectedIndex]);
                listBoxPoolPriorities.Items.RemoveAt(selectedIndex);
                listBoxPoolPriorities.SelectedIndex = selectedIndex + 1;
                UpdateStatsWithLongPolling();
            }
        }

        private void buttonViewBalancesAtNiceHash_Click(object sender, EventArgs e)
        {
            if (ValidateBitcoinAddress())
                System.Diagnostics.Process.Start("https://www.nicehash.com/miner/" + textBoxBitcoinAddress.Text);
        }

        private void tabControlMainForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDatabase();
        }

        private void buttonEthereumBalance_Click(object sender, EventArgs e)
        {
            if (!ValidateEthereumAddress())
                return;
            foreach (String poolName in listBoxPoolPriorities.Items)
            {
                if (poolName == "Nanopool")
                {
                    System.Diagnostics.Process.Start("https://eth.nanopool.org/account/" + textBoxEthereumAddress.Text);
                    return;
                }
                else if (poolName == "DwarfPool")
                {
                    System.Diagnostics.Process.Start("https://dwarfpool.com/eth/address?wallet=" + textBoxEthereumAddress.Text);
                    return;
                }
                else if (poolName == "ethermine.org")
                {
                    System.Diagnostics.Process.Start("https://ethermine.org/miners/" + textBoxEthereumAddress.Text);
                    return;
                }
                else if (poolName == "ethpool.org")
                {
                    System.Diagnostics.Process.Start("https://ethpool.org/miners/" + textBoxEthereumAddress.Text);
                    return;
                }
            }
        }

        private void timerDevFee_Tick(object sender, EventArgs e)
        {
            if (appState != ApplicationGlobalState.Mining)
            {
                timerDevFee.Enabled = false;
            }
            else if (mDevFeeMode)
            {
                labelCurrentPool.Text = "Switching...";
                Enabled = false;
                StopMiners();
                mDevFeeMode = false;
                timerDevFee.Interval = (int)(((double)mDevFeeDurationInSeconds * ((double)(100 - mDevFeePercentage) / mDevFeePercentage)) * 1000);
                LaunchMiners();
                if (mMiners == null || mStratum == null)
                {
                    mDevFeeMode = true;
                    timerDevFee.Interval = 1000;
                }
            }
            else
            {
                labelCurrentPool.Text = "Switching...";
                Enabled = false;
                StopMiners();
                mDevFeeMode = true;
                timerDevFee.Interval = mDevFeeDurationInSeconds * 1000;
                LaunchMiners();
                if (mMiners == null || mStratum == null)
                {
                    mDevFeeMode = false;
                    timerDevFee.Interval = 1000;
                }
            }

            UpdateStatsWithLongPolling();
            Enabled = true;
        }

        private void radioButtonMonero_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMonero.Checked)
            {
                radioButtonMostProfitable.Checked = false;
                radioButtonEthereum.Checked = false;
                radioButtonZcash.Checked = false;
            }
        }

        private void radioButtonEthereum_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEthereum.Checked)
            {
                radioButtonMostProfitable.Checked = false;
                radioButtonMonero.Checked = false;
                radioButtonZcash.Checked = false;
            }
        }

        private void radioButtonMostProfitable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMostProfitable.Checked)
            {
                radioButtonMonero.Checked = false;
                radioButtonEthereum.Checked = false;
                radioButtonZcash.Checked = false;
            }
        }

        private void radioButtonZcash_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonZcash.Checked)
            {
                radioButtonMostProfitable.Checked = false;
                radioButtonEthereum.Checked = false;
                radioButtonMonero.Checked = false;
            }
        }

        private void timerWatchdog_Tick(object sender, EventArgs e)
        {
            if (appState == ApplicationGlobalState.Mining && mMiners != null)
            {
                foreach (var miner in mMiners)
                    miner.KeepAlive();
            }
        }

        private void buttonMoneroBalance_Click(object sender, EventArgs e)
        {
            if (!ValidateMoneroAddress())
                return;
            foreach (String poolName in listBoxPoolPriorities.Items)
            {
                if (poolName == "Nanopool")
                {
                    System.Diagnostics.Process.Start("https://xmr.nanopool.org/account/" + textBoxMoneroAddress.Text);
                    return;
                }
                else if (poolName == "DwarfPool")
                {
                    System.Diagnostics.Process.Start("https://dwarfpool.com/xmr/address?wallet=" + textBoxMoneroAddress.Text);
                    return;
                }
                else if (poolName == "mineXMR.com")
                {
                    System.Diagnostics.Process.Start("http://minexmr.com/");
                    return;
                }
            }
        }

        private void timerUpdateLog_Tick(object sender, EventArgs e)
        {
            UpdateLog();
        }

        private void checkBoxLaunchAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                if (checkBoxLaunchAtStartup.Checked)
                {
                    startInfo.Arguments = "/C schtasks /create /sc onlogon /tn GatelessGateSharp /rl highest /tr \"" + Application.ExecutablePath + "\"";
                }
                else
                {
                    startInfo.Arguments = "/C schtasks /delete /f /tn GatelessGateSharp";
                }
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to complete the operation.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxGPU0Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU1Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU2Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU3Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU4Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU5Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU6Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void checkBoxGPU7Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatsWithShortPolling();
        }

        private void labelGPU0ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU0MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU0Enable.Checked = !checkBoxGPU0Enable.Checked;
        }

        private void labelGPU1ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU1MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU1Enable.Checked = !checkBoxGPU1Enable.Checked;
        }

        private void labelGPU2ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU2MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU2Enable.Checked = !checkBoxGPU2Enable.Checked;
        }

        private void labelGPU3Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU3MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU3Enable.Checked = !checkBoxGPU3Enable.Checked;
        }

        private void labelGPU4ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU4MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU4Enable.Checked = !checkBoxGPU4Enable.Checked;
        }

        private void labelGPU5ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU5MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU5Enable.Checked = !checkBoxGPU5Enable.Checked;
        }

        private void labelGPU6ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU6MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU6Enable.Checked = !checkBoxGPU6Enable.Checked;
        }

        private void labelGPU7ID_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Vendor_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Name_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Speed_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Shares_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Activity_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Temp_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7Fan_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7CoreClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void labelGPU7MemoryClock_Click(object sender, EventArgs e)
        {
            checkBoxGPU7Enable.Checked = !checkBoxGPU7Enable.Checked;
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(logFileName);
        }
    }
}
