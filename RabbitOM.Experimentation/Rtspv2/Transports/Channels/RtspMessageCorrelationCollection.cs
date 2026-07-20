using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public sealed class RtspMessageCorrelationCollection : ICollection<RtspMessageCorrelation> , IReadOnlyCollection<RtspMessageCorrelation>
    {
        private readonly object _lock = new object();

        private readonly Dictionary<long,RtspMessageCorrelation> _collection = new Dictionary<long,RtspMessageCorrelation>(); // we don't use concurrentdic we need to throw exception if we add something bad





        public int Count
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false; // we returns false even if it's implement IReadOnlyCollection
            }
        }






        public static void RemoveAllWithCancellation( RtspMessageCorrelationCollection source )
        {
            if ( source == null )
            {
                return;
            }

            lock ( source._lock )
            {
                foreach ( var element in source )
                {
                    element.CancelOperation();
                }

                source.Clear();
            }
        }






        public void Add( RtspMessageCorrelation item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            lock ( _lock )
            {
                _collection.Add( item.CSeq , item );
            }
        }

        public void Clear()
        {
            lock ( _lock )
            {
                _collection.Clear();
            }
        }

        public bool Contains( uint id )
        {
            lock ( _lock )
            {
                return _collection.ContainsKey( id );
            }
        }

        public bool Contains( RtspMessageCorrelation item )
        {
            if ( item == null )
            {
                return false;
            }

            lock ( _lock )
            {
                return _collection.ContainsValue( item );
            }
        }

        public void CopyTo( RtspMessageCorrelation[] array , int arrayIndex )
        {
            lock ( _lock )
            {
                _collection.Values.CopyTo( array , arrayIndex );
            }
        }

        public bool Remove( RtspMessageCorrelation item )
        {
            if ( item == null )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.ContainsValue( item ) )
                {
                    return _collection.Remove( item.CSeq );
                }

                return false;
            }
        }

        public bool RemoveById( long id )
        {
            lock ( _lock )
            {
                return _collection.Remove( id );
            }
        }

        public IEnumerator<RtspMessageCorrelation> GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.Values.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.Values.ToList().GetEnumerator();
            }
        }

        public bool TryAdd( RtspMessageCorrelation item )
        {
            if ( item == null )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.ContainsKey( item.CSeq ) )
                {
                    return false;
                }

                _collection.Add( item.CSeq , item );

                return true;
            }
        }

        public bool TryGet( long id , out RtspMessageCorrelation result )
        {
            lock ( _lock )
            {
                result = _collection.TryGetValue( id , out result ) ? result : null;

                return result != null;
            }
        }
    }
}
