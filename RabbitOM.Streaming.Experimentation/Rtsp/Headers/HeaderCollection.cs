using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: review and add test when injected un authorized chars and so on
    public class HeaderCollection : IEnumerable, IHeaderCollection, IReadOnlyHeaderCollection
    {
        private readonly IReadOnlyCollection<string> s_forbiddenHeaders = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "CSeq" , "Content-Length"
        };

        private readonly Dictionary<string,List<object>> _collection = new Dictionary<string, List<object>>( StringComparer.OrdinalIgnoreCase );







        public string this[ string name ]
        {
            get => GetValue( name );
        }

        public string this[ string name , int index ]
        {
            get => GetValueAt( name , index );
        }







        public object SyncRoot
        {
            get => _collection;
        }

        public int Count
        {
            get => _collection.Values.Sum( element => element.Count );
        }

        public bool IsEmpty
        {
            get => _collection.Values.Sum( element => element.Count ) == 0;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        public string[] AllKeys
        {
            get => _collection.Keys.ToArray();
        }

        





        // to avoid using linq, and avoid allocation of key<string,string[]>, or any cast of key<string,IEnumerable<string>>
        // and preserve immutability 

        public struct FlattenEnumerator : IEnumerator , IEnumerator<KeyValuePair<string,string>>
        {
            private readonly IEnumerator<KeyValuePair<string,List<object>>> _enumerator;
            private IEnumerator<object> _valuesEnumerator;
            private KeyValuePair<string,string> _current;

            public FlattenEnumerator( HeaderCollection collection )
            {
                _enumerator = collection._collection.GetEnumerator();
                _valuesEnumerator = null;
                _current = default;
            }

            KeyValuePair<string , string> IEnumerator<KeyValuePair<string , string>>.Current
            {
                get => _current ;
            }

            public object Current
            {
                get => _current;
            }

            public void Dispose()
            {
                _enumerator.Dispose();
                _valuesEnumerator?.Dispose();
            }

            public bool MoveNext()
            {
                while ( true )
                {
                    if ( _valuesEnumerator != null && _valuesEnumerator.MoveNext() )
                    {
                        break;
                    }

                    if ( ! _enumerator.MoveNext() )
                    {
                        return false;
                    }

                    _valuesEnumerator = _enumerator.Current.Value.GetEnumerator();
                }

                _current = new KeyValuePair<string,string>( _enumerator.Current.Key , _valuesEnumerator.Current?.ToString() ?? string.Empty );

                return true;
            }

            public void Reset()
            {
                _enumerator.Reset();
                _valuesEnumerator = null;
                _current = default;
            }
        }







        

        public bool ContainsKey( string name )
        {
            return _collection.ContainsKey( name ?? string.Empty );
        }

        public void Add( string name , string value )
        {
            if ( ! HeaderProtocolValidator.IsValidHeaderName( name ) )
            {
                throw new ArgumentException( name );
            }

            // TODO: need to allow empty ? headervalue class can return empty a string
            // TODO: need to remove this code ?
            if ( ! HeaderProtocolValidator.IsValidHeaderValue( value ) )
            {
                throw new ArgumentException( value );
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( $"This header is not allowed: {name}" );
            }

            GetOrCreateValues( name ).Add( value.Trim() );
        }

        public void AddOrUpdate( string name , string value )
        {
            if ( ! HeaderProtocolValidator.IsValidHeaderName( name ) )
            {
                throw new ArgumentException( name );
            }

            if ( ! HeaderProtocolValidator.IsValidHeaderValue( value ) )
            {
                throw new ArgumentException( value );
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( $"This header is not allowed: {name}" );
            }

            var list = GetOrCreateValues( name );

            if ( list.Count == 0 )
            {
                list.Add( value.Trim() );
            }
            else
            {
                list[ 0 ] = value.Trim() ;
            }
        }
        
        public bool Remove( string name )
        {
            return _collection.Remove( name ?? string.Empty );
        }
        
        public bool RemoveAt( string name , int index )
        {
            var key = name ?? string.Empty;

            if ( ! _collection.TryGetValue( key , out var values ) )
            {
                return false;
            }

            if ( 0 > index || index >= values.Count )
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

        public void CopyTo( Array array , int index )
        {
            var queue = new Queue<KeyValuePair<string,string>>();
            
            using ( IEnumerator<KeyValuePair<string,string>> enumerator = new FlattenEnumerator( this ) )
            {
                while ( enumerator.MoveNext() )
                {
                    queue.Enqueue( enumerator.Current );
                }
            }

            var items = queue.ToArray();

            Array.Copy( items , 0 , array , index , items.Length );
        }

        public string GetValue( string name )
        {
            return TryGetValue( name , out var result ) ? result : string.Empty;
        }
        
        public string GetValueAt( string name , int index )
        {
            return TryGetValueAt( name , index , out var result ) ? result : string.Empty;
        }

        public IEnumerable<string> GetValues( string name )
        {
            return TryGetValues( name , out var result ) ? result : Enumerable.Empty<string>();
        }
                
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FlattenEnumerator( this );
        }
        IEnumerator<KeyValuePair<string , string>> IEnumerable<KeyValuePair<string , string>>.GetEnumerator()
        {
            return new FlattenEnumerator( this );
        }

        // the SetValue do the same thing of AddOrUpdate method except, that it force removal if it an empty value is passed
        public void SetValue( string name , string value )
        {
            if ( ! HeaderProtocolValidator.IsValidHeaderName( name ) )
            {
                return;
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( $"This header is not allowed: {name}" );
            }

            if ( _collection.TryGetValue( name , out var list ) )
            {
                if ( HeaderProtocolValidator.IsValidHeaderValue( value ) )
                {
                    if ( list.Count == 0 )
                    {
                        list.Add( value );
                    }
                    else
                    {
                        list[ 0 ] = value ;
                    }
                }
                else
                {
                    _collection.Remove( name );
                }
            }
            else
            {
                if ( HeaderProtocolValidator.IsValidHeaderValue( value ) )
                {
                    GetOrCreateValues( name ).Add( value );
                }
            }
        }



        
        public bool TryAdd( string name , string value )
        {
            if ( ! HeaderProtocolValidator.IsValidHeaderName( name ) || ! HeaderProtocolValidator.IsValidHeaderValue( value ) )
            {
                return false;
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                return false;
            }

            GetOrCreateValues( name ).Add( value );

            return true;
        }

        public bool TryGetValue( string name , out string result )
        {
            result = null;

            if ( ! _collection.TryGetValue( name ?? string.Empty , out var list ) )
            {
                return false;
            }

            if ( list.Count > 0 )
            {
                result = list[ 0 ]?.ToString();
            }

            return result != null;
        }
        
        public bool TryGetValueAt( string name , int index , out string result )
        {
            result = string.Empty;

            if ( ! _collection.TryGetValue( name ?? string.Empty , out var list ) )
            {
                return false;
            }

            if ( index < 0 || index >= list.Count )
            {
                return false;
            }

            result = list[ index ]?.ToString();

            return result != null;
        }
        
        public bool TryGetValues( string name , out IEnumerable<string> result )
        {
            result = null;

            if ( ! _collection.TryGetValue( name ?? string.Empty , out var list ) )
            {
                return false;
            }

            if ( list.Count > 0 )
            {
                result = list.Select( x => x.ToString() );
            }

            return result != null;
        }




        // TODO: remove this method a prefer override the signature of the method using different type of value parameter
        protected void SetValue( string name , object value )
        {
            SetValueObject( name , value );
        }

        protected void SetValueObject( string name , object value )
        {
            if ( ! HeaderProtocolValidator.IsValidHeaderName( name ))
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( $"This header is not allowed: {name}" );
            }
            // TODO: possible issue because the validator must used here
            var list = GetOrCreateValues( name );

            if ( value != null )
            {
                if ( list.Count == 0 )
                {
                    list.Add( value );
                }
                else
                {
                    list[ 0 ] = value ;
                }
            }
            else
            {
                _collection.Remove( name );
            }
        }

        protected object GetValueObject( string name )
        {
            if ( _collection.TryGetValue( name ?? string.Empty , out var list ) )
            {
                return list.FirstOrDefault();
            }
            
            return null;
        }

        protected TValue GetValueObject<TValue>( string name , Func<TValue> factory ) where TValue : class
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return null;
            }

            if ( factory == null )
            {
                return null;
            }

            if ( _collection.TryGetValue( name , out var list ) )
            {
                return list.FirstOrDefault() as TValue;
            }

            var createdValue = factory();

            if ( createdValue == null )
            {
                return null;
            }

            GetOrCreateValues( name ).Add( createdValue );

            return createdValue;
        }

        




        private List<object> GetOrCreateValues( string name )
        {
            System.Diagnostics.Debug.Assert( ! string.IsNullOrWhiteSpace( name ) );

            if ( ! _collection.TryGetValue( name , out var values ) )
            {
                _collection[ name ] = ( values = new List<object>() );
            }

            return values;
        }
    }
}
