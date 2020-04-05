using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Consoles
{
    static class r_ConsoleCommands
    {
        public static void InitCommand(string _command)
        {
            string[] _args = SplitCommand(_command);

            if(_args.Length == 1)
            {
                if(_args[0] == "/help")
                {
                    r_Log.Command("/help");
                    r_Log.Command("/kick userid");
                    r_Log.Command("/players");
                    r_Log.Command("/getposition userid");
                    r_Log.Command("/getrotation userid");
                }
                else if(_args[0] == "/players")
                {
                    if(r_ClientManager.m_Clients.Count == 0)
                    {
                        r_Log.Command($"[PLAYERS] There is no clients connected!");
                        return;
                    }
                    else
                    {
                        foreach(var _client in r_ClientManager.m_Clients)
                            r_Log.Command($"[PLAYERS] UserID:{_client.Value.m_ConnectionID}");
                    }
                }
                else if(_args[0] == "/clear")
                    Console.Clear();
            }
            else if(_args.Length == 2)
            {
                if(_args[0] == "/kick")
                {
                    if (r_ClientManager.m_Clients.ContainsKey(int.Parse(_args[1])))
                    {
                        r_ClientManager.m_Clients[int.Parse(_args[1])].CloseConnection(false);
                        r_Log.Command($"[KICK] User {int.Parse(_args[1])} has been kicked!");
                    }
                    else r_Log.Error("[KICK] Userid doesnt exist!");
                }
                else if(_args[0] == "/getposition")
                {
                    if(r_ClientManager.m_Clients.ContainsKey(int.Parse(_args[1])))
                    {
                        r_Vector3 _position = r_ClientManager.m_Clients[int.Parse(_args[1])].GetPosition();
                        r_Log.Command($"[POSITION] {_position.x},{_position.y},{_position.z}");
                    }
                    else r_Log.Error("[POSITION] Userid doesnt exist!");
                }
                else if (_args[0] == "/getrotation")
                {
                    if (r_ClientManager.m_Clients.ContainsKey(int.Parse(_args[1])))
                    {
                        r_Quaternion _rotation = r_ClientManager.m_Clients[int.Parse(_args[1])].GetRotation();
                        r_Log.Command($"[ROTATION] {_rotation.x},{_rotation.y},{_rotation.z},{_rotation.w}");
                    }
                    else r_Log.Error("[POSITION] Userid doesnt exist!");
                }
            }
        }

        private static string[] SplitCommand(string _command)
        {
            string _original = _command;
            string[] _split = _command.Split(' ');

            return _split;
        }
    }
}
