using System;
namespace GameServerApplication
{
	[Serializable]
    public class Player
    {
        //General
        public string Username;
        public string Password;
        public int Level;
        public byte Access;
        public byte FirstTime;
    }
}
