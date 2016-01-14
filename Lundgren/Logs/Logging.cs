using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundgren.Logs
{
    public class Logging
    {
        public class LogEventArgs : EventArgs
        {
            public LogEventArgs(string text = "")
            {
                _text = text;
            }

            private string _text;
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }
        }
    }
}
