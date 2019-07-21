using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
    public abstract class Packet
    {
        public Packet(WorldPacket worldPacket)
        {
            this.worldPacket = worldPacket;
            Read();
        }

        public WorldPacket GetWorldPacket() => worldPacket;

        protected Packet() { }

        protected abstract void Read();

        protected WorldPacket worldPacket;
    }
}
