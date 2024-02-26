using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal interface IRTSPMediaChannel : IDisposable
    {
        object SyncRoot { get; }
        RTSPPipeLineCollection PipeLines { get; }
        IRTSPClientConfiguration Configuration { get; }
        IRTSPEventDispatcher Dispatcher { get; }

        bool IsConnected { get; }
        bool IsOpened { get; }
        bool IsSetup { get; }
        bool IsPlaying { get; }
        bool IsReceivingPacket { get; }

        bool Open();
        void Close();
        void Abort();
        bool Options();
        bool Describe();
        bool Setup();
        bool Play();
        void TearDown();
        bool KeepAlive();
    }
}
