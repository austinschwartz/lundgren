using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lundgren.Controller
{
    public class MoveQueue
    {
        private readonly Dictionary<int, ControllerState> _moveDict;

        private static MoveQueue _queue;

        private MoveQueue()
        {
            _moveDict = new Dictionary<int, ControllerState>();
        }

        public static MoveQueue MQueue => _queue ?? (_queue = new MoveQueue());

        public bool HasFrame(int frame)
        {
            return _moveDict.ContainsKey(frame);
        }

        public ControllerState Get(int frame)
        {
            return _moveDict[frame];
        }

        public void Clear()
        {
            _moveDict.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var keyPair in _moveDict)
            {
                sb.Append($"{keyPair.Key} : {keyPair.Value}\n");
            }
            return sb.ToString();
        }

        public void Add(int frame, ControllerState currentState)
        {
            _moveDict.Add(frame, currentState);
        }

        public bool Remove(int frame)
        {
            return _moveDict.Remove(frame);
        }

        public void AddToFrame(int frame, ButtonPress bp)
        {
            ControllerState currentState = HasFrame(frame) ? _moveDict[frame] : new ControllerState(frame);
            currentState.Add(bp);
            _moveDict[frame] = currentState;
        }

        public void AddToFrame(int frame, HashSet<ButtonPress> set)
        {
            ControllerState currentState = HasFrame(frame) ? _moveDict[frame] : new ControllerState(frame);
            foreach (ButtonPress bp in set)
            {
                currentState.Add(bp);
            }
            _moveDict[frame] = currentState;
        }

    }
}
