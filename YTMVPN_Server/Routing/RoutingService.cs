using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Routing
{
    class RoutingService : ServiceInterface<DataPacket, DataPacket>
    {

        public RoutingService()
        {
            status = ""

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
            throw new NotImplementedException();
        }
    }
}
