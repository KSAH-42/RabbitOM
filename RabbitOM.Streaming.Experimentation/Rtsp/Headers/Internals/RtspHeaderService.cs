using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderService
    {
        private readonly RtspHeaderServiceSettings _settings;

        private readonly Dictionary<string,IList<object>> _headers;





        public RtspHeaderService( RtspHeaderServiceSettings settings )
        {
            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );
            
            _headers = new Dictionary<string, IList<object>>( StringComparer.OrdinalIgnoreCase );
        }





        public RtspHeaderServiceSettings Settings
        {
            get => _settings;
        }

        public IReadOnlyDictionary<string,IList<object>> Headers
        {
            get => _headers;
        }

        



        
        public void AddHeader( string name , string value )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( name );

            if ( _settings.ForbbidenHeaders.Contains( name ) )
            {
                throw new InvalidOperationException( $"the header {name} is fordidden" );
            }

            if ( ! _headers.ContainsKey( name ) )
            {
                _headers[ name ] = new List<object>();
            }

            _headers[ name ].Add( RtspHeaderValueValidator.EnsureWellFormedOrEmpty( value ) );
        }

        public void ParseAndAddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public bool ContainsHeader( string name )
        {
            return _headers.ContainsKey( name ?? string.Empty );
        }

        public object GetHeaderValue( string name )
        {
            return _headers.TryGetValue( name ?? string.Empty , out var values ) ? values.FirstOrDefault( x =>  ! (x is string) ) : null ;
        }

        public object GetHeaderValue( string name , int index )
        {
            return _headers.TryGetValue( name ?? string.Empty , out var values ) ? values.ElementAtOrDefault( index ) : null ;
        }

        public IEnumerable<string> GetHeaderValues( string name )
        {
            if ( _headers.TryGetValue( name ?? string.Empty , out var values ) )
            {
                return values.Select( value => value?.ToString() ?? string.Empty );
            }

            return Enumerable.Empty<string>();
        }

        public bool RemoveHeader( string name )
        {
            return _headers.Remove( name ?? string.Empty );
        }

        public bool RemoveHeader( string name , int index )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _headers.TryGetValue( name , out var values ) )
            {
                return false;
            }

            if ( index < 0 || index >= values.Count )
            {
                return false;
            }

            values.RemoveAt( index );

            if ( values.Count == 0 )
            {
                _headers.Remove( name );
            }

            return true;
        }

        public void RemoveHeaders()
        {
            _headers.Clear();
        }

        public void SetHeaderValue( string name , object value )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( name );

            if ( _headers.TryGetValue( name , out var values ) )
            {
                values.Remove( values.FirstOrDefault( x => ! ( x is string ) ) );
                
                Debug.Assert( values.FirstOrDefault( x => ! ( x is string ) ) == null );

                if ( value != null )
                {
                    Debug.Assert( ! ( value is string ) );

                    values.Add( value );
                }
                else
                {
                    if ( values.Count == 0 )
                    {
                        _headers.Remove( name );
                    }
                }
            }
            else
            {
                if ( value != null )
                {
                    _headers[ name ] = new List<object>() { value };
                }
            }
        }

        public bool TryAddHeader( string name , string value )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( name );

            if ( _settings.ForbbidenHeaders.Contains( name ) )
            {
                return false;
            }

            if ( ! _headers.ContainsKey( name ) )
            {
                _headers[ name ] = new List<object>();
            }

            if ( ! RtspHeaderValueValidator.TryEnsureWellFormedOrEmpty( value ) )
            {
                return false;
            }

            _headers[ name ].Add( value );

            return true;
        }

        public bool TryParseAndAddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public bool TryGetHeaderValue( string name , out string result )
        {
            return TryGetHeaderValue( name , 0 , out result );
        }

        public bool TryGetHeaderValue( string name , int index , out string result )
        {
            result = string.Empty;

            if ( ! _headers.TryGetValue( name ?? string.Empty , out var values ) )
            {
                return false;
            }

            if ( index < 0 || index >= values.Count )
            {
                return false;
            }

            result = values[ 0 ]?.ToString() ?? string.Empty;

            return true;
        }

        public bool TryGetHeaderValues( string name , out IEnumerable<string> result )
        {
            result = null;

            if ( ! _headers.TryGetValue( name ?? string.Empty , out var values ) )
            {
                return false;
            }

            if ( values.Count <= 0 )
            {
                return false;
            }

            result = values.Select( element => element?.ToString() ?? string.Empty ).ToArray();

            return true;
        }
    }
}
