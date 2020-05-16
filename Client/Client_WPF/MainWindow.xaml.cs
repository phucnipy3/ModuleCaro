using ClassLibraryClient;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Timers;
using System.IO;
using System.Windows.Documents;

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
        private DialogLoading dialogLoading;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            dialogLoading = new DialogLoading();
            Grid.SetRow(dialogLoading, 1);
            grdMain.Children.Add(dialogLoading);
            dialogLoading.loading.IsOpen = true;
            btnLogin.IsEnabled = false;
            InitHomePage();
        }

        public void MoveToHomePage()
        {
            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                ForwardPage();
                grdMain.Children.Remove(dialogLoading);
            }));
        }
        private void InitHomePage()
        {
            ipAddress = txtIPServer.Text.Trim();
            username = txtID.Text.Trim();
            password = txtPassword.Password.Trim();
            myClient = new MyClient(username, password, ipAddress);
            myClient.LoginMessageReceived += myClient_LoginMessageReceived;
            myClient.TakeTooMuchTimeToConnect += myClient_TakeTooMuchTime;
            myClient.ConnectionChanged += myClient_ConnectionChanged;
            myClient.TimesUp += myClient_TimesUp;

        }

        private void myClient_TimesUp(object sender, ElapsedEventArgs e)
        {
            rtxtLog.Dispatcher.BeginInvoke(new Action(delegate
            {
               rtxtLog.AppendText(e.SignalTime.ToString());
            }));
        }

        private void myClient_ConnectionChanged(object sender, ConnectionChangedEventArgs e)
        {
            txbTemp.Dispatcher.BeginInvoke(new Action(delegate
            {
                if (e.isConnected)
                {
                    txbTemp.Text = "Đã kết nối";
                    iconConnection.Foreground = new SolidColorBrush(Colors.YellowGreen);
                }
                else
                {
                    txbTemp.Text = "Mất kết nối";
                    iconConnection.Foreground = new SolidColorBrush(Colors.Red);
                }
                    
            }));
        }

        private void myClient_TakeTooMuchTime(object sender, EventArgs e)
        {
            MessageBox.Show("Mất quá nhiều thời gian để thiết lập kết nối", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            btnLogin.Dispatcher.BeginInvoke(new Action(delegate
            {
                btnLogin.IsEnabled = true;
            }));
        }

        private void myClient_LoginMessageReceived(object sender, LoginMessageReceivedEventArgs e)
        {
            if (e.IsValidLogin)
                MoveToHomePage();
            else
            {
                grdMain.Children.Remove(dialogLoading);
                MessageBox.Show("Sai thông tin đăng nhập", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                btnLogin.Dispatcher.BeginInvoke(new Action(delegate
                {
                    btnLogin.IsEnabled = true;
                }));
            }
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


        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            myClient.ClearInputOutput();
            myClient.StartBot();
            //if (myClient.isRunningBotProcess)
            //{
            //    txbAppStatus.Text = "Đang chạy ứng dụng";
            //    iconApp.Foreground = new SolidColorBrush(Colors.YellowGreen);
            //} 
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Application|*.exe|All Files|*.*";
            openFileDialog.DefaultExt = ".exe";

            if (openFileDialog.ShowDialog() == true)
            {
                txtApp.Text = openFileDialog.FileName;
                myClient.ChangeBot(openFileDialog.FileName);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            myClient.StopBot();
            //txbAppStatus.Text = "Chưa có ứng dụng";
            //iconApp.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Reset();    
        }

        void BackPage()
        {
            grdLogin.Visibility = Visibility.Visible;
            grdHome.Visibility = Visibility.Hidden;
        }

        void Reset()
        {
            BackPage();
            txtID.Clear();
            txtPassword.Clear();
            btnLogin.IsEnabled = true;
        }
    }
}
