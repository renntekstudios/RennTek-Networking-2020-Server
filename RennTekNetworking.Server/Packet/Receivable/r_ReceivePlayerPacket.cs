using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Debug;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet.Receivable
{
    static class r_ReceivePlayerPacket
    {
        public static void HandlePlayerUpdate(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();

            r_Vector3 _position = new r_Vector3(_buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat());
            r_Quaternion _rotation = new r_Quaternion(_buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat(), _buffer.ReadFloat());

            _buffer.Dispose();

            if (r_ClientManager.m_Clients.ContainsKey(_connectionID))
            {
                r_ClientManager.m_Clients[_connectionID].m_Position = _position;
                r_ClientManager.m_Clients[_connectionID].m_Rotation = _rotation;
            }

            r_ClientManager.UpdateNetworkPlayer(_connectionID, _position, _rotation);
        }

        public static void HandlePlayerAnimatorUpdate(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _animatorType = _buffer.ReadInteger();

            switch ((int)_animatorType)
            {
                case (int)AnimatorValueType.Int:
                    r_ClientManager.UpdatePlayerAnimator(_connectionID, _buffer.ReadString(), _buffer.ReadInteger());
                    break;
                case (int)AnimatorValueType.Bool:
                    r_ClientManager.UpdatePlayerAnimator(_connectionID, _buffer.ReadString(), _buffer.ReadBool());
                    break;
                case (int)AnimatorValueType.Float:
                    r_ClientManager.UpdatePlayerAnimator(_connectionID, _buffer.ReadString(), _buffer.ReadFloat());
                    break;
            }

            _buffer.Dispose();
        }

        public static void HandleSetNetworkName(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            string _nickName = _buffer.ReadString();

            _buffer.Dispose();

            if (r_ClientManager.m_Clients.ContainsKey(_connectionID))
                r_ClientManager.m_Clients[_connectionID].m_NetworkName = _nickName;
        }

    }
}
