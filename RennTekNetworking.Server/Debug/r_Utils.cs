using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Server.Debug
{
    static class r_Utils
    {
        public static r_Vector3 ConvertToVector3(float _x, float _y, float _z)
        {
            return new r_Vector3(_x, _y, _z);
        }

        public static r_Quaternion ConvertToQuaternion(float _x, float _y, float _z, float _w)
        {
            return new r_Quaternion(_x, _y, _z, _w);
        }
    }

    public struct r_Vector3
    {
        public float x;
        public float y;
        public float z;

        public r_Vector3(float _x, float _y, float _z)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
        }
    }

    public struct r_Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public r_Quaternion(float _x, float _y, float _z, float _w)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
            this.w = _w;
        }
    }
}
