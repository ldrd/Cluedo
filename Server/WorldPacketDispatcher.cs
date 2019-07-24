using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;
using Server.PacketManager;

namespace Server
{
    //Lie un opcode à un manager
    public class WorldPacketDispatcher
    {
        private readonly Dictionary<OpcodeMsg, IPacketManager> link = new Dictionary<OpcodeMsg, IPacketManager>();

        public WorldPacketDispatcher()
        {
            link.Add(OpcodeMsg.GameplayPacket, new GameplayPacketManager());
        }

        public void Dispatch(WorldPacket worldPacket)
        {
            link[worldPacket.GetOpcode()].Manage(worldPacket);
        }
    }
}
