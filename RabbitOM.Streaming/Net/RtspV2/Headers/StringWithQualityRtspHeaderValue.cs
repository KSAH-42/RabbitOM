using System;
using System.Linq;
using System.Globalization;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public sealed class StringWithQualityRtspHeaderValue : IEquatable<StringWithQualityRtspHeaderValue>
    {
        private readonly string _name;
        
        private readonly double? _quality;




        public StringWithQualityRtspHeaderValue( string name ) 
            : this( name , null )
        {
        }
        
        public StringWithQualityRtspHeaderValue( string name , double? quality )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            _name = name ?? string.Empty;
            _quality = quality;
        }





        public string Name
        {
            get => _name;
        }

        public double? Quality
        {
            get => _quality;
        }



        public static bool IsNullOrEmpty( StringWithQualityRtspHeaderValue value )
        {
            return value == null || string.IsNullOrWhiteSpace( value.Name );
        }


        public static bool Equals( StringWithQualityRtspHeaderValue a , StringWithQualityRtspHeaderValue b , StringComparison comparison )
        {
            if ( object.Equals( a , b ) )
            {
                return true;
            }

            if ( ! string.Equals( a?.Name , b?.Name , comparison ) )
            {
                return false;
            }

            return a.Quality == b.Quality;
        }

        public static bool TryParse( string value , out StringWithQualityRtspHeaderValue result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            // pattern: mytext/text; q=1.1 

            var tokens = value.Split( new char[] { ';' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            var name = tokens.ElementAtOrDefault( 0 )?.Trim() ;

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            var parameters = tokens.ElementAtOrDefault( 1 );

            if ( ! string.IsNullOrWhiteSpace( parameters ) )
            {
                var parameterTokens = parameters.Split( new char[] { '=' } , StringSplitOptions.RemoveEmptyEntries );

                if ( parameterTokens.Length >= 1 )
                {                    
                    var parameterName = parameterTokens.ElementAt( 0 ).Trim();

                    if ( parameterName.Equals( "q" , StringComparison.OrdinalIgnoreCase ) )
                    {
                        if ( double.TryParse( parameterTokens.ElementAt( 1 ) , NumberStyles.Any , CultureInfo.InvariantCulture , out var quality ) )
                        {
                            result = new StringWithQualityRtspHeaderValue( name , quality );
                        }
                    }
                }
            }

            if ( result == null )
            {
                result = new StringWithQualityRtspHeaderValue( name , null );
            }

            return true;
        }


        






        public override bool Equals( object obj )
        {
            return base.Equals( obj as StringWithQualityRtspHeaderValue );
        }

        public bool Equals( StringWithQualityRtspHeaderValue obj )
        {
            return Equals( this , obj , StringComparison.OrdinalIgnoreCase );
        }

        public override int GetHashCode()
        {
            return (_name ?? string.Empty).GetHashCode() ^ _quality.GetValueOrDefault().GetHashCode();
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _name ) )
            {
                return string.Empty;
            }

            if ( _quality.HasValue )
            {
                return string.Format( CultureInfo.InvariantCulture, "{0}; q={1:F1}" , _name , _quality );
            }

            return _name;
        }





        
    }
}
