using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;



namespace GatelessGateSharp
{
    class CLRX
    {
        public CLRX()
        {
        }

        public byte[] Assemble(OpenCLDevice device, string sourceFilePath)
        {
            try {
                string outputFilePath = System.IO.Path.GetTempFileName();
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                startInfo.FileName = @"CLRX\clrxasm";
                startInfo.Arguments = sourceFilePath + " -o \"" + outputFilePath + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;
                process.Start();
                while (!process.StandardOutput.EndOfStream) {
                    string line = process.StandardOutput.ReadLine();
                    MainForm.Logger(line);
                }
                process.WaitForExit();

                byte[] binary = System.IO.File.ReadAllBytes(outputFilePath);
                System.IO.File.Delete(outputFilePath);

                return binary;
            } catch (Exception ex) {
                MainForm.Logger("CLRX failed.");
                MainForm.Logger(ex);
                throw ex;
            }
        }
    }
}
