using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public class H265Frame : RtpFrame
    {
        public H265Frame( byte[] data ) : base ( data )
        {
        }
    }
}