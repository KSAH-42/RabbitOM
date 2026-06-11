using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using System.Net;

    public partial class RtspHeaderCollection : IEnumerable, IEnumerable<KeyValuePair<string , string>>
    {
        private readonly Dictionary<string,List<string>> _collection = new Dictionary<string, List<string>>( StringComparer.OrdinalIgnoreCase );

        private int _count;





        public string this[ string name ]
        {
            get => GetValue( name );
        }

        public string this[ string name , int index ]
        {
            get => GetValueAt( name , index );
        }
        




        public int Count
        {
            get => _count;
        }

        public IEnumerable<string> AllKeys
        {
            get => _collection.Keys;
        }

        public long? CSeq
        {
            get => ReadValueAsLong( this , RtspHeaderNames.CSeq );
            set => WriteValue( this , RtspHeaderNames.CSeq , value );
        }

        public long? ContentLength
        {
            get => ReadValueAsLong( this , RtspHeaderNames.ContentLength );
            set => WriteValue( this , RtspHeaderNames.ContentLength , value );
        }
        




        public static long? ReadValueAsLong( RtspHeaderCollection collection , string name )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            if ( ! collection.TryGetValue( name ?? string.Empty, out var value ) )
            {
                return null;
            }

            if ( ! long.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) , out var result ) )
            {
                return null;
            }

            return result;
        }

        public static void WriteValue( RtspHeaderCollection collection , string name , long? value )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            collection.SetValue( name , value.HasValue ? value.ToString() : null );
        }
        




        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator( this );
        }

        public IEnumerator<KeyValuePair<string , string>> GetEnumerator()
        {
            return new Enumerator( this );
        }

        public bool Contains( string name )
        {
            return _collection.ContainsKey( name );
        }

        public void Add( string name , string value )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( ! _collection.ContainsKey( name ) )
            {
                _collection[ name ] = new List<string>();
            }

            _collection[ name ].Add( value ?? string.Empty );

            OnAdded( name , value );
        }

        public void AddRange( string name , IEnumerable<string> values )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( ! _collection.ContainsKey( name ) )
            {
                _collection[ name ] = new List<string>();
            }

            var items = _collection[ name ];

            foreach ( var value in values )
            {
                items.Add( value ?? string.Empty );

                OnAdded( name , value );
            }
        }

        public string GetValue( string name )
        {
            return _collection[ name ][0] ?? throw new InvalidOperationException();
        }

        public string GetValueAt( string name , int index )
        {
            return _collection[ name ][index] ?? throw new InvalidOperationException();
        }

        public IEnumerable<string> GetValues( string name )
        {
            return new ReadOnlyCollection<string>( _collection[ name ] );
        }

        public void SetValue( string name , string value )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( value != null )
            {
                if ( ! _collection.ContainsKey( name ) )
                {
                    _collection[ name ] = new List<string>();
                }

                _collection[ name ].Add( value );

                OnAdded( name , value );
            }
            else
            {
                _collection.Remove( name ?? string.Empty );

                OnRemoved( name );
            }
        }

        public bool Remove( string name )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _collection.Remove( name ) )
            {
                return false;
            }

            OnRemoved( name );

            return true;
        }

        public bool RemoveAt( string name , int index )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _collection.TryGetValue( name , out var values ) )
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
                _collection.Remove( name );
            }

            OnRemoved( name );

            return true;
        }

        public void Clear()
        {
            _collection.Clear();

            OnClear();
        }

        public bool TryAdd( string name , string value )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _collection.ContainsKey( name ) )
            {
                _collection[ name ] = new List<string>();
            }

            _collection[ name ].Add( value ?? string.Empty );

            OnAdded( name , value );

            return true;
        }

        public bool TryAddRange( string name , IEnumerable<string> values )
        {
            if ( string.IsNullOrEmpty( name ) || values == null)
            {
                return false;
            }

            if ( ! _collection.ContainsKey( name ) )
            {
                _collection[ name ] = new List<string>();
            }

            var items = _collection[ name ];

            foreach( var value in values )
            {
                items.Add( value ?? string.Empty );

                OnAdded( name , value );
            }

            return true;
        }

        public bool TryAddParse( string input )
        {
            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }

            var index = input.IndexOf( ":" );

            if ( index <= 0 )
            {
                return false;
            }

            var name = input.Substring( 0 , index );

            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _collection.ContainsKey( name ) )
            {
                _collection[ name ] = new List<string>();
            }

            var value = ++ index < input.Length ? input.Substring( index , input.Length - index ) : string.Empty ;

            _collection[ name ].Add( value );

            OnAdded( name , value );

            return true;
        }

        public bool TryGetValue( string name , out string result )
        {
            result = null;

            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _collection.TryGetValue( name , out var values ) )
            {
                return false;
            }

            if ( values.Count <= 0 )
            {
                return false;
            }

            result = values[0];

            Debug.Assert( result != null );

            return true;
        }

        public bool TryGetValueAt( string name , int index , out string result )
        {
            result = null;

            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            if ( ! _collection.TryGetValue( name , out var values ) )
            {
                return false;
            }

            if ( index < 0 || index >= values.Count )
            {
                return false;
            }

            result = values[ index ];

            Debug.Assert( result != null );

            return true;
        }

        public bool TryGetValues( string name , out IEnumerable<string> result )
        {
            result = null;

            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            result = _collection.TryGetValue( name , out var values ) ? new ReadOnlyCollection<string>( values ) : null;

            return result != null;
        }





        protected virtual void OnAdded( string name , string value )
        {
            _count ++;

            var checkCount = StringComparer.OrdinalIgnoreCase.Equals( name , RtspHeaderNames.CSeq )
                          || StringComparer.OrdinalIgnoreCase.Equals( name , RtspHeaderNames.ContentLength )
                          ;

            if ( checkCount && _collection.TryGetValue( name , out var values ) && values.Count > 1 )
            {
                throw new ProtocolViolationException( $"the header {name} is present at multiple times: {values.Count} that can cause security issues" );
            }
        }

        protected virtual void OnRemoved( string name )
        {
            if ( _count > 0 )
            {
                _count --;
            }
        }

        protected virtual void OnClear()
        {
            _count = 0;
        }
    }
}
