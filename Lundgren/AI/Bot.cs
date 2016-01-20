using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Lundgren.Controller;
using Lundgren.Forms;
using Lundgren.Game;
using Lundgren.Logs;
using static Lundgren.Controller.MoveQueue;

namespace Lundgren.AI
{
    public class Bot
    {
        public ControllerState State { get; set; }
        public ControllerState Prev { get; set; }

        public Action<object, Logging.LogEventArgs> _log;

        private static Bot instance;

        public static Bot Instance => instance ?? (instance = new Bot());

        public int t = 0;

        private Bot()
        {
            Prev = new ControllerState(-1);
        }

        public bool AboveStage()
        {
            return GameState.p1.y > 0;
        }
        public bool BelowStage()
        {
            return GameState.p1.y < 0;
        }

        public bool OffStage()
        {
            return 
                GameState.p1.x < GameState.Stage.LeftEdge.x ||                                                                                                                                                                                                                                                                                          
                GameState.p1.x > GameState.Stage.RightEdge.x;
        }

        public void AIProcess(int thisFrameNum)
        {
            if (GameState.p1 != null && GameState.Stage != null)
            {
                if (GameState.p1.ActionNum == 13) // rebirthwait
                {
                    MQueue.AddToFrame(thisFrameNum + 0, new StickPress(Direction.S));
                    MQueue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.S));
                    MQueue.AddToFrame(thisFrameNum + 2, new StickPress(Direction.S));
                    MQueue.AddToFrame(thisFrameNum + 3, new StickPress(Direction.S));
                    MQueue.AddToFrame(thisFrameNum + 4, new StickPress(Direction.S));
                    Debug.WriteLine("holding down to rebirth");
                }
                else if (OffStage() && GameState.p1.ActionNum != 253 && GameState.p1.ActionNum != 0)
                {
                    Debug.WriteLine("off stage");
                    if (GameState.p1.ActionNum == 354)
                    {
                        if (GameState.p1.x < 0)
                            MQueue.AddToFrame(thisFrameNum, new StickPress(GameState.p2.x, GameState.p2.y, GameState.Stage.LeftEdge.x, GameState.Stage.LeftEdge.y));
                        else
                            MQueue.AddToFrame(thisFrameNum, new StickPress(GameState.p2.x, GameState.p2.y, GameState.Stage.RightEdge.x, GameState.Stage.RightEdge.y));
                    }
                    else if (GameState.p1.ActionNum == 38 || 
                             GameState.p1.ActionNum == 32 || 
                             GameState.p1.ActionNum == 3 || 
                             GameState.p1.ActionNum == 29)// 88 or 38 = tumble, 29 = fall
                    {
                        MQueue.AddToFrame(thisFrameNum, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Debug.WriteLine("firefoxing");
                    }
                }
                else if (GameState.p1.ActionNum == 253)
                {
                    MQueue.AddToFrame(thisFrameNum + 1, new DigitalPress(DigitalButton.A));
                    MQueue.AddToFrame(thisFrameNum + 2, new DigitalPress(DigitalButton.A));
                    MQueue.AddToFrame(thisFrameNum + 3, new DigitalPress(DigitalButton.A));
                }
                else if (GameState.p1.ActionNum != 0 && GameState.p1.OnLeftLedge(GameState.Stage))
                {
                    Debug.WriteLine("rolling right");
                    MQueue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.E));
                    MQueue.AddToFrame(thisFrameNum + 2, new StickPress(Direction.E));
                    MQueue.AddToFrame(thisFrameNum + 3, new StickPress(Direction.E));
                }

                else if (GameState.p1.ActionNum != 0 && GameState.p1.OnRightLedge(GameState.Stage))
                {
                    Debug.WriteLine("rolling left");
                    MQueue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.W));
                    MQueue.AddToFrame(thisFrameNum + 2, new StickPress(Direction.W));
                    MQueue.AddToFrame(thisFrameNum + 3, new StickPress(Direction.W));
                }
                else if (GameState.p1.ActionNum != 0 && GameState.p1.x < GameState.Stage.LeftEdge.x + 30)
                {
                    MQueue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.E));
                }
                else if (GameState.p1.ActionNum != 0 && GameState.p1.x > GameState.Stage.RightEdge.x - 30)
                {
                    MQueue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.W));
                }
                else if (GameState.p1.ActionNum == 90 && GameState.p1.y <= 1)
                {
                    MQueue.AddToFrame(thisFrameNum, new DigitalPress(DigitalButton.Z));
                }

                else if (GameState.p1.ActionNum == 192 && GameState.p1.y <= 30)
                {
                    Random rnd = new Random();
                    int randNum = rnd.Next(0, 4);
                    if (randNum == 0)
                    {
                        MQueue.AddToFrame(thisFrameNum + 3, new DigitalPress(DigitalButton.A));
                        MQueue.AddToFrame(thisFrameNum + 4, new DigitalPress(DigitalButton.A));
                        MQueue.AddToFrame(thisFrameNum + 5, new DigitalPress(DigitalButton.A));
                    }
                    else
                    {
                        Direction dir;
                        if (randNum == 1)
                            dir = Direction.E;
                        else if (randNum == 2)
                            dir = Direction.W;
                        else
                            dir = Direction.S;
                        MQueue.AddToFrame(thisFrameNum + 3, new StickPress(dir));
                        MQueue.AddToFrame(thisFrameNum + 4, new StickPress(dir));
                        MQueue.AddToFrame(thisFrameNum + 5, new StickPress(dir));
                    }
                }
                else if (t <= 0)
                {
                    /*
                    if (GameState.p1.ActionNum == 14)
                        t = MovesInstance.JumpAndNair();
                        */
                }
                t--;

            }
        }

        public bool ProcessMoves()
        {
            var lastFrameNum = GameState.LastFrame;
            var thisFrameNum = GameState.GetFrame();

            if (thisFrameNum == lastFrameNum)
                return false;

            AIProcess(thisFrameNum);

            if (thisFrameNum != lastFrameNum + 1 && thisFrameNum > lastFrameNum)
                _log?.Invoke(null, new Logging.LogEventArgs($"Lost frames between { lastFrameNum } and { thisFrameNum }"));

            Prev = State;
            if (MQueue.HasFrame(thisFrameNum))
            {
                State = MQueue.Get(thisFrameNum);
                var rem = MQueue.Remove(thisFrameNum);
                LogFrameState(thisFrameNum, State, rem);
            }
            else
            {
                State = new ControllerState(thisFrameNum);
            }
            return true;
        }

        private void LogFrameState(int frameNum, ControllerState controllerState, bool rem)
        {
            _log?.Invoke(null,
                !rem
                    ? new Logging.LogEventArgs($"Something went wrong on frame {frameNum}")
                    : new Logging.LogEventArgs($"F: {frameNum} - {controllerState}"));
        }

    }
}
