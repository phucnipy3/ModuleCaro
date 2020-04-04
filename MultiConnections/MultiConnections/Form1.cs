using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using ClassLibraryServer;
using DBAccessLibrary.DBHelper;

namespace MultiConnections
{
    public partial class Form1 : Form
    {
        private IPAddress ipAddress;
        private MyServer myServer;
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            myServer = new MyServer();
            ipAddress = myServer.GetIPAddress();
            myServer.GetNewConnection();
            myServer.DeleteIP();
            myServer.PushIP();
        }
        private void timerCreateEmptyPlayer_Tick(object sender, EventArgs e)
        {
            myServer.CreateEmptyPlayer();
        }
        
        private void timerRefreshListPlayer_Tick(object sender, EventArgs e)
        {
            myServer.RefreshListPlayer(PnlPlayerList);
        }

        private void timerCheckForConnection_Tick(object sender, EventArgs e)
        {
            myServer.CheckAndRemoveConnection();
        }

        private void btnNewMatch_Click(object sender, EventArgs e)
        {
            FormCreateMatch form = new FormCreateMatch(myServer.Players);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UCMatch ucMatch = new UCMatch(form.GetMatch());
                ucMatch.ButtonCloseClicked += UcMatch_ButtonCloseClicked;
                pnlContainer.Controls.Add(ucMatch);
            }
        }

        private void UcMatch_ButtonCloseClicked(object sender, EventArgs e)
        {
            pnlContainer.Controls.Remove(sender as UCMatch);
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myServer.DeleteIP();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            myServer.HardRefreshListPlayer(PnlPlayerList);
        }
    }
}
