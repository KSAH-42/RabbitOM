using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Sessions
{
    public interface ISession: IDisposable
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
