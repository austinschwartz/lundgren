using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lundgren.Controller;

namespace Lundgren.AI
{
    public abstract class IBot
    {
        public abstract bool ProcessMoves();
        public MoveQueue _queue;
        public ControllerState State, Prev;
    }
}
