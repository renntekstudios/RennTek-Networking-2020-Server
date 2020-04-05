using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Shared
{    
    //This is what the client send's to the server
    public enum ClientPackets
    {
        InstantiateLocalPlayer = 1,
        Authentication = 2,
        UpdatePlayer = 3,
        SetNetworkName = 4,
        RequestServerData = 5,
        UpdatePlayerAnimator = 6,
        UpdateEntity = 7,
    }

    //This is what server sends to clients
    public enum ServerPackets
    {
        InstantiateRemotePlayer = 1,
        InstantiateLocalPlayer = 2,
        DestoryPlayer = 3,
        Authentication = 4,
        UpdatePlayer = 5,
        InstantiateWorldObjects = 6,
        SetNetworkName = 7,
        RequestServerData = 8,
        UpdatePlayerAnimator = 9,
        SendAuthenticationConfirmed = 10,
        UpdateEntity = 11,
    }
}
