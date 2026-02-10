using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public struct PortPair
    {
        public static readonly PortPair Zero = new PortPair( 0 , 0 );





        private readonly int _first;

        private readonly int _second;





        public PortPair( int first , int second )
        {
            if ( first < 0 )
            {
                throw new ArgumentException( nameof( first ) );
            }

            if ( second < 0 )
            {
                throw new ArgumentException( nameof( first ) );
            }

            _first = first;
            _second = second;
        }





        public int First
        {
            get => _first;
        }

        public int Second
        {
            get => _second;
        }
        




        public override string ToString()
        {
            return $"{_first}-{_second}";
        }



        public static bool IsValid( in PortPair value )
        {
            return value.First >= 0 && value.Second >= 0;
        }




        public static PortPair Parse( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return TryParse( value , out var result ) ? result : throw new FormatException();
        }

        public static bool TryParse( string value , out PortPair result )
        {
            result = PortPair.Zero;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }
            
            var tokens = value.Split( new char[] { '-' } , StringSplitOptions.RemoveEmptyEntries );

            var succeed = false;
            
            succeed |= int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var first  );
            succeed |= int.TryParse( tokens.ElementAtOrDefault( 1 ) , out var second );

            if ( ! succeed || first < 0 || second < 0 )
            {
                return false;
            }

            result = new PortPair( first , second );
            
            return true;
        }
    }
}
