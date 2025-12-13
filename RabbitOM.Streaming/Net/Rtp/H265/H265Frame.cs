using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265Frame : RtpFrame
    {
        public H265Frame( byte[] data ) : base ( data )
        {
        }
    }
}