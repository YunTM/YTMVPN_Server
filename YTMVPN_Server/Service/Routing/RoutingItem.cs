using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.Routing
{
    class RoutingItem
    {
        public byte[] DstAddr { get; set; }
        public byte[] DstPort { get; set; }
        public ConcurrentQueue<DataPacket> OutputQueue { get; set; }
        public RoutingItem(byte[] DstAddr, ConcurrentQueue<DataPacket> OutputQueue, byte[] DstPort = null)
        {
            this.DstAddr = DstAddr;
            this.OutputQueue = OutputQueue;
            this.DstPort = DstPort;
        }
    }
}
