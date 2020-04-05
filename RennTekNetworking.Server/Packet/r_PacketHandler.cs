using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Packet;
using RennTekNetworking.Server.Packet.Receivable;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet
{
    static class r_PacketHandler
    {
        public delegate void Packet(int _connectionID, byte[] _data);
        public static Dictionary<int, Packet> m_Packets = new Dictionary<int, Packet>();

        /// <summary>
        /// Client packets are what the client send's to the server
        /// </summary>
        public static void InitializePackets()
        {
            m_Packets.Add((int)ClientPackets.Authentication, r_ReceiveAuthenticationPacket.HandleAuthenticationPacket);
            m_Packets.Add((int)ClientPackets.InstantiateLocalPlayer, r_ReceiveInstantiationPacket.HandleInstantiateLocalPlayerPacket);
            m_Packets.Add((int)ClientPackets.UpdatePlayer, r_ReceivePlayerPacket.HandlePlayerUpdate);
            m_Packets.Add((int)ClientPackets.SetNetworkName, r_ReceivePlayerPacket.HandleSetNetworkName);
            m_Packets.Add((int)ClientPackets.RequestServerData, r_ReceiveRequestsPacket.HandleRequestServerData);
            m_Packets.Add((int)ClientPackets.UpdatePlayerAnimator, r_ReceivePlayerPacket.HandlePlayerAnimatorUpdate);
            m_Packets.Add((int)ClientPackets.UpdateEntity, r_ReceiveEntityUpdatePacket.HandleEntityUpdate);
        }

        public static void HandlePacketData(int _connectionID, byte[] _data)
        {
            byte[] _buffer = (byte[])_data.Clone();
            int _packetLength = 0;

            if (r_ClientManager.m_Clients[_connectionID].m_ByteBuffer == null)
                r_ClientManager.m_Clients[_connectionID].m_ByteBuffer = new r_ByteBuffer();

            r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.WriteBytes(_buffer);

            if(r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Count() == 0)
            {
                r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Clear();
                return;
            }

            if(r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Length() >= 4)
            {
                _packetLength = r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.ReadInteger(false);
                
                if(_packetLength <= 0)
                {
                    r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Clear();
                    return;
                }
            }

            while(_packetLength > 0 & _packetLength <= r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Length() -4)
            {
                if(_packetLength <= r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Length() -4)
                {
                    r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.ReadInteger();
                    _data = r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.ReadBytes(_packetLength);

                    HandlePacket(_connectionID, _data);
                }

                _packetLength = 0;

                if(r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Length() >= 4)
                {
                    _packetLength = r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.ReadInteger(false);

                    if (_packetLength <= 0)
                    {
                        r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Clear();
                        return;
                    }
                }
            }

            if(_packetLength <= 1)
                r_ClientManager.m_Clients[_connectionID].m_ByteBuffer.Clear();
        }

        private static void HandlePacket(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();

            _buffer.Dispose();

            if(m_Packets.TryGetValue(_packetID, out Packet _packet))
                _packet.Invoke(_connectionID, _data);
        }
    }
}
