using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Debug
{
    static class r_Log
    {
        public static void Log(string _message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SERVER][LOG] {_message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string _message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SERVER][WARNING] {_message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string _message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SERVER][ERROR] {_message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Command(string _message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"[SERVER][COMMAND] {_message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
