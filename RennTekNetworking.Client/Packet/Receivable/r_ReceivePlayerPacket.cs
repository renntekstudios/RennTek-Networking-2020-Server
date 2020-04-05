using RennTekNetworking.Client.Public.Managers;
using RennTekNetworking.Shared;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Client.Packet.Receivable
{
    class r_ReceivePlayerPacket
    {
        public static void HandleDestroyPlayer(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _index = _buffer.ReadInteger();

            _buffer.Dispose();

            r_NetworkManager.instance.DestoryPlayer(_index);
        }

        public static void HandlePlayerUpdate(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _index = _buffer.ReadInteger();

            float _pX = _buffer.ReadFloat();
            float _pY = _buffer.ReadFloat();
            float _pZ = _buffer.ReadFloat();


            float _rX = _buffer.ReadFloat();
            float _rY = _buffer.ReadFloat();
            float _rZ = _buffer.ReadFloat();
            float _rW = _buffer.ReadFloat();

            _buffer.Dispose();

            r_NetworkManager.instance.UpdateRemotePlayer(_index, _pX, _pY, _pZ, _rX, _rY, _rZ, _rW);
        }
        public static void HandleSetNetworkName(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _index = _buffer.ReadInteger();
            string _networkName = _buffer.ReadString();

            _buffer.Dispose();

            r_NetworkManager.instance.SetNetworkName(_index, _networkName);
        }

        public static void HandlePlayerAnimatorUpdate(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _index = _buffer.ReadInteger();

            int _animatorType = _buffer.ReadInteger();

            switch (_animatorType)
            {
                case (int)AnimatorValueType.Int:
                    r_NetworkManager.instance.SetRemoteAnimatorValues(_index, _buffer.ReadString(), _buffer.ReadInteger());
                    break;
                case (int)AnimatorValueType.Bool:
                    r_NetworkManager.instance.SetRemoteAnimatorValues(_index, _buffer.ReadString(), _buffer.ReadBool());
                    break;
                case (int)AnimatorValueType.Float:
                    r_NetworkManager.instance.SetRemoteAnimatorValues(_index, _buffer.ReadString(), _buffer.ReadFloat());
                    break;
            }

            _buffer.Dispose();
        }
    }
}
