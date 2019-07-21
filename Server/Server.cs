using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private List<ServerClient> clients;
        private List<ServerClient> disconnectClients;
        private TcpListener server;
        private bool serverStarted;
        public Int32 port = 6321;

        public void Start()
        {
            clients = new List<ServerClient>(6);
            disconnectClients = new List<ServerClient>();

            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                StartListening();
                serverStarted = true;

                Console.WriteLine("Server has been started");
            }
            catch (Exception e)
            {
                Console.WriteLine("Socket error : " + e.Message);
            }
        }

        public void Run()
        {
            while (true) Update();
        }

        void Update()
        {
            if (!serverStarted) return;

            foreach (ServerClient client in clients)
            {
                if (client == null) break;
                if (!IsConnected(client.tcp)) ManageDisconnectedClient(client);
                else ManageConnectedClient(client);
            }
        }

        private bool IsConnected(TcpClient tcpClient)
        {
            try
            {
                if (tcpClient != null && tcpClient.Client != null && tcpClient.Client.Connected)
                {
                    if (tcpClient.Client.Poll(0, SelectMode.SelectRead))
                        return !(tcpClient.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        private void StartListening()
        {
            server.BeginAcceptTcpClient(AcceptTcpClient, server);
        }

        private void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            byte clientCount = (byte)clients.Count;
            clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar), clientCount));
            StartListening();

        }

        private void ManageDisconnectedClient(ServerClient client)
        {
            client.tcp.Close();
            disconnectClients.Add(client);
            clients.Remove(client);
        }

        private void ManageConnectedClient(ServerClient client)
        {
            NetworkStream s = client.tcp.GetStream();
            if (s.DataAvailable)
            {
                //TODO
            }
        }
    }

    public class ServerClient
    {
        public TcpClient tcp;

        public string clientName;

        public byte id;

        public ServerClient(TcpClient clientSocket, byte clientID)
        {
            clientName = "Guest";
            tcp = clientSocket;
            id = clientID;
        }
    }
}
