using System;

namespace RabbitOM.Net.Rtsp.Apha
{
    public interface IRTSPMediaChannel : IDisposable
    {
        object SyncRoot { get; }
        IRTSPClientConfiguration Configuration { get; }
        IRTSPClientDispatcher Dispatcher { get; }
        bool IsConnected { get; }
        bool IsReceivingPacket { get; }
        bool IsStreamingStarted { get; }


        bool Connect();
        bool Disconnect();
        void Abort();
        bool StartStreaming();
        bool StopStreaming();
        bool KeepAlive();
        bool WaitForConnection(TimeSpan shutdownTimeout);
    }
}
