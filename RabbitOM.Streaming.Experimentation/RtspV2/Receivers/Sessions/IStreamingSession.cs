using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions
{
    public interface IStreamingSession : ISession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
