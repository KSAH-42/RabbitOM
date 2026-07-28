using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public struct H264Options
    {
        public H264Options( byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] extraParameters )
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

            if ( extraParameters == null || extraParameters.Length <= 0 )
            {
                throw new ArgumentException( nameof( extraParameters ) );
            }

            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
            ExtraParameters = extraParameters;
        }

        public byte[] StartCodePrefix { get; }

        public byte[] PPS { get; }

        public byte[] SPS { get; }

        public byte[] ExtraParameters { get; }
    }
}