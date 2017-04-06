using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace YTMVPN_Server.Packet
{
    //数据包的封装
    class AuthPacket
    {
        //这个包的raw数据
        private byte[] rawData;
        public byte[] RawData
        {
            get { return rawData; }
            set { rawData = value; }
        }

        public PacketFlags Flag
        {
            get { return (PacketFlags)rawData[0]; }
            set { rawData[0] = (byte)value; }
        }

        public byte[] LogicAddr
        {
            get
            {
                if (Flag != PacketFlags.Hello && Flag != PacketFlags.Hello_ACK)
                {
                    throw new Exception("Not Hello and Hello_ACK");
                }
                byte[] LogicAddr = new byte[rawData.Length - 1];
                Buffer.BlockCopy(rawData, 1, rawData, 0, LogicAddr.Length);
                return LogicAddr;
            }
            set
            {
                if (Flag != PacketFlags.Hello && Flag != PacketFlags.Hello_ACK)
                {
                    throw new Exception("Not Hello and Hello_ACK");
                }
                Buffer.BlockCopy(value, 1, rawData, 0, value.Length);
            }
        }

        public AuthPacket(byte[] rawData = null)
        {
            this.rawData = rawData;
        }



    }
}
