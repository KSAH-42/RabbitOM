using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers.Types
{
    [TestFixture]
    public class InterleavedRangeTest
    {
        [TestCase( "0-0" , 0 , 0 )]
        [TestCase( "0-255" , 0 , 255 )]
        [TestCase( "10-10" , 10 , 10 )]
        [TestCase( "255-255" , 255 , 255 )]
        [TestCase( " 0-255 " , 0 , 255 )]
        [TestCase( "0-255" , 0 , 255 )]
        [TestCase( " 0 - 255 " , 0 , 255 )]
        [TestCase( "0-'255'" , 0 , 255 )]
        [TestCase( "'0'-'255'" , 0 , 255 )]
        public void CheckParseSucceed( string input , byte min , byte max )
        {
            Assert.IsTrue( InterleavedRange.TryParse( input , out var header ) );
            Assert.AreEqual( min , header.Minimum );
            Assert.AreEqual( max , header.Maximum );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "-" )]
        [TestCase( "255-0" )]
        public void CheckParseFailed( string input )
        {
            Assert.IsFalse( InterleavedRange.TryParse( input , out var header ) );
            Assert.AreEqual( 0 , header.Minimum );
            Assert.AreEqual( 0 , header.Maximum );
        }

        [Test]
        public void CheckIsEqual()
        {
            Assert.AreEqual( InterleavedRange.Zero , new InterleavedRange( 0 , 0 ) );
            Assert.AreEqual( new InterleavedRange( 1 , 2 ) , new InterleavedRange( 1 , 2 ) );
        }

        [Test]
        public void CheckIsNotEqual()
        {
            Assert.AreNotEqual( InterleavedRange.Zero , new InterleavedRange( 0 , 1 ) );
            Assert.AreNotEqual( new InterleavedRange( 1 , 20 ) , new InterleavedRange( 1 , 2 ) );
        }

        [Test]
        public void CheckException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(  () => new InterleavedRange( 1 , 0 ) );
        }
    }
}
