using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the base fiel collection class
	/// </summary>
	/// <typeparam name="TField">the type of the field</typeparam>
	public abstract class FieldCollection<TField> : ICollection, ICollection<TField>, IEnumerable<TField>, IEnumerable
		where TField : BaseField
	{
		/// <summary>
		/// Gets a field
		/// </summary>
		/// <param name="index">the index</param>
		/// <returns>returns an instance</returns>
		public abstract TField this[int index]
		{
			get;
		}






		/// <summary>
		/// Gets the sync root
		/// </summary>
		public abstract object SyncRoot
		{
			get;
		}

		/// <summary>
		/// Check if the collection is thread safe
		/// </summary>
		public abstract bool IsSynchronized
		{
			get;
		}

		/// <summary>
		/// Check if the collection is just a read only collection
		/// </summary>
		public abstract bool IsReadOnly
		{
			get;
		}

		/// <summary>
		/// Check if the collection is empty
		/// </summary>
		public abstract bool IsEmpty
		{
			get;
		}

		/// <summary>
		/// Gets the number of fields
		/// </summary>
		public abstract int Count
		{
			get;
		}





		/// <summary>
		/// Add a field
		/// </summary>
		/// <param name="field">the field</param>
		public abstract void Add(TField field);

		/// <summary>
		/// Add a collection of field
		/// </summary>
		/// <param name="fields">the field collection</param>
		public abstract void AddRange(IEnumerable<TField> fields);

		/// <summary>
		/// Remove all fields
		/// </summary>
		public abstract void Clear();

		/// <summary>
		/// Check if a field exists
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool Contains(TField field);

		/// <summary>
		/// Copy the content to an array
		/// </summary>
		/// <param name="array">the target array</param>
		/// <param name="arrayIndex">the start index to begin the copy</param>
		public abstract void CopyTo(Array array, int arrayIndex);

		/// <summary>
		/// Copy the content to an array
		/// </summary>
		/// <param name="array">the target array</param>
		/// <param name="arrayIndex">the start index to begin the copy</param>
		public abstract void CopyTo(TField[] array, int arrayIndex);

		/// <summary>
		/// Remove an existing field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwis false</returns>
		public abstract bool Remove(TField field);

		/// <summary>
		/// Remove all fields
		/// </summary>
		/// <param name="fields">a field collection</param>
		/// <returns>returns the number of fields removed</returns>
		public abstract int RemoveAll(IEnumerable<TField> fields);

		/// <summary>
		/// Remove all fields
		/// </summary>
		/// <param name="predicate">the predicate that select the fields to be removed</param>
		/// <returns>returns the number of fields removed</returns>
		public abstract int RemoveAll(Predicate<TField> predicate);

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
		public abstract IEnumerator<TField> GetEnumerator();

		/// <summary>
		/// Get all fields
		/// </summary>
		/// <param name="predicate">the predicate used for selection</param>
		/// <returns>returns a collection of field</returns>
		public abstract IEnumerable<TField> GetAll(Predicate<TField> predicate);

		/// <summary>
		/// Try to add a field
		/// </summary>
		/// <param name="field">the field</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool TryAdd(TField field);

		/// <summary>
		/// Try to add a collection of fields
		/// </summary>
		/// <param name="fields">the collection of fields</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool TryAddRange(IEnumerable<TField> fields);

		/// <summary>
		/// Try to add a collection of fields
		/// </summary>
		/// <param name="fields">the collection of fields</param>
		/// <param name="result">the number of fields added</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool TryAddRange(IEnumerable<TField> fields, out int result);

		/// <summary>
		/// Try to get a field at the desired index
		/// </summary>
		/// <param name="index">the index</param>
		/// <param name="result">the result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool TryGetAt(int index, out TField result);
	}
}
