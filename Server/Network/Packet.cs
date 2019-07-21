using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
    abstract class Packet : Bytebuffer
    {
        public Packet(Packet packet)
        {
            data = new List<byte>(packet.GetData());
            Read();
        }

        public Packet() { }

        public abstract void Read();
    }
}
