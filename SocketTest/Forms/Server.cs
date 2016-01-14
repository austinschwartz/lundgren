using Lundgren.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTest.Forms
{
    public partial class Server : Form
    {
        public Server()
        {
            SocketServer.ServerLog += Server_Log;
            InitializeComponent();
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (!SocketServer.running)
            {
                var threadDelegate = new ThreadStart(SocketServer.StartListening);
                Thread t = new Thread(threadDelegate);
                Server_Log(null, new Logging.LogEventArgs("Starting Server."));
                t.Start();
            }
            else
            {
                Server_Log(null, new Logging.LogEventArgs("Server is already started."));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (SocketServer.running)
            {
                Server_Log(null, new Logging.LogEventArgs("Stopping Driver."));
                SocketServer.StopListening();
                SocketServer.running = false;
                //trayIcon.Text = "GCN USB Adapter - Stopped";
            }
            else
            {
                Server_Log(null, new Logging.LogEventArgs("Driver is not running."));
            }
        }
        private void Server_Log(object sender, Logging.LogEventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Server_Log);
                Invoke(hnd, new object[] { sender, e });
                return;
            }
            logText.AppendText(e.Text + Environment.NewLine);
            logText.SelectionStart = logText.TextLength;
            logText.ScrollToCaret();
        }
    }
}
