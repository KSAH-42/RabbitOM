using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IEventSink
    {
        void NotifyDataReceived( in InterleavedPacket packet );
    }
}
