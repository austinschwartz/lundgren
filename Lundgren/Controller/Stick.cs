using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundgren.Controller
{
    public enum Direction { N, NE, E, SE, S, SW, W, NW };

    class StickPress : AnalogPress
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
