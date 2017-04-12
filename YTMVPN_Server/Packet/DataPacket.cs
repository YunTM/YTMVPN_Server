using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Packet
{
    //数据包的封装
    class DataPacket
    {
        //包的一些参数 必须指定
        public int AddrLength { get; set; }
        public int PortLength { get; set; }

        public DataPacket(int AddrLength, int PortLength, byte[] rawData = null)
        {
            this.AddrLength = AddrLength;
            this.PortLength = PortLength;
            this.rawData = rawData;
        }

        //构造重载 设置rawData时截取部分长度
        public DataPacket(int AddrLength, int PortLength, byte[] rawData,int Length)
        {
            this.AddrLength = AddrLength;
            this.PortLength = PortLength;
            this.rawData = new byte[Length];
            Buffer.BlockCopy(rawData, 0, this.rawData, 0, Length);
        }

        //这个包的raw数据
        private byte[] rawData;
        public byte[] RawData {
            get { return rawData; }
            set { rawData = value; }
        }

        //目标地址
        public byte[] DstAddr {
            get {
                byte[] dstAddr = new byte[AddrLength];
                Buffer.BlockCopy(rawData, 0, dstAddr, 0, dstAddr.Length);
                return dstAddr;
            }
            set {
                for (int i = 0; i < AddrLength; i++)
                {
                    rawData[i] = value[i];
                }
            }
        }

        //源地址
        public byte[] SrcAddr
        {
            get
            {
                byte[] srcAddr = new byte[AddrLength];
                Buffer.BlockCopy(rawData, AddrLength, srcAddr, 0, srcAddr.Length);
                return srcAddr;
            }
            set
            {
                for (int i = 0; i < AddrLength; i++)
                {
                    rawData[i + AddrLength] = value[i];
                }
            }
        }

        //目标端口
        public byte[] DstPort
        {
            get
            {
                byte[] dstPort = new byte[PortLength];
                Buffer.BlockCopy(rawData, AddrLength * 2, dstPort, 0, dstPort.Length);
                return dstPort;
            }
            set
            {
                for (int i = 0; i < PortLength; i++)
                {
                    rawData[i + AddrLength * 2] = value[i];
                }
            }
        }

        //源端口
        public Byte[] SrcPort
        {
            get
            {
                byte[] srcPort = new byte[PortLength];
                Buffer.BlockCopy(rawData, AddrLength * 2 + PortLength, srcPort, 0, srcPort.Length);
                return srcPort;
            }
            set
            {
                for (int i = 0; i < PortLength; i++)
                {
                    rawData[i + AddrLength * 2 + PortLength] = value[i];
                }
            }
        }

        //载荷数据
        public byte[] PayloadData {
            get {
                byte[] payloadData = new byte[rawData.Length - AddrLength * 2 - PortLength * 2];
                Buffer.BlockCopy(rawData, AddrLength * 2 + PortLength * 2 , payloadData, 0, payloadData.Length);
                return payloadData;
            }
            set {
                List<Byte> raw = new List<byte>(rawData);
                raw.RemoveRange(AddrLength * 2 + PortLength * 2, raw.Count - AddrLength * 2 - PortLength * 2);
                raw.AddRange(value);
                rawData = raw.ToArray();
            }
        }
        
        

    }
}
