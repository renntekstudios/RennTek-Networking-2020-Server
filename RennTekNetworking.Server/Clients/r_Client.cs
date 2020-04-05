using RennTekNetworking.Server.Debug;
using RennTekNetworking.Server.Packet;
using RennTekNetworking.Server.Server;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Clients
{
    public class r_Client
    {
        public int m_ConnectionID { get; set; }
        public bool m_SpawnedInGame { get; set; }
        public string m_NetworkName = "Guest";
        public r_Vector3 m_Position { get; set; }
        public r_Quaternion m_Rotation { get; set; }

        public TcpClient m_Socket;
        public NetworkStream m_Stream;

        private byte[] m_ReceivedBuffer;
        public r_ByteBuffer m_ByteBuffer;

        public void Start()
        {
            m_Socket.SendBufferSize = 4096;
            m_Socket.ReceiveBufferSize = 4096;

            m_Stream = m_Socket.GetStream();

            m_ReceivedBuffer = new byte[4096];

            m_Stream.BeginRead(m_ReceivedBuffer, 0, m_Socket.ReceiveBufferSize, OnReceivedData, null);

            r_Log.Warning($"Incoming connection from '({m_Socket.Client.RemoteEndPoint.ToString()})'");
        }

        private void OnReceivedData(IAsyncResult _result)
        {
            try
            {
                int _length = m_Stream.EndRead(_result);

                if (_length <= 0)
                {
                    CloseConnection(false);
                    return;
                }

                byte[] _newBytes = new byte[_length];
                Array.Copy(m_ReceivedBuffer, _newBytes, _length);

                r_PacketHandler.HandlePacketData(m_ConnectionID, _newBytes);

                m_Stream.BeginRead(m_ReceivedBuffer, 0, m_Socket.ReceiveBufferSize, OnReceivedData, null);

            }
            catch (Exception e)
            {
                CloseConnection(true);
                return;
            }
        }

        public void CloseConnection(bool _exception)
        {
            r_ClientManager.DestoryNetworkPlayer(m_ConnectionID);

            r_ClientManager.m_Clients.Remove(m_ConnectionID);

            if (!_exception)
                r_Log.Warning($"Connection from '({m_ConnectionID})' has been terminated.");

            m_Socket.Close();
        }

        public r_Vector3 GetPosition()
        {
            return m_Position;
        }

        public Vector3 GetPositionToVector3()
        {
            return new Vector3(m_Position.x, m_Position.y, m_Position.z);
        }

        public r_Quaternion GetRotation()
        {
            return m_Rotation;
        }
    }
}
