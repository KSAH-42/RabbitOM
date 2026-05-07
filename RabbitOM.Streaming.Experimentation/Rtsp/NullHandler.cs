using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class NullHandler : IHandler
    {
        public static readonly NullHandler Current = new NullHandler();

        public void NotifyDataReceived( in Packet packet ) { }
    }
}
