using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Client.Public
{
    static class r_ClientConfiguration
    {
        public static string m_ClientVersion = "0.1";

        public static void SetVersion(string _version)
        {
            m_ClientVersion = _version;
        }
    }
}
