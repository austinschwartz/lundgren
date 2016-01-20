using System;
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

namespace Lundgren.AI
{
    public class Lundgren : IBot
    {
        public override sealed MoveQueue Queue { get; set; }

        public override ControllerState State { get; set; }
        public override sealed ControllerState Prev { get; set; }
        public override sealed Moves MovesInstance { get; set; }

        public int t = 0;

        private Action<object, Logging.LogEventArgs> _log;

        public Lundgren()
        {
            Queue = new MoveQueue();
            Prev = new ControllerState(-1);
            MovesInstance = new Moves(this);

        }

        public Lundgren(Action<object, Logging.LogEventArgs> log)
        {
            this._log = log;
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

        public override bool ProcessMoves()
        {
            var lastFrameNum = GameState.LastFrame;
            var thisFrameNum = GameState.GetFrame();

            if (thisFrameNum == lastFrameNum)
                return false;

            if (GameState.p1 != null && GameState.Stage != null)
            {
                PlayerData ai = GameState.p1;

                if (ai.ActionNum == 13) // rebirthwait
                {
                    Queue.AddToFrame(thisFrameNum + 0, new StickPress(Direction.S));
                    Queue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.S));
                    Queue.AddToFrame(thisFrameNum + 2, new StickPress(Direction.S));
                    Queue.AddToFrame(thisFrameNum + 3, new StickPress(Direction.S));
                    Queue.AddToFrame(thisFrameNum + 4, new StickPress(Direction.S));
                    Debug.WriteLine("holding down to rebirth");
                }
                else if (OffStage() && ai.ActionNum != 253)
                {
                    Debug.WriteLine("off stage");
                    if (ai.ActionNum == 354)
                    {
                        //Queue.AddToFrame(thisFrameNum, new StickPress( GameState.p2.x, GameState.p2.y, ai.x, ai.y));
                        if (ai.x < 0)
                            Queue.AddToFrame(thisFrameNum, new StickPress( GameState.p2.x, GameState.p2.y, GameState.Stage.LeftEdge.x, GameState.Stage.LeftEdge.y));
                        else
                            Queue.AddToFrame(thisFrameNum, new StickPress(GameState.p2.x, GameState.p2.y, GameState.Stage.RightEdge.x, GameState.Stage.RightEdge.y));
                    }
                    else // 88 or 38 = tumble
                    {
                        Queue.AddToFrame(thisFrameNum, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Queue.AddToFrame(thisFrameNum + 1, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Queue.AddToFrame(thisFrameNum + 2, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Queue.AddToFrame(thisFrameNum + 3, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Queue.AddToFrame(thisFrameNum + 4, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Queue.AddToFrame(thisFrameNum + 5, new HashSet<ButtonPress> { new StickPress(Direction.N), new DigitalPress(DigitalButton.B) });
                        Debug.WriteLine("firefoxing");
                    }
                }
                else if (ai.ActionNum == 253)
                {
                    Queue.AddToFrame(thisFrameNum + 1, new DigitalPress(DigitalButton.A));
                    Queue.AddToFrame(thisFrameNum + 2, new DigitalPress(DigitalButton.A));
                    Queue.AddToFrame(thisFrameNum + 3, new DigitalPress(DigitalButton.A));
                }
                else if (GameState.p1.OnLeftLedge(GameState.Stage))
                {
                    Queue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.E));
                    Queue.AddToFrame(thisFrameNum + 2, new StickPress(Direction.E));
                    Queue.AddToFrame(thisFrameNum + 3, new StickPress(Direction.E));
                    Debug.WriteLine("on left edge");
                }

                else if (GameState.p1.OnRightLedge(GameState.Stage))
                {
                    Queue.AddToFrame(thisFrameNum + 1, new StickPress(Direction.W));
                    Queue.AddToFrame(thisFrameNum + 2, new StickPress(Direction.W));
                    Queue.AddToFrame(thisFrameNum + 3, new StickPress(Direction.W));
                    Debug.WriteLine("on right edge");
                }
                else if (ai.ActionNum == 90 && ai.y <= 30)
                {
                    Queue.AddToFrame(thisFrameNum, new DigitalPress(DigitalButton.R));
                    Debug.WriteLine("attempting techs");
                }

                else if (ai.ActionNum == 192 && ai.y <= 30)
                {
                    Random rnd = new Random();
                    int randNum = rnd.Next(0, 4); // 1-3
                    if (randNum == 0)
                    {
                        Queue.AddToFrame(thisFrameNum + 3, new DigitalPress(DigitalButton.A));
                        Queue.AddToFrame(thisFrameNum + 4, new DigitalPress(DigitalButton.A));
                        Queue.AddToFrame(thisFrameNum + 5, new DigitalPress(DigitalButton.A));
                        Debug.WriteLine("attempting to get up attack");
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
                        Queue.AddToFrame(thisFrameNum + 3, new StickPress(dir));
                        Queue.AddToFrame(thisFrameNum + 4, new StickPress(dir));
                        Queue.AddToFrame(thisFrameNum + 5, new StickPress(dir));
                        Debug.WriteLine("attempting to roll");
                    }
                } else if (t <= 0)
                {
                    /*
                    if (GameState.p1.ActionNum == 14)
                        t = MovesInstance.JumpAndNair();
                        */
                }
                t--;

            }

            if (thisFrameNum != lastFrameNum + 1 && thisFrameNum > lastFrameNum)
                Debug.WriteLine($"Lost frames between { lastFrameNum } and { thisFrameNum }");

            Prev = State;
            if (Queue.HasFrame(thisFrameNum))
            {
                Debug.WriteLine($"Performing move on frame { thisFrameNum }");
                State = Queue.Get(thisFrameNum);
                Debug.WriteLine(State);
                var rem = Queue.Remove(thisFrameNum);
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
