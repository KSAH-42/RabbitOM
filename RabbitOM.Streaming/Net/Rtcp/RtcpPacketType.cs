using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public enum RtcpPacketType : byte
    {
        UNDEFINED = 0,
        SR = 200 ,
        RR = 201 ,
        SDES = 202 ,
        BYTE = 203 ,
        APP = 204 ,
    }
}
