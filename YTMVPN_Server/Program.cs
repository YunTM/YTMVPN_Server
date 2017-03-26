using System;
using System.Net;
using System.Net.Sockets;

namespace YTMVPN_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //参数和配置文件的解析 先不写了，参数写死先
            Config.IP_Address = "0.0.0.0";
            Config.IP_AuthPort = 52145;

            //认证Socket绑定
            Socket authSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            authSocket.Bind(new IPEndPoint(IPAddress.Parse(Config.IP_Address), Config.IP_AuthPort));
            byte[] authBuffer = new byte[4096];
            EndPoint authRemoteEP = new IPEndPoint(IPAddress.Any,0);

            //接收认证消息
            int count = authSocket.ReceiveFrom(authBuffer, ref authRemoteEP);

            //省略端口复用
            //省略加密
            //省略认证





            //数据Socket绑定
            Socket dataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            dataSocket.Bind(new IPEndPoint(IPAddress.Parse(Config.IP_Address), Config.IP_DataPort));
            byte[] dataBuffer = new byte[4096];
            EndPoint dataRemoteEP = new IPEndPoint(IPAddress.Any, 0);

            //接收数据
            dataSocket.ReceiveFrom(dataBuffer, ref dataRemoteEP);
            



        }
    }

}