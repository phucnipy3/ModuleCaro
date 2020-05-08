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
using System.Diagnostics;

namespace ClassLibraryClient
{
    public class MyClient : IDisposable
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
        private const int START_FIRST_NO_RULE = -1;
        private const int START_SECOND_NO_RULE = -2;
        private const int START_FIRST_RULE = -3;
        private const int START_SECOND_RULE = -4;
        private const int WIN = -5;
        private const int LOSE = -6;
        private const int OUT_RANGE = -7;
        private const int MOVE_EXIST = -8;
        private const int OTHER = -9;

        private TcpClient player;
        private string oldData = "";
        private bool networkAvailable = true;
        private string username;
        private string password;
        private MoveTracker moveTracker;

        private bool isConnected = false;

        private string serverIPAddress;

        private Process botProcess;

        private string botPath;
        private bool botPathDefined = false;
        private bool botStarted = false;

        private List<Thread> runningThreads;

        public MyClient(string username, string password, string serverIPAddress)
        {
            player = new TcpClient();
            this.username = username;
            this.password = password;
            this.serverIPAddress = serverIPAddress;
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            moveTracker = new MoveTracker();
            runningThreads = new List<Thread>();
            runningThreads.Add(StartThread(ReceiveAndSend));
            runningThreads.Add(StartThread(ConnectToServer));
            runningThreads.Add(StartThread(CheckForConnection));
            botProcess = new Process();
        }
        ~MyClient()
        {
            StopBot();
        }
        public void StopBot()
        {
            if (botStarted)
            {
                botProcess.Kill();
                botStarted = false;
            }
        }
        public void ChangeBot(string botPath)
        {
            this.botPath = botPath;
            botPathDefined = true;
            StopBot();
        }
        public void StartBot()
        {
            if(!botStarted)
            {
                if (string.IsNullOrEmpty(botPath))
                    throw new NullReferenceException("Bot path is not set!");
                botProcess = new Process();
                botProcess.StartInfo.FileName = botPath;
                botProcess.StartInfo.UseShellExecute = false;
                botProcess.StartInfo.RedirectStandardInput = true;
                botProcess.StartInfo.RedirectStandardOutput = true;
                botProcess.StartInfo.CreateNoWindow = true;
                botStarted = botProcess.Start();
            }    
        }

        public delegate void ConsecutiveFuction();

        public Thread StartThread(ConsecutiveFuction function)
        {
            Thread thread = new Thread(new ThreadStart(function));
            thread.IsBackground = true;
            thread.Start();
            return thread;
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
                if (botPathDefined && isConnecting())
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
            return MyClient.GetState(player) == TcpState.Established;
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
                if (!isConnected)
                {
                    try
                    {
                        IPAddress ip = GetIPAddress();
                        IPEndPoint ipe = new IPEndPoint(ip, PORT_NUMBER);
                        player = new TcpClient();
                        player.Connect(ipe);
                        if (!SendLoginInfomation())
                            break;
                        isConnected = true;
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
            string opponentMove = data;

            if (opponentMove[0] == '-')
            {
                int signal = int.Parse(opponentMove.Substring(0, 2));
                if (signal == LOSE)
                {
                    opponentMove = opponentMove.Substring(3);
                    moveTracker.AddOpponentMove(opponentMove);
                }
                if (signal >= START_SECOND_RULE)
                {
                    StopBot();
                    StartBot();
                    moveTracker.Reset();
                }
                if(signal != START_FIRST_NO_RULE && signal != START_FIRST_RULE)
                {
                    WriteToConsole(data);

                    ReceiveData();
                    return;
                }    
            }
            else
            {
                moveTracker.AddOpponentMove(opponentMove);
            }
            WriteToConsole(data);
            
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
            
        }

        public bool isEmpty(byte[] data)
        {
            foreach (byte b in data)

                if (b != 0)
                    return false;
            return true;
        }
        public void WriteToConsole(string data)
        {
            //if(!botProcess.StandardOutput.EndOfStream)
            //    botProcess.StandardOutput.ReadToEnd();
            if(data[0] == '-')
            {
                int signal = int.Parse(data.Substring(0, 2));
                botProcess.StandardInput.WriteLine(signal);
                if (signal == LOSE)
                {
                    botProcess.StandardInput.WriteLine(data.Split(',')[1]);
                    botProcess.StandardInput.WriteLine(data.Split(',')[2]);
                }    
            }
            else
            {
                botProcess.StandardInput.WriteLine(data.Split(',')[0]);
                botProcess.StandardInput.WriteLine(data.Split(',')[1]);
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
            //TODO: hk lq j oldata nưuax

            //string data;
            //do
            //{
            //    do
            //    {
            //        data = TryReadConsole();
            //    }
            //    while (data == null || data.Equals(oldData));
            //    //TODO: xử lí lỗi data
            //    if (moveTracker.TryAddAllyMove(data))
            //        break;
            //    else
            //    {
            //        //TODO: xử lí lỗi
            //        WriteToConsole("moveexist");
            //    }
            //}
            //while (true);
            //oldData = data;

            while(true)
            {
                string data = TryReadConsole();
                int row, col;

                if (int.TryParse(data.Split(',')[0], out row) &&
                    int.TryParse(data.Split(',')[1], out col))
                {
                    if (row >= 0 && row < 20 && col >= 0 && col < 20)
                    {
                        if (moveTracker.TryAddAllyMove(data))
                        {
                            TryWriteToStream(data + "[end]");
                            return;
                        }
                        else
                            WriteToConsole(MOVE_EXIST.ToString());
                    }
                    else
                    {
                        WriteToConsole(OUT_RANGE.ToString());
                    }
                }
                else
                {
                    WriteToConsole(OTHER.ToString());
                }
            }    
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

        public string TryReadConsole()
        {
            string result = botProcess.StandardOutput.ReadLine() + ',' + botProcess.StandardOutput.ReadLine();
            return result;
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
        public void Dispose()
        {
            foreach(var thread in runningThreads)
            {
                thread.Abort();
            }
            StopBot();
            if(isConnecting())
            {
                player.GetStream().Close();
                player.Close();
            }
        }
        public static TcpState GetState(TcpClient tcpClient)
        {
            var foo = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().SingleOrDefault(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint));
            return foo != null ? foo.State : TcpState.Unknown;
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
