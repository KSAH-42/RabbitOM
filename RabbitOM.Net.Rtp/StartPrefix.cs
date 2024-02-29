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

        public static int IndexOfPrefix( byte[] buffer )
        {
            return IndexOfPrefix( buffer , 3 );
        }

        public static int IndexOfPrefix( byte[] buffer , int count /*the prefix size*/ ) 
        {
            if ( null == buffer )
                return -1;

            int numberOfZeros = 0;

            for ( int i = 0 ; i < buffer.Length && i <= count ; ++ i )
            {
                if ( buffer[ i ] > 1 )
                    break;

                if ( buffer[ i ] == 0 )
                    numberOfZeros ++;

                if ( buffer[ i ] == 1 )
                    return numberOfZeros >= 2 ? i : -1;
            }

            return -1;
        }
    }
}