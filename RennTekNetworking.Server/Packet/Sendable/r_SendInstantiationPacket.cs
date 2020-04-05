using RennTekNetworking.Server.Clients;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet.Sendable
{
    static class r_SendInstantiationPacket
    {
        public static void SendInstantiateRemotePlayer(int _index, int _connectionID, string _networkName)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.InstantiateRemotePlayer);
            _buffer.WriteInteger(_index);
            _buffer.WriteString(_networkName);

            r_ClientManager.SendDataTo(_connectionID, _buffer.ToArray());

            _buffer.Dispose();
        }

        public static void SendInstantiateLocalPlayer(int _index, string _networkName)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.InstantiateLocalPlayer);
            _buffer.WriteInteger(_index);
            _buffer.WriteString(_networkName);

            r_ClientManager.SendDataToAll(_buffer.ToArray());

            _buffer.Dispose();
        }

    }
}
