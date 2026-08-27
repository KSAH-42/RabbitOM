using System;

namespace RabbitOM.Net.RtspV2
{
    public interface IHandler
    {
        void NotifyDataReceived( RtspPacket packet );
    }
}
