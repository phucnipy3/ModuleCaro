using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Linq;

namespace ClassLibraryServer
{
    public class MyServer
    {
        private const int IMAGE_BYTE_SIZE = 20000;
        private const int BYTES_SIZE = 1024;
        private TcpListener server;
        private List<TcpClient> connectedClients;
        private int connectionCount;
        private List<Player> players;
        private bool connectionsChanged;

        public List<Player> Players { get => players; set => players = value; }

        public MyServer()
        {
            connectionsChanged = false;
            connectionCount = 0;
            Players = new List<Player>();
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



        public void GetNewConnection()
        {
            Thread threadGetNewConnection = new Thread(new ThreadStart(GetConnection));
            threadGetNewConnection.IsBackground = true;
            threadGetNewConnection.Start();
        }

        private void GetConnection()
        {
            connectedClients = new List<TcpClient>();
            server = new TcpListener(GetIPAddress(), 9999);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 0);
                connectedClients.Add(client);
                AddNewPlayer(client);
            }
        }

        public void AddNewPlayer(TcpClient client)
        {
            string name = GetName(client);
            //Image avatar = GetImage(client);
            while (true)
            {
                if (isAvailableToConnect())
                {
                    connectionsChanged = true;
                    Player y = Players.Where(x => x.Name == name).SingleOrDefault();
                    if (y != null)
                    {
                        connectedClients.Remove(y.Client);
                        y.Client = client;

                    }
                    else
                    {
                        Players[connectionCount] = new Player(client, name, null);
                        connectionCount++;
                    }
                    break;
                }
            }
        }

        private string GetName(TcpClient client)
        {
            string name;

            byte[] buffer = new byte[BYTES_SIZE];
            NetworkStream stream = client.GetStream();
            stream.Read(buffer, 0, buffer.Length);
            name = Encoding.UTF8.GetString(buffer);

            return name.Trim();
        }
        private Image GetImage(TcpClient client)
        {
            Image avatar;
            byte[] buffer = new byte[IMAGE_BYTE_SIZE];
            NetworkStream stream = client.GetStream();
            stream.Read(buffer, 0, buffer.Length);
            avatar = (Bitmap)new ImageConverter().ConvertFrom(buffer);
            return avatar;
        }


        private bool isAvailableToConnect()
        {
            return connectionCount < Players.Count;
        }
        public void CreateEmptyPlayer()
        {
            if (!isAvailableToConnect())
            {
                AddNewEmptyPlayer();
            }
        }

        private void AddNewEmptyPlayer()
        {
            Players.Add(new Player());
        }

        public void HardRefreshListPlayer(FlowLayoutPanel PnlPlayerList)
        {
            PnlPlayerList.Controls.Clear();
            for (int i = 0; i < connectionCount; i++)
            {
                PnlPlayerList.Controls.Add(new UCPlayer(Players[i]));
            }
            connectionsChanged = false;
        }
        public void RefreshListPlayer(FlowLayoutPanel PnlPlayerList)
        {
            if (connectionsChanged)
            {
                PnlPlayerList.Controls.Clear();
                for (int i = 0; i < connectionCount; i++)
                {
                    PnlPlayerList.Controls.Add(new UCPlayer(Players[i]));
                }
                connectionsChanged = false;
            }
        }
        public void CheckAndRemoveConnection()
        {
            int i = 0;
            while (i < Players.Count -1)
            {
                if (!Players[i].Playing&&!isConnecting(Players[i].Client))
                {
                    
                        RemoveConnection(Players[i]);
                }
                else 
                {
                    i++;
                }
            }
        }
        private void RemoveConnection(Player player)
        {
            connectionsChanged = true;
            connectedClients.Remove(player.Client);
            Players.Remove(player);
            connectionCount--;
        }
        private bool isConnecting(TcpClient client)
        {
            bool blockingState = client.Client.Blocking;
            try
            {
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

        public void PushIP()
        {
            new ServerIP().PushIP(GetIPAddress().ToString());
        }
        public void DeleteIP()
        {
            new ServerIP().DeleteIP();
        }
    }
}
