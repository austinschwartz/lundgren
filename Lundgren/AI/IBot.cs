using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lundgren.Controller;
using Lundgren.Forms;

namespace Lundgren.AI
{
    public abstract class IBot
    {
        public abstract bool ProcessMoves();
        public abstract MoveQueue Queue { get; set; }
        public abstract ControllerState State { get; set; }
        public abstract ControllerState Prev { get; set; }
        public abstract Moves MovesInstance { get; set; }
    }
}
