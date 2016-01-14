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
        public ControllerState state;
        public Client()
        {
            state = new ControllerState(0);
            // fix this later
            SocketClient.ClientLog += Client_Log;
            InitializeComponent();
            updateBoxes();
            updateStateTextBox();
        }

        private void updateBoxes()
        {
            this.cStickX.Text = state.C_x.ToString();
            this.cStickY.Text = state.C_y.ToString();
            this.stickX.Text  = state.Stick_x.ToString();
            this.stickY.Text  = state.Stick_y.ToString();
            this.lAnalog.Text = state.L_Analog.ToString();
            this.rAnalog.Text = state.R_Analog.ToString();
        }
        private void updateStateTextBox()
        {
            serializedOutput.Text = state.ToString();
        }

        private void bButton_CheckedChanged(object sender, EventArgs e)
        {
            this.state.B = !this.state.B;
            updateStateTextBox();
        }

        private void AButton_CheckedChanged(object sender, EventArgs e)
        {
            this.state.A = !this.state.A;
            updateStateTextBox();
        }
        private void yButton_CheckedChanged(object sender, EventArgs e)
        {
            this.state.Y = !this.state.Y;
            updateStateTextBox();
        }
        private void xButton_CheckedChanged(object sender, EventArgs e)
        {
            this.state.X = !this.state.X;
            updateStateTextBox();
        }

        private void cStickX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                state.C_x = (byte)(Int32.Parse(cStickX.Text) % 256);
            }
            catch (Exception)
            {
                // ADD LOGS HERE LATER
                state.C_x = 0;
            }
            updateStateTextBox();
        }
        private void cStickY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                state.C_y = (byte)(Int32.Parse(cStickY.Text) % 256);
            }
            catch (Exception)
            {
                state.C_y = 0;
            }
            updateStateTextBox();
        }
        private void stickX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                state.Stick_x = (byte)(Int32.Parse(stickX.Text) % 256);
            }
            catch (Exception)
            {
                state.Stick_x = 0;
            }
            updateStateTextBox();
        }
        private void stickY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                state.Stick_y = (byte)(Int32.Parse(stickY.Text) % 256);
            }
            catch (Exception)
            {
                state.Stick_y = 0;
            }
            updateStateTextBox();
        }

        private void dRight_CheckedChanged(object sender, EventArgs e)
        {
            state.D_Right = !state.D_Right;
            updateStateTextBox();
        }

        private void dUp_CheckedChanged(object sender, EventArgs e)
        {
            state.D_Up = !state.D_Up;
            updateStateTextBox();
        }

        private void dDown_CheckedChanged(object sender, EventArgs e)
        {
            state.D_Down = !state.D_Down;
            updateStateTextBox();
        }

        private void dLeft_CheckedChanged(object sender, EventArgs e)
        {
            state.D_Left = !state.D_Left;
            updateStateTextBox();
        }

        private void startButton_CheckedChanged(object sender, EventArgs e)
        {
            state.Start = !state.Start;
            updateStateTextBox();
        }

        private void lButton_CheckedChanged(object sender, EventArgs e)
        {
            state.L_Digital = !state.L_Digital;
            updateStateTextBox();
        }

        private void rButton_CheckedChanged(object sender, EventArgs e)
        {
            state.R_Digital = !state.R_Digital;
            updateStateTextBox();
        }

        private void zButton_CheckedChanged(object sender, EventArgs e)
        {
            state.Z = !state.Z;
            updateStateTextBox();
        }

        private void lAnalog_TextChanged(object sender, EventArgs e)
        {
            try
            {
                state.L_Analog = (byte)(Int32.Parse(lAnalog.Text) % 256);
            }
            catch (Exception)
            {
                //Add logs
                state.L_Analog = 0;
            }
            updateStateTextBox();
        }

        private void rAnalog_TextChanged(object sender, EventArgs e)
        {
            try
            {
                state.R_Analog = (byte)(Int32.Parse(rAnalog.Text) % 256);
            }
            catch (Exception)
            {
                state.R_Analog = 0;
            }
            updateStateTextBox();
        }

        private void enableButton_CheckedChanged(object sender, EventArgs e)
        {
            state.Enabled = !state.Enabled;
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
