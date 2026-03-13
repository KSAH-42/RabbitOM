using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class NameValueRtspCollection : IEnumerable, IEnumerable<KeyValuePair<string , IEnumerable<string>>>, ICollection, ICollection<KeyValuePair<string , IEnumerable<string>>> , IReadOnlyCollection<KeyValuePair<string,IEnumerable<string>>>
    {
        private readonly Dictionary<string,IEnumerable<string>> _collection = new Dictionary<string, IEnumerable<string>>();





        public IEnumerable<string> this[ string key ]
        {
            get => throw new NotImplementedException();
        }

        public IEnumerable<string> this[ string key , int index ]
        {
            get => throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool TryAdd( string key , string value )
        {
            throw new NotImplementedException();
        }

        public void CopyTo( Array array , int index )
        {
            CopyTo( array as KeyValuePair<string , IEnumerable<string>>[] , index );
        }

        public void CopyTo( KeyValuePair<string , IEnumerable<string>>[] array , int arrayIndex )
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue( string key , out string result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueAt( string key , int index , out string result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetValues( string key , out IEnumerable<string> result )
        {
            throw new NotImplementedException();
        }

        public bool Remove( string key )
        {
            return _collection.Remove( key ?? string.Empty );
        }

        public bool RemoveAt( string key , int index )
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _collection.Clear();
        }
    }
}
