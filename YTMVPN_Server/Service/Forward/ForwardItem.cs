using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.Forward
{
    class ForwardItem
    {
        public byte[] DstAddr { get; set; }
        public byte[] DstPort { get; set; }
        public EndPoint DstEndPoint { get; set; }
        public ForwardItem(byte[] DstAddr, EndPoint DstEndPoint, byte[] DstPort = null)
        {
            this.DstAddr = DstAddr;
            this.DstPort = DstPort;
            this.DstEndPoint = DstEndPoint;
        }
    }
}
