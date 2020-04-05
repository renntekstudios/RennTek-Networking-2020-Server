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
    static class r_SendPlayerPacket
    {
        public static void SendDestoryNetworkPlayer(int _index)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.DestoryPlayer);
            _buffer.WriteInteger(_index);

            r_ClientManager.SendDataToAllExcept(_index, _buffer.ToArray());

            _buffer.Dispose();
        }

        /// <summary>
        /// Handles the player's position and rotation update
        /// </summary>
        public static void HandlePlayerUpdate(int _index, r_Vector3 _position, r_Quaternion _rotation)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.UpdatePlayer);
            _buffer.WriteInteger(_index);

            _buffer.WriteFloat(_position.x);
            _buffer.WriteFloat(_position.y);
            _buffer.WriteFloat(_position.z);

            _buffer.WriteFloat(_rotation.x);
            _buffer.WriteFloat(_rotation.y);
            _buffer.WriteFloat(_rotation.z);
            _buffer.WriteFloat(_rotation.w);

            r_ClientManager.SendDataToAll(_buffer.ToArray());

            _buffer.Dispose();
        }

        public static void HandlePlayerAnimatorUpdate(int _index, string _paramaterName, int _value)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.UpdatePlayerAnimator);
            _buffer.WriteInteger(_index);

            //send to clients
            _buffer.WriteInteger((int)AnimatorValueType.Int);
            _buffer.WriteString(_paramaterName);
            _buffer.WriteInteger(_value);

            r_ClientManager.SendDataToAll(_buffer.ToArray());

            _buffer.Dispose();
        }
        public static void HandlePlayerAnimatorUpdate(int _index, string _paramaterName, bool _value)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.UpdatePlayerAnimator);
            _buffer.WriteInteger(_index);

            //send to clients
            _buffer.WriteInteger((int)AnimatorValueType.Bool);
            _buffer.WriteString(_paramaterName);
            _buffer.WriteBool(_value);

            r_ClientManager.SendDataToAll(_buffer.ToArray());

            _buffer.Dispose();
        }
        public static void HandlePlayerAnimatorUpdate(int _index, string _paramaterName, float _value)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.UpdatePlayerAnimator);
            _buffer.WriteInteger(_index);

            //send to clients
            _buffer.WriteInteger((int)AnimatorValueType.Float);
            _buffer.WriteString(_paramaterName);
            _buffer.WriteFloat(_value);

            r_ClientManager.SendDataToAll(_buffer.ToArray());

            _buffer.Dispose();
        }

        public static void SendNetworkName(int _index, string _networkName)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ServerPackets.SetNetworkName);
            _buffer.WriteInteger(_index);
            _buffer.WriteString(_networkName);

            r_ClientManager.SendDataToAll(_buffer.ToArray());

            _buffer.Dispose();
        }
    }
}
