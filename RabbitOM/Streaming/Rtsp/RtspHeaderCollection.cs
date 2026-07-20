using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the message header collection
    /// </summary>
    public sealed class RtspHeaderCollection : IEnumerable , IEnumerable<RtspHeader> , ICollection , ICollection<RtspHeader>
    {
        /// <summary>
        /// Represent the maximum of elements
        /// </summary>                
        public const int Maximum = 1000;




        /// <summary>
        /// The lock
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The collection
        /// </summary>
        private readonly IDictionary<string, RtspHeader> _collection = new Dictionary<string, RtspHeader>(StringComparer.OrdinalIgnoreCase);






        /// <summary>
        /// Constructor
        /// </summary>
        public RtspHeaderCollection()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspHeaderCollection(IEnumerable<RtspHeader> collection)
        {
            AddRange(collection);
        }






        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RtspHeader this[int index]
        {
            get => GetAt(index);
        }

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <returns>returns an instance</returns>
        public RtspHeader this[string name]
        {
            get => GetByName(name);
        }





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return _lock;
            }
        }

        /// <summary>
        /// Check if it is thread safe. It returns true.
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Check if the collection is readonly or not. It returns true.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the number of headers.
        /// </summary>
        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _collection.Count;
                }
            }
        }

        /// <summary>
        /// Check if the collection contains some headers
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock (_lock)
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
                lock (_lock)
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
            lock (_lock)
            {
                return _collection.Values.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<RtspHeader> GetEnumerator()
        {
            lock (_lock)
            {
                return _collection.Values.ToList().GetEnumerator();
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
        /// Check if the collection contains some elements
        /// </summary>
        /// <typeparam name="THeader">the type of header</typeparam>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any<THeader>() where THeader : RtspHeader
        {
            lock (_lock)
            {
                return _collection.Values.Any(x => x is THeader);
            }
        }

        /// <summary>
        /// Check if the collection contains some elements
        /// </summary>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="ArgumentNullException"/>
        public bool Any(Func<RtspHeader, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            lock (_lock)
            {
                foreach (var element in _collection.Values)
                {
                    if (element != null && predicate(element))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if a header exists
        /// </summary>
        /// <param name="name">the header name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ContainsKey(string name)
        {
            lock (_lock)
            {
                return _collection.ContainsKey(name ?? string.Empty);
            }
        }

        /// <summary>
        /// Checks if a header exists
        /// </summary>
        /// <param name="header">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains(RtspHeader header)
        {
            if (header == null)
            {
                return false;
            }

            lock (_lock)
            {
                return _collection.Values.Contains(header);
            }
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Add(RtspHeader header)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }

            if (RtspHeader.IsUnDefined(header))
            {
                throw new ArgumentException(nameof(header));
            }

            lock (_lock)
            {
                if (_collection.Count >= Maximum)
                {
                    throw new InvalidOperationException("Collection is full");
                }

                _collection.Add(header.Name, header);
            }
        }

        /// <summary>
        /// Add or update a header
        /// </summary>
        /// <param name="header">the header</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void AddOrUpdate(RtspHeader header)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }

            if (RtspHeader.IsUnDefined(header))
            {
                throw new ArgumentException(nameof(header));
            }

            lock (_lock)
            {
                if (_collection.ContainsKey(header.Name))
                {
                    _collection[header.Name] = header;
                    return;
                }

                if (_collection.Count < Maximum)
                {
                    _collection[header.Name] = header;
                }
                
                throw new InvalidOperationException("Collection is full");
            }
        }

        /// <summary>
        /// Try to add elements
        /// </summary>
        /// <param name="collection">the collection of headers</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void AddRange(IEnumerable<RtspHeader> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException( nameof(collection) );
            }

            lock (_lock)
            {
                foreach (var element in collection)
                {
                    if (_collection.Count >= Maximum)
                    {
                        break;
                    }

                    if (RtspHeader.IsUnDefined(element))
                    {
                        throw new ArgumentException( "bad header" );
                    }

                    _collection.Add( element.Name , element );
                }
            }
        }

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <returns>returns an instance, otherwise null</returns>
        public THeader Find<THeader>() where THeader : RtspHeader
        {
            lock (_lock)
            {
                return _collection.Values.FirstOrDefault(x => x is THeader) as THeader;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RtspHeader FindByName(string name)
        {
            lock (_lock)
            {
                return _collection.TryGetValue(name ?? string.Empty, out RtspHeader result) ? result : null;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance, otherwise null</returns>
        public THeader FindByName<THeader>(string name) where THeader : RtspHeader
        {
            lock (_lock)
            {
                return _collection.TryGetValue(name ?? string.Empty, out RtspHeader result) ? result as THeader : null;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RtspHeader FindAt(int index)
        {
            lock (_lock)
            {
                return _collection.Values.ElementAtOrDefault(index);
            }
        }

        /// <summary>
        /// Gets element
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance</returns>
        public RtspHeader GetByName(string name)
        {
            return FindByName(name) ?? RtspHeaderNull.Value;
        }

        /// <summary>
        /// Gets an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RtspHeader GetAt(int index)
        {
            return FindAt(index) ?? RtspHeaderNull.Value;
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <returns>returns a collection</returns>
        public IList<RtspHeader> GetAll()
        {
            lock (_lock)
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
        public IList<RtspHeader> GetAll(Func<RtspHeader, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            lock (_lock)
            {
                return _collection.Values.Where(predicate).ToList();
            }
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <typeparam name="THeader">the type of header</typeparam>
        /// <returns>returns a collection</returns>
        public IList<THeader> GetAll<THeader>() where THeader : RtspHeader
        {
            lock (_lock)
            {
                return _collection.Values.Cast<THeader>().ToList();
            }
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <typeparam name="THeader">the type of header</typeparam>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns a collection</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<THeader> GetAll<THeader>(Func<THeader, bool> predicate) where THeader : RtspHeader
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            lock (_lock)
            {
                return _collection.Values.Cast<THeader>().Where(predicate).ToList();
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove(string name)
        {
            lock (_lock)
            {
                return _collection.Remove(name ?? string.Empty);
            }
        }

        /// <summary>
        /// Remove an existing element
        /// </summary>
        /// <param name="header">the element to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove(RtspHeader header)
        {
            if (RtspHeader.IsUnDefined(header))
            {
                return false;
            }

            lock (_lock)
            {
                if (_collection.Values.Contains(header)) // the instance should be present
                {
                    return _collection.Remove(header.Name);
                }

                return false;
            }
        }

        /// <summary>
        /// Remove elements
        /// </summary>
        /// <param name="names">a collection of names</param>
        /// <returns>returns the number of element removed</returns>
        public int RemoveRange(params string[] names)
        {
            return RemoveRange(names as IEnumerable<string>);
        }

        /// <summary>
        /// Remove elements
        /// </summary>
        /// <param name="collection">a collection of names</param>
        /// <returns>returns the number of element removed</returns>
        public int RemoveRange(IEnumerable<string> collection)
        {
            if (collection == null)
            {
                return 0;
            }

            lock (_lock)
            {
                int results = 0;

                foreach (var name in collection)
                {
                    if (_collection.Remove(name ?? string.Empty))
                    {
                        ++results;
                    }
                }

                return results;
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
        /// Copy elements to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="index">the array index</param>
        public void CopyTo(Array array, int index)
        {
            CopyTo( array as RtspHeader[] , index );
        }

        /// <summary>
        /// Copy elements to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the array index</param>
        public void CopyTo(RtspHeader[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _collection.Values.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd(RtspHeader header)
        {
            if (RtspHeader.IsUnDefined(header))
            {
                return false;
            }

            lock (_lock)
            {
                if (_collection.Count >= Maximum)
                {
                    return false;
                }

                if (_collection.ContainsKey(header.Name))
                {
                    return false;
                }

                _collection[header.Name] = header;

                return true;
            }
        }

        /// <summary>
        /// Add or update a header
        /// </summary>
        /// <param name="header">the header</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddOrUpdate(RtspHeader header)
        {
            if (RtspHeader.IsUnDefined(header))
            {
                return false;
            }

            lock (_lock)
            {
                if (_collection.ContainsKey(header.Name))
                {
                    _collection[header.Name] = header;

                    return true;
                }

                if (_collection.Count >= Maximum)
                {
                    return false;
                }

                _collection[header.Name] = header;

                return true;
            }
        }

        /// <summary>
        /// Try to add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange(IEnumerable<RtspHeader> collection)
        {
            return TryAddRange(collection, out int result);
        }

        /// <summary>
        /// Try to add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <param name="result">the number of headers added</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange(IEnumerable<RtspHeader> collection, out int result)
        {
            result = 0;

            if (collection == null)
            {
                return false;
            }

            lock (_lock)
            {
                foreach (var element in collection)
                {
                    if (_collection.Count >= Maximum)
                    {
                        break;
                    }

                    if (RtspHeader.IsUnDefined(element))
                    {
                        continue;
                    }

                    if (_collection.ContainsKey(element.Name))
                    {
                        continue;
                    }

                    _collection[element.Name] = element;

                    ++result;
                }

                return result > 0;
            }
        }

        /// <summary>
        /// Add or update a header range collection
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddOrUpdateRange( IEnumerable<RtspHeader> collection )
        {
            return TryAddOrUpdateRange( collection , out int result );
        }

        /// <summary>
        /// Add or update a header range collection
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddOrUpdateRange( IEnumerable<RtspHeader> collection , out int result )
        {
            result = 0;

            lock (_lock)
            {
                foreach ( var header in collection )
                {
                    if ( RtspHeader.IsUnDefined( header ) )
                    {
                        continue;
                    }
            
                    if ( _collection.ContainsKey( header.Name ) )
                    {
                        _collection[ header.Name ] = header;

                        return true;
                    }

                    if ( _collection.Count >= Maximum )
                    {
                        return false;
                    }

                    _collection[ header.Name ] = header;

                    result ++;
                }
                
                return result > 0;
            }
        }
    }
}
