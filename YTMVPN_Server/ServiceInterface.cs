using System;
using System.Collections.Generic;
using System.Text;
using YTMVPN_Server.Packet;

namespace YTMVPN_Server
{
    public interface ServiceInterface<InputType,OutputType>
    {
        String Status { get; }
        Queue<InputType> InputQueue { get; }
        Queue<OutputType> OutputQueue { get; }
        
    }
}
