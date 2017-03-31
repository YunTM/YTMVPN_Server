using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using YTMVPN_Server.Service.Routing;

namespace YTMVPN_Server
{
    static class DataReceiver
    {
        private static Socket dataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private static Thread tWorking = new Thread(Working);

        public static void Start()
        {
            dataSocket.Bind(new IPEndPoint(IPAddress.Parse(Config.IP_Address), Config.IP_DataPort));
            //懒得写异步，直接丢给线程
            tWorking.IsBackground = true;
            tWorking.Start();
        }

        public static void Abort()
        {
            //强制停止线程
            tWorking.Start();
        }

        private static void Working()
        {
            while (true)
            {
                byte[] dataBuffer = new byte[4096];  //省略MTU和缓冲区设置 是的 我也先作死new
                EndPoint dataRemoteEP = new IPEndPoint(IPAddress.Any, 0);
                //接收数据
                int dataCount = dataSocket.ReceiveFrom(dataBuffer, ref dataRemoteEP);
                //丢给路由队列
                RoutingSrv.SrvPool[0].InputQueue.Enqueue(dp)
            }
        }
    }
}
