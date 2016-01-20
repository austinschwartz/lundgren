using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundgren.Controller
{
    public enum Direction { N, NE, E, SE, S, SW, W, NW };

    /* this is pretty garbage, fix later */

    class StickPress : ButtonPress
    {
        private static readonly Tuple<int, int> Center = Tuple.Create(128, 120);
        public byte x, y;

        public static Dictionary<Direction, Tuple<int, int>> StickOffsets = new Dictionary<Direction, Tuple<int, int>>()
        {
            { Direction.N,  Tuple.Create(0,    106)},
            { Direction.NE, Tuple.Create(77,   77)},
            { Direction.E,  Tuple.Create(106,  0)},
            { Direction.SE, Tuple.Create(77,   -77)},
            { Direction.S,  Tuple.Create(0,    -106)},
            { Direction.SW, Tuple.Create(-77,  -77)},
            { Direction.W,  Tuple.Create(-106, 0)},
            { Direction.NW, Tuple.Create(-77,  77)}
        };


        public StickPress(Direction dir, int xPercent, int yPercent)
        {
            Tuple<int, int> offsets = StickOffsets[dir];
            x = (byte)(Center.Item1 + offsets.Item1 * (.01 * xPercent));
            y = (byte)(Center.Item2 + offsets.Item2 * (.01 * yPercent));
        }

        public StickPress(Direction dir)
        {
            Tuple<int, int> offsets = StickOffsets[dir];
            x = (byte)(Center.Item1 + offsets.Item1);
            y = (byte)(Center.Item2 + offsets.Item2);
        }

        public StickPress(double r, double rad)
        {
            x = (byte)((r * Math.Cos(rad)) + 127);
            y = (byte)((r * Math.Sin(rad)) + 127);
        }
        public StickPress(double rad)
        {
            x = (byte)((127 * Math.Cos(rad)) + 127);
            y = (byte)((127 * Math.Sin(rad)) + 127);
        }

        public StickPress(double x1, double y1, double x2, double y2)
        {
            var deltaX = x1 - x2;
            var deltaY = y1 - y2;
            var rad = Math.Atan2(deltaY, deltaX);
            double r = 127;
            x = (byte)((r * Math.Cos(rad)) + 127);
            y = (byte)((r * Math.Sin(rad)) + 127);
        }

        public override string ToString()
        {
            return $"ANA: {x} {y} ";
        }
    }

    class C_StickPress : StickPress
    {
        private static readonly Tuple<int, int> Center = Tuple.Create(120, 127);

        new private static readonly Dictionary<Direction, Tuple<int, int>> StickOffsets = new Dictionary<Direction, Tuple<int, int>>()
        {
            { Direction.N,  Tuple.Create(3,   93)},
            { Direction.NE, Tuple.Create(72,  72)},
            { Direction.E,  Tuple.Create(93,  -3)},
            { Direction.SE, Tuple.Create(72,  -72)},
            { Direction.S,  Tuple.Create(3,   -93)},
            { Direction.SW, Tuple.Create(-66, -66)},
            { Direction.W,  Tuple.Create(-89, 0)},
            { Direction.NW, Tuple.Create(-66, 66)}
        };

        public C_StickPress(Direction dir, int xPercent, int yPercent) : base(dir, xPercent, yPercent)
        {
            Tuple<int, int> offsets = StickOffsets[dir];
            this.x = (byte)(Center.Item1 + offsets.Item1 * (.01 * xPercent));
            this.y = (byte)(Center.Item2 + offsets.Item2 * (.01 * yPercent));
        }

        public C_StickPress(Direction dir) : base(dir)
        {
            Tuple<int, int> offsets = StickOffsets[dir];
            this.x = (byte)(Center.Item1 + offsets.Item1);
            this.y = (byte)(Center.Item2 + offsets.Item2);
        }
        public C_StickPress(double r, double rad) : base(r, rad)
        {
            x = (byte)((r * Math.Cos(rad)) + 127);
            y = (byte)((r * Math.Sin(rad)) + 127);
        }
        public C_StickPress(double rad) : base(rad)
        {
            x = (byte)((127 * Math.Cos(rad)) + 127);
            y = (byte)((127 * Math.Sin(rad)) + 127);
        }


        public override string ToString()
        {
            return $"C: {x} {y} ";
        }
    }

    public struct StickPos
    {
        private byte x, y;

        public StickPos(byte x, byte y)
        {
            this.x = x;
            this.y = y;
        }

        public byte X
        {
            get { return x; }
            set { x = value; }
        }

        public byte Y
        {
            get { return y; }
            set { y = value; }
        }
    }
    

}
