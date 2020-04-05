using RennTekNetworking.Client.Clients;
using RennTekNetworking.Client.Packet.Sendable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RennTekNetworking.Client.Public.Controllers
{
    public class r_NetView : MonoBehaviour
    {
        public bool m_isMine { get; set; }
        public float m_LerpRate = 9f;

        public void IsMine(bool _value) { m_isMine = _value; }
        public bool IsMine() { return m_isMine; }

        private Vector3 m_NetworkPosition { get; set; }
        private Quaternion m_NetworkRotation { get; set; }

        public string m_NetworkName { get; set; }

        public void SetRemoteValues(float _pX, float _pY, float _pZ, float _rX, float _rY, float _rZ, float _rW)
        {
            m_NetworkPosition = new Vector3(_pX, _pY, _pZ);
            m_NetworkRotation = new Quaternion(_rX, _rY, _rZ, _rW);
        }

        public void SetNetworkName(string _networkName)
        {
            m_NetworkName = _networkName;
        }

        private void Start()
        {
            r_SendPlayerPacket.SendPlayerUpdate(transform.position.x, transform.position.y, transform.position.z, transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }

        public void Update()
        {
            if (m_isMine)
            {
                if (Vector3.Distance(transform.position, m_NetworkPosition) > 0.1f || transform.rotation != m_NetworkRotation)
                    r_SendPlayerPacket.SendPlayerUpdate(transform.position.x, transform.position.y, transform.position.z, transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, m_NetworkPosition, Time.deltaTime * m_LerpRate);
                transform.rotation = Quaternion.Lerp(transform.rotation, m_NetworkRotation, Time.deltaTime * m_LerpRate);
            }
        }
    }
}
