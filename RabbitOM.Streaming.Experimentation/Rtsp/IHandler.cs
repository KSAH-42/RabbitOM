using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IHandler
    {
        void NotifyDataReceived( RtspPacket packet );
    }
}
