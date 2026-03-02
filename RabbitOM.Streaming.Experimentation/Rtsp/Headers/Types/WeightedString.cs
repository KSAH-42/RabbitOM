using System;
using System.Linq;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class WeightedString : IEquatable<WeightedString>
    {
        public WeightedString( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( ! RtspHeaderParser.Formatter.CheckValue( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            Value = value;
        }

        public WeightedString( string value , double quality )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( ! RtspHeaderParser.Formatter.CheckValue( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            Value = value;
            Quality = quality;
        }





        public string Value
        {
            get;
        }

        public double? Quality
        {
            get;
        }





        public static bool IsNullOrEmpty( WeightedString value )
        {
            return value == null || string.IsNullOrWhiteSpace( value.Value );
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







        public override int GetHashCode()
        {
            return Value.ToLower().GetHashCode() ^ Quality.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            return base.Equals( obj );
        }
        
        public bool Equals( WeightedString obj )
        {
            return Equals( this , obj );
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Value ) )
            {
                return string.Empty;
            }

            if ( Quality.HasValue )
            {
                return $"{Value}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}";
            }

            return Value;
        }




        









        public static bool TryParse( string input , out WeightedString result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , ";" , out var tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( string.IsNullOrWhiteSpace( name ) )
                {
                    return false;
                }

                foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                {
                    if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( StringComparer.OrdinalIgnoreCase.Equals( "q" , parameter.Name ) )
                        {
                            if ( RtspHeaderParser.TryParse( parameter.Value , out double quality ) )
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
    }
}
