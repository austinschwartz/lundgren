using Lundgren.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace Lundgren.Controller
{
    // https://bitbucket.org/elmassivo/gcn-usb-adapter
    public class JoystickHelper
    {
        public static event EventHandler<Logging.LogEventArgs> JoystickLog;
        public static void setJoystick(ref vJoy joystick, ControllerState input, uint joystickID, ControllerDeadZones deadZones)
        {
            bool res;
            var multiplier = 127;

            if (!deadZones.analogStick.inDeadZone(input.StickX, input.StickY))
            {
                res = joystick.SetAxis(multiplier * input.StickX, joystickID, HID_USAGES.HID_USAGE_X);
                res = joystick.SetAxis(multiplier * (255 - input.StickY), joystickID, HID_USAGES.HID_USAGE_Y);
            }
            else
            {
                res = joystick.SetAxis(multiplier * 129, joystickID, HID_USAGES.HID_USAGE_X);
                res = joystick.SetAxis(multiplier * 129, joystickID, HID_USAGES.HID_USAGE_Y);
            }
            
            if (!deadZones.cStick.inDeadZone(input.CX, input.CY))
            {
                res = joystick.SetAxis(multiplier * input.CX, joystickID, HID_USAGES.HID_USAGE_RX);
                res = joystick.SetAxis(multiplier * (255 - input.CY), joystickID, HID_USAGES.HID_USAGE_RY);
            }
            else
            {
                res = joystick.SetAxis(multiplier * 129, joystickID, HID_USAGES.HID_USAGE_RX);
                res = joystick.SetAxis(multiplier * 129, joystickID, HID_USAGES.HID_USAGE_RY);
            }
            
            if (!deadZones.LTrigger.inDeadZone(input.LAnalog))
            {
                res = joystick.SetAxis(multiplier * input.LAnalog, joystickID, HID_USAGES.HID_USAGE_Z);
            }
            else
            {
                res = joystick.SetAxis(0, joystickID, HID_USAGES.HID_USAGE_Z);
            }
            if (!deadZones.RTrigger.inDeadZone(input.RAnalog))
            {
                res = joystick.SetAxis(multiplier * input.RAnalog, joystickID, HID_USAGES.HID_USAGE_RZ);
            }
            else
            {
                res = joystick.SetAxis(0, joystickID, HID_USAGES.HID_USAGE_RZ);
            }

            //dpad button mode for DDR pad support
            res = joystick.SetBtn(input.DUp, joystickID, 9);
            res = joystick.SetBtn(input.DDown, joystickID, 10);
            res = joystick.SetBtn(input.DLeft, joystickID, 11);
            res = joystick.SetBtn(input.DRight, joystickID, 12);

            //buttons
            res = joystick.SetBtn(input.A, joystickID, 1);
            res = joystick.SetBtn(input.B, joystickID, 2);
            res = joystick.SetBtn(input.X, joystickID, 3);
            res = joystick.SetBtn(input.Y, joystickID, 4);
            res = joystick.SetBtn(input.Z, joystickID, 5);
            res = joystick.SetBtn(input.RDigital, joystickID, 6);
            res = joystick.SetBtn(input.LDigital, joystickID, 7);
            res = joystick.SetBtn(input.Start, joystickID, 8);
        }

        public static bool checkJoystick(ref vJoy joystick, uint id)
        {
            bool checker = false;
            if (joystick.vJoyEnabled())
            {
                VjdStat status = joystick.GetVJDStatus(id);

                switch (status)
                {
                    case VjdStat.VJD_STAT_OWN:
                        JoystickLog(null, new Logging.LogEventArgs($"Port {id} is already owned by this feeder (OK)."));
                        checker = true;
                        break;
                    case VjdStat.VJD_STAT_FREE:
                        JoystickLog(null, new Logging.LogEventArgs($"Port {id} is detected (OK)."));
                        checker = true;
                        break;
                    case VjdStat.VJD_STAT_BUSY:
                        JoystickLog(null, new Logging.LogEventArgs(
                            $"Port {id} is already owned by another feeder, cannot continue."));
                        checker = false;
                        return checker;
                    case VjdStat.VJD_STAT_MISS:
                        JoystickLog(null, new Logging.LogEventArgs($"Port {id} is not detected."));
                        checker = false;
                        return checker;
                    default:
                        JoystickLog(null, new Logging.LogEventArgs($"Port {id} general error, cannot continue."));
                        checker = false;
                        return checker;
                }

                //fix missing buttons, if the count is off.
                if (joystick.GetVJDButtonNumber(id) != 12)
                {
                    SystemHelper.CreateJoystick(id);
                }
            }
            return checker;
        }
    }

}
