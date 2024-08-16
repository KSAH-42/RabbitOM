using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265Element
    {
		public H265Element( RtpPacket packet , H265NalUnit nalUnit )
		{
            Packet  = packet  ?? throw new ArgumentNullException( nameof( packet  ) ) ;
            NalUnit = nalUnit ?? throw new ArgumentNullException( nameof( nalUnit ) );
        }

        public RtpPacket Packet { get; }

        public H265NalUnit NalUnit { get; }
    } 
}