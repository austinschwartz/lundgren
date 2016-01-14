using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lundgren.Controller
{
    // https://bitbucket.org/elmassivo/gcn-usb-adapter
    public class ControllerState
    {
        public bool A, B, X, Y, Z, R_Digital, L_Digital, D_Up, D_Right, D_Down, D_Left, Start, Enabled;
        public byte L_Analog, R_Analog, Stick_x, Stick_y, C_x, C_y;
        public int frame;

        public int POVstate;

        public ControllerState(int f)
        {
            L_Analog = 30;
            R_Analog = 30;
            Stick_x = 128;
            Stick_y = 121;
            C_x = 127;
            C_y = 127;
            A = false;
            B = false;
            X = false;
            Y = false;
            Z = false;
            R_Digital = false;
            L_Digital = false;
            D_Up = false;
            D_Right = false;
            D_Down = false;
            D_Left = false;
            Start = false;
            Enabled = true;
            frame = f;
        }

        public ControllerState(HashSet<ButtonPress> presses, int f)
        {
            frame = f;
            L_Analog = 30;
            R_Analog = 30;
            Stick_x = 128;
            Stick_y = 121;
            C_x = 127;
            C_y = 127;

            foreach (ButtonPress b in presses)
            {
                Add(b);
            }
        }

        public static ControllerState deserialize(byte[] input)
        {
            ControllerState pad = new ControllerState(0);

            if (input.Length != 9)
                throw new Exception("Invalid byte array for input");

            if ((int)input[0] <= 0)
                return pad;

            pad.A = (input[1] & (1 << 0)) != 0;
            pad.B = (input[1] & (1 << 1)) != 0;
            pad.X = (input[1] & (1 << 2)) != 0;
            pad.Y = (input[1] & (1 << 3)) != 0;

            pad.D_Left  = (input[1] & (1 << 4)) != 0;
            pad.D_Right = (input[1] & (1 << 5)) != 0;
            pad.D_Down  = (input[1] & (1 << 6)) != 0;
            pad.D_Up    = (input[1] & (1 << 7)) != 0;

            //Generate POV state for vJoy.
            if (pad.D_Right)     { pad.POVstate = 1; }
            else if (pad.D_Down) { pad.POVstate = 2; }
            else if (pad.D_Left) { pad.POVstate = 3; }
            else if (pad.D_Up)   { pad.POVstate = 0; }
            else                 { pad.POVstate = -1; }

            pad.Start       = (input[2] & (1 << 0)) != 0;
            pad.Z           = (input[2] & (1 << 1)) != 0;
            pad.R_Digital   = (input[2] & (1 << 2)) != 0;
            pad.L_Digital   = (input[2] & (1 << 3)) != 0;

            pad.Stick_x     = input[3];
            pad.Stick_y     = input[4];
            pad.C_x         = input[5];
            pad.C_y         = input[6];
            pad.L_Analog    = input[7];
            pad.R_Analog    = input[8];

            return pad;
        }

        public byte[] serialize()
        {
            byte[] output = new byte[9];

            output[0] = Enabled == true ? (byte)1 : (byte)0;
            output[1] = 0;

            if (this.A) output[1] |= (1 << 0);
            if (this.B) output[1] |= (1 << 1);
            if (this.X) output[1] |= (1 << 2);
            if (this.Y) output[1] |= (1 << 3);

            if (this.D_Left)   output[1] |= (1 << 4);
            if (this.D_Right)  output[1] |= (1 << 5);
            if (this.D_Down)   output[1] |= (1 << 6);
            if (this.D_Up)     output[1] |= (1 << 7);

            output[2] = 0;

            if (this.Start)     output[2] |= (1 << 0);
            if (this.Z)         output[2] |= (1 << 1);
            if (this.R_Digital) output[2] |= (1 << 2);
            if (this.L_Digital) output[2] |= (1 << 3);

            output[3] = this.Stick_x;
            output[4] = this.Stick_y;
            output[5] = this.C_x;
            output[6] = this.C_y;
            output[7] = this.L_Analog;
            output[8] = this.R_Analog;

            return output;
        }
        public String ToBinaryString()
        {
            StringBuilder sb = new StringBuilder();
            byte[] s = serialize();

            byte b;
            for (int i = 0; i < 9; i++)
            {
                b = s[i];
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0') + " ");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Stick_x != 128 || Stick_y != 121) sb.Append($"Ana:{Stick_x}-{Stick_y} ");
            if (R_Analog != 30) sb.Append($"R:{R_Analog} ");
            if (L_Analog != 30) sb.Append($"L:{L_Analog} ");
            if (A) sb.Append("A ");
            if (B) sb.Append("B ");
            if (X) sb.Append("X ");
            if (Y) sb.Append("Y ");
            if (L_Digital) sb.Append("L ");
            if (R_Digital) sb.Append("R ");
            return sb.ToString();
        }

        public void Add(ButtonPress b)
        {
            if (b is C_StickPress)
            {
                C_x = ((C_StickPress)b).x;
                C_y = ((C_StickPress)b).y;
            }
            else if (b is StickPress)
            {
                Stick_x = ((StickPress)b).x;
                Stick_y = ((StickPress)b).y;
            }
            else if (b is ShoulderPress)
            {
                L_Analog = ((ShoulderPress)b).L;
                R_Analog = ((ShoulderPress)b).R;
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
                    case DigitalButton.D_DOWN:
                        D_Down = true;
                        break;
                    case DigitalButton.D_LEFT:
                        D_Left = true;
                        break;
                    case DigitalButton.D_UP:
                        D_Up = true;
                        break;
                    case DigitalButton.D_RIGHT:
                        D_Right = true;
                        break;
                    case DigitalButton.R:
                        R_Digital = true;
                        break;
                    case DigitalButton.L:
                        L_Digital = true;
                        break;
                    case DigitalButton.START:
                        Start = true;
                        break;
                }
            }
        }
    }
}
