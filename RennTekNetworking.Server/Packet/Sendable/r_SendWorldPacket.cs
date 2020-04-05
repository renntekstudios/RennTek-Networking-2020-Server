using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Debug;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet.Sendable
{
    static class r_SendWorldPacket
    {
        public static void SendWorldObjectsToClient(int _connectionID, string _prefab, string _guid, r_Vector3 _position, r_Quaternion _rotation, EntityType _entityType)
        {
            r_ByteBuffer _ByteBuffer = new r_ByteBuffer();
            _ByteBuffer.WriteInteger((int)ServerPackets.InstantiateWorldObjects);

            _ByteBuffer.WriteString(_prefab);
            _ByteBuffer.WriteString(_guid);

            _ByteBuffer.WriteFloat(_position.x);
            _ByteBuffer.WriteFloat(_position.y);
            _ByteBuffer.WriteFloat(_position.z);

            _ByteBuffer.WriteFloat(_rotation.x);
            _ByteBuffer.WriteFloat(_rotation.y);
            _ByteBuffer.WriteFloat(_rotation.z);
            _ByteBuffer.WriteFloat(_rotation.w);

            _ByteBuffer.WriteInteger((int)_entityType);

            r_ClientManager.SendDataTo(_connectionID, _ByteBuffer.ToArray());

            _ByteBuffer.Dispose();
        }

        public static void SendWorldObjectUpdateToClient(string _guid, r_Vector3 _position, r_Quaternion _rotation, EntityType _entityType)
        {
            r_ByteBuffer _ByteBuffer = new r_ByteBuffer();
            _ByteBuffer.WriteInteger((int)ServerPackets.UpdateEntity);

            _ByteBuffer.WriteString(_guid);

            _ByteBuffer.WriteFloat(_position.x);
            _ByteBuffer.WriteFloat(_position.y);
            _ByteBuffer.WriteFloat(_position.z);

            _ByteBuffer.WriteFloat(_rotation.x);
            _ByteBuffer.WriteFloat(_rotation.y);
            _ByteBuffer.WriteFloat(_rotation.z);
            _ByteBuffer.WriteFloat(_rotation.w);

            _ByteBuffer.WriteInteger((int)_entityType);

            r_ClientManager.SendDataToAll(_ByteBuffer.ToArray());

            _ByteBuffer.Dispose();
        }
    }
}
