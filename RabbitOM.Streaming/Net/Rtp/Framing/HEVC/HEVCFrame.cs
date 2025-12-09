using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrame : RtpFrame
    {
        public HEVCFrame( byte[] data , byte[] pps , byte[] sps , byte[] vps ) 
            : base ( data )
        {
            if ( pps == null || pps.Length == 0 )
            {
                throw new ArgumentNullException( nameof( pps ) );
            }

            if ( sps == null || sps.Length == 0 )
            {
                throw new ArgumentNullException( nameof( sps ) );
            }

            if ( vps == null || vps.Length == 0 )
            {
                throw new ArgumentNullException( nameof( vps ) );
            }

            PPS = pps;
            SPS = sps;
            VPS = vps;
        }

        public byte[] PPS { get; }
        public byte[] SPS { get; }
        public byte[] VPS { get; }
    }
}