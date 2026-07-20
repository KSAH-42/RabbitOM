using System;
using System.Linq;

namespace RabbitOM.Streaming.RtspV2.Headers.DataTypes
{
    public struct ValueRange : IEquatable<ValueRange>
    {
        public static readonly ValueRange Zero = new ValueRange( 0 , 0 );





        public ValueRange( int minimum , int maximum )
        {
            if ( minimum > maximum )
            {
                throw new ArgumentOutOfRangeException( nameof( minimum ) );
            }

            Minimum = minimum;
            Maximum = maximum;
        }






        public int Minimum { get; }

        public int Maximum { get; }






        public static bool operator == ( ValueRange a , ValueRange b )
        {
            return Equals( a , b );
        }

        public static bool operator != ( ValueRange a , ValueRange b )
        {
            return ! Equals( a , b );
        }







        public static bool Equals( ValueRange a , ValueRange b )
        {
            return a.Minimum == b.Minimum && a.Maximum == b.Maximum;
        }

        public static bool TryParse( string input , out ValueRange result )
        {
            result = default;

            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }
            
            var tokens = input.Split( new [] { '-' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length < 1 )
            {
                return false;
            }

            int minimum = 0;
                
            if ( ! string.IsNullOrEmpty( tokens[0] ) )
            {
                if ( ! int.TryParse( tokens[0] , out minimum ) )
                {
                    return false;
                }
            }

            int maximum = 0;

            if ( ! string.IsNullOrEmpty( tokens.ElementAtOrDefault( 1 ) ) )
            {
                // TODO: do we need to be resilient here and accept a failure ?

                if ( ! int.TryParse( tokens.ElementAtOrDefault( 1 ) , out maximum ) )
                {
                    return false;
                }
            }

            if ( minimum > maximum )
            {
                return false;
            }

            result = new ValueRange( minimum , maximum );

            return true;
        }





        public bool Equals( ValueRange obj )
        {
            return Equals( obj , this );
        }

        public override bool Equals( object obj )
        {
            return obj is ValueRange && Equals( (ValueRange) obj );
        }

        public override int GetHashCode()
        {
            return Minimum.GetHashCode() ^ Maximum.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Minimum}-{Maximum}";
        }
    }
}
