using System;
using System.Net;
using System.Net.Sockets;

namespace GameServerApplication
{
    public class Network
    {
		public const int MaximumClientConnections = 100;
		public const int DefaultPort = 5500;
		public TcpListener ServerSocket;
		public static Network instance = new Network(); //singleton pattern

		public static Client[] Clients = new Client[MaximumClientConnections];
		public static Player[] Player = new Player[MaximumClientConnections];
		public void ServerStart()
		{
			//assign default clients to client array before server starts to prevent null exception
            for (int i = 0; i < MaximumClientConnections; i++)
			{
				Clients[i] = new Client();
				Player[i] = new Player();
			}
			ServerSocket = new TcpListener(IPAddress.Any,DefaultPort );
			ServerSocket.Start();
			ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);
			Console.WriteLine("Server has successfully started");
		}

		void OnClientConnect(IAsyncResult result)
		{
			TcpClient client = ServerSocket.EndAcceptTcpClient(result);
			client.NoDelay = false;
            
			ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

            for (int i = 0; i < MaximumClientConnections; i++)
			{
                if (Clients[i].Socket == null)
				{
					Clients[i].Socket = client;
					Clients[i].Index = i;
					Clients[i].IP = client.Client.RemoteEndPoint.ToString();
					Clients[i].Start();
					Console.WriteLine("Incoming connection from " + Clients[i].IP + " Index is: " + i + 1);

					//todo Send Welcome message to client
					return;
				}
			}
		}

    }
}
