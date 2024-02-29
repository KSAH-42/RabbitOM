/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED

*/

using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class StartPrefix
    {
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

        public static int LastIndexOf( byte[] buffer )
        {
            return LastIndexOf( buffer , 3 , 4 );
        }

        public static int LastIndexOf( byte[] buffer , int minimum , int maximum /*the prefix size*/ ) 
        {
            if ( null == buffer )
                throw new ArgumentNullException( nameof( buffer ) );

            if ( minimum > maximum )
                throw new ArgumentException( nameof( minimum ) );

            int size = 0;

            for ( int i = 0 ; i < buffer.Length && i <= maximum ; ++ i )
            {
                if ( buffer[ i ] > 1 )
                    break;

                size ++;

                if ( buffer[ i ] == 0 )
                    continue;

                if ( buffer[ i ] == 1 )
                    return ( minimum <= size && size <= maximum ) ? i : -1;
            }

            return -1;
        }
    }
}