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
    class r_SendPlayerPacket
    {
        public static void SendPlayerUpdate(float _pX, float _pY, float _pZ, float _rX, float _rY, float _rZ, float _rW)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.UpdatePlayer);

            _buffer.WriteFloat(_pX);
            _buffer.WriteFloat(_pY);
            _buffer.WriteFloat(_pZ);

            _buffer.WriteFloat(_rX);
            _buffer.WriteFloat(_rY);
            _buffer.WriteFloat(_rZ);
            _buffer.WriteFloat(_rW);

            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }

        public static void SendNetworkName(string _networkName)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.SetNetworkName);
            _buffer.WriteString(_networkName);

            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }
        public static void SendAnimatorValue(string _paramaterName, int _value)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.UpdatePlayerAnimator);

            _buffer.WriteInteger((int)AnimatorValueType.Int);
            _buffer.WriteString(_paramaterName);
            _buffer.WriteInteger(_value);

            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }
        public static void SendAnimatorValue(string _paramaterName, bool _value)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.UpdatePlayerAnimator);

            _buffer.WriteInteger((int)AnimatorValueType.Bool);
            _buffer.WriteString(_paramaterName);
            _buffer.WriteBool(_value);

            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }
        public static void SendAnimatorValue(string _paramaterName, float _value)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((int)ClientPackets.UpdatePlayerAnimator);

            _buffer.WriteInteger((int)AnimatorValueType.Float);
            _buffer.WriteString(_paramaterName);
            _buffer.WriteFloat(_value);

            UnityEngine.Debug.Log($"[SENDING][FLOAT] ({_value}) to server");
            r_Client.SendData(_buffer.ToArray());

            _buffer.Dispose();
        }
    }
}
