﻿using RennTekNetworking.Server.Clients;
using RennTekNetworking.Shared.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Packet.Receivable
{
    static class r_ReceiveInstantiationPacket
    {
        public static void HandleInstantiateLocalPlayerPacket(int _connectionID, byte[] _data)
        {
            r_ByteBuffer _buffer = new r_ByteBuffer();
            _buffer.WriteBytes(_data);

            int _packetID = _buffer.ReadInteger();

            _buffer.Dispose();

            r_ClientManager.InstantiateLocalPlayer(_connectionID);
            r_ClientManager.UpdateNetworkName(_connectionID);
        }

    }
}
