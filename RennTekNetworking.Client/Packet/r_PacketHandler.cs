using RennTekNetworking.Client.Packet;
using RennTekNetworking.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RennTekNetworking.Shared;
using RennTekNetworking.Client.Packet.Receivable;
using RennTekNetworking.Shared.Buffer;

namespace RennTekNetworking.Client.Packet
{
    static class r_PacketHandler
    {
        private static r_ByteBuffer m_Buffer;

        public delegate void Packet(byte[] _data);
        public static Dictionary<int, Packet> m_Packets = new Dictionary<int, Packet>();

        /// <summary>
        /// Server packets are what the server send's to the clients
        /// </summary>
        public static void InitializePackets()
        {
            m_Packets.Clear();

            m_Packets.Add((int)ServerPackets.Authentication, r_ReceiveAuthenticationPacket.HandleAuthentication);
            m_Packets.Add((int)ServerPackets.InstantiateLocalPlayer, r_ReceiveInstantiationPacket.HandleInstantiatePlayer);
            m_Packets.Add((int)ServerPackets.InstantiateRemotePlayer, r_ReceiveInstantiationPacket.HandleInstantiatePlayer);
            m_Packets.Add((int)ServerPackets.DestoryPlayer, r_ReceivePlayerPacket.HandleDestroyPlayer);
            m_Packets.Add((int)ServerPackets.UpdatePlayer, r_ReceivePlayerPacket.HandlePlayerUpdate);
            m_Packets.Add((int)ServerPackets.InstantiateWorldObjects, r_ReceiveWorldPacket.HandleWorldObjects);
            m_Packets.Add((int)ServerPackets.SetNetworkName, r_ReceivePlayerPacket.HandleSetNetworkName);
            m_Packets.Add((int)ServerPackets.UpdatePlayerAnimator, r_ReceivePlayerPacket.HandlePlayerAnimatorUpdate);
            m_Packets.Add((int)ServerPackets.SendAuthenticationConfirmed, r_ReceiveAuthenticationPacket.HandleAuthenticationConfirmed);
            m_Packets.Add((int)ServerPackets.UpdateEntity, r_ReceiveEntityUpdatePacket.HandleEntityUpdatePacket);
        }

        public static void HandlePacketData(byte[] _data)
        {
            byte[] _buffer = (byte[])_data.Clone();
            int _packetLength = 0;

            if (m_Buffer == null)
                m_Buffer = new r_ByteBuffer();

            m_Buffer.WriteBytes(_buffer);

            if (m_Buffer.Count() == 0)
            {
                m_Buffer.Clear();
                return;
            }

            if (m_Buffer.Length() >= 4)
            {
                _packetLength = m_Buffer.ReadInteger(false);

                if (_packetLength <= 0)
                {
                    m_Buffer.Clear();
                    return;
                }
            }

            while (_packetLength > 0 & _packetLength <= m_Buffer.Length() - 4)
            {
                if (_packetLength <= m_Buffer.Length() - 4)
                {
                    m_Buffer.ReadInteger();
                    _data = m_Buffer.ReadBytes(_packetLength);

                    HandlePacket(_data);
                }

                _packetLength = 0;

                if (m_Buffer.Length() >= 4)
                {
                    _packetLength = m_Buffer.ReadInteger(false);

                    if (_packetLength <= 0)
                    {
                        m_Buffer.Clear();
                        return;
                    }
                }
            }

            if (_packetLength <= 1)
                m_Buffer.Clear();
        }

        private static void HandlePacket(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();

            _buffer.Dispose();

            if (m_Packets.TryGetValue(_packetID, out Packet _packet))
                _packet.Invoke(_data);
        }
    }
}
