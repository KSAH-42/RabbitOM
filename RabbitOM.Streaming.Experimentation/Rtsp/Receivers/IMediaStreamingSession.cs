using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public interface IMediaStreamingSession : IMediaSession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
