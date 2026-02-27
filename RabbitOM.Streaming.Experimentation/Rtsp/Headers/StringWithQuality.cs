using System;
using System.Linq;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class StringWithQuality : IEquatable<StringWithQuality>
    {
        public StringWithQuality( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( ! StringRtspHeaderNormalizer.CheckValue( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            Name = name;
        }

        public StringWithQuality( string name , double quality )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( ! StringRtspHeaderNormalizer.CheckValue( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            Name = name;
            Quality = quality;
        }





        public string Name
        {
            get;
        }

        public double? Quality
        {
            get;
        }




        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode() ^ Quality.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            return base.Equals( obj );
        }
        
        public bool Equals( StringWithQuality obj )
        {
            return Equals( this , obj );
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Name ) )
            {
                return string.Empty;
            }

            if ( Quality.HasValue )
            {
                return $"{Name}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}";
            }

            return Name;
        }




        public static bool IsNullOrEmpty( StringWithQuality value )
        {
            return value == null || string.IsNullOrWhiteSpace( value.Name );
        }
        
        public static bool Equals( StringWithQuality a , StringWithQuality b )
        {
            if ( object.ReferenceEquals( a , b ) )
            {
                return true;
            }

            if ( object.ReferenceEquals( a , null ) || object.ReferenceEquals( b , null ) )
            {
                return false;
            }

            return string.Equals( a.Name , b.Name , StringComparison.OrdinalIgnoreCase ) && a.Quality == b.Quality;
        }


        public static bool TryParse( string input , out StringWithQuality result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , ";" , out var tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( string.IsNullOrWhiteSpace( name ) )
                {
                    return false;
                }

                foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                {
                    if ( StringParameter.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( string.Equals( "q" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            if ( double.TryParse( parameter.Value.Trim( ' ' , '\'' , '\"' ) , NumberStyles.Any , CultureInfo.InvariantCulture , out var quality ) )
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

            return result != null;
        }
    }
}
