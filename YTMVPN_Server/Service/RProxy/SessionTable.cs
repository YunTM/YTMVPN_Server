using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace YTMVPN_Server.Service.RProxy
{
    class SessionTable
    {
        private List<SessionItem> items = new List<SessionItem>();

        public void Add(SessionItem item)
        {
            items.Add(item);
        }


    }
}
