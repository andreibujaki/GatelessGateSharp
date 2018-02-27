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
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ATI.ADL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Winforms.Cartesian.ConstantChanges;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using NetTools;



namespace GatelessGateSharp
{
    public partial class MainForm : Form
    {

        #region NativeMethods

        class NativeMethods
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GlobalMemoryStatusEx", SetLastError = true)]
            public static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatusEx lpBuffer);
            [DllImport("kernel32.dll")]
            public static extern uint SetThreadExecutionState(uint esFlags);

            [StructLayout(LayoutKind.Sequential)]
            internal class MemoryStatusEx
            {
                public uint dwLength;
                public uint dwMemoryLoad;
                public UInt64 ullTotalPhys;
                public UInt64 ullAvailPhys;
                public UInt64 ullTotalPageFile;
                public UInt64 ullAvailPageFile;
                public UInt64 ullTotalVirtual;
                public UInt64 ullAvailVirtual;
                public UInt64 ullAvailExtendedVirtual;
                public MemoryStatusEx()
                {
                    this.dwLength = (uint)Marshal.SizeOf(typeof(MemoryStatusEx));
                }
            }

            [DllImport("Shell32.dll")]
            public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);

            [Flags]
            public enum KnownFolderFlags : uint
            {
                SimpleIDList = 0x00000100,
                NotParentRelative = 0x00000200,
                DefaultPath = 0x00000400,
                Init = 0x00000800,
                NoAlias = 0x00001000,
                DontUnexpand = 0x00002000,
                DontVerify = 0x00004000,
                Create = 0x00008000,
                NoAppcontainerRedirection = 0x00010000,
                AliasOnly = 0x80000000
            }

            public const uint ES_CONTINUOUS = 0x80000000;
            public const uint ES_SYSTEM_REQUIRED = 0x00000001;
            public const uint ES_AWAYMODE_REQUIRED = 0x00000040;
        }

        #endregion

        private static MainForm instance;
        public static string shortAppName = "Gateless Gate Sharp";
        public static string appVersion = "1.3.0";
        public static string appName = shortAppName + " " + appVersion + " prealpha";
        public static string normalizedShortAppName = "gateless-gate-sharp";
        private static string databaseFileName = "GatelessGateSharp.sqlite";
        private static string logFileName = "GatelessGateSharp.log";
        private static string mAppStateFileName = "GatelessGateSharpState.txt";
        private static int mLaunchInterval = 1000;
        public static readonly string sAlgorithmListRegexPattern = @"ethash_pascal|ethash|cryptonight|neoscrypt|pascal|lbry|lyra2rev2";
        public static readonly string[] sAlgorithmList = sAlgorithmListRegexPattern.Split('|');
        private bool mAreSettingsDirty = false;

        private System.Threading.Mutex loggerMutex = new System.Threading.Mutex();
        private TabPage[] tabPageDeviceArray;

        // device ID, algorithm/type, pararmeter
        private Dictionary<Tuple<int, string, string>, CheckBox> checkBoxDeviceParameterArray = new Dictionary<Tuple<int, string, string>, CheckBox> { };
        private Dictionary<Tuple<int, string, string>, NumericUpDown> numericUpDownDeviceParameterArray = new Dictionary<Tuple<int, string, string>, NumericUpDown> { };
        private Dictionary<Tuple<int, string, string>, TextBox> textBoxDeviceParameterArray = new Dictionary<Tuple<int, string, string>, TextBox> { };
        private Dictionary<Tuple<int, string, string>, GroupBox> groupBoxDeviceParameterArray = new Dictionary<Tuple<int, string, string>, GroupBox> { };
        private Dictionary<int, TabControl> tabControlAlgorithmsArray = new Dictionary<int, TabControl> { };

        private Button[] buttonDeviceResetToDefaultArray;
        private Button[] buttonDeviceResetAllArray;
        private Button[] buttonDeviceCopyToOthersArray;

        public static bool ADLInitialized = false;
        public static bool NVMLInitialized = false;
        private System.Threading.Mutex DeviceManagementLibrariesMutex = new System.Threading.Mutex();
        private ManagedCuda.Nvml.nvmlDevice[] nvmlDeviceArray;
        private bool phymemLoaded = false;
        private CancellationTokenSource mBackgroundTasksCancellationTokenSource;
        private bool mDeviceColorCodesInitialized = false;
        private bool mChartsInitialized = false;

        string mUserMoneroAddress = "";
        string mUserPascalAddress = "";
        string mUserLbryAddress = "";
        string mUserEthereumAddress = "";
        string mUserBitcoinAddress = "";
        string mUserFeathercoinAddress = "";
        string mUserZcashAddress = "";

        private bool mDevFeeMode = true;
        private DateTime mDevFeeModeStartTime = DateTime.Now; // dummy

        private string mCurrentPool = "NiceHash";

        public static MainForm Instance { get { return instance; } }
        public static bool DevFeeMode { get { return Instance.mDevFeeMode; } }

        private static string sLoggerBuffer = "";

        public static void Logger(string lines)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

            lines = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " [" + System.Threading.Thread.CurrentThread.ManagedThreadId + "] " + lines + "\r\n";
            Console.Write(lines);
            try { Instance.loggerMutex.WaitOne(5000); } catch (Exception) { }
            sLoggerBuffer += lines;
            try { Instance.loggerMutex.ReleaseMutex(); } catch (Exception) { }
        }

        public static void Logger(
            Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            Logger("Exception: " + ex.Message);
            Logger("    Member:      " + memberName);
            Logger("    Source File: " + sourceFilePath);
            Logger("    Line Number: " + sourceLineNumber);
            Logger("    StackTrace:  " + ex.StackTrace);
        }


        public static void UpdateLog()
        {
            try { Instance.loggerMutex.WaitOne(5000); } catch (Exception) { }
            var loggerBuffer = sLoggerBuffer;
            sLoggerBuffer = "";
            try { Instance.loggerMutex.ReleaseMutex(); } catch (Exception) { }

            if (loggerBuffer == "")
                return;

            try {
                // Update the log file.
                using (var file = new System.IO.StreamWriter(LogFilePath, true))
                    file.Write(loggerBuffer);
                if (new System.IO.FileInfo(LogFilePath).Length > Parameters.MaxLogFileSize) {
                    var backupFilePath = LogFilePath + "." + DateTime.Now.ToString("yyyy-MM-dd--HHmm");
                    System.IO.File.Copy(LogFilePath, backupFilePath);
                    System.IO.File.Delete(LogFilePath);
                }

                // Avoid unhandled exceptions.
                Utilities.FixFPU();

                // Scroll down to the bottom.
                if (Instance.richTextBoxLog.Lines.Length > Parameters.LogMaxNumLines) {
                    Instance.richTextBoxLog.ReadOnly = false;
                    Instance.richTextBoxLog.Select(0, Instance.richTextBoxLog.GetFirstCharIndexFromLine(Instance.richTextBoxLog.Lines.Length - Parameters.LogMaxNumLines));
                    Instance.richTextBoxLog.SelectedText = "";
                    Instance.richTextBoxLog.ReadOnly = true;
                }
                Instance.richTextBoxLog.SelectionLength = 0;
                Instance.richTextBoxLog.SelectionStart = Instance.richTextBoxLog.Text.Length;
                Instance.richTextBoxLog.ScrollToCaret();
                Instance.richTextBoxLog.Text += loggerBuffer;
                Instance.richTextBoxLog.SelectionLength = 0;
                Instance.richTextBoxLog.SelectionStart = Instance.richTextBoxLog.Text.Length;
                Instance.richTextBoxLog.ScrollToCaret();

                // Update the status bar.
                Instance.toolStripStatusLabel1.Text = loggerBuffer.Split('\n')[0].Replace("\r", "");
            } catch (Exception) { }
        }

        private static bool CheckVirtualMemorySize()
        {
            var status = new NativeMethods.MemoryStatusEx();

            NativeMethods.GlobalMemoryStatusEx(status);

            /*
            MainForm.Logger("dwLength: " + (ulong)status.dwLength);
            MainForm.Logger("dwMemoryLoad: " + (ulong)status.dwMemoryLoad);
            MainForm.Logger("ullTotalPhys: " + (ulong)status.ullTotalPhys);
            MainForm.Logger("ullAvailPhys: " + (ulong)status.ullAvailPhys);
            MainForm.Logger("ullTotalPageFile: " + (ulong)status.ullTotalPageFile);
            MainForm.Logger("ullAvailPageFile: " + (ulong)status.ullAvailPageFile);
            MainForm.Logger("ullTotalVirtual: " + (ulong)status.ullTotalVirtual);
            MainForm.Logger("ullAvailVirtual: " + (ulong)status.ullAvailVirtual);
            */

            if ((ulong)status.ullTotalPageFile - status.ullTotalPhys < (ulong)24 * 1024 * 1024 * 1024) {
                var result = MessageBox.Show(Utilities.GetAutoClosingForm(20), "The total size of page files is too small.\nAt least 24GB is recommended for this application.\nWould you like this application to automatically set it for you?\nThe computer will be rebooted after the change is made.\nAlternatively, you can manually increase the page file size in Advanced System Settings.", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (result == DialogResult.Yes) {
                    try {
                        var process = new System.Diagnostics.Process();
                        var startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                        startInfo.FileName = "wmic";
                        startInfo.Arguments = "computersystem set AutomaticManagedPagefile=False";
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();

                        startInfo.FileName = "wmic";
                        startInfo.Arguments = "pagefileset create name=\"C:\\pagefile.sys\"";
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();

                        startInfo.FileName = "wmic";
                        startInfo.Arguments = "pagefileset set InitialSize=24576,MaximumSize=24576";
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();

                        startInfo.FileName = "shutdown";
                        startInfo.Arguments = "/r";
                        process.StartInfo = startInfo;
                        process.Start();

                        Program.Exit(true);
                    } catch (Exception ex) {
                        MainForm.Logger("Failed to increase the total size of page files: " + ex.Message + ex.StackTrace);
                    }
                }
                return false;
            }
            return true;
        }

        public MainForm()
        {
            instance = this;

            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainForm_DragEnter);
            this.DragDrop += new DragEventHandler(MainForm_DragDrop);

            foreach (var checkBox in Utilities.FindAllChildrenByType<CheckBox>(this)) {
                var tag = (string)(checkBox.GetType().GetProperty("Tag").GetValue(checkBox));
                if (tag == null)
                    continue;
                var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                var match = regex.Match(tag);
                var type = match.Success ? match.Groups[1].Value : null;
                var name = match.Success ? match.Groups[2].Value : null;
                if (type == "parameter")
                    checkBox.CheckedChanged += new System.EventHandler(this.MarkSettingsAsDirty);
            }

            comboBoxDefaultAlgorithm.SelectedIndex = 0;

            comboBoxCustomPool0Algorithm.SelectedIndex = 0;
            comboBoxCustomPool1Algorithm.SelectedIndex = 0;
            comboBoxCustomPool2Algorithm.SelectedIndex = 0;
            comboBoxCustomPool3Algorithm.SelectedIndex = 0;

            comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex = 0;
            comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex = 0;
            comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex = 0;
            comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex = 0;

            comboBoxCurrency.SelectedIndex = 0;

            comboBoxGraphType.SelectedIndex = 0;
            comboBoxGraphCoverage.SelectedIndex = 0;
            comboBoxSecondGraphType.SelectedIndex = 0;
            comboBoxSecondGraphCoverage.SelectedIndex = 0;

            // LiveCharts
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);
            Charting.For<MeasureModel>(mapper);
        }

#if COMMAND_LINE_VERSION
        protected override void OnLoad(EventArgs e) {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            base.OnLoad(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            this.Visible = false;
        }
#endif

        void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                LoadSettingsFromDatabase(file);
        }

        private void CreateNewDatabase(string filePath)
        {
            SQLiteConnection.CreateFile(filePath);
            using (var conn = new SQLiteConnection("Data Source=" + filePath + ";Version=3;")) {
                conn.Open();
                var sql = "create table wallet_addresses (coin varchar(64), address varchar(256));";
                using (var command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                sql = "create table pools (name varchar(64));";
                using (var command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                sql = "create table properties (name varchar(64), value varchar(256));";
                using (var command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                sql = "create table device_parameters (device_id int, device_vendor varchar(64), device_name varchar(64), parameter_name varchar(64), parameter_value varchar(256));";
                using (var command = new SQLiteCommand(sql, conn)) { command.ExecuteNonQuery(); }

                conn.Close();
            }
        }

        static string AppDataPathBase {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GatelessGateSharp"; }
        }

        static string LogFilePathBase {
            get { return AppDataPathBase + "\\Logs"; }
        }

        static string SettingsBackupPathBase {
            get { return AppDataPathBase + "\\Backups"; }
        }

        public static string SavedOpenCLBinaryKernelPathBase {
            get { return AppDataPathBase + "\\BinaryKernels"; }
        }

        static string DatabaseFilePath {
            get { return AppDataPathBase + "\\" + databaseFileName; }
        }

        static string LogFilePath {
            get { return LogFilePathBase + "\\" + logFileName; }
        }

        static string AppStateFilePath {
            get { return AppDataPathBase + "\\" + mAppStateFileName; }
        }

        static string OldDatabaseFilePath {
            get { return databaseFileName; }
        }

        static string OldLogFilePath {
            get { return logFileName; }
        }

        static string OldAppStateFilePath {
            get { return mAppStateFileName; }
        }

        class CommandLineParameter
        {
            string mParameterName;
            string mParameterValue;

            public string Name { get { return mParameterName; } }
            public string Value { get { return mParameterValue; } }

            public CommandLineParameter(string parameterName, string parameterValue = "true")
            {
                mParameterName = parameterName;
                mParameterValue = parameterValue;
            }

            public CommandLineParameter(string s)
            {
                Regex re = new Regex(@"^--([a-zA-Z0-9_-]+)=(.*)$");
                Match match;
                if ((match = re.Match(s)).Success) {
                    mParameterName = match.Groups[1].Value;
                    mParameterValue = match.Groups[2].Value;
                    mParameterName = (new Regex(@"-")).Replace(mParameterName, "_");
                } else {
                    re = new Regex(@"^--([a-zA-Z0-9_-]+)$");
                    if ((match = re.Match(s)).Success) {
                        mParameterName = match.Groups[1].Value;
                        mParameterValue = "true";
                        mParameterName = (new Regex(@"-")).Replace(mParameterName, "_");
                    } else {
                        throw new System.ArgumentException("\"" + s + "\" is not a valid command-line option.");
                    }
                }

                Console.WriteLine("Parameter Name: " + Name);
                Console.WriteLine("Parameter Value: " + Value);
            }
        }

        private void ParseCommandLineArguments(string[] arguments)
        {
            var list = new List<CommandLineParameter> { };
            foreach (var argument in arguments) {
                try {
                    list.Add(new CommandLineParameter(argument));
                } catch (Exception ex) {
                    Logger("Invalid argument: " + argument);
                }
            }

            var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");

            foreach (var comboBox in Utilities.FindAllChildrenByType<ComboBox>(this)) {
                var tag = (string)(comboBox.GetType().GetProperty("Tag").GetValue(comboBox));
                if (tag == null)
                    continue;
                var match = regex.Match(tag);
                var type = match.Success ? match.Groups[1].Value : null;
                var name = match.Success ? match.Groups[2].Value : null;
                if (type == "parameter") {
                    foreach (var param in list) {
                        if (param.Name == name) {
                            if (name == "currency" && !comboBox.Items.Contains(param.Value))
                                comboBox.Items.Add(param.Value);
                            try { comboBox.SelectedIndex = comboBox.FindStringExact(param.Value); } catch (Exception) { }
                        }
                    }
                }
            }

            foreach (var textBox in Utilities.FindAllChildrenByType<TextBox>(this)) {
                var tag = (string)(textBox.GetType().GetProperty("Tag").GetValue(textBox));
                if (tag == null)
                    continue;
                var match = regex.Match(tag);
                var type = match.Success ? match.Groups[1].Value : null;
                var name = match.Success ? match.Groups[2].Value : null;
                if (type == "parameter") {
                    foreach (var param in list) {
                        if (param.Name == name)
                            textBox.Text = param.Value;
                    }
                }
            }

            foreach (var checkBox in Utilities.FindAllChildrenByType<CheckBox>(this)) {
                var tag = (string)(checkBox.GetType().GetProperty("Tag").GetValue(checkBox));
                if (tag == null)
                    continue;
                var match = regex.Match(tag);
                var type = match.Success ? match.Groups[1].Value : null;
                var name = match.Success ? match.Groups[2].Value : null;
                if (type == "parameter") {
                    foreach (var param in list) {
                        if (param.Name == name)
                            checkBox.Checked = param.Value == "true";
                    }
                }
            }

            foreach (var numericUpDown in Utilities.FindAllChildrenByType<NumericUpDown>(this)) {
                var tag = (string)(numericUpDown.GetType().GetProperty("Tag").GetValue(numericUpDown));
                if (tag == null)
                    continue;
                var match = regex.Match(tag);
                var type = match.Success ? match.Groups[1].Value : null;
                var name = match.Success ? match.Groups[2].Value : null;
                if (type == "parameter") {
                    foreach (var param in list) {
                        if (param.Name == name)
                            numericUpDown.Value = decimal.Parse(param.Value);
                    }
                }
            }
        }

        private void LoadSettingsFromDatabase(string filePath = null)
        {
            if (filePath == null) {
                filePath = (System.IO.File.Exists(OldDatabaseFilePath) ? OldDatabaseFilePath : DatabaseFilePath);
            } else {
                filePath = (new Regex("'")).Replace(filePath, "");
            }
            Application.DoEvents();
            Logger("Loading settings from " + filePath + "...");
            UpdateLog();
            Application.DoEvents();
            MainForm.Instance.Enabled = false;

            int databaseVersion = 0;
            try {
                using (var conn = new SQLiteConnection("Data Source=" + filePath + ";Version=3;")) {
                    conn.Open();
                    try {
                        var sql = "select * from wallet_addresses";
                        using (var command = new SQLiteCommand(sql, conn)) {
                            using (var reader = command.ExecuteReader()) {
                                while (reader.Read()) {
                                    if ((string)reader["coin"] == "bitcoin")
                                        textBoxBitcoinAddress.Text = (string)reader["address"];
                                    else if ((string)reader["coin"] == "ethereum")
                                        textBoxEthereumAddress.Text = (string)reader["address"];
                                    else if ((string)reader["coin"] == "monero")
                                        textBoxMoneroAddress.Text = (string)reader["address"];
                                    else if ((string)reader["coin"] == "zcash")
                                        textBoxZcashAddress.Text = (string)reader["address"];
                                    else if ((string)reader["coin"] == "pascal")
                                        textBoxPascalAddress.Text = (string)reader["address"];
                                    else if ((string)reader["coin"] == "lbry")
                                        textBoxLbryAddress.Text = (string)reader["address"];
                                }
                            }
                        }
                    } catch (Exception ex) { Logger(ex); }

                    try {
                        var sql = "select * from pools";
                        using (var command = new SQLiteCommand(sql, conn)) {
                            using (var reader = command.ExecuteReader()) {
                                var oldItems = new List<string>();
                                foreach (string poolName in listBoxPoolPriorities.Items)
                                    oldItems.Add(poolName);
                                listBoxPoolPriorities.Items.Clear();
                                while (reader.Read())
                                    listBoxPoolPriorities.Items.Add((string)reader["name"]);
                                foreach (var poolName in oldItems)
                                    if (!listBoxPoolPriorities.Items.Contains(poolName))
                                        listBoxPoolPriorities.Items.Add(poolName);
                            }
                        }
                    } catch (Exception ex) { Logger(ex); }

                    try {
                        var sql = "select * from properties";
                        using (var command = new SQLiteCommand(sql, conn)) {
                            using (var reader = command.ExecuteReader()) {
                                while (reader.Read()) {
                                    var propertyName = (string)reader["name"];
                                    if (propertyName == "database_version") {
                                        databaseVersion = int.Parse((string)reader["value"]);
                                    } else if (propertyName == "coin_to_mine") {
                                        // This field is obsolete.
                                        var coinToMine = (string)reader["value"];
                                        if (coinToMine == "ethereum") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Ethash");
                                        } else if (coinToMine == "ethereum_pascal") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Ethash/Pascal");
                                        } else if (coinToMine == "monero") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("CryptoNight");
                                        } else if (coinToMine == "zcash") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Equihash");
                                        } else if (coinToMine == "lbry") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Lbry");
                                        } else if (coinToMine == "pascal") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Pascal");
                                        } else if (coinToMine == "feathercoin") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("NeoScrypt");
                                        } else if (coinToMine == "monacoin") {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Lyra2REv2");
                                        } else {
                                            comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Ethash/Pascal");
                                        }
                                    } else if ((new System.Text.RegularExpressions.Regex(@"^enable_gpu([0-9]+)$")).Match(propertyName).Success) {
                                        int index = int.Parse((new System.Text.RegularExpressions.Regex(@"^enable_gpu([0-9]+)$")).Match(propertyName).Groups[1].Captures[0].Value);
                                        if (0 <= index && index < Controller.OpenCLDevices.Length)
                                            dataGridViewDevices.Rows[index].Cells["enabled"].Value = (string)reader["value"] == "true";
                                    }

                                    foreach (var comboBox in Utilities.FindAllChildrenByType<ComboBox>(this)) {
                                        var tag = (string)(comboBox.GetType().GetProperty("Tag").GetValue(comboBox));
                                        if (tag == null)
                                            continue;
                                        var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                                        var match = regex.Match(tag);
                                        var type = match.Success ? match.Groups[1].Value : null;
                                        var name = match.Success ? match.Groups[2].Value : null;
                                        if (type == "parameter" && name == propertyName) {
                                            if (name == "currency" && !comboBox.Items.Contains((string)reader["value"]))
                                                comboBox.Items.Add((string)reader["value"]);
                                            try { comboBox.SelectedIndex = comboBox.FindStringExact((string)reader["value"]); } catch (Exception) { }
                                        }
                                    }

                                    foreach (var textBox in Utilities.FindAllChildrenByType<TextBox>(this)) {
                                        var tag = (string)(textBox.GetType().GetProperty("Tag").GetValue(textBox));
                                        if (tag == null)
                                            continue;
                                        var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                                        var match = regex.Match(tag);
                                        var type = match.Success ? match.Groups[1].Value : null;
                                        var name = match.Success ? match.Groups[2].Value : null;
                                        if (type == "parameter" && name == propertyName)
                                            textBox.Text = (string)reader["value"];
                                    }

                                    foreach (var checkBox in Utilities.FindAllChildrenByType<CheckBox>(this)) {
                                        var tag = (string)(checkBox.GetType().GetProperty("Tag").GetValue(checkBox));
                                        if (tag == null)
                                            continue;
                                        var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                                        var match = regex.Match(tag);
                                        var type = match.Success ? match.Groups[1].Value : null;
                                        var name = match.Success ? match.Groups[2].Value : null;
                                        if (type == "parameter" && name == propertyName)
                                            checkBox.Checked = (string)reader["value"] == "true";
                                    }

                                    foreach (var numericUpDown in Utilities.FindAllChildrenByType<NumericUpDown>(this)) {
                                        var tag = (string)(numericUpDown.GetType().GetProperty("Tag").GetValue(numericUpDown));
                                        if (tag == null)
                                            continue;
                                        var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                                        var match = regex.Match(tag);
                                        var type = match.Success ? match.Groups[1].Value : null;
                                        var name = match.Success ? match.Groups[2].Value : null;
                                        if (type == "parameter" && name == propertyName)
                                            numericUpDown.Value = decimal.Parse((string)reader["value"]);
                                    }
                                }
                            }
                        }
                    } catch (Exception ex) {
                        Logger(ex);
                    }

                    try {
                        var sql = "select * from device_parameters";
                        using (var command = new SQLiteCommand(sql, conn)) {
                            using (var reader = command.ExecuteReader()) {
                                while (reader.Read()) {
                                    var deviceID = (int)reader["device_id"];
                                    var deviceVendor = (string)reader["device_vendor"];
                                    var deviceName = (string)reader["device_name"];
                                    var name = (string)reader["parameter_name"];
                                    var value = (string)reader["parameter_value"];
                                    if (deviceID >= Controller.OpenCLDevices.Length || deviceVendor != Controller.OpenCLDevices[deviceID].GetVendor() ||
                                        deviceName != Controller.OpenCLDevices[deviceID].GetName())
                                        continue;

                                    var regex = new System.Text.RegularExpressions.Regex(@"^(" + sAlgorithmListRegexPattern + @"|fan_control|common)_([a-z_0-9]+)$");
                                    var match = regex.Match(name);
                                    var type = match.Success ? match.Groups[1].Value : null;
                                    var parameter = match.Success ? match.Groups[2].Value : null;
                                    var tuple = new Tuple<int, string, string>(deviceID, type, parameter);
                                    try {
                                        if ((new Regex(@"enabled$")).Match(parameter).Success) {
                                            checkBoxDeviceParameterArray[tuple].Checked = (value == "true");
                                        } else if (parameter == "memory_timings_polaris10_seq_misc1"
                                                   || parameter == "memory_timings_polaris10_seq_misc3"
                                                   || parameter == "memory_timings_polaris10_seq_misc8") {
                                            textBoxDeviceParameterArray[tuple].Text = value;
                                        } else if (parameter != null) {
                                            numericUpDownDeviceParameterArray[tuple].Value = decimal.Parse(value);
                                        }
                                    } catch (Exception) {
                                        Logger("Failed to load " + parameter + " = " + value + " as a device parameter.");
                                    }
                                }
                            }
                        }
                    } catch (Exception ex) {
                        Logger(ex);
                    }

                    conn.Close();
                }
                if (databaseVersion == 0) {
                    // Values of intensity were reinterpreted at v1.1.14.
                    foreach (var device in Controller.OpenCLDevices)
                        ResetDeviceSettings(device);
                    checkBoxDisableAutoStartPrompt.Checked = true;
                }
                if (databaseVersion < 2) {
                    foreach (var device in Controller.OpenCLDevices) {
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "target_temperature".ToLower())].Value = (decimal)75;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_temperature".ToLower())].Value = (decimal)85;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "minimum_fan_speed".ToLower())].Value = (decimal)50;
                    }
                }
                if (databaseVersion < 4) {
                    if (checkBoxCustomPool0Enable.Checked
                        || checkBoxCustomPool1Enable.Checked
                        || checkBoxCustomPool2Enable.Checked
                        || checkBoxCustomPool3Enable.Checked)
                        comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact("Custom Pools");
                }
                Logger("Loaded settings.");
                mAreSettingsDirty = false;
            } catch (Exception ex) {
                Logger(ex);
            }

            MainForm.Instance.Enabled = true;
        }

        private void SaveSettingsToDatabase(string filePath = null)
        {
            bool createBackup = filePath == null;
            filePath = ((filePath == null) ? DatabaseFilePath : (new Regex("'")).Replace(filePath, ""));
            Application.DoEvents();
            Logger("Saving settings to " + filePath + "...");
            UpdateLog();
            Application.DoEvents();
            MainForm.Instance.Enabled = false;

            // Delete the old database in case it is corrupt.
            try { System.IO.File.Delete(filePath); } catch (Exception) { }
            try { CreateNewDatabase(filePath); } catch (Exception) { }

            System.Data.SQLite.SQLiteTransaction trans = null;
            try {
                using (var conn = new SQLiteConnection("Data Source=" + filePath + ";Version=3;")) {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    var sql = "insert into pools (name) values (@name)";
                    using (var command = new SQLiteCommand(sql, conn)) {
                        foreach (string poolName in listBoxPoolPriorities.Items) {
                            command.Parameters.AddWithValue("@name", poolName);
                            command.ExecuteNonQuery();
                        }
                    }

                    sql = "insert into properties (name, value) values (@name, @value)";
                    using (var command = new SQLiteCommand(sql, conn)) {
                        command.Parameters.AddWithValue("@name", "database_version");
                        command.Parameters.AddWithValue("@value", "4"); // started with v1.3.0
                        command.ExecuteNonQuery();
                        for (var i = 0; i < Controller.OpenCLDevices.Length; ++i) {
                            command.Parameters.AddWithValue("@name", "enable_gpu" + i);
                            command.Parameters.AddWithValue("@value", (bool)(dataGridViewDevices.Rows[i].Cells["enabled"].Value) ? "true" : "false");
                            command.ExecuteNonQuery();
                        }

                        foreach (var comboBox in Utilities.FindAllChildrenByType<ComboBox>(this)) {
                            var tag = (string)(comboBox.GetType().GetProperty("Tag").GetValue(comboBox));
                            if (tag == null)
                                continue;
                            var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                            var match = regex.Match(tag);
                            var type = match.Success ? match.Groups[1].Value : null;
                            var name = match.Success ? match.Groups[2].Value : null;
                            if (type == "parameter") {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@value", (string)comboBox.SelectedItem);
                                command.ExecuteNonQuery();
                            }
                        }

                        foreach (var textBox in Utilities.FindAllChildrenByType<TextBox>(this)) {
                            var tag = (string)(textBox.GetType().GetProperty("Tag").GetValue(textBox));
                            if (tag == null)
                                continue;
                            var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                            var match = regex.Match(tag);
                            var type = match.Success ? match.Groups[1].Value : null;
                            var name = match.Success ? match.Groups[2].Value : null;
                            if (type == "parameter") {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@value", textBox.Text);
                                command.ExecuteNonQuery();
                            }
                        }

                        foreach (var checkBox in Utilities.FindAllChildrenByType<CheckBox>(this)) {
                            var tag = (string)(checkBox.GetType().GetProperty("Tag").GetValue(checkBox));
                            if (tag == null)
                                continue;
                            var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                            var match = regex.Match(tag);
                            var type = match.Success ? match.Groups[1].Value : null;
                            var name = match.Success ? match.Groups[2].Value : null;
                            if (type == "parameter") {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@value", checkBox.Checked ? "true" : "false");
                                command.ExecuteNonQuery();
                            }
                        }

                        foreach (var numericUpDown in Utilities.FindAllChildrenByType<NumericUpDown>(this)) {
                            var tag = (string)(numericUpDown.GetType().GetProperty("Tag").GetValue(numericUpDown));
                            if (tag == null)
                                continue;
                            var regex = new System.Text.RegularExpressions.Regex(@"^([a-z_0-9]+):([a-z_0-9]+)$");
                            var match = regex.Match(tag);
                            var type = match.Success ? match.Groups[1].Value : null;
                            var name = match.Success ? match.Groups[2].Value : null;
                            if (type == "parameter") {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@value", numericUpDown.Value.ToString());
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "insert into device_parameters (device_id, device_vendor, device_name, parameter_name, parameter_value) values (@device_id, @device_vendor, @device_name, @parameter_name, @parameter_value)";
                    using (var command = new SQLiteCommand(sql, conn)) {
                        foreach (var key in numericUpDownDeviceParameterArray.Keys) {
                            command.Parameters.AddWithValue("@device_id", key.Item1);
                            command.Parameters.AddWithValue("@device_vendor", Controller.OpenCLDevices[key.Item1].GetVendor());
                            command.Parameters.AddWithValue("@device_name", Controller.OpenCLDevices[key.Item1].GetName());

                            command.Parameters.AddWithValue("@parameter_name", key.Item2 + "_" + key.Item3);
                            command.Parameters.AddWithValue("@parameter_value", numericUpDownDeviceParameterArray[key].Value.ToString());
                            command.ExecuteNonQuery();
                        }
                        foreach (var key in checkBoxDeviceParameterArray.Keys) {
                            command.Parameters.AddWithValue("@device_id", key.Item1);
                            command.Parameters.AddWithValue("@device_vendor", Controller.OpenCLDevices[key.Item1].GetVendor());
                            command.Parameters.AddWithValue("@device_name", Controller.OpenCLDevices[key.Item1].GetName());

                            command.Parameters.AddWithValue("@parameter_name", key.Item2 + "_" + key.Item3);
                            command.Parameters.AddWithValue("@parameter_value", checkBoxDeviceParameterArray[key].Checked ? "true" : "false");
                            command.ExecuteNonQuery();
                        }
                        foreach (var key in textBoxDeviceParameterArray.Keys) {
                            command.Parameters.AddWithValue("@device_id", key.Item1);
                            command.Parameters.AddWithValue("@device_vendor", Controller.OpenCLDevices[key.Item1].GetVendor());
                            command.Parameters.AddWithValue("@device_name", Controller.OpenCLDevices[key.Item1].GetName());

                            command.Parameters.AddWithValue("@parameter_name", key.Item2 + "_" + key.Item3);
                            command.Parameters.AddWithValue("@parameter_value", textBoxDeviceParameterArray[key].Text);
                            command.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    conn.Close();
                }
                if (System.IO.File.Exists(OldDatabaseFilePath))
                    System.IO.File.Delete(OldDatabaseFilePath);
                Logger("Saved settings.");
                if (filePath == DatabaseFilePath)
                    mAreSettingsDirty = false;
            } catch (Exception ex) {
                Logger("Exception in UpdateDatabase(): " + ex.Message + ex.StackTrace);
                if (trans != null)
                    trans.Rollback();
            }
            MainForm.Instance.Enabled = true;
            if (createBackup && checkBoxAutomaticBackups.Checked)
                CreateSettingsBackup();
        }

        void CreateSettingsBackup(string name = null)
        {
            if (name == null)
                name = System.DateTime.Now.ToString("yyyy-MM-dd--HHmm") + ".sqlite";
            try {
                System.IO.File.Copy(DatabaseFilePath, SettingsBackupPathBase + "\\" + name);
            } catch (Exception ex) { }
            UpdateSettingBackupList();
        }

        void UpdateSettingBackupList()
        {
            listBoxSettingBackups.Items.Clear();
            var regex = new Regex(@"^.*\\(.*)--(..)(..)\.sqlite$");
            foreach (string file in System.IO.Directory.GetFiles(SettingsBackupPathBase))
                if (regex.Match(file).Success)
                    listBoxSettingBackups.Items.Add(regex.Replace(file, "$1 $2:$3"));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.AboveNormal;

            Logger(appName + " started.");

            try { System.IO.Directory.CreateDirectory(AppDataPathBase); } catch (Exception) { }
            try { System.IO.Directory.CreateDirectory(LogFilePathBase); } catch (Exception) { }
            try { System.IO.Directory.CreateDirectory(SettingsBackupPathBase); } catch (Exception) { }
            try { System.IO.Directory.CreateDirectory(SavedOpenCLBinaryKernelPathBase); } catch (Exception) { }

            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED | NativeMethods.ES_AWAYMODE_REQUIRED);

#if !COMMAND_LINE_VERSION
            CheckVirtualMemorySize();

            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            Application.DoEvents();
#endif          

            try {
                //RunNGen();
            } catch (Exception ex) {
                Logger("Exception in RuznNGen(): " + ex.Message + ex.StackTrace);
            }

            try {
                InitializeDevices();
            } catch (Exception ex) {
                Logger("Exception in InitializeDevices(): " + ex.Message + ex.StackTrace);
            }

            try {
                if (System.IO.File.Exists(DatabaseFilePath))
                    LoadSettingsFromDatabase();
            } catch (Exception ex) {
                Logger("Exception in LoadDatabase(): " + ex.Message + ex.StackTrace);
            }

            ParseCommandLineArguments(Environment.GetCommandLineArgs());

            UpdateSettingBackupList();

            // Do everything to turn off TDR.
            foreach (var controlSet in new string[] { "CurrentControlSet", "ControlSet001" })
                // This shouldn't be necessary but it doesn't work without this.
                foreach (var path in new string[] {
                @"HKEY_LOCAL_MACHINE\System\" + controlSet + @"\Control\GraphicsDrivers",
                @"HKEY_LOCAL_MACHINE\System\" + controlSet + @"\Control\GraphicsDrivers\TdrWatch"
            }) {
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrLevel", 0); } catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrDelay", 60); } catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrDdiDelay", 60); } catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrLimitTime", 60); } catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TdrLimitCount", 256); } catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(path, "TDR_RECOVERY", 0); } catch (Exception) { } // Undocumented but found on Windows 10.
                }

            // Not ideal but absolutely necessary.
            try { Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "ForceQueue", 1); } catch (Exception) { }
            try { Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting\Consent", "DefaultConsent", 1); } catch (Exception) { }
            try { Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "DontShowUI", 1); } catch (Exception) { }

            // Remove garbage left by old versions.
            try {
                if (System.IO.File.Exists(OldLogFilePath))
                    System.IO.File.Delete(OldLogFilePath);
                if (System.IO.File.Exists(OldAppStateFilePath))
                    System.IO.File.Delete(OldAppStateFilePath);
            } catch (Exception) { }

            InitializeShareChart(cartesianChartShare1Minute, 60);
            InitializeShareChart(cartesianChartShare1Hour, 60);
            InitializeShareChart(cartesianChartShare1Day, 24);
            InitializeShareChart(cartesianChartShare1Month, 31);

            for (int i = 0; i < Controller.OpenCLDevices.Length; ++i) {
                var newRow = new DataGridViewRow();
                dataGridViewBenchmarks.Rows.Add();
                dataGridViewBenchmarks.Rows[i].Cells["dataGridViewTextBoxColumnBenchmarksDeviceIndex"].Value = i.ToString();
                dataGridViewBenchmarks.Rows[i].Cells["dataGridViewTextBoxColumnBenchmarksDeviceVendor"].Value = Controller.OpenCLDevices[i].GetVendor();
                dataGridViewBenchmarks.Rows[i].Cells["dataGridViewTextBoxColumnBenchmarksDeviceName"].Value = Controller.OpenCLDevices[i].GetName();
            }

            Controller.AppState = Controller.ApplicationGlobalState.Idle;
            UpdateControls();
            mBackgroundTasksCancellationTokenSource = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Controller.Task_FanControl), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Controller.Task_MemoryTimings), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_UpdateBitcoinRates), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_UpdateAltcoinRates), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_UpdatePoolStats), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_UpdateLatestReleaseInfo), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_UpdateCPUUsage), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_CollectGarbage), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_UpdateShareCharts), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_APIListener), mBackgroundTasksCancellationTokenSource.Token);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task_KillInterferingProcesses), mBackgroundTasksCancellationTokenSource.Token);
            mAreSettingsDirty = false;

            // Auto-start mining if necessary.
#if COMMAND_LINE_VERSION
            var autoStart = true;
#else
            var autoStart = checkBoxAutoStart.Checked;
            splashScreen.Dispose();
            Application.DoEvents();
#endif
            try {
                if (System.IO.File.ReadAllLines(AppStateFilePath)[0] == "Mining")
                    autoStart = true;
            } catch (Exception) { }
            if (autoStart
                && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift)
                && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift)
                && (checkBoxDisableAutoStartPrompt.Checked
                    || MessageBox.Show(Utilities.GetAutoClosingForm(), "Mining will start automatically in 10 seconds.",
                        "Gateless Gate Sharp", MessageBoxButtons.OKCancel) != DialogResult.Cancel)) {
                timerAutoStart.Enabled = true;
            } else {
                try { using (var file = new System.IO.StreamWriter(AppStateFilePath, false)) file.WriteLine("Idle"); } catch (Exception) { }
                buttonStart.Enabled = true;
            }
        }

        #region Devices

        private void InitializeDevices()
        {
            try {
                Controller.OpenCLDevices = OpenCLDevice.GetAllOpenCLDevices();
            } catch (Exception ex) {
                Logger("Exception in OpenCLDevice.GetAllOpenCLDevices(): " + ex.Message + ex.StackTrace);
                MessageBox.Show("Failed to initialize OpenCL devices. Please install appropriate device driver(s) for your graphics cards.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Controller.OpenCLDevices = new OpenCLDevice[] { };
            }
            Logger("Number of Devices: " + Controller.OpenCLDevices.Length);

            ADLInitialized = OpenCLDevice.InitializeADL(Controller.OpenCLDevices);

            try {
                if (ManagedCuda.Nvml.NvmlNativeMethods.nvmlInit() == 0) {
                    Logger("Successfully initialized NVIDIA Management Library.");
                    uint nvmlDeviceCount = 0;
                    ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetCount(ref nvmlDeviceCount);
                    Logger("NVML Device Count: " + nvmlDeviceCount);

                    nvmlDeviceArray = new ManagedCuda.Nvml.nvmlDevice[Controller.OpenCLDevices.Length];
                    for (uint i = 0; i < nvmlDeviceCount; ++i) {
                        var nvmlDevice = new ManagedCuda.Nvml.nvmlDevice();
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetHandleByIndex(i, ref nvmlDevice);
                        var info = new ManagedCuda.Nvml.nvmlPciInfo();
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetPciInfo(nvmlDevice, ref info);

                        uint j;
                        for (j = 0; j < Controller.OpenCLDevices.Length; ++j)
                            if (Controller.OpenCLDevices[j].GetComputeDevice().Vendor == "NVIDIA Corporation" && Controller.OpenCLDevices[j].GetComputeDevice().PciBusIdNV == info.bus) {
                                nvmlDeviceArray[j] = nvmlDevice;
                                break;
                            }
                        if (j >= Controller.OpenCLDevices.Length)
                            throw new Exception();
                    }

                    NVMLInitialized = true;
                }
            } catch (Exception) {
            }
            if (!NVMLInitialized) {
                Logger("Failed to initialize NVIDIA Management Library.");
            } else {
            }

            foreach (var device in Controller.OpenCLDevices) {
                dataGridViewDevices.Rows.Add(new object[] {
                    true,
                    device.DeviceIndex,
                    device.GetVendor(),
                    device.GetName()
                });
            }

            tabPageDeviceArray = new TabPage[Controller.OpenCLDevices.Length];
            buttonDeviceResetToDefaultArray = new Button[Controller.OpenCLDevices.Length];
            buttonDeviceResetAllArray = new Button[Controller.OpenCLDevices.Length];
            buttonDeviceCopyToOthersArray = new Button[Controller.OpenCLDevices.Length];

            for (var i = 0; i < Controller.OpenCLDevices.Length; ++i) {
                DeviceSettingsUserControl.DeviceSettingsUserControl uc = new DeviceSettingsUserControl.DeviceSettingsUserControl();

                uc.Tag = i;
                TabPage tp = new TabPage();
                tp.Controls.Add(uc);
                this.tabControlDeviceSettings.TabPages.Add(tp);

                foreach (var container in Utilities.FindAllChildrenByType<GroupBox>(uc)) {
                    var containerTag = (string)container.Tag;
                    if (containerTag == "fan_control"
                        || containerTag == "overclocking"
                        || containerTag == "memory_timings_polaris10") {
                        foreach (var control in Utilities.FindAllChildrenByType<Control>(container)) {
                            var tag = (string)control.Tag;
                            if (tag != null && tag != string.Empty) {
                                control.Tag = containerTag + "_" + tag;
                            }
                        }
                    }
                }
                foreach (var container in Utilities.FindAllChildrenByType<TabPage>(uc)) {
                    var containerTag = (string)container.Tag;
                    if (sAlgorithmList.Contains(containerTag)) {
                        foreach (var control in Utilities.FindAllChildrenByType<Control>(container)) {
                            var tag = (string)control.Tag;
                            if (tag != null && tag != string.Empty) {
                                control.Tag = containerTag + "_" + tag;
                            }
                        }
                    }
                }

                buttonDeviceResetToDefaultArray[i] = (Button)uc.Controls[0];
                buttonDeviceResetAllArray[i] = (Button)uc.Controls[2];
                buttonDeviceCopyToOthersArray[i] = (Button)uc.Controls[1];
                uc.ButtonResetToDefaultClicked += new EventHandler(DeviceSettingsUserControl_ButtonResetToDefaultClicked);
                uc.ButtonResetAllClicked += new EventHandler(DeviceSettingsUserControl_ButtonResetAllClicked);
                uc.ButtonCopyToOthersClicked += new EventHandler(DeviceSettingsUserControl_ButtonCopyToOthersClicked);
                uc.ValueChanged += new EventHandler(DeviceSettingsUserControl_ValueChanged);

                tabPageDeviceArray[i] = tp;
                foreach (var control in Utilities.FindAllChildrenByType<Control>(uc)) {
                    var tag = control.Tag;
                    if (tag == null)
                        continue;
                    var regex = new System.Text.RegularExpressions.Regex(@"^(" + sAlgorithmListRegexPattern + @"|fan_control|common)_([a-z_0-9]+)$");
                    var match = regex.Match((string)tag);
                    var type = match.Success ? match.Groups[1].Value : null;
                    var parameter = match.Success ? match.Groups[2].Value : null;
                    if (parameter == "algorithms") {
                        tabControlAlgorithmsArray[i] = (TabControl)control;
                    } else if (control.GetType() == typeof(GroupBox)) {
                        groupBoxDeviceParameterArray[new Tuple<int, string, string>(i, type, parameter)] = (GroupBox)control;
                    } else if (control.GetType() == typeof(CheckBox)) {
                        checkBoxDeviceParameterArray[new Tuple<int, string, string>(i, type, parameter)] = ((CheckBox)control);
                    } else if (control.GetType() == typeof(NumericUpDown)) {
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(i, type, parameter)] = ((NumericUpDown)control);
                    } else if (control.GetType() == typeof(TextBox)) {
                        textBoxDeviceParameterArray[new Tuple<int, string, string>(i, type, parameter)] = ((TextBox)control);
                    }
                }
            }

            foreach (var device in Controller.OpenCLDevices)
                ResetDeviceSettings(device);

            UpdateStats();
            timerStatsUpdates.Enabled = true;
        }

        void DeviceSettingsUserControl_ButtonResetToDefaultClicked(object sender, EventArgs e)
        {
            int deviceIndex = (int)(((DeviceSettingsUserControl.DeviceSettingsUserControl)sender).GetType().GetProperty("Tag").GetValue((DeviceSettingsUserControl.DeviceSettingsUserControl)sender));
            ResetDeviceSettings(Controller.OpenCLDevices[deviceIndex]);
        }

        void DeviceSettingsUserControl_ButtonResetAllClicked(object sender, EventArgs e)
        {
            foreach (var device in Controller.OpenCLDevices)
                ResetDeviceSettings(device);
        }

        void DeviceSettingsUserControl_ButtonCopyToOthersClicked(object sender, EventArgs e)
        {
            int deviceIndex = (int)(((DeviceSettingsUserControl.DeviceSettingsUserControl)sender).GetType().GetProperty("Tag").GetValue((DeviceSettingsUserControl.DeviceSettingsUserControl)sender));
            CopyDeviceSettings(deviceIndex);
        }

        void DeviceSettingsUserControl_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void ResetDeviceSettings(Device device)
        {
            tabPageDeviceArray[device.DeviceIndex].Text = "#" + device.DeviceIndex + ": " + device.GetVendor() + " " + device.GetName();

            ResetDeviceFanControlSettings(device);
            ResetDeviceAlgorithmSettings(device);
            ResetDeviceOverclockingSettings(device);
            ResetDeviceMemoryTimingSettings(device);
        }

        private void ResetDeviceAlgorithmSettings(Device device)
        {
            mAreSettingsDirty = true;
            var openCLName = ((OpenCLDevice)device).GetComputeDevice().Name;
            var GCN1 = openCLName == "Capeverde" || openCLName == "Hainan" || openCLName == "Oland" || openCLName == "Pitcairn" || openCLName == "Tahiti";

            // EthashPascal
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash_pascal", "threads")].Value = (decimal)1;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash_pascal", "intensity")].Value = (decimal)1024;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash_pascal", "pascal_iterations")].Value = (decimal)4;

            // Ethash
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash", "threads")].Value = (decimal)1;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash", "intensity")].Value = (decimal)1024;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash", "local_work_size")].Maximum = (decimal)(device.GetVendor() == "NVIDIA" ? 512 : 256);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "ethash", "local_work_size")].Value = (decimal)(device.GetVendor() == "NVIDIA" ? 192 : 192);

            // Lbry
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lbry", "threads")].Value = (decimal)2;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lbry", "intensity")].Value = (decimal)32;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lbry", "local_work_size")].Maximum = (decimal)(device.GetVendor() == "NVIDIA" ? 512 : 256);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lbry", "local_work_size")].Value = (decimal)(device.GetVendor() == "NVIDIA" ? 32 : 64);

            // Lyra2REv2
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lyra2rev2", "threads")].Value = (decimal)2;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lyra2rev2", "intensity")].Value = (decimal)4;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lyra2rev2", "local_work_size")].Maximum = (decimal)(device.GetVendor() == "NVIDIA" ? 512 : 256);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "lyra2rev2", "local_work_size")].Value = (decimal)(device.GetVendor() == "NVIDIA" ? 256 : 256);

            // Pasacal
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "pascal", "threads")].Value = (decimal)2;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "pascal", "intensity")].Value = (decimal)32;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "pascal", "local_work_size")].Maximum = (decimal)(device.GetVendor() == "NVIDIA" ? 512 : 256);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "pascal", "local_work_size")].Value = (decimal)(device.GetVendor() == "NVIDIA" ? 256 : 256);

            // NeoScrypt
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "neoscrypt", "threads")].Value = (decimal)1;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "neoscrypt", "local_work_size")].Maximum = (decimal)(device.GetVendor() == "NVIDIA" ? 512 : 256);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "neoscrypt", "local_work_size")].Value = (decimal)(device.GetVendor() == "NVIDIA" ? 256 : 256);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "neoscrypt", "raw_intensity")].Value
                = (decimal)(device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" ? 8 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" ? 8 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" ? 8 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 580" ? 8 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 Nano" ? 4 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7970" ? 4 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7990" ? 4 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && (new Regex("Vega")).Match(device.GetName()).Success ? 8 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && GCN1 ? 4 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" ? 4 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "NVIDIA" && device.GetName() == "GeForce GTX 1080 Ti" ? 8 * device.GetMaxComputeUnits() :
                                                                                                          8 * device.GetMaxComputeUnits());

            // CryptoNight
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "cryptonight", "threads")].Value = (decimal)(device.GetVendor() == "AMD" && /*!GCN1 &&*/ openCLName != "Fiji" ? 2 : 1);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "cryptonight", "local_work_size")].Value
                = (decimal)(device.GetVendor() == "AMD" && (new Regex("Vega")).Match(device.GetName()).Success ? 16 :
                            device.GetVendor() == "AMD" ? 8 :
                                                          4);
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "cryptonight", "raw_intensity")].Value
                = (decimal)(device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 270X" ? 60 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" && device.MemorySize <= 4L * 1024 * 1024 * 1024 ? 112 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" ? 128 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" && device.MemorySize <= 4L * 1024 * 1024 * 1024 ? 112 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" ? 128 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" && device.MemorySize <= 4L * 1024 * 1024 * 1024 ? 112 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" ? 128 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 580" ? 128 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 Nano" ? 128 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7970" ? 64 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7990" ? 64 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX Vega 56" ? 112 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX Vega 64" ? 112 :
                            device.GetVendor() == "AMD" && (new Regex("Vega")).Match(device.GetName()).Success ? 2 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" && GCN1 ? 4 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "AMD" ? 2 * device.GetMaxComputeUnits() :
                            device.GetVendor() == "NVIDIA" && device.GetName() == "GeForce GTX 1080 Ti" ? 4 * device.GetMaxComputeUnits() :
                                                                                                          2 * device.GetMaxComputeUnits());
        }

        private void ResetDeviceOverclockingSettings(Device device)
        {
            foreach (var algorithm in sAlgorithmList) {
                checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_enabled")].Checked = checkBoxEnableHardwareAccelerationForDefaultSettings.Checked;

                numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_power_limit")].Value = 100;

                int maxCoreClock = ((OpenCLDevice)device).MaxCoreClock; if (maxCoreClock > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Maximum = maxCoreClock;
                int minCoreClock = ((OpenCLDevice)device).MinCoreClock; if (minCoreClock > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Minimum = minCoreClock;
                int coreClockStep = ((OpenCLDevice)device).CoreClockStep; if (coreClockStep > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Increment = coreClockStep;
                int defaultCoreClock = ((OpenCLDevice)device).DefaultCoreClock; if (defaultCoreClock > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Value = defaultCoreClock;

                int maxMemoryClock = ((OpenCLDevice)device).MaxMemoryClock; if (maxMemoryClock > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Maximum = maxMemoryClock;
                int minMemoryClock = ((OpenCLDevice)device).MinMemoryClock; if (minMemoryClock > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Minimum = minMemoryClock;
                int memoryClockStep = ((OpenCLDevice)device).MemoryClockStep; if (memoryClockStep > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Increment = memoryClockStep;
                int defaultMemoryClock = ((OpenCLDevice)device).DefaultMemoryClock; if (defaultMemoryClock > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Value = defaultMemoryClock;

                numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_voltage")].Maximum = 2000;
                int defaultCoreVoltage = ((OpenCLDevice)device).DefaultCoreVoltage; if (defaultCoreVoltage > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_voltage")].Value = defaultCoreVoltage;

                numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_voltage")].Maximum = 2000;
                int defaultMemoryVoltage = ((OpenCLDevice)device).DefaultMemoryVoltage; if (defaultMemoryVoltage > 0) numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_voltage")].Value = defaultMemoryVoltage;

                if (checkBoxEnableHardwareAccelerationForDefaultSettings.Checked) {
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_power_limit")].Value = 120;

                    var newCoreVoltage
                         = (device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 270X" ? defaultCoreVoltage :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" ? 920 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" ? 920 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" ? 1050 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 580" ? 1050 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 Nano" ? 1120 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7970" ? defaultCoreVoltage :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7990" ? 1100 :
                            device.GetVendor() == "NVIDIA" && device.GetName() == "GeForce GTX 1080 Ti" ? defaultCoreVoltage :
                                                                                                          defaultCoreVoltage);
                    if (newCoreVoltage > 0)
                        try { numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_voltage")].Value = (decimal)newCoreVoltage; } catch (Exception) { }

                    var newMemoryVoltage
                         = (device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" ? 960 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" ? 960 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" ? 1000 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 580" ? 1000 :
                                                                                                 defaultMemoryVoltage);
                    if (newMemoryVoltage > 0)
                        try { numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_voltage")].Value = (decimal)newMemoryVoltage; } catch (Exception) { }

                    var newCoreClock
                        = (device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 270X" ? defaultCoreClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" ? defaultCoreClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" ? defaultCoreClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" ? defaultCoreClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 580" ? defaultCoreClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 Nano" ? 1030 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7970" ? defaultCoreClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7990" ? 1000 :
                            device.GetVendor() == "NVIDIA" && device.GetName() == "GeForce GTX 1080 Ti" ? defaultCoreClock :
                                                                                                            defaultCoreClock);
                    if (newCoreClock > 0)
                        try { numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Value = (decimal)newCoreClock; } catch (Exception) { }

                    var newMemoryClock
                        = (device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 270X" ? defaultMemoryClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 470" ? defaultMemoryClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 570" ? defaultMemoryClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 480" ? defaultMemoryClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon RX 580" ? defaultMemoryClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon R9 Nano" ? 500 :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7970" ? defaultMemoryClock :
                            device.GetVendor() == "AMD" && device.GetName() == "Radeon HD 7990" ? defaultMemoryClock :
                            device.GetVendor() == "NVIDIA" && device.GetName() == "GeForce GTX 1080 Ti" ? defaultMemoryClock :
                                                                                                            defaultMemoryClock);
                    if (newMemoryClock > 0)
                        try { numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Value = (decimal)newMemoryClock; } catch (Exception) { }
                }
            }
        }

        private void ResetDeviceMemoryTimingSettings(Device device)
        {
            foreach (var algorithm in sAlgorithmList) {
                bool ethash = (new Regex(@"^ethash")).Match(algorithm).Success;
                checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_enabled")].Checked = checkBoxEnableHardwareAccelerationForDefaultSettings.Checked;
                if (device.GetVendor() == "AMD"
                    && (new System.Text.RegularExpressions.Regex(@"Radeon RX [45][78]0")).Match(device.GetName()).Success
                    && device.MemoryVendor == "Elpida") {

                    if (algorithm == "neoscrypt")
                        checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_enabled")].Checked = false;

                    if (algorithm == "ethash") {
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actrd")].Value = 18;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actwr")].Value = 15;
                    } else {
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actrd")].Value = 18;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actwr")].Value = 15;
                    }
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactrd")].Value = 30;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactwr")].Value = 34;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_ras2ras")].Value = 78;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rp")].Value = 33;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_wrplusrp")].Value = 43;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_bus_turn")].Value = 16;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdw")].Value = 21;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdwa")].Value = 21;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdr")].Value = 26;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdra")].Value = 26;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trrd")].Value = 5;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trc")].Value = 70;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_wra")].Value = 190;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_rda")].Value = 14;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp")].Value = 13;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trfc")].Value = 113;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2w")].Value = 28;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tccdl")].Value = 2;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2r")].Value = 5;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tw2r")].Value = 16;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcl")].Value = 23;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_faw")].Value = 8;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tredc")].Value = 3;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_twedc")].Value = 25;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_t32aw")].Value = 6;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksre")].Value = 2;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksrx")].Value = 2;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke_pulse")].Value = 10;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke")].Value = 10;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle")].Value = 7;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle_ss")].Value = 16;

                    textBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc1")].Text = "20140604";
                    textBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc3")].Text = "AA4089EA";
                    textBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc8")].Text = "C0040003";

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actrd_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actwr_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactrd_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactwr_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_ras2ras_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rp_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_wrplusrp_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_bus_turn_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdw_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdwa_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdr_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdra_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trrd_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trc_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_wra_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_rda_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trfc_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2w_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tccdl_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2r_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tw2r_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcl_enabled")].Checked = false;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_faw_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tredc_enabled")].Checked = false;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_twedc_enabled")].Checked = false;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_t32aw_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksre_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksrx_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke_pulse_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle_ss_enabled")].Checked = false;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc1_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc3_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc8_enabled")].Checked = true;

                } else if (device.GetVendor() == "AMD"
                           && (new System.Text.RegularExpressions.Regex(@"Radeon RX [45][78]0")).Match(device.GetName()).Success
                           /* && device.MemoryVendor == "Samsung" */) {

                    // 555000000000000022CC1C00AD595B41C0570E14B00B450A0068C70003011420FA8900A003000000170E2B34A42A3116
                    if (ethash) { // == "ethash") {
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actrd")].Value = 18;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actwr")].Value = 18;
                    } else {
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actrd")].Value = 18;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actwr")].Value = 18;
                    }
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactrd")].Value = 37;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactwr")].Value = 45;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_ras2ras")].Value   = 140;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rp")].Value        = 33;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_wrplusrp")].Value  = 40;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_bus_turn")].Value  = 16;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdw")].Value     = 17;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdwa")].Value    = 17;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdr")].Value     = 29;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdra")].Value    = 29;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trrd")].Value      = 5;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trc")].Value       = 87;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_wra")].Value   = 63;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_rda")].Value   = 79;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp")].Value       = 13;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trfc")].Value      = 219;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2w")].Value      = 31;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tccdl")].Value     = 3;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2r")].Value      = 5;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tw2r")].Value      = 17;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcl")].Value       = 24;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_faw")].Value       = 0;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tredc")].Value     = 3;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_twedc")].Value     = 25;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_t32aw")].Value     = 0;

                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksre")].Value    = 2;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksrx")].Value    = 2;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke_pulse")].Value = 12;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke")].Value      = 12;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle")].Value  = 7;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle_ss")].Value = 16;

                    textBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc1")].Text = "20140103";
                    textBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc3")].Text = "A00089FA";
                    textBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc8")].Text = "00000003";

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actrd_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_actwr_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactrd_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rasmactwr_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_ras2ras_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_rp_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_wrplusrp_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_bus_turn_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdw_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdwa_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdr_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trcdra_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trrd_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trc_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_wra_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_rda_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trp_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_trfc_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2w_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tccdl_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tr2r_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tw2r_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcl_enabled")].Checked = false;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_faw_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tredc_enabled")].Checked = false;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_twedc_enabled")].Checked = false;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_t32aw_enabled")].Checked = true;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksre_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcksrx_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke_pulse_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_tcke_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_idle_ss_enabled")].Checked = false;

                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc1_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc3_enabled")].Checked = true;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10_seq_misc8_enabled")].Checked = true;
                }
            }
        }

        private void ResetDeviceFanControlSettings(Device device)
        {
            mAreSettingsDirty = true;
            checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "enabled")].Checked = true;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "target_temperature".ToLower())].Value = (decimal)75;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_temperature".ToLower())].Value = (decimal)85;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "minimum_fan_speed".ToLower())].Value = (decimal)50;
            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_fan_speed".ToLower())].Value = (decimal)100;
        }

        private void CopyDeviceSettings(int sourceDeviceIndex)
        {
            mAreSettingsDirty = true;
            foreach (var tuple in numericUpDownDeviceParameterArray.Keys) {
                if (tuple.Item1 != sourceDeviceIndex)
                    continue;
                foreach (var device in Controller.OpenCLDevices) {
                    if (sourceDeviceIndex == device.DeviceIndex
                        || device.GetVendor() != Controller.OpenCLDevices[sourceDeviceIndex].GetVendor()
                        || device.GetName() != Controller.OpenCLDevices[sourceDeviceIndex].GetName())
                        continue;
                    numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, tuple.Item2, tuple.Item3)] = numericUpDownDeviceParameterArray[tuple];
                }
            }
            foreach (var tuple in checkBoxDeviceParameterArray.Keys) {
                if (tuple.Item1 != sourceDeviceIndex)
                    continue;
                foreach (var device in Controller.OpenCLDevices) {
                    if (sourceDeviceIndex == device.DeviceIndex
                        || device.GetVendor() != Controller.OpenCLDevices[sourceDeviceIndex].GetVendor()
                        || device.GetName() != Controller.OpenCLDevices[sourceDeviceIndex].GetName())
                        continue;
                    checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, tuple.Item2, tuple.Item3)] = checkBoxDeviceParameterArray[tuple];
                }
            }
        }


        #endregion

        private class CustomWebClient : System.Net.WebClient
        {
            protected override System.Net.WebRequest GetWebRequest(Uri uri)
            {
                Encoding = System.Text.Encoding.UTF8;
                var request = base.GetWebRequest(uri);
                request.Timeout = 60 * 1000;
                return request;
            }
        }

        static Dictionary<string, double> sBitcoinRates = new Dictionary<string, double> { };
        static Dictionary<string, double> sAltcoinRates = new Dictionary<string, double> { };
        delegate void StringArgReturningVoidDelegate(string text);
        delegate bool NoArgReturningBoolDelegate();
        delegate int NoArgReturningIntDelegate();

        static void Task_UpdateBitcoinRates(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    using (var client = new CustomWebClient()) {
                        var jsonString = client.DownloadString("https://blockchain.info/ticker");
                        var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        foreach (var currency in response.Keys) {
                            var info = (JContainer)response[currency];
                            sBitcoinRates[currency] = (double)info["15m"];
                            if (Instance.comboBoxCurrency.InvokeRequired) {
                                Instance.Invoke(new StringArgReturningVoidDelegate(aCurrency => {
                                    if (!MainForm.Instance.comboBoxCurrency.Items.Contains(aCurrency))
                                        MainForm.Instance.comboBoxCurrency.Items.Add(aCurrency);
                                }), new object[] { currency });
                            }
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update Bitcoin rates.");
                }
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }

        static void Task_UpdateAltcoinRates(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    using (var client = new CustomWebClient()) {
                        var jsonString = client.DownloadString("https://api.coinmarketcap.com/v1/ticker/?convert=USD");
                        var responseArray = JsonConvert.DeserializeObject<JArray>(jsonString);
                        foreach (JContainer coin in responseArray) {
                            try {
                                sAltcoinRates[(string)coin["name"]] = sAltcoinRates[(string)coin["symbol"]] = double.Parse((string)coin["price_btc"], System.Globalization.CultureInfo.InvariantCulture);
                            } catch (Exception) { }
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update altcoin rates.");
                }
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }

        static Dictionary<string, object> sNiceHashProviderStats = null;
        static Dictionary<string, object> sNiceHashGlobalCurrentStats = null;
        static Dictionary<string, object> sEthermineStats = null;
        static Dictionary<string, object> sEthpoolStats = null;
        static Dictionary<string, object> sNanopoolEthereumStats = null;
        static Dictionary<string, object> sNanopoolEthereumEarningStats = null;
        static Dictionary<string, object> sNanopoolMoneroStats = null;
        static Dictionary<string, object> sNanopoolMoneroEarningStats = null;
        static Dictionary<string, object> sDwarfPoolEthereumStats = null;
        static Dictionary<string, object> sDwarfPoolMoneroStats = null;

        static void Task_UpdatePoolStats(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    if (Instance.ValidateBitcoinAddress(false)) {
                        using (var client = new CustomWebClient()) {
                            var jsonString = client.DownloadString("https://api.nicehash.com/api?method=stats.provider&addr=" + Instance.mUserBitcoinAddress);
                            sNiceHashProviderStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                            jsonString = client.DownloadString("https://api.nicehash.com/api?method=stats.global.current");
                            sNiceHashGlobalCurrentStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update NiceHash stats.");
                }
                try {
                    if (Instance.ValidateEthereumAddress(false)) {
                        using (var client = new CustomWebClient()) {
                            var jsonString = client.DownloadString("https://api.ethermine.org/miner/" + Instance.mUserEthereumAddress + "/currentStats");
                            sEthermineStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update ethermine.org stats.");
                }
                try {
                    if (Instance.ValidateEthereumAddress(false)) {
                        using (var client = new CustomWebClient()) {
                            var jsonString = client.DownloadString("http://api.ethpool.org/miner/" + Instance.mUserEthereumAddress + "/currentStats");
                            sEthpoolStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update ethpool.org stats.");
                }
                try {
                    using (var client = new CustomWebClient()) {
                        if (Instance.ValidateEthereumAddress(false)) {
                            var jsonString = client.DownloadString("https://api.nanopool.org/v1/eth/user/" + Instance.mUserEthereumAddress);
                            sNanopoolEthereumStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                            jsonString = client.DownloadString("https://api.nanopool.org/v1/eth/approximated_earnings/1");
                            sNanopoolEthereumEarningStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                        if (Instance.ValidateMoneroAddress(false)) {
                            var jsonString = client.DownloadString("https://api.nanopool.org/v1/xmr/user/" + Instance.mUserMoneroAddress);
                            sNanopoolMoneroStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                            jsonString = client.DownloadString("https://api.nanopool.org/v1/xmr/approximated_earnings/1000");
                            sNanopoolMoneroEarningStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update Nanopool stats.");
                }
                try {
                    using (var client = new CustomWebClient()) {
                        if (Instance.ValidateEthereumAddress(false)) {
                            var jsonString = client.DownloadString("http://dwarfpool.com/eth/api?wallet=" + Instance.mUserEthereumAddress);
                            sDwarfPoolEthereumStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                        if (Instance.ValidateMoneroAddress(false)) {
                            var jsonString = client.DownloadString("http://dwarfpool.com/xmr/api?wallet=" + Instance.mUserMoneroAddress);
                            sDwarfPoolMoneroStats = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                        }
                    }
                } catch (Exception) {
                    Logger("Failed to update DwarfPool stats.");
                }
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }

        public string GetPrettyAlgorithmName(string algorithmName)
        {
            return (algorithmName == "ethash_pascal") ? "Ethash/Pascal" :
                   (algorithmName == "ethash") ? "Ethash" :
                   (algorithmName == "pascal") ? "Pascal" :
                   (algorithmName == "cryptonight") ? "CryptoNight" :
                   (algorithmName == "neoscrypt") ? "NeoScrypt" :
                   (algorithmName == "lbry") ? "Lbry" :
                   (algorithmName == "lyra2rev2") ? "Lyra2REv2" :
                                            null;
        }

        public string DefaultAlgorithm {
            get {
                var selected = (string)comboBoxDefaultAlgorithm.SelectedItem;
                return (selected == "Ethash/Pascal") ? "ethash_pascal" :
                       (selected == "Ethash") ? "ethash" :
                       (selected == "Pascal") ? "pascal" :
                       (selected == "CryptoNight") ? "cryptonight" :
                       (selected == "NeoScrypt") ? "neoscrypt" :
                       (selected == "Lbry") ? "lbry" :
                       (selected == "Lyra2REv2") ? "lyra2rev2" :
                                                       null;
            }
            set {
                comboBoxDefaultAlgorithm.SelectedIndex = comboBoxDefaultAlgorithm.FindStringExact(GetPrettyAlgorithmName(value));
            }
        }

        private void UpdatePoolStats()
        {
            try {
                double totalSpeed = 0;
                foreach (var miner in Controller.Miners)
                    totalSpeed += miner.Speed;
                double secondaryTotalSpeed = 0;
                foreach (var miner in Controller.Miners)
                    secondaryTotalSpeed += miner.SpeedSecondaryAlgorithm;

                string currency = (string)comboBoxCurrency.SelectedItem;
                double BTCRate = sBitcoinRates.Keys.Contains<string>(currency) ? sBitcoinRates[currency] : 0;
                var ETHRate = sBitcoinRates.Keys.Contains<string>(currency) && sAltcoinRates.Keys.Contains<string>("ETH") ? sBitcoinRates[currency] * sAltcoinRates["ETH"] : 0;
                var XMRRate = sBitcoinRates.Keys.Contains<string>(currency) && sAltcoinRates.Keys.Contains<string>("XMR") ? sBitcoinRates[currency] * sAltcoinRates["XMR"] : 0;

                labelPriceDay.Text = "-";
                labelPriceDay.Text = "-";
                labelPriceWeek.Text = "-";
                labelPriceMonth.Text = "-";

                if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "ethash" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 20, totalSpeed, 1000000000.0);
                } else if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "ethash_pascal" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 20, totalSpeed, 1000000000.0, 25, secondaryTotalSpeed, 1000000000000.0);
                } else if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "cryptonight" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 22, totalSpeed, 1000000.0);
                } else if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "lbry" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 23, totalSpeed, 1000000000000.0);
                } else if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "pascal" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 58 - 33, totalSpeed, 1000000000000.0);
                } else if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "neoscrypt" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 41 - 33, totalSpeed, 1000000000.0);
                } else if (mCurrentPool == "NiceHash" && DefaultAlgorithm == "lyra2rev2" && textBoxBitcoinAddress.Text != "") {
                    UpdateProfitabilityInfoForNiceHash(currency, BTCRate, 47 - 33, totalSpeed, 1000000000000.0);
                } else if (mCurrentPool == "ethermine.org" && DefaultAlgorithm == "ethash" && textBoxEthereumAddress.Text != "" && sEthermineStats != null) {
                    var response = sEthermineStats;
                    var data = (JContainer)response["data"];
                    var balance = (double)data["unpaid"] * 1e-18;
                    var averageHashrate = (double)data["averageHashrate"];
                    var coinsPerMin = (double)data["coinsPerMin"];
                    labelBalance.Text = string.Format("{0:N6}", balance) + " ETH (" + string.Format("{0:N2}", balance * ETHRate) + " " + currency + ")";

                    if (Controller.AppState == Controller.ApplicationGlobalState.Mining && averageHashrate != 0) {
                        var price = coinsPerMin * 60 * 24 * (totalSpeed / averageHashrate);
                        UpdateLabelsForProfitability("ETH", price, ETHRate, currency);
                    }
                } else if (mCurrentPool == "ethpool.org" && DefaultAlgorithm == "ethash" && textBoxEthereumAddress.Text != "" && sEthpoolStats != null) {
                    var data = (JContainer)sEthpoolStats["data"];
                    double balance = 0;
                    try {
                        balance = (double)data["unpaid"] * 1e-18;
                    } catch (Exception) { }
                    var averageHashrate = (double)data["averageHashrate"];
                    var coinsPerMin = (double)data["coinsPerMin"];
                    labelBalance.Text = string.Format("{0:N6}", balance) + " ETH (" + string.Format("{0:N2}", balance * ETHRate) + " " + currency + ")";

                    if (Controller.AppState == Controller.ApplicationGlobalState.Mining && averageHashrate != 0) {
                        var price = coinsPerMin * 60 * 24 * (totalSpeed / averageHashrate);
                        UpdateLabelsForProfitability("ETH", price, ETHRate, currency);
                    }
                } else if (mCurrentPool == "Nanopool" && DefaultAlgorithm == "ethash" && textBoxEthereumAddress.Text != "") {
                    var data = (JContainer)sNanopoolEthereumStats["data"];
                    double balance = 0;
                    try {
                        balance = (double)data["balance"];
                    } catch (Exception) { }
                    labelBalance.Text = string.Format("{0:N6}", balance) + " ETH (" + string.Format("{0:N2}", balance * ETHRate) + " " + currency + ")";
                    if (Controller.AppState == Controller.ApplicationGlobalState.Mining && sNanopoolEthereumEarningStats != null) {
                        var earningData = (JContainer)sNanopoolEthereumEarningStats["data"];
                        double price1Day = (double)(((JContainer)earningData["day"])["coins"]) * totalSpeed / 1000000.0;
                        UpdateLabelsForProfitability("ETH", price1Day, ETHRate, currency);
                    }
                } else if (mCurrentPool == "Nanopool" && DefaultAlgorithm == "cryptonight" && textBoxMoneroAddress.Text != "") {
                    var data = (JContainer)sNanopoolMoneroStats["data"];
                    double balance = 0;
                    try {
                        balance = (double)data["balance"];
                    } catch (Exception) { }
                    labelBalance.Text = string.Format("{0:N6}", balance) + " XMR (" + string.Format("{0:N2}", balance * XMRRate) + " " + currency + ")";
                    if (Controller.AppState == Controller.ApplicationGlobalState.Mining && sNanopoolMoneroEarningStats != null) {
                        var earningData = (JContainer)sNanopoolMoneroEarningStats["data"];
                        double price1Day = (double)(((JContainer)earningData["day"])["coins"]) * totalSpeed / 1000.0;
                        UpdateLabelsForProfitability("XMR", price1Day, XMRRate, currency);
                    }
                } else if (mCurrentPool == "DwarfPool" && DefaultAlgorithm == "ethash" && textBoxEthereumAddress.Text != "") {
                    double balance = 0;
                    try {
                        balance = double.Parse((string)sDwarfPoolEthereumStats["wallet_balance"], System.Globalization.CultureInfo.InvariantCulture);
                    } catch (Exception) { }
                    labelBalance.Text = string.Format("{0:N6}", balance) + " ETH (" + string.Format("{0:N2}", balance * ETHRate) + " " + currency + ")";
                    if (Controller.AppState == Controller.ApplicationGlobalState.Mining && sNanopoolEthereumEarningStats != null) { // TODO
                        var earningData = (JContainer)sNanopoolEthereumEarningStats["data"];
                        double price1Day = (double)(((JContainer)earningData["day"])["coins"]) * totalSpeed / 1000000.0;
                        UpdateLabelsForProfitability("ETH", price1Day, ETHRate, currency);
                    }
                } else if (mCurrentPool == "DwarfPool" && DefaultAlgorithm == "cryptonight" && textBoxMoneroAddress.Text != "") {
                    double balance = 0;
                    try {
                        balance = double.Parse((string)sDwarfPoolMoneroStats["wallet_balance"], System.Globalization.CultureInfo.InvariantCulture);
                    } catch (Exception) { }
                    labelBalance.Text = string.Format("{0:N6}", balance) + " XMR (" + string.Format("{0:N2}", balance * XMRRate) + " " + currency + ")";
                    if (Controller.AppState == Controller.ApplicationGlobalState.Mining && sNanopoolMoneroEarningStats != null) { // TODO
                        var earningData = (JContainer)sNanopoolMoneroEarningStats["data"];
                        double price1Day = (double)(((JContainer)earningData["day"])["coins"]) * totalSpeed / 1000.0;
                        UpdateLabelsForProfitability("XMR", price1Day, XMRRate, currency);
                    }
                }
            } catch (Exception) {
                Logger("Failed to update pool statistics.");
            }
        }

        private void UpdateProfitabilityInfoForNiceHash(string currency, double BTCRate, int algo, double totalSpeed, double speedDivisor, int secondaryAlgo = -1, double secondaryTotalSpeed = 0, double secondarySpeedDivisor = 1)
        {
            if (sNiceHashProviderStats == null)
                return;

            double balance = 0;
            var result = (JContainer)sNiceHashProviderStats["result"];
            var stats = (JArray)result["stats"];
            foreach (JContainer item in stats)
                try {
                    balance += double.Parse((string)item["balance"], System.Globalization.CultureInfo.InvariantCulture);
                } catch (Exception) { }
            labelBalance.Text = string.Format("{0:N6}", balance) + " BTC (" + string.Format("{0:N2}", balance * BTCRate) + " " + currency + ")";

            if (Controller.AppState == Controller.ApplicationGlobalState.Mining && textBoxBitcoinAddress.Text != "" && !DevFeeMode) {
                double price = 0;
                result = (JContainer)sNiceHashGlobalCurrentStats["result"];
                stats = (JArray)result["stats"];
                foreach (JContainer item in stats)
                    try {
                        if ((double)item["algo"] == algo)
                            price += double.Parse((string)item["price"], System.Globalization.CultureInfo.InvariantCulture) * totalSpeed / speedDivisor;
                        else if ((double)item["algo"] == secondaryAlgo)
                            price += double.Parse((string)item["price"], System.Globalization.CultureInfo.InvariantCulture) * secondaryTotalSpeed / secondarySpeedDivisor;
                    } catch (Exception) { }
                UpdateLabelsForProfitability("BTC", price, BTCRate, currency);
            }
        }

        void UpdateLabelsForProfitability(string coin, double price, double rate, string currency)
        {
            labelPriceDay.Text = string.Format("{0:N6}", price) + " " + coin + "/Day (" + string.Format("{0:N2}", price * rate) + " " + currency + "/Day)";
            labelPriceWeek.Text = string.Format("{0:N6}", price * 7) + " " + coin + "/Week (" + string.Format("{0:N2}", price * 7 * rate) + " " + currency + "/Week)";
            labelPriceMonth.Text = string.Format("{0:N6}", price * (365.25 / 12)) + " " + coin + "/Month (" + string.Format("{0:N2}", price * (365.25 / 12) * rate) + " " + currency + "/Month)";
        }

        private string ConvertHashrateToString(double totalSpeed)
        {
            if (totalSpeed < 1000)
                return string.Format("{0:N1} h/s", totalSpeed);
            else if (totalSpeed < 10000)
                return string.Format("{0:N0} h/s", totalSpeed);
            else if (totalSpeed < 100000)
                return string.Format("{0:N2} Kh/s", totalSpeed / 1000);
            else if (totalSpeed < 1000000)
                return string.Format("{0:N1} Kh/s", totalSpeed / 1000);
            else if (totalSpeed < 10000000)
                return string.Format("{0:N0} Kh/s", totalSpeed / 1000);
            else if (totalSpeed < 100000000)
                return string.Format("{0:N2} Mh/s", totalSpeed / 1000000);
            else if (totalSpeed < 1000000000)
                return string.Format("{0:N1} Mh/s", totalSpeed / 1000000);
            else
                return string.Format("{0:N0} Mh/s", totalSpeed / 1000000);
        }

        private void UpdateStats()
        {
            if (Controller.AppState == Controller.ApplicationGlobalState.Initializing || Controller.AppState == Controller.ApplicationGlobalState.Switching)
                return;
            UpdateLocalStats();
            UpdatePoolStats();
        }

        private void UpdateLocalStats()
        {
            try { DeviceManagementLibrariesMutex.WaitOne(5000); } catch (Exception) { }
            try {
                // Set the window title.
                Text = appName + " ("
                    + (mLatestReleaseDiff == 0 ? "latest release" :
                       mLatestReleaseDiff == 1 ? ("1 release behind") :
                       mLatestReleaseDiff < 10 ? (mLatestReleaseDiff + " releases behind") :
                                                  (mLatestReleaseDiff + "+ releases behind"))
                    + ")";

                // Pool
                mCurrentPool = (Controller.AppState == Controller.ApplicationGlobalState.Mining && Controller.PrimaryStratum != null) ? (Controller.PrimaryStratum.PoolName) :
                               checkBoxCustomPool0Enable.Checked ? textBoxCustomPool0Host.Text :
                               checkBoxCustomPool1Enable.Checked ? textBoxCustomPool1Host.Text :
                               checkBoxCustomPool2Enable.Checked ? textBoxCustomPool2Host.Text :
                               checkBoxCustomPool3Enable.Checked ? textBoxCustomPool3Host.Text :
                               (string)listBoxPoolPriorities.Items[0];
                var currentSecondaryPool
                             = (Controller.AppState == Controller.ApplicationGlobalState.Mining && Controller.SecondaryStratum != null) ? (Controller.SecondaryStratum.PoolName) :
                               checkBoxCustomPool0Enable.Checked && comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex != 0 ? textBoxCustomPool0SecondaryHost.Text :
                               checkBoxCustomPool1Enable.Checked && comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex != 0 ? textBoxCustomPool1SecondaryHost.Text :
                               checkBoxCustomPool2Enable.Checked && comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex != 0 ? textBoxCustomPool2SecondaryHost.Text :
                               checkBoxCustomPool3Enable.Checked && comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex != 0 ? textBoxCustomPool3SecondaryHost.Text :
                               "";
                if (Controller.AppState == Controller.ApplicationGlobalState.Mining && mDevFeeMode) {
                    labelCurrentPool.Text = "DEVFEE(" + Parameters.DevFeePercentage + "%; " + string.Format("{0:N0}", Parameters.DevFeeDurationInSeconds - (DateTime.Now - mDevFeeModeStartTime).TotalSeconds) + " seconds remaining...)";
                    labelCurrentSecondaryPool.Text = "";
                } else if (Controller.AppState == Controller.ApplicationGlobalState.Mining && !CustomPoolEnabled && Controller.PrimaryStratum != null && Controller.SecondaryStratum != null) {
                    labelCurrentPool.Text = mCurrentPool + " (" + Controller.PrimaryStratum.ServerAddress + ")";
                    labelCurrentSecondaryPool.Text = currentSecondaryPool + " (" + Controller.SecondaryStratum.ServerAddress + ")";
                } else if (Controller.AppState == Controller.ApplicationGlobalState.Mining && !CustomPoolEnabled && Controller.PrimaryStratum != null) {
                    labelCurrentPool.Text = mCurrentPool + " (" + Controller.PrimaryStratum.ServerAddress + ")";
                    labelCurrentSecondaryPool.Text = "";
                } else {
                    labelCurrentPool.Text = mCurrentPool;
                    labelCurrentSecondaryPool.Text = "";
                }

                var benchmarking = Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running;
                var elapsedTimeInSeconds = benchmarking ? (long)Controller.BenchmarkStopwatch.Elapsed.TotalSeconds : (long)Controller.StopWatch.Elapsed.TotalSeconds;
                if (benchmarking) {
                    labelElapsedTime.Text = "Benchmarking...";
                    var remaining = (Controller.CompletedBenchmarkEntries.Count + Controller.BenchmarkEntries.Count) * (Parameters.BenchmarkPreparationDurationInMilliseconds + Parameters.BenchmarkDurationInMilliseconds) / 1000 - elapsedTimeInSeconds;
                    if (remaining > 0)
                        labelElapsedTime.Text += " (" + string.Format("{2:00}:{1:00}:{0:00}", remaining % 60, remaining / 60 % 60, remaining / 60 / 60 % 24) + " remaining)";
                } else {
                    if (elapsedTimeInSeconds == 0) {
                        labelElapsedTime.Text = "-";
                    } else if (elapsedTimeInSeconds >= 24 * 60 * 60) {
                        labelElapsedTime.Text = string.Format("{3} Day{4} {2:00}:{1:00}:{0:00}", elapsedTimeInSeconds % 60, elapsedTimeInSeconds / 60 % 60, elapsedTimeInSeconds / 60 / 60 % 24, elapsedTimeInSeconds / 60 / 60 / 24, elapsedTimeInSeconds / 60 / 60 / 24 == 1 ? "" : "s");
                    } else {
                        labelElapsedTime.Text = string.Format("{2:00}:{1:00}:{0:00}", elapsedTimeInSeconds % 60, elapsedTimeInSeconds / 60 % 60, elapsedTimeInSeconds / 60 / 60 % 24);
                    }
                    if (elapsedTimeInSeconds > 0 && Controller.AppState == Controller.ApplicationGlobalState.Idle)
                        labelElapsedTime.Text = labelElapsedTime.Text + " (Paused)";
                }

                Dictionary<string, double> speeds = new Dictionary<string, double>();
                foreach (var miner in Controller.Miners) {
                    if (!speeds.ContainsKey(miner.PrimaryAlgorithmName)) {
                        speeds.Add(miner.PrimaryAlgorithmName, miner.Speed);
                    } else {
                        speeds[miner.PrimaryAlgorithmName] += miner.Speed;
                    }
                }
                foreach (var miner in Controller.Miners) {
                    if (miner.SecondaryAlgorithmName == null || miner.SecondaryAlgorithmName == "")
                        continue;
                    if (!speeds.ContainsKey(miner.SecondaryAlgorithmName)) {
                        speeds.Add(miner.SecondaryAlgorithmName, miner.SpeedSecondaryAlgorithm);
                    } else {
                        speeds[miner.SecondaryAlgorithmName] += miner.SpeedSecondaryAlgorithm;
                    }
                }
                labelCurrentSpeed.Text = "-";
                foreach (var algorithm in speeds.Keys) {
                    if (labelCurrentSpeed.Text == "-") {
                        labelCurrentSpeed.Text = ConvertHashrateToString(speeds[algorithm]) + " (" + algorithm + ")";
                    } else {
                        labelCurrentSpeed.Text += ", " + ConvertHashrateToString(speeds[algorithm]) + " (" + algorithm + ")";
                    }
                }

                UpdateCharts();

                foreach (var device in Controller.OpenCLDevices) {
                    var computeDevice = device.GetComputeDevice();
                    var deviceIndex = device.DeviceIndex;
                    double speedPrimary = 0, speedSecondary = 0;
                    foreach (var miner in Controller.Miners)
                        if (miner.DeviceIndex == device.DeviceIndex)
                            speedPrimary += miner.Speed;
                    foreach (var miner in Controller.Miners)
                        if (miner.DeviceIndex == device.DeviceIndex && miner.SecondaryAlgorithmName != null && miner.SecondaryAlgorithmName != "")
                            speedSecondary += miner.SpeedSecondaryAlgorithm;

                    dataGridViewDevices.Rows[deviceIndex].Cells["speed"].Value = (Controller.AppState != Controller.ApplicationGlobalState.Mining) ? "-" :
                                                           speedSecondary > 0 ? ConvertHashrateToString(speedPrimary) + ", " + ConvertHashrateToString(speedSecondary) :
                                                                                                         ConvertHashrateToString(speedPrimary);

                    if (Controller.AppState != Controller.ApplicationGlobalState.Mining || Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["shares"].Style.ForeColor = Color.Black;
                        dataGridViewDevices.Rows[deviceIndex].Cells["shares"].Value = "-";
                    } else if (device.AcceptedShares + device.RejectedShares == 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["shares"].Style.ForeColor = Color.Black;
                        dataGridViewDevices.Rows[deviceIndex].Cells["shares"].Value = "0";
                    } else {
                        var acceptanceRate = (double)device.AcceptedShares / (device.AcceptedShares + device.RejectedShares);
                        dataGridViewDevices.Rows[deviceIndex].Cells["shares"].Value = device.AcceptedShares.ToString() + " (" + string.Format("{0:N1}", acceptanceRate * 100) + "%)";
                        dataGridViewDevices.Rows[deviceIndex].Cells["shares"].Style.ForeColor = acceptanceRate >= 0.99 ? Color.Green : acceptanceRate >= 0.95 ? Color.Black : Color.Red; // TODO
                    }

                    // hardware monitoring
                    int temperature = device.Temperature;
                    if (temperature >= 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["Temperature"].Value = temperature.ToString() + "℃";
                        dataGridViewDevices.Rows[deviceIndex].Cells["Temperature"].Style.ForeColor =
                                                                   temperature >= 85 ? Color.Red :
                                                                   temperature >= 75 ? Color.Purple :
                                                                                       Color.Blue;

                    }
                    int fanSpeed = device.FanSpeed;
                    if (fanSpeed >= 0)
                        dataGridViewDevices.Rows[deviceIndex].Cells["fan"].Value = fanSpeed.ToString() + "%";
                    int power = device.Power;
                    if (power >= 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["power"].Value = power.ToString() + "W";
                        dataGridViewDevices.Rows[deviceIndex].Cells["power"].Style.ForeColor = Color.Black;
                    }
                    int powerLimit = device.PowerLimit;
                    if (powerLimit >= 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["power_limit"].Value = "(" + powerLimit.ToString() + "%)";
                        dataGridViewDevices.Rows[deviceIndex].Cells["power_limit"].Style.ForeColor =
                                                                   powerLimit > 100 ? Color.Red :
                                                                   powerLimit < 100 ? Color.Blue :
                                                                                  Color.Black;
                    }
                    int activity = device.Activity;
                    if (activity >= 0)
                        dataGridViewDevices.Rows[deviceIndex].Cells["activity"].Value = activity.ToString() + "%";
                    int coreClock = device.CoreClock;
                    if (coreClock >= 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["core_clock"].Value = coreClock.ToString() + " MHz";
                        dataGridViewDevices.Rows[deviceIndex].Cells["core_clock"].Style.ForeColor =
                                                                   device.DefaultCoreClock < 0 ? Color.Black :
                                                                   coreClock > device.DefaultCoreClock ? Color.Red :
                                                                   coreClock < device.DefaultCoreClock ? Color.Blue :
                                                                                  Color.Black;
                    }
                    int memoryClock = device.MemoryClock;
                    if (memoryClock >= 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_clock"].Value = memoryClock.ToString() + " MHz";
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_clock"].Style.ForeColor =
                                                                    device.DefaultMemoryClock < 0 ? Color.Black :
                                                                   memoryClock > device.DefaultMemoryClock ? Color.Red :
                                                                   memoryClock < device.DefaultMemoryClock ? Color.Blue :
                                                                                  Color.Black;
                    }
                    if (device.MemoryType != null && device.MemoryVendor != null) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_info"].Value = device.MemoryVendor + " " + device.MemoryType;
                    }
                    long memoryUsed = 0;
                    foreach (var miner in Controller.Miners)
                        if (device.DeviceIndex == miner.DeviceIndex)
                            memoryUsed += miner.MemoryUsage;
                    if (memoryUsed > 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_used"].Value = String.Format("{0:0.0}", memoryUsed / 1000000000.0) + "GB";
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_used"].Style.ForeColor = Color.Black;
                    } else {
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_used"].Value = "";
                    }
                    long memoryReserved = device.MemoryUsage;
                    if (memoryReserved > 0) {
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_reserved"].Value = String.Format("{0:0.0}", memoryReserved / 1000000000.0) + "GB";
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_reserved"].Style.ForeColor = Color.Black;
                    } else {
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_reserved"].Value = "";
                    }

                    if (NVMLInitialized && device.GetComputeDevice().Vendor.Equals("NVIDIA Corporation")) {
                        uint temp = 0;
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetTemperature(nvmlDeviceArray[deviceIndex], ManagedCuda.Nvml.nvmlTemperatureSensors.Gpu, ref temp);
                        dataGridViewDevices.Rows[deviceIndex].Cells["Temperature"].Value = temp.ToString() + "℃";
                        dataGridViewDevices.Rows[deviceIndex].Cells["Temperature"].Style.ForeColor =
                                                                   temp >= 80 ? Color.Red :
                                                                   temp >= 60 ? Color.Purple :
                                                                                  Color.Blue;

                        uint nvmlFanSpeed = 0;
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetFanSpeed(nvmlDeviceArray[deviceIndex], ref nvmlFanSpeed);
                        dataGridViewDevices.Rows[deviceIndex].Cells["fan"].Value = nvmlFanSpeed.ToString() + "%";

                        var utilization = new ManagedCuda.Nvml.nvmlUtilization();
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetUtilizationRates(nvmlDeviceArray[deviceIndex], ref utilization);
                        dataGridViewDevices.Rows[deviceIndex].Cells["activity"].Value = utilization.gpu.ToString() + "%";

                        uint clock = 0;
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetClockInfo(nvmlDeviceArray[deviceIndex], ManagedCuda.Nvml.nvmlClockType.Graphics, ref clock);
                        dataGridViewDevices.Rows[deviceIndex].Cells["core_clock"].Value = clock.ToString() + " MHz";
                        ManagedCuda.Nvml.NvmlNativeMethods.nvmlDeviceGetClockInfo(nvmlDeviceArray[deviceIndex], ManagedCuda.Nvml.nvmlClockType.Mem, ref clock);
                        dataGridViewDevices.Rows[deviceIndex].Cells["memory_clock"].Value = clock.ToString() + " MHz";
                    }
                }
            } catch (Exception ex) {
                Logger(ex);
            } finally {
                try { DeviceManagementLibrariesMutex.ReleaseMutex(); } catch (Exception) { }
            }
        }

        public static bool IsAPIEnabled()
        {
            if (Instance.checkBoxAPIEnabled.InvokeRequired) {
                return (bool)Instance.checkBoxAPIEnabled.Invoke(new NoArgReturningBoolDelegate(() => {
                    return Instance.checkBoxAPIEnabled.Checked;
                }));
            } else {
                return Instance.checkBoxAPIEnabled.Checked;
            }
        }

        public static int GetAPIPort()
        {
            if (Instance.numericUpDownAPIPort.InvokeRequired) {
                return (int)Instance.numericUpDownAPIPort.Invoke(new NoArgReturningIntDelegate(() => {
                    return decimal.ToInt32(Instance.numericUpDownAPIPort.Value);
                }));
            } else {
                return decimal.ToInt32(Instance.numericUpDownAPIPort.Value);
            }
        }

        public static IPAddressRange GetAdminIPAddressRange()
        {
            try {
                string text = null;
                if (Instance.textBoxAdminIPRange.InvokeRequired) {
                    text = (string)Instance.textBoxAdminIPRange.Invoke(new NoArgReturningStringDelegate(() => {
                        return Instance.textBoxAdminIPRange.Text;
                    }));
                } else {
                    text = Instance.textBoxAdminIPRange.Text;
                }
                return IPAddressRange.Parse(text);
            } catch (Exception ex) {
                //Logger(ex);
                return null;
            }
        }

        public static IPAddressRange GetAllowedIPAddressRange()
        {
            try {
                string text = null;
                if (Instance.textBoxAllowedIPRange.InvokeRequired) {
                    text = (string)Instance.textBoxAllowedIPRange.Invoke(new NoArgReturningStringDelegate(() => {
                        return Instance.textBoxAllowedIPRange.Text;
                    }));
                } else {
                    text = Instance.textBoxAllowedIPRange.Text;
                }
                return IPAddressRange.Parse(text);
            } catch (Exception ex) {
                // Logger(ex);
                return null;
            }
        }

        public static IPAddressRange GetDeniedIPAddressRange()
        {
            try {
                string text = null;
                if (Instance.textBoxDeniedIPRange.InvokeRequired) {
                    text = (string)Instance.textBoxDeniedIPRange.Invoke(new NoArgReturningStringDelegate(() => {
                        return Instance.textBoxDeniedIPRange.Text;
                    }));
                } else {
                    text = Instance.textBoxDeniedIPRange.Text;
                }
                return IPAddressRange.Parse(text);
            } catch (Exception ex) {
                // Logger(ex);
                return null;
            }
        }

        enum APIMessageCodes
        {
            MSG_INVGPU = 1,
            MSG_ALRENA = 2,
            MSG_ALRDIS = 3,
            MSG_GPUMRE = 4,
            MSG_GPUREN = 5,
            MSG_GPUNON = 6,
            MSG_POOL = 7,
            MSG_NOPOOL = 8,
            MSG_DEVS = 9,
            MSG_NODEVS = 10,
            MSG_SUMM = 11,
            MSG_GPUDIS = 12,
            MSG_GPUREI = 13,
            MSG_INVCMD = 14,
            MSG_MISID = 15,
            MSG_GPUDEV = 17,

            MSG_NUMGPU = 20,

            MSG_VERSION = 22,
            MSG_INVJSON = 23,
            MSG_MISCMD = 24,
            MSG_MISPID = 25,
            MSG_INVPID = 26,
            MSG_SWITCHP = 27,
            MSG_MISVAL = 28,
            MSG_NOADL = 29,
            MSG_NOGPUADL = 30,
            MSG_INVINT = 31,
            MSG_GPUINT = 32,
            MSG_MINECONFIG = 33,
            MSG_GPUMERR = 34,
            MSG_GPUMEM = 35,
            MSG_GPUEERR = 36,
            MSG_GPUENG = 37,
            MSG_GPUVERR = 38,
            MSG_GPUVDDC = 39,
            MSG_GPUFERR = 40,
            MSG_GPUFAN = 41,
            MSG_MISFN = 42,
            MSG_BADFN = 43,
            MSG_SAVED = 44,
            MSG_ACCDENY = 45,
            MSG_ACCOK = 46,
            MSG_ENAPOOL = 47,
            MSG_DISPOOL = 48,
            MSG_ALRENAP = 49,
            MSG_ALRDISP = 50,
            MSG_MISPDP = 52,
            MSG_INVPDP = 53,
            MSG_TOOMANYP = 54,
            MSG_ADDPOOL = 55,

            MSG_NOTIFY = 60,

            MSG_REMLASTP = 66,
            MSG_ACTPOOL = 67,
            MSG_REMPOOL = 68,
            MSG_DEVDETAILS = 69,
            MSG_MINESTATS = 70,
            MSG_MISCHK = 71,
            MSG_CHECK = 72,
            MSG_POOLPRIO = 73,
            MSG_DUPPID = 74,
            MSG_MISBOOL = 75,
            MSG_INVBOOL = 76,
            MSG_FOO = 77,
            MSG_MINECOIN = 78,
            MSG_DEBUGSET = 79,
            MSG_SETCONFIG = 82,
            MSG_UNKCON = 83,
            MSG_INVNUM = 84,
            MSG_CONPAR = 85,
            MSG_CONVAL = 86,

            MSG_NOUSTA = 88,

            MSG_ZERMIS = 94,
            MSG_ZERINV = 95,
            MSG_ZERSUM = 96,
            MSG_ZERNOSUM = 97,

            MSG_BYE = 0x101,

            MSG_INVNEG = 121,
            MSG_SETQUOTA = 122,
            MSG_LOCKOK = 123,
            MSG_LOCKDIS = 124,

            MSG_CHSTRAT = 125,
            MSG_MISSTRAT = 126,
            MSG_INVSTRAT = 127,
            MSG_MISSTRATINT = 128,

            MSG_PROFILE = 129,
            MSG_NOPROFILE = 130,

            MSG_PROFILEEXIST = 131,
            MSG_MISPRD = 132,
            MSG_ADDPROFILE = 133,

            MSG_MISPRID = 134,
            MSG_PRNOEXIST = 135,
            MSG_PRISDEFAULT = 136,
            MSG_PRINUSE = 137,
            MSG_REMPROFILE = 138,

            MSG_CHPOOLPR = 139,

            MSG_INVXINT = 140,
            MSG_GPUXINT = 141,
            MSG_INVRAWINT = 142,
            MSG_GPURAWINT = 143
        }

        public static async void Task_APIListener(object cancellationToken)
        {
            TcpListener server = null;

            Logger("API Listener started.");
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                if (MainForm.IsAPIEnabled()) {
                    try {
                        int port = MainForm.GetAPIPort();
                        var process = new System.Diagnostics.Process();
                        var startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "netsh";
                        startInfo.Arguments = "advfirewall firewall add rule name=\"Open Port " + port + "\" dir=in action=allow protocol=TCP localport=" + port;
                        process.StartInfo = startInfo;
                        process.Start();
                        Thread.Sleep(1000);
                        server = new TcpListener(IPAddress.Any, port);
                        server.Start();

                        Logger("Listening to API requests on port " + port + ".");
                        while (!((CancellationToken)cancellationToken).IsCancellationRequested && IsAPIEnabled()) {
                            var task = server.AcceptTcpClientAsync();
                            while (!task.IsCompleted && !((CancellationToken)cancellationToken).IsCancellationRequested && IsAPIEnabled())
                                Thread.Sleep(1);
                            if (((CancellationToken)cancellationToken).IsCancellationRequested || !IsAPIEnabled())
                                break;
                            var client = await task;
                            var admin = GetAdminIPAddressRange();
                            var allowed = GetAllowedIPAddressRange();
                            var denied = GetDeniedIPAddressRange();
                            var address = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address;
                            var accessAllowed =
                                (allowed != null)
                                && allowed.Contains(address)
                                && (denied == null || !denied.Contains(address));
                            var adminAccessAllowed =
                                (admin != null)
                                && admin.Contains(address)
                                && accessAllowed;
                            if (!accessAllowed) {
                                client.Close();
                                continue;
                            }

                            var childSocketThread = new Thread(() => {
                                try {
                                    Byte[] bytes = new Byte[1024];
                                    String data = null;
                                    NetworkStream stream = client.GetStream();
                                    int i;
                                    while (client.Connected) {
                                        if ((i = stream.Read(bytes, 0, bytes.Length)) <= 0) {
                                            Thread.Sleep(1);
                                            continue;
                                        }
                                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                                        Dictionary<string, object> message = null;
                                        try {
                                            message = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                                        } catch (Exception) { }
                                        string command = null, param = null;
                                        if (message != null) {
                                            try {
                                                command = (string)message["command"];
                                                param = (string)message["param"];
                                            } catch (Exception) { }
                                        }
                                        var unixTimeNow = (ulong)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                        //Logger("API listener received: " + data);

                                        if (!accessAllowed) {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "E" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 45 },
                                                            {"Msg", "Access denied" },
                                                            {"Description", appName } } } } });
                                        } else if (command == "config") {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 33 },
                                                            {"Msg", "sgminer config" },
                                                            {"Description", appName} } } }, {
                                                        "CONFIG", new object[] { new Dictionary<string, object> {
                                                            {"GPU Count", Controller.OpenCLDevices.Length},
                                                            {"Pool Count", ((Controller.PrimaryStratum != null) ? 1 : 0)},
                                                            {"ADL", "Y"},
                                                            {"ADL in use", (MainForm.ADLInitialized ? "Y" : "N")},
                                                            //{"Strategy", "Load Balance"},
                                                            //{"Rotate Period", 0},
                                                            //{"Log Interval", 5},
                                                            {"Device Code", "GPU"},
                                                            {"OS", "Windows"},
                                                            //{"Failover-Only", false},
                                                            //{"Failover Switch Delay", 60},
                                                            //{"ScanTime", 7},
                                                            //{"Queue", 1},
                                                            //{"Expiry", 28}
                                                            } } }, {
                                                        "id", 1 } });
                                        } else if (command == "summary") {
                                            double speed = 0;
                                            int acceptedShares = 0;
                                            int rejectedShares = 0;
                                            double totalHashes = 0;
                                            try {
                                                foreach (var miner in Controller.Miners)
                                                    speed += miner.Speed;
                                                foreach (var device in Controller.OpenCLDevices) {
                                                    acceptedShares += device.AcceptedShares;
                                                    rejectedShares += device.RejectedShares;
                                                    totalHashes += device.TotalHashesPrimaryAlgorithm;
                                                }
                                            } catch (Exception) { }
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 11 },
                                                            {"Msg", "Summary" },
                                                            {"Description", appName } } } }, {
                                                        "SUMMARY", new object[] { new Dictionary<string, object> {
                                                            { "Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds },
                                                            { "MHS av", totalHashes / Controller.StopWatch.Elapsed.TotalSeconds / 1000000.0 },
                                                            { "MHS 5s", speed / 1000000.0 },
                                                            { "KHS av", totalHashes / Controller.StopWatch.Elapsed.TotalSeconds / 1000.0 },
                                                            { "KHS 5s", speed / 1000.0 },
                                                            { "Accepted", acceptedShares },
                                                            { "Rejected", rejectedShares },
                                                            { "Total MH", totalHashes },
                                                            { "Pool Rejected%", (acceptedShares + rejectedShares == 0) ? 0.0 : rejectedShares / (acceptedShares + rejectedShares) * 100.0 },
                                                            //{ "Found Blocks", 0 },
                                                            //{ "Getworks", 5 },
                                                            /*
                                                            { "Hardware Errors", 0 },
                                                            { "Utility", 2.496 },
                                                            { "Discarded", 108 },
                                                            { "Stale", 0 },
                                                            { "Get Failures", 1 },
                                                            { "Local Work", 295 },
                                                            { "Remote Failures", 0 },
                                                            { "Network Blocks", 1 },
                                                            { "Work Utility", 2.496 },
                                                            { "Difficulty Accepted", 5.00000000 },
                                                            { "Difficulty Rejected", 0.00000000 },
                                                            { "Difficulty Stale", 0.00000000 },
                                                            { "Best Share", 2960.443637 },
                                                            { "Device Hardware%", 0.0000 },
                                                            { "Device Rejected%", 0.0000 },
                                                            { "Pool Stale%", 0.0000 },
                                                            { "Last getwork", unixTimeNow }
                                                            */
                                                        } } }, {
                                                        "id", 1 } });
                                        } else if (command == "devs") {
                                            object[] devices = new object[Controller.OpenCLDevices.Length];
                                            foreach (var device in Controller.OpenCLDevices) {
                                                double speed = 0, averageSpeed = 0;
                                                try {
                                                    foreach (var miner in Controller.Miners)
                                                        if (miner.DeviceIndex == device.DeviceIndex)
                                                            speed += miner.Speed;
                                                    averageSpeed = device.TotalHashesPrimaryAlgorithm / Controller.StopWatch.Elapsed.TotalSeconds;
                                                } catch (Exception) { }
                                                devices[device.DeviceIndex] = new Dictionary<string, object> {
                                                    { "GPU", device.DeviceIndex },
                                                    { "Enabled", "Y" },
                                                    { "Status", "Alive" },
                                                    { "Temperature", device.Temperature },
                                                    { "Fan Percent", device.FanSpeed },
                                                    { "GPU Clock", device.CoreClock },
                                                    { "Memory Clock", device.MemoryClock },
                                                    { "GPU Voltage", device.CoreVoltage },
                                                    { "GPU Activity", device.Activity },
                                                    { "MHS av", averageSpeed / 1000000.0 },
                                                    { "MHS 5s", speed / 1000000.0 },
                                                    { "KHS av", averageSpeed / 1000.0 },
                                                    { "KHS 5s", speed / 1000.0 },
                                                    { "Accepted", device.AcceptedShares },
                                                    { "Rejected", device.RejectedShares },
                                                    { "Total MH", device.TotalHashesPrimaryAlgorithm },
                                                    { "Device Rejected%", (device.AcceptedShares + device.RejectedShares == 0) ? 0.0 : device.RejectedShares / (device.AcceptedShares + device.RejectedShares) * 100.0},
                                                    { "Device Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds }
                                                    /*
                                                    { "Fan Speed", 862 },
                                                    { "Powertune", 74.00 },
                                                    { "Hardware Errors", 0 },
                                                    { "Utility", 2.496 },

                                                    { "Intensity", "8" },
                                                    { "XIntensity", 0 },
                                                    { "RawIntensity", 0 },

                                                    { "Last Share Pool", 0 },
                                                    { "Last Share Time", unixTimeNow },
                                                    
                                                    { "Diff1 Work", 7.000000},
                                                    { "Difficulty Accepted", 7.00000000},
                                                    { "Difficulty Rejected", 0.00000000},
                                                    { "Last Share Difficulty", 1.00000000},
                                                    { "Last Valid Work", 1517380047},
                                                    { "Device Hardware%", 0.0000},
                                                    */
                                                };
                                            }
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 9 },
                                                            {"Msg", Controller.OpenCLDevices.Length + " GPU(s)" },
                                                            {"Description", appName } } } }, {
                                                        "DEVS", devices }, {
                                                        "id", 1 } });
                                        } else if (data == "{\"command\":\"pools\"}") {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 7 },
                                                            {"Msg", ((Controller.PrimaryStratum != null) ? 1 : 0) + " Pool(s)" },
                                                            {"Description", appName } } } }, {
                                                        "POOLS", (Controller.PrimaryStratum == null)
                                                            ? null
                                                            : new object[] { new Dictionary<string, object> {
                                                                {"POOL", 0},
                                                                { "Name", Controller.PrimaryStratum.ServerAddress },
                                                                { "URL", "stratum+tcp://" + Controller.PrimaryStratum.ServerAddress + ":" + Controller.PrimaryStratum.ServerPort },
                                                                { "Profile", ""},
                                                                { "Algorithm", Controller.PrimaryStratum.AlgorithmName },
                                                                { "Description", Controller.PrimaryStratum.PoolName },
                                                                { "Status", "Alive"},
                                                                /*
                                                                 * { "Priority", 0},
                                                                { "Quota", 99},
                                                                { "Long Poll", "N"},
                                                                { "Getworks", 1},
                                                                { "Accepted", 2},
                                                                { "Rejected", 0},
                                                                { "Works", 86},
                                                                { "Discarded", 84},
                                                                { "Stale", 0},
                                                                { "Get Failures", 0},
                                                                { "Remote Failures", 0},
                                                                { "User", "t1NwUDeSKu4BxkD58mtEYKDjzw5toiLfmCu"},
                                                                { "Last Share Time", unixTimeNow},
                                                                { "Diff1 Shares", 2.000000},
                                                                { "Proxy Type", ""},
                                                                { "Proxy", ""},
                                                                { "Difficulty Accepted", 2.00000000},
                                                                { "Difficulty Rejected", 0.00000000},
                                                                { "Difficulty Stale", 0.00000000},
                                                                { "Last Share Difficulty", 1.00000000},
                                                                { "Has Stratum", true},
                                                                { "Stratum Active", true},
                                                                { "Stratum URL", "us1-zcash.flypool.org"},
                                                                { "Has GBT", false},
                                                                { "Best Share", 1298.459805},
                                                                { "Pool Rejected%", 0.0000},
                                                                { "Pool Stale%", 0.0000},
                                                                */
                                                        } } }, {
                                                        "id", 1 } });
                                        } else if (data == "{\"command\":\"coin\"}") {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 78 },
                                                            {"Msg", "sgminer coin" },
                                                            {"Description", appName } } } }, {
                                                        "COIN", new object[] { new Dictionary<string, object> {
                                                            { "Hash Method", (Controller.PrimaryStratum == null) ? "" : Controller.PrimaryStratum.AlgorithmName },
                                                            //{ "Current Block Time", 1517385796.332927},
                                                            //{ "Current Block Hash", "0000000000000000000000000000000000000000000000000000000000000000"},
                                                            //{ "LP", true},
                                                            //{ "Network Difficulty", 0.00000000},
                                                        } } }, {
                                                        "id", 1 } });
                                        } else if (data == "{\"command\":\"notify\"}") {
                                            object[] devices = new object[Controller.OpenCLDevices.Length];
                                            foreach (var device in Controller.OpenCLDevices) {
                                                devices[device.DeviceIndex] = new Dictionary<string, object> {
                                                            { "NOTIFY", device.DeviceIndex},
                                                            { "Name", device.GetVendor() + " " + device.GetName() },
                                                            { "ID", device.DeviceIndex },
                                                            /*
                                                            { "Last Well", unixTimeNow},
                                                            { "Last Not Well", 0},
                                                            { "Reason Not Well", "None"},
                                                            { "*Thread Fail Init", 0},
                                                            { "*Thread Zero Hash", 0},
                                                            { "*Thread Fail Queue", 0},
                                                            { "*Dev Sick Idle 60s", 0},
                                                            { "*Dev Dead Idle 600s", 0},
                                                            { "*Dev Nostart", 0},
                                                            { "*Dev Over Heat", 0},
                                                            { "*Dev Thermal Cutoff", 0},
                                                            { "*Dev Comms Error", 0},
                                                            { "*Dev Throttle", 0},
                                                            */
                                                };
                                            }
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 60 },
                                                            {"Msg", "Notify" },
                                                            {"Description", appName } } } }, {
                                                        "NOTIFY", devices }, {
                                                        "id", 1 } });
                                        } else if (data == "{\"command\":\"stats\"}") {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "S" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 70 },
                                                            {"Msg", "sgminer stats" },
                                                            {"Description", appName } } } }, {
                                                        "NOTIFY", null
                                                        /*new object[] {
                                                            new Dictionary<string, object> { {"STATS", 0}, {"ID", "GPU0"}, {"Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds }, {"Calls", 954763}, {"Wait", 34.141733}, {"Max", 5.000288}, {"Min", 0.000000}, },
                                                            new Dictionary<string, object> { {"STATS", 1}, {"ID", "GPU1"}, {"Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds }, {"Calls", 913823}, {"Wait", 33.403002}, {"Max", 4.994387}, {"Min", 0.000000}, },
                                                            new Dictionary<string, object> { {"STATS", 2}, {"ID", "GPU2"}, {"Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds }, {"Calls", 1185434}, {"Wait", 34.380935}, {"Max", 4.998388}, {"Min", 0.000000}, },
                                                            new Dictionary<string, object> { {"STATS", 3}, {"ID", "POOL0"}, {"Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds }, {"Calls", 3031862}, {"Wait", 96.887867}, {"Max", 5.000288}, {"Min", 0.000000}, {"Pool Calls", 0}, {"Pool Attempts", 0}, {"Pool Wait", 0.000000}, {"Pool Max", 0.000000}, {"Pool Min", 99999999.000000}, {"Pool Av", 0.000000}, {"Work Had Roll Time", false}, {"Work Can Roll", false}, {"Work Had Expire", false}, {"Work Roll Time", 0}, {"Work Diff", 1.00000000}, {"Min Diff", 1.00000000}, {"Max Diff", 1.00000000}, {"Min Diff Count", 13870}, {"Max Diff Count", 13870}, {"Times Sent", 358}, {"Bytes Sent", 1009486}, {"Times Recv", 442}, {"Bytes Recv", 34063}, {"Net Bytes Sent", 1009486}, {"Net Bytes Recv", 34063}, },
                                                            new Dictionary<string, object> { {"STATS", 4}, {"ID", "POOL1"}, {"Elapsed", (ulong)Controller.StopWatch.Elapsed.TotalSeconds }, {"Calls", 22151}, {"Wait", 5.037803}, {"Max", 4.992286}, {"Min", 0.000000}, {"Pool Calls", 0}, {"Pool Attempts", 0}, {"Pool Wait", 0.000000}, {"Pool Max", 0.000000}, {"Pool Min", 99999999.000000}, {"Pool Av", 0.000000}, {"Work Had Roll Time", false}, {"Work Can Roll", false}, {"Work Had Expire", false}, {"Work Roll Time", 0}, {"Work Diff", 1.00000000}, {"Min Diff", 1.00000000}, {"Max Diff", 1.00000000}, {"Min Diff Count", 192}, {"Max Diff Count", 192}, {"Times Sent", 91}, {"Bytes Sent", 18166}, {"Times Recv", 255}, {"Bytes Recv", 46804}, {"Net Bytes Sent", 18166}, {"Net Bytes Recv", 46804 } },
                                                        }*/ }, {
                                                        "id", 1 } });
                                        } else if (data == "{\"command\":\"privileged\"}") {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "E" },
                                                            {"When", unixTimeNow },
                                                            {"Code", 45 },
                                                            {"Msg", "Access denied to 'privileged' command" },
                                                            {"Description", appName } } } } });
                                        } else {
                                            data = JsonConvert.SerializeObject(new Dictionary<string, object> { {
                                                        "STATUS", new object[] { new Dictionary<string, object> {
                                                            {"STATUS", "E" },
                                                            {"When", unixTimeNow },
                                                            {"Code", APIMessageCodes.MSG_INVCMD },
                                                            {"Msg", "Invalid command" },
                                                            {"Description", appName } } } } });
                                        }

                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                                        stream.Write(msg, 0, msg.Length);
                                        //Logger("API Listener Sent: " + data);
                                        break;
                                    }
                                } catch (Exception ex) {
                                    Logger(ex);
                                } finally {
                                    client.Close();
                                }
                            });
                            childSocketThread.Start();
                        }
                    } catch (Exception ex) {
                        Logger(ex);
                    } finally {
                        server.Stop();
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                timerDevFee.Enabled = false;
                timerStatsUpdates.Enabled = false;
                timerUpdateLog.Enabled = false;
                timerWatchdog.Enabled = false;
                mBackgroundTasksCancellationTokenSource.Cancel();

                if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {
                    buttonRunBenchmarks_Click();
                } else if (Controller.AppState == Controller.ApplicationGlobalState.Mining) {
                    StopMiners(false);
                }
                if (e.CloseReason == CloseReason.UserClosing) {
                    try { using (var file = new System.IO.StreamWriter(AppStateFilePath, false)) file.WriteLine("Idle"); } catch (Exception) { }
                    Program.KillMonitor = true;
                }

                if (mAreSettingsDirty)
                    SaveSettingsToDatabase();
                //if (ADLInitialized && null != ADL.ADL_Main_Control_Destroy)
                //    ADL.ADL_Main_Control_Destroy();
                if (PCIExpress.Available)
                    PCIExpress.UnloadPhyMem();
                foreach (var device in Controller.OpenCLDevices)
                    device.Dispose();
                Controller.OpenCLDevices = null;

                mBackgroundTasksCancellationTokenSource.Dispose();
            } catch (Exception ex) { Logger(ex); }
        }

        private void timerDeviceStatusUpdates_Tick(object sender, EventArgs e)
        {
            try {
                UpdateStats();
            } catch (Exception ex) {
                Logger(ex);
            }
        }

        void UpdateCharts()
        {
            try {
                if (!mChartsInitialized) {
                    InitializeChart(cartesianChartTemperature, value => value + "℃", 100, ChartType.Device);
                    InitializeChart(cartesianChartSpeedPrimaryAlgorithm, ConvertHashrateToString, double.NaN, ChartType.Device);
                    InitializeChart(cartesianChartSpeedSecondaryAlgorithm, ConvertHashrateToString, double.NaN, ChartType.Device);
                    InitializeChart(cartesianChartFanSpeed, value => value + "%", 100, ChartType.Device);
                    InitializeChart(cartesianChartDeviceActivity, value => value + "%", 100, ChartType.Device);
                    InitializeChart(cartesianChartPower, value => (value < 0 ? "" : value + "W"), double.NaN, ChartType.Device);
                    InitializeChart(cartesianChartCPUUsage, value => value + "%", 100, ChartType.Total);
                    mChartsInitialized = true;
                }

                UpdateChart(cartesianChartTemperature, deviceIndex => Controller.OpenCLDevices[deviceIndex].Temperature, ChartType.Device);
                UpdateChart(cartesianChartFanSpeed, deviceIndex => Controller.OpenCLDevices[deviceIndex].FanSpeed, ChartType.Device);
                UpdateChart(cartesianChartDeviceActivity, deviceIndex => Controller.OpenCLDevices[deviceIndex].Activity, ChartType.Device);
                UpdateChart(cartesianChartPower, deviceIndex => (Controller.OpenCLDevices[deviceIndex].Power < 0 ? 0 : Controller.OpenCLDevices[deviceIndex].Power), ChartType.Device);
                UpdateChart(cartesianChartCPUUsage, dummy => (mCPUUsage), ChartType.Total);

                UpdateChart(cartesianChartSpeedPrimaryAlgorithm,
                    (int deviceIndex) => {
                        double speed = 0;
                        foreach (var miner in Controller.Miners)
                            speed += (deviceIndex == miner.DeviceIndex) ? miner.Speed : 0;
                        return speed;
                    }, ChartType.Device, 1);
                UpdateChart(cartesianChartSpeedSecondaryAlgorithm,
                    (int deviceIndex) => {
                        double speed = 0;
                        foreach (var miner in Controller.Miners)
                            speed += (deviceIndex == miner.DeviceIndex) ? miner.SpeedSecondaryAlgorithm : 0;
                        return speed;
                    }, ChartType.Device, 1);
            } catch (Exception ex) { Logger(ex); }
        }

        void InitializeChart(LiveCharts.WinForms.CartesianChart chart, System.Func<double, string> formatter, double maxValue, ChartType type)
        {
            int numSerieses = (type == ChartType.Device) ? Controller.OpenCLDevices.Length :
                              (type == ChartType.Total) ? 1 :
                              (type == ChartType.Algorithm) ? sAlgorithmList.Length :
                                                              Controller.OpenCLDevices.Length;

            chart.DisableAnimations = true;
            chart.AxisX.Clear();
            chart.AxisX.Add(new Axis {
                LabelFormatter = value => new System.DateTime((long)value).ToString("yyyy-MM-dd HH:mm:ss"),
                ShowLabels = false,
                MinValue = System.DateTime.Now.Ticks - TimeSpan.FromSeconds(60).Ticks
            });
            //
            chart.AxisY.Clear();
            chart.AxisY.Add(new Axis {
                DisableAnimations = false,
                LabelFormatter = formatter,
                Separator = new Separator {
                    Stroke = System.Windows.Media.Brushes.DarkGray,
                    StrokeThickness = 1,
                },
                MaxValue = maxValue,
                MinValue = 0
            });
            //
            chart.Series.Clear();
            for (int i = 0; i < numSerieses; ++i) {
                chart.Series.Add(
                    new LiveCharts.Wpf.LineSeries {
                        Title = (type == ChartType.Device) ? "Device #" + i + ": " + Controller.OpenCLDevices[i].GetVendor() + " " + Controller.OpenCLDevices[i].GetName() :
                                (type == ChartType.Total) ? "Total" :
                                (type == ChartType.Algorithm) ? sAlgorithmList[i] :
                                                                "",
                        Values = new ChartValues<MeasureModel>(),
                        PointGeometry = null,
                        LineSmoothness = 0,
                        Fill = System.Windows.Media.Brushes.Transparent
                    }
                );
            }
        }

        void InitializeShareChart(LiveCharts.WinForms.CartesianChart chart, int size)
        {
            chart.DisableAnimations = true;
            chart.AxisX.Clear();
            chart.AxisX.Add(new Axis {
                ShowLabels = false,
                Labels = new List<string> { }
            });
            for (int j = 0; j < size; ++j)
                chart.AxisX[0].Labels.Add("");
            //
            chart.AxisY.Clear();
            chart.AxisY.Add(new Axis {
                DisableAnimations = false,
                Separator = new Separator {
                    Stroke = System.Windows.Media.Brushes.DarkGray,
                    StrokeThickness = 1,
                },
                MinValue = 0
            });
            //
            chart.Series.Clear();
            for (int i = 0; i < 2; ++i) {
                var values = new ChartValues<int> { };
                for (int j = 0; j < size; ++j)
                    values.Add(0);
                chart.Series.Add(
                    new LiveCharts.Wpf.StackedColumnSeries {
                        Title = (i == 0) ? "Accepted" : "Rejected",
                        Values = values,
                        PointGeometry = null,
                        Fill = (i == 0) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red
                    }
                );
            }
        }

        delegate void NoArgReturningVoidDelegate();
        delegate string NoArgReturningStringDelegate();
        delegate uint NoArgReturningUIntDelegate();

        void Task_UpdateShareCharts(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            int counter = 0;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    if (cartesianChartShare1Minute.InvokeRequired) {
                        cartesianChartShare1Minute.Invoke(new NoArgReturningVoidDelegate(() => {
                            AddNewValueToShareChart(cartesianChartShare1Minute);
                        }));
                    } else {
                        AddNewValueToShareChart(cartesianChartShare1Minute);
                    }
                    if (counter % 60 == 0) {
                        if (cartesianChartShare1Hour.InvokeRequired) {
                            cartesianChartShare1Hour.Invoke(new NoArgReturningVoidDelegate(() => {
                                AddNewValueToShareChart(cartesianChartShare1Hour);
                            }));
                        } else {
                            AddNewValueToShareChart(cartesianChartShare1Hour);
                        }
                    }
                    if (counter % (60 * 60) == 0) {
                        if (cartesianChartShare1Day.InvokeRequired) {
                            cartesianChartShare1Day.Invoke(new NoArgReturningVoidDelegate(() => {
                                AddNewValueToShareChart(cartesianChartShare1Day);
                            }));
                        } else {
                            AddNewValueToShareChart(cartesianChartShare1Day);
                        }
                    }
                    if (counter % (60 * 60 * 24) == 0) {
                        if (cartesianChartShare1Month.InvokeRequired) {
                            cartesianChartShare1Month.Invoke(new NoArgReturningVoidDelegate(() => {
                                AddNewValueToShareChart(cartesianChartShare1Month);
                            }));
                        } else {
                            AddNewValueToShareChart(cartesianChartShare1Month);
                        }
                    }
                } catch (Exception ex) { Logger(ex); }
                ++counter;
                System.Threading.Thread.Sleep(1000);
            }
        }

        delegate void IntArgReturningVoidDelegate(int i);

        public void ReportAcceptedShare()
        {
            AddShareToShareChart(cartesianChartShare1Minute, 0);
            AddShareToShareChart(cartesianChartShare1Hour, 0);
            AddShareToShareChart(cartesianChartShare1Day, 0);
            AddShareToShareChart(cartesianChartShare1Month, 0);
        }

        public void ReportRejectedShare()
        {
            AddShareToShareChart(cartesianChartShare1Minute, 1);
            AddShareToShareChart(cartesianChartShare1Hour, 1);
            AddShareToShareChart(cartesianChartShare1Day, 1);
            AddShareToShareChart(cartesianChartShare1Month, 1);
        }

        void AddShareToShareChart(LiveCharts.WinForms.CartesianChart chart, int index)
        {
            if (chart.InvokeRequired) {
                chart.Invoke(new IntArgReturningVoidDelegate((i) => {
                    var count = ((LiveCharts.Wpf.StackedColumnSeries)(chart.Series[i])).Values.Count;
                    var value = (int)(((LiveCharts.Wpf.StackedColumnSeries)(chart.Series[i])).Values[count - 1]);
                    ((LiveCharts.Wpf.StackedColumnSeries)(chart.Series[i])).Values[count - 1] = value + 1;
                }), new object[] { index });
            } else {
                var count = ((LiveCharts.Wpf.StackedColumnSeries)(chart.Series[index])).Values.Count;
                var value = (int)(((LiveCharts.Wpf.StackedColumnSeries)(chart.Series[index])).Values[count - 1]);
                ((LiveCharts.Wpf.StackedColumnSeries)(chart.Series[index])).Values[count - 1] = value + 1;
            }
        }

        void AddNewValueToShareChart(LiveCharts.WinForms.CartesianChart chart)
        {
            chart.Series[0].Values.RemoveAt(0);
            chart.Series[1].Values.RemoveAt(0);
            chart.AxisX[0].Labels.RemoveAt(0);

            chart.Series[0].Values.Add(0);
            chart.Series[1].Values.Add(0);
            chart.AxisX[0].Labels.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        enum ChartType { Device, Total, Algorithm }

        void UpdateChart(LiveCharts.WinForms.CartesianChart chart, System.Func<int, double> deviceIndexToValue, ChartType type, int chartIndex = 0)
        {
            var now = System.DateTime.Now;
            var numSerieses = (type == ChartType.Device) ? Controller.OpenCLDevices.Length :
                              (type == ChartType.Total) ? 1 :
                              (type == ChartType.Algorithm) ? sAlgorithmList.Length :
                                                              Controller.OpenCLDevices.Length;

            for (int i = 0; i < numSerieses; ++i) {
                chart.Series[i].Values.Add(new MeasureModel {
                    DateTime = now,
                    Value = deviceIndexToValue(i)
                });
                int valueIndex = chart.Series[i].Values.Count - 1;
                valueIndex = chart.Series[i].Values.Count - 1 - 60; if (valueIndex >= 0 && ((MeasureModel)chart.Series[i].Values[valueIndex]).DateTime.Second != 0) chart.Series[i].Values.RemoveAt(valueIndex);
                valueIndex = chart.Series[i].Values.Count - 1 - 60 - 60; if (valueIndex >= 0 && ((MeasureModel)chart.Series[i].Values[valueIndex]).DateTime.Minute != 0) chart.Series[i].Values.RemoveAt(valueIndex);
                valueIndex = chart.Series[i].Values.Count - 1 - 60 - 60 - 24; if (valueIndex >= 0 && ((MeasureModel)chart.Series[i].Values[valueIndex]).DateTime.Hour != 0) chart.Series[i].Values.RemoveAt(valueIndex);
                while (chart.Series[i].Values.Count > 60 + 60 + 24 + 365) // Keep data for one year.
                    chart.Series[i].Values.RemoveAt(0);
            }
            //
            string coverage = (chartIndex == 0) ? (string)comboBoxGraphCoverage.Items[comboBoxGraphCoverage.SelectedIndex] : (string)comboBoxSecondGraphCoverage.Items[comboBoxSecondGraphCoverage.SelectedIndex];
            chart.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(0).Ticks;
            if (coverage == "1 Minute") {
                chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(60).Ticks;
            } else if (coverage == "1 Hour") {
                chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(60 * 60).Ticks;
            } else if (coverage == "1 Day") {
                chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(60 * 60 * 24).Ticks;
            } else if (coverage == "1 Month") {
                chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(60 * 60 * 24 * 31).Ticks;
            } else if (coverage == "1 Year") {
                chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(60 * 60 * 24 * 365).Ticks;
            }
            //
            if (!mDeviceColorCodesInitialized && type == ChartType.Device) {
                for (int i = 0; i < Controller.OpenCLDevices.Length; ++i) {
                    try {
                        if (((LiveCharts.Wpf.LineSeries)chart.Series[i]).Stroke != null) {
                            var color = ((System.Windows.Media.SolidColorBrush)(((LiveCharts.Wpf.LineSeries)chart.Series[i]).Stroke)).Color;
                            dataGridViewDevices.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(
                                color.R + (255 - color.R) / 2,
                                color.G + (255 - color.G) / 2,
                                color.B + (255 - color.B) / 2);
                            mDeviceColorCodesInitialized = true;
                        }
                    } catch (Exception) { }
                }
            }
        }

        public bool ValidateCustomPoolSettings(bool showMessageBox = true)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^stratum\+tcp\:\/\/");
            textBoxCustomPool0Host.Text = regex.Replace(textBoxCustomPool0Host.Text.Trim(), "");
            textBoxCustomPool1Host.Text = regex.Replace(textBoxCustomPool1Host.Text.Trim(), "");
            textBoxCustomPool2Host.Text = regex.Replace(textBoxCustomPool2Host.Text.Trim(), "");
            textBoxCustomPool3Host.Text = regex.Replace(textBoxCustomPool3Host.Text.Trim(), "");
            textBoxCustomPool0SecondaryHost.Text = regex.Replace(textBoxCustomPool0SecondaryHost.Text.Trim(), "");
            textBoxCustomPool1SecondaryHost.Text = regex.Replace(textBoxCustomPool1SecondaryHost.Text.Trim(), "");
            textBoxCustomPool2SecondaryHost.Text = regex.Replace(textBoxCustomPool2SecondaryHost.Text.Trim(), "");
            textBoxCustomPool3SecondaryHost.Text = regex.Replace(textBoxCustomPool3SecondaryHost.Text.Trim(), "");

            regex = new System.Text.RegularExpressions.Regex(@" +");
            textBoxCustomPool0Host.Text = regex.Replace(textBoxCustomPool0Host.Text, string.Empty);
            textBoxCustomPool0Login.Text = regex.Replace(textBoxCustomPool0Login.Text, string.Empty);
            textBoxCustomPool0Password.Text = regex.Replace(textBoxCustomPool0Password.Text, string.Empty);
            textBoxCustomPool0SecondaryHost.Text = regex.Replace(textBoxCustomPool0SecondaryHost.Text, string.Empty);
            textBoxCustomPool0SecondaryLogin.Text = regex.Replace(textBoxCustomPool0SecondaryLogin.Text, string.Empty);
            textBoxCustomPool0SecondaryPassword.Text = regex.Replace(textBoxCustomPool0SecondaryPassword.Text, string.Empty);

            textBoxCustomPool1Host.Text = regex.Replace(textBoxCustomPool1Host.Text, string.Empty);
            textBoxCustomPool1Login.Text = regex.Replace(textBoxCustomPool1Login.Text, string.Empty);
            textBoxCustomPool1Password.Text = regex.Replace(textBoxCustomPool1Password.Text, string.Empty);
            textBoxCustomPool1SecondaryHost.Text = regex.Replace(textBoxCustomPool1SecondaryHost.Text, string.Empty);
            textBoxCustomPool1SecondaryLogin.Text = regex.Replace(textBoxCustomPool1SecondaryLogin.Text, string.Empty);
            textBoxCustomPool1SecondaryPassword.Text = regex.Replace(textBoxCustomPool1SecondaryPassword.Text, string.Empty);

            textBoxCustomPool2Host.Text = regex.Replace(textBoxCustomPool2Host.Text, string.Empty);
            textBoxCustomPool2Login.Text = regex.Replace(textBoxCustomPool2Login.Text, string.Empty);
            textBoxCustomPool2Password.Text = regex.Replace(textBoxCustomPool2Password.Text, string.Empty);
            textBoxCustomPool2SecondaryHost.Text = regex.Replace(textBoxCustomPool2SecondaryHost.Text, string.Empty);
            textBoxCustomPool2SecondaryLogin.Text = regex.Replace(textBoxCustomPool2SecondaryLogin.Text, string.Empty);
            textBoxCustomPool2SecondaryPassword.Text = regex.Replace(textBoxCustomPool2SecondaryPassword.Text, string.Empty);

            textBoxCustomPool3Host.Text = regex.Replace(textBoxCustomPool3Host.Text, string.Empty);
            textBoxCustomPool3Login.Text = regex.Replace(textBoxCustomPool3Login.Text, string.Empty);
            textBoxCustomPool3Password.Text = regex.Replace(textBoxCustomPool3Password.Text, string.Empty);
            textBoxCustomPool3SecondaryHost.Text = regex.Replace(textBoxCustomPool3SecondaryHost.Text, string.Empty);
            textBoxCustomPool3SecondaryLogin.Text = regex.Replace(textBoxCustomPool3SecondaryLogin.Text, string.Empty);
            textBoxCustomPool3SecondaryPassword.Text = regex.Replace(textBoxCustomPool3SecondaryPassword.Text, string.Empty);

            bool valid = (checkBoxCustomPool0Enable.Checked || checkBoxCustomPool1Enable.Checked || checkBoxCustomPool2Enable.Checked || checkBoxCustomPool3Enable.Checked);

            valid = valid && !(checkBoxCustomPool0Enable.Checked && textBoxCustomPool0Host.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool0Enable.Checked && textBoxCustomPool0Login.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool0Enable.Checked && textBoxCustomPool0Password.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool0Enable.Checked && comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool0SecondaryHost.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool0Enable.Checked && comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool0SecondaryLogin.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool0Enable.Checked && comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool0SecondaryPassword.Text == string.Empty);

            valid = valid && !(checkBoxCustomPool1Enable.Checked && textBoxCustomPool1Host.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool1Enable.Checked && textBoxCustomPool1Login.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool1Enable.Checked && textBoxCustomPool1Password.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool1Enable.Checked && comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool1SecondaryHost.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool1Enable.Checked && comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool1SecondaryLogin.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool1Enable.Checked && comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool1SecondaryPassword.Text == string.Empty);

            valid = valid && !(checkBoxCustomPool2Enable.Checked && textBoxCustomPool2Host.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool2Enable.Checked && textBoxCustomPool2Login.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool2Enable.Checked && textBoxCustomPool2Password.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool2Enable.Checked && comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool2SecondaryHost.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool2Enable.Checked && comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool2SecondaryLogin.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool2Enable.Checked && comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool2SecondaryPassword.Text == string.Empty);

            valid = valid && !(checkBoxCustomPool3Enable.Checked && textBoxCustomPool3Host.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool3Enable.Checked && textBoxCustomPool3Login.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool3Enable.Checked && textBoxCustomPool3Password.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool3Enable.Checked && comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool3SecondaryHost.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool3Enable.Checked && comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool3SecondaryLogin.Text == string.Empty);
            valid = valid && !(checkBoxCustomPool3Enable.Checked && comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex != 0 && textBoxCustomPool3SecondaryPassword.Text == string.Empty);

            if (!valid)
                MessageBox.Show("Custom pool settings are invalid.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return valid;
        }

        public bool ValidateBitcoinAddress(bool showMessageBox = true)
        {
            var regex = new System.Text.RegularExpressions.Regex("^[13][a-km-zA-HJ-NP-Z1-9]{25,34}$");
            var match = regex.Match(mUserBitcoinAddress);
            if (match.Success) {
                return true;
            } else {
                if (showMessageBox)
                    MessageBox.Show("Please enter a valid Bitcoin address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidateEthereumAddress(bool showMessageBox = true)
        {
            var regex = new System.Text.RegularExpressions.Regex("^0x[a-fA-Z0-9]{40}$");
            var match = regex.Match(mUserEthereumAddress);
            if (match.Success) {
                return true;
            } else {
                if (showMessageBox)
                    MessageBox.Show("Please enter a valid Ethereum address starting with \"0x\".", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidateMoneroAddress(bool showMessageBox = true)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^(4[0-9AB][123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz]{93}(\.?(([0-9a-fA-F]{16})|([0-9a-fA-F]{64})))?)|([0-9a-fA-F]{64})?$");
            var match = regex.Match(mUserMoneroAddress);
            if (match.Success) {
                return true;
            } else {
                if (showMessageBox)
                    MessageBox.Show("Please enter a valid Monero address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidatePascalAddress()
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^[\-0-9a-zA-Z\.]+$");
            var match = regex.Match(mUserPascalAddress);
            if (match.Success) {
                return true;
            } else {
                MessageBox.Show("Please enter a valid Pascal address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidateLbryAddress()
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^[0-9a-zA-Z]+?$");
            var match = regex.Match(mUserLbryAddress);
            if (match.Success) {
                return true;
            } else {
                MessageBox.Show("Please enter a valid Lbry address.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ValidateRigID()
        {
            textBoxRigID.Text = textBoxRigID.Text.Trim();
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]+$");
            var match = regex.Match(textBoxRigID.Text);
            if (match.Success) {
                return true;
            } else {
                MessageBox.Show("Please enter a valid rig ID consisting of alphanumeric characters.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private struct StratumServerInfo : IComparable<StratumServerInfo>
        {
            public string name;
            public long delay;
            public long time;

            public StratumServerInfo(string aName, long aDelay)
            {
                name = aName;
                delay = aDelay;
                try {
                    time = Utilities.MeasurePingRoundtripTime(aName);
                } catch (Exception) {
                    time = -1;
                }
                if (time >= 0)
                    time += delay;
            }

            public int CompareTo(StratumServerInfo other)
            {
                if (time == other.time)
                    return 0;
                else if (other.time < 0 && time >= 0)
                    return -1;
                else if (other.time >= 0 && time < 0)
                    return 1;
                else if (other.time > time)
                    return -1;
                else
                    return 1;
            }
        };

        #region GetServers

        List<StratumServerInfo> GetNiceHashLyra2REv2Servers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("lyra2rev2.usa.nicehash.com", 0),
                new StratumServerInfo("lyra2rev2.eu.nicehash.com", 0),
                new StratumServerInfo("lyra2rev2.hk.nicehash.com", 150),
                new StratumServerInfo("lyra2rev2.jp.nicehash.com", 100),
                new StratumServerInfo("lyra2rev2.in.nicehash.com", 200),
                new StratumServerInfo("lyra2rev2.br.nicehash.com", 180)
            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetNiceHashNeoScryptServers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("neoscrypt.usa.nicehash.com", 0),
                new StratumServerInfo("neoscrypt.eu.nicehash.com", 0),
                new StratumServerInfo("neoscrypt.hk.nicehash.com", 150),
                new StratumServerInfo("neoscrypt.jp.nicehash.com", 100),
                new StratumServerInfo("neoscrypt.in.nicehash.com", 200),
                new StratumServerInfo("neoscrypt.br.nicehash.com", 180)
            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetNiceHashLbryServers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("lbry.usa.nicehash.com", 0),
                new StratumServerInfo("lbry.eu.nicehash.com", 0),
                new StratumServerInfo("lbry.hk.nicehash.com", 150),
                new StratumServerInfo("lbry.jp.nicehash.com", 100),
                new StratumServerInfo("lbry.in.nicehash.com", 200),
                new StratumServerInfo("lbry.br.nicehash.com", 180)
            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetNiceHashEthashServers()
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
            return hosts;
        }

        List<StratumServerInfo> GetNanopoolEthashServers()
        {
            var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("eth-eu1.nanopool.org", 0),
                                new StratumServerInfo("eth-eu2.nanopool.org", 0),
                                new StratumServerInfo("eth-asia1.nanopool.org", 0),
                                new StratumServerInfo("eth-us-east1.nanopool.org", 0),
                                new StratumServerInfo("eth-us-west1.nanopool.org", 0)
                            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetNiceHashCryptoNightServers()
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
            return hosts;
        }

        List<StratumServerInfo> GetNiceHashPascalServers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("pascal.usa.nicehash.com", 0),
                new StratumServerInfo("pascal.eu.nicehash.com", 0),
                new StratumServerInfo("pascal.hk.nicehash.com", 150),
                new StratumServerInfo("pascal.jp.nicehash.com", 100),
                new StratumServerInfo("pascal.in.nicehash.com", 200),
                new StratumServerInfo("pascal.br.nicehash.com", 180)
            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetDwarfPoolCryptoNightServers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("xmr-eu.dwarfpool.com", 0),
                new StratumServerInfo("xmr-usa.dwarfpool.com", 0)
            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetNanopoolCryptoNightServers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("xmr-eu1.nanopool.org", 0),
                new StratumServerInfo("xmr-eu2.nanopool.org", 0),
                new StratumServerInfo("xmr-us-east1.nanopool.org", 0),
                new StratumServerInfo("xmr-us-west1.nanopool.org", 0),
                new StratumServerInfo("xmr-asia1.nanopool.org", 0)
            };
            hosts.Sort();
            return hosts;
        }

        List<StratumServerInfo> GetNanopoolPascalServers()
        {
            var hosts = new List<StratumServerInfo> {
                new StratumServerInfo("pasc-eu1.nanopool.org", 0),
                new StratumServerInfo("pasc-eu2.nanopool.org", 0),
                new StratumServerInfo("pasc-us-east1.nanopool.org", 0),
                new StratumServerInfo("pasc-us-west1.nanopool.org", 0),
                new StratumServerInfo("pasc-asia1.nanopool.org", 0),
                new StratumServerInfo("pasc-jp1.nanopool.org", 0),
                new StratumServerInfo("pasc-au1.nanopool.org", 0)
            };
            hosts.Sort();
            return hosts;
        }

        #endregion

        public void LaunchOpenCLCryptoNightMiners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            CryptoNightStratum stratum = null;
            var niceHashMode = false;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNiceHashCryptoNightServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new CryptoNightStratum(host.name, 3355, username, "x", pool);
                            break;
                        } catch (Exception ex) { Logger(ex); }
                niceHashMode = true;
            } else if (pool == "DwarfPool" && (mDevFeeMode || textBoxMoneroAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeMoneroAddress + Parameters.DevFeeUsernamePostfix : textBoxMoneroAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var password = textBoxEmail.Text != "" ? textBoxEmail.Text : "x";
                var hosts = GetDwarfPoolCryptoNightServers();
                hosts.Sort();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new CryptoNightStratum(host.name, 8005, username, password, pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "Nanopool" && (mDevFeeMode || textBoxMoneroAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeMoneroAddress : textBoxMoneroAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "") {
                    username += "." + textBoxRigID.Text;
                    if (textBoxEmail.Text != "")
                        username += "/" + textBoxEmail.Text;
                }
                var hosts = GetNanopoolCryptoNightServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new CryptoNightStratum(host.name, 14444, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "mineXMR.com" && (mDevFeeMode || textBoxMoneroAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeMoneroAddress + Parameters.DevFeeUsernamePostfix : textBoxMoneroAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                stratum = new CryptoNightStratum("pool.minexmr.com", 7777, username, "x", pool);
            }

            if (stratum != null) {
                Controller.PrimaryStratum = (Stratum)stratum;
                LaunchOpenCLCryptoNightMinersWithStratum(stratum, niceHashMode);
            }
        }

        public void LaunchOpenCLLbryMiners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            LbryStratum stratum = null;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNiceHashLbryServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new LbryStratum(host.name, 3356, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "zpool" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress : textBoxBitcoinAddress.Text;
                stratum = new LbryStratum("lbry.mine.zpool.ca", 3334, username, "c=BTC", pool);
            }

            if (stratum != null) {
                LaunchOpenCLLbryMinersWithStratum(stratum);
                Controller.PrimaryStratum = (Stratum)stratum;
            }
        }

        public void LaunchOpenCLNeoScryptMiners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            NeoScryptStratum stratum = null;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNiceHashNeoScryptServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new NeoScryptStratum(host.name, 3341, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "zpool" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress : textBoxBitcoinAddress.Text;
                stratum = new NeoScryptStratum("neoscrypt.mine.zpool.ca", 4233, username, "c=BTC", pool);
            }

            if (stratum != null) {
                LaunchOpenCLNeoScryptMinersWithStratum(stratum);
                Controller.PrimaryStratum = (Stratum)stratum;
            }
        }

        public void LaunchOpenCLLyra2REv2Miners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            Lyra2REv2Stratum stratum = null;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNiceHashLyra2REv2Servers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new Lyra2REv2Stratum(host.name, 3347, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "zpool" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress : textBoxBitcoinAddress.Text;
                stratum = new Lyra2REv2Stratum("lyra2v2.mine.zpool.ca", 4533, username, "c=BTC", pool);
            }

            if (stratum != null) {
                LaunchOpenCLLyra2REv2MinersWithStratum(stratum);
                Controller.PrimaryStratum = (Stratum)stratum;
            }
        }

        public void LaunchOpenCLDualEthashPascalMiners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            EthashStratum ethashStratum = null;
            PascalStratum pascalStratum = null;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var ethashHosts = GetNiceHashEthashServers();
                foreach (var ethashHost in ethashHosts)
                    if (ethashHost.time >= 0)
                        try {
                            ethashStratum = new NiceHashEthashStratum(ethashHost.name, 3353, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
                var pascalHosts = GetNiceHashPascalServers();
                foreach (var pascalHost in pascalHosts)
                    if (pascalHost.time >= 0)
                        try {
                            pascalStratum = new PascalStratum(pascalHost.name, 3358, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "Nanopool" && (mDevFeeMode || (textBoxEthereumAddress.Text.Length > 0 && textBoxPascalAddress.Text.Length > 0))) {
                var username = mDevFeeMode ? Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix : textBoxEthereumAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var ethashHosts = GetNanopoolEthashServers();
                foreach (var ethashHost in ethashHosts)
                    if (ethashHost.time >= 0)
                        try {
                            ethashStratum = new OpenEthereumPoolEthashStratum(ethashHost.name, 9999, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
                username = mDevFeeMode ? Parameters.DevFeePascalAddress + Parameters.DevFeeUsernamePostfix : textBoxPascalAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var pascalHosts = GetNanopoolPascalServers();
                foreach (var pascalHost in pascalHosts)
                    if (pascalHost.time >= 0)
                        try {
                            pascalStratum = new PascalStratum(pascalHost.name, 15555, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            }

            if (ethashStratum != null && pascalStratum != null) {
                LaunchOpenCLDualEthashPascalMinersWithStratum(ethashStratum, pascalStratum);
                Controller.PrimaryStratum = (Stratum)ethashStratum;
                Controller.SecondaryStratum = (Stratum)pascalStratum;
            }
        }

        public void LaunchOpenCLPascalMiners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            PascalStratum stratum = null;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNiceHashPascalServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new PascalStratum(host.name, 3358, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "Nanopool" && (mDevFeeMode || textBoxPascalAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeePascalAddress + Parameters.DevFeeUsernamePostfix : textBoxPascalAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNanopoolPascalServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new PascalStratum(host.name, 15555, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            }

            if (stratum != null) {
                LaunchOpenCLPascalMinersWithStratum(stratum);
                Controller.PrimaryStratum = (Stratum)stratum;
            }
        }

        public void LaunchOpenCLEthashMiners(string pool)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            EthashStratum stratum = null;

            if (pool == "NiceHash" && (mDevFeeMode || textBoxBitcoinAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix : textBoxBitcoinAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                var hosts = GetNiceHashEthashServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new NiceHashEthashStratum(host.name, 3353, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "DwarfPool" && (mDevFeeMode || textBoxEthereumAddress.Text.Length > 0)) {
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
                var username = mDevFeeMode ? Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix : textBoxEthereumAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                hosts.Sort();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 8008, username, textBoxEmail.Text != "" ? textBoxEmail.Text : "x", pool, true);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "ethermine.org" && (mDevFeeMode || textBoxEthereumAddress.Text.Length > 0)) {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("us1.ethermine.org", 0),
                                new StratumServerInfo("us2.ethermine.org", 0),
                                new StratumServerInfo("eu1.ethermine.org", 0),
                                new StratumServerInfo("asia1.ethermine.org", 0)
                            };
                var username = mDevFeeMode ? Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix : textBoxEthereumAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                hosts.Sort();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 4444, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "ethpool.org" && (mDevFeeMode || textBoxEthereumAddress.Text.Length > 0)) {
                var hosts = new List<StratumServerInfo> {
                                new StratumServerInfo("us1.ethpool.org", 0),
                                new StratumServerInfo("us2.ethpool.org", 0),
                                new StratumServerInfo("eu1.ethpool.org", 0),
                                new StratumServerInfo("asia1.ethpool.org", 0)
                            };
                var username = mDevFeeMode ? Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix : textBoxEthereumAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "")
                    username += "." + textBoxRigID.Text;
                hosts.Sort();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 3333, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            } else if (pool == "Nanopool" && (mDevFeeMode || textBoxEthereumAddress.Text.Length > 0)) {
                var username = mDevFeeMode ? Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix : textBoxEthereumAddress.Text;
                if (!mDevFeeMode && textBoxRigID.Text != "") {
                    username += "." + textBoxRigID.Text;
                    if (textBoxEmail.Text != "")
                        username += "/" + textBoxEmail.Text;
                }
                var hosts = GetNanopoolEthashServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            stratum = new OpenEthereumPoolEthashStratum(host.name, 9999, username, "x", pool);
                            break;
                        } catch (Exception ex) {
                            Logger(ex);
                        }
            }

            if (stratum != null) {
                LaunchOpenCLEthashMinersWithStratum(stratum);
                Controller.PrimaryStratum = (Stratum)stratum;
            }
        }

        void LaunchOpenCLCryptoNightMinersWithStratum(CryptoNightStratum stratum, bool niceHashMode)
        {
            EnableHardwareManagement(stratum, null);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, i, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    for (i = 0; i < numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "cryptonight", "threads")].Value; ++i)
                        ++minerCount;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (i = 0; i < numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "cryptonight", "threads")].Value; ++i) {
                        OpenCLCryptoNightMiner miner = new OpenCLCryptoNightMiner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(miner);
                        miner.Start(stratum,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "cryptonight", "raw_intensity")]
                                .Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "cryptonight", "local_work_size")]
                                .Value)), niceHashMode);
                        toolStripMainFormProgressBar.Value = ++minerCount;
                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        void LaunchOpenCLDualEthashLbryMinersWithStratum(EthashStratum stratum, LbryStratum stratum2)
        {
            EnableHardwareManagement(stratum, stratum2);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    minerCount += 1;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;

            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    OpenCLDualEthashLbryMiner dualMiner = new OpenCLDualEthashLbryMiner(Controller.OpenCLDevices[deviceIndex]);
                    Controller.Miners.Add(dualMiner);
                    dualMiner.Start(stratum,
                        Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash", "intensity")]
                            .Value)),
                        Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash", "local_work_size")]
                            .Value)),
                            stratum2,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lbry", "intensity")].Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lbry", "local_work_size")].Value)));
                    toolStripMainFormProgressBar.Value = ++minerCount;

                    for (int j = 0; j < mLaunchInterval; j += 10) {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(10);
                    }

                }
            }
        }

        void LaunchOpenCLDualEthashPascalMinersWithStratum(EthashStratum stratum, PascalStratum stratum2)
        {
            EnableHardwareManagement(stratum, stratum2);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    minerCount += Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash_pascal", "threads")].Value));
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;

            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (int i = 0; i < numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash_pascal", "threads")].Value; ++i) {
                        OpenCLDualEthashPascalMiner dualMiner = new OpenCLDualEthashPascalMiner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(dualMiner);
                        dualMiner.Start(stratum,
                                stratum2,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash_pascal", "intensity")]
                                .Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash_pascal", "pascal_iterations")]
                                .Value)));
                        toolStripMainFormProgressBar.Value = ++minerCount;

                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        void LaunchOpenCLEthashMinersWithStratum(EthashStratum stratum)
        {
            EnableHardwareManagement(stratum, null);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, i, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    for (i = 0; i < numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash", "threads")].Value; ++i)
                        ++minerCount;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (i = 0; i < numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash", "threads")].Value; ++i) {
                        OpenCLEthashMiner miner = new OpenCLEthashMiner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(miner);
                        miner.Start(stratum,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash", "intensity")]
                                .Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "ethash", "local_work_size")]
                                .Value)));
                        toolStripMainFormProgressBar.Value = ++minerCount;
                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        void LaunchOpenCLLbryMinersWithStratum(LbryStratum stratum)
        {
            EnableHardwareManagement(stratum, null);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, i, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    for (i = 0; i < Convert.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lbry", "threads")].Value); ++i)
                        ++minerCount;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (i = 0; i < Convert.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lbry", "threads")].Value); ++i) {
                        OpenCLLbryMiner miner = new OpenCLLbryMiner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(miner);
                        miner.Start(stratum,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lbry", "intensity")].Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lbry", "local_work_size")].Value)));
                        toolStripMainFormProgressBar.Value = ++minerCount;
                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        void LaunchOpenCLPascalMinersWithStratum(PascalStratum stratum)
        {
            EnableHardwareManagement(stratum, null);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, i, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    for (i = 0; i < Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "pascal", "threads")].Value)); ++i)
                        ++minerCount;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (i = 0; i < Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "pascal", "threads")].Value)); ++i) {
                        OpenCLPascalMiner miner = new OpenCLPascalMiner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(miner);
                        miner.Start(stratum,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "pascal", "intensity")].Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "pascal", "local_work_size")].Value)));
                        toolStripMainFormProgressBar.Value = ++minerCount;
                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        void LaunchOpenCLNeoScryptMinersWithStratum(NeoScryptStratum stratum)
        {
            EnableHardwareManagement(stratum, null);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, i, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    for (i = 0; i < Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "neoscrypt", "threads")].Value)); ++i)
                        ++minerCount;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (i = 0; i < Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "neoscrypt", "threads")].Value)); ++i) {
                        OpenCLNeoScryptMiner miner = new OpenCLNeoScryptMiner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(miner);
                        miner.Start(stratum,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "neoscrypt", "raw_intensity")].Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "neoscrypt", "local_work_size")].Value))
                            );
                        toolStripMainFormProgressBar.Value = ++minerCount;
                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        void LaunchOpenCLLyra2REv2MinersWithStratum(Lyra2REv2Stratum stratum)
        {
            EnableHardwareManagement(stratum, null);
            if (mDevFeeMode)
                throw new InvalidOperationException();
            this.Activate();
            toolStripMainFormProgressBar.Value = toolStripMainFormProgressBar.Minimum = 0;
            int deviceIndex, i, minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex)
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value))
                    for (i = 0; i < Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lyra2rev2", "threads")].Value)); ++i)
                        ++minerCount;
            toolStripMainFormProgressBar.Maximum = minerCount;
            minerCount = 0;
            for (deviceIndex = 0; deviceIndex < Controller.OpenCLDevices.Length; ++deviceIndex) {
                if ((bool)(dataGridViewDevices.Rows[deviceIndex].Cells["enabled"].Value)) {
                    for (i = 0; i < Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lyra2rev2", "threads")].Value)); ++i) {
                        OpenCLLyra2REv2Miner miner = new OpenCLLyra2REv2Miner(Controller.OpenCLDevices[deviceIndex]);
                        Controller.Miners.Add(miner);
                        miner.Start(stratum,
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lyra2rev2", "intensity")].Value)),
                            Convert.ToInt32(Math.Round(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(deviceIndex, "lyra2rev2", "local_work_size")].Value))
                            );
                        toolStripMainFormProgressBar.Value = ++minerCount;
                        for (int j = 0; j < mLaunchInterval; j += 10) {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        bool CustomPoolEnabled {
            get {
                return (string)comboBoxDefaultAlgorithm.SelectedItem == "Custom Pools";
            }
        }

        private void LaunchMinersForCustomPool(string algo, string host, int port, string login, string password, string algo2, string host2, int port2, string login2, string password2)
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            if (algo == "Ethash" && algo2 == "Lbry") {
                var stratum = new OpenEthereumPoolEthashStratum(host, port, login, password, host);
                var stratum2 = new LbryStratum(host2, port2, login2, password2, host2);
                LaunchOpenCLDualEthashLbryMinersWithStratum(stratum, stratum2);
                Controller.PrimaryStratum = stratum;
                Controller.SecondaryStratum = stratum2;
            } else if (algo == "Ethash" && algo2 == "Pascal") {
                var stratum = new OpenEthereumPoolEthashStratum(host, port, login, password, host);
                var stratum2 = new PascalStratum(host2, port2, login2, password2, host2);
                LaunchOpenCLDualEthashPascalMinersWithStratum(stratum, stratum2);
                Controller.PrimaryStratum = stratum;
                Controller.SecondaryStratum = stratum2;
            } else if (algo == "Ethash") {
                var stratum = new OpenEthereumPoolEthashStratum(host, port, login, password, host);
                LaunchOpenCLEthashMinersWithStratum(stratum);
                Controller.PrimaryStratum = stratum;
            } else if (algo == "Ethash (NiceHash)" && algo2 == "Lbry") {
                var stratum = new NiceHashEthashStratum(host, port, login, password, host);
                var stratum2 = new LbryStratum(host2, port2, login2, password2, host2);
                LaunchOpenCLDualEthashLbryMinersWithStratum(stratum, stratum2);
                Controller.PrimaryStratum = stratum;
                Controller.SecondaryStratum = stratum2;
            } else if (algo == "Ethash (NiceHash)" && algo2 == "Pascal") {
                var stratum = new NiceHashEthashStratum(host, port, login, password, host);
                var stratum2 = new PascalStratum(host2, port2, login2, password2, host2);
                LaunchOpenCLDualEthashPascalMinersWithStratum(stratum, stratum2);
                Controller.PrimaryStratum = stratum;
                Controller.SecondaryStratum = stratum2;
            } else if (algo == "Ethash (NiceHash)") {
                var stratum = new NiceHashEthashStratum(host, port, login, password, host);
                LaunchOpenCLEthashMinersWithStratum(stratum);
                Controller.PrimaryStratum = stratum;
            } else if (algo == "CryptoNight" || algo == "CryptoNight (NiceHash)") {
                var stratum = new CryptoNightStratum(host, port, login, password, host);
                LaunchOpenCLCryptoNightMinersWithStratum(stratum, (algo == "CryptoNight (NiceHash)"));
                Controller.PrimaryStratum = stratum;
            } else if (algo == "Lbry") {
                var stratum = new LbryStratum(host, port, login, password, host);
                LaunchOpenCLLbryMinersWithStratum(stratum);
            } else if (algo == "Pascal") {
                var stratum = new PascalStratum(host, port, login, password, host);
                LaunchOpenCLPascalMinersWithStratum(stratum);
                Controller.PrimaryStratum = stratum;
            } else if (algo == "NeoScrypt") {
                var stratum = new NeoScryptStratum(host, port, login, password, host);
                LaunchOpenCLNeoScryptMinersWithStratum(stratum);
                Controller.PrimaryStratum = stratum;
            } else if (algo == "Lyra2REv2") {
                var stratum = new Lyra2REv2Stratum(host, port, login, password, host);
                LaunchOpenCLLyra2REv2MinersWithStratum(stratum);
                Controller.PrimaryStratum = stratum;
            }
        }

        private void EnableHardwareManagement(Stratum stratum, Stratum stratum2)
        {
            // The basic strategy here is to enable hardware management before launching miners for the sake of stability.
            // I got better results, as far as stability is concerned, by "teaching" RAM's about modded memory timings
            // as soon as overclocking kicks in. It seems that switching from one set of memory timings to another is a major source
            // of instability. - zawawa

            string algorithm = stratum.AlgorithmName;
            if (stratum2 != null)
                algorithm += "_" + stratum2.AlgorithmName;
            foreach (var device in Controller.OpenCLDevices) {
                if (!(bool)dataGridViewDevices.Rows[device.DeviceIndex].Cells["enabled"].Value)
                    continue;
                var tuple = new Tuple<int, string>(device.DeviceIndex, algorithm);
                if (checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_enabled")].Checked) {
                    device.SaveOverclockingSettings();
                    device.TargetPowerLimit = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_power_limit")].Value);
                    device.TargetCoreClock = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Value);
                    device.TargetMemoryClock = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Value);
                    device.TargetCoreVoltage = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_voltage")].Value);
                    device.TargetMemoryVoltage = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_voltage")].Value);
                    device.OverclockingEnabled = true;
                    device.UpdateOverclockingSettings();
                }
                if (checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_enabled")].Checked)
                    device.PrepareMemoryTimingMods(algorithm);
                if (checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "enabled")].Checked) {
                    device.TargetTemperature = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "target_temperature".ToLower())].Value);
                    device.TargetMaxTemperature = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_temperature".ToLower())].Value);
                    device.TargetMinFanSpeed = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "minimum_fan_speed".ToLower())].Value);
                    device.TargetMaxFanSpeed = Decimal.ToInt32(numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_fan_speed".ToLower())].Value);
                    device.FanControlEnabled = true;
                }
            }
            Thread.Sleep(1000);
        }

        private void LaunchMiners()
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (CustomPoolEnabled) {
                LaunchMinersForCustomPools();
            } else {
                LaunchMinersForDefaultPools();
            }
        }

        private void LaunchMinersForDefaultPools()
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            foreach (string pool in listBoxPoolPriorities.Items) {
                try {
                    if (DefaultAlgorithm == "ethash_pascal") {
                        Logger("Launching Dual Ethash/Pascal for " + pool + "...");
                        LaunchOpenCLDualEthashPascalMiners(pool);
                    } else if (DefaultAlgorithm == "ethash") {
                        Logger("Launching Ethash miners for " + pool + "...");
                        LaunchOpenCLEthashMiners(pool);
                    } else if (DefaultAlgorithm == "cryptonight") {
                        Logger("Launching CryptoNight miners for " + pool + "...");
                        LaunchOpenCLCryptoNightMiners(pool);
                    } else if (DefaultAlgorithm == "lbry") {
                        Logger("Launching Lbry miners for " + pool + "...");
                        LaunchOpenCLLbryMiners(pool);
                    } else if (DefaultAlgorithm == "pascal") {
                        Logger("Launching Pascal miners for " + pool + "...");
                        LaunchOpenCLPascalMiners(pool);
                    } else if (DefaultAlgorithm == "neoscrypt") {
                        Logger("Launching NeoScrypt miners for " + pool + "...");
                        LaunchOpenCLNeoScryptMiners(pool);
                    } else if (DefaultAlgorithm == "lyra2rev2") {
                        Logger("Launching Lyra2REv2 miners for " + pool + "...");
                        LaunchOpenCLLyra2REv2Miners(pool);
                    }
                    if (Controller.PrimaryStratum != null && Controller.Miners.Count > 0) {
                        return;
                    } else {
                        Logger("Failed to launch miner(s) for " + pool);
                    }
                } catch (UnrecoverableException ex) {
                    throw ex;
                } catch (Exception ex) {
                    Logger("Failed to launch miners for " + pool + ": " + ex.Message + ex.StackTrace);
                }

                // Clean up the mess.
                if (Controller.PrimaryStratum != null)
                    Controller.PrimaryStratum.Stop();
                if (Controller.SecondaryStratum != null)
                    Controller.SecondaryStratum.Stop();
                foreach (Miner miner in Controller.Miners)
                    miner.Stop();
                Controller.PrimaryStratum = null;
                Controller.SecondaryStratum = null;
                Controller.Miners.Clear();
            }
        }

        private void LaunchMinersForCustomPools()
        {
            if (mDevFeeMode)
                throw new InvalidOperationException();
            for (int customPoolIndex = 0; customPoolIndex < 4; customPoolIndex++) {
                bool enabled = (customPoolIndex == 0) ? checkBoxCustomPool0Enable.Checked :
                               (customPoolIndex == 1) ? checkBoxCustomPool1Enable.Checked :
                               (customPoolIndex == 2) ? checkBoxCustomPool2Enable.Checked :
                                                        checkBoxCustomPool3Enable.Checked;
                String host = (customPoolIndex == 0) ? textBoxCustomPool0Host.Text :
                              (customPoolIndex == 1) ? textBoxCustomPool1Host.Text :
                              (customPoolIndex == 2) ? textBoxCustomPool2Host.Text :
                                                       textBoxCustomPool3Host.Text;
                int port = (customPoolIndex == 0) ? Convert.ToInt32(numericUpDownCustomPool0Port.Value) :
                           (customPoolIndex == 1) ? Convert.ToInt32(numericUpDownCustomPool1Port.Value) :
                           (customPoolIndex == 2) ? Convert.ToInt32(numericUpDownCustomPool2Port.Value) :
                                                    Convert.ToInt32(numericUpDownCustomPool3Port.Value);
                String login = (customPoolIndex == 0) ? textBoxCustomPool0Login.Text :
                               (customPoolIndex == 1) ? textBoxCustomPool1Login.Text :
                               (customPoolIndex == 2) ? textBoxCustomPool2Login.Text :
                                                        textBoxCustomPool3Login.Text;
                String password = (customPoolIndex == 0) ? textBoxCustomPool0Password.Text :
                                  (customPoolIndex == 1) ? textBoxCustomPool1Password.Text :
                                  (customPoolIndex == 2) ? textBoxCustomPool2Password.Text :
                                                           textBoxCustomPool3Password.Text;
                String host2 = (customPoolIndex == 0) ? textBoxCustomPool0SecondaryHost.Text :
                               (customPoolIndex == 1) ? textBoxCustomPool1SecondaryHost.Text :
                               (customPoolIndex == 2) ? textBoxCustomPool2SecondaryHost.Text :
                                                        textBoxCustomPool3SecondaryHost.Text;
                int port2 = (customPoolIndex == 0) ? Convert.ToInt32(numericUpDownCustomPool0SecondaryPort.Value) :
                            (customPoolIndex == 1) ? Convert.ToInt32(numericUpDownCustomPool1SecondaryPort.Value) :
                            (customPoolIndex == 2) ? Convert.ToInt32(numericUpDownCustomPool2SecondaryPort.Value) :
                                                     Convert.ToInt32(numericUpDownCustomPool3SecondaryPort.Value);
                String login2 = (customPoolIndex == 0) ? textBoxCustomPool0SecondaryLogin.Text :
                                (customPoolIndex == 1) ? textBoxCustomPool1SecondaryLogin.Text :
                                (customPoolIndex == 2) ? textBoxCustomPool2SecondaryLogin.Text :
                                                         textBoxCustomPool3SecondaryLogin.Text;
                String password2 = (customPoolIndex == 0) ? textBoxCustomPool0SecondaryPassword.Text :
                                   (customPoolIndex == 1) ? textBoxCustomPool1SecondaryPassword.Text :
                                   (customPoolIndex == 2) ? textBoxCustomPool2SecondaryPassword.Text :
                                                            textBoxCustomPool3SecondaryPassword.Text;
                String algo = (customPoolIndex == 0) ? (string)comboBoxCustomPool0Algorithm.Items[comboBoxCustomPool0Algorithm.SelectedIndex] :
                              (customPoolIndex == 1) ? (string)comboBoxCustomPool1Algorithm.Items[comboBoxCustomPool1Algorithm.SelectedIndex] :
                              (customPoolIndex == 2) ? (string)comboBoxCustomPool2Algorithm.Items[comboBoxCustomPool2Algorithm.SelectedIndex] :
                                                       (string)comboBoxCustomPool3Algorithm.Items[comboBoxCustomPool3Algorithm.SelectedIndex];
                String algo2 = (customPoolIndex == 0) ? (string)comboBoxCustomPool0SecondaryAlgorithm.Items[comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex] :
                               (customPoolIndex == 1) ? (string)comboBoxCustomPool1SecondaryAlgorithm.Items[comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex] :
                               (customPoolIndex == 2) ? (string)comboBoxCustomPool2SecondaryAlgorithm.Items[comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex] :
                                                        (string)comboBoxCustomPool3SecondaryAlgorithm.Items[comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex];



                if (!enabled)
                    continue;

                try {
                    Logger("Launching miner(s) for Custom Pool " + customPoolIndex + "...");
                    LaunchMinersForCustomPool(algo, host, port, login, password, algo2, host2, port2, login2, password2);
                    break;
                } catch (UnrecoverableException ex) {
                    throw ex;
                } catch (Exception ex) {
                    Logger("Failed to launch miner(s) for Custom Pool " + customPoolIndex + ": " + ex.Message + ex.StackTrace);
                }

                // Clean up the mess.
                if (Controller.PrimaryStratum != null)
                    Controller.PrimaryStratum.Stop();
                if (Controller.SecondaryStratum != null)
                    Controller.SecondaryStratum.Stop();
                foreach (Miner miner in Controller.Miners)
                    miner.Stop();
                Controller.PrimaryStratum = null;
                Controller.SecondaryStratum = null;
                Controller.Miners.Clear();
            }
        }

        private void StopMiners(bool saveStateToFile = true)
        {
            try {
                Logger("Stopping miners...");

                Controller.AppState = Controller.ApplicationGlobalState.Switching;

                Controller.StopWatch.Stop();
                timerWatchdog.Enabled = false;
                if (mDevFeeMode)
                    SetDevFeeMode(false);
                foreach (var miner in Controller.Miners)
                    miner.Stop();
                var allDone = false;
                var counter = 10 * 1000;
                while (!allDone && (counter -= 10) > 0) {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    allDone = true;
                    foreach (var miner in Controller.Miners)
                        if (!miner.Done) {
                            allDone = false;
                            break;
                        }
                }

                Thread.Sleep(1000);
                foreach (var device in Controller.OpenCLDevices) {
                    if (device.MemoryTimingModsEnabled) {
                        device.MemoryTimingModsEnabled = false;
                        device.RestoreMemoryTimings();
                    }
                }
                Thread.Sleep(1000);

                foreach (var device in Controller.OpenCLDevices) {
                    if (device.OverclockingEnabled) {
                        device.OverclockingEnabled = false;
                        device.RestoreOverclockingSettings();
                    }
                    if (device.FanControlEnabled) {
                        device.FanControlEnabled = false;
                        device.FanSpeed = -1;
                    }
                }

                if (Controller.PrimaryStratum != null)
                    Controller.PrimaryStratum.Stop();
                if (Controller.SecondaryStratum != null)
                    Controller.SecondaryStratum.Stop();
                if (Controller.PrimaryStratumBackup != null)
                    Controller.PrimaryStratumBackup.Stop();
                if (Controller.SecondaryStratumBackup != null)
                    Controller.SecondaryStratumBackup.Stop();
                toolStripMainFormProgressBar.Value = 0;

                foreach (var miner in Controller.Miners) {
                    if (!miner.Done)
                        miner.Abort();
                    miner.Dispose();
                }
                Controller.Miners.Clear();
                Controller.PrimaryStratum = null;
                Controller.SecondaryStratum = null;
                Controller.PrimaryStratumBackup = null;
                Controller.SecondaryStratumBackup = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Controller.AppState = Controller.ApplicationGlobalState.Idle;
                if (saveStateToFile)
                    try { using (var file = new System.IO.StreamWriter(AppStateFilePath, false)) file.WriteLine("Idle"); } catch (Exception) { }
                UpdateStats();
                UpdateControls();

                Logger("Stopped miners.");
            } catch (Exception ex) {
                Logger(ex);
            }
        }

        private void buttonStart_Click(object sender = null, EventArgs e = null)
        {
            if (Controller.AppState == Controller.ApplicationGlobalState.Idle && !CheckSettingsBeforeMining())
                return;

            if (Controller.AppState == Controller.ApplicationGlobalState.Idle) {
                Controller.AppState = Controller.ApplicationGlobalState.Switching;
                UpdateControls();
                Application.DoEvents();
                Controller.StopWatch.Start();
                tabControlMainForm.SelectedIndex = 0;
                if (mAreSettingsDirty)
                    SaveSettingsToDatabase();

                Controller.PrimaryStratum = null;
                Controller.SecondaryStratum = null;
                Controller.Miners.Clear();
                mDevFeeMode = false;
                try { using (var file = new System.IO.StreamWriter(AppStateFilePath, false)) file.WriteLine("Mining"); } catch (Exception) { }
                Exception unrecoverableException = null;
                try {
                    LaunchMiners();
                } catch (Exception ex) {
                    unrecoverableException = new UnrecoverableException(ex.Message);
                }
                if (unrecoverableException != null) {
                    GetUnrecoverableException();
                } else {
                    unrecoverableException = GetUnrecoverableException();
                }

                if (unrecoverableException != null || Controller.PrimaryStratum == null || !Controller.Miners.Any()) {
                    StopMiners();

                    if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {
                        Controller.BenchmarkState = Controller.ApplicationBenchmarkState.NotRunning;
                        MessageBox.Show((unrecoverableException != null ? unrecoverableException.Message : "Failed to launch miner.") + "\nBenchmarks have been aborted.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else if (MessageBox.Show(
                        Utilities.GetAutoClosingForm(20),
                        (unrecoverableException != null ? unrecoverableException.Message : "Failed to launch miner.") + "\nWould you like to stop mining now?",
                        appName, MessageBoxButtons.YesNo, MessageBoxIcon.Error) != System.Windows.Forms.DialogResult.Yes) {
                        timerAutoStart.Enabled = true;
                    }
                } else {
                    timerDevFee.Interval = Parameters.DevFeeInitialDelayInSeconds * 1000;
                    timerDevFee.Enabled = true;
                    mDevFeeModeStartTime = DateTime.Now;
                    timerWatchdog.Enabled = true;
                    Controller.AppState = Controller.ApplicationGlobalState.Mining;
                }
            } else if (Controller.AppState == Controller.ApplicationGlobalState.Mining) {
                StopMiners();
            }

            UpdateStats();
            UpdateControls();
        }

        private bool CheckSettingsBeforeMining()
        {
            try {
                if (CustomPoolEnabled && !ValidateCustomPoolSettings())
                    return false;
                if (!CustomPoolEnabled) {
                    if (textBoxBitcoinAddress.Text != "" && !ValidateBitcoinAddress())
                        return false;
                    if (textBoxEthereumAddress.Text != "" && !ValidateEthereumAddress())
                        return false;
                    if (textBoxMoneroAddress.Text != "" && !ValidateMoneroAddress())
                        return false;
                    if (textBoxPascalAddress.Text != "" && !ValidatePascalAddress())
                        return false;
                    if (textBoxLbryAddress.Text != "" && !ValidateLbryAddress())
                        return false;
                    if (textBoxRigID.Text != "" && !ValidateRigID())
                        return false;
                    if (textBoxBitcoinAddress.Text == ""
                        && textBoxEthereumAddress.Text == ""
                        && textBoxMoneroAddress.Text == ""
                        && textBoxPascalAddress.Text == ""
                        && textBoxLbryAddress.Text == "") {
                        MessageBox.Show("Please enter at least one valid wallet address.", appName, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        tabControlMainForm.TabIndex = 1;
                        return false;
                    }
                }
                var enabled = false;
                foreach (var device in Controller.OpenCLDevices)
                    enabled = enabled || (bool)(dataGridViewDevices.Rows[device.DeviceIndex].Cells["enabled"].Value);
                if (!enabled) {
                    MessageBox.Show("Please enable at least one device.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlMainForm.TabIndex = 0;
                    return false;
                }

                return true;
            } catch (Exception ex) { Logger(ex); return false; }
        }

        private void UpdateControls()
        {
            try {
                tabControlMainForm.Enabled = (Controller.AppState != Controller.ApplicationGlobalState.Switching);
                comboBoxDefaultAlgorithm.Enabled = buttonStart.Enabled = (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.NotRunning) 
                                      && (Controller.AppState != Controller.ApplicationGlobalState.Switching);
                buttonRunBenchmarks.Enabled = (Controller.AppState != Controller.ApplicationGlobalState.Switching)
                                              && (Controller.AppState == Controller.ApplicationGlobalState.Idle
                                                  || (Controller.AppState == Controller.ApplicationGlobalState.Mining
                                                      && Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running));

                buttonBoostPerformance.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonRestoreStockSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;

                buttonStart.Text
                    = Controller.StopWatch.Elapsed.TotalSeconds == 0 ? "Start" :
                      Controller.AppState == Controller.ApplicationGlobalState.Mining ? "Pause" :
                                                                                        "Resume";
                buttonRunBenchmarks.Text
                    = (Controller.AppState == Controller.ApplicationGlobalState.Mining
                       && Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) ? "Abort Benchmarking" :
                                                                                                       "Run Benchmarks";
                buttonReleaseMemory.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonRelaunch.Enabled = Controller.AppState  == Controller.ApplicationGlobalState.Idle;

                groupBoxPoolPriorities.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle && !CustomPoolEnabled;
                groupBoxPoolParameters.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle && !CustomPoolEnabled;
                groupBoxWalletAddresses.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle && !CustomPoolEnabled;
                groupBoxAutomation.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                groupBoxHadrwareAcceleration.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                dataGridViewDevices.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                groupBoxCustmPool0.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                groupBoxCustmPool1.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                groupBoxCustmPool2.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                groupBoxCustmPool3.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;

                textBoxCustomPool0Host.Enabled = textBoxCustomPool0Login.Enabled = textBoxCustomPool0Password.Enabled = comboBoxCustomPool0Algorithm.Enabled = comboBoxCustomPool0SecondaryAlgorithm.Enabled = numericUpDownCustomPool0Port.Enabled = checkBoxCustomPool0Enable.Checked;
                textBoxCustomPool1Host.Enabled = textBoxCustomPool1Login.Enabled = textBoxCustomPool1Password.Enabled = comboBoxCustomPool1Algorithm.Enabled = comboBoxCustomPool1SecondaryAlgorithm.Enabled = numericUpDownCustomPool1Port.Enabled = checkBoxCustomPool1Enable.Checked;
                textBoxCustomPool2Host.Enabled = textBoxCustomPool2Login.Enabled = textBoxCustomPool2Password.Enabled = comboBoxCustomPool2Algorithm.Enabled = comboBoxCustomPool2SecondaryAlgorithm.Enabled = numericUpDownCustomPool2Port.Enabled = checkBoxCustomPool2Enable.Checked;
                textBoxCustomPool3Host.Enabled = textBoxCustomPool3Login.Enabled = textBoxCustomPool3Password.Enabled = comboBoxCustomPool3Algorithm.Enabled = comboBoxCustomPool3SecondaryAlgorithm.Enabled = numericUpDownCustomPool3Port.Enabled = checkBoxCustomPool3Enable.Checked;

                if ((string)comboBoxCustomPool0Algorithm.SelectedItem != "Ethash" && (string)comboBoxCustomPool0Algorithm.SelectedItem != "Ethash (NiceHash)") comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex = 0;
                if ((string)comboBoxCustomPool1Algorithm.SelectedItem != "Ethash" && (string)comboBoxCustomPool1Algorithm.SelectedItem != "Ethash (NiceHash)") comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex = 0;
                if ((string)comboBoxCustomPool2Algorithm.SelectedItem != "Ethash" && (string)comboBoxCustomPool2Algorithm.SelectedItem != "Ethash (NiceHash)") comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex = 0;
                if ((string)comboBoxCustomPool3Algorithm.SelectedItem != "Ethash" && (string)comboBoxCustomPool3Algorithm.SelectedItem != "Ethash (NiceHash)") comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex = 0;

                textBoxCustomPool0SecondaryHost.Enabled = textBoxCustomPool0SecondaryLogin.Enabled = textBoxCustomPool0SecondaryPassword.Enabled = numericUpDownCustomPool0SecondaryPort.Enabled = checkBoxCustomPool0Enable.Checked && comboBoxCustomPool0SecondaryAlgorithm.SelectedIndex != 0;
                textBoxCustomPool1SecondaryHost.Enabled = textBoxCustomPool1SecondaryLogin.Enabled = textBoxCustomPool1SecondaryPassword.Enabled = numericUpDownCustomPool1SecondaryPort.Enabled = checkBoxCustomPool1Enable.Checked && comboBoxCustomPool1SecondaryAlgorithm.SelectedIndex != 0;
                textBoxCustomPool2SecondaryHost.Enabled = textBoxCustomPool2SecondaryLogin.Enabled = textBoxCustomPool2SecondaryPassword.Enabled = numericUpDownCustomPool2SecondaryPort.Enabled = checkBoxCustomPool2Enable.Checked && comboBoxCustomPool2SecondaryAlgorithm.SelectedIndex != 0;
                textBoxCustomPool3SecondaryHost.Enabled = textBoxCustomPool3SecondaryLogin.Enabled = textBoxCustomPool3SecondaryPassword.Enabled = numericUpDownCustomPool3SecondaryPort.Enabled = checkBoxCustomPool3Enable.Checked && comboBoxCustomPool3SecondaryAlgorithm.SelectedIndex != 0;

                tabControlDeviceSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonResetAllSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonResetOverclockingSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonResetFanControlSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonResetAlgorithmSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;

                if (Controller.OpenCLDevices != null) {
                    foreach (var device in Controller.OpenCLDevices) {
                        var fanControlEnabled = checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "enabled")].Checked;
                        checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "enabled")].Enabled = (Controller.AppState == Controller.ApplicationGlobalState.Idle);
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "target_temperature".ToLower())].Enabled = (Controller.AppState == Controller.ApplicationGlobalState.Idle) && fanControlEnabled;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_temperature".ToLower())].Enabled = (Controller.AppState == Controller.ApplicationGlobalState.Idle) && fanControlEnabled;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "minimum_fan_speed".ToLower())].Enabled = (Controller.AppState == Controller.ApplicationGlobalState.Idle) && fanControlEnabled;
                        numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, "fan_control", "maximum_fan_speed".ToLower())].Enabled = (Controller.AppState == Controller.ApplicationGlobalState.Idle) && fanControlEnabled;

                        foreach (var algorithm in sAlgorithmList) {
                            var overclockingEnabled = checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_enabled")].Checked;
                            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_power_limit")].Enabled = overclockingEnabled; // && ((OpenCLDevice)device).PowerLimit >= 0;
                            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_clock")].Enabled = overclockingEnabled && ((OpenCLDevice)device).CoreClock >= 0;
                            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_clock")].Enabled = overclockingEnabled && ((OpenCLDevice)device).MemoryClock >= 0;
                            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_core_voltage")].Enabled = overclockingEnabled; // && ((OpenCLDevice)device).CoreVoltage >= 0;
                            numericUpDownDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "overclocking_memory_voltage")].Enabled = overclockingEnabled && ((OpenCLDevice)device).DefaultMemoryVoltage >= 0;
                        }

                        foreach (var algorithm in sAlgorithmList) {
                            checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_enabled")].Visible = (device.GetType() == typeof(AMDPolaris10));
                            groupBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_polaris10")].Visible = (device.GetType() == typeof(AMDPolaris10));
                            var memoryTimingEnabled = checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_enabled")].Checked;
                            if (!memoryTimingEnabled)
                                checkBoxEnablePhymem.Checked = true;
                        }
                    }
                }

                numericUpDownAPIPort.Enabled = !checkBoxAPIEnabled.Checked;
                groupBoxAPIIPRange.Enabled = !checkBoxAPIEnabled.Checked;

                groupBoxOpenCLBinaries.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;

                groupBoxUserSettings.Enabled = Controller.AppState == Controller.ApplicationGlobalState.Idle;
                buttonRestoreSettingsBackup.Enabled = listBoxSettingBackups.SelectedIndex >= 0;
                buttonDeleteSettingsBackup.Enabled = listBoxSettingBackups.SelectedIndex >= 0;
                buttonDeleteAllSettingsBackups.Enabled = listBoxSettingBackups.Items.Count > 0;

                cartesianChartTemperature.Visible = ((string)comboBoxGraphType.SelectedItem == "Temperature");
                cartesianChartFanSpeed.Visible = ((string)comboBoxGraphType.SelectedItem == "Fan Speed");
                cartesianChartPower.Visible = ((string)comboBoxGraphType.SelectedItem == "Power");
                cartesianChartDeviceActivity.Visible = ((string)comboBoxGraphType.SelectedItem == "Activity");
                cartesianChartCPUUsage.Visible = ((string)comboBoxGraphType.SelectedItem == "CPU Usage");

                cartesianChartSpeedPrimaryAlgorithm.Visible = ((string)comboBoxSecondGraphType.SelectedItem == "Speed (Primary Algorithm)");
                cartesianChartSpeedSecondaryAlgorithm.Visible = ((string)comboBoxSecondGraphType.SelectedItem == "Speed (Secondary Algorithm)");
                cartesianChartShare1Minute.Visible = ((string)comboBoxSecondGraphType.SelectedItem == "Share") && ((string)comboBoxSecondGraphCoverage.SelectedItem == "1 Minute");
                cartesianChartShare1Hour.Visible = ((string)comboBoxSecondGraphType.SelectedItem == "Share") && ((string)comboBoxSecondGraphCoverage.SelectedItem == "1 Hour");
                cartesianChartShare1Day.Visible = ((string)comboBoxSecondGraphType.SelectedItem == "Share") && ((string)comboBoxSecondGraphCoverage.SelectedItem == "1 Day");
                cartesianChartShare1Month.Visible = ((string)comboBoxSecondGraphType.SelectedItem == "Share") && ((string)comboBoxSecondGraphCoverage.SelectedItem == "1 Month");
            } catch (Exception ex) {
                Logger("Exception in UpdateControls(): " + ex.Message + ex.StackTrace);
            }
        }

        private void buttonPoolPrioritiesUp_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBoxPoolPriorities.SelectedIndex;
            if (selectedIndex > 0) {
                listBoxPoolPriorities.Items.Insert(selectedIndex - 1, listBoxPoolPriorities.Items[selectedIndex]);
                listBoxPoolPriorities.Items.RemoveAt(selectedIndex + 1);
                listBoxPoolPriorities.SelectedIndex = selectedIndex - 1;
                UpdateStats();
            }
        }

        private void buttonPoolPrioritiesDown_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBoxPoolPriorities.SelectedIndex;
            if ((selectedIndex < listBoxPoolPriorities.Items.Count - 1) & (selectedIndex != -1)) {
                listBoxPoolPriorities.Items.Insert(selectedIndex + 2, listBoxPoolPriorities.Items[selectedIndex]);
                listBoxPoolPriorities.Items.RemoveAt(selectedIndex);
                listBoxPoolPriorities.SelectedIndex = selectedIndex + 1;
                UpdateStats();
            }
        }

        private void buttonViewBalancesAtNiceHash_Click(object sender, EventArgs e)
        {
            if (ValidateBitcoinAddress())
                System.Diagnostics.Process.Start("https://www.nicehash.com/miner/" + textBoxBitcoinAddress.Text);
        }

        private void tabControlMainForm_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonEthereumBalance_Click(object sender, EventArgs e)
        {
            if (!ValidateEthereumAddress())
                return;
            foreach (string poolName in listBoxPoolPriorities.Items)
                if (poolName == "Nanopool") {
                    System.Diagnostics.Process.Start("https://eth.nanopool.org/account/" + textBoxEthereumAddress.Text);
                    return;
                } else if (poolName == "DwarfPool") {
                    System.Diagnostics.Process.Start("https://dwarfpool.com/eth/address?wallet=" + textBoxEthereumAddress.Text);
                    return;
                } else if (poolName == "ethermine.org") {
                    System.Diagnostics.Process.Start("https://ethermine.org/miners/" + textBoxEthereumAddress.Text);
                    return;
                } else if (poolName == "ethpool.org") {
                    System.Diagnostics.Process.Start("https://ethpool.org/miners/" + textBoxEthereumAddress.Text);
                    return;
                }
        }

        #region DEVFEE

        private bool SwitchToStratumForDEVFEE()
        {
            string algo = Controller.Miners[0].AlgorithmName;
            Stratum newPrimaryStratum = null;
            Stratum newSecondaryStratum = null;

            if (algo == "neoscrypt") {
                var username = Parameters.DevFeeBitcoinAddress;
                try {
                    newPrimaryStratum = new NeoScryptStratum("neoscrypt.mine.zpool.ca", 4233, username, "c=BTC", "neoscrypt.mine.zpool.ca");
                } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    var hosts = GetNiceHashNeoScryptServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new NeoScryptStratum(host.name, 3341, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else if (algo == "cryptonight") {
                var username = Parameters.DevFeeMoneroAddress + Parameters.DevFeeUsernamePostfix;
                var hosts = GetNanopoolCryptoNightServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            newPrimaryStratum = new CryptoNightStratum(host.name, 14444, username, "x", host.name);
                            break;
                        } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    hosts = GetNiceHashCryptoNightServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new CryptoNightStratum(host.name, 3355, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else if (algo == "ethash") {
                var username = Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix;
                var hosts = GetNanopoolEthashServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            newPrimaryStratum = new OpenEthereumPoolEthashStratum(host.name, 9999, username, "x", host.name);
                            break;
                        } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    hosts = GetNiceHashEthashServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new NiceHashEthashStratum(host.name, 3353, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else if (algo == "ethash_pascal") {
                var username = Parameters.DevFeeEthereumAddress + Parameters.DevFeeUsernamePostfix;
                var hosts = GetNanopoolEthashServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            newPrimaryStratum = new OpenEthereumPoolEthashStratum(host.name, 9999, username, "x", host.name);
                            break;
                        } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    hosts = GetNiceHashEthashServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new NiceHashEthashStratum(host.name, 3353, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }

                username = Parameters.DevFeePascalAddress + Parameters.DevFeeUsernamePostfix;
                hosts = GetNanopoolPascalServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            newSecondaryStratum = new PascalStratum(host.name, 15555, username, "x", host.name);
                            break;
                        } catch (Exception ex) { Logger(ex); }

                if (newSecondaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    hosts = GetNiceHashPascalServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newSecondaryStratum = new PascalStratum(host.name, 3358, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else if (algo == "lyra2rev2") {
                var username = Parameters.DevFeeBitcoinAddress;
                try {
                    newPrimaryStratum = new Lyra2REv2Stratum("lyra2v2.mine.zpool.ca", 4533, username, "c=BTC", "lyra2v2.mine.zpool.ca");
                } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    var hosts = GetNiceHashLyra2REv2Servers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new Lyra2REv2Stratum(host.name, 3347, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else if (algo == "lbry") {
                var username = Parameters.DevFeeBitcoinAddress;
                try {
                    newPrimaryStratum = new LbryStratum("lbry.mine.zpool.ca", 3334, username, "c=BTC", "lbry.mine.zpool.ca");
                } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    username = Parameters.DevFeeBitcoinAddress + Parameters.DevFeeUsernamePostfix;
                    var hosts = GetNiceHashLbryServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new LbryStratum(host.name, 3356, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else if (algo == "pascal") {
                var username = Parameters.DevFeePascalAddress + Parameters.DevFeeUsernamePostfix;
                var hosts = GetNanopoolPascalServers();
                foreach (var host in hosts)
                    if (host.time >= 0)
                        try {
                            newPrimaryStratum = new PascalStratum(host.name, 15555, username, "x", host.name);
                            break;
                        } catch (Exception ex) { Logger(ex); }

                if (newPrimaryStratum == null) {
                    hosts = GetNiceHashPascalServers();
                    foreach (var host in hosts)
                        if (host.time >= 0)
                            try {
                                newPrimaryStratum = new PascalStratum(host.name, 3358, username, "x", host.name);
                                break;
                            } catch (Exception ex) { Logger(ex); }
                }
            } else {
                throw new System.InvalidOperationException(algo);
            }

            if (newPrimaryStratum != null && (Controller.SecondaryStratum == null || newSecondaryStratum != null)) {
                Logger("Switching to the DEVFEE mode...");
                mDevFeeMode = true;

                foreach (var miner in Controller.Miners) {
                    miner.SetPrimaryStratum(newPrimaryStratum);
                    if (newSecondaryStratum != null)
                        miner.SetSecondaryStratum(newSecondaryStratum);
                    if (miner.GetType() == typeof(OpenCLCryptoNightMiner))
                        ((OpenCLCryptoNightMiner)miner).SaveNiceHashMode();
                }
                Controller.PrimaryStratumBackup = Controller.PrimaryStratum;
                Controller.PrimaryStratum = newPrimaryStratum;
                Controller.SecondaryStratumBackup = Controller.SecondaryStratum;
                Controller.SecondaryStratum = newSecondaryStratum;
                Controller.PrimaryStratumBackup.SilentMode = true;
                if (Controller.SecondaryStratumBackup != null)
                    Controller.SecondaryStratumBackup.SilentMode = true;
                return true;
            } else {
                Logger("Failed to switch to the DEVFEE mode...");
                if (newPrimaryStratum != null) newPrimaryStratum.Stop();
                if (newSecondaryStratum != null) newSecondaryStratum.Stop();
                return false;
            }
        }

        private void SwitchFromStratumForDEVFEE()
        {
            Controller.PrimaryStratumBackup.SilentMode = false;
            if (Controller.SecondaryStratumBackup != null) {
                Controller.SecondaryStratumBackup.SilentMode = false;
            }

            Logger("Switching back from the DEVFEE mode...");
            mDevFeeMode = false;

            Stratum oldPrimaryStratum = Controller.PrimaryStratum;
            Controller.PrimaryStratum = Controller.PrimaryStratumBackup;
            Controller.PrimaryStratumBackup = null;
            oldPrimaryStratum.Stop();

            Stratum oldSecondaryStratum = Controller.SecondaryStratum;
            Controller.SecondaryStratum = Controller.SecondaryStratumBackup;
            Controller.SecondaryStratumBackup = null;
            if (oldSecondaryStratum != null)
                oldSecondaryStratum.Stop();

            foreach (var miner in Controller.Miners) {
                if (miner.PrimaryAlgorithmName == Controller.PrimaryStratum.AlgorithmName)
                    miner.SetPrimaryStratum(Controller.PrimaryStratum);
                if (Controller.SecondaryStratum != null && miner.SecondaryAlgorithmName == Controller.SecondaryStratum.AlgorithmName)
                    miner.SetSecondaryStratum(Controller.SecondaryStratum);
                if (miner.GetType() == typeof(OpenCLCryptoNightMiner))
                    ((OpenCLCryptoNightMiner)miner).RestoreNiceHashMode();
            }
        }

        private void timerDevFee_Tick(object sender, EventArgs e)
        {
            if (Controller.AppState == Controller.ApplicationGlobalState.Idle) {
                timerDevFee.Enabled = false;
                return;
            } else if (Controller.AppState == Controller.ApplicationGlobalState.Initializing || Controller.AppState == Controller.ApplicationGlobalState.Switching) {
                timerDevFee.Interval = 1000;
                return;
            }

            Controller.AppState = Controller.ApplicationGlobalState.Switching;
            timerDevFee.Stop();
            tabControlMainForm.Enabled = buttonStart.Enabled = false;

            if (!mDevFeeMode) {
                SwitchToStratumForDEVFEE();
            } else {
                SwitchFromStratumForDEVFEE();
            }
            SetDevFeeMode(mDevFeeMode);

            Controller.AppState = Controller.ApplicationGlobalState.Mining;
            tabControlMainForm.Enabled = buttonStart.Enabled = true;
        }

        void SetDevFeeMode(bool mode)
        {
            mDevFeeMode = mode;
            timerDevFee.Stop();
            timerDevFee.Interval = ((mDevFeeMode) ? Parameters.DevFeeDurationInSeconds * 1000 : (int)((double)Parameters.DevFeeDurationInSeconds * ((double)(100 - Parameters.DevFeePercentage) / Parameters.DevFeePercentage) * 1000));
            timerDevFee.Start();
            if (mDevFeeMode)
                mDevFeeModeStartTime = DateTime.Now;
        }

        #endregion

        private Exception GetUnrecoverableException()
        {
            Exception ex = null;
            if (Controller.PrimaryStratum != null && Controller.PrimaryStratum.UnrecoverableException != null) {
                ex = Controller.PrimaryStratum.UnrecoverableException;
                Controller.PrimaryStratum.UnrecoverableException = null;
            }
            if (Controller.SecondaryStratum != null && Controller.SecondaryStratum.UnrecoverableException != null) {
                ex = Controller.SecondaryStratum.UnrecoverableException;
                Controller.SecondaryStratum.UnrecoverableException = null;
            }
            foreach (var miner in Controller.Miners) {
                if (miner.UnrecoverableException != null) {
                    ex = miner.UnrecoverableException;
                    miner.UnrecoverableException = null;
                }
            }
            return ex;
        }

        private void timerWatchdog_Tick(object sender, EventArgs e)
        {
            if (Controller.AppState != Controller.ApplicationGlobalState.Mining)
                return;

            try {
                Exception ex = GetUnrecoverableException();
                if (ex != null && ex.GetType() == typeof(StratumServerUnavailableException)) {
                    Controller.AppState = Controller.ApplicationGlobalState.Switching;
                    tabControlMainForm.Enabled = buttonStart.Enabled = false;
                    StopMiners();
                    timerAutoStart.Enabled = true;
                } else if (ex != null) {
                    Controller.AppState = Controller.ApplicationGlobalState.Switching;
                    tabControlMainForm.Enabled = buttonStart.Enabled = false;
                    StopMiners();
                    if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {
                        Controller.BenchmarkState = Controller.ApplicationBenchmarkState.NotRunning;
                        MessageBox.Show(ex.Message + "\nBenchmarks have been aborted.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else if (MessageBox.Show(Utilities.GetAutoClosingForm(10), ex.Message + "\n\nMining will automatically resume in 10 seconds.\nWould you like to stop mining now?", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Error) != System.Windows.Forms.DialogResult.Yes) { 
                        timerAutoStart.Enabled = true;
                    }
                } else {
                    foreach (var miner in Controller.Miners) {
                        if (!miner.Alive) {
                            MainForm.Logger("Miner thread for Device #" + miner.DeviceIndex + " is unresponsive. Restarting the application...");
                            Program.Exit(false);
                        }
                    }
                }
                UpdateStats();
                UpdateControls();
            } catch (Exception ex) {
                Logger("Exception in timerWatchdog_Tick(): " + ex.Message + ex.StackTrace);
            }
        }

        private void buttonMoneroBalance_Click(object sender, EventArgs e)
        {
            if (!ValidateMoneroAddress())
                return;
            foreach (string poolName in listBoxPoolPriorities.Items)
                if (poolName == "Nanopool") {
                    System.Diagnostics.Process.Start("https://xmr.nanopool.org/account/" + textBoxMoneroAddress.Text);
                    return;
                } else if (poolName == "DwarfPool") {
                    System.Diagnostics.Process.Start("https://dwarfpool.com/xmr/address?wallet=" + textBoxMoneroAddress.Text);
                    return;
                } else if (poolName == "mineXMR.com") {
                    System.Diagnostics.Process.Start("http://minexmr.com/");
                    return;
                }
        }

        private void timerUpdateLog_Tick(object sender, EventArgs e)
        {
            try {
                timerUpdateLog.Enabled = false;
                UpdateLog();
                timerUpdateLog.Enabled = true;
            } catch (Exception ex) {
                Logger("Exception in timerUpdateLog_Tick(): " + ex.Message + ex.StackTrace);
            }
        }

        private void checkBoxLaunchAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            try {
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                if (checkBoxLaunchAtStartup.Checked)
                    startInfo.Arguments = "/C schtasks /create /sc onlogon /tn GatelessGateSharp /rl highest /tr \"\\\"" + Application.ExecutablePath + "\\\"\"";
                else
                    startInfo.Arguments = "/C schtasks /delete /f /tn GatelessGateSharp";
                process.StartInfo = startInfo;
                process.Start();
            } catch (Exception) {
                MessageBox.Show("Failed to complete the operation.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxGPU0Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU1Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU2Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU3Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU4Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU5Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU6Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxGPU7Enable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void labelGPU0ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU0MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU1MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU2MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU3MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU4MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU5MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU6MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7ID_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Vendor_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Name_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Speed_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Shares_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Activity_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Temp_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7Fan_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7CoreClock_Click(object sender, EventArgs e)
        {

        }

        private void labelGPU7MemoryClock_Click(object sender, EventArgs e)
        {

        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            Utilities.FixFPU();
            richTextBoxLog.Clear();
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LogFilePath);
        }

        private void timerAutoStart_Tick(object sender, EventArgs e)
        {
            timerAutoStart.Enabled = false;
            if (Controller.AppState == Controller.ApplicationGlobalState.Idle) {
                buttonStart_Click();
            }
        }

        private void checkBoxCustomPool0Enable_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void checkBoxCustomPool1Enable_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void checkBoxCustomPool2Enable_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void checkBoxCustomPool3Enable_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool0Algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool0SecondaryAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool1Algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool1SecondaryAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool2Algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool2SecondaryAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool3Algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxCustomPool3SecondaryAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void dataGridViewDevices_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewDevices.ClearSelection();
        }

        private void dataGridViewDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (0 <= e.RowIndex && e.RowIndex < dataGridViewDevices.RowCount)
                dataGridViewDevices.Rows[e.RowIndex].Cells["enabled"].Value = !(bool)(dataGridViewDevices.Rows[e.RowIndex].Cells["enabled"].Value);
        }

        private void buttonConfigureAutomaticLogin_Click(object sender, EventArgs e)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "netplwiz";
            startInfo.Arguments = "";
            process.StartInfo = startInfo;
            process.Start();
            //process.WaitForExit();
        }

        private void buttonDisableAuomaticRepair_Click(object sender, EventArgs e)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "bcdedit";
            startInfo.Arguments = "/set recoveryenabled NO";
            process.StartInfo = startInfo;
            process.Start();
            //process.WaitForExit();

            MessageBox.Show("Automatic Repair has been disabled.", appName, MessageBoxButtons.OK);
        }

        private void buttonDisableDriverInstallation_Click(object sender, EventArgs e)
        {
            try {
                Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Device Metadata", "PreventDeviceMetadataFromNetwork", 1);
                MessageBox.Show("Automatic driver installation has been disabled.", appName, MessageBoxButtons.OK);
            } catch (Exception) { }
        }

        private void buttonDeviceInstallationSettings_Click(object sender, EventArgs e)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "rundll32";
            startInfo.Arguments = "newdev.dll,DeviceInternetSettingUi 2";
            process.StartInfo = startInfo;
            process.Start();
            //process.WaitForExit();
        }

        private void buttonInstallRecommendedAMDDriver_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://support.amd.com/en-us/kb-articles/Pages/Radeon-Software-Adrenalin-Edition-17.12.2-Release-Notes.aspx");
        }

        private void buttonDownloadDisplayDriverUninstaller_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.guru3d.com/files-get/display-driver-uninstaller-download,9.html");
        }

        private void buttonUserAccountControlSettings_Click(object sender, EventArgs e)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "useraccountcontrolsettings";
            startInfo.Arguments = "";
            process.StartInfo = startInfo;
            process.Start();
            //process.WaitForExit();
        }

        private void buttonDisableUserAccountControl_Click(object sender, EventArgs e)
        {
            try {
                Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLUA", 0);
                MessageBox.Show("User Account Control has been disabled.", appName, MessageBoxButtons.OK);
            } catch (Exception) { }
        }

        private void RunNGen()
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = @"C:\Windows\Microsoft.Net\Framework64\v4.0.30319\ngen.exe";

            startInfo.Arguments = "install GatelessGateSharp.exe";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "install GatelessGateSharpMonitor.exe";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "install Cloo.dll";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "install DeviceSettingsUserControl.dll";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "install HashLib.dll";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "install ManagedCuda.dll";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            startInfo.Arguments = "install Nvml.dll";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private void Task_KillInterferingProcesses(object cancellationToken)
        {
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    foreach (var name in new List<string> { "amdow", "amddvr", "AUEPMaster", "AUEPMaster", "AUEPUF", "AUEPDU" , "RadeonSettings" })
                        foreach (var process in System.Diagnostics.Process.GetProcessesByName(name))
                            try { process.Kill(); } catch (Exception) { }
                } catch (Exception) { }
                System.Threading.Thread.Sleep(60 * 1000);
            }
        }

        private void buttonResetAll_Click(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            foreach (var device in Controller.OpenCLDevices) {
                ResetDeviceSettings(device);
                device.FanSpeed = -1;
                device.ResetOverclockingSettings();
            }
        }

        private void buttonResetFanControlSettings_Click(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            foreach (var device in Controller.OpenCLDevices) {
                ResetDeviceFanControlSettings(device);
                device.FanSpeed = -1;
            }
        }

        private void buttonResetDeviceAlgorithmSettings_Click(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            foreach (var device in Controller.OpenCLDevices)
                ResetDeviceAlgorithmSettings(device);
        }

        private void buttonResetDeviceOverclockingSettings_Click(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            foreach (var device in Controller.OpenCLDevices) {
                ResetDeviceOverclockingSettings(device);
                device.ResetOverclockingSettings();
            }
        }

        int mCPUUsage = 0;

        private void Task_UpdateCPUUsage(object cancellationToken)
        {
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
                    System.Diagnostics.PerformanceCounter counter = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
                    System.Threading.Thread.Sleep(1000);

                    while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                        mCPUUsage = (int)counter.NextValue();
                        System.Threading.Thread.Sleep(1000);
                    }
                } catch (Exception) {
                    // https://support.microsoft.com/en-us/help/300956/how-to-manually-rebuild-performance-counter-library-values
                    try { Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\Perflib", "Last Counter", 1846); } catch (Exception) { }
                    try { Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\Perflib", "Last Help", 1847); } catch (Exception) { }
                    try {
                        using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\Processor", true)) {
                            if (key != null) {
                                try { key.DeleteValue("First Counter"); } catch (Exception) { }
                                try { key.DeleteValue("First Help"); } catch (Exception) { }
                                try { key.DeleteValue("Last Counter"); } catch (Exception) { }
                                try { key.DeleteValue("Last Help"); } catch (Exception) { }
                            }
                        }
                    } catch (Exception) { }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Task_CollectGarbage(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    Logger("Running garbage collection...");
                    Logger("Memory used before collection: " + GC.GetTotalMemory(false) / 1024 / 1024 + "MB");
                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                    GC.Collect();
                    Logger("Memory used before collection: " + GC.GetTotalMemory(true) / 1024 / 1024 + "MB");
                } catch (Exception ex) { Logger(ex); }
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }

        string mLatestReleaseUrl;
        string mLatestReleaseName;
        int mLatestReleaseDiff = 0;
        string mLatestReleaseFileName;
        string mLatestReleaseInstallerPath;

        private void Task_UpdateLatestReleaseInfo(object cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            while (!((CancellationToken)cancellationToken).IsCancellationRequested) {
                try {
                    string url = "https://github.com/zawawawa/GatelessGateSharp/releases.atom";
                    System.ServiceModel.Syndication.Atom10FeedFormatter formatter = new System.ServiceModel.Syndication.Atom10FeedFormatter();
                    using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(url)) {
                        formatter.ReadFrom(reader);
                    }
                    String latestReleaseUrl = "";
                    String latestReleaseName = "";
                    int latestReleaseDiff = 0;
                    foreach (System.ServiceModel.Syndication.SyndicationItem item in formatter.Feed.Items) {
                        if (latestReleaseName == "") {
                            latestReleaseUrl = @"https://github.com" + item.Links[0].Uri.ToString();
                            latestReleaseName = item.Title.Text;
                        }
                        if (item.Title.Text == appName)
                            break;
                        ++latestReleaseDiff;
                    }

                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml((new CustomWebClient()).DownloadString(latestReleaseUrl));
                    document.DocumentNode.Descendants("a")
                        .Where(e => {
                            string src = e.GetAttributeValue("href", null) ?? "";
                            return !string.IsNullOrEmpty(src);
                        })
                        .ToList()
                        .ForEach(x => {
                            var href = x.Attributes["href"].Value;
                            var regex = new System.Text.RegularExpressions.Regex(@"^/zawawawa/GatelessGateSharp/releases/download/[^/]+/([^/]+\.msi)$");
                            var match = regex.Match(href);
                            if (match.Success) {
                                mLatestReleaseName = latestReleaseName;
                                mLatestReleaseUrl = "https://github.com" + href;
                                mLatestReleaseDiff = latestReleaseDiff;
                                mLatestReleaseFileName = match.Groups[1].Value;
                            }
                        });
                } catch (Exception) { } // TODO
                System.Threading.Thread.Sleep(60 * 1000 * 10);
            }
        }

        private void InstallLatestVersion()
        {
            try {
                if (mLatestReleaseName == null) {
                    MessageBox.Show("There is no information available on the latest release.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } else if (mLatestReleaseName == appName) {
                    if (MessageBox.Show("You are already running the latest version of Gateless Gate Sharp.\nWould you still like to install it?", appName, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        return;
                } else if (MessageBox.Show("The latest version is " + mLatestReleaseName + ".\nWould you like to install it?", appName, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) {
                    return;
                }
                toolStripMainFormProgressBar.Minimum = 0;
                toolStripMainFormProgressBar.Maximum = 100;
                toolStripMainFormProgressBar.Value = 0;
                IntPtr outPath;
                int result = NativeMethods.SHGetKnownFolderPath(new Guid("{374DE290-123F-4565-9164-39C4925E467B}"), (uint)NativeMethods.KnownFolderFlags.DontVerify, new IntPtr(0), out outPath);
                if (result == 0) {
                    mLatestReleaseInstallerPath = Marshal.PtrToStringUni(outPath) + "\\" + mLatestReleaseFileName;
                    using (WebClient wc = new WebClient()) {
                        wc.DownloadProgressChanged += LatestReleaseDownloader_DownloadProgressChanged;
                        wc.DownloadFileCompleted += LatestReleaseDownloader_DownloadFileCompleted;
                        wc.DownloadFileAsync(new System.Uri(mLatestReleaseUrl), mLatestReleaseInstallerPath);
                    }
                    Logger("Downloading the latest version of Gateless Gate Sharp...");
                }
            } catch (Exception) { } // TODO
        }

        void LatestReleaseDownloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            toolStripMainFormProgressBar.Value = e.ProgressPercentage;
        }

        void LatestReleaseDownloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(mLatestReleaseInstallerPath, "/passive");
            Program.Exit(true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InstallLatestVersion();
        }

        private void buttonInstallTeamViewer_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://download.teamviewer.com/download/TeamViewer_Setup.exe");
        }

        private void comboBoxGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxGraphCoverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show("Would you like to save settings?", appName, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    SaveSettingsToDatabase();
            } catch (Exception) { }
        }

        private void buttonSaveSettingsAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Gateless Gate Sharp settings files (*.sqlite)|*.sqlite|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "GatelessGateSharp.sqlite";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                SaveSettingsToDatabase(saveFileDialog.FileName);
        }

        private void buttonLoadSettings_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show("Would you like to load settings?", appName, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    LoadSettingsFromDatabase();
            } catch (Exception) { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Gateless Gate Sharp settings files (*.sqlite)|*.sqlite|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                LoadSettingsFromDatabase(openFileDialog.FileName);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void radioButtonEthereumPascal_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void radioButtonFeathercoin_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void radioButtonMonacoin_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void listBoxPoolPriorities_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxBitcoinAddress_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            mUserBitcoinAddress = textBoxBitcoinAddress.Text.Trim();
        }

        private void textBoxPascalAddress_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            mUserPascalAddress = textBoxPascalAddress.Text.Trim();
        }

        private void textBoxEthereumAddress_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            mUserEthereumAddress = textBoxEthereumAddress.Text.Trim();
        }

        private void textBoxLbryAddress_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            mUserLbryAddress = textBoxLbryAddress.Text.Trim();
        }

        private void textBoxMoneroAddress_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            mUserMoneroAddress = textBoxMoneroAddress.Text.Trim();
        }

        private void textBoxZcashAddress_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            mUserZcashAddress = textBoxZcashAddress.Text.Trim();
        }

        private void textBoxRigID_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool0Host_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool0SecondaryHost_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool0Port_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool0SecondaryPort_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool0Login_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool0SecondaryLogin_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool0Password_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool0SecondaryPassword_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool1Host_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool1SecondaryHost_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool1Port_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool1SecondaryPort_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool1Login_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool1SecondaryLogin_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool1Password_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool1SecondaryPassword_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool2Host_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool2SecondaryHost_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool2Port_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool2SecondaryPort_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool2Login_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool2SecondaryLogin_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool2Password_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool2SecondaryPassword_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool3Host_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool3SecondaryHost_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool3Port_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void numericUpDownCustomPool3SecondaryPort_ValueChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;

        }

        private void textBoxCustomPool3Login_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool3SecondaryLogin_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool3Password_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void textBoxCustomPool3SecondaryPassword_TextChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void checkBoxEnablePhymem_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            if (checkBoxEnablePhymem.Checked) {
                PCIExpress.LoadPhyMem();
            } else {
                PCIExpress.UnloadPhyMem();
                foreach (var device in Controller.OpenCLDevices) {
                    foreach (var algorithm in sAlgorithmList)
                        checkBoxDeviceParameterArray[new Tuple<int, string, string>(device.DeviceIndex, algorithm, "memory_timings_enabled")].Checked = false;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void groupBoxAutomation_Enter(object sender, EventArgs e)
        {
        }

        private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void checkBoxDisableAutoStartPrompt_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void checkBoxCreateSettingsBackupWhenSettingsAreSaved_CheckedChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void dataGridViewDevices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                mAreSettingsDirty = true;
        }

        private void buttonCreateSettingsBackup_Click(object sender, EventArgs e)
        {
            try {
                CreateSettingsBackup();
            } catch (Exception ex) {
                Logger(ex);
                MessageBox.Show(this, "Failed to create backup.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRestoreSettings_Click(object sender, EventArgs e)
        {
            try {
                if (listBoxSettingBackups.SelectedIndex >= 0 && MessageBox.Show(this, "Would you like to restore settings from backup?", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                    Regex re = new Regex(@"^(..........) (..):(..)$");
                    string path = SettingsBackupPathBase + "\\" + re.Replace((string)listBoxSettingBackups.Items[listBoxSettingBackups.SelectedIndex], "$1--$2$3.sqlite");
                    LoadSettingsFromDatabase(path);
                }
            } catch (Exception ex) {
                Logger(ex);
                MessageBox.Show(this, "Failed to restore settings.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteSettingsBackup_Click(object sender, EventArgs e)
        {
            try {
                if (listBoxSettingBackups.SelectedIndex >= 0 && MessageBox.Show(this, "Would you like to delete backup?", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                    Regex re = new Regex(@"^(..........) (..):(..)$");
                    string path = SettingsBackupPathBase + "\\" + re.Replace((string)listBoxSettingBackups.Items[listBoxSettingBackups.SelectedIndex], "$1--$2$3.sqlite");
                    System.IO.File.Delete(path);
                    UpdateSettingBackupList();
                }
            } catch (Exception ex) {
                Logger(ex);
                MessageBox.Show(this, "Failed to delete backup.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try {
                if (listBoxSettingBackups.Items.Count > 0 && MessageBox.Show(this, "Would you like to delete all backups?", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                    var regex = new Regex(@"^.*\\(.*)--(..)(..)\.sqlite$");
                    foreach (string file in System.IO.Directory.GetFiles(SettingsBackupPathBase))
                        if (regex.Match(file).Success)
                            System.IO.File.Delete(file);
                    UpdateSettingBackupList();
                }
            } catch (Exception ex) {
                Logger(ex);
                MessageBox.Show(this, "Failed to delete backups.", appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerFailOver_Tick(object sender, EventArgs e)
        {
            timerFailOver.Enabled = false;
            if (Controller.AppState == Controller.ApplicationGlobalState.Idle)
                return;

            Controller.AppState = Controller.ApplicationGlobalState.Switching;
            tabControlMainForm.Enabled = buttonStart.Enabled = false;
            try {
                StopMiners();
                LaunchMiners();
            } catch (Exception) { }
            Controller.AppState = Controller.ApplicationGlobalState.Mining;
            tabControlMainForm.Enabled = buttonStart.Enabled = true;
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void MarkSettingsAsDirty(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
        }

        private void buttonOpenOpenCLBinaryFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(SavedOpenCLBinaryKernelPathBase);
        }

        private void listBoxSettingBackups_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        public static bool UseDefaultOpenCLBinariesChecked {
            get {
                if (Instance.checkBoxUseDefaultOpenCLBinaries.InvokeRequired) {
                    return (bool)Instance.Invoke(new NoArgReturningBoolDelegate(() => {
                        return Instance.checkBoxUseDefaultOpenCLBinaries.Checked;
                    }));
                } else {
                    return Instance.checkBoxUseDefaultOpenCLBinaries.Checked;
                }
            }
        }

        public static bool ReuseCompiledBinariesChecked {
            get {
                if (Instance.checkBoxReuseCompiledBinaries.InvokeRequired) {
                    return (bool)Instance.Invoke(new NoArgReturningBoolDelegate(() => {
                        return Instance.checkBoxReuseCompiledBinaries.Checked;
                    }));
                } else {
                    return Instance.checkBoxReuseCompiledBinaries.Checked;
                }
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            try { using (var file = new System.IO.StreamWriter(AppStateFilePath, false)) file.WriteLine("Mining"); } catch (Exception) { }
            Program.Exit(false);
        }

        private void buttonReleaseMemory_Click(object sender, EventArgs e)
        {
            foreach (var device in Controller.OpenCLDevices)
                device.ReleaseAllComputeBuffers();
        }

        private void comboBoxSecondGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void comboBoxSecondGraphCoverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            UpdateControls();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sgminer-dev/sgminer/blob/master/doc/API.md");
        }

        private void textBoxAllowedIPRange_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxAPIEnabled_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void buttonOpenLogContainingFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LogFilePathBase);

        }

        private void buttonBoostPerformance_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                        "DO NOT USE THIS FEATURE WITH MODDED BIOS'ES!!\n\n"
                        + "This feature will adjust overclocking/memory timing settings automatically for better performance. "
                        + "Although extensive testing has been done, it is not without risk and should be used with utmost caution. "
                        + "You can always confirm the results on the \"Devices\" tab page before you start mining.\n\n"
                        + "WARNING: Altering GPU frequency, voltage, and/or memory timings may (i) reduce system stability and useful life of "
                        + "the system and GPU; (ii) cause the GPU and other system components to fail; (iii) cause reductions "
                        + "in system performance; (iv) cause additional heat or other damage; and (v) affect system data "
                        + "integrity. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. "
                        + "SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.",
                        appName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK) {

                checkBoxEnableHardwareAccelerationForDefaultSettings.Checked = true;
                foreach (var device in Controller.OpenCLDevices)
                    ResetDeviceSettings(device);
            }
        }

        private void buttonRestoreStockSettings_Click(object sender, EventArgs e)
        {
            checkBoxEnableHardwareAccelerationForDefaultSettings.Checked = false;
            foreach (var device in Controller.OpenCLDevices)
                ResetDeviceSettings(device);
        }

        private void buttonPrintMemoryTimings_Click(object sender, EventArgs e)
        {
            foreach (var device in Controller.OpenCLDevices)
                device.PrintMemoryTimings();
        }

        delegate uint MemoryTimingDelegate(Tuple<int, string, string> tuple);
        delegate bool MemoryTimingEnabledDelegate(Tuple<int, string, string> tuple);

        public static bool GetMemoryTiming(int deviceIndex, string algorithm, string parameter, out uint value)
        {
            var tuple = new Tuple<int, string, string>(deviceIndex, algorithm, "memory_timings_" + parameter);
            var tupleEnabled = new Tuple<int, string, string>(deviceIndex, algorithm, "memory_timings_" + parameter + "_enabled");
            var enabled = false;
            if (Instance.checkBoxDeviceParameterArray[tupleEnabled].InvokeRequired) {
                enabled = (bool)Instance.textBoxDeviceParameterArray[tuple].Invoke(new MemoryTimingEnabledDelegate((deviceTuple) => {
                    try {
                        return Instance.checkBoxDeviceParameterArray[deviceTuple].Checked;
                    } catch (Exception) {
                        return false;
                    }
                }), new object[] { tupleEnabled });
            } else {
                try {
                    enabled = Instance.checkBoxDeviceParameterArray[tupleEnabled].Checked;
                } catch (Exception) {
                }
            }
            if ((new System.Text.RegularExpressions.Regex(@"^polaris10_seq_misc[138]$")).Match(parameter).Success) {
                if (Instance.textBoxDeviceParameterArray[tuple].InvokeRequired) {
                    value = (uint)Instance.textBoxDeviceParameterArray[tuple].Invoke(new MemoryTimingDelegate((deviceTuple) => {
                        try {
                            return uint.Parse(Instance.textBoxDeviceParameterArray[deviceTuple].Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo);
                        } catch (Exception) {
                            return 0;
                        }
                    }), new object[] { tuple });
                } else {
                    try {
                        value = uint.Parse(Instance.textBoxDeviceParameterArray[tuple].Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo);
                    } catch (Exception) {
                        value = 0;
                    }
                }
            } else {
                if (Instance.numericUpDownDeviceParameterArray[tuple].InvokeRequired) {
                    value = (uint)Instance.numericUpDownDeviceParameterArray[tuple].Invoke(new MemoryTimingDelegate((deviceTuple) => {
                        return (uint)Instance.numericUpDownDeviceParameterArray[tuple].Value;
                    }), new object[] { tuple });
                } else {
                    value = (uint)Instance.numericUpDownDeviceParameterArray[tuple].Value;
                }
            }
            return enabled;
        }

        private void comboBoxDefaultAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            mAreSettingsDirty = true;
            if (Controller.AppState != Controller.ApplicationGlobalState.Initializing && Controller.AppState != Controller.ApplicationGlobalState.Idle)
                timerFailOver.Enabled = true;
            if (comboBoxDefaultAlgorithm.SelectedIndex < comboBoxDefaultAlgorithm.Items.Count - 1) {
                foreach (var pair in tabControlAlgorithmsArray)
                    pair.Value.SelectedIndex = comboBoxDefaultAlgorithm.SelectedIndex;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetBenchmarkParameter(Controller.BenchmarkParameter param, bool restore = false)
        {
            if (param.Name == "default_algorithm") {
                DefaultAlgorithm = (restore) ? param.OriginalValue : param.Value;
            }
        }

        private void buttonRunBenchmarks_Click(object sender = null, EventArgs e = null)
        {
            if (Controller.AppState == Controller.ApplicationGlobalState.Idle
                && Controller.BenchmarkState == Controller.ApplicationBenchmarkState.NotRunning) {

                timerBenchmarks.Interval = Parameters.BenchmarkPreparationDurationInMilliseconds + Parameters.BenchmarkDurationInMilliseconds;
                timerBenchmarks.Enabled = true;

                timerResetStopwatch.Interval = Parameters.BenchmarkPreparationDurationInMilliseconds;
                timerResetStopwatch.Enabled = true;

                if (mAreSettingsDirty)
                    SaveSettingsToDatabase();

                // Set up benchmarks.
                Controller.BenchmarkEntries.Clear();
                Controller.CompletedBenchmarkEntries.Clear();
                foreach (var algorithm in sAlgorithmList) {
                    var param = new Controller.BenchmarkParameter("default_algorithm", algorithm, DefaultAlgorithm);
                    var entry = new Controller.BenchmarkEntry();
                    entry.Parameters.Add(param);
                    Controller.BenchmarkEntries.Add(entry);
                }

                // Start the first benchmark.
                foreach (var param in Controller.BenchmarkEntries[0].Parameters)
                    SetBenchmarkParameter(param);
                foreach (var device in Controller.OpenCLDevices) {
                    device.ClearShares();
                    device.TotalHashesPrimaryAlgorithm = device.TotalHashesSecondaryAlgorithm = 0;
                }
                mAreSettingsDirty = false;
                Controller.BenchmarkStopwatch.Reset();
                Controller.BenchmarkStopwatch.Start();
                Controller.StopWatch.Reset();
                Controller.BenchmarkState = Controller.ApplicationBenchmarkState.Running;
                buttonStart_Click();

            } else if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {

                buttonStart_Click();
                foreach (var param in Controller.BenchmarkEntries[0].Parameters)
                    SetBenchmarkParameter(param, true);
                mAreSettingsDirty = false;
                Controller.BenchmarkEntries.Clear();
                Controller.BenchmarkState = Controller.ApplicationBenchmarkState.NotRunning;
                timerBenchmarks.Enabled = false;
                timerResetStopwatch.Enabled = false;
                Controller.StopWatch.Reset();

                UpdateStats();
                UpdateControls();
            }
        }

        private void timerBenchmarks_Tick(object sender, EventArgs e)
        {
            timerResetStopwatch.Enabled = false;

            if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {
                string defaultAlgorithm = null;
                foreach (var param in Controller.BenchmarkEntries[0].Parameters) {
                    if (param.Name == "default_algorithm") {
                        defaultAlgorithm = param.Value;
                        break;
                    }
                }
                foreach (var device in Controller.OpenCLDevices) {
                    if (!(bool)dataGridViewDevices.Rows[device.DeviceIndex].Cells["enabled"].Value)
                        continue;
                    foreach (DataGridViewColumn column in dataGridViewBenchmarks.Columns) {
                        if (column.HeaderText == GetPrettyAlgorithmName(defaultAlgorithm)) {
                            dataGridViewBenchmarks.Rows[device.DeviceIndex].Cells[column.Index].Value
                                = (ConvertHashrateToString(device.TotalHashesPrimaryAlgorithm / Controller.StopWatch.Elapsed.TotalSeconds)).ToString()
                                + (device.TotalHashesSecondaryAlgorithm <= 0 ? "" : ", " + (ConvertHashrateToString(device.TotalHashesSecondaryAlgorithm / Controller.StopWatch.Elapsed.TotalSeconds)).ToString());
                            break;
                        }
                    }
                }
                Controller.CompletedBenchmarkEntries.Add(Controller.BenchmarkEntries[0]);

                if (Controller.BenchmarkEntries.Count > 1) {
                    // Start the next benchmark.
                    timerResetStopwatch.Enabled = true;
                    buttonStart_Click();
                    Controller.BenchmarkEntries.RemoveAt(0);
                    foreach (var param in Controller.BenchmarkEntries[0].Parameters)
                        SetBenchmarkParameter(param);
                    mAreSettingsDirty = false;
                    Controller.StopWatch.Reset();
                    foreach (var device in Controller.OpenCLDevices) {
                        device.ClearShares();
                        device.TotalHashesPrimaryAlgorithm = device.TotalHashesSecondaryAlgorithm = 0;
                    }
                    //foreach (var device in Controller.OpenCLDevices)
                    //    device.ReleaseAllComputeBuffers();
                    buttonStart_Click();
                } else {
                    // Finish benchmarking.
                    Controller.BenchmarkState = Controller.ApplicationBenchmarkState.NotRunning;
                    buttonStart_Click();
                    foreach (var param in Controller.BenchmarkEntries[0].Parameters)
                        SetBenchmarkParameter(param, true);
                    mAreSettingsDirty = false;
                    Controller.BenchmarkEntries.Clear();
                    Controller.StopWatch.Reset();
                    foreach (var device in Controller.OpenCLDevices) {
                        device.ClearShares();
                        device.TotalHashesPrimaryAlgorithm = device.TotalHashesSecondaryAlgorithm = 0;
                    }
                    tabControlMainForm.SelectedIndex = 4;
                }
            }
        }

        private void timerResetStopwatch_Tick(object sender, EventArgs e)
        {
            timerResetStopwatch.Enabled = false;

            if (Controller.BenchmarkState == Controller.ApplicationBenchmarkState.Running) {
                Controller.StopWatch.Reset();
                Controller.StopWatch.Start();
                foreach (var device in Controller.OpenCLDevices) {
                    device.ClearShares();
                    device.TotalHashesPrimaryAlgorithm = device.TotalHashesSecondaryAlgorithm = 0;
                }
            }
        }

        private void dataGridViewBenchmarks_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewBenchmarks.ClearSelection();
        }

        private void buttonSelectAllDevices_Click(object sender, EventArgs e)
        {
            foreach (var device in Controller.OpenCLDevices)
                dataGridViewDevices.Rows[device.DeviceIndex].Cells["enabled"].Value = true;
        }

        private void buttonDeselectAllDevices_Click(object sender, EventArgs e)
        {
            foreach (var device in Controller.OpenCLDevices)
                dataGridViewDevices.Rows[device.DeviceIndex].Cells["enabled"].Value = false;
        }
    }
}
