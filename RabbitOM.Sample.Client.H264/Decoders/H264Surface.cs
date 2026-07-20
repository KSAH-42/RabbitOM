using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public struct H264Surface
    {
        public H264Surface( byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] extraParameters , object targetControl )
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

            if ( targetControl == null )
            {
                throw new ArgumentException( nameof( targetControl ) );
            }

            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
            ExtraParameters = extraParameters;
            TargetControl = targetControl;
        }

        public byte[] StartCodePrefix { get; }

        public byte[] PPS { get; }

        public byte[] SPS { get; }

        public byte[] ExtraParameters { get; }

        public object TargetControl { get; }
    }
}