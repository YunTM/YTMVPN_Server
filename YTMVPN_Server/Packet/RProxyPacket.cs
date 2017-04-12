using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Packet
{
    class RProxyPacket:DataPacket
    {
        public RProxyPacket(int AddrLength, int PortLength, byte[] rawData, int Length) : base(AddrLength, PortLength, rawData, Length) { }

        public byte[] SessionID {
            get
            {
                this.RawData[this.s]

            }
            set
            {
            }
        }

        override public byte[] PayloadData
        {
            get
            {

            }
            set
            {

            }
        }
    }
}
