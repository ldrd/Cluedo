using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network.Packets
{
    internal class GameplayPacket : Packet
    {

        public static readonly UInt32 MASK_FUNCTION = 0xF8000000;
        public static readonly byte SHIFT_POS_FUNCTION = 27;

        public Int32 infos;

        public GameplayPacket(WorldPacket worldPacket) : base(worldPacket) { }

        public GameplayPacket(Int32 infos) : base()
        {
            this.infos = infos;
            worldPacket.SetOpCode(OpcodeMsg.GameplayPacket);
            worldPacket.Append(this.infos);
        }

        protected override void Read()
        {
            infos = worldPacket.Read<Int32>();
        }

        public int getFunctionNum()
        {
            return (int)(infos & MASK_FUNCTION >> SHIFT_POS_FUNCTION);
        }
    }
}
