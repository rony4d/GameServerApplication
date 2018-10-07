using System;
namespace GameServerApplication
{
    public class ServerSendData
    {
		public static ServerSendData instance = new ServerSendData();

		public void SendDataTo(int index, byte[] data)
		{
			ByteBuffer buffer = new ByteBuffer();
			buffer.WriteBytes(data);
			Network.Clients[index].mystream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
			buffer = null;
		}

        public void SendInGame(int index)
		{
			ByteBuffer buffer = new ByteBuffer();

			buffer.WriteInteger((int)AccountHanlderEnum.NewAccount);
			buffer.WriteString(Network.Player[index].Username);
			buffer.WriteString(Network.Player[index].Password);
			buffer.WriteInteger(Network.Player[index].Level);
            buffer.WriteByte(Network.Player[index].Access);
            buffer.WriteByte(Network.Player[index].FirstTime);

			SendDataTo(index, buffer.ToArray());
			buffer = null;
		}
    }
}
