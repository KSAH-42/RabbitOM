using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public enum RtspClientErrorCode
    {
        Unknown = 0,
        ConnectionFailed,
        GetOptionsFailed,
        DescribeFailed,
        SetupFailed,
        PlayFailed,
        TransportOpenFailed,
        KeepAliveFailed ,
        PingFailed,
    }
}
