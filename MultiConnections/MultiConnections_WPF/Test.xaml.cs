using ClassLibraryServer.UserControls;
using DBAccessLibary.Models;
using DBAccessLibrary.DBHelper;
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
            storedMatch = Helper.GetMatch(26);
            InitMatch();
            //storedMatch = match;


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
