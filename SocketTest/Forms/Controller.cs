using Lundgren.Controller;
using System.Threading;
using System.Windows.Forms;
using System;
using System.Linq;
using Lundgren.Logs;

namespace SocketTest.Forms
{
    internal class Controller : Form
    {
        private bool exit = false;

        private Button btnStop;
        private TextBox logText;
        private Button btnStart;
        private TextBox joystickText;
        private TextBox buttonsText;
        public Driver driver;

        public Controller()
        {
            driver = new Driver();
            Driver.DriverLog += Driver_Log;
            Driver.InputLog += Input_Log;
            JoystickHelper.JoystickLog += Joystick_Log;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.logText = new System.Windows.Forms.TextBox();
            this.joystickText = new System.Windows.Forms.TextBox();
            this.buttonsText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(137, 264);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(137, 293);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // logText
            // 
            this.logText.Location = new System.Drawing.Point(35, 34);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.Size = new System.Drawing.Size(237, 202);
            this.logText.TabIndex = 2;
            // 
            // joystickText
            // 
            this.joystickText.Location = new System.Drawing.Point(278, 34);
            this.joystickText.Multiline = true;
            this.joystickText.Name = "joystickText";
            this.joystickText.Size = new System.Drawing.Size(531, 112);
            this.joystickText.TabIndex = 3;
            // 
            // buttonsText
            // 
            this.buttonsText.Location = new System.Drawing.Point(278, 152);
            this.buttonsText.Multiline = true;
            this.buttonsText.Name = "buttonsText";
            this.buttonsText.Size = new System.Drawing.Size(531, 202);
            this.buttonsText.TabIndex = 4;
            // 
            // Controller
            // 
            this.ClientSize = new System.Drawing.Size(821, 366);
            this.Controls.Add(this.buttonsText);
            this.Controls.Add(this.joystickText);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
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
                //Invoke to talk safely across threads
                if (InvokeRequired)
                {
                    EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Driver_Log);
                    Invoke(hnd, new object[] { sender, e });
                    return;
                }
                logText.AppendText(e.Text + Environment.NewLine);
                logText.SelectionStart = logText.TextLength;
                logText.ScrollToCaret();
            }
        }
        private void Input_Log(object sender, Logging.LogEventArgs e)
        {
            if (!exit)
            {
                //Invoke to talk safely across threads
                if (InvokeRequired)
                {
                    EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Input_Log);
                    Invoke(hnd, new object[] { sender, e });
                    return;
                }
                buttonsText.AppendText(e.Text + Environment.NewLine);
                var lines = (from item in buttonsText.Text.Split('\n') select item.Trim());
                int count = 24;
                lines = lines.Skip(Math.Max(0, lines.Count() - count));
                buttonsText.Text = string.Join(Environment.NewLine, lines.ToArray());
                buttonsText.SelectionStart = logText.TextLength;
                buttonsText.ScrollToCaret();
            }
        }
        private void Joystick_Log(object sender, Logging.LogEventArgs e)
        {
            if (!exit)
            {
                //Invoke to talk safely across threads
                if (InvokeRequired)
                {
                    EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Joystick_Log);
                    Invoke(hnd, new object[] { sender, e });
                    return;
                }
                joystickText.AppendText(e.Text + Environment.NewLine);
                var lines = (from item in joystickText.Text.Split('\n') select item.Trim());
                int count = 10;
                lines = lines.Skip(Math.Max(0, lines.Count() - count));
                joystickText.Text = string.Join(Environment.NewLine, lines.ToArray());
                joystickText.SelectionStart = joystickText.TextLength;
                joystickText.ScrollToCaret();
            }
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (!Driver.run)
            {
                Driver.run = true;
                var threadDelegate = new ThreadStart(driver.Start);
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
                //trayIcon.Text = "GCN USB Adapter - Stopped";
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