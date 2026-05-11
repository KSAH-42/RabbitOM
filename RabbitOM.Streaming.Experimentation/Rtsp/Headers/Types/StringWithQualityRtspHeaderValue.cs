using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class StringWithQualityRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
    
        private static readonly IReadOnlyCollection<char> AcceptedChars = ".!$_/|*-&~`%#'\"".ToHashSet();



        public StringWithQualityRtspHeaderValue( string value )
        {
            RtspHeaderValueValidator.EnsureNotNullOrEmpty( value );
            RtspHeaderValueValidator.EnsureWellFormedToken( value );
            RtspHeaderValueValidator.EnsureContainsNoSpace( value );
            RtspHeaderValueValidator.EnsureContains( value , x => char.IsLetterOrDigit( x ) || AcceptedChars.Contains( x ) );

            Value = value;
        }

        public StringWithQualityRtspHeaderValue( string value , double quality )
        {
            RtspHeaderValueValidator.EnsureNotNullOrEmpty( value );
            RtspHeaderValueValidator.EnsureWellFormedToken( value );
            RtspHeaderValueValidator.EnsureContainsNoSpace( value );
            RtspHeaderValueValidator.EnsureContains( value , x => char.IsLetterOrDigit( x ) || AcceptedChars.Contains( x ) );

            Value = value;
            Quality = quality;
        }





        public string Value { get; }

        public double? Quality { get; }
        






        public static implicit operator StringWithQualityRtspHeaderValue( string value )
        {
            return new StringWithQualityRtspHeaderValue( value );
        }







        public static bool IsValidValue( string value )
        {
            return RtspHeaderValueValidator.TryEnsureWellFormedToken( value )
                && RtspHeaderValueValidator.ContainsNoSpace( value )
                && RtspHeaderValueValidator.Contains( value , x => char.IsLetterOrDigit( x ) || AcceptedChars.Contains( x ) )
                ;
        }

        public static bool TryParse( string input , out StringWithQualityRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.FirstOrDefault( token => ! token.Contains( "=" ) ) );

                if ( ! IsValidValue( name ) )
                {
                    return false;
                }

                foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                {
                    if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "q" , parameter.Key ) )
                        {
                            if ( double.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var quality ) )
                            {
                                result = new StringWithQualityRtspHeaderValue( name , quality );
                                break;
                            }
                        }
                    }
                }

                if ( result == null )
                {
                    result = new StringWithQualityRtspHeaderValue( name );
                }
            }

            return result != null;
        }






        public override string ToString()
        {
            return Quality.HasValue ? $"{Value}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : Value;
        }
    }
}
