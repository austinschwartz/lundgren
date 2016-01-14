using Lundgren;
using Lundgren.Controller;
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
    public partial class Client : Form
    {
        public ControllerState State;
        public Client()
        {
            State = new ControllerState(0);
            // fix this later
            SocketClient.ClientLog += Client_Log;
            InitializeComponent();
            updateBoxes();
            updateStateTextBox();
        }

        private void updateBoxes()
        {
            this.cStickX.Text = State.CX.ToString();
            this.cStickY.Text = State.CY.ToString();
            this.stickX.Text  = State.StickX.ToString();
            this.stickY.Text  = State.StickY.ToString();
            this.lAnalog.Text = State.LAnalog.ToString();
            this.rAnalog.Text = State.RAnalog.ToString();
        }
        private void updateStateTextBox()
        {
            serializedOutput.Text = State.ToString();
        }

        private void bButton_CheckedChanged(object sender, EventArgs e)
        {
            this.State.B = !this.State.B;
            updateStateTextBox();
        }

        private void AButton_CheckedChanged(object sender, EventArgs e)
        {
            this.State.A = !this.State.A;
            updateStateTextBox();
        }
        private void yButton_CheckedChanged(object sender, EventArgs e)
        {
            this.State.Y = !this.State.Y;
            updateStateTextBox();
        }
        private void xButton_CheckedChanged(object sender, EventArgs e)
        {
            this.State.X = !this.State.X;
            updateStateTextBox();
        }

        private void cStickX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.CX = (byte)(Int32.Parse(cStickX.Text) % 256);
            }
            catch (Exception)
            {
                // ADD LOGS HERE LATER
                State.CX = 0;
            }
            updateStateTextBox();
        }
        private void cStickY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.CY = (byte)(Int32.Parse(cStickY.Text) % 256);
            }
            catch (Exception)
            {
                State.CY = 0;
            }
            updateStateTextBox();
        }
        private void stickX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.StickX = (byte)(Int32.Parse(stickX.Text) % 256);
            }
            catch (Exception)
            {
                State.StickX = 0;
            }
            updateStateTextBox();
        }
        private void stickY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.StickY = (byte)(Int32.Parse(stickY.Text) % 256);
            }
            catch (Exception)
            {
                State.StickY = 0;
            }
            updateStateTextBox();
        }

        private void dRight_CheckedChanged(object sender, EventArgs e)
        {
            State.DRight = !State.DRight;
            updateStateTextBox();
        }

        private void dUp_CheckedChanged(object sender, EventArgs e)
        {
            State.DUp = !State.DUp;
            updateStateTextBox();
        }

        private void dDown_CheckedChanged(object sender, EventArgs e)
        {
            State.DDown = !State.DDown;
            updateStateTextBox();
        }

        private void dLeft_CheckedChanged(object sender, EventArgs e)
        {
            State.DLeft = !State.DLeft;
            updateStateTextBox();
        }

        private void startButton_CheckedChanged(object sender, EventArgs e)
        {
            State.Start = !State.Start;
            updateStateTextBox();
        }

        private void lButton_CheckedChanged(object sender, EventArgs e)
        {
            State.LDigital = !State.LDigital;
            updateStateTextBox();
        }

        private void rButton_CheckedChanged(object sender, EventArgs e)
        {
            State.RDigital = !State.RDigital;
            updateStateTextBox();
        }

        private void zButton_CheckedChanged(object sender, EventArgs e)
        {
            State.Z = !State.Z;
            updateStateTextBox();
        }

        private void lAnalog_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.LAnalog = (byte)(Int32.Parse(lAnalog.Text) % 256);
            }
            catch (Exception)
            {
                //Add logs
                State.LAnalog = 0;
            }
            updateStateTextBox();
        }

        private void rAnalog_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.RAnalog = (byte)(Int32.Parse(rAnalog.Text) % 256);
            }
            catch (Exception)
            {
                State.RAnalog = 0;
            }
            updateStateTextBox();
        }

        private void enableButton_CheckedChanged(object sender, EventArgs e)
        {
            State.Enabled = !State.Enabled;
            updateStateTextBox();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (!SocketClient.running)
            {
                var threadDelegate = new ThreadStart(SocketClient.StartClient);
                Thread t = new Thread(threadDelegate);
                Client_Log(null, new Logging.LogEventArgs("Attempting Connection..."));
                t.Start();
            }
            else
            {
                Client_Log(null, new Logging.LogEventArgs("Client is already connected."));
            }
        }

        private void dcButton_Click(object sender, EventArgs e)
        {

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            SocketClient.SendReceive(SocketClient.socket, "HELLO<EOF>");
        }



        private void Client_Log(object sender, Logging.LogEventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Client_Log);
                Invoke(hnd, new object[] { sender, e });
                return;
            }
            logText.AppendText(e.Text + System.Environment.NewLine);
            var lines = (from item in logText.Text.Split('\n') select item.Trim());
            int count = 24;
            lines = lines.Skip(Math.Max(0, lines.Count() - count));
            logText.Text = string.Join(System.Environment.NewLine, lines.ToArray());
            logText.SelectionStart = logText.TextLength;
            logText.ScrollToCaret();
        }

        private void serializedOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
