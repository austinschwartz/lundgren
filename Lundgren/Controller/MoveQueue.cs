using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundgren.Controller
{
    class MoveQueue
    {
        Dictionary<int, ControllerState> moveDict;

        public MoveQueue()
        {
            moveDict = new Dictionary<int, ControllerState>();
        }

        public bool HasFrame(int frame)
        {
            return moveDict.ContainsKey(frame);
        }

        public ControllerState Get(int frame)
        {
            return moveDict[frame];
        }

        public void Clear()
        {
            moveDict.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var keyPair in moveDict)
            {
                sb.Append($"{keyPair.Key} : {keyPair.Value}\n");
            }
            return sb.ToString();
        }

        public void Add(int frame, ControllerState currentState)
        {
            moveDict.Add(frame, currentState);
        }

        public bool Remove(int frame)
        {
            return moveDict.Remove(frame);
        }

        public void AddToFrame(int frame, ButtonPress bp)
        {
            ControllerState currentState = HasFrame(frame) ? moveDict[frame] : new ControllerState(frame);
            currentState.Add(bp);
            moveDict[frame] = currentState;
        }
    }
}
