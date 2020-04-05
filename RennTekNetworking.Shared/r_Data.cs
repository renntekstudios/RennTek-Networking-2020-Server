using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Shared
{
    [System.Serializable]
    public class r_AnimatorInt
    {
        public string m_ParamaterName;
        public int m_Value;
    }
    [System.Serializable]
    public class r_AnimatorBool
    {
        public string m_ParamaterName;
        public bool m_Value;
    }
    [System.Serializable]
    public class r_AnimatorFloat
    {
        public string m_ParamaterName;
        public float m_Value;
    }

    [System.Serializable]
    public enum AnimatorValueType
    {
        Int = 1,
        Bool = 2,
        Float = 3,
    }

    [System.Serializable]
    public enum EntityType
    {
        Object,
        Enemy
    }
}
