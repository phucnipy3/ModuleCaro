using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryServer;

namespace MultiConnections
{
    public partial class FormCreateMatch : Form
    {   
        private List<Player> players;
        private Player lastPlayer = null;
        private int playersCount;
        private bool leftSide = true;
        private Player player1;
        private Player player2;
        private Match match;
        public FormCreateMatch(List<Player> players)
        {
            InitializeComponent();
            this.players = players;
            playersCount = players.Count;
            lastPlayer = players[players.Count - 1];
            AddUCPlayer();
        }

        private void AddUCPlayer()
        {
            for (int i = 0; i < players.Count - 1; i++)
            {
                UCPlayer ucPlayer = new UCPlayer(players[i]);
                ucPlayer.MouseClick += new MouseEventHandler(UcPlayer_MouseClick);
                PnlPlayerList.Controls.Add(ucPlayer);
            }
        }
        private void timerRefreshListPlayer_Tick(object sender, EventArgs e)
        {
            RefreshListPlayer();
        }
        private void RefreshListPlayer()
        {
            if (PlayersChange())
            {
                this.PnlPlayerList.Controls.Clear();
                AddUCPlayer();
                RemoveDisconnectedPlayersOnMatch();
            }
        }

        private void RemoveDisconnectedPlayersOnMatch()
        {
            if(!players.Contains(player1))
            {
                RemovePlayer1();
            }
            if(!players.Contains(player2))
            {
                RemovePlayer2();
            }
        }

        private void RemovePlayer2()
        {
            player2 = null;
            ptbAvatar2.Image = null;
            lblNameOfPlayer2.Text = "Player's Name";
        }

        private void RemovePlayer1()
        {
            player1 = null;
            ptbAvatar1.Image = null;
            lblNameOfPlayer1.Text = "Player's Name";
        }

        private void UcPlayer_MouseClick(object sender, MouseEventArgs e)
        {
            Player selectedPlayer = ((UCPlayer)sender).Player;
            if (selectedPlayer.Playing)
            {
                MessageBox.Show("Player is now playing!!!");
                return;
            }
            if (selectedPlayer == player1 || selectedPlayer == player2)
                return;
            if (player1 == null || (player2 != null && leftSide))
            {
                lblNameOfPlayer1.Text = selectedPlayer.Name;
                player1 = selectedPlayer;
                leftSide = false;
            }
            else
            {
                lblNameOfPlayer2.Text = selectedPlayer.Name;
                player2 = selectedPlayer;
                leftSide = true;
            }
            
        }

        private bool PlayersChange()
        {
            if(playersCount == players.Count)
            {
                if (lastPlayer == players[players.Count - 1])
                    return false;
            }
            playersCount = players.Count;
            lastPlayer = players[playersCount - 1];
            return true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ucPlayer1_Load(object sender, EventArgs e)
        {

        }

        private void btnGoOn_Click(object sender, EventArgs e)
        {
            
            if (player1 == null || player2 == null)
            {
                MessageBox.Show("Not enough players");
                return;
            }
            player1.OnMatch();
            player2.OnMatch();
            match = new Match(player1, player2, (int)numBestOf.Value, chkRule.Checked);
            if (ptbFirstPlay2.Visible)
                match.SwapPlayers();
            this.DialogResult = DialogResult.OK;
        }

        public Match GetMatch()
        {
            return match;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            bool tempState = ptbFirstPlay1.Visible;
            ptbFirstPlay1.Visible = ptbFirstPlay2.Visible;
            ptbFirstPlay2.Visible = tempState;
        }
    }
}
