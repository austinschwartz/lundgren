using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lundgren.Controller;
using Lundgren.Game;

namespace Lundgren.AI
{
    public class Lundgren : IBot
    {
        public override sealed MoveQueue Queue { get; set; }

        public override ControllerState State { get; set; }
        public override sealed ControllerState Prev { get; set; }

        public Lundgren()
        {
            Queue = new MoveQueue();
            Prev = new ControllerState(-1);
        }

        public override bool ProcessMoves()
        {
            var lastFrameNum = GameState.LastFrame;
            var thisFrameNum = GameState.GetFrame();

            if (thisFrameNum == lastFrameNum)
                return false;

            if (GameState.p1 != null && GameState.Stage != null)
            {
                if (GameState.p1.OnLeftLedge(GameState.Stage))
                    Debug.WriteLine("on left edge");
                if (GameState.p1.OnRightLedge(GameState.Stage))
                    Debug.WriteLine("on right edge");
            }

            if (thisFrameNum != lastFrameNum + 1)
                Debug.WriteLine($"Lost frames between { lastFrameNum } and { thisFrameNum }");

            Prev = State;
            if (Queue.HasFrame(thisFrameNum))
            {
                //Debug.WriteLine($"Performing move on frame { thisFrameNum }");
                State = Queue.Get(thisFrameNum);
                //Debug.WriteLine(State);
                //var rem = Queue.Remove(thisFrameNum);
                //LogFrameState(thisFrameNum, State, rem);
            }
            else
            {
                State = new ControllerState(thisFrameNum);
            }
            return true;
        }

    }
}
