using NUnit.Framework;
using RabbitOM.Streaming.Net.Sdp;
using System;

namespace RabbitOM.Streaming.Tests.Sdp
{
    [TestFixture]
    public class BaseFieldCollectionTest
    {
        
        [Test]
        public void SyncRoot_Excpect_IsNotNull()
        {
            var collection = new BaseFieldCollection();
            Assert.IsNotNull(collection.SyncRoot);
        }

        [Test]
        public void IsSynchronize_Returns_False()
        {
            var collection = new BaseFieldCollection();
            Assert.IsFalse( collection.IsSynchronized);
        }

        [Test]
        public void IsReadOnly_Returns_False()
        {
            var collection = new BaseFieldCollection();
            Assert.IsFalse( collection.IsReadOnly);
        }

        [Test]
        public void IsEmpty_Returns_True()
        {
            var collection = new BaseFieldCollection();
            Assert.IsTrue( collection.IsEmpty);
        }

        [Test]
        public void Count_Returns_Zero()
        {
            var collection = new BaseFieldCollection();
            Assert.AreEqual(0 , collection.Count );
        }

        [Test]
        public void Add_Throw_ArgumentNullException()
        {
            var collection = new BaseFieldCollection();
            Assert.Throws<ArgumentNullException>( () => collection.Add( null));
        }

        [Test]
        public void AddRang_Throw_ArgumentNullException()
        {
            var collection = new BaseFieldCollection();
            Assert.Throws<ArgumentNullException>(() => collection.AddRange(null));
        }

        [Test]
        public void Add_NoThrow_Exception()
        {
            var collection = new BaseFieldCollection();
            try
            {
                collection.Add(new AttributeField("A", "1"));
                collection.Add(new AttributeField("B", "2"));
                collection.Add(new AttributeField("C", "3"));
                collection.Add(new AttributeField("C", "3"));
            }
            catch (Exception ex)
            {
                Assert.Fail( ex?.ToString() );
            }
        }
    }
}
