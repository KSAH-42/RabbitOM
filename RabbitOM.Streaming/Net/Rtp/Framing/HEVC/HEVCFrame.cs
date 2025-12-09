using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrame : RtpFrame
    {
        public HEVCFrame( byte[] data ) : base ( data )
        {
        }
    }
}