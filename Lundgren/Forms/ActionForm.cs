using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lundgren.Game;
using Lundgren.Logs;

namespace Lundgren.Forms
{
    public partial class ActionForm : Form
    {
        public ActionForm()
        {
            InitializeComponent();
        }

        private void btnSwag_Click(object sender, EventArgs e)
        {
            Moves.MoveLol();
        }

        private void btnMultishine_Click(object sender, EventArgs e)
        {
            Moves.MoveMultiShine();
        }

        private void btnSelectFox_Click(object sender, EventArgs e)
        {
            Moves.AttemptToPickFox20XX();
        }

        private void btnWaveshine_Click(object sender, EventArgs e)
        {
            Moves.MoveWaveshine();
            /*
            var threadDelegate = new ThreadStart(Moves.MoveWaveshine);
            var t = new Thread(threadDelegate);
            //Log(null, new Logging.LogEventArgs("Attempting to waveshine."));
            t.Start();
            */
        }

        private void btnMoveTowards_Click(object sender, EventArgs e)
        {
            Moves.MoveTowards();
        }

        private void btnRunUpsmash_Click(object sender, EventArgs e)
        {
            Moves.RunUpUpsmash();
        }

        private void btnFirefoxToCenter_Click(object sender, EventArgs e)
        {
            Moves.Firefox();
        }

        private void btnDoubleLaser_Click_1(object sender, EventArgs e)
        {
            Moves.DoubleLaser();
        }
    }
}
