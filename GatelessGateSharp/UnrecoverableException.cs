using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatelessGateSharp {
    class UnrecoverableException : Exception {
        public static bool IsUnrecoverableException(Exception ex) {
            if (ex.Message == "OpenCL error code detected: InvalidBufferSize.")
                return true;
            return false;
        }

        public UnrecoverableException(string s)
            : base(s) {
        }

        public UnrecoverableException(Exception ex, Device device)
            : base(ex.Message == "OpenCL error code detected: InvalidBufferSize." ? "Not enough memory on Device #" + device.DeviceIndex + " (" + device.GetVendor() + " " + device.GetName() + ").\nIntensity may be too high." :
                                                                                    ex.Message) {
        }
    }
}
