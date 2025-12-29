using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264Frame : RtpFrame
    {
        public H264Frame( byte[] data , byte[] startCodePrefix , byte[] pps , byte[] sps ) : base ( data )
        {
            StartCodePrefix = startCodePrefix;
            PPS = pps;
            SPS = sps;
        }


        public byte[] StartCodePrefix { get; }

        public byte[] PPS { get; }
        
        public byte[] SPS { get; }



        public static byte[] CreateParamsBuffer( H264Frame frame )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            return CreateParamsBuffer( frame.StartCodePrefix , frame.PPS , frame.SPS );
        }


        public static byte[] CreateParamsBuffer( byte[] startCodePrefix , byte[] pps , byte[] sps )
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