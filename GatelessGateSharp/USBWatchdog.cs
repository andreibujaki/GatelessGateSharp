using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;


namespace GatelessGateSharp
{
    class USBWatchdog
    {
        static SerialPort sSerialPort = null;
        static byte sCommand = 0x01;

        public static void Initialize()
        {
            foreach (string s in SerialPort.GetPortNames()) {
                try {
                    MainForm.Logger("Checking " + s + " for USB watchdog...");
                    var serialPort = new SerialPort(s);
                    serialPort.BaudRate = 9600;
                    serialPort.ReadTimeout = 100;
                    serialPort.WriteTimeout = 100;
                    serialPort.Open();

                    serialPort.Write(new byte[] { 0x80 }, 0, 1);

                    byte[] data = new byte[3];
                    serialPort.Read(data, 0, 3);
                    if (data[0] == 0x81) {
                        MainForm.Logger("Found USB watchdog on " + s + "!");
                        sSerialPort = serialPort;
                        sSerialPort.Write(new byte[] { 0x6 }, 0, 1);
                        break;
                    }
                    serialPort.Close();
                } catch (Exception) { }
            }
        }

        public static void KeepAlive()
        {
            try {
                if (sSerialPort != null)
                    sSerialPort.Write(new byte[] { sCommand }, 0, 1);
            } catch (Exception) { }
        }

        public static void Deactivate()
        {
            try {
                if (sSerialPort != null) {
                    sCommand = 0x7e;
                    sSerialPort.Write(new byte[] { sCommand }, 0, 1);
                    sSerialPort.Close();
                    sSerialPort = null;
                    System.Windows.Forms.MessageBox.Show("The USB watchdog is now unmanaged!", MainForm.appName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            } catch (Exception) { }
        }
    }
}
