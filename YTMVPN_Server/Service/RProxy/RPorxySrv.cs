﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.RProxy
{
    class RProxySrv : IService<DataPacket>
    {
        public static List<RProxySrv> SrvPool { get; set; } = new List<RProxySrv>();

        #region SessionTable
        private SessionTable sessionTable = new SessionTable();
        public SessionTable SessionTable
        {
            get
            {
                return sessionTable;
            }
        }
        #endregion

        #region Status
        private ESrvStatus status;
        public ESrvStatus Status { get { return status; } }
        #endregion

        #region InputQueue
        private ConcurrentQueue<DataPacket> iQueue;
        public ConcurrentQueue<DataPacket> InputQueue { get { return iQueue; } }
        #endregion

        #region ListenPort

        private byte[] listenPort;

        public byte[] ListenPort
        {
            get { return listenPort; }
        }
        #endregion

        #region DstEP
        private EndPoint dstEP;

        public EndPoint DstEP
        {
            get { return dstEP; }
        }

        #endregion

        public RProxySrv()
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
                throw new Exception("RProxySrv: Already Working");
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoWorking));
            ThreadPool.QueueUserWorkItem(); //维护会话表
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
#if DEBUG
                        LogHelper.Logging("RProxySrv: Dequeue!");
#endif
                        switch (((RProxyPacket)dp).State)
                        {
                            case RProxyPacketState.TCP_Connect:
                                sessionTable.Add(new SessionItem())
                                break;
                            case RProxyPacketState.RProxyPacket:
                                break;
                            case RProxyPacketState.TCP_Data:
                                break;
                            case RProxyPacketState.TCP_Fin:
                                break;
                            case RProxyPacketState.TCP_Rst:
                                break;
                            default:
                                break;
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
