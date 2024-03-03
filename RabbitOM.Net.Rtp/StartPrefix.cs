using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class StartPrefix
    {
        public static readonly StartPrefix Null          = new StartPrefix( new byte[0] );
        public static readonly StartPrefix StartPrefixS3 = new StartPrefix( new byte[] { 0 , 0 , 1 } );
        public static readonly StartPrefix StartPrefixS4 = new StartPrefix( new byte[] { 0 , 0 , 0 , 1 } );




        private readonly byte[] _values;




        private StartPrefix( byte[] values )
        {
            _values = values;
        }       





        public byte[] Values
        {
            get => _values;
        }




        public static StartPrefix NewStartPrefix( byte[] buffer )
        {
            if ( buffer == null )
                throw new ArgumentNullException( nameof( buffer ) ) ;

            if ( buffer.Length == 0 )
                throw new ArgumentException( nameof( buffer ) );

            return new StartPrefix( buffer );
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
    }
}