using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the extension list that allow duplicated field values
    /// </summary>
    public sealed class StringList
        : IEnumerable
        , IEnumerable<string>
        , ICollection
        , ICollection<string>
        , IReadOnlyCollection<string>
    {
        /// <summary>
        /// Represent the maximum of element
        /// </summary>
        public const int Maximum = 10000;




        /// <summary>
        /// The collection
        /// </summary>
        private readonly IList<string> _collection = new List<string>();





        /// <summary>
        /// Constructor
        /// </summary>
        public StringList()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public StringList(IEnumerable<string> collection)
        {
            AddRange( collection ?? throw new ArgumentNullException(nameof(collection)));
        }





        /// <summary>
        /// Gets an element at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns an instance</returns>
        public string this[int index]
        {
            get => _collection[index];
        }





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _collection;
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
        /// Check if the collection contains some elements
        /// </summary>
        public bool IsEmpty
        {
            get => _collection.Count <= 0;
        }

        /// <summary>
        /// Check if the collection is full
        /// </summary>
        public bool IsFull
        {
            get => _collection.Count >= Maximum;
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count
        {
            get => _collection.Count;
        }




        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Add(string element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (string.IsNullOrWhiteSpace(element))
            {
                throw new ArgumentException(nameof(element));
            }

            if (_collection.Count >= Maximum)
            {
                throw new InvalidOperationException();
            }

            _collection.Add(DataConverter.Filter(element));
        }

        /// <summary>
        /// Add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void AddRange(IEnumerable<string> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var element in collection)
            {
                Add(element);
            }
        }

        /// <summary>
        /// Check if the collection is not empty
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Any()
        {
            return _collection.Count > 0;
        }

        /// <summary>
        /// Remove all elements
        /// </summary>
        public void Clear()
        {
            _collection.Clear();
        }

        /// <summary>
        /// Checks if an element exists
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Contains(string element)
        {
            return _collection.Contains(DataConverter.Filter(element));
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="array">the array</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void CopyTo(Array array)
        {
            CopyTo(array, 0);
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="array">the array</param>
        /// <param name="arrayIndex">the start index when the copy begin</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void CopyTo(Array array, int arrayIndex)
        {
            CopyTo(array as string[], 0);
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="array">the array</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void CopyTo(string[] array)
        {
            CopyTo(array, 0);
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="array">the array</param>
        /// <param name="arrayIndex">the start index when the copy begin</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void CopyTo(string[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns element or an empty string if the element is not found
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns a string value</returns>
        public string ElementAtOrDefault(int index)
        {
            return _collection.ElementAtOrDefault(index) ?? string.Empty;
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
        public IEnumerator<string> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Remove an element
        /// </summary>
        /// <param name="element">the element to be removed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Remove(string element)
        {
            return _collection.Remove(DataConverter.Filter(element));
        }

        /// <summary>
        /// Remove an element at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _collection.Count)
            {
                return false;
            }

            _collection.RemoveAt(index);

            return true;
        }





        /// <summary>
        /// Try to add an element
        /// </summary>
        /// <param name="element">the element name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAdd(string element)
        {
            if (string.IsNullOrWhiteSpace(element) || _collection.Count >= Maximum)
            {
                return false;
            }

            _collection.Add(DataConverter.Filter(element));

            return true;
        }

        /// <summary>
        /// Try to add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <returns>returns the number of element added</returns>
        public bool TryAddRange(IEnumerable<string> collection)
        {
            return TryAddRange(collection, out int result);
        }

        /// <summary>
        /// Try to add elements
        /// </summary>
        /// <param name="collection">the collection of elements</param>
        /// <param name="result">the number of elements added</param>
        /// <returns>returns the number of element added</returns>
        public bool TryAddRange(IEnumerable<string> collection, out int result)
        {
            result = collection?.Where(TryAdd).Count() ?? 0;

            return result > 0;
        }

        /// <summary>
        /// Try to get an element at the desired index
        /// </summary>
        /// <param name="index">the index</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryGetAt(int index, out string result)
        {
            result = _collection.ElementAtOrDefault(index) ?? string.Empty;

            return ! string.IsNullOrWhiteSpace( result );
        }
    }
}
