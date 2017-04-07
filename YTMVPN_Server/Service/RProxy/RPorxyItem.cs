using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.RProxy
{
    class RProxyItem
    {
        public byte[] DstAddr { get; set; }
        public byte[] DstPort { get; set; }
        public EndPoint DstEndPoint { get; set; }
        public RProxyItem(byte[] DstAddr, EndPoint DstEndPoint, byte[] DstPort = null)
        {
            this.DstAddr = DstAddr;
            this.DstPort = DstPort;
            this.DstEndPoint = DstEndPoint;
        }
    }
}
