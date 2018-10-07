using System;
using System.Collections.Generic;

namespace GameServerApplication
{
    public class ServerHandleData
    {
		public static ServerHandleData instance = new ServerHandleData();
		delegate void Packet_(int index, byte[] data);
		Dictionary<int, Packet_> Packets;

		public void InitMessages()
		{
			Packets = new Dictionary<int, Packet_>();
			Packets.Add((int)AccountHanlderEnum.NewAccount, HandleNewAccount);
			Packets.Add((int)AccountHanlderEnum.Login, HandleLogin);

		}

	

		public void HandleData(int index, byte[] data)
		{
			int packetNumber;
			Packet_ packet;
			ByteBuffer buffer = new ByteBuffer();
			buffer.WriteBytes(data);
			packetNumber = buffer.ReadInteger();
			buffer = null;
			if(packetNumber == 0)
			{
				return;
			}
            if (Packets.TryGetValue(packetNumber, out packet))
			{
				packet.Invoke(index, data);
			}
		}

		void HandleLogin(int index, byte[] data)
        {
			ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packet = buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();

            if (Database.instance.AccountExist(username) == false)
            {
                //SendAlertMsg user does not exist
                return;
            }

            if (Database.instance.PasswordOK(username, password) == false)
            {
                //SendAlertMsg password does not match
                return;
            }
            Database.instance.LoadPlayer(index, username);
			ServerSendData.instance.SendInGame(index);
        }
        
        void HandleNewAccount(int index, byte[] data)
        {
			ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packet = buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();

            if (Database.instance.AccountExist(username) == true)
            {
                //SendAlertMsg
                return;
            }


            Database.instance.AddAccount(index, username, password);

			ServerSendData.instance.SendInGame(index);
        }

    }
}
