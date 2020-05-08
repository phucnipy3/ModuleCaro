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

using System.Threading;
using MultiConnections_WPF.UserControls;

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
            AddUCPlayer(spnlPlayer);
        }

        
        public void RefreshListPlayer()
        {
            spnlPlayer.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                AddUCPlayer(spnlPlayer);
                RemoveDisconnectedPlayersOnMatch();
            }));
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

        private void AddUCPlayer(ListBox listBox) 
        {
            listBox.Items.Clear();
            for (int i = 0; i < players.Count; i++)
            {
                UCPlayer ucPlayer = new UCPlayer(players[i]);
                ucPlayer.MouseDown += new MouseButtonEventHandler(UcPlayer_MouseDown);
                listBox.Items.Add(ucPlayer);
            }
        }

        private void UcPlayer_MouseDown(object sender, MouseButtonEventArgs e)
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

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {

            if (player1 == null || player2 == null)
            {
                MessageBox.Show("Không đủ số người chơi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            player1.OnMatch();
            player2.OnMatch();

            match = await Match.CreateNewMatchAsync(player1, player2, BestOf(), (bool)tbtnCheckRule.IsChecked);
            if (rbtnPlayer2.IsChecked == true)
                await match.SwapPlayersAsync();
            this.DialogResult = true;
            this.Close();
        }

        public int BestOf()
        {
            int index = lbxBestOf.SelectedIndex;
            switch (index)
            {
                case 0:
                    return 1;
                case 1:
                    return 3;
                case 2:
                    return 5;   
            }
            return 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshListPlayer();
        }
    }
}
