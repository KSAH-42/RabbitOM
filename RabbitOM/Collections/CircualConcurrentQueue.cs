using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Collections
{
    using RabbitOM.Threading;

    // TODO: remove this collection
    public sealed partial class CircualConcurrentQueue<TElement> : ICollection, IReadOnlyCollection<TElement>
        where TElement : class
    {
        private readonly int _limit;
        private readonly object _lock;
        private readonly ManualResetEventSlim _eventHandle;
        private readonly Queue<TElement> _collection;
        private readonly Scope _scope;





        public CircualConcurrentQueue()
            : this ( 10000 )
        {
        }

        public CircualConcurrentQueue( int limit )
        {
            if ( limit <= 0 )
            {
                throw new ArgumentException( nameof( limit ) );
            }

            _limit = limit;
            _lock = new object();
            _collection = new Queue<TElement>();
            _eventHandle = new ManualResetEventSlim( false );
            _scope = new Scope( this );
        }






        public object SyncRoot
        {
            get
            {
                return _lock;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

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

        public bool IsEmpty
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count == 0 ;
                }
            }
        }






        public static bool Wait( CircualConcurrentQueue<TElement> queue , WaitHandle cancellationHandle )
        {
            if ( queue == null )
            {
                throw new ArgumentNullException( nameof( queue ) );
            }

            if ( cancellationHandle == null )
            {
                throw new ArgumentNullException( nameof( cancellationHandle ) );
            }

            return queue._eventHandle.TryWait( cancellationHandle );
        }






        IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        public bool Any()
        {
            lock ( _lock )
            {
                return _collection.Count > 0;
            }
        }

        public void CopyTo(Array array, int index)
        {
            lock ( _lock )
            {
                _collection.CopyTo( array as TElement[] , index );
            }
        }

        public void Enqueue( TElement element )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    if ( _limit < _collection.Count )
                    {
                        _collection.Dequeue();
                    }

                    _collection.Enqueue( element );
                }
            }
        }

        public TElement Dequeue()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    return _collection.Dequeue();
                }
            }
        }

        public bool TryDequeue( out TElement result )
        {
            result = default;

            lock ( _lock )
            {
                using ( _scope )
                {
                    if ( _collection.Count <= 0 )
                    {
                        return false;
                    }

                    result = _collection.Dequeue();

                    return true;
                }
            }
        }

        public void Clear()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    _collection.Clear();
                }
            }
        }
    }
}
