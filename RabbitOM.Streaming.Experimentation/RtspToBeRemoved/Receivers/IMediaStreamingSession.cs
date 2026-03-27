using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Receivers
{
    public interface IMediaStreamingSession : IMediaSession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
