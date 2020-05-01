using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Linq;
using DBAccessLibrary.DBHelper;
using System.Windows;
using System.Net.NetworkInformation;

namespace ClassLibraryServer
{
    public class MyServer
    {
        private const int IMAGE_BYTE_SIZE = 20000;
        private const int BYTES_SIZE = 1024;
        private TcpListener server;
        private List<Player> players;
        private List<Thread> runningThreads;

        public List<Player> Players { get => players; set => players = value; }

        public MyServer()
        {
            Players = new List<Player>();
            runningThreads = new List<Thread>();
            runningThreads.Add(StartThread(CheckAndRemoveConnection));
            runningThreads.Add(StartThread(GetConnection));
        }
        public delegate void ConsecutiveFuction();

        public Thread StartThread(ConsecutiveFuction function)
        {
            Thread thread = new Thread(new ThreadStart(function));
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }
        public IPAddress GetIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    return ip;
                }
            }
            return null;
        }

        
        private void GetConnection()
        {
            server = new TcpListener(GetIPAddress(), 9999);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 0);
                AddNewPlayer(client);
            }
        }
        public void AddNewPlayer(TcpClient client)
        {
            string loginString = GetLoginString(client);
            string name = loginString.Substring(0, loginString.IndexOf("[password]")).Substring(loginString.IndexOf("[username]")+10);
            if (!Helper.Login(loginString))
            {
                SendMesssage(client, "invalid[end]");
            }
            else
            {
                SendMesssage(client, "valid[end]");

                Player y = Players.Where(x => x.Name == name).SingleOrDefault();
                if (y != null)
                {
                    y.Client = client;
                }
                else
                {
                    Players.Add(new Player(client, name));
                }

                OnConnectionsChanged(EventArgs.Empty);
            }
        }

        private string GetLoginString(TcpClient client)
        {
            string login;

            byte[] buffer = new byte[BYTES_SIZE];
            NetworkStream stream = client.GetStream();
            stream.Read(buffer, 0, buffer.Length);
            login = Encoding.UTF8.GetString(buffer);

            return login.Trim();
        }
        private void SendMesssage(TcpClient client, string loginMessage)
        {
            byte[] buffer = new byte[BYTES_SIZE];
            buffer = Encoding.ASCII.GetBytes(loginMessage);
            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);
        }
        
        

        public void CheckAndRemoveConnection()
        {
            while(true)
            {
                int i = 0;
                while (i < Players.Count)
                {
                    if (!Players[i].Playing && MyServer.GetState(Players[i].Client) != TcpState.Established)
                    {
                        RemoveConnection(Players[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
                Thread.Sleep(500);
            }
            
        }
        private void RemoveConnection(Player player)
        {
            OnConnectionsChanged(EventArgs.Empty);
            Players.Remove(player);
        }
        private bool isConnecting(TcpClient client)
        {
            bool blockingState = client.Client.Blocking;
            try
            {
                if (!client.Client.Connected)
                    return false;
                byte[] tmp = new byte[1];

                client.Client.Blocking = false;
                client.Client.Send(tmp, 0, 0);
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
                client.Client.Blocking = blockingState;
            }

        }
        public static TcpState GetState(TcpClient tcpClient)
        {
            var foo = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().FirstOrDefault(x => x.RemoteEndPoint.Equals(tcpClient.Client.RemoteEndPoint));
            return foo != null ? foo.State : TcpState.Unknown;
        }

        public EventHandler ConnectionsChanged;
        protected virtual void OnConnectionsChanged(EventArgs e)
        {
            EventHandler handler = ConnectionsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
