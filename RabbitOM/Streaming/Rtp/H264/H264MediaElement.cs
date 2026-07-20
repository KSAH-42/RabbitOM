using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.H264
{
    public sealed class H264MediaElement : RtpMediaElement
    {
        public H264MediaElement( byte[] buffer , byte[] startCodePrefix , byte[] sps , byte[] pps ) : base ( buffer )
        {
            if ( startCodePrefix == null )
            {
                throw new ArgumentNullException(  nameof( startCodePrefix ) );
            }

            if ( sps == null )
            {
                throw new ArgumentNullException(  nameof( sps ) );
            }

            if ( pps == null )
            {
                throw new ArgumentNullException(  nameof( pps ) );
            }

            StartCodePrefix = startCodePrefix.Length > 0 ? startCodePrefix : throw new ArgumentException( nameof( startCodePrefix ) );

            SPS = sps.Length > 0 ? sps : throw new ArgumentException( nameof( sps ) );

            PPS = pps.Length > 0 ? pps : throw new ArgumentException( nameof( pps ) );
        }






        public byte[] StartCodePrefix { get; }

        public byte[] SPS { get; }

        public byte[] PPS { get; }







        public static byte[] CreateExtraParameters( H264MediaElement frame )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            return CreateExtraParameters( frame.StartCodePrefix , frame.SPS , frame.PPS );
        }

        public static byte[] CreateExtraParameters( byte[] startCodePrefix , byte[] sps , byte[] pps )
        {
            if ( startCodePrefix == null )
            {
                throw new ArgumentNullException( nameof( startCodePrefix ) );
            }

            if ( startCodePrefix.Length == 0 )
            {
                throw new ArgumentException( nameof( startCodePrefix ) );
            }

            var result = new List<byte>( 50 );

            if ( sps?.Length > 0 )
            {
                result.AddRange( startCodePrefix );
                result.AddRange( sps );
            }

            if ( pps?.Length > 0 )
            {
                result.AddRange( startCodePrefix );
                result.AddRange( pps );
            }

            return result.ToArray();
        }
    }
}