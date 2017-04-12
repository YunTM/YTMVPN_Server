using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Packet
{
    public enum RProxyPacketState
    {
        TCP_Connect = 0x00,
        RProxyPacket = 0x01,
        TCP_Data = 0x02,
        TCP_Fin = 0x03,
        TCP_Rst = 0x04
    }
}
