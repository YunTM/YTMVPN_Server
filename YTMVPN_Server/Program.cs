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

            //初始化服务
            RoutingSrv routingSrv = new RoutingSrv();
            ForwardSrv forwardSrv = new ForwardSrv();
            EchoSrv echoSrv = new EchoSrv();

            //路由表 写死
            RoutingSrv.RoutingTable.Add(new RoutingItem(config.Logic_LocalAddr, EchoSrv.SrvPool[0].InputQueue));  //地址本地 端口无 Echo服务
            RoutingSrv.RoutingTable.Add(new RoutingItem(new byte[1] { 0x00 }, ForwardSrv.SrvPool[0].InputQueue, new byte[1] { 0x00 }));  //地址0x00 端口0x00 Forward服务
            
            //转发表 写死
            ForwardSrv.ForwardTable.Add(new ForwardItem(new byte[1] { 0x00 }, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50000), new byte[1]{0x00}));  //地址0x00 端口0x00 远程127.0.0.1:50000

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