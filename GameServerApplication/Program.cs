using System;
using System.Threading;

namespace GameServerApplication
{
    class Program
    {
		 static Thread threadConsole;
		 static bool consoleRunning;

        static void Main(string[] args)
        {
			threadConsole = new Thread(new ThreadStart(ConsoleThread));
			threadConsole.Start();
			Database.instance.CheckPath(Database.instance.PATH_ACCOUNT);
            ServerHandleData.instance.InitMessages();
            Network.instance.ServerStart();
        }

		static void ConsoleThread()
		{
			string line;
			consoleRunning = true;

            while (consoleRunning)
			{
				line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
				{
					consoleRunning = false;
					return;
				}
				else
				{
					
				}
			}
		}
    }
}
