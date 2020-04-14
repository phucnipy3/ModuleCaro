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
        private bool playing;

        public string Name
        {
            get { return name; }
            set { name = value; }
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
        }
        public Player(TcpClient client, string name)
        {
            this.Client = client;
            this.Name = name;
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
