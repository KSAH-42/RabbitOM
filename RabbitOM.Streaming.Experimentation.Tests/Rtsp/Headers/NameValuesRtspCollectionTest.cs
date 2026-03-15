using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class NameValuesRtspCollectionTest
    {
        [Test]
        public void CheckAddElement()
        {
            var colletion = new NameValuesRtspCollection();

            Assert.AreEqual( 0 , colletion.Count );

            Assert.IsTrue( colletion.TryAdd( "a" , "b" ) );
            Assert.IsTrue( colletion.TryAdd( "b" , "" ) );

            Assert.AreEqual( 2 , colletion.Count );

            Assert.IsFalse( colletion.TryAdd( null , "aa" ) );
            Assert.IsFalse( colletion.TryAdd( " " , "aa" ) );
            Assert.IsFalse( colletion.TryAdd( "  " , "aa" ) );
            Assert.IsFalse( colletion.TryAdd( "b" , "" ) );
            Assert.AreEqual( 2 , colletion.Count );

            Assert.IsTrue( colletion.TryAdd( "b" , "b" ) );
            Assert.AreEqual( 3 , colletion.Count );

            Assert.IsFalse( colletion.TryAdd( "b" , "b" ) );
            Assert.AreEqual( 3 , colletion.Count );
        }

        [Test]
        public void CheckAddElementWithExceptions()
        {
            var colletion = new NameValuesRtspCollection();

            Assert.AreEqual( 0 , colletion.Count );

            Assert.DoesNotThrow( () => colletion.Add( "a" , "b" ) );
            Assert.DoesNotThrow( () => colletion.Add( "b" , "" ) );

            Assert.AreEqual( 2 , colletion.Count );

            Assert.Throws<ArgumentException>( () => colletion.Add( null , "aa" ) );
            Assert.Throws<ArgumentException>( () => colletion.Add( " " , "aa" ) );
            Assert.Throws<ArgumentException>( () => colletion.Add( "  " , "aa" ) );
            Assert.Throws<ArgumentException>( () => colletion.Add( "b" , "" ) );
            Assert.AreEqual( 2 , colletion.Count );

            Assert.DoesNotThrow( () => colletion.Add( "b" , "b" ) );
            Assert.AreEqual( 3 , colletion.Count );

            Assert.Throws<ArgumentException>( () => colletion.Add( "b" , "b" ) );
            Assert.AreEqual( 3 , colletion.Count );
        }

        [Test]
        public void CheckTryGetValue()
        {
            var colletion = new NameValuesRtspCollection();

            Assert.IsTrue( colletion.TryAdd( "a" , "b" ) );
            
            Assert.IsTrue( colletion.TryGetValue( "a" , out var result ) );
            Assert.AreEqual( "b" , result );

            Assert.IsTrue( colletion.TryAdd( "a" , "bc" ) );

            Assert.IsTrue( colletion.TryGetValue( "a" , out result ) );
            Assert.AreEqual( "b" , result );

            Assert.IsTrue( colletion.TryGetValueAt( "a" , 0 , out result ) );
            Assert.AreEqual( "b" , result );

            Assert.IsTrue( colletion.TryGetValueAt( "a" , 1 , out result ) );
            Assert.AreEqual( "bc" , result );

            Assert.IsFalse( colletion.TryGetValueAt( "a" , 2 , out result ) );
            Assert.IsFalse( colletion.TryGetValueAt( "a" , -1 , out result ) );
        }

        [Test]
        public void CheckForEach()
        {
            var colletion = new NameValuesRtspCollection();

            Assert.IsTrue( colletion.TryAdd( "a" , "a" ) );
            Assert.IsTrue( colletion.TryAdd( "a" , "b" ) );
            Assert.IsTrue( colletion.TryAdd( "a" , "c" ) );

            Assert.IsTrue( colletion.TryAdd( "b" , "aa" ) );
            Assert.IsTrue( colletion.TryAdd( "b" , "ab" ) );
            Assert.IsTrue( colletion.TryAdd( "b" , "ac" ) );
            
            Assert.AreEqual( 6 , colletion.Count );

            foreach ( KeyValuePair<string,IEnumerable<string>> element in colletion )
            {
                if ( element.Key == "a" )
                {
                    Assert.AreEqual( 3 , element.Value.Count() );
                    Assert.AreEqual( "a" , element.Value.ElementAtOrDefault(0) );
                    Assert.AreEqual( "b" , element.Value.ElementAtOrDefault(1) );
                    Assert.AreEqual( "c" , element.Value.ElementAtOrDefault(2) );
                    continue;
                }
                
                if ( element.Key == "b" )
                {
                    Assert.AreEqual( 3 , element.Value.Count() );
                    Assert.AreEqual( "aa" , element.Value.ElementAtOrDefault(0) );
                    Assert.AreEqual( "ab" , element.Value.ElementAtOrDefault(1) );
                    Assert.AreEqual( "ac" , element.Value.ElementAtOrDefault(2) );
                    continue;
                }
                
                Assert.Fail();
            }
        }

        [Test]
        public void CheckRemove()
        {
            var colletion = new NameValuesRtspCollection();

            Assert.IsTrue( colletion.TryAdd( "a" , "a" ) );
            Assert.IsTrue( colletion.TryAdd( "a" , "b" ) );
            Assert.IsTrue( colletion.TryAdd( "a" , "c" ) );

            Assert.IsTrue( colletion.TryAdd( "b" , "aa" ) );
            Assert.IsTrue( colletion.TryAdd( "b" , "ab" ) );
            Assert.IsTrue( colletion.TryAdd( "b" , "ac" ) );
            
            Assert.AreEqual( 6 , colletion.Count );
            Assert.AreEqual( 2 , colletion.Keys.Count );

            Assert.IsTrue( colletion.RemoveAt( "b" , 1 ) );
            Assert.IsFalse( colletion.RemoveAt( "b" , 10 ) );
            Assert.IsFalse( colletion.RemoveAt( "b" , -1 ) );

            Assert.AreEqual( 5 , colletion.Count );

            Assert.IsTrue( colletion.Remove( "b" ) );
            Assert.IsFalse( colletion.Remove( "b" ) );
            
            Assert.AreEqual( 3 , colletion.Count );
            Assert.AreEqual( 1 , colletion.Keys.Count );

            colletion.Clear();
            Assert.AreEqual( 0 , colletion.Count );
            Assert.AreEqual( 0 , colletion.Keys.Count );
        }
    }
}
