using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265Frame : RtpFrame
    {
        private readonly byte[] _vps;
        private readonly byte[] _sps;
        private readonly byte[] _pps;

        public H265Frame( byte[] data , byte[] vps , byte[] sps , byte[] pps ) : base ( data )
        {
            _vps = vps ?? throw new ArgumentNullException( nameof( vps ) );
            _sps = sps ?? throw new ArgumentNullException( nameof( sps ) );
            _pps = pps ?? throw new ArgumentNullException( nameof( pps ) );
        }

        public byte[] VPS
        {
            get => _vps;
        }

        public byte[] SPS
        {
            get => _sps;
        }

        public byte[] PPS
        {
            get => _pps;
        }
    }
}