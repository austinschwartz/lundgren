using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Lundgren.AI;
using Lundgren.Controller;
using Lundgren.Controller.vJoyFeeder;
using Lundgren.Game;
using Lundgren.Game.Helpers;
using Lundgren.Logs;
using static Lundgren.Controller.MoveQueue;

namespace Lundgren.Forms
{
    public partial class MainForm : Form
    {
        private readonly System.Reflection.Assembly _assembly
            = System.Reflection.Assembly.GetExecutingAssembly();

        private readonly System.Timers.Timer        _gameTimer = new System.Timers.Timer();
        private readonly System.Windows.Forms.Timer _formTimer = new System.Windows.Forms.Timer();

        public MainForm()
        {
            // Refactor this later

            InitializeComponent();

            _gameTimer.Elapsed += new ElapsedEventHandler(MoveTimer);
            _gameTimer.Interval = 1;
            _gameTimer.Enabled = true;

            _formTimer.Tick += new EventHandler(FormTimer);
            _formTimer.Interval = 1;
            _formTimer.Enabled = true;

            Driver = new Driver();

            Driver.DriverLog += Log;
            Driver.InputLog += Log;
            JoystickHelper.JoystickLog += Log;

            Bot.Instance._log = Log;
        }
        
        public Driver Driver { get; }


        void MoveTimer(Object sender, EventArgs e)
        {
            Bot.Instance.ProcessMoves();
        }

        private void FormTimer(Object sender, EventArgs e) 
        {
            GameState.GetState();
            UpdateTextboxes();
        }

        private void UpdateTextboxes()
        {
            frame.Text = GameState.LastFrame.ToString();
            stage.Text = GameState.StageString;
            p1NameEntryPos.Text = GameState.P1NameEntryPos;

            if (GameState.p1 != null)
            {
                p1percent.Text = GameState.P1Percent;
                p1Animation.Text = GameState.P1Action;
                p1char.Text = GameState.P1Char;
                p1stocks.Text = GameState.P1Stocks;
                p1X.Text = GameState.P1X;
                p1Y.Text = GameState.P1Y;
                p1CursorX.Text = GameState.P1CursorX;
                p1CursorY.Text = GameState.P1CursorY;
                if (GameState.p1.HasCharacterSelected)
                {
                    String str = GameState.p1.CharacterImage;
                    Stream myStream = null;
                    if (str != "None")
                        myStream = _assembly.GetManifestResourceStream(str);
                    else
                        p1PictureBox.Image = null;
                    if (myStream != null)
                        p1PictureBox.Image = new Bitmap(myStream);
                }
            }
            if (GameState.p2 != null)
            {
                p2percent.Text = GameState.P2Percent;
                p2Animation.Text = GameState.P2Action;
                p2char.Text = GameState.P2Char;
                p2stocks.Text = GameState.P2Stocks;
                p2X.Text = GameState.P2X;
                p2Y.Text = GameState.P2Y;
                p2CursorX.Text = GameState.P2CursorX;
                p2CursorY.Text = GameState.P2CursorY;
                if (GameState.p2.HasCharacterSelected)
                {
                    String str = GameState.p2.CharacterImage;
                    Stream myStream = null;
                    if (str != "None")
                        myStream = _assembly.GetManifestResourceStream(str);
                    else
                        p2PictureBox.Image = null;
                    if (myStream != null)
                        p2PictureBox.Image = new Bitmap(myStream);
                }
            }

            timer.Text = GameState.TimerString;
        }


        public void Log(object sender, Logging.LogEventArgs e)
        {
            if (InvokeRequired)
            {
                var hnd = new EventHandler<Logging.LogEventArgs>(Log);
                Invoke(hnd, new object[] { sender, e });
                return;
            }
            logText.AppendText(e.Text + Environment.NewLine);
            logText.SelectionStart = logText.TextLength;
            logText.ScrollToCaret();
        }


        private void beginButton_Click(object sender, EventArgs e)
        {
            var threadDelegate = new ThreadStart(Moves.AttemptToPickFox20XX);
            var t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to pick fox."));
            t.Start();
        }

        private void waveshineBtn_Click(object sender, EventArgs e)
        {
            var threadDelegate = new ThreadStart(Moves.MoveWaveshine);
            var t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to waveshine."));
            t.Start();
        }


        private void multishineBtn_Click(object sender, EventArgs e)
        {
            /*
            var threadDelegate = new ThreadStart(Moves.MoveMultiShine);
            var t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to multishine."));
            t.Start();
            */
            Moves.MoveMultiShine();
        }


        private void lolBtn_Click(object sender, EventArgs e)
        {
            Moves.MoveLol();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            Moves.MoveTowards();
        }

        /* Side buttons */

        private void btnA_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new DigitalPress(DigitalButton.A));
            Log(null, new Logging.LogEventArgs("Adding A to frame " + (GameState.LastFrame + 15)));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new DigitalPress(DigitalButton.Start));
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new DigitalPress(DigitalButton.B));
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 150, new DigitalPress(DigitalButton.Y));
            Log(null, new Logging.LogEventArgs("Adding A to frame " + (GameState.LastFrame + 150)));
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new DigitalPress(DigitalButton.X));
        }

        private void btnSRight_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new StickPress(Direction.E));
        }

        private void btnSUp_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new StickPress(Direction.N));
        }

        private void btnSDown_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new StickPress(Direction.S));
        }

        private void btnSLeft_Click(object sender, EventArgs e)
        {
            MQueue.AddToFrame(GameState.LastFrame + 15, new StickPress(Direction.W));
        }

        private void btnThing_Click(object sender, EventArgs e)
        {
            GameState.P1Data();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!Driver.run)
            {
                Driver.run = true;
                var threadDelegate = new ThreadStart(Driver.Start);
                var t = new Thread(threadDelegate);
                Log(null, new Logging.LogEventArgs("Starting driver."));
                t.Start();
            }
            else
            {
                Log(null, new Logging.LogEventArgs("Driver is already started."));
            }
        }

        private void btnDoubleLaser_Click(object sender, EventArgs e)
        {
            Moves.DoubleLaser();
        }

        private void btnFirefox_Click(object sender, EventArgs e)
        {
            Moves.Firefox();
        }

        private void checkNotHosting_CheckedChanged(object sender, EventArgs e)
        {
            GameState.SwapPlayers();
        }

        private void btnUpsmash_Click(object sender, EventArgs e)
        {
            Moves.RunUpUpsmash();
        }
    }
}
