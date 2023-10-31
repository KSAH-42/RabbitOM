using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the message method list
    /// </summary>
    public sealed class RTSPMethodTypeList : IEnumerable , IEnumerable<RTSPMethodType> , ICollection , ICollection<RTSPMethodType>
    {
        private readonly object                _lock      = new object();

        private readonly ISet<RTSPMethodType> _collection = new HashSet<RTSPMethodType>();








        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPMethodTypeList()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPMethodTypeList( IEnumerable<RTSPMethodType> collection )
        {
            AddRange( collection );
        }









        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPMethodType this[int index]
        {
            get => GetAt( index );
        }









        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Returns true
        /// </summary>
        public bool IsSynchronized
        {
            get => true;
        }

        /// <summary>
        /// Returns false
        /// </summary>
        public bool IsReadOnly
        {
            get => false;
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
		/// Gets the enumerator
		/// </summary>
		/// <returns>returns an instance</returns>
		IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<RTSPMethodType> GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
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
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RTSPMethodType element )
        {
            lock ( _lock )
            {
                return _collection.Contains( element );
            }
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <exception cref="ArgumentException"/>
        public void Add(RTSPMethodType element)
        {
            if (element == RTSPMethodType.UnDefined)
            {
                throw new ArgumentException( nameof(element) );
            }

            lock (_lock)
            {
                if ( ! _collection.Add(element) )
                {
                    throw new ArgumentException( "Duplicated value" );
                }
            }
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddRange(IEnumerable<RTSPMethodType> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException( nameof(collection) );
            }

            lock (_lock)
            {
                foreach (var element in collection)
                {
                    if (element == RTSPMethodType.UnDefined)
                    {
                        throw new ArgumentException("Bad element");
                    }

                    if ( ! _collection.Add(element))
                    {
                        throw new ArgumentException("Duplicated value");
                    }
                }
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPMethodType? FindAt( int index )
        {
            lock ( _lock )
            {
                if ( index < 0 || index >= _collection.Count )
                {
                    return null;
                }

                return _collection.ElementAt( index );
            }
        }

        /// <summary>
        /// Gets an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPMethodType GetAt( int index )
        {
            return FindAt( index ) ?? RTSPMethodType.UnDefined;
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <returns>returns a collection</returns>
        public IList<RTSPMethodType> GetAll()
        {
            lock ( _lock )
            {
                return _collection.ToList();
            }
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns a collection</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<RTSPMethodType> GetAll( Func<RTSPMethodType , bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            lock ( _lock )
            {
                return _collection.Where( predicate ).ToList();
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="element">the element to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( RTSPMethodType element )
        {
            lock ( _lock )
            {
                return _collection.Remove( element );
            }
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void Clear()
        {
            lock ( _lock )
            {
                _collection.Clear();
            }
        }

        /// <summary>
        /// Copy to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="index">the index</param>
        public void CopyTo(Array array, int index)
        {
            CopyTo(array as RTSPMethodType[], index);
        }

        /// <summary>
        /// Copy to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the index</param>
        public void CopyTo(RTSPMethodType[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _collection.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd(RTSPMethodType element)
        {
            if (element == RTSPMethodType.UnDefined)
            {
                return false;
            }

            lock (_lock)
            {
                return _collection.Add(element);
            }
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange(IEnumerable<RTSPMethodType> collection)
        {
            return TryAddRange(collection, out int result);
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange(IEnumerable<RTSPMethodType> collection, out int result)
        {
            result = default;

            if (collection == null)
            {
                return false;
            }

            lock (_lock)
            {
                foreach (var element in collection)
                {
                    if (element == RTSPMethodType.UnDefined)
                    {
                        continue;
                    }

                    if (_collection.Add(element))
                    {
                        ++result;
                    }
                }

                return result > 0;
            }
        }
    }
}
