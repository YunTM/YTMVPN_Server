using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace YTMVPN_Server.Service.Forward
{
    class ForwardTable
    {
        private List<ForwardItem> items = new List<ForwardItem>();

        public void Add(ForwardItem item)
        {
            items.Add(item);
        }

        /// <summary>
        /// 查询转发表
        /// 支持端口匹配查询，若没有匹配的端口或DstPort为null，返回相应的DstAddr查询结果。
        /// DstAddr没有匹配结果将返回null。
        /// </summary>
        /// <param name="DstAddr">注意：Big Endian字节序</param>
        /// <param name="DstPort">注意：Big Endian字节序</param>
        /// <returns></returns>
        public ForwardItem Query(byte[] DstAddr, byte[] DstPort = null)
        {
            //注意此函数可能会返回null
            if (DstPort == null)
            {
                //不带端口的查询
                return items.Find(item => item.DstAddr.SequenceEqual(DstAddr));
            }
            else
            {
                //带端口的查询
                ForwardItem tmp = items.Find(delegate (ForwardItem item)
                {
                    if (item.DstPort == null)
                    {
                        return false;
                    }
                    return item.DstAddr.SequenceEqual(DstAddr) && item.DstPort.SequenceEqual(DstPort);
                });

                if (tmp != null)
                {
                    return tmp;
                }
                else
                {
                    //如果没有找到匹配端口的条目将尝试查找匹配地址的条目
                    return items.Find(item => item.DstAddr.SequenceEqual(DstAddr));
                }
            }
        }


        /// <summary>
        /// 查表并返回EndPoint
        /// 没有匹配的结果会返回null
        /// </summary>
        /// <param name="DstAddr">注意：Big Endian字节序</param>
        /// <returns></returns>
        public EndPoint GetQueueByAddr(byte[] DstAddr, byte[] DstPort = null)
        {
            //注意此函数可能会返回null
            ForwardItem tmp = Query(DstAddr, DstPort);
            if (tmp != null)  //避免null引用
            {
                return tmp.DstEndPoint;
            }
            else
            {
                return null;
            }

        }
    }
}
