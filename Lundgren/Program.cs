using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Lundgren
{
    static class Program {
        [STAThread]
        static void Main()
        {
            //Ladder l = new Ladder();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.LundgrenForm());
        }
    }
}
