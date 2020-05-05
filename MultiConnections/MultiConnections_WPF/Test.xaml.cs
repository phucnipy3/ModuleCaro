
using DBAccessLibary.Models;
using DBAccessLibrary.DBHelper;
using MultiConnections_WPF.UserControls;
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

namespace MultiConnections_WPF
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private StoredMatch storedMatch;
        public Test()
        {
            InitializeComponent();
            //storedMatch = await Helper.GetMatchAsync(26);
            InitMatch();
            //storedMatch = match;

            ShowPlayerInformations();
        }

        private void InitMatch()
        {
            for (int i = 0; i < storedMatch.GameCount; i++)
            {
                UCTab tab = new UCTab(storedMatch.Games[i]);

                TabItem tabItem = new TabItem() { Header = "Trận " + (i + 1).ToString() };
                tabItem.Content = tab;
                tcMatch.Items.Add(tabItem);
            }
        }

        private void ShowPlayerInformations()
        {
            ShowName();
            ShowScore();
        }

        private void ShowName()
        {
            txbPlayer1.Text = storedMatch.Player1.Username.Trim();
            txbPlayer2.Text = storedMatch.Player2.Username.Trim();

        }

        private void ShowScore()
        {
            int score1 = 0;
            int score2 = 0;
            foreach (var game in storedMatch.Games)
            {
                if (game.Winner == storedMatch.Player1)
                    score1++;
                if (game.Winner == storedMatch.Player2)
                    score2++;
            }

            txbPlayer1Score.Text = score1.ToString();
            txbPlayer2Score.Text = score2.ToString();
        }


        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
