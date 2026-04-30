using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;
    
    public sealed class StringWithQuality
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        private static readonly StringValueValidator ValueValidator = StringValueValidator.DefaultValidator;




        // TODO: add a special validator to check it does not include the seperator
        public StringWithQuality( string value )
        {
            Value = ValueValidator.TryValidate( value = ValueNormalizer.Normalize( value ) ) ? value : throw new ArgumentException();
        }

        public StringWithQuality( string value , double quality )
        {
            Value = ValueValidator.TryValidate( value = ValueNormalizer.Normalize( value ) ) ? value : throw new ArgumentException();
            Quality = quality;
        }





        public string Value { get; }

        public double? Quality { get; }
        






        public static implicit operator StringWithQuality( string value )
        {
            return new StringWithQuality( value );
        }

        public static bool IsNullOrEmpty( StringWithQuality obj )
        {
            return object.ReferenceEquals( obj , null ) || string.IsNullOrWhiteSpace( obj.Value );
        }

        public static bool TryParse( string input , out StringWithQuality result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( ValueValidator.TryValidate( name ) )
                {
                    foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                    {
                        if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "q" , parameter.Key ) )
                            {
                                if ( double.TryParse( ValueNormalizer.Normalize( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var quality ) )
                                {
                                    result = new StringWithQuality( name , quality );
                                    break;
                                }
                            }
                        }
                    }

                    if ( result == null )
                    {
                        result = new StringWithQuality( name );
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
