using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: adding unit tests
    public class RtspHeaderCollection : IEnumerable , IEnumerable<KeyValuePair<string , RtspHeaderValue[]>> , IHeaderCollection , IReadOnlyHeaderCollection
    {
        private readonly RtspHeaderNameValueCollection _collection = new RtspHeaderNameValueCollection();
        



        public RtspHeaderValue[] this[string key] 
        { 
            get => _collection.GetValues( key );
        }

        public RtspHeaderValue this[string key,int index]
        {
            get => _collection.GetValueAt<RtspHeaderValue>( key , index );
        }
        



        public object SyncRoot
        {
            get => _collection;
        }

        public int Count
        {
            get => _collection.Count;
        }

        public string[] AllKeys
        {
            get => _collection.AllKeys;
        }

        public bool IsEmpty
        {
            get => _collection.IsEmpty;
        }
        
        public bool IsReadOnly
        {
            get => false;
        }

        public bool IsSynchronized
        {
            get => false;
        }
        



        public static List<KeyValuePair<string,RtspHeaderValue>> EnumerateValues( RtspHeaderCollection collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }
            
            var headers = new List<KeyValuePair<string,RtspHeaderValue>>();

            foreach ( var header in collection )
            {
                foreach ( var value in header.Value )
                {
                    headers.Add( new KeyValuePair<string, RtspHeaderValue>( header.Key , value ) );
                }
            }

            return headers;
        }





        public void Add( string key , string value )
        {
            Add( key , new StringRtspHeaderValue( value ?? throw new ArgumentNullException( nameof( value ) ) ) );
        }

        public void Add( string key , RtspHeaderValue value )
        {
            _collection.Add( key , value );
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool ContainsKey( string key )
        {
            return _collection.ContainsKey( key );
        }

        public void CopyTo( Array array , int index )
        {
            _collection.CopyTo( array , index );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string , RtspHeaderValue[]>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void SetValue<TValue>( string name , TValue value ) where TValue : RtspHeaderValue
        {
            _collection.SetValue<TValue>( name , value );
        }

        public TValue GetValue<TValue>( string name ) where TValue : RtspHeaderValue
        {
            return _collection.GetValue<TValue>( name );
        }

        public TValue GetValue<TValue>( string name , Func<TValue> factory ) where TValue : RtspHeaderValue
        {
            return _collection.GetValue<TValue>( name , factory );
        }

        public TValue GetValueAt<TValue>( string name , int index ) where TValue : RtspHeaderValue
        {
            return _collection.GetValueAt<TValue>( name , index );
        }

        public RtspHeaderValue[] GetValues( string name )
        {
            return _collection.GetValues( name );
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
            return TryAdd( key , new StringRtspHeaderValue( value ) );
        }

        public bool TryAdd( string key , RtspHeaderValue value )
        {
            return _collection.TryAdd( key , value );
        }

        public bool TryGetValue( string key , out RtspHeaderValue result )
        {
            return _collection.TryGetValue( key , out result );
        }

        public bool TryGetValueAt( string key , int index , out RtspHeaderValue result )
        {
            return _collection.TryGetValueAt( key , index ,out result );
        }

        public bool TryGetValues( string key , out RtspHeaderValue[] result )
        {
            return _collection.TryGetValues( key , out result );
        }
    }
}
