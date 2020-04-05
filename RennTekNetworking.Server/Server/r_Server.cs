using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Debug;
using RennTekNetworking.Server.World;
using RennTekNetworking.Server.Packet;
using System.Threading;
using RennTekNetworking.Server.Consoles;

namespace RennTekNetworking.Server.Server
{
    static class r_Server
    {
        static TcpListener m_ServerSocket = new TcpListener(IPAddress.Any, 4435);

        public static void InitializeNetwork()
        {
            r_Log.Log("Initilizing Packets");

            r_PacketHandler.InitializePackets();
            r_WorldManager.InitializeWorldObjects();

            m_ServerSocket.Start();

            m_ServerSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), null);
        }

        public static void NetworkUpdate()
        {
            r_WorldManager.UpdateWorldEnemies();
            r_General.InitializeCommands();
        }

        private static void OnClientConnected(IAsyncResult _result)
        {
            TcpClient _client = m_ServerSocket.EndAcceptTcpClient(_result);

            m_ServerSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), null);

            r_ClientManager.CreateNewConnection(_client);
        }
    }
}
