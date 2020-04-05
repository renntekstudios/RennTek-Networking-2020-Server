using RennTekNetworking.Client.Clients;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Client.Packet.Sendable
{
    class r_SendInstantiationPacket
    {
        public static void SendInstantiateLocalPlayer()
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.InstantiateLocalPlayer);

            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }
    }
}
