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

namespace ClassLibraryServer.UserControls
{
    /// <summary>
    /// Interaction logic for UCMatch.xaml
    /// </summary>
    public partial class UCMatch : UserControl
    {
        private Match match;

        public event EventHandler ButtonCloseClicked;
        public UCMatch(Match match)
        {
            InitializeComponent();
            this.match = match;

            txbPlayer1.Text = match.Player1.Name;
            txbPlayer2.Text = match.Player2.Name;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.Margin = new Thickness(10, 10, 0, 0);
        }
        protected virtual void OnButtonCloseClicked(EventArgs e)
        {
            EventHandler handler = this.ButtonCloseClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if(radOneGame.IsChecked == true)
            {
                Thread thread = new Thread(new ThreadStart(match.TryStartOneGame));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                Thread thread = new Thread(new ThreadStart(match.TryStartAllGames));
                thread.IsBackground = true;
                thread.Start();
                btnPlay.IsEnabled = false;
            }

            Thread threadGetScore = new Thread(new ThreadStart(ShowScore));
            threadGetScore.Start();
        }

        public void ShowScore()
        {
            string[] result = match.ShowScore().Trim().Split(':');
            txbPlayer1.Text = result[0].Trim();
            txbPlayer2.Text = result[1].Trim();
        }


        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            match.Player1.OutMatch();
            match.Player2.OutMatch();
            OnButtonCloseClicked(e);
        }

        private void btnShowMatch_Click(object sender, RoutedEventArgs e)
        {
            StoredMatch match = Helper.GetMatch(this.match.StoredMatch.Id);
        }
    }
}
