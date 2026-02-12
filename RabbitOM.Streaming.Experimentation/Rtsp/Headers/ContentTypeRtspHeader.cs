using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentTypeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Type";

        private string _mediaType = string.Empty;

        private readonly Dictionary<string,string> _parameters = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
        

        public string MediaType
        {
            get => _mediaType;
            set => _mediaType = StringRtspNormalizer.Normalize( value );
        }

        public IReadOnlyDictionary<string,string> Parameters
        {
            get => _parameters;
        }




        public static bool TryParse( string value , out ContentTypeRtspHeader result )
        {
            result = null;

            if ( ! RtspHeaderParser.TryParse( StringRtspNormalizer.Normalize( value ) , ";" , out var tokens ) )
            {
                return false;
            }

            var mediaType = tokens.ElementAtOrDefault( 0 );

            if ( string.IsNullOrWhiteSpace( mediaType ) || mediaType.Contains( "=" ) || ! mediaType.Any( x => char.IsLetterOrDigit( x ) ) )
            {
                return false;
            }

            result = new ContentTypeRtspHeader() { MediaType = mediaType };

            foreach ( var token in tokens.Skip( 1 ) )
            {
                if ( RtspHeaderParser.TryParse( token , "=" , out var parameters ) )
                {
                    result.TryAddParameter( parameters.ElementAtOrDefault( 0 ) , parameters.ElementAtOrDefault( 1 ) );
                }
            }

            return true;
        }





        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _mediaType ) && _mediaType.Any( x => char.IsLetterOrDigit( x ) );
        }

        public bool TryAddParameter( string name , string value )
        {
            var key = StringRtspNormalizer.Normalize( name );

            if ( string.IsNullOrWhiteSpace( key ) || _parameters.ContainsKey( key ) )
            {
                return false;
            }

            _parameters[ key ] = StringRtspNormalizer.Normalize( value );

            return true;
        }

        public void AddParameter( string name , string value )
        {
            if ( name == null )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            if ( ! TryAddParameter( name , value ) )
            {
                throw new ArgumentException( nameof( name ) );
            }
        }

        public void RemoveParameter( string name )
        {
            _parameters.Remove( StringRtspNormalizer.Normalize( name ) );
        }

        public void RemoveParameters()
        {
            _parameters.Clear();
        }

        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _mediaType ) )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat( "{0}; " , _mediaType );

            foreach ( var parameter in _parameters )
            {
                builder.AppendFormat( "{0}={1}; " , parameter.Key , parameter.Value );
            }

            while ( builder.Length > 0 && ( builder[ builder.Length - 1 ] == ';' || builder[ builder.Length - 1 ] == ' ' ) )
            {
                builder.Remove( builder.Length - 1 , 1 );
            }

            return builder.ToString();
        }
    }
}
