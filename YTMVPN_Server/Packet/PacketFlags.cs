using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Packet
{
    public enum PacketState
    {
        Hello = 0x00,
        Hello_ACK = 0x01,
        Hello_Succsss = 0x02,
        Hello_Done = 0x03,
    }
}
