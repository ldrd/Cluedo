using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network;
using Server.Network.Packets;

namespace Server.PacketManager
{
    public class GameplayPacketManager : IPacketManager
    {
        private readonly List<Action> actions;

        public void Manage(WorldPacket worldpacket)
        {
            actions[new GameplayPacket(worldpacket).getFunctionNum()]();
        }
    }
}
