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
    /// Interaction logic for CreateMatch.xaml
    /// </summary>
    public partial class CreateMatch : Window
    {
        public CreateMatch()
        {
            InitializeComponent();
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
    }
}
