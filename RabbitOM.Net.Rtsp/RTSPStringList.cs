using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the message method list
    /// </summary>
    public sealed class RTSPStringList : IEnumerable , IEnumerable<string> , ICollection, ICollection<string>
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
        public RTSPStringList()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPStringList( IEnumerable<string> collection )
        {
            AddRange( collection );
        }








        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public string this[int index]
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
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<string> GetEnumerator()
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
        public bool Contains( string element )
        {
            lock ( _lock )
            {
                return _collection.Contains( RTSPDataFilter.Trim( element ) );
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

                if ( ! _collection.Add( RTSPDataFilter.Trim( element ) ) )
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

                    if ( !  _collection.Add( RTSPDataFilter.Trim( element ) ) )
                    {
                        throw new ArgumentException("Duplicated value");
                    }
                }
            }
        }

        /// <summary>
        /// Get an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public string GetAt( int index )
        {
            lock ( _lock )
            {
                if ( index < 0 || index >= _collection.Count )
                {
                    return string.Empty;
                }

                return _collection.ElementAt( index ) ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <returns>returns a collection</returns>
        public IList<string> GetAll()
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
        public IList<string> GetAll( Func<string , bool> predicate )
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
        public bool Remove( string element )
        {
            lock ( _lock )
            {
                return _collection.Remove( RTSPDataFilter.Trim( element ) );
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

                return _collection.Add(RTSPDataFilter.Trim(element));
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

                    if (_collection.Add(RTSPDataFilter.Trim(element)))
                    {
                        ++result;
                    }
                }

                return result > 0;
            }
        }
	}
}
