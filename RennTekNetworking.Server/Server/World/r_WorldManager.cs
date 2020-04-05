using RennTekNetworking.Server.Clients;
using RennTekNetworking.Server.Debug;
using RennTekNetworking.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using RennTekNetworking.Server.Server;
using RennTekNetworking.Server.Packet.Sendable;

namespace RennTekNetworking.Server.World
{
    static class r_WorldManager
    {
        public static Dictionary<string, r_WorldObject> m_WorldObjects = new Dictionary<string, r_WorldObject>();

        public static void InitializeWorldObjects()
        {
         //   RegisterWorldObject(new r_WorldObject("Cube", new r_Vector3(0, 0.5f, 1), new r_Quaternion(0, 0, 0, 0), EntityType.Object));
           /* RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(0, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(1, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(2, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(3, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(4, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(5, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(6, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(7, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));

            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));*/
            RegisterWorldObject(new r_WorldObject("Enemy", new r_Vector3(8, 1, 0), new r_Quaternion(0, 0, 0, 0), EntityType.Enemy));

            r_Log.Log($"Initialized World Objects '{m_WorldObjects.Count}'");
        }

        public static void RemoveWorldObject(string _objectGUID)
        {
            m_WorldObjects.Remove(_objectGUID);
        }

        public static void RegisterWorldObject(r_WorldObject _object)
        {
            m_WorldObjects.Add(_object.GenerateID(), _object);
        }

        public static void UpdateWorldEnemies()
        {
            if (m_WorldObjects.Count == 0)
                return;

            foreach(var _object in m_WorldObjects)
            {
                if (_object.Value.m_EntityType == EntityType.Enemy)
                    _object.Value.Update();
            }
        }


        public static void UpdateEntityPosition(string _guid, r_Vector3 _position)
        {
            if (m_WorldObjects.ContainsKey(_guid))
            {
                m_WorldObjects[_guid].m_Position = _position;
            }
        }
    }
    
    [System.Serializable]
    public class r_WorldObject
    {
        public string m_Prefab;

        public string m_GUID;

        public r_Vector3 m_Position;
        public r_Vector3 m_TargetPosition;

        public r_Quaternion m_Rotation;

        public EntityType m_EntityType = EntityType.Object;

        public r_WorldObject(string _prefab, r_Vector3 _position, r_Quaternion _rotation, EntityType _entityType)
        {
            this.m_Prefab = _prefab;

            this.m_Position = _position;
            this.m_Rotation = _rotation;

            this.m_EntityType = _entityType;
        }

        public void Update()
        {
            if(r_ClientManager.m_Clients.Count > 0)
            {
                if (GetClosestTarget() == null)
                    return;

                r_Client _target = GetClosestTarget();

                m_TargetPosition = _target.m_Position;
            }
            
            if (r_ClientManager.m_Clients.Count > 0)
                r_SendWorldPacket.SendWorldObjectUpdateToClient(this.m_GUID, this.m_TargetPosition, this.m_Rotation, this.m_EntityType);
        }

        private Vector3 PositionToVector()
        {
            return new Vector3(m_Position.x, m_Position.y, m_Position.z);
        }

        private Quaternion RotationToQuaternion()
        {
            return new Quaternion(m_Rotation.x, m_Rotation.y, m_Rotation.z, m_Rotation.w);
        }

        public string GenerateID()
        {
            m_GUID = Guid.NewGuid().ToString();
            return m_GUID;
        }

        private r_Client GetClosestTarget()
        {
            r_Client bestTarget = null;

            for (int i = 0; i < r_ClientManager.m_Clients.Count; i++)
            {
                float closestDistanceSqr = UnityEngine.Mathf.Infinity;
                Vector3 currentPosition = new Vector3(m_Position.x, m_Position.y, m_Position.z);
                foreach (var potentialTarget in r_ClientManager.m_Clients)
                {
                    Vector3 directionToTarget = potentialTarget.Value.GetPositionToVector3() - currentPosition;
                    float dSqrToTarget = directionToTarget.LengthSquared();
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        if(dSqrToTarget > 5 && dSqrToTarget < 0.9f)
                        {
                            return null;
                        }
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget.Value;
                    }
                }
            }
            return bestTarget;
        }
    }
}