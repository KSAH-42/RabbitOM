using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    
    public sealed class StringWithQuality : IEquatable<StringWithQuality>
    {
        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;




        private readonly string _value;

        private readonly double? _quality;




        public StringWithQuality( string value )
        {
            _value = RtspHeaderValueValidator.TryValidateToken( (value = ValueAdapter.Adapt( value )) ) ? value : throw new ArgumentException();
        }

        public StringWithQuality( string value , double quality )
        {
            _value = RtspHeaderValueValidator.TryValidateToken( (value = ValueAdapter.Adapt( value )) ) ? value : throw new ArgumentException();
            _quality = quality;
        }






        public string Value { get => _value; }

        public double? Quality { get => _quality; }
        






        public static bool IsNullOrEmpty( StringWithQuality obj )
        {
            return object.ReferenceEquals( obj , null ) || string.IsNullOrWhiteSpace( obj.Value );
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

            return ValueComparer.Equals( a.Value , b.Value ) && a.Quality == b.Quality;
        }









        public override bool Equals( object obj )
        {
            return Equals( obj as StringWithQuality );
        }
        
        public bool Equals( StringWithQuality obj )
        {
            return Equals( this , obj );
        }

        public override int GetHashCode()
        {
            return _value.ToLower().GetHashCode() ^ _quality.GetHashCode();
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _value ) )
            {
                return string.Empty;
            }

            return _quality.HasValue ? $"{_value}; q={_quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : _value;
        }





        

        public static bool TryParse( string input , out StringWithQuality result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = tokens.FirstOrDefault( token => ! token.Contains( "=" ) );

                if ( RtspHeaderValueValidator.TryValidateToken( name ) )
                {
                    foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                    {
                        if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( ValueComparer.Equals( "q" , parameter.Key ) )
                            {
                                if ( double.TryParse( ValueAdapter.Adapt( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var quality ) )
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
    }
}
