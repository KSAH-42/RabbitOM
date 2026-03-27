using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;
    
    public sealed class StringWithQualityHeaderValue : IEquatable<StringWithQualityHeaderValue>
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;


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

        public static bool IsNullOrEmpty( StringWithQualityHeaderValue obj )
        {
            return object.ReferenceEquals( obj , null ) || string.IsNullOrWhiteSpace( obj.Value );
        }

        public static bool Equals( StringWithQualityHeaderValue a , StringWithQualityHeaderValue b )
        {
            if ( object.ReferenceEquals( a , b ) )
            {
                return true;
            }

            if ( object.ReferenceEquals( a , null ) || object.ReferenceEquals( b , null ) )
            {
                return false;
            }

            return ValueComparer.Equals( a.Value , b.Value ) && a.Quality == b.Quality;
        }




        public override bool Equals( object obj )
        {
            return Equals( obj as StringWithQualityHeaderValue );
        }
        
        public bool Equals( StringWithQualityHeaderValue obj )
        {
            return Equals( this , obj );
        }

        public override int GetHashCode()
        {
            return Value.ToLower().GetHashCode() ^ Quality.GetHashCode();
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Value ) )
            {
                return string.Empty;
            }

            return Quality.HasValue ? $"{Value}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : Value;
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
    }
}
