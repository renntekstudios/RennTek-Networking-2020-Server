using RennTekNetworking.Server.Debug;
using RennTekNetworking.Server.World;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet.Receivable
{
    static class r_ReceiveEntityUpdatePacket
    {
        public static void HandleEntityUpdate(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            string _guid = _buffer.ReadString();

            r_Vector3 _position = new r_Vector3(_buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat());

            _buffer.Dispose();

            r_WorldManager.UpdateEntityPosition(_guid, _position);
        }
    }
}
