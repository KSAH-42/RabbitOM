using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public struct StringParameter
    {
        public StringParameter( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            if ( ! RtspHeaderValueNormalizer.CheckValue( name ) )
            {
                throw new ArgumentNullException( nameof( name ) , "the argument called name contains bad things" );
            }

            Name = name ?? string.Empty;

            Value = RtspHeaderValueNormalizer.Normalize( value );
        }





        public string Name { get; }

        public string Value { get; }





        public override string ToString()
        {
            return ToString( "=" );
        }

        public string ToString( string separator )
        {
            if ( string.IsNullOrEmpty( separator ) )
            {
                throw new ArgumentNullException( nameof( separator ) );
            }

            return $"{Name}-{separator}-{Value}";
        }





        
        public static bool TryParse( string input , out StringParameter result )
        {
            return TryParse( input , "=" , out result );
        }

        public static bool TryParse( string input , string separator , out StringParameter result )
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( separator ) )
            {
                return false;
            }

            if ( ! input.Contains( separator ) )
            {
                return false;
            }

            if ( ! RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , separator , out string[] tokens ) )
            {
                return false;
            }

            result = new StringParameter( tokens.ElementAtOrDefault( 0 ) , tokens.ElementAtOrDefault( 1 ) );

            return true;
        }
    }
}
