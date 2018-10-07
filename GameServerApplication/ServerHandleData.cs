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
            throw new NotImplementedException();
        }

        void HandleNewAccount(int index, byte[] data)
        {
            throw new NotImplementedException();
        }

    }
}
