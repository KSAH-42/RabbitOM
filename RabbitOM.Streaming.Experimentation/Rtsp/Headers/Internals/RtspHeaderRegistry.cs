using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed partial class RtspHeaderRegistry
    {
        private readonly RtspHeaderRegistrySettings _settings;

        private readonly Dictionary<string,RtspHeaderRegistryBucket> _headers;
        




        public RtspHeaderRegistry( RtspHeaderRegistrySettings settings )
        {
            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );
            
            _headers = new Dictionary<string, RtspHeaderRegistryBucket>( StringComparer.OrdinalIgnoreCase );
        }





        public void CopyTo( Array array , int index )
        {
            _headers.Values.ToArray().CopyTo( array , index );
        }
        
        public int CountHeaders()
        {
            return _headers.Values.Sum( bucket => bucket.Values.Count );
        }
        
        public int CountHeadersKeys()
        {
            return _headers.Keys.Count;
        }
        
        public bool ContainsHeader( string name )
        {
            return _headers.ContainsKey( name ?? string.Empty );
        }
        
        public bool IsHeaderForbidden( string name )
        {
            return _settings.ForbiddenHeaders.Contains( name ?? string.Empty );
        }
        
        public void AddHeader( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( _settings.ForbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( ! _headers.ContainsKey( name ) )
            {
                _headers[ name ] = RtspHeaderRegistryBucket.NewBucket();
            }

            _headers[ name ].Values.Add( value ?? string.Empty );
        }

        public void AddParseHeader( string input )
        {
            throw new NotImplementedException();
        }
        
        public bool RemoveHeader( string name )
        {
            return _headers.Remove( name ?? string.Empty );
        }
        
        public bool RemoveHeader( string name , int index )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            if ( ! _headers.TryGetValue( name , out var bucket ) )
            {
                return false;
            }

            if ( index < 0 || index >= bucket.Values.Count )
            {
                return false;
            }

            bucket.Values.RemoveAt( index );

            if ( bucket.IsEmpty )
            {
                _headers.Remove( name );
            }

            return true;
        }
        
        public void ClearHeaders()
        {
            _headers.Clear();
        }

        public IEnumerable<string> GetHeaderNames()
        {
            return _headers.Keys;
        }
        
        public string GetHeaderValue( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( ! _headers.ContainsKey( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            return _headers[ name ].Values.First().ToString() ?? throw new InvalidOperationException() ;
        }
        
        public string GetHeaderValue( string name , int index )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            return _headers[ name ].Values.ElementAt( index ).ToString() ?? throw new InvalidOperationException();
        }
        
        public IEnumerable<string> GetHeaderValues( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            return _headers[ name ].Values.Select( value => value.ToString() );
        }

        public object GetValue( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            return _headers.TryGetValue( name , out var bucket ) ? bucket.ValueObject : null;
        }
        
        public void SetValue( string name , object value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( _headers.TryGetValue( name , out var bucket ) )
            {
                bucket.ValueObject = value;

                if ( bucket.IsEmpty )
                {
                    _headers.Remove( name );
                }
            }
            else
            {
                if ( value != null )
                {
                    _headers[ name ] = RtspHeaderRegistryBucket.NewBucket( value );
                }
            }
        }
        
        public bool TryAddHeader( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            if ( _settings.ForbiddenHeaders.Contains( name ) )
            {
                return false;
            }

            if ( ! _headers.ContainsKey( name ) )
            {
                _headers[ name ] = RtspHeaderRegistryBucket.NewBucket();
            }

            _headers[ name ].Values.Add( value ?? string.Empty );

            return true;
        }

        public bool TryAddParseHeader( string input )
        {
            throw new NotImplementedException();
        }
        
        public bool TryGetHeaderValues( string name , out IEnumerable<string> result )
        {
            result = null;

            if ( ! _headers.TryGetValue( name ?? string.Empty , out var bucket ) )
            {
                return false;
            }

            result = bucket.Values.Select( value => value.ToString() );

            return true;
        }
        
        public bool TryGetHeaderValue( string name , out string result )
        {
            result = string.Empty;

            if ( ! _headers.TryGetValue( name ?? string.Empty , out var bucket ) )
            {
                return false;
            }

            result = bucket.Values.FirstOrDefault()?.ToString() ?? string.Empty;

            return true;
        }
        
        public bool TryGetHeaderValueAt( string name , int index , out string result )
        {
            result = string.Empty;

            if ( ! _headers.TryGetValue( name ?? string.Empty , out var bucket ) )
            {
                return false;
            }

            result = bucket.Values.ElementAtOrDefault( index )?.ToString() ?? string.Empty;

            return true;
        }
        
        public IEnumerator<KeyValuePair<string,string>> GetEnumerator()
        {
            return new Enumerator( this );
        }
    }
}
