using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpBuildFrameEventArgs : RtpBuildEventArgs
    {
        public RtpBuildFrameEventArgs( RtpFrame frame ) => Frame = frame ?? throw new ArgumentNullException( nameof( frame ) );
        
        public RtpFrame Frame { get; }
    }
}
