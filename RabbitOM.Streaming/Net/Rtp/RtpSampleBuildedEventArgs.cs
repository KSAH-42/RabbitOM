using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpSampleBuildedEventArgs : RtpBuildEventArgs
    {
        public RtpSampleBuildedEventArgs( RtpSample sample ) => Sample = sample ?? throw new ArgumentNullException( nameof( sample ) );
        public RtpSample Sample { get; }
    }
}
