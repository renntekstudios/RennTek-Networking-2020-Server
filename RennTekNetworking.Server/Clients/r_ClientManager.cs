using RennTekNetworking.Server.Debug;
using RennTekNetworking.Server.Packet;
using RennTekNetworking.Server.Packet.Sendable;
using RennTekNetworking.Server.Server;
using RennTekNetworking.Server.World;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Clients
{
    static class r_ClientManager
    {
        public static Dictionary<int, r_Client> m_Clients = new Dictionary<int, r_Client>();

        public static void CreateNewConnection(TcpClient _tempClient)
        {
            r_Client _newClient = new r_Client
            {
                m_Socket = _tempClient,
                m_ConnectionID = ((IPEndPoint)_tempClient.Client.RemoteEndPoint).Port
            };
            _newClient.Start();

            m_Clients.Add(_newClient.m_ConnectionID, _newClient);

            r_SendAuthenticationPacket.SendRequestAuthenticationToClient(_newClient.m_ConnectionID);
        }

        /// <summary>
        /// Everyone that's already spawned
        /// </summary>
        /// <param name="_connectionID"></param>
        public static void InstantiateRemotePlayers(int _connectionID)
        {
            foreach (var _player in m_Clients)
                if (_player.Value.m_SpawnedInGame)
                    if (_player.Key != _connectionID)
                        r_SendInstantiationPacket.SendInstantiateRemotePlayer(_player.Key, _connectionID, _player.Value.m_NetworkName);
        }

        /// <summary>
        /// called when client requests to spawn
        /// </summary>
        /// <param name="_connectionID"></param>
        public static void InstantiateLocalPlayer(int _connectionID)
        {
            //spawn us
            if (m_Clients.ContainsKey(_connectionID))
            {
                r_SendInstantiationPacket.SendInstantiateLocalPlayer(_connectionID, m_Clients[_connectionID].m_NetworkName);

                m_Clients[_connectionID].m_SpawnedInGame = true;
            }
        }

        /// <summary>
        /// called when the client has disconnected
        /// </summary>
        /// <param name="_connectionID"></param>
        public static void DestoryNetworkPlayer(int _connectionID)
        {
            r_SendPlayerPacket.SendDestoryNetworkPlayer(_connectionID);
    
            m_Clients.Remove(_connectionID);
        }

        public static void UpdateNetworkPlayer(int _connectionID,r_Vector3 _position, r_Quaternion _rotation)
        {
            r_SendPlayerPacket.HandlePlayerUpdate(_connectionID, _position, _rotation);
        }

        public static void UpdatePlayerAnimator(int _connectionID, string _paramaterName, int _value)
        {
            r_SendPlayerPacket.HandlePlayerAnimatorUpdate(_connectionID, _paramaterName, _value);
        }
        public static void UpdatePlayerAnimator(int _connectionID, string _paramaterName, bool _value)
        {
            r_SendPlayerPacket.HandlePlayerAnimatorUpdate(_connectionID, _paramaterName, _value);
        }
        public static void UpdatePlayerAnimator(int _connectionID, string _paramaterName, float _value)
        {
            r_SendPlayerPacket.HandlePlayerAnimatorUpdate(_connectionID, _paramaterName, _value);
        }

        public static void InstantiateWorldObjects(int _connectionID)
        {
            if (r_WorldManager.m_WorldObjects.Count == 0)
                return;

            foreach(var _objects in r_WorldManager.m_WorldObjects)
            {
                r_SendWorldPacket.SendWorldObjectsToClient(_connectionID, _objects.Value.m_Prefab,
                    _objects.Value.m_GUID,
                    _objects.Value.m_Position,
                    _objects.Value.m_Rotation,
                    _objects.Value.m_EntityType);
            }
        }

        public static void UpdateNetworkName(int _connectionID)
        {
            r_SendPlayerPacket.SendNetworkName(_connectionID, m_Clients[_connectionID].m_NetworkName);
        }

        public static void SendDataTo(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((_data.GetUpperBound(0) - _data.GetLowerBound(0)) + 1);
            _buffer.WriteBytes(_data);

            m_Clients[_connectionID].m_Stream.BeginWrite(_buffer.ToArray(), 0, _buffer.ToArray().Length, null, null);

            _buffer.Dispose();
        }

        public static void SendDataToAllExcept(int _exceptConnectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((_data.GetUpperBound(0) - _data.GetLowerBound(0)) + 1);
            _buffer.WriteBytes(_data);

            if (m_Clients.Count == 0)
            {
                _buffer.Dispose();
                return;
            }

            foreach (var _client in m_Clients)
                if(_client.Key != _exceptConnectionID)
                    _client.Value.m_Stream.BeginWrite(_buffer.ToArray(), 0, _buffer.ToArray().Length, null, null);

            _buffer.Dispose();
        }

        public static void SendDataToAll(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((_data.GetUpperBound(0) - _data.GetLowerBound(0)) + 1);
            _buffer.WriteBytes(_data);

            if(m_Clients.Count == 0)
            {
                _buffer.Dispose();
                return;
            }

            if (m_Clients.Count > 0)
            {
                foreach (var _client in m_Clients)
                    _client.Value.m_Stream.BeginWrite(_buffer.ToArray(), 0, _buffer.ToArray().Length, null, null);
            }

            _buffer.Dispose();
        }
    }
}
