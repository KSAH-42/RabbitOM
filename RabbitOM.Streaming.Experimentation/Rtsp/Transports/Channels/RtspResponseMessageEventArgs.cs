using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspResponseMessageEventArgs : EventArgs
    {        
        public RtspResponseMessageEventArgs( RtspResponseMessage response  )
        {
            Response = response ?? throw new ArgumentNullException( nameof( response ) );
        }

        public RtspResponseMessage Response { get; }
    }
}
