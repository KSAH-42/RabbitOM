using System;

namespace RabbitOM.Net.Rtsp.Clients
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
