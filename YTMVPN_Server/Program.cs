using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;
using YTMVPN_Server.Service.Echo;
using YTMVPN_Server.Service.Routing;

namespace YTMVPN_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置控制台输出为UTF8
            Console.OutputEncoding = Encoding.UTF8;

            //参数和配置文件的解析 先不写了，参数写死先
            Config.IP_Address = "0.0.0.0";
            Config.IP_AuthPort = 52145;
            Config.IP_DataPort = 52146;

            Config.Logic_AddrLength = 1;
            Config.Logic_PortLength = 1;
            Config.Logic_LocalAddr = new byte[1] { 0xFF };


            //认证Socket绑定
            Socket authSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            authSocket.Bind(new IPEndPoint(IPAddress.Parse(Config.IP_Address), Config.IP_AuthPort));
            byte[] authBuffer = new byte[4096];
            EndPoint authRemoteEP = new IPEndPoint(IPAddress.Any,0);

            

            //省略端口复用
            //省略可靠
            //省略加密
            //省略认证




            //数据Socket绑定
            Socket dataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            dataSocket.Bind(new IPEndPoint(IPAddress.Parse(Config.IP_Address), Config.IP_DataPort));
            byte[] dataBuffer = new byte[4096];  //省略MTU和缓冲区设置
            EndPoint dataRemoteEP = new IPEndPoint(IPAddress.Any, 0);

            //接收认证消息
            int authCount = authSocket.ReceiveFrom(authBuffer, ref authRemoteEP);

            //接收数据
            int dataCount = dataSocket.ReceiveFrom(dataBuffer, ref dataRemoteEP);

            //省略对分段包的处理

            DataPacket dataPacket =  new DataPacket(Config.Logic_AddrLength, Config.Logic_PortLength, dataBuffer, dataCount);

            LogHelper.Logging("\nRecvData" +
                              "\n\tSize: " + dataPacket.RawData.Length +
                              "\n\tDstAddr: " + BitConverter.ToString(dataPacket.DstAddr) +
                              "\n\tSrcAddr: " + BitConverter.ToString(dataPacket.SrcAddr) +
                              "\n\tDstPort: " + BitConverter.ToString(dataPacket.DstPort) +
                              "\n\tSrcPort: " + BitConverter.ToString(dataPacket.SrcPort) +
                              "\n\tPayloadData: " + BitConverter.ToString(dataPacket.PayloadData) +
                              "\n");


            //初始化服务
            RoutingSrv routingSrv = new RoutingSrv(new RoutingTable());

            //修改路由表
            //同样写死先
            routingSrv.RoutingTable.Add(new RoutingItem(Config.Logic_LocalAddr, EchoSrv.SrvPool[0].InputQueue));

            //启动服务
            routingSrv.StartWork();


            //调试循环
            while (true)
            {
                Thread.Sleep(100);
            }

        }
    }

}