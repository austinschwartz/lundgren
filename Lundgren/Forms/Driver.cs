using System;
using System.Diagnostics;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using Lundgren.Controller;
using Lundgren.Game;
using Lundgren.Logs;
using vJoyInterfaceWrap;

namespace Lundgren
{
    public class Driver
    {
        private Forms.LundgrenForm _lundgrenForm;

        public static ControllerDeadZones gcn1DZ;

        public static bool run = false;
        public static bool noEventMode = true;
        public static bool gcn1Enabled = true;

        private vJoy gcn1 = new vJoy();
        private bool gcn1ok = false;

        private UsbEndpointReader reader = null;
        private UsbEndpointWriter writer = null;
        private UsbDevice GCNAdapter = null;
        private IUsbDevice wholeGCNAdapter = null;

        public Driver(Forms.LundgrenForm lundgrenForm)
        {
            _lundgrenForm = lundgrenForm;
        }

        public static event EventHandler<Logging.LogEventArgs> DriverLog;
        public static event EventHandler<Logging.LogEventArgs> InputLog;

        public void Start()
        {
            var USBFinder = new UsbDeviceFinder(0x057E, 0x0337);
            GCNAdapter = UsbDevice.OpenUsbDevice(USBFinder);

            gcn1DZ = new ControllerDeadZones();

            if (GCNAdapter != null)
            {
                int transferLength;
                
                writer = GCNAdapter.OpenEndpointWriter(WriteEndpointID.Ep02);

                //prompt controller to start sending
                writer.Write(Convert.ToByte((char)19), 10, out transferLength);

                try
                {
                    if (!JoystickHelper.checkJoystick(ref gcn1, 1)) { SystemHelper.CreateJoystick(1); }

                    if (gcn1Enabled && gcn1.AcquireVJD(1))
                    {
                        gcn1ok = true;
                        gcn1.ResetAll();
                    }
                }
                catch (Exception ex)
                {
                    DriverLog(null, new Logging.LogEventArgs("Error: " + ex.ToString()));
                    if (ex.Message.Contains("HRESULT: 0x8007000B"))
                    {
                        DriverLog(null, new Logging.LogEventArgs("Error: vJoy driver mismatch. Did you install the wrong version (x86/x64)?"));
                        Driver.run = false;
                        return;
                    }
                }

                if (noEventMode)
                {
                    DriverLog(null, new Logging.LogEventArgs("Driver successfully started, entering input loop."));
                    run = true;

                    while (run)
                    {
                        if (gcn1ok)
                        {
                            JoystickHelper.setJoystick(ref gcn1, _lundgrenForm.CurrentAI.State, 1, gcn1DZ);
                        }
                    }

                    if (GCNAdapter != null)
                    {
                        if (GCNAdapter.IsOpen)
                        {
                            if (!ReferenceEquals(wholeGCNAdapter, null))
                            {
                                wholeGCNAdapter.ReleaseInterface(0);
                            }
                            GCNAdapter.Close();
                        }
                        GCNAdapter = null;
                        UsbDevice.Exit();
                        DriverLog(null, new Logging.LogEventArgs("Closing driver thread..."));
                    }
                    DriverLog(null, new Logging.LogEventArgs("Driver thread has been stopped."));
                }
                else
                {
                    DriverLog(null, new Logging.LogEventArgs("Driver successfully started, entering input loop."));
                    //using  Interrupt request instead of looping behavior.
                    reader.DataReceivedEnabled = true;
                    reader.DataReceived += reader_DataReceived;
                    reader.ReadBufferSize = 37;
                    reader.ReadThreadPriority = System.Threading.ThreadPriority.Highest;
                    run = true;
                }
            }
            else
            {
                DriverLog(null, new Logging.LogEventArgs("GCN Adapter not detected."));
                Driver.run = false;
            }
        }

        private byte[] getFastInput1(ref byte[] input)
        {
            return new byte[] { input[1], input[2], input[3], input[4], input[5], input[6], input[7], input[8], input[9] };
        }

        public void reader_DataReceived(object sender, EndpointDataEventArgs e)
        {
            if (run)
            {
                var data = e.Buffer;
                var input1 = ControllerState.deserialize(getFastInput1(ref data));

                if (gcn1ok) { JoystickHelper.setJoystick(ref gcn1, input1, 1, gcn1DZ); }
            }
            else
            {
                reader.DataReceivedEnabled = false;

                if (GCNAdapter != null)
                {
                    if (GCNAdapter.IsOpen)
                    {
                        if (!ReferenceEquals(wholeGCNAdapter, null))
                        {
                            wholeGCNAdapter.ReleaseInterface(0);
                        }
                        GCNAdapter.Close();
                    }
                    GCNAdapter = null;
                    UsbDevice.Exit();
                    DriverLog(null, new Logging.LogEventArgs("Closing driver thread..."));
                }
                DriverLog(null, new Logging.LogEventArgs("Driver thread has been stopped."));
            }
        }
    }
}