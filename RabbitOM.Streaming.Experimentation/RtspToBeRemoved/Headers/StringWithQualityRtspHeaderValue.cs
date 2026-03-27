using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;
    
    public sealed class StringWithQualityRtspHeaderValue : RtspHeaderValue , IEquatable<StringWithQualityRtspHeaderValue>
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;


        public StringWithQualityRtspHeaderValue( string value )
        {
            Value = RtspHeaderProtocolValidator.IsValidToken( value = ValueNormalizer.Normalize( value ) ) ? value : throw new ArgumentException();
        }

        public StringWithQualityRtspHeaderValue( string value , double quality )
        {
            Value = RtspHeaderProtocolValidator.IsValidToken( value = ValueNormalizer.Normalize( value ) ) ? value : throw new ArgumentException();
            Quality = quality;
        }




        public string Value { get; }

        public double? Quality { get; }
        



        public static implicit operator StringWithQualityRtspHeaderValue( string value )
        {
            return new StringWithQualityRtspHeaderValue( value );
        }

        public static bool IsNullOrEmpty( StringWithQualityRtspHeaderValue obj )
        {
            return object.ReferenceEquals( obj , null ) || string.IsNullOrWhiteSpace( obj.Value );
        }

        public static bool Equals( StringWithQualityRtspHeaderValue a , StringWithQualityRtspHeaderValue b )
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
            return Equals( obj as StringWithQualityRtspHeaderValue );
        }
        
        public bool Equals( StringWithQualityRtspHeaderValue obj )
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





        

        public static bool TryParse( string input , out StringWithQualityRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( RtspHeaderProtocolValidator.IsValidToken( name ) )
                {
                    foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                    {
                        if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "q" , parameter.Key ) )
                            {
                                if ( double.TryParse( ValueNormalizer.Normalize( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var quality ) )
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
            }

            return result != null;
        }
    }
}
