using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public interface IStreamingSession : ISession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
