using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Consoles;
using RennTekNetworking.Server.Debug;
using RennTekNetworking.Server.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Server
{
    static class r_General
    {
        public static string m_Version = "0.1";

        public const int TICKS_PER_SEC = 30; // How many ticks per second
        public const float MS_PER_TICK = 1000f / TICKS_PER_SEC; // How many milliseconds per tick

        public static bool m_Running = false;

        public static void InitializeServer()
        {
            r_Server.InitializeNetwork();

            r_Log.Log("Server Initialized");
        }

        public static void InitializeCommands()
        {
          //  string _command = Console.ReadLine();
          //  if (!string.IsNullOrEmpty(_command) && _command.Contains("/"))
                //r_ConsoleCommands.InitCommand(_command);
        }

        public static bool CheckVersionMatch(string _suppliedVersion)
        {
            if(m_Version == _suppliedVersion)
                return true;
            return false;
        }
    }
}
