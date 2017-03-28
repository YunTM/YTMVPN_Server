using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.Routing
{
    class RoutingService : ServiceInterface<DataPacket, DataPacket>
    {

        public RoutingService()
        {
            status = "";


            //IO Queue
            iQueue = new ConcurrentQueue<DataPacket>();
            oQueue = new ConcurrentQueue<DataPacket>();


        }

        #region Status
        private string status;
        public string Status { get { return status; } }
        #endregion

        #region IO Queue
        private ConcurrentQueue<DataPacket> iQueue;
        private ConcurrentQueue<DataPacket> oQueue;
        public ConcurrentQueue<DataPacket> InputQueue { get { return iQueue; } }
        public ConcurrentQueue<DataPacket> OutputQueue { get { return oQueue; } }
        #endregion

        public void StartWork()
        {
            
        }
        void DoRecv()
        {
            //初始化局部变量
            byte[] destAddr = new byte[Program.options.LogicAddrLength];
            byte[] destPort = new byte[Program.options.LogicPortLength];

            while (true)
            {
                while (!RecvQueue.IsEmpty)
                {
                    //初始化局部变量
                    //尝试从队列取出
                    while (RecvQueue.TryDequeue(out RoutingPackge rp))
                    {
                        LogHelper.Log("RoutingService: Dequeue!");
                        //取得目标地址和端口
                        Buffer.BlockCopy(rp.Data, 0, destAddr, 0, Program.options.LogicAddrLength);  //地址
                        Buffer.BlockCopy(rp.Data, Program.options.LogicAddrLength, destPort, 0, Program.options.LogicPortLength);  //端口

                        //路由
                        ConcurrentQueue<RoutingPackge> outQueue = RoutingTable.GetQueueByAddr(destAddr, destPort);  //!!!注意路由表线程安全
                        if (outQueue != null)
                        {
                            //交给目标队列
                            outQueue.Enqueue(rp);
                        }
                        else
                        {
                            //查询不到路由 目标不可达
                            //!!需要端口没有路由的情况
                            LogHelper.Log("DestAddr Unreachable: " + destAddr + ":" + destPort);  //!!地址格式化
                        }

                    }
                }
                //休眠
                Thread.Sleep(10);
            }
        }
        void DoSend()
        {

        }
    }
}
