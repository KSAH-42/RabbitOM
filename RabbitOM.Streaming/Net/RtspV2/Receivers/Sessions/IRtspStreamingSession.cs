using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions
{
    public interface IRtspStreamingSession : IRtspSession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
