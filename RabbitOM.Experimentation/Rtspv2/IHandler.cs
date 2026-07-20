using System;

namespace RabbitOM.Streaming.RtspV2
{
    public interface IHandler
    {
        void NotifyDataReceived( RtspPacket packet );
    }
}
