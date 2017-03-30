using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;
using YTMVPN_Server.Service.Routing;

namespace YTMVPN_Server.Service.Echo
{
    public class EchoSrv : IService<DataPacket>
    {
        #region Status
        private ESrvStatus status;
        public ESrvStatus Status { get { return status; } }
        #endregion

        #region InputQueue
        private ConcurrentQueue<DataPacket> iQueue;
        public ConcurrentQueue<DataPacket> InputQueue { get { return iQueue; } }
        #endregion


        public EchoSrv()
        {
            //设置状态
            status = ESrvStatus.Initializing;

            //InputQueue
            iQueue = new ConcurrentQueue<DataPacket>();

            //设置状态
            status = ESrvStatus.Initialized;
        }

        public void StartWork()
        {
            if (status == ESrvStatus.Working)
            {
                throw new Exception("EchoSrv: Already Working");
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
                        LogHelper.Logging("EchoSrv: Dequeue!");

                        //交换Src Dst
                        byte[] swap = dp.DstAddr;
                        dp.DstAddr = dp.SrcAddr;
                        dp.SrcAddr = swap;
                        swap = dp.DstPort;
                        dp.DstPort = dp.SrcPort;
                        dp.SrcPort = swap;

                        //交给路由队列
                        //先写死用0号服务 反正就是个echo（逃
                        RoutingSrv.SrvPool[0].InputQueue.Enqueue(dp);

                    }

                }
                //休眠
                Thread.Sleep(10);
            }
            status = ESrvStatus.Stoped;
        }
    }
}
