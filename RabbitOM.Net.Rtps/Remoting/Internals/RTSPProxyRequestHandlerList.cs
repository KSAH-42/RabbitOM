using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent the request handler list
    /// </summary>
    internal sealed class RTSPProxyRequestHandlerList : IEnumerable<RTSPProxyRequestHandler>
    {
        /// <summary>
        /// Represent the maximum of elements
        /// </summary>
        public  const    int                                       Maximum      = 1000;





        /// <summary>
        /// The lock
        /// </summary>
        private readonly object                                    _lock        = new object();

        /// <summary>
        /// The collection
        /// </summary>
        private readonly IDictionary<long,RTSPProxyRequestHandler> _collection  = new Dictionary<long,RTSPProxyRequestHandler>();





        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPProxyRequestHandlerList()
        {
        }





        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPProxyRequestHandler this[int index]
        {
            get => FindAt( index );
        }





        /// <summary>
        /// Gets the number of elements
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
        /// Check if the collection contains some elements
        /// </summary>
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
        /// Execute a custom action on all elements of the collection
        /// </summary>
        /// <param name="action">the action</param>
        public void ForEach( Action<RTSPProxyRequestHandler> action )
		{
            if ( action == null )
			{
                throw new ArgumentNullException(nameof(action));
			}

            lock ( _lock )
			{
                foreach ( var handler in _collection.Values )
				{
                    if ( handler == null )
					{
                        continue;
					}

                    action(handler);
				}
			}
		}

        /// <summary>
        /// Check if the collection contains some elements
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
        /// Check if the collection contains some elements
        /// </summary>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="ArgumentNullException"/>
        public bool Any( Func<RTSPProxyRequestHandler , bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            lock ( _lock )
            {
                foreach ( var element in _collection.Values )
                {
                    if ( element != null && predicate( element ) )
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ContainsKey( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.ContainsKey( sequenceId );
            }
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RTSPProxyRequestHandler element )
        {
            if ( element == null )
            {
                return false;
            }

            lock ( _lock )
            {
                return _collection.Values.Contains( element );
            }
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Add( RTSPProxyRequestHandler element )
        {
            if ( element == null )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.ContainsKey( element.RequestId ) )
                {
                    return false;
                }

                if ( _collection.Count >= Maximum )
                {
                    return false;
                }

                _collection[element.RequestId] = element;

                return true;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPProxyRequestHandler FirstOrDefault()
        {
            lock ( _lock )
            {
                return _collection.Values.Count > 0 ? _collection.Values.ElementAt( 0 ) : null;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPProxyRequestHandler FindById( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( sequenceId , out RTSPProxyRequestHandler result ) ? result : null;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPProxyRequestHandler FindAt( int index )
        {
            lock ( _lock )
            {
                if ( index < 0 || index >= _collection.Values.Count )
                {
                    return null;
                }

                return _collection.Values.ElementAt( index );
            }
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <returns>returns a collection</returns>
        public IList<RTSPProxyRequestHandler> GetAll()
        {
            lock ( _lock )
            {
                return _collection.Values.ToList();
            }
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns a collection</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<RTSPProxyRequestHandler> GetAll( Func<RTSPProxyRequestHandler , bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            lock ( _lock )
            {
                return _collection.Values.Where( predicate ).ToList();
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="sequenceId">the correlation identifier</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( long sequenceId )
        {
            lock ( _lock )
            {
                return _collection.Remove( sequenceId );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="element">the element to be removed</param>
        /// <param name="dispose">set true to call release</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( RTSPProxyRequestHandler element , bool dispose = true)
        {
            if ( element == null )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.Values.Contains( element ) )
                {
                    if ( dispose )
					{
                        element.Dispose();
					}

                    return _collection.Remove( element.RequestId );
                }

                return false;
            }
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void Clear()
        {
            lock ( _lock )
            {
                foreach ( var element in _collection )
				{
                    element.Value?.Dispose();
				}

                _collection.Clear();
            }
        }
    }
}
