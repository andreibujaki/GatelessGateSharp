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
using System.Threading;
using Cloo;



namespace GatelessGateSharp
{
    class OpenCLX16RMiner : OpenCLMiner
    {
        public static readonly int sX16RInputSize = 80;
        public static readonly int sX16ROutputSize = 256;

        long[] X16RGlobalWorkSizeArray = new long[] { 0 };
        long[] X16RLocalWorkSizeArray = new long[] { 0 };
        long[] X16RGlobalWorkOffsetArray = new long[] { 0 };
        UInt32[] X16ROutput = new UInt32[sX16ROutputSize];
        byte[] X16RInput = new byte[sX16RInputSize];
        public string Variant;



        public OpenCLX16RMiner(OpenCLDevice aGatelessGateDevice, string aVariant)
            : base(aGatelessGateDevice, aVariant) {
            Variant = aVariant;
        }

        public void Start(X16RStratum aX16RStratum, int aX16RIntensity, int aX16RLocalWorkSize, int aKernelOptimizationLevel = -1)
        {
            KernelOptimizationLevel = aKernelOptimizationLevel;
            Stratum = aX16RStratum;
            X16RGlobalWorkSizeArray[0] = aX16RIntensity * OpenCLDevice.GetMaxComputeUnits() * aX16RLocalWorkSize;
            X16RLocalWorkSizeArray[0] = aX16RLocalWorkSize;

            base.Start();
        }

        public void BuildX16RProgram() {
        }

        public X16RStratum Stratum { get; set; }

        public override void SetPrimaryStratum(StratumServer stratum) {
            Stratum = (X16RStratum)stratum;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        [System.Security.SecurityCritical]
        override unsafe protected void MinerThread() {
            ComputeProgram program = null;

            try { 
                Random r = new Random();

                MarkAsAlive();

                MainForm.Logger("Miner thread for Device #" + DeviceIndex + " started.");

                program = BuildProgram("x16r", X16RLocalWorkSizeArray[0],
                    "",
                    "", 
                    "");

                using (var X16RSearchKernel = program.CreateKernel("search"))
                using (var X16RSearch1Kernel = program.CreateKernel("search1"))
                using (var X16RSearch2Kernel = program.CreateKernel("search2"))
                using (var X16RSearch3Kernel = program.CreateKernel("search3"))
                using (var X16RSearch4Kernel = program.CreateKernel("search4"))
                using (var X16RSearch5Kernel = program.CreateKernel("search5"))
                using (var X16RSearch6Kernel = program.CreateKernel("search6"))
                using (var X16RSearch7Kernel = program.CreateKernel("search7"))
                using (var X16RSearch8Kernel = program.CreateKernel("search8"))
                using (var X16RSearch9Kernel = program.CreateKernel("search9"))
                using (var X16RSearch10Kernel = program.CreateKernel("search10"))
                using (var X16RSearch11Kernel = program.CreateKernel("search11"))
                using (var X16RSearch12Kernel = program.CreateKernel("search12"))
                using (var X16RSearch13Kernel = program.CreateKernel("search13"))
                using (var X16RSearch14Kernel = program.CreateKernel("search14"))
                using (var X16RSearch15Kernel = program.CreateKernel("search15"))
                using (var X16RSearch16Kernel = program.CreateKernel("search16"))
                using (var X16RSearch17Kernel = program.CreateKernel("search17"))
                using (var X16RSearch18Kernel = program.CreateKernel("search18"))
                using (var X16RSearch19Kernel = program.CreateKernel("search19"))
                using (var X16RSearch20Kernel = program.CreateKernel("search20"))
                using (var X16RSearch21Kernel = program.CreateKernel("search21"))
                using (var X16RSearch22Kernel = program.CreateKernel("search22"))
                using (var X16RSearch23Kernel = program.CreateKernel("search23"))
                using (var X16RSearch24Kernel = program.CreateKernel("search24"))
                using (var X16RSearch25Kernel = program.CreateKernel("search25"))
                using (var X16RSearch26Kernel = program.CreateKernel("search26"))
                using (var X16RSearch27Kernel = program.CreateKernel("search27"))
                using (var X16RSearch28Kernel = program.CreateKernel("search28"))
                using (var X16RSearch29Kernel = program.CreateKernel("search29"))
                using (var X16RSearch30Kernel = program.CreateKernel("search30"))
                using (var X16RSearch31Kernel = program.CreateKernel("search31"))
                using (var X16RSearch32Kernel = program.CreateKernel("search32"))
                using (var X16RInputBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadOnly, sX16RInputSize))
                using (var X16ROutputBuffer = new ComputeBuffer<UInt32>(Context, ComputeMemoryFlags.ReadWrite, sX16ROutputSize))
                fixed (long* X16RGlobalWorkOffsetArrayPtr = X16RGlobalWorkOffsetArray)
                fixed (long* X16RGlobalWorkSizeArrayPtr = X16RGlobalWorkSizeArray)
                fixed (long* X16RLocalWorkSizeArrayPtr = X16RLocalWorkSizeArray)
                fixed (byte* X16RInputPtr = X16RInput)
                fixed (UInt32* X16ROutputPtr = X16ROutput)
                while (!Stopped) {
                    MarkAsAlive();

                    try {
                        using (var X16RHashesBuffer = new ComputeBuffer<byte>(Context, ComputeMemoryFlags.ReadWrite, (X16RGlobalWorkSizeArray[0] * 64))) { 
                            X16RSearchKernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearchKernel.SetMemoryArgument(1, X16ROutputBuffer);
                            
                            X16RSearch1Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch2Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch3Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch4Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch5Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch6Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch7Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch8Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch9Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch10Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch11Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch12Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch13Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch14Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch15Kernel.SetMemoryArgument(0, X16RHashesBuffer);
                            X16RSearch16Kernel.SetMemoryArgument(0, X16RHashesBuffer);

                            X16RSearch17Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch18Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch19Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch20Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch21Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch22Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch23Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch24Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch25Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch26Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch27Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch28Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch29Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch30Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch31Kernel.SetMemoryArgument(0, X16RInputBuffer);
                            X16RSearch32Kernel.SetMemoryArgument(0, X16RInputBuffer);

                            X16RSearch17Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch18Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch19Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch20Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch21Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch22Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch23Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch24Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch25Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch26Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch27Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch28Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch29Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch30Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch31Kernel.SetMemoryArgument(1, X16RHashesBuffer);
                            X16RSearch32Kernel.SetMemoryArgument(1, X16RHashesBuffer);

                            // Wait for the first job to arrive.
                            int elapsedTime = 0;
                            while ((Stratum == null || Stratum.GetJob() == null) && elapsedTime < Parameters.TimeoutForFirstJobInMilliseconds && !Stopped) {
                                Thread.Sleep(100);
                                elapsedTime += 100;
                            }
                            if (Stratum == null || Stratum.GetJob() == null)
                                throw new TimeoutException("Stratum server failed to send a new job.");

                            System.Diagnostics.Stopwatch consoleUpdateStopwatch = new System.Diagnostics.Stopwatch();
                            X16RStratum.Work X16RWork;
                            X16RStratum.Job X16RJob;

                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();
                            while (!Stopped && (X16RWork = Stratum.GetWork()) != null && (X16RJob = X16RWork.Job) != null) {
                                MarkAsAlive();

                                UInt32 X16RStartNonce = (UInt32)(r.Next(0, int.MaxValue));

                                Array.Copy(X16RWork.Blob, X16RInput, sX16RInputSize);
                                Queue.Write<byte>(X16RInputBuffer, true, 0, sX16RInputSize, (IntPtr)X16RInputPtr, null);

                                var reversed = X16RInput;
                                Array.Reverse(reversed);
                                var order = Utilities.ByteArrayToString(reversed);
                                order = order.Substring(order.Length - 16 - 8, 16);
                                
                                if (Variant == "x16s") {
                                    string originalOrder = order;
                                    order = "0123456789abcdef";
                                    while (originalOrder.Length > 0) {
                                        string c = originalOrder.Substring(0, 1);
                                        originalOrder = originalOrder.Substring(1);
                                        int index = (int)(Utilities.StringToByteArray("0" + c)[0]);
                                        order = order.Substring(index, 1) + order.Substring(0, index) + order.Substring(index + 1, 15 - index);
                                    }
                                }
                                
                                consoleUpdateStopwatch.Start();

                                while (!Stopped && Stratum.GetJob() != null && Stratum.GetJob().Equals(X16RJob)) {

                                    MarkAsAlive();

                                    UInt64 x16RTarget = (UInt64)((double)0xffff0000U / (Stratum.Difficulty / 256));
                                    //MainForm.Logger("Target: " + String.Format("{0:x16}", x16RTarget));
                                    X16RSearchKernel.SetValueArgument<UInt64>(2, x16RTarget);
                                    X16RGlobalWorkOffsetArray[0] = X16RStartNonce;

                                    // Get a new local extranonce if necessary.
                                    if (0xffffffffu - X16RStartNonce < (UInt32)X16RGlobalWorkSizeArray[0])
                                        break;

                                    X16ROutput[255] = 0; // X16ROutput[255] is used as an atomic counter.
                                    Queue.Write<UInt32>(X16ROutputBuffer, true, 0, sX16ROutputSize, (IntPtr)X16ROutputPtr, null);
                                    
                                    string s = order;
                                    string c = s.Substring(0, 1);
                                    if (c == "0") {
                                        Queue.Execute(X16RSearch17Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "1") {
                                        Queue.Execute(X16RSearch18Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "2") {
                                        Queue.Execute(X16RSearch19Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "3") {
                                        Queue.Execute(X16RSearch20Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "4") {
                                        Queue.Execute(X16RSearch21Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "5") {
                                        Queue.Execute(X16RSearch22Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "6") {
                                        Queue.Execute(X16RSearch23Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "7") {
                                        Queue.Execute(X16RSearch24Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "8") {
                                        Queue.Execute(X16RSearch25Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "9") {
                                        Queue.Execute(X16RSearch26Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "a") {
                                        Queue.Execute(X16RSearch27Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "b") {
                                        Queue.Execute(X16RSearch28Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "c") {
                                        Queue.Execute(X16RSearch29Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "d") {
                                        Queue.Execute(X16RSearch30Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "e") {
                                        Queue.Execute(X16RSearch31Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else if (c == "f") {
                                        Queue.Execute(X16RSearch32Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    } else {
                                        throw new Exception();
                                    }
                                    s = s.Substring(1);
                                    while (s.Length > 0) {
                                        c = s.Substring(0, 1);
                                        if (c == "0") { 
                                            Queue.Execute(X16RSearch1Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "1") { 
                                            Queue.Execute(X16RSearch2Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "2") { 
                                            Queue.Execute(X16RSearch3Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "3") { 
                                            Queue.Execute(X16RSearch4Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "4") { 
                                            Queue.Execute(X16RSearch5Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "5") { 
                                            Queue.Execute(X16RSearch6Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "6") { 
                                            Queue.Execute(X16RSearch7Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "7") { 
                                            Queue.Execute(X16RSearch8Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "8") { 
                                            Queue.Execute(X16RSearch9Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "9") { 
                                            Queue.Execute(X16RSearch10Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "a") { 
                                            Queue.Execute(X16RSearch11Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "b") { 
                                            Queue.Execute(X16RSearch12Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "c") { 
                                            Queue.Execute(X16RSearch13Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "d") { 
                                            Queue.Execute(X16RSearch14Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "e") { 
                                            Queue.Execute(X16RSearch15Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else if (c == "f") { 
                                            Queue.Execute(X16RSearch16Kernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                        } else {
                                            throw new Exception();
                                        }
                                        s = s.Substring(1);
                                    }
                                    Queue.Execute(X16RSearchKernel, X16RGlobalWorkOffsetArray, X16RGlobalWorkSizeArray, X16RLocalWorkSizeArray, null); Queue.Finish();
                                    Queue.Read<UInt32>(X16ROutputBuffer, true, 0, sX16ROutputSize, (IntPtr)X16ROutputPtr, null);
                                    if (Stratum.GetJob() != null && Stratum.GetJob().Equals(X16RJob) && X16ROutput[255] > 0) {
                                        MainForm.Logger("X16R order: " + order);
                                        for (int i = 0; i < X16ROutput[255]; ++i)
                                            Stratum.Submit(Device, X16RWork, X16ROutput[i]);
                                    }
                                    X16RStartNonce += (UInt32)X16RGlobalWorkSizeArray[0];

                                    ReportHashCount((double)X16RGlobalWorkSizeArray[0], 0, sw.Elapsed.TotalSeconds);
                                    sw.Restart();
                                    if (consoleUpdateStopwatch.ElapsedMilliseconds >= 10 * 1000) {
                                        MainForm.Logger("Device #" + DeviceIndex + ": " + String.Format("{0:N2} Mh/s (X16R)", Speed / 1000000));
                                        consoleUpdateStopwatch.Restart();
                                    }
                                }
                            }
                        }
                    } catch (Exception ex) {
                        MainForm.Logger("Exception in miner thread: " + ex.Message + ex.StackTrace);
                        if (UnrecoverableException.IsUnrecoverableException(ex)) {
                            this.UnrecoverableException = new UnrecoverableException(ex, Device);
                            Stop();
                        }
                    }

                    Speed = 0;

                    if (!Stopped) {
                        MainForm.Logger("Restarting miner thread...");
                        System.Threading.Thread.Sleep(Parameters.WaitTimeForRestartingMinerThreadInMilliseconds);
                    }
                }
            } catch (UnrecoverableException ex) {
                this.UnrecoverableException = ex;
            } catch (Exception ex) {
                this.UnrecoverableException = new UnrecoverableException(ex, Device);
            } finally {
                MarkAsDone();
                MemoryUsage = 0;
                if (program != null) { program.Dispose(); program = null; }
            }
        }
    }
}

