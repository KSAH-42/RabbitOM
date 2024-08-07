﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the message method list
    /// </summary>
    public sealed class RtspStringCollection : IEnumerable , IEnumerable<string> , ICollection, ICollection<string>
    {
        /// <summary>
        /// Represent the maximum of element
        /// </summary>
        public  const int               Maximum       = 2000;







        /// <summary>
        /// The lock
        /// </summary>
        private readonly object         _lock         = new object();

        /// <summary>
        /// The collection
        /// </summary>
        private readonly ISet<string>   _collection   = new HashSet<string>( StringComparer.OrdinalIgnoreCase );








        /// <summary>
        /// Constructor
        /// </summary>
        public RtspStringCollection()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspStringCollection( IEnumerable<string> collection )
        {
            AddRange( collection );
        }








        /// <summary>
        /// Gets an element at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public string this[int index]
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
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Add( string element )
        {
            if ( string.IsNullOrWhiteSpace( element ) )
            {
                throw new ArgumentException(nameof(element));
            }

            lock ( _lock )
            {
                if ( _collection.Count >= Maximum )
                {
                    throw new InvalidOperationException();
                }

                if ( ! _collection.Add( RtspDataConverter.Trim( element ) ) )
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
        public void AddRange( IEnumerable<string> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof(collection) );
            }

            lock ( _lock )
            {
                foreach ( var element in collection )
                {
                    if ( _collection.Count >= Maximum )
                    {
                        break;
                    }

                    if ( string.IsNullOrWhiteSpace( element ) )
                    {
                        throw new ArgumentException( "Bad element" );
                    }

                    if ( !  _collection.Add( RtspDataConverter.Trim( element ) ) )
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
            lock ( _lock )
            {
                _collection.Clear();
            }
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains(string element)
        {
            lock (_lock)
            {
                return _collection.Contains(RtspDataConverter.Trim(element));
            }
        }

        /// <summary>
        /// Copy to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="index">the index</param>
        public void CopyTo(Array array, int index)
        {
            CopyTo(array as string[], index);
        }

        /// <summary>
        /// Copy to a target array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="index">the index</param>
        public void CopyTo(string[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _collection.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Get an element at the desired index otherwise throw an exception
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public string ElementAt(int index)
        {
            lock (_lock)
            {
                return _collection.ElementAt(index) ?? string.Empty;
            }
        }

        /// <summary>
        /// Get an element at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public string ElementAtOrDefault(int index)
        {
            lock (_lock)
            {
                return _collection.ElementAtOrDefault(index) ?? string.Empty;
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
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<string> GetEnumerator()
        {
            lock (_lock)
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="element">the element to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove(string element)
        {
            lock (_lock)
            {
                return _collection.Remove(RtspDataConverter.Trim(element));
            }
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd(string element)
        {
            if (string.IsNullOrWhiteSpace(element))
            {
                return false;
            }

            lock (_lock)
            {
                if (_collection.Count >= Maximum)
                {
                    return false;
                }

                return _collection.Add( RtspDataConverter.Trim( element ) );
            }
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange(IEnumerable<string> collection , out int result )
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
                    if (_collection.Count >= Maximum)
                    {
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(element))
                    {
                        continue;
                    }

                    if (_collection.Add( RtspDataConverter.Trim( element ) ) )
                    {
                        ++result;
                    }
                }

                return result > 0;
            }
        }
    }
}
