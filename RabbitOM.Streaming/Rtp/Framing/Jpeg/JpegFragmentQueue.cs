using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a the fragment queue collection
    /// </summary>
    public sealed class JpegFragmentQueue : IEnumerable , IEnumerable<JpegFragment> , IReadOnlyCollection<JpegFragment>
    {
        private readonly Queue<JpegFragment> _collection;



        /// <summary>
        /// Initialize a new instance of the fragment queue
        /// </summary>

        public JpegFragmentQueue()
        {
            _collection = new Queue<JpegFragment>();
        }

        /// <summary>
        /// Initialize a new instance of the fragment queue
        /// </summary>
        /// <param name="capacity">the capacity</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public JpegFragmentQueue( int capacity )
        {
            _collection = new Queue<JpegFragment>( capacity );
        }


        /// <summary>
        /// Initialize a new instance of the fragment queue
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public JpegFragmentQueue( IEnumerable<JpegFragment> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            _collection = new Queue<JpegFragment>();  // avoid a filter using linq and pass the result on the constuctor for performance reason, look using reflector

            foreach ( var element in collection )
            {
                _collection.Enqueue( element ?? throw new ArgumentNullException( "Bad element" , nameof( collection ) ) );
            }
        }



        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count
        {
            get => _collection.Count;
        }

        /// <summary>
        /// Check if the collection empty
        /// </summary>
        public bool IsEmpty
        {
            get => _collection.Count == 0;
        }



        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>returns an instance</returns>
        public IEnumerator<JpegFragment> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Check if the collection has elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any()
        {
            return _collection.Count > 0;
        }

        /// <summary>
        /// Check if the collection contains a particular elements
        /// </summary>
        /// <param name="fragment">the fragment</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( JpegFragment fragment )
        {
            return _collection.Contains( fragment );
        }

        /// <summary>
        /// Clear the collection
        /// </summary>
        public void Clear()
        {
            _collection.Clear();
        }

        /// <summary>
        /// Create an array of elements
        /// </summary>
        /// <returns>returns an array</returns>
        public JpegFragment[] ToArray()
        {
            return _collection.ToArray();
        }

        /// <summary>
        /// Enqueue an element
        /// </summary>
        /// <param name="fragment">the fragment</param>
        /// <exception cref="ArgumentNullException"/>
        public void Enqueue( JpegFragment fragment )
        {
            _collection.Enqueue( fragment ?? throw new ArgumentNullException( nameof( fragment ) ) );
        }

        /// <summary>
        /// Dequeue a fragment
        /// </summary>
        /// <returns>returns an instance, otherwise it throw an exception</returns>
        /// <exception cref="InvalidOperationException"/>
        public JpegFragment Dequeue()
        {
            return _collection.Dequeue() ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Peek a fragment
        /// </summary>
        /// <returns>returns an instance, otherwise it thrown an exception</returns>
        /// <exception cref="InvalidOperationException"/>
        public JpegFragment Peek()
        {
            return _collection.Peek() ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Try to enqueue an element
        /// </summary>
        /// <param name="fragment">the fragment</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryEnqueue( JpegFragment fragment )
        {
            if ( fragment == null )
            {
                return false;
            }

            _collection.Enqueue( fragment );

            return true;
        }

        /// <summary>
        /// Try to dequeue an element
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryDequeue( out JpegFragment result )
        {
            result = _collection.Count > 0 ? _collection.Dequeue() : null;

            return result != null;
        }

        /// <summary>
        /// Try to peek an element
        /// </summary>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryPeek( out JpegFragment result )
        {
            result = _collection.Count > 0 ? _collection.Peek() : null;

            return result != null;
        }
    }
}