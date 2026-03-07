using System;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;
    using System.Collections.Generic;

    public sealed class WeightedString : IEquatable<WeightedString>
    {
        public static readonly StringValueValidator ValueValidator = StringValueValidator.TokenValidator;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;


        public WeightedString( string value )
        {
            value = ValueAdapter.Adapt( value );
            Value = ValueValidator.TryValidate( value ) ? value : throw new ArgumentException();
        }

        public WeightedString( string value , double quality )
        {
            value = ValueAdapter.Adapt( value );
            Value = ValueValidator.TryValidate( value ) ? value : throw new ArgumentException();
            Quality = quality;
        }



        public string Value { get; }

        public double? Quality { get; }
        


        public static bool IsNullOrEmpty( WeightedString obj )
        {
            return object.ReferenceEquals( obj , null ) || string.IsNullOrWhiteSpace( obj.Value );
        }

        public static bool Equals( WeightedString a , WeightedString b )
        {
            if ( object.ReferenceEquals( a , b ) )
            {
                return true;
            }

            if ( object.ReferenceEquals( a , null ) || object.ReferenceEquals( b , null ) )
            {
                return false;
            }

            return StringComparer.OrdinalIgnoreCase.Equals( a.Value , b.Value ) && a.Quality == b.Quality;
        }

        public static bool TryParse( string input , out WeightedString result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( string.IsNullOrWhiteSpace( name ) )
                {
                    return false;
                }

                foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( StringComparer.OrdinalIgnoreCase.Equals( "q" , parameter.Key ) )
                        {
                            if ( double.TryParse( ValueAdapter.Adapt( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var quality ) )
                            {
                                result = new WeightedString( name , quality );
                                break;
                            }
                        }
                    }
                }

                if ( result == null )
                {
                    result = new WeightedString( name );
                }
            }

            return result != null;
        }
        




        public override int GetHashCode()
        {
            return Value.ToLower().GetHashCode() ^ Quality.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as WeightedString );
        }
        
        public bool Equals( WeightedString obj )
        {
            return Equals( this , obj );
        }

        public override string ToString()
        {
            if ( ! string.IsNullOrWhiteSpace( Value ) )
            {
                return Quality.HasValue ? $"{Value}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : Value;
            }

            return string.Empty;
        }
    }
}
