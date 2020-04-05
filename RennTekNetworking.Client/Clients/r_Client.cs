using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using RennTekNetworking.Client.Public.Thread;
using RennTekNetworking.Client.Packet;

using UnityEngine;
using RennTekNetworking.Client.Public.Managers;
using UnityEngine.SceneManagement;
using RennTekNetworking.Shared.Buffer;

namespace RennTekNetworking.Client.Clients
{
    public static class r_Client
    {
        public static TcpClient m_Socket;
        private static NetworkStream m_Stream;

        public static string m_NetworkName { get; set; }

        private static byte[] m_ReceivedBuffer;

        public static void ConnectToServer()
        {
            r_PacketHandler.InitializePackets();

            InitializeNetwork();
        }

        private static void InitializeNetwork()
        {
            m_Socket = new TcpClient
            {
                ReceiveBufferSize = 4096,
                SendBufferSize = 4096
            };

            m_ReceivedBuffer = new byte[4096 * 2];

            m_Socket.BeginConnect(r_NetworkManager.instance.m_ServerIP, r_NetworkManager.instance.m_ServerPort, new AsyncCallback(OnConnected), m_Socket);
        }

        private static void OnConnected(IAsyncResult _result)
        {
            m_Socket.EndConnect(_result);

            if (!m_Socket.Connected)
            {
                Disconnect(false);
                return;
            }
            else
            {
                m_Socket.NoDelay = true;

                m_Stream = m_Socket.GetStream();

                m_Stream.BeginRead(m_ReceivedBuffer, 0, 4096 * 2, ReceiveCallback, null);
            }
        }

        private static void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _length = m_Stream.EndRead(_result);

                if (_length <= 0)
                {
                    Disconnect(true);
                    return;
                }

                byte[] _newBytes = new byte[_length];
                Array.Copy(m_ReceivedBuffer, _newBytes, _length);

                r_ThreadManager.executeInFixedUpdate(() =>
                {
                    r_PacketHandler.HandlePacketData(_newBytes);
                });

                m_Stream.BeginRead(m_ReceivedBuffer, 0, 4096 * 2, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                //Disconnect(false);
                return;
            }
        }

        public static void SendData(byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteInteger((_data.GetUpperBound(0) - _data.GetLowerBound(0)) + 1);
            _buffer.WriteBytes(_data);

            m_Stream.BeginWrite(_buffer.ToArray(), 0, _buffer.ToArray().Length, null, null);

            _buffer.Dispose();
        }

        public static void Disconnect(bool _exception)
        {
            if(!_exception)
                Debug.Log("Disconnected From Server");

            m_Socket.Close();
            m_Socket = null;
            m_Stream = null;

            SceneManager.LoadScene("MainMenu");
        }

        public static bool IsConnected()
        {
            if (m_Socket != null)
            {
                if (m_Socket.Connected == false)
                    return false;
            }
            return true;
        }
    }
}
