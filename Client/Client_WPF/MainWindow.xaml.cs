using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Process app;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DialogLoading dialog = new DialogLoading();
            Grid.SetRow(dialog, 1);
            grdMain.Children.Add(dialog);
            dialog.loading.IsOpen = true;
            ForwardPage();
            grdMain.Children.Remove(dialog);
            
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

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
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
