﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service.RProxy
{
    class RProxyItem
    {
        public byte[] SessionID { get; set; }
        public byte[] Status { get; set; }
        public RProxyItem(byte[] SessionID, byte[] Status)
        {
            this.SessionID = SessionID;
            this.Status = Status;
        }
    }
}
