using RennTekNetworking.Client.Public.Managers;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RennTekNetworking.Client.Packet.Receivable
{
    class r_ReceiveWorldPacket
    {
        public static void HandleWorldObjects(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();

            string _prefab = _buffer.ReadString();
            string _guid = _buffer.ReadString();

            Vector3 _position = new Vector3(_buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat());
            Quaternion _rotation = new Quaternion(_buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat());

            EntityType _entityType = (EntityType)_buffer.ReadInteger();

            _buffer.Dispose();

            r_NetworkInstantiate.InstantiateWorldObject(_prefab, _guid, _position, _rotation, _entityType);
        }

    }
}
