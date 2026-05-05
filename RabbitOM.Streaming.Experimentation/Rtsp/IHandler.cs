using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IHandler
    {
        void HandleDataReceived( object sender , in InterleavedPacket packet );
    }
}
