using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Packet
{
    class RProxyPacket:DataPacket
    {
        public RProxyPacket(int AddrLength, int PortLength, byte[] rawData, int Length) : base(AddrLength, PortLength, rawData, Length) { }

        public RProxyPacketState State
        {
            get
            {
                return (RProxyPacketState)this.RawData[this.AddrLength * 2 + this.PortLength * 2];
            }
            set
            {
                this.RawData[this.AddrLength * 2 + this.PortLength * 2] = (byte)value;
            }
        }
        public byte[] SessionID {
            get
            {
                byte[] sessionID = new byte[2];
                Buffer.BlockCopy(RawData, this.AddrLength * 2 + this.PortLength * 2 + 1, sessionID, 0, 2);
                return sessionID;
            }
            set
            {
                Buffer.BlockCopy(value, 0, RawData, this.AddrLength * 2 + this.PortLength * 2 + 1, 2);
            }
        }

        public new byte[] PayloadData
        {
            get
            {
                if (State != RProxyPacketState.TCP_Data)
                {
                    throw new Exception("Not TCP_Data");
                }
                byte[] payloadData = new byte[this.RawData.Length - this.AddrLength * 2 + this.PortLength * 2 + 3];
                Buffer.BlockCopy(RawData, this.AddrLength * 2 + this.PortLength * 2 + 3, payloadData, 0, payloadData.Length);
                return payloadData;
            }
            set
            {
                List<Byte> raw = new List<byte>(this.RawData);
                raw.RemoveRange(AddrLength * 2 + PortLength * 2 + 3, raw.Count - AddrLength * 2 - PortLength * 2 - 3);
                raw.AddRange(value);
                this.RawData = raw.ToArray();
            }
        }
    }
}
