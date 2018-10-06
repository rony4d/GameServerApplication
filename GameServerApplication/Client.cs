using System;
using System.Net.Sockets;

namespace GameServerApplication
{
    public class Client
    {
		public int Index;
		public string IP;
		public TcpClient Socket; // where to send the packets
		public NetworkStream mystream; // network to carry the packets
		byte[] readBuffer;

		public void Start()
        {
			Socket.SendBufferSize = 4096;
			Socket.ReceiveBufferSize = 4096;
			mystream = Socket.GetStream(); // get all the data
			Array.Resize(ref readBuffer, Socket.ReceiveBufferSize);
			mystream.BeginRead(readBuffer, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
        }

		void OnReceiveData(IAsyncResult result)
		{
			try
			{
				int readBytes = mystream.EndRead(result);
                if (Socket == null)
				{
					return;
				}
                if (readBytes <= 0)
				{
					CloseConnection();
					return;
				}
				byte[] newBytes = null;
				Array.Resize(ref newBytes, readBytes);
				Buffer.BlockCopy(readBuffer, 0, newBytes, 0, readBytes);

                //Handle Data
                if (Socket == null) // maybe user disconnected
				{
					return;
				}
				mystream.BeginRead(readBuffer, 0, Socket.ReceiveBufferSize, OnReceiveData, null);

			}
			catch (Exception ex)
			{
				CloseConnection();
				return;
			}
		}


        /// <summary>
        /// Closes the connection and opens the slot so another player can connect to that slot
        /// </summary>
		private void CloseConnection()
		{
			Socket.Close();
			Socket = null;
			Console.WriteLine("Player " + IP + " has disconnected");
		}
	}


}
