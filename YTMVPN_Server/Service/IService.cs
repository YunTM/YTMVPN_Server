using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server.Service
{
    public interface IService<InputType>
    {
        ESrvStatus Status { get; }
        ConcurrentQueue<InputType> InputQueue { get; }

        void StartWork();
        //void StopWork();
        //void Suspend();


    }

}
