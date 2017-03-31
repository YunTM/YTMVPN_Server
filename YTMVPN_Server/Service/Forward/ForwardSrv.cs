using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.Forward
{
    class ForwardSrv : IService<DataPacket>
    {
        public static List<ForwardSrv> SrvPool { get; set; } = new List<ForwardSrv>();

        #region Status
        private ESrvStatus status;
        public ESrvStatus Status { get { return status; } }
        #endregion

        #region InputQueue
        private ConcurrentQueue<DataPacket> iQueue;
        public ConcurrentQueue<DataPacket> InputQueue { get { return iQueue; } }
        #endregion


        public ForwardSrv()
        {
            //设置状态
            status = ESrvStatus.Initializing;

            //InputQueue
            iQueue = new ConcurrentQueue<DataPacket>();

            //设置SrvPool
            SrvPool.Add(this);

            //设置状态
            status = ESrvStatus.Initialized;
        }

        public void StartWork()
        {
            if (status == ESrvStatus.Working)
            {
                throw new Exception("ForwardSrv: Already Working");
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
                        LogHelper.Logging("ForwardSrv: Dequeue!");

                        //处理数据发送
                        

                    }

                }
                //休眠
                Thread.Sleep(10);
            }
            status = ESrvStatus.Stoped;
        }
    }
}
