using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;
    
    public sealed class StringWithQualityHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;





        // TODO: add a special validator to check it does not include the seperator
        public StringWithQualityHeaderValue( string value )
        {
            Value = HeaderProtocolValidator.IsValidToken( value = ValueNormalizer.Normalize( value ) ) ? value : throw new ArgumentException();
        }

        public StringWithQualityHeaderValue( string value , double quality )
        {
            Value = HeaderProtocolValidator.IsValidToken( value = ValueNormalizer.Normalize( value ) ) ? value : throw new ArgumentException();
            Quality = quality;
        }





        public string Value { get; }

        public double? Quality { get; }
        






        public static implicit operator StringWithQualityHeaderValue( string value )
        {
            return new StringWithQualityHeaderValue( value );
        }

        public static bool TryParse( string input , out StringWithQualityHeaderValue result )
        {
            result = null;

            if ( HeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( HeaderProtocolValidator.IsValidToken( name ) )
                {
                    foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                    {
                        if ( HeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "q" , parameter.Key ) )
                            {
                                if ( double.TryParse( ValueNormalizer.Normalize( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var quality ) )
                                {
                                    result = new StringWithQualityHeaderValue( name , quality );
                                    break;
                                }
                            }
                        }
                    }

                    if ( result == null )
                    {
                        result = new StringWithQualityHeaderValue( name );
                    }
                }
            }

            return result != null;
        }





        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Value ) )
            {
                return string.Empty;
            }

            return Quality.HasValue ? $"{Value}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : Value;
        }
    }
}
