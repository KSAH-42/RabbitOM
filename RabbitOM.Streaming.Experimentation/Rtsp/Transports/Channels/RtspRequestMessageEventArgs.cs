using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspRequestMessageEventArgs : EventArgs
    {        
        public RtspRequestMessageEventArgs( RtspRequestMessage request )
        {
            Request = request ?? throw new ArgumentNullException( nameof( request ) );
        }

        public RtspRequestMessage Request { get; }
    }
}
