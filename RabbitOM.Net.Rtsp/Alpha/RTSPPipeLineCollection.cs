using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the pipeline collection
    /// </summary>
    internal sealed class RTSPPipeLineCollection 
        : IEnumerable 
        , IEnumerable<RTSPPipeLine> 
        , ICollection 
        , ICollection<RTSPPipeLine>
        , IReadOnlyCollection<RTSPPipeLine>
    {
        private readonly object _lock = new object();

        private readonly IList<RTSPPipeLine> _collection = new List<RTSPPipeLine>();




        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public RTSPPipeLineCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="collection">the collection</param>
        public RTSPPipeLineCollection( IEnumerable<RTSPPipeLine> collection )
        {
            AddRange( collection );
        }





        /// <summary>
        /// Gets a field at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPPipeLine this[ int index ]
        {
            get
            {
                lock ( _lock )
                {
                    return _collection[ index ];
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
            get => false;
        }

        /// <summary>
        /// Check if the collection is just a read only collection
        /// </summary>
        public bool IsReadOnly
        {
            get => false;
        }

        /// <summary>
        /// Check if the collection is empty
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
        /// Gets the number of fields
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
        /// Add a field
        /// </summary>
        /// <param name="field">the field</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void Add( RTSPPipeLine field )
        {
            if ( field == null )
            {
                throw new ArgumentNullException( nameof( field ) );
            }

            if ( _collection.Contains( field ) )
            {
                throw new ArgumentException( "The element already exist" , nameof( field ) );
            }

            lock ( _lock )
            {
                _collection.Add( field );
            }
        }

        /// <summary>
        /// Add a collection of field
        /// </summary>
        /// <param name="fields">the field collection</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddRange( IEnumerable<RTSPPipeLine> fields )
        {
            if ( fields == null )
            {
                throw new ArgumentNullException( nameof( fields ) );
            }

            foreach ( var field in fields )
            {
                Add( field );
            }
        }

        /// <summary>
        /// Check if the collection is not empty
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Any()
        {
			lock ( _lock )
			{
				return _collection.Count > 0; 
			}
        }

        /// <summary>
        /// Remove all fields
        /// </summary>
        public void Clear()
        {
			lock ( _lock )
			{
				_collection.Clear(); 
			}
        }

        /// <summary>
        /// Check if a field exists
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RTSPPipeLine field )
        {
			lock ( _lock )
			{
				return _collection.Contains( field ); 
			}
        }

        /// <summary>
        /// Copy the content to an array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the start index to begin the copy</param>
        public void CopyTo( Array array , int arrayIndex )
        {
            CopyTo( array as RTSPPipeLine[] , arrayIndex );
        }

        /// <summary>
        /// Copy the content to an array
        /// </summary>
        /// <param name="array">the target array</param>
        /// <param name="arrayIndex">the start index to begin the copy</param>
        public void CopyTo( RTSPPipeLine[] array , int arrayIndex )
        {
			lock ( _lock )
			{
				_collection.CopyTo( array , arrayIndex ); 
			}
        }

        /// <summary>
        /// Gets a field at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="InvalidOperationException"/>
        public RTSPPipeLine ElementAt( int index )
        {
			lock ( _lock )
			{
				return _collection.ElementAt( index ) ?? throw new InvalidOperationException( "The returns field is null" ); 
			}
        }

        /// <summary>
        /// Find a field at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null is return</returns>
        public RTSPPipeLine ElementAtOrDefault( int index )
        {
			lock ( _lock )
			{
				return _collection.ElementAtOrDefault( index ); 
			}
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        public IEnumerator<RTSPPipeLine> GetEnumerator()
        {
			lock ( _lock )
			{
				return _collection.GetEnumerator(); 
			}
        }

        /// <summary>
        /// Remove an existing field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns true for a success, otherwis false</returns>
        public bool Remove( RTSPPipeLine field )
        {
			lock ( _lock )
			{
				return _collection.Remove( field ); 
			}
        }

        /// <summary>
        /// Remove all fields
        /// </summary>
        /// <param name="fields">a field collection</param>
        /// <returns>returns the number of fields removed</returns>
        public int RemoveAll( IEnumerable<RTSPPipeLine> fields )
        {
            if ( fields == null )
            {
                return 0;
            }

			lock ( _lock )
			{
				return _collection
						.Intersect( fields )
						.ToList()
						.Where( _collection.Remove )
						.Count(); 
			}
        }

        /// <summary>
        /// Remove all fields
        /// </summary>
        /// <param name="predicate">the predicate that select the fields to be removed</param>
        /// <returns>returns the number of fields removed</returns>
        /// <exception cref="ArgumentNullException"/>
        public int RemoveAll( Func<RTSPPipeLine , bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

			lock ( _lock )
			{
				return _collection
						.Where( predicate )
						.ToList()
						.Where( _collection.Remove )
						.Count(); 
			}
        }

        /// <summary>
        /// Try to add a field
        /// </summary>
        /// <param name="field">the field</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd( RTSPPipeLine field )
        {
            if ( field == null || _collection.Contains( field ) )
            {
                return false;
            }

			lock ( _lock )
			{
				_collection.Add( field );

				return true; 
			}
        }

        /// <summary>
        /// Try to add a collection of fields
        /// </summary>
        /// <param name="fields">the collection of fields</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange( IEnumerable<RTSPPipeLine> fields )
        {
            return TryAddRange( fields , out int result );
        }

        /// <summary>
        /// Try to add a collection of fields
        /// </summary>
        /// <param name="fields">the collection of fields</param>
        /// <param name="result">the number of fields added</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddRange( IEnumerable<RTSPPipeLine> fields , out int result )
        {
			lock ( _lock )
			{
				result = fields?.Where( TryAdd ).Count() ?? 0;

				return result > 0; 
			}
        }

        /// <summary>
        /// Try to get a field at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryElementAt( int index , out RTSPPipeLine result )
        {
			lock ( _lock )
			{
				result = _collection.ElementAtOrDefault( index );

				return result != null; 
			}
        }
    }
}
