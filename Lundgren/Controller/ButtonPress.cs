using System.Text;

namespace Lundgren.Controller
{
    public enum DigitalButton { A, B, X, Y, L, R, Z, START, D_UP, D_RIGHT, D_DOWN, D_LEFT };

    public class ButtonPress { }
    class AnalogPress : ButtonPress { }

    class ShoulderPress : AnalogPress
    {
        public byte L, R;
        public ShoulderPress(byte l = 0, byte r = 0)
        {
            L = l;
            R = r;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (L != 0) sb.Append($"L:{L} ");
            if (R != 0) sb.Append($"R:{R} ");
            return sb.ToString();
        }
    }


    class DigitalPress : ButtonPress
    {
        public DigitalButton db;
        public DigitalPress(DigitalButton _db)
        {
            this.db = _db;
        }

        public override string ToString()
        {
            return db.ToString() + " ";
        }
    }
}
