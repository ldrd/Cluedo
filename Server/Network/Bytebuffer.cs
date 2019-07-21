using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
    public class Bytebuffer
    {
        private int rpos = 0;
        protected List<byte> data;
        public Bytebuffer(byte[] data)
        {
            this.data = new List<byte>(data);
        }

        public Bytebuffer()
        {
            data = new List<byte>();
        }

        public T Read<T>()
        {
            var type = typeof(T);

            if (type == typeof(Byte))
                return (T)(object)data[rpos++];
            else if (type == typeof(Int16))
                return (T)(object)(data[rpos++] | data[rpos++] << 8);
            else if (type == typeof(Int32))
                return (T)(object)(data[rpos++] | data[rpos++] << 8 | data[rpos++] << 16 | data[rpos++] << 24);

            throw new NotImplementedException();
        }

        public void Append<T>(T value)
        {
            var type = typeof(T);

            if (type == typeof(Byte))
                data.Add((byte)(object)value);
            else if (type == typeof(Int16))
                data.AddRange(EnsureLittleEndian(BitConverter.GetBytes((Int16)(object)value)));
            else if (type == typeof(Int32))
                data.AddRange(EnsureLittleEndian(BitConverter.GetBytes((Int32)(object)value)));
        }

        private byte[] EnsureLittleEndian(byte[] bytes)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public void SkipBytes(int count)
        {
            rpos += count;
        }

        public Int16 GetSize()
        {
            return (Int16)data.Count;
        }

        public byte[] GetData()
        {
            return data.ToArray();
        }
    }
}
