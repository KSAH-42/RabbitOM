using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the collection for attribute field that a allow duplicate field names
	/// </summary>
	public sealed class AttributeFieldCollection : FieldCollection<AttributeField>
	{
		private readonly FieldCollectionInternal<AttributeField> _collection = new FieldCollectionInternal<AttributeField>();




		/// <summary>
		/// Initialize a new instance of the collection
		/// </summary>
		public AttributeFieldCollection()
		{
		}

		/// <summary>
		/// Initialize a new instance of the collection
		/// </summary>
		/// <param name="fields">the collection of field</param>
		public AttributeFieldCollection(IEnumerable<AttributeField> fields)
		{
			AddRange(fields);
		}




		/// <summary>
		/// Gets a field
		/// </summary>
		/// <param name="index">the index</param>
		/// <returns>returns an instance</returns>
		public override AttributeField this[int index]
		{
			get => _collection[index];
		}

		/// <summary>
		/// Gets a field
		/// </summary>
		/// <param name="name">the name</param>
		/// <returns>returns an instance</returns>
		public AttributeField this[string name]
		{
			get => GetByName(name);
		}






		/// <summary>
		/// Gets the sync root
		/// </summary>
		public override object SyncRoot
		{
			get => _collection.SyncRoot;
		}

		/// <summary>
		/// Check if the collection is thread safe
		/// </summary>
		public override bool IsSynchronized
		{
			get => _collection.IsSynchronized;
		}

		/// <summary>
		/// Check if the collection is just a read only collection
		/// </summary>
		public override bool IsReadOnly
		{
			get => _collection.IsReadOnly;
		}

		/// <summary>
		/// Check if the collection is empty
		/// </summary>
		public override bool IsEmpty
		{
			get => _collection.IsEmpty;
		}

		/// <summary>
		/// Gets the number of fields
		/// </summary>
		public override int Count
		{
			get => _collection.Count;
		}





		/// <summary>
		/// Add a field
		/// <param name="field">the field</param>
		/// </summary>
		public override void Add(AttributeField field)
		{
			_collection.Add(field);
		}

		/// <summary>
		/// Add a collection of field
		/// </summary>
		/// <param name="fields">the field collection</param>
		public override void AddRange(IEnumerable<AttributeField> fields)
		{
			_collection.AddRange(fields);
		}

		/// <summary>
		/// Remove all fields
		/// </summary>
		public override void Clear()
		{
			_collection.Clear();
		}

		/// <summary>
		/// Check if a field exists
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool Contains(AttributeField field)
		{
			return _collection.Contains(field);
		}

		/// <summary>
		/// Copy the content to an array
		/// </summary>
		/// <param name="array">the target array</param>
		/// <param name="arrayIndex">the start index to begin the copy</param>
		public override void CopyTo(Array array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Copy the content to an array
		/// </summary>
		/// <param name="array">the target array</param>
		/// <param name="arrayIndex">the start index to begin the copy</param>
		public override void CopyTo(AttributeField[] array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Remove an existing field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwis false</returns>
		public override bool Remove(AttributeField field)
		{
			return _collection.Remove(field);
		}

		/// <summary>
		/// Remove all fields
		/// </summary>
		/// <param name="fields">a field collection</param>
		/// <returns>returns the number of fields removed</returns>
		public override int RemoveAll(IEnumerable<AttributeField> fields)
		{
			return _collection.RemoveAll(fields);
		}

		/// <summary>
		/// Remove all fields
		/// </summary>
		/// <param name="predicate">the predicate that select the fields to be removed</param>
		/// <returns>returns the number of fields removed</returns>
		public override int RemoveAll(Predicate<AttributeField> predicate)
		{
			return _collection.RemoveAll(predicate);
		}

		/// <summary>
		/// Gets the enumerator
		/// </summary>
		/// <returns>returns an enumerator</returns>
		public override IEnumerator<AttributeField> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		/// <summary>
		/// Gets a field using it's name
		/// </summary>
		/// <param name="name">the name</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="InvalidOperationException"/>
		public AttributeField GetByName(string name)
		{
			return GetByName(name, true);
		}

		/// <summary>
		/// Gets a field using it's name
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="ignoreCase">set true to ignore the case</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="InvalidOperationException"/>
		public AttributeField GetByName(string name, bool ignoreCase)
		{
			return _collection.First(field => string.Compare(field.Name ?? string.Empty, name ?? string.Empty, ignoreCase) == 0) ?? throw new InvalidOperationException("The returns field is null");
		}

		/// <summary>
		/// Finds a field using it's name
		/// </summary>
		/// <param name="name">the name</param>
		/// <returns>returns an instance,otherwise null</returns>
		public AttributeField FindByName(string name)
		{
			return FindByName(name, true);
		}

		/// <summary>
		/// Gets a field using it's name
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="ignoreCase">set true to ignore the case</param>
		/// <returns>returns an instance,otherwise null</returns>
		public AttributeField FindByName(string name, bool ignoreCase)
		{
			return _collection.FirstOrDefault(field => string.Compare(field.Name ?? string.Empty, name ?? string.Empty, ignoreCase) == 0);
		}

		/// <summary>
		/// Get all fields
		/// </summary>
		/// <param name="predicate">the predicate used for selection</param>
		/// <returns>returns a collection of field</returns>
		public override IEnumerable<AttributeField> GetAll(Predicate<AttributeField> predicate)
		{
			return _collection.GetAll(predicate);
		}

		/// <summary>
		/// Try to add a field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryAdd(AttributeField field)
		{
			return _collection.TryAdd(field);
		}

		/// <summary>
		/// Try to add a collection of fields
		/// </summary>
		/// <param name="fields">the collection of fields</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryAddRange(IEnumerable<AttributeField> fields)
		{
			return _collection.TryAddRange(fields);
		}

		/// <summary>
		/// Try to add a collection of fields
		/// </summary>
		/// <param name="fields">the collection of fields</param>
		/// <param name="result">the number of fields added</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryAddRange(IEnumerable<AttributeField> fields, out int result)
		{
			return _collection.TryAddRange(fields, out result);
		}

		/// <summary>
		/// Try to get a field at the desired index
		/// </summary>
		/// <param name="index">the index</param>
		/// <param name="result">the result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryGetAt(int index, out AttributeField result)
		{
			return _collection.TryGetAt(index, out result);
		}

		/// <summary>
		/// Try to get a field using it's name
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="result">the result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool TryGetByName(string name, out AttributeField result)
		{
			return TryGetByName(name, true, out result);
		}

		/// <summary>
		/// Try to get a field using it's name
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="ignoreCase">set to true to ignore the case</param>
		/// <param name="result">the result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool TryGetByName(string name, bool ignoreCase, out AttributeField result)
		{
			result = _collection.FirstOrDefault(field => string.Compare(field.Name ?? string.Empty, name ?? string.Empty, ignoreCase) == 0);

			return result != null;
		}
	}
}
