using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameServerApplication
{
    public class Database
    {
		public static Database instance = new Database();

        public string PATH_DATA = "data/";
        public string PATH_ACCOUNT = "accounts/";
        public string PATH_ITEMS = "items/";
        public string PATH_MONSTERS = "monsters/";

        public string FILE_EXTENSION = ".bin";
        public string FILE_ITEMS = "item";
        public string FILE_MONSTERS = "monster";

        public void CheckPath(string folder)
        {
            Console.WriteLine("Checking: .../" + PATH_DATA + folder);

            if (!Directory.Exists(PATH_DATA + folder))
                Directory.CreateDirectory(PATH_DATA + folder);
        }

        #region"Accounts"
        public bool PasswordOK(string name, string password)
        {
            Stream stream = File.Open(PATH_DATA + PATH_ACCOUNT + "/" + name + FILE_EXTENSION, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            var player = (Player)bf.Deserialize(stream);

            if (player.Password == password)
            {
                stream.Close();
                return true;
            }
            else
            {
                stream.Close();
                return false;
            }
        }

        public bool AccountExist(string name)
        {
            if (!File.Exists(PATH_DATA + PATH_ACCOUNT + name + FILE_EXTENSION))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LoadPlayer(int index, string username)
        {
            Stream stream = File.Open(PATH_DATA + PATH_ACCOUNT + "/" + username + FILE_EXTENSION, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            
            Network.Player[index] = null;
            Network.Player[index] = (Player)bf.Deserialize(stream);
            stream.Close();
        }

        public void SavePlayer(int index)
        {
            Stream stream = File.Open(PATH_DATA + PATH_ACCOUNT + "/" + Network.Player[index].Username + FILE_EXTENSION, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, Network.Player[index]);
            stream.Close();

        }

        public void AddAccount(int index, string username, string password)
        {
            Network.Player[index].Username = username;
            Network.Player[index].Password = password;
            Network.Player[index].Level = 1;
            Network.Player[index].FirstTime = 1;
            Network.Player[index].Access = 1;


            SavePlayer(index);
            Console.WriteLine("Account:' " + username + "' has been created.");
        }
        #endregion
    }
}
