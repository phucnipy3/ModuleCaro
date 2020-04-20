using ClassLibraryClient;
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

namespace Client_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ipAddress;
        private string username;
        private string password;
        private MyClient myClient;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            InitHomePage();
            DialogLoading dialog = new DialogLoading();
            Grid.SetRow(dialog, 1);
            grdMain.Children.Add(dialog);
            dialog.loading.IsOpen = true;
            ForwardPage();
            grdMain.Children.Remove(dialog);
        }

        private void InitHomePage()
        {
            ipAddress = txtIPServer.Text.Trim();
            username = txtID.Text.Trim();
            password = txtPassword.Password.Trim();
            myClient = new MyClient(username, password, ipAddress);
            myClient.StartConnectToServer();
            myClient.StartReceiveAndSend();
            myClient.StartCheckForConnection(txbConnectionStatus);
        }

        void ForwardPage()
        {
            grdLogin.Visibility = Visibility.Hidden;
            grdHome.Visibility = Visibility.Visible;
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

        private void TxtIPServer_LostFocus(object sender, RoutedEventArgs e)
        {
            txtIPServer.IsEnabled = false;
        }

        private void BtnReFill_Click(object sender, RoutedEventArgs e)
        {
            txtIPServer.IsEnabled = true;
            txtIPServer.Clear();
            txtIPServer.Focus();
        }
    }
}
