using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent the message method list
    /// </summary>
    public sealed class RTSPMethodTypeReadonlyList : IEnumerable<RTSPMethodType>
    {
        private readonly RTSPMethodTypeList _collection = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPMethodTypeReadonlyList( RTSPMethodTypeList collection )
        {
            _collection = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }



        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPMethodType this[int index]
        {
            get => _collection[ index ];
        }



        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count
        {
            get => _collection.Count;
        }

        /// <summary>
        /// Check if the collection contains some elements
        /// </summary>
        public bool IsEmpty
        {
            get => _collection.IsEmpty;
        }



        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<RTSPMethodType> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Check if the collection contains some elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any()
        {
            return _collection.Any();
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RTSPMethodType element )
        {
            return _collection.Contains( element );
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPMethodType? FindAt( int index )
        {
            return _collection.FindAt( index );
        }

        /// <summary>
        /// Gets an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPMethodType GetAt( int index )
        {
            return _collection.GetAt( index );
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <returns>returns a collection</returns>
        public IList<RTSPMethodType> GetAll()
        {
            return _collection.GetAll();
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns a collection</returns>
        public IList<RTSPMethodType> GetAll( Func<RTSPMethodType , bool> predicate )
        {
            return _collection.GetAll( predicate );
        }
    }
}
