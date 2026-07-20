using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.H265
{
    public sealed class H265MediaElement : RtpMediaElement
    {
        public H265MediaElement( byte[] buffer , byte[] startCodePrefix , byte[] vps , byte[] sps , byte[] pps ) : base ( buffer )
        {
            if ( startCodePrefix == null )
            {
                throw new ArgumentNullException(  nameof( startCodePrefix ) );
            }

            if ( vps == null )
            {
                throw new ArgumentNullException(  nameof( vps ) );
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

            VPS = vps.Length > 0 ? vps : throw new ArgumentException( nameof( vps ) );

            SPS = sps.Length > 0 ? sps : throw new ArgumentException( nameof( sps ) );

            PPS = pps.Length > 0 ? pps : throw new ArgumentException( nameof( pps ) );
        }





        public byte[] StartCodePrefix { get; }

        public byte[] VPS { get; }

        public byte[] SPS { get; }

        public byte[] PPS { get; }






        public static byte[] CreateParamsBuffer( H265MediaElement frame )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            return CreateParamsBuffer( frame.StartCodePrefix , frame.VPS , frame.SPS , frame.PPS );
        }

        public static byte[] CreateParamsBuffer( byte[] startCodePrefix , byte[] vps , byte[] sps , byte[] pps )
        {
            if ( startCodePrefix == null )
            {
                throw new ArgumentNullException( nameof( startCodePrefix ) );
            }

            if ( startCodePrefix.Length == 0 )
            {
                throw new ArgumentException( nameof( startCodePrefix ) );
            }

            var result = new List<byte>();

            if ( vps?.Length > 0 )
            {
                result.AddRange( startCodePrefix );
                result.AddRange( vps );
            }

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