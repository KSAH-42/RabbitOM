using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Sessions
{
    public interface IStreamingSession : ISession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
