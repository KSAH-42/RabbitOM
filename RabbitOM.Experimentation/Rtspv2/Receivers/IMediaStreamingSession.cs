using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
{
    public interface IMediaStreamingSession: IDisposable
    {
        bool IsOpened { get; }

        bool IsStreamingStarted { get; }

        bool IsReceivingData { get; }

        TimeSpan PingInteral { get; }

        TimeSpan RetryInterval { get; }
        



        bool Open();

        void Close();

        bool SendHeartBeat();

        bool StartStreaming();

        void StopStreaming();
    }
}
