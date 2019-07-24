using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.PacketManager
{
    public interface IPacketManager
    {
        void Manage(WorldPacket worldpacket);
    }
}
