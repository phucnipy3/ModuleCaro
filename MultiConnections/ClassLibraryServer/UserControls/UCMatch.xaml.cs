using DBAccessLibary.Models;
using DBAccessLibrary.DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibraryServer.Operating;

namespace ClassLibraryServer.UserControls
{
    /// <summary>
    /// Interaction logic for UCMatch.xaml
    /// </summary>
    public partial class UCMatch : UserControl
    {
        private Match match;
        private int gamesPlayed = 0;

        public event EventHandler ButtonCloseClicked;
        public UCMatch(Match match)
        {
            InitializeComponent();
            this.match = match;
            this.match.ScoreChanged += match_ScoreChanged;
            txbPlayer1.Text = match.Player1.Name;
            txbPlayer2.Text = match.Player2.Name;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.Margin = new Thickness(10, 10, 0, 0);
            
        }

        private void match_ScoreChanged(object sender, ScoreChangedEventArgs e)
        {
            txbPlayer1Score.Dispatcher.BeginInvoke(new Action(delegate {
                txbPlayer1Score.Text = e.ScoreOfPlayer1.ToString();
            }));
            txbPlayer2Score.Dispatcher.BeginInvoke(new Action(delegate {
                txbPlayer2Score.Text = e.ScoreOfPlayer2.ToString();
            }));
        }

        protected virtual void OnButtonCloseClicked(EventArgs e)
        {
            EventHandler handler = this.ButtonCloseClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private async void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            btnPlay.IsEnabled = false;
            await PlayAsync();
        }

        private async Task PlayAsync()
        {
            if (radOneGame.IsChecked == true)
            {
                await match.TryStartOneGameAsync();

                gamesPlayed++;
                if (gamesPlayed < match.MaxGames)
                    btnPlay.IsEnabled = true;

            }
            else
            {
                await match.TryStartAllGamesAsync();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            match.Player1.OutMatch();
            match.Player2.OutMatch();
            match.Player1 = null;
            match.Player2 = null;
            OnButtonCloseClicked(e);
        }

        private async void btnShowMatch_Click(object sender, RoutedEventArgs e)
        {
            await ShowMatchAsync();
        }

        private async Task ShowMatchAsync()
        {
            StoredMatch match = await Helper.GetMatchAsync(this.match.StoredMatchId);
            ShowMatch showMatch = new ShowMatch(match);
            await Task.Run(() => showMatch.Dispatcher.BeginInvoke(new Action(delegate
            {
                showMatch.Show();
            })));
        }
    }
}
