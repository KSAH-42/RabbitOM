using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp.Clients
{
    internal sealed class RtspRequestHandlerList : ICollection , ICollection<RtspRequestHandler> , IReadOnlyCollection<RtspRequestHandler>
    {
        public const int Maximum = 1000;


        private readonly object _lock = new object();
        private readonly IDictionary<long,RtspRequestHandler> _collection  = new Dictionary<long,RtspRequestHandler>();


        public RtspRequestHandler this[ int sequenceId ]
        {
            get
            {
                lock ( _lock )
                {
                    return _collection[ sequenceId ];
                }
            }
        }


        public object SyncRoot
        {
            get => _lock;
        }

        public bool IsSynchronized
        {
            get => true;
        }

        public bool IsReadOnly
        {
            get => false;
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
                    return _collection.Count <= 0;
                }
            }
        }

        public bool IsFull
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count >= Maximum;
                }
            }
        }



        public void Add( RtspRequestHandler handler )
        {
            if ( handler == null )
            {
                throw new ArgumentNullException( nameof( handler ) );
            }

            lock ( _lock )
            {
                if (_collection.ContainsKey(handler.RequestId))
                {
                    throw new InvalidOperationException( "The element with same identifier is already present" );
                }

                if (_collection.Count >= Maximum)
                {
                    throw new InvalidOperationException( "The collection is full" );
                }

                _collection.Add( handler.RequestId , handler );
            }
        }

        public bool Any()
        {
            lock ( _lock )
            {
                return _collection.Count > 0;
            }
        }

        public void Clear()
        {
            lock ( _lock )
            {
                foreach (var element in _collection)
                {
                    element.Value?.Dispose();
                }

                _collection.Clear();
            }
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo( array as RtspRequestHandler[] , index );
        }

        public void CopyTo(RtspRequestHandler[] array, int arrayIndex)
        {
            lock ( _lock )
            {
                _collection.Values.CopyTo( array , arrayIndex );
            }
        }

        public bool Contains( RtspRequestHandler handler )
        {
            if ( handler == null )
            {
                return false;
            }

            lock ( _lock )
            {
                return _collection.Values.Contains(handler);
            }
        }

        public bool ContainsById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.ContainsKey(sequenceId);
            }
        }

        public RtspRequestHandler FindById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out RtspRequestHandler result ) ? result : null ;
            }
        }

        public RtspRequestHandler ElementAtOrDefault( int index )
        {
            lock ( _lock )
            {
                return _collection.Values.ElementAtOrDefault(index);
            }
        }

        public void ForEach( Action<RtspRequestHandler> action )
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            lock ( _lock )
            {
                foreach (var handler in _collection.Values)
                {
                    if (handler == null)
                    {
                        continue;
                    }

                    action(handler);
                }
            }
        }

        public IEnumerator<RtspRequestHandler> GetEnumerator()
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

        public bool Remove( RtspRequestHandler handler )
        {
            return Remove( handler , true );
        }

        public bool Remove( RtspRequestHandler handler , bool dispose )
        {
            if ( handler == null )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.Values.Contains( handler ) )
                {
                    if ( dispose )
                    {
                        handler.Dispose();
                    }

                    return _collection.Remove( handler.RequestId );
                }

                return false;
            }
        }

        public bool RemoveById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.Remove( sequenceId );
            }
        }

        public bool TryAdd( RtspRequestHandler handler )
        {
            if (handler == null)
            {
                return false;
            }

            lock ( _lock )
            {
                if (_collection.ContainsKey(handler.RequestId))
                {
                    return false;
                }

                if (_collection.Count >= Maximum)
                {
                    return false;
                }

                _collection[ handler.RequestId ] = handler;

                return true;
            }
        }

        public bool TryGetById( long sequenceId , out RtspRequestHandler result )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out result );
            }
        }
    }
}
