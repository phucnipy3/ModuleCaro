using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DBAccessLibrary.DBHelper;

namespace ClassLibraryServer
{
    public partial class UCMatch : UserControl
    {
        private Match match;
        private bool quickPlay = true;

        public event EventHandler ButtonCloseClicked;
        public UCMatch(Match match)
        {
            InitializeComponent();
            this.match = match;
            
            lblNameOfPlayer1.Text = match.Player1.Name;
            lblNameOfPlayer2.Text = match.Player2.Name;
        }

        private void btnByGame_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(match.TryStartOneGame));
            thread.IsBackground = true;
            thread.Start();
        }

        private void btnByMatch_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(match.TryStartAllGames));
            thread.IsBackground = true;
            thread.Start();
            btnByGame.Enabled = false;
            btnByMatch.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            match.Player1.OutMatch();
            match.Player2.OutMatch();
            OnButtonCloseClicked(e);
        }
        protected virtual void OnButtonCloseClicked(EventArgs e)
        {
            EventHandler handler = this.ButtonCloseClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
