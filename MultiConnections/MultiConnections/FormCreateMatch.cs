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
        private List<Player> listPlayer;
        private Player lastPlayer = null;
        private int playersCount;
        private bool leftSide = true;
        private Player player1;
        private Player player2;
        private Match match;
        public FormCreateMatch(List<Player> listPlayer)
        {
            InitializeComponent();
            this.listPlayer = listPlayer;
            playersCount = listPlayer.Count;
            lastPlayer = listPlayer[listPlayer.Count - 1];
            AddUCPlayer();
        }

        private void AddUCPlayer()
        {
            for (int i = 0; i < listPlayer.Count - 1; i++)
            {
                UCPlayer ucPlayer = new UCPlayer(listPlayer[i]);
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
                RemovePlayerIfNescessary();
            }
        }

        private void RemovePlayerIfNescessary()
        {
            if(!ExistingPlayer(player1))
            {
                RemovePlayer1();
            }
            if(!ExistingPlayer(player2))
            {
                RemovePlayer2();
            }
        }

        private bool ExistingPlayer(Player playerOnMatch)
        {
            foreach(Player player in listPlayer)
            {
                if (player == playerOnMatch)
                    return true;
            }
            return false;
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
            Player player = ((UCPlayer)sender).Player;
            if (player.Playing)
            {
                MessageBox.Show("Player is now playing!!!");
                return;
            }
            if (player == player1 || player == player2)
                return;
            if (player1 == null || (player2 != null && leftSide))
            {
                ptbAvatar1.Image = player.Avatar;
                lblNameOfPlayer1.Text = player.Name;
                player1 = player;
                leftSide = false;
            }
            else
            {
                ptbAvatar2.Image = player.Avatar;
                lblNameOfPlayer2.Text = player.Name;
                player2 = player;
                leftSide = true;
            }
            
        }

        private bool PlayersChange()
        {
            if(playersCount == listPlayer.Count)
            {
                if (lastPlayer == listPlayer[listPlayer.Count - 1])
                    return false;
            }
            playersCount = listPlayer.Count;
            lastPlayer = listPlayer[playersCount - 1];
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
