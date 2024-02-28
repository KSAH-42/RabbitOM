/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;

namespace RabbitOM.Net.Rtsp.Tests
{
    public sealed class H264StartPrefix
    {
        private H264StartPrefix( byte[] values , ulong hash )
        {
            if ( values == null )
                throw new ArgumentNullException( nameof( values ) );

            if ( values.Length == 0 )
                throw new ArgumentException( nameof( values ) );

            Values = values;
            HashCode = hash;
        }       




        public byte[] Values { get; private set; }
        public ulong HashCode { get; private set; }





        public static H264StartPrefix NewPrefix( byte[] buffer )
        {
            return new H264StartPrefix( buffer , Hash( buffer , buffer.Length ) );
        }

        public static bool StartWith( byte[] buffer , H264StartPrefix prefix )
        {
            return Hash( buffer , prefix.Values.Length ) == prefix.HashCode;
        }






        private static ulong Hash( byte[] buffer , int count )
        {
            ulong sum = 0;

            count = count > buffer.Length ? buffer.Length : count;

            while ( --count >= 0 )
                sum += buffer[ count ];

            return sum;
        }
    }
}