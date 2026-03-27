using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;

    public struct ValueRange : IEquatable<ValueRange>
    {
        public static readonly ValueRange Zero = new ValueRange( 0 , 0 );

        public readonly static StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;






        public ValueRange( int minimum , int maximum )
        {
            if ( minimum < 0 || maximum < 0 || minimum > maximum )
            {
                throw new ArgumentOutOfRangeException();
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







        public static bool Equals( in ValueRange a , in ValueRange b )
        {
            return a.Minimum == b.Minimum && a.Maximum == b.Maximum;
        }

        public static bool TryParse( string input , out ValueRange result )
        {
            result = default;

            // TODO: add a new adapter that replace all quotes, not trim quotes, but before to check if it is possible to remove adapter when using the parser to retrive a key value pair, perhaps a new type called property and some static methods for filter or even make any comparisions between name and value

            if ( RtspHeaderParser.TryParse( StringValueNormalizer.TrimWithSuppressQuoteNormalizer.Normalize( input ) , "-" , out string[] tokens ) )
            {
                if ( ! int.TryParse( ValueNormalizer.Normalize( tokens.ElementAtOrDefault( 0 ) ) , out var minimum ) )
                {
                    return false;
                }

                if ( ! int.TryParse( ValueNormalizer.Normalize( tokens.ElementAtOrDefault( 1 ) ) , out var maximum ) )
                {
                    return false;
                }

                if ( minimum < 0 || maximum < 0 || minimum > maximum )
                {
                    return false;
                }

                result = new ValueRange( minimum , maximum );
                
                return true;
            }
            
            return false;
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
