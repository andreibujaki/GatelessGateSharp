using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;



namespace GatelessGateSharp
{
    class AMDDevice : OpenCLDevice
    {
        public AMDDevice(int aDeviceIndex, ComputeDevice aComputeDevice)
            : base(aDeviceIndex, aComputeDevice)
        {
        }
    }
}
