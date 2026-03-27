using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers
{
    public interface IMediaStreamingSession : IMediaSession
    {
        bool StartStreaming();

        bool StopStreaming();
    }
}
