using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    [TestFixture]
    public class PortRangeTest
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
            Assert.IsTrue( PortRange.TryParse( input , out var header ) );
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
            Assert.IsFalse( PortRange.TryParse( input , out var header ) );
            Assert.AreEqual( 0 , header.Minimum );
            Assert.AreEqual( 0 , header.Maximum );
        }

        [Test]
        public void CheckIsEqual()
        {
            Assert.AreEqual( PortRange.Zero , new PortRange( 0 , 0 ) );
            Assert.AreEqual( new PortRange( 1 , 2 ) , new PortRange( 1 , 2 ) );
        }

        [Test]
        public void CheckIsNotEqual()
        {
            Assert.AreNotEqual( PortRange.Zero , new PortRange( 0 , 1 ) );
            Assert.AreNotEqual( new PortRange( 1 , 20 ) , new PortRange( 1 , 2 ) );
        }

        [Test]
        public void CheckException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(  () => new PortRange( 1 , 0 ) );
            Assert.Throws<ArgumentOutOfRangeException>(  () => new PortRange( -1 , 0 ) );
        }
    }
}
