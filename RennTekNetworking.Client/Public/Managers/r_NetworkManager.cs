using RennTekNetworking.Client.Clients;
using RennTekNetworking.Client.Packet.Sendable;
using RennTekNetworking.Client.Public.Controllers;
using RennTekNetworking.Client.Public.Thread;
using RennTekNetworking.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using UnityEngine;

namespace RennTekNetworking.Client.Public.Managers
{
    public class r_NetworkManager : MonoBehaviour
    {
        public static r_NetworkManager instance;

        public int m_ConnectionID { get; set; }

        [Header("Connection")]
        public string m_ServerIP = "127.0.0.1";
        public int m_ServerPort = 4435;

        public Dictionary<int, GameObject> m_NetworkPlayers = new Dictionary<int, GameObject>();

        [Header("Spawning Of The Player")]
        public GameObject m_PlayerPrefab;

        private void Awake()
        {
            if (instance)
                Destroy(instance.gameObject);
            instance = this;

            InvokeRepeating("CheckNetworkPing", 1, 5);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public float m_EnemyMovementSpeed = 1f;

        private void FixedUpdate()
        {
            if (r_WorldObjectManager.m_ObjectEntitys.Count == 0)
                return;

            foreach (var _enemy in r_WorldObjectManager.m_ObjectEntitys)
            {
                if (_enemy.Value.m_EntityType == EntityType.Enemy)
                {
                    if (_enemy.Value.m_Object != null)
                    {
                        if (_enemy.Value.m_Object.transform.position != _enemy.Value.m_Position)
                        {
                            _enemy.Value.m_Object.transform.position = Vector3.Lerp(_enemy.Value.m_Object.transform.position, _enemy.Value.m_Position, m_EnemyMovementSpeed * Time.deltaTime);

                            //only need to send update for the first client, dont need everyone to have it
                            if(m_NetworkPlayers.First().Key == m_ConnectionID)
                                r_SendWorldUpdatePacket.SendEntityUpdate(_enemy.Key, _enemy.Value.m_Object.transform.position);
                        }
                    }
                }
            }
        }

        public void DestoryPlayer(int _index)
        {
            if (m_NetworkPlayers.ContainsKey(_index))
            {
                Destroy(m_NetworkPlayers[_index]);
                m_NetworkPlayers.Remove(_index);
            }
            Debug.Log($"[SERVER] Destroyed Network Player With ID:{_index}");
        }

        public void UpdateRemotePlayer(int _index, float _pX, float _pY, float _pZ, float _rX, float _rY, float _rZ, float _rW)
        {
            if (m_NetworkPlayers.ContainsKey(_index))
                if (m_NetworkPlayers[_index].GetComponent<r_NetView>() != null)
                    m_NetworkPlayers[_index].GetComponent<r_NetView>().SetRemoteValues(_pX, _pY, _pZ, _rX, _rY, _rZ, _rW);
        }

        public void SetRemoteAnimatorValues(int _index, string _paramaterName, int _value)
        {
            if(m_NetworkPlayers.ContainsKey(_index))
                if(m_NetworkPlayers[_index].GetComponent<r_NetworkAnimator>() != null)
                    m_NetworkPlayers[_index].GetComponent<r_NetworkAnimator>().SetRemoteAnimatorValues(_paramaterName, _value);
        }
        public void SetRemoteAnimatorValues(int _index, string _paramaterName, bool _value)
        {
            Debug.Log($"[ANIM] {_index} : {_paramaterName} : {_value}");

            if (m_NetworkPlayers.ContainsKey(_index))
                if (m_NetworkPlayers[_index].GetComponent<r_NetworkAnimator>() != null)
                    m_NetworkPlayers[_index].GetComponent<r_NetworkAnimator>().SetRemoteAnimatorValues(_paramaterName, _value);
        }
        public void SetRemoteAnimatorValues(int _index, string _paramaterName, float _value)
        {
            if (m_NetworkPlayers.ContainsKey(_index))
                if (m_NetworkPlayers[_index].GetComponent<r_NetworkAnimator>() != null)
                    m_NetworkPlayers[_index].GetComponent<r_NetworkAnimator>().SetRemoteAnimatorValues(_paramaterName, _value);
        }

        public void SetNetworkName(int _index, string _networkName)
        {
            if (m_NetworkPlayers.ContainsKey(_index))
                m_NetworkPlayers[_index].GetComponent<r_NetView>().SetNetworkName(_networkName);
        }

        private void OnApplicationQuit()
        {
            if(r_Client.m_Socket != null)
                if(r_Client.m_Socket.Connected)
                    r_Client.Disconnect(false);
        }

        public bool IsLocalPlayer(int _connectionID)
        {
            return (_connectionID == m_ConnectionID) ? true : false;
        }

        private void CheckNetworkPing()
        {
            if (r_Client.IsConnected() == false)
                r_Client.Disconnect(false);
        }

        //demo
        public void CreateWorldObject(WorldObject _object)
        {
            StartCoroutine(SpawnWorldObject(_object));
        }

        public IEnumerator SpawnWorldObject(WorldObject _object)
        {
            yield return new WaitForSeconds(2);
            r_WorldObjectManager.CreateObject(_object);
        }

    }
}
