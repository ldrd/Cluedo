namespace Server.Network
{
    public class WorldPacket : Bytebuffer
    {
        public WorldPacket() : base() { opcode = OpcodeMsg.Unknown; }
        public WorldPacket(OpcodeMsg opcode) : base() { this.opcode = opcode; }
        public WorldPacket(OpcodeMsg opcode, byte[] bytes) : base(bytes) { this.opcode = opcode; }

        public void SetOpCode(OpcodeMsg opcode) { this.opcode = opcode; }
        public OpcodeMsg GetOpcode() { return this.opcode; }

        private OpcodeMsg opcode;
    }
}