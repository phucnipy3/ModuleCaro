using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using ClassLibraryClient;
using System.Net.NetworkInformation;

namespace AppClient
{
    public partial class Form1 : Form
    {
        MyClient myClient;


        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            myClient = new MyClient(txtName.Text, ptbAvatar.Image);

            myClient.StartLookingForServer();
            myClient.StartConnectToServer();
            myClient.StartReciveAndSend();
            myClient.StartCheckForConnection(lblStatus);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myClient.StopCheckForConnection();
        }

        private void lblUploadImage_Click(object sender, EventArgs e)
        {

            myClient.UploadImageTo(ptbAvatar);
        }

        private void lblUploadImage_MouseEnter(object sender, EventArgs e)
        {
            lblUploadImage.BackColor = Color.FromArgb(70, Color.Black);
            lblUploadImage.Text = "Upload";
        }

        private void lblUploadImage_MouseLeave(object sender, EventArgs e)
        {
            lblUploadImage.Visible = false;
        }

        private void ptbAvatar_MouseEnter(object sender, EventArgs e)
        {
            lblUploadImage.Visible = true;
        }

        private void timerCheckConnection_Tick(object sender, EventArgs e)
        {
           // Ping x = new Ping()
        }

        
        private void timerCheckServerFound_Tick(object sender, EventArgs e)
        {
            if(myClient.ServerFound)
            {
                lblServerStatus.Text = "Server found";
            }
            else
            {
                lblServerStatus.Text = "Looking for server";
            }
        }

        private void timerLookingForServer_Tick(object sender, EventArgs e)
        {
            
        }

        private void timerConnectToServer_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
