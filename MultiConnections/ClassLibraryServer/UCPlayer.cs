using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ClassLibraryServer
{
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
            lblName.Text = player.Name;
            ptbAvatar.Image = player.Avatar;
        }

        public Player Player { get => player; set => player = value; }

        private void ptbAvatar_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void lblName_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }
    }
}
