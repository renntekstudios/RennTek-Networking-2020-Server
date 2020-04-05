using RennTekNetworking.Client.Clients;
using RennTekNetworking.Client.Packet.Sendable;
using RennTekNetworking.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RennTekNetworking.Client.Public.Controllers
{
    public class r_NetworkAnimator : MonoBehaviour
    {
        [Header("Network Variables")]
        public List<r_AnimatorInt> m_Ints = new List<r_AnimatorInt>();
        public List<r_AnimatorBool> m_Bools = new List<r_AnimatorBool>();
        public List<r_AnimatorFloat> m_Floats = new List<r_AnimatorFloat>();

        [Header("Animator")]
        public Animator m_Animator;

        [Header("NetView")]
        public r_NetView m_NetView;

        public void Update()
        {
            if (m_NetView == null)
                this.enabled = false;

            if (m_Animator == null)
                this.enabled = false;

            if(m_NetView.IsMine())
                UpdateAnimatorForLocal();
        }

        private void UpdateAnimatorForLocal()
        {
            if (m_Ints.Count > 0)
            {
                for(int i = 0; i < m_Ints.Count; i++)
                {
                    if(m_Animator.GetInteger(m_Ints[i].m_ParamaterName) != m_Ints[i].m_Value)
                    {
                        m_Ints[i].m_Value = m_Animator.GetInteger(m_Ints[i].m_ParamaterName);
                        r_SendPlayerPacket.SendAnimatorValue(m_Ints[i].m_ParamaterName, m_Ints[i].m_Value);
                    }
                }
            }

            if (m_Bools.Count > 0)
            {
                for (int b = 0; b < m_Bools.Count; b++)
                {
                    if (m_Animator.GetBool(m_Bools[b].m_ParamaterName) != m_Bools[b].m_Value)
                    {
                        m_Bools[b].m_Value = m_Animator.GetBool(m_Bools[b].m_ParamaterName);
                        r_SendPlayerPacket.SendAnimatorValue(m_Bools[b].m_ParamaterName, m_Bools[b].m_Value);
                    }
                }
            }

            if (m_Floats.Count > 0)
            {
                for (int f = 0; f < m_Floats.Count; f++)
                {
                    if (m_Animator.GetFloat(m_Floats[f].m_ParamaterName) != m_Floats[f].m_Value)
                    {
                        m_Floats[f].m_Value = m_Animator.GetFloat(m_Floats[f].m_ParamaterName);
                        r_SendPlayerPacket.SendAnimatorValue(m_Floats[f].m_ParamaterName, m_Floats[f].m_Value);
                    }
                }
            }
        }

        public void SetRemoteAnimatorValues(string _paramaterName, int _value)
        {
            m_Ints.Find(x => x.m_ParamaterName == _paramaterName).m_Value = _value;
        }
        public void SetRemoteAnimatorValues(string _paramaterName, bool _value)
        {
            m_Bools.Find(x => x.m_ParamaterName == _paramaterName).m_Value = _value;
        }
        public void SetRemoteAnimatorValues(string _paramaterName, float _value)
        {
            m_Floats.Find(x => x.m_ParamaterName == _paramaterName).m_Value = _value;
        }
    }
}
