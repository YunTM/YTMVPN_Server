using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;
using YTMVPN_Server.Service.Forward;
using YTMVPN_Server.Service.Routing;

namespace YTMVPN_Server
{
    public static class AuthSocket
    {
        private static Config config;
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public static Socket Socket { get { return socket; } }

        private static Thread tWorking = new Thread(Working);

        public static void Start(ref Config config)
        {
            AuthSocket.config = config;
            #region 修复bug UDP出现 SocketException 10054
            const uint IOC_IN = 0x80000000;
            int IOC_VENDOR = 0x18000000;
            int SIO_UDP_CONNRESET = (int)(IOC_IN | IOC_VENDOR | 12);
            socket.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, new byte[4]);
            #endregion

            //绑定
            socket.Bind(new IPEndPoint(IPAddress.Parse(config.IP_Address), config.IP_AuthPort));
            //懒得写异步，直接丢给线程
            tWorking.IsBackground = true;
            tWorking.Start();
        }

        public static void Abort()
        {
            //强制停止线程
            throw new Exception("尼玛没有Abort，先不做相关处理吧");
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
                AuthPacket ap = new AuthPacket(buffer);
                
#if DEBUG
                LogHelper.Logging("\nRecvAuthPacket" +
                                  "\n\tSize: " + ap.RawData.Length +
                                  "\n\tState: " + BitConverter.ToString(new byte[] { (byte)ap.State }) +
                                  "\n");
#endif

                //握手
                switch (ap.State)
                {
                    case AuthPacketState.Hello:
                        //回应Hello_ACK
                        ap.State = AuthPacketState.Hello_ACK;
                        ap.LogicAddr = config.Logic_LocalAddr;
                        socket.SendTo(ap.RawData, remoteEP);
                        break;
                    case AuthPacketState.Hello_Succsss:
                        //加入转发表 这里还要加判断之前客户端是否有请求 Hello
                        ForwardSrv.ForwardTable.Add(new ForwardItem(ap.LogicAddr, remoteEP));
                        ap.State = AuthPacketState.Hello_Done;
                        socket.SendTo(ap.RawData, remoteEP);
                        break;
                    default:
#if DEBUG
                        LogHelper.Logging("Unknow AuthPacketState");
#endif
                        break;
                }


            }
        }
    }
}

