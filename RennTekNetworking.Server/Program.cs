using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Server;
using System;
using System.Threading;

namespace RennTekNetworking.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            r_General.InitializeServer();

            while (true)
            {
                Console.Title = $"[CLIENTS={r_ClientManager.m_Clients.Count}]";

                DateTime _nextLoop = DateTime.Now;

                while (_nextLoop < DateTime.Now)
                {
                    r_Server.NetworkUpdate();
                    _nextLoop = _nextLoop.AddMilliseconds(r_General.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                        Thread.Sleep(_nextLoop - DateTime.Now);
                }
            }
        }
    }
}
