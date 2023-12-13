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
        bool Close();
        void Abort();
        bool StartStreaming();
        bool StopStreaming();
        bool Ping();
        bool WaitForConnection(TimeSpan shutdownTimeout);
    }
}
