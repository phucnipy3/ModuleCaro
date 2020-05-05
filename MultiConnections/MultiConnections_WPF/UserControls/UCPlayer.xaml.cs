using ClassLibraryServer;
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

namespace MultiConnections_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for UCPlayer.xaml
    /// </summary>
    public partial class UCPlayer : UserControl
    {
        private Player player;
        public UCPlayer()
        {
            InitializeComponent();
        }
        public UCPlayer(Player player)
        {
            InitializeComponent();
            this.Player = player;
            txbPlayerName.Text = player.Name;
        }


        public Player Player { get => player; set => player = value; }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.OnMouseDown(e);
        }
    }
}
