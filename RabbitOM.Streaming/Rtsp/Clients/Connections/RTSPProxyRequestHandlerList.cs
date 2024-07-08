using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the request handler list
    /// </summary>
    internal sealed class RTSPProxyRequestHandlerList 
        : IEnumerable
        , IEnumerable<RTSPProxyRequestHandler>
        , ICollection
        , ICollection<RTSPProxyRequestHandler>
        , IReadOnlyCollection<RTSPProxyRequestHandler>
    {
        /// <summary>
        /// Represent the maximum of elements allowed
        /// </summary>
        public const int                                           Maximum      = 1000;




        private readonly object                                    _lock        = new object();

        private readonly IDictionary<long,RTSPProxyRequestHandler> _collection  = new Dictionary<long,RTSPProxyRequestHandler>();




        /// <summary>
        /// Gets a handler
        /// </summary>
        /// <param name="sequenceId">the sequence identifier</param>
        /// <returns>returns an instance otherwise throw an exception</returns>
        /// <exception cref="KeyNotFoundException"/>
        public RTSPProxyRequestHandler this[ int sequenceId ]
        {
            get
            {
                lock ( _lock )
                {
                    return _collection[ sequenceId ];
                }
            }
        }




        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Check if the collection is thread safe
        /// </summary>
        public bool IsSynchronized
        {
            get => true;
        }

        /// <summary>
        /// Check if the collection is just a read only collection
        /// </summary>
        public bool IsReadOnly
        {
            get => false;
        }
        
        /// <summary>
        /// Gets the number of handlers
        /// </summary>
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

        /// <summary>
        /// Check if the collection contains some handlers
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count == 0;
                }
            }
        }

        /// <summary>
        /// Check if the collection is full
        /// </summary>
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




        /// <summary>
        /// Add a handler
        /// </summary>
        /// <param name="handler">the handler</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Add( RTSPProxyRequestHandler handler )
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

        /// <summary>
        /// Check if the collection contains some handlers
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any()
        {
            lock ( _lock )
            {
                return _collection.Count > 0;
            }
        }

        /// <summary>
        /// Remove all handlers and dispose each one
        /// </summary>
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

        /// <summary>
        /// Copy the content to an array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the start index to begin the copy</param>
        public void CopyTo(Array array, int arrayIndex)
        {
            CopyTo( array as RTSPProxyRequestHandler[] , arrayIndex );
        }

        /// <summary>
        /// Copy the content to an array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the start index to begin the copy</param>
        public void CopyTo(RTSPProxyRequestHandler[] array, int arrayIndex)
        {
            lock ( _lock )
            {
                _collection.Values.CopyTo( array , arrayIndex );
            }
        }

        /// <summary>
        /// Checks if a handler exists
        /// </summary>
        /// <param name="handler">the handler</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RTSPProxyRequestHandler handler )
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

        /// <summary>
        /// Checks if a handler exists
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ContainsById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.ContainsKey(sequenceId);
            }
        }

        /// <summary>
        /// Finds a handler
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPProxyRequestHandler FindById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out RTSPProxyRequestHandler result ) ? result : null ;
            }
        }

        /// <summary>
        /// Get a handler at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPProxyRequestHandler ElementAtOrDefault( int index )
        {
            lock ( _lock )
            {
                return _collection.Values.ElementAtOrDefault(index);
            }
        }

        /// <summary>
        /// Execute a custom action on all handlers of the collection
        /// </summary>
        /// <param name="action">the action</param>
        public void ForEach( Action<RTSPProxyRequestHandler> action )
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

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<RTSPProxyRequestHandler> GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.Values.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.Values.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Remove a handler
        /// </summary>
        /// <param name="handler">the handler to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( RTSPProxyRequestHandler handler )
        {
            return Remove( handler , true );
        }

        /// <summary>
        /// Remove a handler
        /// </summary>
        /// <param name="handler">the handler to be removed</param>
        /// <param name="dispose">set true to call release</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( RTSPProxyRequestHandler handler , bool dispose )
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

        /// <summary>
        /// Remove a handler
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool RemoveById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.Remove( sequenceId );
            }
        }

        /// <summary>
        /// Try to add a handler
        /// </summary>
        /// <param name="handler">the handler</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd( RTSPProxyRequestHandler handler )
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

        /// <summary>
        /// Try to get a handler
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <param name="result">the out result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryGetById( long sequenceId , out RTSPProxyRequestHandler result )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out result );
            }
        }
    }
}
