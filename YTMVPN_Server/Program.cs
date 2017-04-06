using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;
using YTMVPN_Server.Service.Echo;
using YTMVPN_Server.Service.Forward;
using YTMVPN_Server.Service.Routing;

namespace YTMVPN_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //config.json
            Config config = Config.GetConfig();

            //设置控制台输出为UTF8
            Console.OutputEncoding = Encoding.UTF8;

            //认证Socket绑定
            //Socket authSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            //authSocket.Bind(new IPEndPoint(IPAddress.Parse(Config.IP_Address), Config.IP_AuthPort));
            //byte[] authBuffer = new byte[4096];
            //EndPoint authRemoteEP = new IPEndPoint(IPAddress.Any,0);

            //省略端口复用
            //省略可靠
            //省略加密
            //省略认证

            //接收认证消息
            //int authCount = authSocket.ReceiveFrom(authBuffer, ref authRemoteEP);

            //初始化服务
            RoutingSrv routingSrv = new RoutingSrv(new RoutingTable());
            ForwardSrv forwardSrv = new ForwardSrv(new ForwardTable());
            EchoSrv echoSrv = new EchoSrv();

            //修改路由表和转发表
            //同样写死先
            routingSrv.RoutingTable.Add(new RoutingItem(config.Logic_LocalAddr, EchoSrv.SrvPool[0].InputQueue));

            routingSrv.RoutingTable.Add(new RoutingItem(new byte[1] { 0x00 }, ForwardSrv.SrvPool[0].InputQueue, new byte[1] { 0x00 }));
            forwardSrv.ForwardTable.Add(new ForwardItem(new byte[1] { 0x00 }, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50000), new byte[1]{0x00}));

            //启动服务
            routingSrv.StartWork();
            forwardSrv.StartWork();
            echoSrv.StartWork();

            DataSocket.Start(ref config);
            //调试循环
            while (true)
            {
                Thread.Sleep(100);
            }

        }
    }

}