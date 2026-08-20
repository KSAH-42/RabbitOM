using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspProxyRequestHandlerList : ICollection , ICollection<RtspProxyRequestHandler> , IReadOnlyCollection<RtspProxyRequestHandler>
    {
        public const int Maximum = 1000;


        private readonly object _lock = new object();
        private readonly IDictionary<long,RtspProxyRequestHandler> _collection  = new Dictionary<long,RtspProxyRequestHandler>();


        public RtspProxyRequestHandler this[ int sequenceId ]
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



        public void Add( RtspProxyRequestHandler handler )
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
            CopyTo( array as RtspProxyRequestHandler[] , index );
        }

        public void CopyTo(RtspProxyRequestHandler[] array, int arrayIndex)
        {
            lock ( _lock )
            {
                _collection.Values.CopyTo( array , arrayIndex );
            }
        }

        public bool Contains( RtspProxyRequestHandler handler )
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

        public RtspProxyRequestHandler FindById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out RtspProxyRequestHandler result ) ? result : null ;
            }
        }

        public RtspProxyRequestHandler ElementAtOrDefault( int index )
        {
            lock ( _lock )
            {
                return _collection.Values.ElementAtOrDefault(index);
            }
        }

        public void ForEach( Action<RtspProxyRequestHandler> action )
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

        public IEnumerator<RtspProxyRequestHandler> GetEnumerator()
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

        public bool Remove( RtspProxyRequestHandler handler )
        {
            return Remove( handler , true );
        }

        public bool Remove( RtspProxyRequestHandler handler , bool dispose )
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

        public bool TryAdd( RtspProxyRequestHandler handler )
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

        public bool TryGetById( long sequenceId , out RtspProxyRequestHandler result )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out result );
            }
        }
    }
}
