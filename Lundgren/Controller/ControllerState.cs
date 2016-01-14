using System;
using System.Collections.Generic;
using System.Text;
namespace Lundgren.Controller
{
    // https://bitbucket.org/elmassivo/gcn-usb-adapter
    public class ControllerState
    {
        public bool A, B, X, Y, Z, RDigital, LDigital, DUp, DRight, DDown, DLeft, Start, Enabled;
        public byte LAnalog, RAnalog, StickX, StickY, CX, CY;
        public int Frame;

        public int POVstate;

        public ControllerState(int f)
        {
            LAnalog = 30;
            RAnalog = 30;
            StickX = 128;
            StickY = 121;
            CX = 127;
            CY = 127;
            A = false;
            B = false;
            X = false;
            Y = false;
            Z = false;
            RDigital = false;
            LDigital = false;
            DUp = false;
            DRight = false;
            DDown = false;
            DLeft = false;
            Start = false;
            Enabled = true;
            Frame = f;
        }

        public ControllerState(HashSet<ButtonPress> presses, int f)
        {
            Frame = f;
            LAnalog = 30;
            RAnalog = 30;
            StickX = 128;
            StickY = 121;
            CX = 127;
            CY = 127;

            foreach (ButtonPress b in presses)
            {
                Add(b);
            }
        }

        public static ControllerState deserialize(byte[] input)
        {
            var pad = new ControllerState(0);

            if (input.Length != 9)
                throw new Exception("Invalid byte array for input");

            if ((int)input[0] <= 0)
                return pad;

            pad.A = (input[1] & (1 << 0)) != 0;
            pad.B = (input[1] & (1 << 1)) != 0;
            pad.X = (input[1] & (1 << 2)) != 0;
            pad.Y = (input[1] & (1 << 3)) != 0;

            pad.DLeft  = (input[1] & (1 << 4)) != 0;
            pad.DRight = (input[1] & (1 << 5)) != 0;
            pad.DDown  = (input[1] & (1 << 6)) != 0;
            pad.DUp    = (input[1] & (1 << 7)) != 0;

            //Generate POV state for vJoy.
            if (pad.DRight)     { pad.POVstate = 1; }
            else if (pad.DDown) { pad.POVstate = 2; }
            else if (pad.DLeft) { pad.POVstate = 3; }
            else if (pad.DUp)   { pad.POVstate = 0; }
            else                 { pad.POVstate = -1; }

            pad.Start       = (input[2] & (1 << 0)) != 0;
            pad.Z           = (input[2] & (1 << 1)) != 0;
            pad.RDigital   = (input[2] & (1 << 2)) != 0;
            pad.LDigital   = (input[2] & (1 << 3)) != 0;

            pad.StickX     = input[3];
            pad.StickY     = input[4];
            pad.CX         = input[5];
            pad.CY         = input[6];
            pad.LAnalog    = input[7];
            pad.RAnalog    = input[8];

            return pad;
        }

        public byte[] Serialize()
        {
            byte[] output = new byte[9];

            output[0] = Enabled == true ? (byte)1 : (byte)0;
            output[1] = 0;

            if (A) output[1] |= (1 << 0);
            if (B) output[1] |= (1 << 1);
            if (X) output[1] |= (1 << 2);
            if (Y) output[1] |= (1 << 3);

            if (DLeft)   output[1] |= (1 << 4);
            if (DRight)  output[1] |= (1 << 5);
            if (DDown)   output[1] |= (1 << 6);
            if (DUp)     output[1] |= (1 << 7);

            output[2] = 0;

            if (Start)     output[2] |= (1 << 0);
            if (Z)         output[2] |= (1 << 1);
            if (RDigital) output[2] |= (1 << 2);
            if (LDigital) output[2] |= (1 << 3);

            output[3] = StickX;
            output[4] = StickY;
            output[5] = CX;
            output[6] = CY;
            output[7] = LAnalog;
            output[8] = RAnalog;

            return output;
        }
        public string ToBinaryString()
        {
            var sb = new StringBuilder();
            var s = Serialize();

            byte b;
            for (var i = 0; i < 9; i++)
            {
                b = s[i];
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0') + " ");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (StickX != 128 || StickY != 121) sb.Append($"Ana:{StickX}-{StickY} ");
            if (RAnalog != 30) sb.Append($"R:{RAnalog} ");
            if (LAnalog != 30) sb.Append($"L:{LAnalog} ");
            if (A) sb.Append("A ");
            if (B) sb.Append("B ");
            if (X) sb.Append("X ");
            if (Y) sb.Append("Y ");
            if (LDigital) sb.Append("L ");
            if (RDigital) sb.Append("R ");
            return sb.ToString();
        }

        public void Add(ButtonPress b)
        {
            if (b is C_StickPress)
            {
                CX = ((C_StickPress)b).x;
                CY = ((C_StickPress)b).y;
            }
            else if (b is StickPress)
            {
                StickX = ((StickPress)b).x;
                StickY = ((StickPress)b).y;
            }
            else if (b is ShoulderPress)
            {
                LAnalog = ((ShoulderPress)b).L;
                RAnalog = ((ShoulderPress)b).R;
            }
            else if (b is DigitalPress)
            {
                DigitalButton db = ((DigitalPress)b).db;
                switch (db)
                {
                    case DigitalButton.A:
                        A = true;
                        break;
                    case DigitalButton.B:
                        B = true;
                        break;
                    case DigitalButton.Y:
                        Y = true;
                        break;
                    case DigitalButton.X:
                        X = true;
                        break;
                    case DigitalButton.Z:
                        Z = true;
                        break;
                    case DigitalButton.DDown:
                        DDown = true;
                        break;
                    case DigitalButton.DLeft:
                        DLeft = true;
                        break;
                    case DigitalButton.DUp:
                        DUp = true;
                        break;
                    case DigitalButton.DRight:
                        DRight = true;
                        break;
                    case DigitalButton.R:
                        RDigital = true;
                        break;
                    case DigitalButton.L:
                        LDigital = true;
                        break;
                    case DigitalButton.Start:
                        Start = true;
                        break;
                }
            }
        }
    }
}
