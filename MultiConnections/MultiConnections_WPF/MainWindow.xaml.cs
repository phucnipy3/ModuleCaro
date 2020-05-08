using ClassLibraryServer;
using DBAccessLibrary.DBHelper;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MultiConnections_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyServer myServer;
        CreateMatch createMatch;
        public MainWindow()
        {
            InitializeComponent();
            myServer = new MyServer();
            txbIP.Text = myServer.GetIPAddress().ToString();
            myServer.ConnectionsChanged += myServer_ConnectionsChanged;
            createMatch = new CreateMatch(myServer.Players);
        }

        private void myServer_ConnectionsChanged(object sender, EventArgs e)
        {
            RefreshListPlayer(myServer.Players);
            if(createMatch.Visibility == Visibility.Visible)
                createMatch.RefreshListPlayer();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                iconMaximize.Kind = PackIconKind.WindowMaximize;
                btnMaximize.ToolTip = "Maximize";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                iconMaximize.Kind = PackIconKind.WindowRestore;
                btnMaximize.ToolTip = "Restore Down";
            }    
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCreateMatch_Click(object sender, RoutedEventArgs e)
        {
            createMatch = new CreateMatch(myServer.Players);
            if (createMatch.ShowDialog() == true)
            {
                UCMatch ucMatch = new UCMatch(createMatch.GetMatch());
                ucMatch.ButtonCloseClicked += UcMatch_ButtonCloseClicked;
                wrapPanelMatches.Children.Add(ucMatch);
            }
        }

        private void UcMatch_ButtonCloseClicked(object sender, EventArgs e)
        {
            wrapPanelMatches.Children.Remove(sender as UCMatch);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshListPlayer(myServer.Players);
        }

        private void RefreshListPlayer(List<Player> players)
        {
            spnlPlayer.Dispatcher.BeginInvoke(new Action(delegate
            {
                spnlPlayer.Items.Clear();
                for (int i = 0; i < players.Count; i++)
                {
                    spnlPlayer.Items.Add(new UCPlayer(players[i]));
                }
            }));

            
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
        }
    }   
}
