using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent the message method list
    /// </summary>
    public sealed class RtspMethodList 
        : IEnumerable 
        , IEnumerable<RtspMethod> 
        , ICollection 
        , ICollection<RtspMethod>
        , IReadOnlyCollection<RtspMethod>
    {
        private readonly object           _lock       = new object();

        private readonly ISet<RtspMethod> _collection = new HashSet<RtspMethod>();








        /// <summary>
        /// Constructor
        /// </summary>
        public RtspMethodList()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspMethodList( IEnumerable<RtspMethod> collection )
        {
            AddRange( collection );
        }









        /// <summary>
        /// Gets an element at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RtspMethod this[int index]
        {
            get => ElementAt( index );
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
        public IEnumerator<RtspMethod> GetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <exception cref="ArgumentException"/>
        public void Add(RtspMethod element)
        {
            if (element == RtspMethod.UnDefined)
            {
                throw new ArgumentException( nameof(element) );
            }

            lock ( _lock )
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
        public void AddRange(IEnumerable<RtspMethod> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException( nameof(collection) );
            }

            lock (_lock)
            {
                foreach (var element in collection)
                {
                    if (element == RtspMethod.UnDefined)
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
        /// Check if the collection contains some elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any()
        {
            lock (_lock)
            {
                return _collection.Count > 0;
            }
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _collection.Clear();
            }
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains(RtspMethod element)
        {
            lock (_lock)
            {
                return _collection.Contains(element);
            }
        }

        /// <summary>
        /// Copy to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="index">the index</param>
        public void CopyTo(Array array, int index)
        {
            CopyTo(array as RtspMethod[], index);
        }

        /// <summary>
        /// Copy to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the index</param>
        public void CopyTo(RtspMethod[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _collection.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Get an element at the desired index or throw an exception
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns a value</returns>
        public RtspMethod ElementAt(int index)
        {
            lock (_lock)
            {
                return _collection.ElementAt(index);
            }
        }

        /// <summary>
        /// Get an element at the desired index or the default value
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns a value</returns>
        public RtspMethod ElementAtOrDefault(int index)
        {
            lock (_lock)
            {
                return _collection.ElementAtOrDefault(index);
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RtspMethod? FindAt( int index )
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
        /// Remove an element
        /// </summary>
        /// <param name="element">the element to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( RtspMethod element )
        {
            lock ( _lock )
            {
                return _collection.Remove( element );
            }
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd(RtspMethod element)
        {
            if (element == RtspMethod.UnDefined)
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
        public bool TryAddRange(IEnumerable<RtspMethod> collection)
        {
            return TryAddRange(collection, out int result);
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange(IEnumerable<RtspMethod> collection, out int result)
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
                    if (element == RtspMethod.UnDefined)
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
