using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Sdp
{
    public sealed class StringList : ICollection , ICollection<string> , IReadOnlyCollection<string>
    {
        public const int Maximum = 10000;




        private readonly IList<string> _collection = new List<string>();





        public StringList()
        {
        }

        public StringList(IEnumerable<string> collection)
        {
            AddRange( collection ?? throw new ArgumentNullException(nameof(collection)));
        }





        public string this[int index]
        {
            get => _collection[index];
        }





        public object SyncRoot
        {
            get => _collection;
        }

        public bool IsSynchronized
        {
            get => false;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public bool IsEmpty
        {
            get => _collection.Count <= 0;
        }

        public bool IsFull
        {
            get => _collection.Count >= Maximum;
        }

        public int Count
        {
            get => _collection.Count;
        }




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

        public bool Any()
        {
            return _collection.Count > 0;
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(string element)
        {
            return _collection.Contains(DataConverter.Filter(element));
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array as string[], index);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public string ElementAtOrDefault(int index)
        {
            return _collection.ElementAtOrDefault(index) ?? string.Empty;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove(string element)
        {
            return _collection.Remove(DataConverter.Filter(element));
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _collection.Count)
            {
                return false;
            }

            _collection.RemoveAt(index);

            return true;
        }





        public bool TryAdd(string element)
        {
            if (string.IsNullOrWhiteSpace(element) || _collection.Count >= Maximum)
            {
                return false;
            }

            _collection.Add(DataConverter.Filter(element));

            return true;
        }

        public bool TryAddRange(IEnumerable<string> collection)
        {
            return TryAddRange(collection, out int result);
        }

        public bool TryAddRange(IEnumerable<string> collection, out int result)
        {
            result = collection?.Where(TryAdd).Count() ?? 0;

            return result > 0;
        }

        public bool TryGetAt(int index, out string result)
        {
            result = _collection.ElementAtOrDefault(index) ?? string.Empty;

            return ! string.IsNullOrWhiteSpace( result );
        }
    }
}
