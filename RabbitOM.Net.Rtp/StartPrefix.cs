/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED

*/

using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class StartPrefix
    {
        public static readonly StartPrefix StartPrefixS3 = new StartPrefix( new byte[] { 0 , 0 , 1 } );
        public static readonly StartPrefix StartPrefixS4 = new StartPrefix( new byte[] { 0 , 0 , 0 , 1 } );


        private readonly byte[] _values;




        public StartPrefix( byte[] values )
        {
            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( values.Length == 0 )
            {
                throw new ArgumentException( nameof( values ) );
            }

            _values = values;
        }       





        public byte[] Values
        {
            get => _values;
        }




        // TOBE REMOVED...
        public static bool StartsWith( byte[] buffer , StartPrefix prefix )
        {
            if ( buffer == null || prefix == null )
            {
                return false;
            }

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