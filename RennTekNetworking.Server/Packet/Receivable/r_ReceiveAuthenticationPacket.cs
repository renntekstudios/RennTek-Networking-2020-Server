using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Packet.Sendable;
using RennTekNetworking.Server.Server;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet.Receivable
{
    static class r_ReceiveAuthenticationPacket
    {
        public static void HandleAuthenticationPacket(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            string _clientVersion = _buffer.ReadString();

            _buffer.Dispose();

            if (r_General.CheckVersionMatch(_clientVersion))
                r_SendAuthenticationPacket.SendAuthenticationConfirmed(_connectionID);
            else r_ClientManager.m_Clients[_connectionID].CloseConnection(false);
        }

    }
}
