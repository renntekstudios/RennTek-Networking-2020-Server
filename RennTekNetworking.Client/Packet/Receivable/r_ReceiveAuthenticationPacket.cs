using RennTekNetworking.Client.Packet.Sendable;
using RennTekNetworking.Client.Public;
using RennTekNetworking.Client.Public.Managers;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Client.Packet.Receivable
{
    class r_ReceiveAuthenticationPacket
    {
        public static void HandleAuthentication(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _connectionID = _buffer.ReadInteger();

            _buffer.Dispose();

            //we save the connection id, even if the client get's kicked.
            r_NetworkManager.instance.m_ConnectionID = _connectionID;

            r_SendAuthenticationPacket.SendAuthenticationToServer(r_ClientConfiguration.m_ClientVersion);
        }

        public static void HandleAuthenticationConfirmed(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();
            int _connectionID = _buffer.ReadInteger();

            _buffer.Dispose();

            //set it
            r_NetworkManager.instance.m_ConnectionID = _connectionID;

            r_SceneManager.instance.LoadSceneAsync("GameScene");
        }
    }
}
