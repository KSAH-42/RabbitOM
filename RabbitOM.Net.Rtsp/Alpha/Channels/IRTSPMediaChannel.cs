using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public interface IRTSPMediaChannel : IDisposable
    {
        object SyncRoot { get; }
        IRTSPClientConfiguration Configuration { get; }
        IRTSPEventDispatcher Dispatcher { get; }

        bool IsConnected { get; }
        bool IsOpened { get; }
        bool IsReceivingPacket { get; }
        bool IsSetup { get; }
        bool IsPlaying { get; }

        bool Open();
        bool Close();
        void Abort();
        bool Options();
        bool Describe();
        bool Setup();
        bool Play();
        void TearDown();
        bool KeepAlive();
        bool WaitForConnection(TimeSpan shutdownTimeout);
    }
}
