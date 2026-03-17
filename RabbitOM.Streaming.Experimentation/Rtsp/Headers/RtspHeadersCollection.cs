using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class RtspHeadersCollection : IEnumerable , IEnumerable<KeyValuePair<string , string[]>> , IReadOnlyNameValuesCollection
    {
        private readonly NameHeaderValuesCollection _collection = new NameHeaderValuesCollection();


        


        public string[] this[ string key ] 
        { 
            get => _collection[ key ];
        }

        public string this[ string key  ,int index ]
        {
            get => _collection[ key , index ];
        }





        public int Count
        {
            get => _collection.Count;
        }

        public string[] AllKeys
        {
            get => _collection.AllKeys;
        }
        public bool IsReadOnly
        {
            get => false;
        }





        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string , string[]>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
                
        public void Add( string key , string value )
        {
            _collection.Add( key , value );
        }

        public void Add( string key , IEnumerable<string> values )
        {
            _collection.Add( key , values );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool ContainsKey( string key )
        {
            return _collection.ContainsKey( key );
        }

        public bool Remove( string key )
        {
            return _collection.Remove( key );
        }

        public bool RemoveAt( string key , int index )
        {
            return _collection.RemoveAt( key , index );
        }

        public bool TryAdd( string key , string value )
        {
            return _collection.TryAdd( key , value );
        }

        public bool TryAdd( string key , IEnumerable<string> values )
        {
            return _collection.TryAdd( key , values );
        }


        public bool TryGetValue( string key , out string result )
        {
            return _collection.TryGetValue( key , out result );
        }

        public bool TryGetValueAt( string key , int index , out string result )
        {
            return _collection.TryGetValueAt( key , index , out result );
        }

        public bool TryGetValues( string key , out string[] result )
        {
            return _collection.TryGetValues( key , out result );
        }
    }
}
