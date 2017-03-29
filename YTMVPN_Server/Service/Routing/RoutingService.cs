using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
            //大循环
            while (true)
            {
                //循环直到队列为空，之后线程短暂休眠避免抢占cpu时间
                while (!iQueue.IsEmpty)
                {
                    //尝试从队列取出
                    if (iQueue.TryDequeue(out DataPacket dp))
                    {
                        LogHelper.Logging("RoutingService: Dequeue!");
                        
                        //路由
                        ConcurrentQueue<DataPacket> oQueue = RoutingTable.GetQueueByAddr(destAddr, destPort);  //!!!注意路由表线程安全
                        if (oQueue != null)
                        {
                            //交给目标队列
                            oQueue.Enqueue(dp);
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
