using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Lundgren.Controller;
using Lundgren.Game;
using Lundgren.Game.Helpers;
using Lundgren.Logs;
using Timer = System.Timers.Timer;

namespace Lundgren
{
    public partial class Lundgren : Form
    {

        private readonly Timer _timer = new Timer();
        public static Memory Mem = null;
        public static int lastFrameNum = 0;
        private MoveQueue queue = new MoveQueue();
        public ControllerState state, prev;

        public Lundgren()
        {
            queue = new MoveQueue();
            prev = new ControllerState(-1);
            InitializeComponent();

            _timer.Interval = 1;
            _timer.Elapsed += new ElapsedEventHandler(TimerTick);
            _timer.Enabled = true;

            _driver = new Driver(this);
            Driver.DriverLog += Log;
            Driver.InputLog += Log;
            JoystickHelper.JoystickLog += Log;
        }
        
        public Driver Driver
        {
            get { return _driver; }
        }

        private bool processMoves()
        {
            int thisFrameNum = GameState.GetFrame();
            if (thisFrameNum == lastFrameNum)
                return false;

            //Debug.WriteLine("ON FRAME " + thisFrameNum);
            if (thisFrameNum != lastFrameNum + 1)
                Debug.WriteLine("Lost frames between " + lastFrameNum + " and " + thisFrameNum);

            lastFrameNum = thisFrameNum;
            prev = state;
            if (queue.HasFrame(thisFrameNum))
            {
                Debug.WriteLine("PERFORMING ON " + thisFrameNum);
                state = queue.Get(thisFrameNum);
                bool rem = queue.Remove(thisFrameNum);
                LogFrameState(thisFrameNum, state, rem);
            }
            else
            {
                state = new ControllerState(lastFrameNum);
            }
            return true;
        }

        private void LogFrameState(int frameNum, ControllerState controllerState, bool rem)
        {
            Log(null, new Logging.LogEventArgs("F: " + (rem ? frameNum.ToString() : "BROKE") + " - " + controllerState.ToString()));
        }
        
        void TimerTick(Object sender, EventArgs e)
        {
            // only pass this point if its a new frame
            if (processMoves() == false)
                return;
            /*
            frame.Text = lastFrameNum.ToString();

            p1percent.Text = $"{GameState.p1Percent.ToString()}%";
            p2percent.Text = $"{GameState.p2Percent.ToString()}%";
            p1stocks.Text = GameState.p1Stocks.ToString();
            p2stocks.Text = GameState.p2Stocks.ToString();

            stage.Text = GameData.stages[GameState.stageNum];
            p1char.Text = GameData.chars[GameState.p1CharNum];
            p2char.Text = GameData.chars[GameState.p2CharNum];

            timer.Text = GameState.timerString;
            */
        }

        public String MoveToString(HashSet<ButtonPress> set) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (ButtonPress bp in set)
            {
                sb.Append(bp.ToString() + " ");
            }
            return sb.ToString();
        }

        private void Log(object sender, Logging.LogEventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler<Logging.LogEventArgs> hnd = new EventHandler<Logging.LogEventArgs>(Log);
                Invoke(hnd, new object[] { sender, e });
                return;
            }
            logText.AppendText(e.Text + Environment.NewLine);
            logText.SelectionStart = logText.TextLength;
            logText.ScrollToCaret();
        }


        private void beginButton_Click(object sender, EventArgs e)
        {
            var threadDelegate = new ThreadStart(attemptToPickFox20xx);
            Thread t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to pick fox."));
            t.Start();
        }

        private void attemptToPickFox()
        {
            int currentFrame = lastFrameNum + 5;
            for (int i = 0; i < 8; i++)
            {
                currentFrame++;
                queue.AddToFrame(currentFrame, new StickPress(Direction.NE));
                currentFrame++;
                queue.AddToFrame(currentFrame, new StickPress(Direction.N));
            }
            queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.A));
            for (int i = 0; i < 8; i++)
            {
                currentFrame++;
                queue.AddToFrame(currentFrame, new StickPress(Direction.SE));
                currentFrame++;
                queue.AddToFrame(currentFrame, new StickPress(Direction.S));
            }
            queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.A));
            queue.AddToFrame(currentFrame + 15, new DigitalPress(DigitalButton.A));
            queue.AddToFrame(currentFrame + 30, new DigitalPress(DigitalButton.START));
        }

        private void attemptToPickAndName20xx()
        {
            attemptToPickFox20xx();
            attemptToName20xx();
        }

        private void attemptToName20xx()
        {
            int currentFrame = lastFrameNum + 1;
            for (int i = 0; i < 20; i++)
            {
                currentFrame++;
                queue.AddToFrame(currentFrame, new StickPress(Direction.S));
            }
            Thread.Sleep(100);
            queue.Clear();
            queue.AddToFrame(lastFrameNum + 5, new DigitalPress(DigitalButton.A));

        }

        private void attemptToPickFox20xx()
        {
            int currentFrame = lastFrameNum + 5;
            for (int i = 0; i < 6; i++)
            {
                currentFrame++;
                queue.AddToFrame(currentFrame + i, new StickPress(Direction.N));
            }
            Thread.Sleep(100);

            byte prev = 0;
            byte p1 = GameState.mem.ReadByte(0x8042208F);
            while (p1 != 2)
            {
                prev = p1;
                queue.AddToFrame(lastFrameNum + 1, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(lastFrameNum + 2, new DigitalPress(DigitalButton.A));
                Thread.Sleep(50);
                p1 = GameState.mem.ReadByte(0x8042208F);
                if (p1 == 2)
                {
                    Log(null, new Logging.LogEventArgs("Fox selected!"));
                    queue.Clear();
                    return;
                }
            }
        }


        private void SleepUntilFrame(int frame)
        {
            int count = 0;
            Debug.WriteLine("Beginning to wait...");
            while (lastFrameNum != frame)
            {
                Debug.WriteLine("wait..." + lastFrameNum + " " + frame);
                count++;
            }
            Debug.WriteLine("Done waiting... waited " + count);
        }

        private void waveshineBtn_Click(object sender, EventArgs e)
        {

            var threadDelegate = new ThreadStart(moveWaveshine);
            Thread t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to waveshine."));
            t.Start();
        }

        private void moveWaveshine()
        {
            int currentFrame = lastFrameNum + 15;

            for (int i = 0; i < 10; i++) {
                queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.Y));

                queue.AddToFrame(currentFrame + 10, new StickPress(Direction.SE));
                queue.AddToFrame(currentFrame + 10, new ShoulderPress(150));
                currentFrame += 15;

                queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.Y));

                queue.AddToFrame(currentFrame + 10, new StickPress(Direction.SW));
                queue.AddToFrame(currentFrame + 10, new ShoulderPress(150));
                currentFrame += 15;
            }
        }


        private void multishineBtn_Click(object sender, EventArgs e)
        {
            var threadDelegate = new ThreadStart(moveMultiShine);
            Thread t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to multishine."));
            t.Start();
        }


        private void moveMultiShine()
        {
            int currentFrame = lastFrameNum + 10;

            for (int i = 0; i < 10; i++)
            {
                 
                queue.AddToFrame(currentFrame + 1, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 4, new DigitalPress(DigitalButton.Y));

                queue.AddToFrame(currentFrame + 7, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 7, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 12, new DigitalPress(DigitalButton.Y));

                queue.AddToFrame(currentFrame + 15, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 15, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 20, new DigitalPress(DigitalButton.Y));
            
                queue.AddToFrame(currentFrame + 23, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 28, new DigitalPress(DigitalButton.Y));
            
                queue.AddToFrame(currentFrame + 31, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 36, new DigitalPress(DigitalButton.Y));

                currentFrame += 40;
            }

            Debug.WriteLine(queue.ToString());
            /*
            queue.AddToFrame(((currentFrame + 39) % 60), new StickPress(Direction.S));
            queue.AddToFrame(((currentFrame + 39) % 60), new DigitalPress(DigitalButton.B));

            queue.AddToFrame(((currentFrame + 44) % 60), new DigitalPress(DigitalButton.Y));

            queue.AddToFrame(((currentFrame + 47) % 60), new StickPress(Direction.S));
            queue.AddToFrame(((currentFrame + 47) % 60), new DigitalPress(DigitalButton.B));

            queue.AddToFrame(((currentFrame + 52) % 60), new DigitalPress(DigitalButton.Y));

            queue.AddToFrame(((currentFrame + 55) % 60), new StickPress(Direction.S));
            queue.AddToFrame(((currentFrame + 55) % 60), new DigitalPress(DigitalButton.B));
            */


        }

        private void lolBtn_Click(object sender, EventArgs e)
        {
            var threadDelegate = new ThreadStart(moveLol);
            Thread t = new Thread(threadDelegate);
            Log(null, new Logging.LogEventArgs("Attempting to multishine."));
            t.Start();
        }


        private void moveLol()
        {
            int currentFrame = lastFrameNum + 15;

            var dir1 = Direction.E;
            var dir2 = Direction.W;
            

            for (int i = 0; i < 20; i++)
            {
                queue.AddToFrame(currentFrame + 1, new StickPress(dir1));
                queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 4, new DigitalPress(DigitalButton.Y));

                queue.AddToFrame(currentFrame + 7, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 13, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 14, new DigitalPress(DigitalButton.B));
                currentFrame += 35;
                var temp = dir1;
                dir1 = dir2;
                dir2 = temp;
            }


        }
        /*private void moveLol()
        {
            int currentFrame = lastFrameNum + 15;

            for (int i = 0; i < 10; i++)
            {
                queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                queue.AddToFrame(currentFrame + 4, new DigitalPress(DigitalButton.Y));

                queue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 7, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 9, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 10, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 11, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 12, new DigitalPress(DigitalButton.B));
                queue.AddToFrame(currentFrame + 13, new DigitalPress(DigitalButton.B));


                currentFrame += 35;
            }


        }*/

        private readonly Driver _driver;

        private void btnA_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new DigitalPress(DigitalButton.A));
            Log(null, new Logging.LogEventArgs("Adding A to frame " + (lastFrameNum + 15)));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new DigitalPress(DigitalButton.START));
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new DigitalPress(DigitalButton.B));
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 150, new DigitalPress(DigitalButton.Y));
            Log(null, new Logging.LogEventArgs("Adding A to frame " + (lastFrameNum + 150)));
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new DigitalPress(DigitalButton.X));
        }

        private void btnSRight_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new StickPress(Direction.E));
        }

        private void btnSUp_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new StickPress(Direction.N));
        }

        private void btnSDown_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new StickPress(Direction.S));
        }

        private void btnSLeft_Click(object sender, EventArgs e)
        {
            queue.AddToFrame(lastFrameNum + 15, new StickPress(Direction.W));
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            if (!Driver.run)
            {
                Driver.run = true;
                var threadDelegate = new ThreadStart(Driver.Start);
                Thread t = new Thread(threadDelegate);
                Log(null, new Logging.LogEventArgs("Starting output."));
                t.Start();
            }
            else
            {
                Log(null, new Logging.LogEventArgs("Output is already started."));
            }
        }

    }
}
