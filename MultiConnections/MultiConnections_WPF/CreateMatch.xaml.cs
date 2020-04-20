using ClassLibraryServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassLibraryServer.UserControls;
using System.Threading;

namespace MultiConnections_WPF
{
    /// <summary>
    /// Interaction logic for CreateMatch.xaml
    /// </summary>
    public partial class CreateMatch : Window
    {
        private List<Player> players;
        private Player lastPlayer = null;
        private int playersCount;
        private bool leftSide = true;
        private Player player1;
        private Player player2;
        private Match match;
        public CreateMatch()
        {
            InitializeComponent();
        }
        public CreateMatch(List<Player> players)
        {
            InitializeComponent();
            this.players = players;
            playersCount = players.Count;
            lastPlayer = players[players.Count - 1];
            AddUCPlayer(spnlPlayer);
            StartThreadRefreshListPlayer(spnlPlayer);
        }

        private void StartThreadRefreshListPlayer(ListBox spnlPlayer)
        {
            Thread threadRefreshListPlayer = new Thread(RefreshListPlayer);
            threadRefreshListPlayer.IsBackground = true;
            threadRefreshListPlayer.Start(spnlPlayer);
        }

        private void RefreshListPlayer(object sender)
        {
            while(true)
            {
                ListBox listBox = sender as ListBox;
                if (PlayersChange())
                {
                    listBox.Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        AddUCPlayer(listBox);
                        RemoveDisconnectedPlayersOnMatch();
                    }));

                }
                Thread.Sleep(1000);
            }
            
        }
        private void RemoveDisconnectedPlayersOnMatch()
        {
            if (!players.Contains(player1))
            {
                RemovePlayer1();
            }
            if (!players.Contains(player2))
            {
                RemovePlayer2();
            }
        }

        private void RemovePlayer2()
        {
            player2 = null;
            txbPlayerName1.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                txbPlayerName1.Text = "Người 1";

            }));
        }

        private void RemovePlayer1()
        {
            player1 = null;
            txbPlayerName2.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                txbPlayerName2.Text = "Người 2";

            }));
        }
        private bool PlayersChange()
        {
            if (playersCount == players.Count)
            {
                if (lastPlayer == players[players.Count - 1])
                    return false;
            }
            playersCount = players.Count;
            lastPlayer = players[playersCount - 1];
            return true;
        }

        private void AddUCPlayer(ListBox listBox) 
        {
            listBox.Items.Clear();
            for (int i = 0; i < players.Count - 1; i++)
            {
                ClassLibraryServer.UserControls.UCPlayer ucPlayer = new ClassLibraryServer.UserControls.UCPlayer(players[i]);
                ucPlayer.MouseDown += new MouseButtonEventHandler(UcPlayer_MouseDown);
                listBox.Items.Add(ucPlayer);
            }
        }

        private void UcPlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Player selectedPlayer = ((ClassLibraryServer.UserControls.UCPlayer)sender).Player;
            if (selectedPlayer.Playing)
            {
                MessageBox.Show("Player is now playing!!!");
                return;
            }
            if (selectedPlayer == player1 || selectedPlayer == player2)
                return;
            if (player1 == null || (player2 != null && leftSide))
            {
                txbPlayerName1.Text = selectedPlayer.Name;
                player1 = selectedPlayer;
                leftSide = false;
            }
            else
            {
                txbPlayerName2.Text = selectedPlayer.Name;
                player2 = selectedPlayer;
                leftSide = true;
            }
        }



        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (rbtnPlayer1.IsChecked == true)
            {
                rbtnPlayer1.IsChecked = false;
                rbtnPlayer2.IsChecked = true;
            }
            else
            {
                rbtnPlayer1.IsChecked = true;
                rbtnPlayer2.IsChecked = false;
            }
        }
        public Match GetMatch()
        {
            return match;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (player1 == null || player2 == null)
            {
                MessageBox.Show("Not enough players");
                return;
            }
            player1.OnMatch();
            player2.OnMatch();
            //TODO
            //match = new Match(player1, player2, (int)numBestOf.Value, chkRule.Checked);
            if (rbtnPlayer2.IsChecked == true)
                match.SwapPlayers();
            this.Close();
        }
    }
}
