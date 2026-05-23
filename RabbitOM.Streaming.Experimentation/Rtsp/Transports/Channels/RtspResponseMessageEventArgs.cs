using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging;

    public sealed class RtspResponseMessageEventArgs : EventArgs
    {        
        public RtspResponseMessageEventArgs( RtspResponseMessage response  )
        {
            Response = response ?? throw new ArgumentNullException( nameof( response ) );
        }

        public RtspResponseMessage Response { get; }
    }
}
