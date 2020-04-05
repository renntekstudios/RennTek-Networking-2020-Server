using RennTekNetworking.Client.Public.Managers;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Client.Packet.Receivable
{
    class r_ReceiveInstantiationPacket
    {
        public static void HandleInstantiatePlayer(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _index = _buffer.ReadInteger();
            string _networkName = _buffer.ReadString();

            _buffer.Dispose();

            r_NetworkInstantiate.InstantiatePlayer(_index, _networkName);
        }
    }
}
