using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the message header list
    /// </summary>
    public sealed class RTSPHeaderList : IEnumerable<RTSPHeader>
    {
        /// <summary>
        /// Represent the maximum of elements
        /// </summary>
        public  const    int                                   Maximum     = 1000;



        /// <summary>
        /// The lock
        /// </summary>
        private readonly object                                _lock       = new object();

        /// <summary>
        /// The collection
        /// </summary>
        private readonly IDictionary<string,RTSPHeader> _collection = new Dictionary<string,RTSPHeader>( StringComparer.OrdinalIgnoreCase );






        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderList()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPHeaderList( IEnumerable<RTSPHeader> collection )
        {
            AddRange( collection ?? throw new ArgumentNullException( nameof( collection ) ) );
        }






        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPHeader this[int index]
        {
            get => GetAt( index );
        }

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance</returns>
        public RTSPHeader this[string name]
        {
            get => GetByName( name );
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
        public IEnumerator<RTSPHeader> GetEnumerator()
        {
            lock ( _lock )
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
            lock ( _lock )
            {
                return _collection.Count > 0;
            }
        }

        /// <summary>
        /// Check if the collection contains some elements
        /// </summary>
        /// <typeparam name="THeader">the type of header</typeparam>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Any<THeader>() where THeader : RTSPHeader
        {
            lock ( _lock )
            {
                foreach ( var element in _collection.Values )
                {
                    if ( element is THeader )
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Check if the collection contains some elements
        /// </summary>
        /// <param name="predicate">the predicate</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="ArgumentNullException"/>
        public bool Any( Func<RTSPHeader , bool> predicate )
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
        /// <param name="name">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ContainsKey( string name )
        {
            lock ( _lock )
            {
                return _collection.ContainsKey( name ?? string.Empty );
            }
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains( RTSPHeader element )
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
        public bool Add( RTSPHeader element )
        {
            if ( RTSPHeader.IsUnDefined( element ) )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.ContainsKey( element.Name ) )
                {
                    return false;
                }

                if ( _collection.Count >= Maximum )
                {
                    return false;
                }

                _collection[element.Name] = element;

                return true;
            }
        }

        /// <summary>
        /// Add or update an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool AddOrUpdate( RTSPHeader element )
        {
            if ( RTSPHeader.IsUnDefined( element ) )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.ContainsKey( element.Name ) )
                {
                    _collection[element.Name] = element;

                    return true;
                }

                if ( _collection.Count >= Maximum )
                {
                    return false;
                }

                _collection[element.Name] = element;

                return true;
            }
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <returns>returns the number of element added</returns>
        public int AddRange( IEnumerable<RTSPHeader> collection )
        {
            if ( collection == null )
            {
                return 0;
            }

            lock ( _lock )
            {
                int results = 0;

                foreach ( var element in collection )
                {
                    if ( _collection.Count >= Maximum )
                    {
                        break;
                    }

                    if ( RTSPHeader.IsUnDefined( element ) )
                    {
                        continue;
                    }

                    if ( _collection.ContainsKey( element.Name ) )
                    {
                        continue;
                    }

                    _collection[element.Name] = element;

                    ++results;
                }

                return results;
            }
        }

        /// <summary>
        /// Update the header if it already exists
        /// </summary>
        /// <param name="element">the header</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Update( RTSPHeader element )
        {
            if ( RTSPHeader.IsUnDefined( element ) )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( !_collection.ContainsKey( element.Name ) )
                {
                    return false;
                }

                _collection[element.Name] = element;

                return true;
            }
        }

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <returns>returns an instance, otherwise null</returns>
        public THeader Find<THeader>() where THeader : RTSPHeader
        {
            lock ( _lock )
            {
                return _collection.Values.FirstOrDefault( x => x is THeader ) as THeader;
            }
        }

        /// <summary>
        /// Gets a header
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <param name="result">the output result</param>
        /// <returns>returns an instance, otherwise null</returns>
        public bool Find<THeader>( out THeader result ) where THeader : RTSPHeader
        {
            result = Find<THeader>();

            return result != null;
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPHeader FindByName( string name )
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( name ?? string.Empty , out RTSPHeader result ) ? result : null;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance, otherwise null</returns>
        public THeader FindByName<THeader>( string name ) where THeader : RTSPHeader
        {
            lock ( _lock )
            {
                return _collection.TryGetValue( name ?? string.Empty , out RTSPHeader result ) ? result as THeader : null;
            }
        }

        /// <summary>
        /// Finds an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance, otherwise null</returns>
        public RTSPHeader FindAt( int index )
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
        /// Gets element
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns an instance</returns>
        public RTSPHeader GetByName( string name )
        {
            return FindByName( name ) ?? RTSPHeaderNull.Value;
        }

        /// <summary>
        /// Gets an element
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public RTSPHeader GetAt( int index )
        {
            return FindAt( index ) ?? RTSPHeaderNull.Value;
        }

        /// <summary>
        /// Gets all elements
        /// </summary>
        /// <returns>returns a collection</returns>
        public IList<RTSPHeader> GetAll()
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
        public IList<RTSPHeader> GetAll( Func<RTSPHeader , bool> predicate )
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
        /// Gets all elements
        /// </summary>
        /// <typeparam name="THeader">the type of header</typeparam>
        /// <returns>returns a collection</returns>
        public IList<THeader> GetAll<THeader>() where THeader : RTSPHeader
        {
            lock ( _lock )
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
        public IList<THeader> GetAll<THeader>( Func<THeader , bool> predicate ) where THeader : RTSPHeader
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            lock ( _lock )
            {
                return _collection.Values.Cast<THeader>().Where( predicate ).ToList();
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="name">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( string name )
        {
            lock ( _lock )
            {
                return _collection.Remove( name ?? string.Empty );
            }
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="element">the element to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove( RTSPHeader element )
        {
            if ( RTSPHeader.IsUnDefined( element ) )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _collection.Values.Contains( element ) )
                {
                    return _collection.Remove( element.Name );
                }

                return false;
            }
        }

        /// <summary>
        /// Remove elements
        /// </summary>
        /// <param name="names">a collection of names</param>
        /// <returns>returns the number of element removed</returns>
        public int Remove( params string[] names )
        {
            return Remove( names as IEnumerable<string> );
        }

        /// <summary>
        /// Remove elements
        /// </summary>
        /// <param name="collection">a collection of names</param>
        /// <returns>returns the number of element removed</returns>
        public int Remove( IEnumerable<string> collection )
        {
            if ( collection == null )
            {
                return 0;
            }

            lock ( _lock )
            {
                int results = 0;

                foreach ( var name in collection )
                {
                    if ( !_collection.TryGetValue( name ?? string.Empty , out RTSPHeader element ) || element == null )
                    {
                        continue;
                    }

                    if ( _collection.Remove( element.Name ) )
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
            lock ( _lock )
            {
                _collection.Clear();
            }
        }
    }
}
