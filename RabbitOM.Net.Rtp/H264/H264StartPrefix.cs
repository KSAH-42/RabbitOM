/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264StartPrefix
    {
        public H264StartPrefix( byte[] values )
        {
            if ( values == null )
                throw new ArgumentNullException( nameof( values ) );

            if ( values.Length == 0 )
                throw new ArgumentException( nameof( values ) );

            Values = values;
        }       


        public byte[] Values { get; private set; }


        public static bool StartsWith( byte[] buffer , H264StartPrefix prefix )
        {
            if ( buffer == null )
                throw new ArgumentNullException( nameof( buffer ) );

            if ( prefix == null )
                throw new ArgumentNullException( nameof( prefix ) );

            int count = buffer.Length > prefix.Values.Length ? prefix.Values.Length : buffer.Length;

            while ( -- count >= 0 )
            {
                if ( buffer[ count ] != prefix.Values[ count ] )
                {
                    return false;
                }
            }

            return true;
        }
    }
}