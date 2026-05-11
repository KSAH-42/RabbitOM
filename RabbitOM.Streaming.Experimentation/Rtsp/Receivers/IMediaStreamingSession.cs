using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public interface IMediaStreamingSession: IDisposable
    {
        bool IsOpened { get; }

        bool IsStreamingStarted { get; }

        bool IsReceivingData { get; }

        TimeSpan PingInteral { get; }

        TimeSpan RetryInteral { get; }
        



        bool Open();

        void Close();

        bool SendHeartBeat();

        bool StartStreaming();

        void StopStreaming();
    }
}
