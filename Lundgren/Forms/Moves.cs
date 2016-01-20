using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Lundgren.AI;
using Lundgren.Controller;
using Lundgren.Game;
using Lundgren.Game.Helpers;
using Lundgren.Logs;

namespace Lundgren.Forms
{
    public class Moves
    {
        private IBot CurrentAI;

        public Moves(IBot _currentAI)
        {
            CurrentAI = _currentAI;
        }

        private void AttemptToPickFox()
        {
            var currentFrame = GameState.LastFrame + 5;
            for (var i = 0; i < 8; i++)
            {
                currentFrame++;
                CurrentAI.Queue.AddToFrame(currentFrame, new StickPress(Direction.NE));
                currentFrame++;
                CurrentAI.Queue.AddToFrame(currentFrame, new StickPress(Direction.N));
            }
            CurrentAI.Queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.A));
            for (var i = 0; i < 8; i++)
            {
                currentFrame++;
                CurrentAI.Queue.AddToFrame(currentFrame, new StickPress(Direction.SE));
                currentFrame++;
                CurrentAI.Queue.AddToFrame(currentFrame, new StickPress(Direction.S));
            }
            CurrentAI.Queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.A));
            CurrentAI.Queue.AddToFrame(currentFrame + 15, new DigitalPress(DigitalButton.A));
            CurrentAI.Queue.AddToFrame(currentFrame + 30, new DigitalPress(DigitalButton.Start));
        }

        private void AttemptToPickAndName20XX()
        {
            AttemptToPickFox20XX();
            AttemptToName20XX();
        }

        private void AttemptToName20XX()
        {
            var currentFrame = GameState.LastFrame + 1;
            for (var i = 0; i < 20; i++)
            {
                currentFrame++;
                CurrentAI.Queue.AddToFrame(currentFrame, new StickPress(Direction.S));
            }
            Thread.Sleep(100);
            CurrentAI.Queue.Clear();
            CurrentAI.Queue.AddToFrame(GameState.LastFrame + 5, new DigitalPress(DigitalButton.A));

        }

        public void AttemptToPickFox20XX()
        {
            var currentFrame = GameState.LastFrame + 5;
            for (var i = 0; i < 6; i++)
            {
                currentFrame++;
                CurrentAI.Queue.AddToFrame(currentFrame + i, new StickPress(Direction.N));
            }
            Thread.Sleep(100);

            var prev = 0;
            var p1 = Memory.ReadByte(0x8042208F);
            while (p1 != 2)
            {
                prev = p1;
                CurrentAI.Queue.AddToFrame(GameState.LastFrame + 1, new DigitalPress(DigitalButton.B));
                CurrentAI.Queue.AddToFrame(GameState.LastFrame + 2, new DigitalPress(DigitalButton.A));
                Thread.Sleep(75);
                p1 = Memory.ReadByte(0x8042208F);
                if (p1 != 2) continue;
                //_lundgrenForm.Log(null, new Logging.LogEventArgs("Fox selected!"));
                CurrentAI.Queue.Clear();
                return;
            }
        }

        public void MoveWaveshine()
        {
            int currentFrame = GameState.LastFrame + 15;

            for (int i = 0; i < 10; i++) {
                CurrentAI.Queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                CurrentAI.Queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                CurrentAI.Queue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.Y));

                CurrentAI.Queue.AddToFrame(currentFrame + 10, new StickPress(Direction.SE));
                CurrentAI.Queue.AddToFrame(currentFrame + 10, new ShoulderPress(150));
                currentFrame += 55;

                CurrentAI.Queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                CurrentAI.Queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                CurrentAI.Queue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.Y));

                CurrentAI.Queue.AddToFrame(currentFrame + 10, new StickPress(Direction.SW));
                CurrentAI.Queue.AddToFrame(currentFrame + 10, new ShoulderPress(150));
                currentFrame += 55;
            }
        }

        public int Multishine(Direction dir)
        {
            var currentFrame = GameState.LastFrame + 2;

            CurrentAI.Queue.AddToFrame(currentFrame + 0, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 1, new StickPress(Direction.S));
            CurrentAI.Queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.B));

            CurrentAI.Queue.AddToFrame(currentFrame + 5, new DigitalPress(DigitalButton.Y));

            CurrentAI.Queue.AddToFrame(currentFrame + 8, new StickPress(Direction.S));
            CurrentAI.Queue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.B));

            CurrentAI.Queue.AddToFrame(currentFrame + 14, new StickPress(Direction.N));
            CurrentAI.Queue.AddToFrame(currentFrame + 15, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 16, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 17, new StickPress(dir));

            CurrentAI.Queue.AddToFrame(currentFrame + 18, new DigitalPress(DigitalButton.A));
            CurrentAI.Queue.AddToFrame(currentFrame + 19, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 20, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 21, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 22, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 23, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 24, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 25, new DigitalPress(DigitalButton.R));
            CurrentAI.Queue.AddToFrame(currentFrame + 26, new DigitalPress(DigitalButton.R));
            CurrentAI.Queue.AddToFrame(currentFrame + 27, new DigitalPress(DigitalButton.R));

            return 35;
        }

        public void MoveMultiShine()
        {
            var currentFrame = GameState.LastFrame + 10;

            var aix = GameState.p1.x;
            var playerx = GameState.p2.x;
            var dir = playerx > aix ? Direction.E : Direction.W;
            CurrentAI.Queue.AddToFrame(currentFrame + 0, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 1, new StickPress(Direction.S));
            CurrentAI.Queue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.B));

            CurrentAI.Queue.AddToFrame(currentFrame + 5, new DigitalPress(DigitalButton.Y));

            CurrentAI.Queue.AddToFrame(currentFrame + 8, new StickPress(Direction.S));
            CurrentAI.Queue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.B));

            CurrentAI.Queue.AddToFrame(currentFrame + 14, new StickPress(Direction.N));
            CurrentAI.Queue.AddToFrame(currentFrame + 15, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 16, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 17, new StickPress(dir));

            CurrentAI.Queue.AddToFrame(currentFrame + 18, new DigitalPress(DigitalButton.A));
            CurrentAI.Queue.AddToFrame(currentFrame + 19, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 20, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 21, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 22, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 23, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 24, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 25, new DigitalPress(DigitalButton.R));
            CurrentAI.Queue.AddToFrame(currentFrame + 26, new DigitalPress(DigitalButton.R));
            CurrentAI.Queue.AddToFrame(currentFrame + 27, new DigitalPress(DigitalButton.R));


            //Debug.WriteLine(CurrentAI.Queue.ToString());
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

        public void MoveLol()
        {

            var currentFrame = GameState.LastFrame + 15;

            var dir1 = Direction.E;
            var dir2 = Direction.W;
            

            for (var i = 0; i < 20; i++)
            {
                CurrentAI.Queue.AddToFrame(currentFrame + 1, new StickPress(dir1));
                CurrentAI.Queue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                CurrentAI.Queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                CurrentAI.Queue.AddToFrame(currentFrame + 4, new DigitalPress(DigitalButton.Y));

                CurrentAI.Queue.AddToFrame(currentFrame + 7, new DigitalPress(DigitalButton.B));
                CurrentAI.Queue.AddToFrame(currentFrame + 13, new DigitalPress(DigitalButton.B));
                CurrentAI.Queue.AddToFrame(currentFrame + 14, new DigitalPress(DigitalButton.B));
                currentFrame += 35;
                var temp = dir1;
                dir1 = dir2;
                dir2 = temp;
            }
        }

        public void DoubleLaser()
        {
            var currentFrame = GameState.LastFrame + 5;

            for (var i = 0; i < 20; i++)
            {
                CurrentAI.Queue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.Y));

                CurrentAI.Queue.AddToFrame(currentFrame + 3, new DigitalPress(DigitalButton.B));
                CurrentAI.Queue.AddToFrame(currentFrame + 9, new DigitalPress(DigitalButton.B));
                CurrentAI.Queue.AddToFrame(currentFrame + 10, new DigitalPress(DigitalButton.B));
                currentFrame += 30;
            }
        }

        public void MoveTowards()
        {
            var currentFrame = GameState.LastFrame + 15;

            double aix = GameState.p1.x;
            double playerx = GameState.p2.x;

            if (playerx > aix)
            {
                CurrentAI.Queue.AddToFrame(currentFrame + 1, new StickPress(Direction.E));
            }
            else
            {
                CurrentAI.Queue.AddToFrame(currentFrame + 1, new StickPress(Direction.W));
            }

        }

        public static void FirefoxOnstage()
        {
            

        }

        public int JumpAndNair()
        {
            var currentFrame = GameState.LastFrame + 2;

            var aix = GameState.p1.x;
            var playerx = GameState.p2.x;
            var dir = playerx > aix ? Direction.E : Direction.W;

            CurrentAI.Queue.AddToFrame(currentFrame + 0, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 4, new StickPress(Direction.N));
            CurrentAI.Queue.AddToFrame(currentFrame + 5, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 6, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 7, new StickPress(dir));

            CurrentAI.Queue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.A));
            CurrentAI.Queue.AddToFrame(currentFrame + 9, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 10, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 11, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 12, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 13, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 14, new StickPress(dir));
            CurrentAI.Queue.AddToFrame(currentFrame + 15, new DigitalPress(DigitalButton.R));
            CurrentAI.Queue.AddToFrame(currentFrame + 16, new DigitalPress(DigitalButton.R));
            CurrentAI.Queue.AddToFrame(currentFrame + 17, new DigitalPress(DigitalButton.R));

            return 15;
        }

        public void Firefox()
        {
            var currentFrame = GameState.LastFrame + 2;
            //CurrentAI.Queue.AddToFrame(currentFrame, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            //CurrentAI.Queue.AddToFrame(currentFrame + 1, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            //CurrentAI.Queue.AddToFrame(currentFrame + 2, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            //CurrentAI.Queue.AddToFrame(currentFrame + 3, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            //CurrentAI.Queue.AddToFrame(currentFrame + 4, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            //CurrentAI.Queue.AddToFrame(currentFrame + 5, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            for (int i = 0; i < 600; i++)
                CurrentAI.Queue.AddToFrame(currentFrame + 5 + i, new StickPress(0, 0, GameState.p1.x, GameState.p1.y));
        }
    }
}