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
using System.Windows.Media;

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
        private const string FIRST_TURN = "playfirst";
        private const string SECOND_TURN = "playsecond";
        private const int IMAGE_BYTE_SIZE = 20000;


        private TcpClient player;
        private string oldData = "";
        private bool serverConnected = false;
        private string ipString;
        private bool serverFound = false;
        private bool networkAvailable = true;
        private string username;
        private string password;
        private MoveTracker moveTracker;
        private Thread threadReceiveAndSend;
        private Thread threadLookingForServer;
        private Thread threadConnectToServer;
        private Thread threadCheckForConnection;

        private bool isConnected = false;

        private string serverIPAddress;

        public bool ServerFound { get => serverFound; set => serverFound = value; }

        public MyClient(string username,string password, string serverIPAddress)
        {
            player = new TcpClient();
            this.username = username;
            this.password = password;
            this.serverIPAddress = serverIPAddress;
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            moveTracker = new MoveTracker();
            StartThread(ReceiveAndSend);
            StartThread(ConnectToServer);
            StartThread(CheckForConnection);
        }

        public delegate void ConsecutiveFuction();

        public void StartThread(ConsecutiveFuction function)
        {
            Thread thread = new Thread(new ThreadStart(function));
            thread.IsBackground = true;
            thread.Start();
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
            int counter = 4;
            bool ignoreCounter = false;
            while (true)
            {
                if(!ignoreCounter)
                {
                    counter--;
                    if (counter < 0)
                    {
                        OnTakeTooMuchTimeToConnect(EventArgs.Empty);
                        break;
                    }
                }
                Thread.Sleep(1000);
                if (!serverConnected)
                {
                    try
                    {
                        IPAddress ip = GetIPAddress();
                        IPEndPoint ipe = new IPEndPoint(ip, PORT_NUMBER);
                        player = new TcpClient();
                        player.Connect(ipe);
                        if (!SendLoginInfomation())
                            break;
                        serverConnected = true;
                        ignoreCounter = true;
                    }
                    catch (Exception e )
                    {
                        continue;
                    }
                }
            }

        }

        private IPAddress GetIPAddress()
        {
            return IPAddress.Parse(serverIPAddress);
        }
        public bool SendLoginInfomation()
        {
            byte[] nameBuffer = new byte[SIZE_OF_BYTE];
            byte[] imgBuffer = new byte[IMAGE_BYTE_SIZE];
            nameBuffer = Encoding.UTF8.GetBytes($"[username]{username}[password]{password}[end]");

            NetworkStream stream = player.GetStream();
            stream.Write(nameBuffer, 0, nameBuffer.Length);
            byte[] buffer = new byte[SIZE_OF_BYTE];
            stream.Read(buffer, 0, buffer.Length);
            string loginMessage = Encoding.UTF8.GetString(buffer);

            LoginMessageReceivedEventArgs args = new LoginMessageReceivedEventArgs();
            if (loginMessage.Substring(0,loginMessage.IndexOf("[end]")).Equals("valid"))
            {
                args.IsValidLogin = true;
            }
            else
            {
                args.IsValidLogin = false;
            }
            OnLoginMessageReceived(args);
            return args.IsValidLogin;
                
        }

        public void ReceiveData()
        {
            string data = TryReadFromStream();
            TryWriteFile(data);
            moveTracker.AddOpponentMove(data);
            if(data.Equals(FIRST_TURN) || data.Equals(SECOND_TURN))
            {
                moveTracker.Reset();
                if (data.Equals(SECOND_TURN))
                    ReceiveData();
            }
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
                do
                {
                    data = TryReadFile(LINK_OUTPUT);
                }
                while (data == null || data.Equals(oldData));
                if (moveTracker.TryAddAllyMove(data))
                    break;
                else
                {
                    TryWriteFile("moveexist");
                }
            }
            while (true);
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
        public void CheckForConnection()
        {
            while (true)
            {
                if(isConnecting() != isConnected)
                {
                    isConnected = !isConnected;
                    ConnectionChangedEventArgs args = new ConnectionChangedEventArgs();
                    args.isConnected = isConnected;
                    OnConnectionChanged(args);
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
        protected virtual void OnTakeTooMuchTimeToConnect(EventArgs e)
        {
            EventHandler handler = TakeTooMuchTimeToConnect;
            if(handler!=null)
            {
                handler(this, e);
            }
        }
        public EventHandler TakeTooMuchTimeToConnect;
        protected virtual void OnLoginMessageReceived(LoginMessageReceivedEventArgs e)
        {
            EventHandler<LoginMessageReceivedEventArgs> handler = LoginMessageReceived;
            if (handler != null)
            {
                handler(this, e);
                
            }
        }
        protected virtual void OnConnectionChanged(ConnectionChangedEventArgs e)
        {
            EventHandler<ConnectionChangedEventArgs> handler = ConnectionChanged;
            if (handler != null)
            {
                handler(this, e);

            }
        }
        public EventHandler<LoginMessageReceivedEventArgs> LoginMessageReceived;

        public EventHandler<ConnectionChangedEventArgs> ConnectionChanged;
    }
    public class LoginMessageReceivedEventArgs : EventArgs
    {
        public bool IsValidLogin { get; set; }
    }
    public class ConnectionChangedEventArgs : EventArgs
    {
        public bool isConnected { get; set; }
    }

}
