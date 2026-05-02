using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IEventSink
    {
        void OnDataReceived( in InterleavedPacket packet );
    }
}
