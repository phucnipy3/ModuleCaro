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
using System.Diagnostics;
using Microsoft.Win32;

namespace Client_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process app;

        private string ipAddress;
        private string username;
        private string password;
        private MyClient myClient;
        public MainWindow()
        {
            InitializeComponent();
            txbConnectionStatus.Tag = Colors.Red;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            InitHomePage();
        }
        public void MoveToHomePage()
        {
            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                DialogLoading dialog = new DialogLoading();
                Grid.SetRow(dialog, 1);
                grdMain.Children.Add(dialog);
                dialog.loading.IsOpen = true;
                ForwardPage();
                grdMain.Children.Remove(dialog);
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
            myClient.StartCheckForConnection(txbTemp);

            
        }

        private void myClient_TakeTooMuchTime(object sender, EventArgs e)
        {
            MessageBox.Show("Mất quá nhiều thời gian để thiết lập kết nối");
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
                MessageBox.Show("Sai thông tin đăng nhập");
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
            app = Process.Start(txtApp.Text.Trim());
            
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Application|*.exe|All Files|*.*";
            openFileDialog.DefaultExt = ".exe";

            if (openFileDialog.ShowDialog() == true)
            {
                txtApp.Text = openFileDialog.FileName;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (!app.HasExited)
                app.Kill();
        }
    }
}
