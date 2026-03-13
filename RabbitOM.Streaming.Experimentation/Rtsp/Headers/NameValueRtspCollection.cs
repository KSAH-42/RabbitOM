using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: add unit tests

    public sealed class NameValueRtspCollection : IEnumerable, IEnumerable<KeyValuePair<string , IEnumerable<string>>>, ICollection, IReadOnlyCollection<KeyValuePair<string,IEnumerable<string>>>
    {
        private readonly Dictionary<string,IEnumerable<string>> _collection = new Dictionary<string, IEnumerable<string>>();






        public IEnumerable<string> this[ string key ]
        {
            get => _collection[ key ];
        }

        public string this[ string key , int index ]
        {
            get => _collection[ key ].ElementAt( index );
        }






        public object SyncRoot
        {
            get => _collection;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public int Count
        {
            get => _collection.Count;
        }

        public IReadOnlyCollection<string> Keys
        {
            get => _collection.Keys;
        }



        


        IEnumerator<KeyValuePair<string , IEnumerable<string>>> IEnumerable<KeyValuePair<string , IEnumerable<string>>>.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
        
        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }        

        public bool Contains( string key )
        {
            return _collection.ContainsKey( key ?? string.Empty );
        }

        public void Add( string key , string value )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new ArgumentException( nameof( key ) );
            }

            if ( ! _collection.TryGetValue( key , out var elements ) || elements == null )
            {
                elements = new HashSet<string>();
            }

            var items = elements as HashSet<string>;

            if ( items == null )
            {
                throw new InvalidOperationException();
            }

            items.Add( value ?? string.Empty );
        }

        public bool TryAdd( string key , string value )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                return false;
            }

            if ( ! _collection.TryGetValue( key , out var elements ) || elements == null )
            {
                elements = new HashSet<string>();
            }

            var items = elements as HashSet<string>;

            System.Diagnostics.Debug.Assert( items != null );

            if ( items != null )
            {
                return items.Add( value ?? string.Empty );
            }

            return false;
        }

        public void CopyTo( Array array , int index )
        {
            CopyTo( array as KeyValuePair<string , IEnumerable<string>>[] , index );
        }

        public void CopyTo( KeyValuePair<string , IEnumerable<string>>[] array , int arrayIndex )
        {
            _collection.ToList().CopyTo( array , arrayIndex );
        }

        public bool TryGetValue( string key , out string result )
        {
            result = null;

            if ( ! _collection.TryGetValue( key ?? string.Empty , out var elements ) )
            {
                return false;
            }

            var items = elements as HashSet<string>;

            System.Diagnostics.Debug.Assert( items != null );

            if ( items == null || items.Count <= 0 )
            {
                return false;
            }

            result = items.First() ?? string.Empty;

            return true;
        }

        public bool TryGetValueAt( string key , int index , out string result )
        {
            result = null;

            if ( ! _collection.TryGetValue( key ?? string.Empty , out var elements ) )
            {
                return false;
            }

            var items = elements as HashSet<string>;

            System.Diagnostics.Debug.Assert( items != null );

            if ( items == null || index < 0 || index >= items.Count )
            {
                return false;
            }

            result = items.ElementAt( index );

            return true;
        }

        public bool TryGetValues( string key , out IEnumerable<string> result )
        {
            return _collection.TryGetValue( key ?? string.Empty , out result ) ? result != null : false;
        }

        public bool Remove( string key )
        {
            return _collection.Remove( key ?? string.Empty );
        }

        public bool RemoveAt( string key , int index )
        {
            if ( ! _collection.TryGetValue( key ?? string.Empty , out var elements ) )
            {
                return false;
            }

            var items = elements as HashSet<string>;

            System.Diagnostics.Debug.Assert( items != null );

            if ( items == null )
            {
                return false;
            }

            return items.Remove( items.ElementAtOrDefault( index ) );
        }

        public void Clear()
        {
            _collection.Clear();
        }
    }
}
