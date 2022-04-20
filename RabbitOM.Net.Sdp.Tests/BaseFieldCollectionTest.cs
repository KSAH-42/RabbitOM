using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace RabbitOM.Net.Sdp.Tests
{
	[TestClass]
	public class BaseFieldCollectionTest
	{
		public readonly BaseFieldCollection _collection = new BaseFieldCollection();
	    
		[TestMethod]
		public void SyncRoot_Excpect_IsNotNull()
		{
			Assert.IsNotNull(_collection.SyncRoot);
		}

		[TestMethod]
		public void IsSynchronize_Returns_False()
		{
			Assert.IsFalse(_collection.IsSynchronized);
		}

		[TestMethod]
		public void IsReadOnly_Returns_False()
		{
			Assert.IsFalse(_collection.IsReadOnly);
		}

		[TestMethod]
		public void IsEmpty_Returns_True()
		{
			Assert.IsTrue(_collection.IsEmpty);
		}

		[TestMethod]
		public void Count_Returns_Zero()
		{
			Assert.AreEqual(_collection.Count , 0 );
		}

		[TestMethod]
		public void Add_Except_ArgumentNullException()
		{
			Assert.ThrowsException<ArgumentNullException>( () => _collection.Add( null));
		}

		[TestMethod]
		public void AddRang_Except_ArgumentNullException()
		{
			Assert.ThrowsException<ArgumentNullException>(() => _collection.AddRange(null));
		}

		[TestMethod]
		public void GetAt_Except_Exception()
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _collection.GetAt(-1));
		}

		[TestMethod]
		public void FindAt_Returns_Null()
		{
			Assert.IsNull( _collection.FindAt(-1) );
		}

		[TestMethod]
		public void FindAll_Exception_Empty()
		{
			Assert.IsTrue(_collection.FindAll( x => true ).Count() == 0 );
		}

		[TestMethod]
		public void Add_Except_NotException()
		{
			Assert.ThrowsException<Exception>(() => _collection.Add(new AttributeField("A", "1")));
			Assert.ThrowsException<Exception>(() => _collection.Add(new AttributeField("B", "2")));
			Assert.ThrowsException<Exception>(() => _collection.Add(new AttributeField("C", "3")));
		}

	}
}
