using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Lundgren.AI;
using Lundgren.Controller;
using Lundgren.Game;
using Lundgren.Game.Helpers;
using Lundgren.Logs;
using static Lundgren.Controller.MoveQueue;

namespace Lundgren.Forms
{
    public static class Moves
    {
        private static void AttemptToPickFox()
        {
            var currentFrame = GameState.LastFrame + 5;
            for (var i = 0; i < 8; i++)
            {
                currentFrame++;
                MQueue.AddToFrame(currentFrame, new StickPress(Direction.NE));
                currentFrame++;
                MQueue.AddToFrame(currentFrame, new StickPress(Direction.N));
            }
            MQueue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.A));
            for (var i = 0; i < 8; i++)
            {
                currentFrame++;
                MQueue.AddToFrame(currentFrame, new StickPress(Direction.SE));
                currentFrame++;
                MQueue.AddToFrame(currentFrame, new StickPress(Direction.S));
            }
            MQueue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.A));
            MQueue.AddToFrame(currentFrame + 15, new DigitalPress(DigitalButton.A));
            MQueue.AddToFrame(currentFrame + 30, new DigitalPress(DigitalButton.Start));
        }

        private static void AttemptToPickAndName20XX()
        {
            AttemptToPickFox20XX();
            AttemptToName20XX();
        }

        private static void AttemptToName20XX()
        {
            var currentFrame = GameState.LastFrame + 1;
            for (var i = 0; i < 20; i++)
            {
                currentFrame++;
                MQueue.AddToFrame(currentFrame, new StickPress(Direction.S));
            }
            Thread.Sleep(100);
            MQueue.Clear();
            MQueue.AddToFrame(GameState.LastFrame + 5, new DigitalPress(DigitalButton.A));

        }

        public static void AttemptToPickFox20XX()
        {
            var currentFrame = GameState.LastFrame + 5;
            for (var i = 0; i < 6; i++)
            {
                currentFrame++;
                MQueue.AddToFrame(currentFrame + i, new StickPress(Direction.N));
            }
            Thread.Sleep(100);

            var prev = 0;
            var p1 = Memory.ReadByte(0x8042208F);
            while (p1 != 2)
            {
                prev = p1;
                MQueue.AddToFrame(GameState.LastFrame + 1, new DigitalPress(DigitalButton.B));
                MQueue.AddToFrame(GameState.LastFrame + 2, new DigitalPress(DigitalButton.A));
                Thread.Sleep(75);
                p1 = Memory.ReadByte(0x8042208F);
                if (p1 != 2) continue;
                //Log(null, new Logging.LogEventArgs("Fox selected!"));
                MQueue.Clear();
                return;
            }
        }

        public static void MoveWaveshine()
        {
            int currentFrame = GameState.LastFrame + 15;

            for (int i = 0; i < 10; i++) {
                MQueue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                MQueue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                MQueue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.Y));

                MQueue.AddToFrame(currentFrame + 10, new StickPress(Direction.SE));
                MQueue.AddToFrame(currentFrame + 10, new ShoulderPress(150));
                currentFrame += 55;

                MQueue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                MQueue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                MQueue.AddToFrame(currentFrame + 6, new DigitalPress(DigitalButton.Y));

                MQueue.AddToFrame(currentFrame + 10, new StickPress(Direction.SW));
                MQueue.AddToFrame(currentFrame + 10, new ShoulderPress(150));
                currentFrame += 55;
            }
        }

        public static int Multishine(Direction dir)
        {
            var currentFrame = GameState.LastFrame + 2;

            MQueue.AddToFrame(currentFrame + 0, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 1, new StickPress(Direction.S));
            MQueue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.B));

            MQueue.AddToFrame(currentFrame + 5, new DigitalPress(DigitalButton.Y));

            MQueue.AddToFrame(currentFrame + 8, new StickPress(Direction.S));
            MQueue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.B));

            MQueue.AddToFrame(currentFrame + 14, new StickPress(Direction.N));
            MQueue.AddToFrame(currentFrame + 15, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 16, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 17, new StickPress(dir));

            MQueue.AddToFrame(currentFrame + 18, new DigitalPress(DigitalButton.A));
            MQueue.AddToFrame(currentFrame + 19, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 20, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 21, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 22, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 23, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 24, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 25, new DigitalPress(DigitalButton.R));
            MQueue.AddToFrame(currentFrame + 26, new DigitalPress(DigitalButton.R));
            MQueue.AddToFrame(currentFrame + 27, new DigitalPress(DigitalButton.R));

            return 35;
        }

        public static void MoveMultiShine()
        {
            var currentFrame = GameState.LastFrame + 10;

            var aix = GameState.p1.x;
            var playerx = GameState.p2.x;
            var dir = playerx > aix ? Direction.E : Direction.W;
            MQueue.AddToFrame(currentFrame + 0, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 1, new StickPress(Direction.S));
            MQueue.AddToFrame(currentFrame + 1, new DigitalPress(DigitalButton.B));

            MQueue.AddToFrame(currentFrame + 5, new DigitalPress(DigitalButton.Y));

            MQueue.AddToFrame(currentFrame + 8, new StickPress(Direction.S));
            MQueue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.B));

            MQueue.AddToFrame(currentFrame + 14, new StickPress(Direction.N));
            MQueue.AddToFrame(currentFrame + 15, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 16, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 17, new StickPress(dir));

            MQueue.AddToFrame(currentFrame + 18, new DigitalPress(DigitalButton.A));
            MQueue.AddToFrame(currentFrame + 19, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 20, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 21, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 22, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 23, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 24, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 25, new DigitalPress(DigitalButton.R));
            MQueue.AddToFrame(currentFrame + 26, new DigitalPress(DigitalButton.R));
            MQueue.AddToFrame(currentFrame + 27, new DigitalPress(DigitalButton.R));

        }

        public static void MoveLol()
        {

            var currentFrame = GameState.LastFrame + 15;

            var dir1 = Direction.E;
            var dir2 = Direction.W;
            

            for (var i = 0; i < 20; i++)
            {
                MQueue.AddToFrame(currentFrame + 1, new StickPress(dir1));
                MQueue.AddToFrame(currentFrame + 0, new StickPress(Direction.S));
                MQueue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.B));

                MQueue.AddToFrame(currentFrame + 4, new DigitalPress(DigitalButton.Y));

                MQueue.AddToFrame(currentFrame + 7, new DigitalPress(DigitalButton.B));
                MQueue.AddToFrame(currentFrame + 13, new DigitalPress(DigitalButton.B));
                MQueue.AddToFrame(currentFrame + 14, new DigitalPress(DigitalButton.B));
                currentFrame += 35;
                var temp = dir1;
                dir1 = dir2;
                dir2 = temp;
            }
        }

        public static void DoubleLaser()
        {
            var currentFrame = GameState.LastFrame + 5;

            for (var i = 0; i < 20; i++)
            {
                MQueue.AddToFrame(currentFrame + 0, new DigitalPress(DigitalButton.Y));

                MQueue.AddToFrame(currentFrame + 3, new DigitalPress(DigitalButton.B));
                MQueue.AddToFrame(currentFrame + 9, new DigitalPress(DigitalButton.B));
                MQueue.AddToFrame(currentFrame + 10, new DigitalPress(DigitalButton.B));
                currentFrame += 30;
            }
        }

        public static void MoveTowards()
        {
            var currentFrame = GameState.LastFrame + 15;

            double aix = GameState.p1.x;
            double playerx = GameState.p2.x;

            if (playerx > aix)
            {
                MQueue.AddToFrame(currentFrame + 1, new StickPress(Direction.E));
            }
            else
            {
                MQueue.AddToFrame(currentFrame + 1, new StickPress(Direction.W));
            }

        }

        public static void FirefoxOnstage()
        {
            

        }

        public static int JumpAndNair()
        {
            var currentFrame = GameState.LastFrame + 2;

            var aix = GameState.p1.x;
            var playerx = GameState.p2.x;
            var dir = playerx > aix ? Direction.E : Direction.W;

            MQueue.AddToFrame(currentFrame + 0, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 4, new StickPress(Direction.N));
            MQueue.AddToFrame(currentFrame + 5, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 6, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 7, new StickPress(dir));

            MQueue.AddToFrame(currentFrame + 8, new DigitalPress(DigitalButton.A));
            MQueue.AddToFrame(currentFrame + 9, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 10, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 11, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 12, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 13, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 14, new StickPress(dir));
            MQueue.AddToFrame(currentFrame + 15, new DigitalPress(DigitalButton.R));
            MQueue.AddToFrame(currentFrame + 16, new DigitalPress(DigitalButton.R));
            MQueue.AddToFrame(currentFrame + 17, new DigitalPress(DigitalButton.R));

            return 15;
        }

        public static void Firefox()
        {
            var currentFrame = GameState.LastFrame + 2;
            MQueue.AddToFrame(currentFrame + 0, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            MQueue.AddToFrame(currentFrame + 1, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            MQueue.AddToFrame(currentFrame + 2, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            MQueue.AddToFrame(currentFrame + 3, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            MQueue.AddToFrame(currentFrame + 4, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            MQueue.AddToFrame(currentFrame + 5, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
            for (int i = 5; i < 60; i++)
                MQueue.AddToFrame(currentFrame + 5 + i, new StickPress(0, 0, GameState.p1.x, GameState.p1.y));
        }

        public static void MoveCursorTo(float x, float y)
        {
            double _x = GameState.p1.CursorX;
            double _y = GameState.p1.CursorY;
        }

        public static void RunUpUpsmash()
        {

        }
    }
}