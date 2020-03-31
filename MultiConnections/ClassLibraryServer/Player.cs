using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace ClassLibraryServer
{
    public class Player
    {
        private TcpClient client;
        private string name;
        private Image avatar;
        private bool playing;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Image Avatar
        {
            get { return avatar; }
            set
            {
                if (value == null)
                {
                    avatar = Properties.Resources.DefaultAvatar;
                }
                else
                {
                    avatar = value;
                }
            }
        }
        public TcpClient Client
        {
            get { return client; }
            set { client = value; }
        }

        public bool Playing { get => playing; set => playing = value; }

        public Player()
        {
            client = null;
            name = "";
            avatar = Properties.Resources.DefaultAvatar;
        }
        public Player(TcpClient client, string name, Image avatar)
        {
            this.Client = client;
            this.Name = name;
            this.Avatar = avatar;
            playing = false;
        }

        public void OnMatch()
        {
            playing = true;
        }

        public void OutMatch()
        {
            playing = false;
        }
    }
}
