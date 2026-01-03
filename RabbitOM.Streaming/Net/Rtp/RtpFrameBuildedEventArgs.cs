using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpFrameBuildedEventArgs : RtpBuildEventArgs
    {
        public RtpFrameBuildedEventArgs( RtpFrame frame ) => Frame = frame ?? throw new ArgumentNullException( nameof( frame ) );
        
        public RtpFrame Frame { get; }
    }
}
