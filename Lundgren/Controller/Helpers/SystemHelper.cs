﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;
using Microsoft.Win32;

using HardwareHelperLib;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Lundgren.Logs;

namespace Lundgren.Controller
{
    // https://bitbucket.org/elmassivo/gcn-usb-adapter
    public class SystemHelper
    {
        public static event EventHandler<Logging.LogEventArgs> Log;

        #region Windows Version
        public static double GetOSVersion()
        {
            switch (System.Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S: return 3.1;
                case PlatformID.Win32Windows:
                    switch (System.Environment.OSVersion.Version.Minor)
                    {
                        case 0: return 3.2;
                        case 10: return 3.3;
                        case 90: return 3.4;
                    }
                    break;
                case PlatformID.Win32NT:
                    switch (System.Environment.OSVersion.Version.Major)
                    {
                        case 3: return 3.51;
                        case 4: return 4.0;
                        case 5:
                            switch (System.Environment.OSVersion.Version.Minor)
                            {
                                case 0: return 5.0;
                                case 1: return 5.1;
                                case 2: return 5.2;
                            }
                            break;
                        case 6:
                            switch (System.Environment.OSVersion.Version.Minor)
                            {
                                case 0: return 6.0; // Vista
                                case 1: return 6.1; // 7.0
                                case 2: return 6.2; // 8.0
                                case 3: return 6.3; // 8.1
                                case 4: return 6.4; // 10 TP
                            }
                            break;
                        case 10: return 10.0; // 10?
                    }
                    break;
                case PlatformID.WinCE: return 2.0;
            }
            return 0;
        }
        #endregion

        #region Driver Functions
        public static void checkForMissingDrivers()
        {
            bool vJoy = false;
            bool libUsb = false;
            try
            {
                SelectQuery query = new SelectQuery("Win32_SystemDriver");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                var drivers = searcher.Get();
                foreach (var d in drivers)
                {
                    if (d["Name"].ToString() == "vjoy") vJoy = true;
                    if (d["Name"].ToString().Contains("libusb")) libUsb = true;
                    if (vJoy && libUsb) break;
                }
                if (!vJoy)
                {
                    Log(null, new Logging.LogEventArgs("vJoy was not detected, please rerun the installer."));
                }
            }
            catch
            {
                Log(null, new Logging.LogEventArgs("Driver check failed, (Are you running as Administrator?)"));
            }
        }
        #endregion

        #region External vJoy Functions
        //as of vjoy 2.05, x32 and x64 binaries are separated
        static string vJoyDirectory = Path.GetPathRoot(System.Environment.SystemDirectory) + @"Program Files\vJoy" + ((IntPtr.Size == 8) ? @"\x64\" : @"\x86\");

        public static void CreateJoystick(uint portNum)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                WorkingDirectory = vJoyDirectory,
                FileName = "vJoyConfig.exe",
                Arguments = portNum + " -f -a x y z rx ry rz -b 12", //create
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            var p = new Process();
            p.StartInfo = psi;
            try
            {
                Log(null, new Logging.LogEventArgs("Enabling port " + portNum + "..."));
                p.Start();
                while (!p.HasExited)
                {
                    Thread.Sleep(500);
                }
                Log(null, new Logging.LogEventArgs("Port " + portNum + " is detected (OK)."));
            }
            catch
            {
                Log(null, new Logging.LogEventArgs("Error: Unable to complete configuration for port " + portNum + ". (Check vJoy install?)"));
            }
        }

        public static void DestroyJoystick(int portNum)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                WorkingDirectory = vJoyDirectory,
                FileName = "vJoyConfig.exe",
                Arguments = "-d " + portNum, //delete
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            var p = new Process();
            p.StartInfo = psi;
            try
            {
                p.Start();
                while (!p.HasExited)
                {
                    Thread.Sleep(500);
                }
                Log(null, new Logging.LogEventArgs("Port " + portNum + " disabled."));
            }
            catch
            {
                Log(null, new Logging.LogEventArgs("Error: Unable to complete configuration for port " + portNum + ". (Check vJoy install?)"));
            }
        }

        public static void EnablevJoyDLL()
        {
            try
            {
                HardwareHelperLib.HH_Lib lb = new HH_Lib();
                lb.SetDeviceState("vJoy Device", true);
            }
            catch
            {
                Log(null, new Logging.LogEventArgs("Error: Unable to complete vJoy enable."));
            }
        }
        //This is a workaround since vJoy does not support disabling all 4 joysticks.
        public static void DisablevJoyDLL()
        {
            try
            {
                HardwareHelperLib.HH_Lib lb = new HH_Lib();
                lb.SetDeviceState("vJoy Device", false);
            }
            catch
            {
                Log(null, new Logging.LogEventArgs("Error: Unable to disable vJoy."));
            }
        }
        #endregion
    }
}
