using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server
{
    public interface ServiceInterface<InputType,OutputType>
    {
        String Status { get; }
        ConcurrentQueue<InputType> InputQueue { get; }
        ConcurrentQueue<OutputType> OutputQueue { get; }

        void StartWork();
        //void StopWork();
        //void Suspend();


    }

}
