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
    static class r_SendAuthenticationPacket
    {
        public static void SendRequestAuthenticationToClient(int _connectionID)
        {
            r_ByteBuffer _ByteBuffer = new r_ByteBuffer();
            _ByteBuffer.WriteInteger((int)ServerPackets.Authentication);
            _ByteBuffer.WriteInteger(_connectionID);

            r_ClientManager.SendDataTo(_connectionID, _ByteBuffer.ToArray());

            _ByteBuffer.Dispose();
        }

        public static void SendAuthenticationConfirmed(int _connectionID)
        {
            r_ByteBuffer _ByteBuffer = new r_ByteBuffer();
            _ByteBuffer.WriteInteger((int)ServerPackets.SendAuthenticationConfirmed);
            _ByteBuffer.WriteInteger(_connectionID);

            r_ClientManager.SendDataTo(_connectionID, _ByteBuffer.ToArray());

            _ByteBuffer.Dispose();
        }
    }
}
