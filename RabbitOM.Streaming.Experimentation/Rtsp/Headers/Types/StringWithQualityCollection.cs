using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class StringWithQualityCollection : IEnumerable , IEnumerable<StringWithQuality> , ICollection<StringWithQuality> , IReadOnlyCollection<StringWithQuality>
    {
        private readonly List<StringWithQuality> _collection;

        private readonly Func<StringWithQuality,bool> _validator;



        public StringWithQualityCollection( Func<StringWithQuality,bool> validator = null )
        {
            _collection = new List<StringWithQuality>();
            _validator = validator;
        }



        public int Count
        {
            get => _collection.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }





        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<StringWithQuality> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add( StringWithQuality item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            if ( _validator != null && ! _validator( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }
            
            _collection.Add( item );
        }

        public bool TryAdd( StringWithQuality item )
        {
            if ( item == null )
            {
                return false;
            }

            if ( _validator != null && ! _validator( item ) )
            {
                return false;
            }

            _collection.Add( item );

            return true;
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains( StringWithQuality item )
        {
            return _collection.Contains( item );
        }

        public void CopyTo( StringWithQuality[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public bool Remove( StringWithQuality item )
        {
            return _collection.Remove( item );
        }

        public bool RemoveAt( int index )
        {
            if ( index < 0 || index >= _collection.Count )
            {
                return false;
            }

            _collection.RemoveAt( index );

            return true;
        }

        public bool RemoveBy( Func<StringWithQuality,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }
            
            var item = _collection.FirstOrDefault( predicate );

            if ( item == null )
            {
                return false;
            }

            return _collection.Remove( item );
        }
    }
}
