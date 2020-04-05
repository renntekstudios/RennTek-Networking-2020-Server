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
    static class r_WorldObjectManager
    {
        public static Dictionary<string, WorldObject> m_ObjectEntitys = new Dictionary<string, WorldObject>();

        public static void CreateWorldObject(string _prefab, string _guid, Vector3 _position, Quaternion _rotation, EntityType _entityType)
        {
            WorldObject _newWorldObject = new WorldObject(_prefab, _guid, _position, _rotation, _entityType);
            m_ObjectEntitys.Add(_guid, _newWorldObject);

            r_NetworkManager.instance.CreateWorldObject(_newWorldObject);
          //  CreateObject(_newWorldObject);
        }

        public static void CreateObject(WorldObject _worldObject)
        {
            if (Resources.Load($"WorldObjects/{_worldObject.m_Prefab}"))
            {
                GameObject _object = (GameObject)MonoBehaviour.Instantiate(Resources.Load($"WorldObjects/{_worldObject.m_Prefab}"));

                _object.transform.position = _worldObject.m_Position;
                _object.transform.rotation = _worldObject.m_Rotation;

                _object.name = $"[WORLD]{_worldObject.m_GUID}";

                _worldObject.m_Object = _object;
            }
            else
            {
                Debug.LogError("[ERROR] Unable to locate world object");
                return;
            }
        }
        
        public static void UpdateObjectPosition(string _guid, Vector3 _position, Quaternion _rotation, EntityType _entityType)
        {
            switch (_entityType)
            {
                case EntityType.Enemy:
                    if (m_ObjectEntitys.ContainsKey(_guid))
                    {
                        m_ObjectEntitys[_guid].m_Position = _position;
                        m_ObjectEntitys[_guid].m_Rotation = _rotation;
                    }
                    break;
            }
        }
    }

    [System.Serializable]
    public class WorldObject
    {
        public string m_Prefab;
        public string m_GUID;
        public Vector3 m_Position;
        public Quaternion m_Rotation;
        public EntityType m_EntityType;
        public GameObject m_Object;

        public WorldObject(string _prefab, string _guid, Vector3 _position, Quaternion _rotation, EntityType _entityType)
        {
            this.m_Prefab = _prefab;
            this.m_GUID = _guid;
            this.m_Position = _position;
            this.m_Rotation = _rotation;
            this.m_EntityType = _entityType;
        }

        public void Update()
        {
            
        }
    }
}
