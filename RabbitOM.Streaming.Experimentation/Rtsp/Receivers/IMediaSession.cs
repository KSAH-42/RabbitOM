using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public interface IMediaSession: IDisposable
    {
        bool IsOpened { get; }
        bool IsStreamingStarted { get; }
        bool IsReceivingData { get; }
        TimeSpan IdleTimeout { get; }
        


        bool Open();

        void Close();

        bool CheckStatus();
    }
}
