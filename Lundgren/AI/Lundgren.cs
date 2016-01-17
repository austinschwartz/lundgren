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

        public readonly MoveQueue _queue;

        public ControllerState State, Prev;

        public Lundgren()
        {
            _queue = new MoveQueue();
            Prev = new ControllerState(-1);
        }

        public override bool ProcessMoves()
        {
            var thisFrameNum = GameState.Frame;
            var lastFrameNum = GameState.LastFrame;

            if (thisFrameNum == lastFrameNum)
                return false;

            if (GameState.p1 != null && GameState.Stage != null)
            {
                if (GameState.p1.OnLeftLedge(GameState.Stage))
                    Debug.WriteLine("on left edge");
                if (GameState.p1.OnRightLedge(GameState.Stage))
                    Debug.WriteLine("on right edge");
            }

            //Debug.WriteLine("On frame " + thisFrameNum);
            if (thisFrameNum != lastFrameNum + 1 && thisFrameNum > lastFrameNum)
                Debug.WriteLine($"Lost frames between { lastFrameNum } and { thisFrameNum }");

            lastFrameNum = thisFrameNum;
            Prev = State;
            if (_queue.HasFrame(thisFrameNum))
            {
                Debug.WriteLine($"Performing move on frame { thisFrameNum }");
                State = _queue.Get(thisFrameNum);
                var rem = _queue.Remove(thisFrameNum);
                //LogFrameState(thisFrameNum, State, rem);
            }
            else
            {
                State = new ControllerState(lastFrameNum);
            }
            return true;
        }
    }
}
