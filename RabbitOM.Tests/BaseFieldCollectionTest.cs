using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Net.Sdp;
using System;

namespace RabbitOM.Streaming.Tests.Sdp
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
        public void Add_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _collection.Add( null));
        }

        [TestMethod]
        public void AddRang_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _collection.AddRange(null));
        }

        [TestMethod]
        public void Add_NoThrow_Exception()
        {
            try
            {
                _collection.Add(new AttributeField("A", "1"));
                _collection.Add(new AttributeField("B", "2"));
                _collection.Add(new AttributeField("C", "3"));
                _collection.Add(new AttributeField("C", "3"));
            }
            catch (Exception ex)
            {
                Assert.Fail( ex?.ToString() );
            }
        }
    }
}
