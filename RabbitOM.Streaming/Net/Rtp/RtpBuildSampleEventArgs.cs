using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpBuildSampleEventArgs : RtpBuildEventArgs
    {
        public RtpBuildSampleEventArgs( RtpSample sample ) => Sample = sample ?? throw new ArgumentNullException( nameof( sample ) );
        public RtpSample Sample { get; }
    }
}
