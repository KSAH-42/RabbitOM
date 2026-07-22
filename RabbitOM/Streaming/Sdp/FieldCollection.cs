using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Sdp
{
    public class FieldCollection<TField> : ICollection , ICollection<TField> , IReadOnlyCollection<TField> where TField : BaseField
    {
        private readonly IList<TField> _collection = new List<TField>();




        public FieldCollection()
        {
        }

        public FieldCollection(IEnumerable<TField> collection)
        {
            AddRange(collection);
        }





        public TField this[int index]
        {
            get => _collection[ index ];
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

        public int Count
        {
            get => _collection.Count;
        }

        protected ICollection<TField> Items
        {
            get => _collection;
        }





        public void Add(TField item)
        {
            if ( item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if ( _collection.Contains( item ) )
            {
                throw new ArgumentException("The element already exist", nameof(item));
            }

            _collection.Add( item );
        }

        public void AddRange(IEnumerable<TField> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (var item in items)
            {
                Add( item );
            }
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(TField item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array as TField[], index);
        }

        public void CopyTo(TField[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public IEnumerator<TField> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public bool Remove(TField item)
        {
            return _collection.Remove(item);
        }

        public bool TryAdd(TField item)
        {
            if ( item == null || _collection.Contains( item ) )
            {
                return false;
            }

            _collection.Add(item);

            return true;
        }

        public bool TryAddRange(IEnumerable<TField> fields)
        {
            return TryAddRange( fields , out int result );
        }

        public bool TryAddRange(IEnumerable<TField> fields, out int result)
        {
            result = fields?.Where(TryAdd).Count() ?? 0;

            return result > 0;
        }

        public bool TryElementAt(int index, out TField result)
        {
            result = _collection.ElementAtOrDefault(index);

            return result != null;
        }
    }
}
