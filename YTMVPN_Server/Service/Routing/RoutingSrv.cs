using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.Routing
{
    class RoutingSrv : IService<DataPacket>
    {
        public static List<RoutingSrv> SrvPool { get; set; } = new List<RoutingSrv>();

        public RoutingSrv(RoutingTable RoutingTable)
        {
            //设置状态
            status = ESrvStatus.Initializing;

            //路由表 以后要考虑一下多线程 路由表的随时替换
            this.RoutingTable = RoutingTable;

            //InputQueue
            iQueue = new ConcurrentQueue<DataPacket>();

            //设置SrvPool
            SrvPool.Add(this);

            //设置状态
            status = ESrvStatus.Initialized;
        }

        #region RoutingTable
        public RoutingTable RoutingTable { get; set; }
        #endregion

        #region Status
        private ESrvStatus status;
        public ESrvStatus Status { get { return status; } }
        #endregion

        #region InputQueue
        private ConcurrentQueue<DataPacket> iQueue;
        public ConcurrentQueue<DataPacket> InputQueue { get { return iQueue; } }
        #endregion

        public void StartWork()
        {
            if (status == ESrvStatus.Working)
            {
                throw new Exception("RoutingSrv: Already Working");
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoWorking));
        }
        void DoWorking(object args)
        {
            status = ESrvStatus.Working;
            //大循环
            while (status != ESrvStatus.Stoping)
            {
                //循环直到队列为空，之后线程短暂休眠避免抢占cpu时间
                while (!iQueue.IsEmpty)
                {
                    //尝试从队列取出
                    if (iQueue.TryDequeue(out DataPacket dp))
                    {
                        LogHelper.Logging("RoutingSrv: Dequeue!");
                        
                        //路由
                        ConcurrentQueue<DataPacket> oQueue = RoutingTable.GetQueueByAddr(dp.DstAddr, dp.DstPort);  //!!!注意路由表线程安全
                        if (oQueue != null)
                        {
                            //交给目标队列
                            oQueue.Enqueue(dp);
                        }
                        else
                        {
                            //查询不到路由 目标不可达
                            LogHelper.Logging("DestAddr Unreachable: " + dp.DstAddr + ":" + dp.DstPort);  //!!地址格式化
                        }
                    }

                }
                //休眠
                Thread.Sleep(10);
            }
            status = ESrvStatus.Stoped;
        }
    }
}
