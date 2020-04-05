using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RennTekNetworking.Shared.Buffer
{
    public class r_ByteBuffer : IDisposable
    {
        #region Variables
        private List<byte> m_Buffer;
        private byte[] m_ReadBuffer;
        private int m_ReadPosition;
        private bool m_BufferUpdated = false;
        #endregion

        #region Structure
        public r_ByteBuffer()
        {
            m_Buffer = new List<byte>();
            m_ReadPosition = 0;
        }
        #endregion

        #region Shared
        public int GetReadPosition()
        {
            return m_ReadPosition;
        }

        public byte[] ToArray()
        {
            return m_Buffer.ToArray();
        }

        public int Count()
        {
            return m_Buffer.Count;
        }

        public int Length()
        {
            return Count() - m_ReadPosition;
        }

        public void Clear()
        {
            m_Buffer.Clear();
            m_ReadPosition = 0;
        }
        #endregion

        #region Write
        //write
        public void WriteByte(byte _value)
        {
            m_Buffer.Add(_value);
            m_BufferUpdated = true;
        }

        public void WriteBytes(byte[] _value)
        {
            m_Buffer.AddRange(_value);
            m_BufferUpdated = true;
        }

        public void WriteShort(short _value)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(_value));
            m_BufferUpdated = true;
        }

        public void WriteInteger(int _value)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(_value));
            m_BufferUpdated = true;
        }

        public void WriteLong(long _value)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(_value));
            m_BufferUpdated = true;
        }

        public void WriteFloat(float _value)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(_value));
            m_BufferUpdated = true;
        }

        public void WriteBool(bool _value)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(_value));
            m_BufferUpdated = true;
        }

        public void WriteString(string _value)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(_value.Length));
            m_Buffer.AddRange(Encoding.ASCII.GetBytes(_value));
            m_BufferUpdated = true;
        }
        #endregion

        #region Read
        public byte ReadByte(bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                byte _value = m_ReadBuffer[m_ReadPosition];

                if (_peek & m_Buffer.Count > m_ReadPosition)
                    m_ReadPosition += 1;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read byte.");
            }
        }

        public byte[] ReadBytes(int _length, bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                byte[] _value = m_Buffer.GetRange(m_ReadPosition, _length).ToArray();

                if (_peek)
                    m_ReadPosition += _length;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read byte array.");
            }
        }

        public short ReadShort(bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                short _value = BitConverter.ToInt16(m_ReadBuffer, m_ReadPosition);

                if (_peek & m_Buffer.Count > m_ReadPosition)
                    m_ReadPosition += 2;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read short.");
            }
        }

        public int ReadInteger(bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                int _value = BitConverter.ToInt32(m_ReadBuffer, m_ReadPosition);

                if (_peek & m_Buffer.Count > m_ReadPosition)
                    m_ReadPosition += 4;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read int.");
            }
        }

        public long ReadLong(bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                long _value = BitConverter.ToInt64(m_ReadBuffer, m_ReadPosition);

                if (_peek & m_Buffer.Count > m_ReadPosition)
                    m_ReadPosition += 8;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read long.");
            }
        }

        public float ReadFloat(bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                float _value = BitConverter.ToSingle(m_ReadBuffer, m_ReadPosition);

                if (_peek & m_Buffer.Count > m_ReadPosition)
                    m_ReadPosition += 4;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read float.");
            }
        }

        public bool ReadBool(bool _peek = true)
        {
            if (m_Buffer.Count > m_ReadPosition)
            {
                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                bool _value = BitConverter.ToBoolean(m_ReadBuffer, m_ReadPosition);

                if (_peek & m_Buffer.Count > m_ReadPosition)
                    m_ReadPosition += 8;

                return _value;
            }
            else
            {
                throw new Exception("Not trying to read bool.");
            }
        }

        public string ReadString(bool _peek = true)
        {
            try
            {
                int _length = ReadInteger(true);

                if (m_BufferUpdated)
                {
                    m_ReadBuffer = m_Buffer.ToArray();
                    m_BufferUpdated = false;
                }

                string _value = Encoding.ASCII.GetString(m_ReadBuffer, m_ReadPosition, _length);

                if (_peek & m_Buffer.Count > m_ReadPosition)
                {
                    if (_value.Length > 0)
                        m_ReadPosition += _length;
                }
                return _value;
            }
            catch (Exception _ex)
            {
                throw new Exception("Not trying to read string.");
            }
        }
        #endregion

        #region Functions
        private bool m_DisposedValue = false;
        protected virtual void Dispose(bool _disposing)
        {
            if (!_disposing)
            {
                if (_disposing)
                {
                    m_Buffer.Clear();
                    m_ReadPosition = 0;
                }
                m_DisposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}