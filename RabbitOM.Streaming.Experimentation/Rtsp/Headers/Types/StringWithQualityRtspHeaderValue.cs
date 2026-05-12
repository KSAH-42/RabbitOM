using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class StringWithQualityRtspHeaderValue
    {
        public StringWithQualityRtspHeaderValue( string value )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( value );
            RtspHeaderValueValidator.EnsureHasLettersAndDigits( value );

            Value = value;
        }

        public StringWithQualityRtspHeaderValue( string value , double quality )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( value );
            RtspHeaderValueValidator.EnsureHasLettersAndDigits( value );

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
            return RtspHeaderValueValidator.TryEnsureWellFormedToken( value ) && RtspHeaderValueValidator.TryEnsureHasLettersAndDigits( value );
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
                        if ( StringComparer.OrdinalIgnoreCase.Equals( "q" , parameter.Key ) )
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
