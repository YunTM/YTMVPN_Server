using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server.Service
{
    public enum ESrvStatus
    {
        NotInit = 0,
        Initializing = 1,
        Initialized = 2,
        Working = 3,
        Stoped = 4,
        Suspended = 5,
    }
}
