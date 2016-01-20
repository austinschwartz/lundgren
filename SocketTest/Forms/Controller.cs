using Lundgren.Controller;
using System.Threading;
using System.Windows.Forms;
using System;
using System.Linq;
using Lundgren.Controller.vJoyFeeder;
using Lundgren.Logs;

namespace SocketTest.Forms
{
    internal class Controller : Form
    {
        private bool exit = false;

        private Button _btnStop;
        private TextBox _logText;
        private Button _btnStart;
        private TextBox _joystickText;
        private TextBox _buttonsText;
        public Driver Driver;

        public Controller()
        {
            Driver = new Driver();
            JoystickHelper.JoystickLog += Joystick_Log;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._btnStart = new System.Windows.Forms.Button();
            this._btnStop = new System.Windows.Forms.Button();
            this._logText = new System.Windows.Forms.TextBox();
            this._joystickText = new System.Windows.Forms.TextBox();
            this._buttonsText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this._btnStart.Location = new System.Drawing.Point(137, 264);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(75, 23);
            this._btnStart.TabIndex = 0;
            this._btnStart.Text = "Start";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this._btnStop.Location = new System.Drawing.Point(137, 293);
            this._btnStop.Name = "_btnStop";
            this._btnStop.Size = new System.Drawing.Size(75, 23);
            this._btnStop.TabIndex = 1;
            this._btnStop.Text = "Stop";
            this._btnStop.UseVisualStyleBackColor = true;
            this._btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // logText
            // 
            this._logText.Location = new System.Drawing.Point(35, 34);
            this._logText.Multiline = true;
            this._logText.Name = "_logText";
            this._logText.Size = new System.Drawing.Size(237, 202);
            this._logText.TabIndex = 2;
            // 
            // joystickText
            // 
            this._joystickText.Location = new System.Drawing.Point(278, 34);
            this._joystickText.Multiline = true;
            this._joystickText.Name = "_joystickText";
            this._joystickText.Size = new System.Drawing.Size(531, 112);
            this._joystickText.TabIndex = 3;
            // 
            // buttonsText
            // 
            this._buttonsText.Location = new System.Drawing.Point(278, 152);
            this._buttonsText.Multiline = true;
            this._buttonsText.Name = "_buttonsText";
            this._buttonsText.Size = new System.Drawing.Size(531, 202);
            this._buttonsText.TabIndex = 4;
            // 
            // Controller
            // 
            this.ClientSize = new System.Drawing.Size(821, 366);
            this.Controls.Add(this._buttonsText);
            this.Controls.Add(this._joystickText);
            this.Controls.Add(this._logText);
            this.Controls.Add(this._btnStop);
            this.Controls.Add(this._btnStart);
            this.Name = "Controller";
            this.Text = "Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                this.Show();
            }
        }

        private void Driver_Log(object sender, Logging.LogEventArgs e)
        {
            if (!exit)
            {
                if (InvokeRequired)
                {
                    EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Driver_Log);
                    Invoke(hnd, new object[] { sender, e });
                    return;
                }
                _logText.AppendText(e.Text + Environment.NewLine);
                _logText.SelectionStart = _logText.TextLength;
                _logText.ScrollToCaret();
            }
        }
        private void Input_Log(object sender, Logging.LogEventArgs e)
        {
            if (!exit)
            {
                if (InvokeRequired)
                {
                    EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Input_Log);
                    Invoke(hnd, new object[] { sender, e });
                    return;
                }
                _buttonsText.AppendText(e.Text + Environment.NewLine);
                var lines = (from item in _buttonsText.Text.Split('\n') select item.Trim());
                int count = 24;
                lines = lines.Skip(Math.Max(0, lines.Count() - count));
                _buttonsText.Text = string.Join(Environment.NewLine, lines.ToArray());
                _buttonsText.SelectionStart = _logText.TextLength;
                _buttonsText.ScrollToCaret();
            }
        }
        private void Joystick_Log(object sender, Logging.LogEventArgs e)
        {
            if (!exit)
            {
                if (InvokeRequired)
                {
                    EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Joystick_Log);
                    Invoke(hnd, new object[] { sender, e });
                    return;
                }
                _joystickText.AppendText(e.Text + Environment.NewLine);
                var lines = (from item in _joystickText.Text.Split('\n') select item.Trim());
                int count = 10;
                lines = lines.Skip(Math.Max(0, lines.Count() - count));
                _joystickText.Text = string.Join(Environment.NewLine, lines.ToArray());
                _joystickText.SelectionStart = _joystickText.TextLength;
                _joystickText.ScrollToCaret();
            }
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (!Driver.run)
            {
                Driver.run = true;
                var threadDelegate = new ThreadStart(Driver.Start);
                Thread t = new Thread(threadDelegate);
                Driver_Log(null, new Logging.LogEventArgs("Starting Driver."));
                t.Start();
                //trayIcon.Text = "GCN USB Adapter - Started";
            }
            else
            {
                Driver_Log(null, new Logging.LogEventArgs("Driver is already started."));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Driver.run)
            {
                Driver.run = false;
                Driver_Log(null, new Logging.LogEventArgs("Stopping Driver."));
            }
            else
            {
                Driver_Log(null, new Logging.LogEventArgs("Driver is not running."));
            }
        }

        private void logText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}