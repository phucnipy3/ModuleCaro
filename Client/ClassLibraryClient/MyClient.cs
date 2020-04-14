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
using System.Net.NetworkInformation;

namespace ClassLibraryClient
{
    public class MyClient
    {
        private const int PORT_NUMBER = 9999;
        private const int SIZE_OF_BYTE = 1024;

        private const int SIZE_OF_BOARD = 10;
        private const int SIZE_OF_AVT = 69;
        private const string LINK_OUTPUT = "Output.txt";
        private const string LINK_INPUT = "Input.txt";
        private const string FISRT_TURN = "-1,-1,";
        private const string SECOND_TURN = "-2,-2,";
        private const int IMAGE_BYTE_SIZE = 20000;


        private TcpClient player;
        private string oldData = "";
        private bool serverConnected = false;
        private string ipString;
        private bool serverFound = false;
        private bool networkAvailable = true;
        private string name;
        private Image img;
        private Thread threadReceiveAndSend;
        private Thread threadLookingForServer;
        private Thread threadConnectToServer;
        private Thread threadCheckForConnection;

        public bool ServerFound { get => serverFound; set => serverFound = value; }

        public MyClient(string name, Image img)
        {
            player = new TcpClient();
            this.name = name;
            this.img = img;
            ClearInputOutput();
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            networkAvailable = e.IsAvailable;
        }

        public void ClearInputOutput()
        {
            StreamWriter writer1 = new StreamWriter(LINK_INPUT);
            StreamWriter writer2 = new StreamWriter(LINK_OUTPUT);
            writer1.Write("");
            writer2.Write("");
            writer1.Close();
            writer2.Close();
        }
        public void StartReceiveAndSend()
        {
            threadReceiveAndSend = new Thread(new ThreadStart(ReceiveAndSend));
            threadReceiveAndSend.IsBackground = true;
            threadReceiveAndSend.Start();
        }
        public void StartLookingForServer()
        {
            threadLookingForServer = new Thread(new ThreadStart(LookingForServer));
            threadLookingForServer.IsBackground = true;
            threadLookingForServer.Start();
        }

        public void StartConnectToServer()
        {
            threadConnectToServer = new Thread(new ThreadStart(ConnectToServer));
            threadConnectToServer.IsBackground = true;
            threadConnectToServer.Start();
        }

        public void StartCheckForConnection(Label lbl)
        {
            threadCheckForConnection = new Thread(CheckForConnection);
            threadCheckForConnection.IsBackground = true;
            threadCheckForConnection.Start(lbl);
        }
        
        public void LookingForServer()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (!ServerFound)
                {
                    ServerIP serverIP = new ServerIP();
                    if (serverIP.Accessable())
                    {
                        string str = serverIP.GetIP();
                        if (str != "")
                        {
                            ServerFound = true;
                            ipString = str;
                        }
                    }
                }
            }
        }
        public void ReceiveAndSend()
        {
            while (true)
            {
                if (serverConnected && isConnecting())
                {
                    try
                    {
                        ReceiveData();
                        SendData();
                    }
                    catch(Exception e)
                    {
                        //MessageBox.Show(e.Message);
                        Thread.Sleep(1000);
                        continue;
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private bool isConnecting()
        {
            if (!networkAvailable)
                return false;
            bool blockingState = player.Client.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                player.Client.Blocking = false;
                player.Client.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                player.Client.Blocking = blockingState;
            }
        }
        public void ConnectToServer()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (ServerFound && !serverConnected)
                {
                    try
                    {
                        IPAddress ip = GetIPAddress();
                        IPEndPoint ipe = new IPEndPoint(ip, PORT_NUMBER);
                        player = new TcpClient();
                        player.Connect(ipe);
                        SendNameAndAvatar();
                        serverConnected = true;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

        }

        private IPAddress GetIPAddress()
        {
            return IPAddress.Parse(ipString);
        }
        public void SendNameAndAvatar()
        {
            byte[] nameBuffer = new byte[SIZE_OF_BYTE];
            byte[] imgBuffer = new byte[IMAGE_BYTE_SIZE];
            nameBuffer = Encoding.UTF8.GetBytes($"[username]{name}[password]{"123456"}[end]");
            imgBuffer = (byte[])new ImageConverter().ConvertTo(img, typeof(byte[]));

            NetworkStream stream = player.GetStream();
            stream.Write(nameBuffer, 0, nameBuffer.Length);
            byte[] buffer = new byte[SIZE_OF_BYTE];
            stream.Read(buffer, 0, buffer.Length);
            string loginMessage = Encoding.UTF8.GetString(buffer);
            if (loginMessage.Trim().Equals("valid"))
                MessageBox.Show("login valid");
        }

        public void ReceiveData()
        {
            string data = TryReadFromStream();
            TryWriteFile(data);
            if (data.Substring(0, 6).Equals(SECOND_TURN))
                ReceiveData();
        }

        public string TryReadFromStream()
        {
            while(true)
            {
                try
                {
                    return ReadFromStream();
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }    
        }
        public string ReadFromStream()
        {
            byte[] dataTemp = new byte[SIZE_OF_BYTE];
            NetworkStream stream = player.GetStream();
            stream.ReadTimeout = 10000;
            stream.Read(dataTemp, 0, dataTemp.Length);
            string data = Encoding.ASCII.GetString(dataTemp);
            return data.Substring(0, data.IndexOf("[end]"));
;
        }

        public bool isEmpty(byte[] data)
        {
            foreach (byte b in data)

                if (b != 0)
                    return false;
            return true;
        }
        public void TryWriteFile(string data)
        {
            while (true)
            {
                try
                {
                    WriteFile(data);
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        public void WriteFile(string data)
        {
            using (StreamWriter sw = new StreamWriter(LINK_INPUT))
            {
                sw.Write(data);
            }
        }

        public void SendData()
        {
            string data;
            do
            {
                data = TryReadFile(LINK_OUTPUT);
            }
            while (data == null || data.Equals(oldData));
            oldData = data;

            TryWriteToStream(data + "[end]");
        }

        public void TryWriteToStream(string data)
        {
            while(true)
            {
                try
                {
                    WriteToStream(data);
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }    
        }
        public void WriteToStream(string data)
        {
            byte[] dataTemp = new byte[SIZE_OF_BYTE];
            dataTemp = Encoding.ASCII.GetBytes(data);
            NetworkStream stream = player.GetStream();
            stream.WriteTimeout = 10000;
            stream.Write(dataTemp, 0, dataTemp.Length);
        }

        public string TryReadFile(string filename)
        {
            while (true)
            {
                try
                {
                    return ReadFile(filename);
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        public string ReadFile(string filename)
        {
            using (StreamReader read = new StreamReader(filename))
            { 
                string newData = read.ReadLine();
                return newData;
            }
        }
        public void CheckForConnection(object sender)
        {
            Label lblStatus = sender as Label;
            while (true)
            {
                if (serverConnected)
                {
                    if (isConnecting())
                    {
                        lblStatus.Text = "Connected";
                    }
                    else
                    {
                        lblStatus.Text = "No connection";
                        serverConnected = false;
                        ServerFound = false;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        public void StopCheckForConnection()
        {
            threadCheckForConnection.Abort();
        }

        public void UploadImageTo(PictureBox ptb)
        {
            OpenFileDialog openDiaglog = new OpenFileDialog();
            string imgURL;
            if (openDiaglog.ShowDialog() == DialogResult.OK)
            {
                imgURL = openDiaglog.FileName;
                Bitmap bmp = new Bitmap(Image.FromFile(imgURL), new Size(SIZE_OF_AVT, SIZE_OF_AVT));
                ptb.Image = bmp;
            }
        }
    }
}
