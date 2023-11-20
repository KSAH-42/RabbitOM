using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the base field collection class
	/// </summary>
	/// <typeparam name="TField">the type of the field</typeparam>
	public class FieldCollection<TField> 
		: IEnumerable
		, IEnumerable<TField>
		, ICollection
		, ICollection<TField>
		, IReadOnlyCollection<TField>

		where TField : BaseField

	{
		private readonly IList<TField> _collection = new List<TField>();




		/// <summary>
		/// Initialize a new instance of the collection
		/// </summary>
		public FieldCollection()
		{
		}


		/// <summary>
		/// Initialize a new instance of the collection
		/// </summary>
		/// <param name="collection">the collection</param>
		public FieldCollection(IEnumerable<TField> collection)
		{
			AddRange(collection);
		}





		/// <summary>
		/// Gets a field
		/// </summary>
		/// <param name="index">the index</param>
		/// <returns>returns an instance</returns>
		public TField this[int index]
		{
			get => _collection[ index ];
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
		/// Check if the collection is empty
		/// </summary>
		public bool IsEmpty
		{
			get => _collection.Count == 0;
		}

		/// <summary>
		/// Gets the number of fields
		/// </summary>
		public int Count
		{
			get => _collection.Count;
		}

		/// <summary>
		/// Gets the internal items
		/// </summary>
		protected ICollection<TField> Items
		{
			get => _collection;
		}





		/// <summary>
		/// Add a field
		/// </summary>
		/// <param name="field">the field</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public void Add(TField field)
		{
			if ( field == null)
			{
				throw new ArgumentNullException(nameof(field));
			}

			if ( _collection.Contains( field ) )
			{
				throw new ArgumentException("The element already exist", nameof(field));
			}

			_collection.Add( field );
		}

		/// <summary>
		/// Add a collection of field
		/// </summary>
		/// <param name="fields">the field collection</param>
		/// <exception cref="ArgumentNullException"/>
		public void AddRange(IEnumerable<TField> fields)
		{
			if (fields == null)
			{
				throw new ArgumentNullException(nameof(fields));
			}

			foreach (var field in fields)
			{
				Add( field );
			}
		}

		/// <summary>
		/// Remove all fields
		/// </summary>
		public void Clear()
		{
			_collection.Clear();
		}

		/// <summary>
		/// Check if a field exists
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool Contains(TField field)
		{
			return _collection.Contains(field);
		}

		/// <summary>
		/// Copy the content to an array
		/// </summary>
		/// <param name="array">the target array</param>
		/// <param name="arrayIndex">the start index to begin the copy</param>
		public void CopyTo(Array array, int arrayIndex)
		{
			CopyTo(array as TField[], arrayIndex);
		}

		/// <summary>
		/// Copy the content to an array
		/// </summary>
		/// <param name="array">the target array</param>
		/// <param name="arrayIndex">the start index to begin the copy</param>
		public void CopyTo(TField[] array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Remove an existing field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwis false</returns>
		public bool Remove(TField field)
		{
			return _collection.Remove(field);
		}

		/// <summary>
		/// Remove all fields
		/// </summary>
		/// <param name="fields">a field collection</param>
		/// <returns>returns the number of fields removed</returns>
		public int RemoveAll(IEnumerable<TField> fields)
		{
			if (fields == null)
			{
				return 0;
			}

			return _collection
				.Intersect(fields)
				.ToList()
				.Where(_collection.Remove)
				.Count();
		}

		/// <summary>
		/// Remove all fields
		/// </summary>
		/// <param name="predicate">the predicate that select the fields to be removed</param>
		/// <returns>returns the number of fields removed</returns>
		/// <exception cref="ArgumentNullException"/>
		public int RemoveAll(Predicate<TField> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			return _collection
				.Where(field => predicate(field))
				.ToList()
				.Where(_collection.Remove)
				.Count();
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
		public IEnumerator<TField> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		/// <summary>
		/// Gets a field at the desired index
		/// </summary>
		/// <param name="index">the index</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="InvalidOperationException"/>
		public TField GetAt(int index)
		{
			return _collection.ElementAt(index) ?? throw new InvalidOperationException("The returns field is null");
		}

		/// <summary>
		/// Find a field at the desired index
		/// </summary>
		/// <param name="index">the index</param>
		/// <returns>returns an instance, otherwise null is return</returns>
		public TField FindAt(int index)
		{
			return _collection.ElementAtOrDefault(index);
		}

		/// <summary>
		/// Get all fields
		/// </summary>
		/// <param name="predicate">the predicate used for selection</param>
		/// <returns>returns a collection of field</returns>
		/// <exception cref="ArgumentNullException"/>
		public IEnumerable<TField> GetAll(Predicate<TField> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			return _collection.Where(field => predicate(field)).ToList();
		}

		/// <summary>
		/// Try to add a field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool TryAdd(TField field)
		{
			if ( field == null || _collection.Contains( field ) )
			{
				return false;
			}

			_collection.Add(field);

			return true;
		}

		/// <summary>
		/// Try to add a collection of fields
		/// </summary>
		/// <param name="fields">the collection of fields</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool TryAddRange(IEnumerable<TField> fields)
		{
			if (fields == null)
			{
				return false;
			}

			return fields.Where(TryAdd).Count() > 0;
		}

		/// <summary>
		/// Try to add a collection of fields
		/// </summary>
		/// <param name="fields">the collection of fields</param>
		/// <param name="result">the number of fields added</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool TryAddRange(IEnumerable<TField> fields, out int result)
		{
			result = fields?.Where(TryAdd).Count() ?? 0;

			return result > 0;
		}

		/// <summary>
		/// Try to get a field at the desired index
		/// </summary>
		/// <param name="index">the index</param>
		/// <param name="result">the result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool TryGetAt(int index, out TField result)
		{
			result = _collection.ElementAtOrDefault(index);

			return result != null;
		}
	}
}
