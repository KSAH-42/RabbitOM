using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class HeaderCollection : IEnumerable, IEnumerable<KeyValuePair<string , IEnumerable<string>>>, IHeaderCollection, IReadOnlyHeaderCollection
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
            get => _collection.Count;
        }

        public bool IsEmpty
        {
            get => _collection.Count == 0;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        public string[] AllKeys
        {
            get => _collection.Keys.ToArray();
        }

        





        

        public bool ContainsKey( string name )
        {
            return _collection.ContainsKey( name ?? string.Empty );
        }

        public void Add( string name , string value )
        {
            if ( name == null )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( $"This header is not allowed: {name}" );
            }

            GetOrCreateValues( name ).Add( value.Trim() );
        }

        public void AddOrUpdate( string name , string value )
        {
            if ( name == null )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
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
            var items = ToKeyValues().ToArray();

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
            return ToKeyValues().GetEnumerator();
        }
        IEnumerator<KeyValuePair<string , IEnumerable<string>>> IEnumerable<KeyValuePair<string , IEnumerable<string>>>.GetEnumerator()
        {
            return ToKeyValues().GetEnumerator();
        }




        
        public bool TryAdd( string name , string value )
        {
            if ( string.IsNullOrWhiteSpace( name ) || string.IsNullOrWhiteSpace( value ) )
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





        protected void SetValue( string name , object value )
        {
            SetValueObject( name , value );
        }

        protected void SetValueObject( string name , object value )
        {
            if ( name == null )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( s_forbiddenHeaders.Contains( name ) )
            {
                throw new ArgumentException( $"This header is not allowed: {name}" );
            }

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
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return null;
            }

            if ( ! _collection.TryGetValue( name ?? string.Empty , out var list ) )
            {
                return null;
            }

            return list.FirstOrDefault();
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

        private IEnumerable<KeyValuePair<string , IEnumerable<string>>> ToKeyValues()
        {
            return _collection.Select( x => new KeyValuePair<string , IEnumerable<string>> ( x.Key , x.Value.Select( y => y.ToString() ) ));
        }
    }
}
