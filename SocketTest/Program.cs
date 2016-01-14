using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lundgren;

namespace SocketTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var actuator = new Forms.Controller();
            var client = new Forms.Client();
            var server = new Forms.Server();
            actuator.Show();
            client.Show();
            server.Show();
            Application.Run();
        }
    }
}
