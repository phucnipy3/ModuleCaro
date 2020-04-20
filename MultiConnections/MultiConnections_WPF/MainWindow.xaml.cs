using ClassLibraryServer;
using MaterialDesignThemes.Wpf;
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
        public MainWindow()
        {
            InitializeComponent();
            myServer = new MyServer();
            txbIP.Text = myServer.GetIPAddress().ToString();
            myServer.StartThreadGetConnections();
            myServer.StartThreadCreateEmptyPlayer();
            myServer.StartThreadRefreshListPlayer(spnlPlayer);
            myServer.StartThreadCheckAndRemoveConnection();
            
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
            CreateMatch createMatch = new CreateMatch(myServer.Players);
            if (createMatch.ShowDialog() == true)
            {
                ClassLibraryServer.UserControls.UCMatch ucMatch = new ClassLibraryServer.UserControls.UCMatch(createMatch.GetMatch());
                ucMatch.ButtonCloseClicked += UcMatch_ButtonCloseClicked;
                wrapPanelMatches.Children.Add(ucMatch);
            }
        }

        private void UcMatch_ButtonCloseClicked(object sender, EventArgs e)
        {
            wrapPanelMatches.Children.Remove(sender as ClassLibraryServer.UserControls.UCMatch);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
              myServer.HardRefreshListPlayer(spnlPlayer);
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
        }
    }   
}
