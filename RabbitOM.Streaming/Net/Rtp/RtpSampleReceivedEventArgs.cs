using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpSampleReceivedEventArgs : EventArgs
    {
        public RtpSampleReceivedEventArgs( RtpSample sample )
        {
            Sample = sample ?? throw new ArgumentNullException( nameof( sample ) );
        }

        public DateTime TimeStamp { get; } = DateTime.Now;
        public RtpSample Sample { get; }
    }
}
