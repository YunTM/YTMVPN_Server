using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;
using YTMVPN_Server.Service.Routing;

namespace YTMVPN_Server
{
    static class DataSocket
    {
        private static Config config;
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public static Socket Socket { get { return socket; } }

        private static Thread tWorking = new Thread(Working);

        public static void Start(ref Config config)
        {
            DataSocket.config = config;
            #region 修复bug UDP出现 SocketException 10054
            const uint IOC_IN = 0x80000000;
            int IOC_VENDOR = 0x18000000;
            int SIO_UDP_CONNRESET = (int)(IOC_IN | IOC_VENDOR | 12);
            socket.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, new byte[4]);
            #endregion

            //绑定
            socket.Bind(new IPEndPoint(IPAddress.Parse(config.IP_Address), config.IP_DataPort));
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
                byte[] buffer = new byte[4096];  //省略MTU和缓冲区设置 是的 我也先作死new
                EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                //接收数据
                int count = socket.ReceiveFrom(buffer, ref remoteEP);

                //省略对分段包的处理
                DataPacket dp = new DataPacket(config.Logic_AddrLength, config.Logic_PortLength, buffer, count);
                LogHelper.Logging("\nRecvData" +
                                  "\n\tSize: " + dp.RawData.Length +
                                  "\n\tDstAddr: " + BitConverter.ToString(dp.DstAddr) +
                                  "\n\tSrcAddr: " + BitConverter.ToString(dp.SrcAddr) +
                                  "\n\tDstPort: " + BitConverter.ToString(dp.DstPort) +
                                  "\n\tSrcPort: " + BitConverter.ToString(dp.SrcPort) +
                                  "\n\tPayloadData: " + BitConverter.ToString(dp.PayloadData) +
                                  "\n");

                //丢给路由队列
                RoutingSrv.SrvPool[0].InputQueue.Enqueue(dp);

            }
        }
    }
}
