using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Types;

    [TestFixture]
    public class ValueRangeTest
    {
        [TestCase( "0-0" , 0 , 0 )]
        [TestCase( "0-1234" , 0 , 1234 )]
        [TestCase( "10-10" , 10 , 10 )]
        [TestCase( "1234-1234" , 1234 , 1234 )]
        [TestCase( " 0-1234 " , 0 , 1234 )]
        [TestCase( "0-1234" , 0 , 1234 )]
        [TestCase( " 0 - 1234 " , 0 , 1234 )]
        [TestCase( "0-'1234'" , 0 , 1234 )]
        [TestCase( "'0'-'1234'" , 0 , 1234 )]
        public void CheckTryParseSucceed( string input , int min , int max )
        {
            Assert.IsTrue( ValueRange.TryParse( input , out var header ) );
            Assert.AreEqual( min , header.Minimum );
            Assert.AreEqual( max , header.Maximum );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "-" )]
        [TestCase( "-1234-0" )]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( ValueRange.TryParse( input , out var header ) );
            Assert.AreEqual( 0 , header.Minimum );
            Assert.AreEqual( 0 , header.Maximum );
        }

        [Test]
        public void CheckIsEqual()
        {
            Assert.AreEqual( ValueRange.Zero , new ValueRange( 0 , 0 ) );
            Assert.AreEqual( new ValueRange( 1 , 2 ) , new ValueRange( 1 , 2 ) );
        }

        [Test]
        public void CheckIsNotEqual()
        {
            Assert.AreNotEqual( ValueRange.Zero , new ValueRange( 0 , 1 ) );
            Assert.AreNotEqual( new ValueRange( 1 , 20 ) , new ValueRange( 1 , 2 ) );
        }

        [Test]
        public void CheckException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(  () => new ValueRange( 1 , 0 ) );
            Assert.Throws<ArgumentOutOfRangeException>(  () => new ValueRange( -1 , 0 ) );
        }
    }
}
