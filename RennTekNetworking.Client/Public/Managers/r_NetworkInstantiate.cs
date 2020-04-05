using RennTekNetworking.Client.Public.Controllers;
using RennTekNetworking.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine;

namespace RennTekNetworking.Client.Public.Managers
{
    public static class r_NetworkInstantiate
    {
        public static void InstantiateWorldObject(string _prefab, string _guid, Vector3 _position, Quaternion _rotation, EntityType _entityType)
        {
            r_WorldObjectManager.CreateWorldObject(_prefab, _guid, _position, _rotation, _entityType);
        }

        public static void InstantiatePlayer(int _index, string _networkName)
        {
            GameObject _player = (GameObject)MonoBehaviour.Instantiate(r_NetworkManager.instance.m_PlayerPrefab);

            if (r_NetworkManager.instance.IsLocalPlayer(_index))
            {
                _player.name = $"[{_networkName}]:({_index})";

                if (_player.GetComponent<r_NetView>() == null)
                {
                    Debug.LogError($"[{_networkName}] Your missing a netview on your prefab.");
                    return;
                }
                else _player.GetComponent<r_NetView>().IsMine(true);

                _player.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                _player.name = $"[REMOTE]:{_index}";
                _player.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
            }

            r_NetworkManager.instance.m_NetworkPlayers.Add(_index, _player);
        }
    }
}
