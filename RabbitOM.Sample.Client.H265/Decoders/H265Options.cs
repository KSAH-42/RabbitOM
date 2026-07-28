using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public struct H265Options
    {
        public H265Options( byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] vps , byte[] extraParameters )
        {
            if ( startCodePrefix == null || startCodePrefix.Length <= 0 )
            {
                throw new ArgumentException( nameof( startCodePrefix ) );
            }

            if ( pps == null || pps.Length <= 0 )
            {
                throw new ArgumentException( nameof( pps ) );
            }

            if ( sps == null || sps.Length <= 0 )
            {
                throw new ArgumentException( nameof( sps ) );
            }

            if ( vps == null || vps.Length <= 0 )
            {
                throw new ArgumentException( nameof( vps ) );
            }

            if ( extraParameters == null || extraParameters.Length <= 0 )
            {
                throw new ArgumentException( nameof( extraParameters ) );
            }

            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
            VPS = vps;
            ExtraParameters = extraParameters;
        }

        public byte[] StartCodePrefix { get; }

        public byte[] PPS { get; }

        public byte[] SPS { get; }

        public byte[] VPS { get; }

        public byte[] ExtraParameters { get; }
    }
}