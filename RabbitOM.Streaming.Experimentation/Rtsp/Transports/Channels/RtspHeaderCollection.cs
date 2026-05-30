using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    // TODO: /!\ not finished, implement an enumerator
    // TODO: it's a low level class, the risk of casting IEnumerable<string> into List is low, expose IReadOnlyCollection an do not make a toArray()
    // TODO: /!\ refactor the count properties, used instead a counter to avoid linq expression that slow down the performance
    // TODO: /!\ take care about the mirror class 
    // we don't use NameValueCollection here is more slower than the dictionary
    // we don't use string.IsNullOrWhiteSpace here, because we are at a lower lever, and we prefer to speed up and let the validation done at a higher level

    public sealed class RtspHeaderCollection : IEnumerable, IEnumerable<KeyValuePair<string , IEnumerable<string>>>
    {
        private readonly Dictionary<string,List<string>> _collection = new Dictionary<string, List<string>>( StringComparer.OrdinalIgnoreCase );




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
            // TODO: snipe this code - too slow
            get => _collection.Values.Sum( x => x.Count );
        }

        public IEnumerable<string> AllKeys
        {
            get => _collection.Keys;
        }

        // TODO: need to remove and let the upper layer to parse this header ?
        public long? CSeq
        {
            get => throw new NotImplementedException(); // add string extensions ToNullableLong()
            set => throw new NotImplementedException();
        }

        // TODO: need to remove and let the upper layer to parse this header ?
        public long? ContentLength
        {
            get => throw new NotImplementedException(); // add string extensions ToNullableLong()
            set => throw new NotImplementedException();
        }






        IEnumerator IEnumerable.GetEnumerator()
        {
            // TODO: snipe this code - too slow
            return _collection.Select( x => new KeyValuePair<string, IEnumerable<string>>( x.Key , x.Value ) ).GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string , IEnumerable<string>>> GetEnumerator()
        {
            // TODO: snipe this code - too slow
            return _collection.Select( x => new KeyValuePair<string, IEnumerable<string>>( x.Key , x.Value ) ).GetEnumerator();
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
        }

        public string GetValue( string name )
        {
            return _collection[ name ][0];
        }

        public string GetValueAt( string name , int index )
        {
            return _collection[ name ][index];
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
            }
            else
            {
                _collection.Remove( name ?? string.Empty );
            }
        }

        public bool Remove( string name )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                return false;
            }

            return _collection.Remove( name );
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

            return true;
        }

        public void Clear()
        {
            _collection.Clear();
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

            var name = input.Substring( 0 , index - 1 );

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
    }
}
