using RennTekNetworking.Client.Clients;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RennTekNetworking.Client.Packet.Sendable
{
    static class r_SendWorldUpdatePacket
    {
        public static void SendEntityUpdate(string _guid, Vector3 _position)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.UpdateEntity);

            _buffer.WriteString(_guid);

            _buffer.WriteFloat(_position.x);
            _buffer.WriteFloat(_position.y);
            _buffer.WriteFloat(_position.z);

            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }
    }
}
