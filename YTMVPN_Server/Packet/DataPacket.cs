using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Packet
{
    //数据包的封装
    class DataPacket
    {
        byte[] rawData; //这个包的raw数据
        byte[] dstAddr; //目标地址
        byte[] srcAddr; //源地址
        byte[] payloadData; //载荷数据
        
        

    }
}
