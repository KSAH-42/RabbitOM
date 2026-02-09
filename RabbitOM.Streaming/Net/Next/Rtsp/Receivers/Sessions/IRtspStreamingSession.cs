using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Receivers.Sessions
{
    public interface IRtspStreamingSession : IRtspSession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
