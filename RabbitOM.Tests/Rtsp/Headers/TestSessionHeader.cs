using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;
using System.Runtime.Serialization;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestSessionHeader
    {
        [Test]
        [TestCase("mysessionId" )]
        [TestCase("mysessionId;;;" )]
        [TestCase("  mysessionId  " )]
        [TestCase("  mysessionId  ;;;" )]
        [TestCase("\r\nmysessionId" )]
        [TestCase("\r\n'mysessionId'" )]
        [TestCase("\r\n\"mysessionId\"" )]
        [TestCase("\r\n\" mysessionId \"" )]
        public void ParseTestSucceed(string input)
        {
            if ( ! SessionRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( "mysessionId" , result.Identifier );
        }

        [Test]
        [TestCase("mysessionId;timeout=123" )]
        [TestCase("mysessionId;timeout=123;" )]
        [TestCase("mysessionId ; timeout = 123 ;" )]
        [TestCase("mysessionId ; timeout = '123' ;" )]
        [TestCase("mysessionId ; timeout = \"123\" ;" )]
        [TestCase(" mysessionId ; timeout = \"123\" ;" )]
        [TestCase(" mysessionId ; parameter=321; timeout = \"123\" ;" )]
        public void ParseTestSucceedWithTimeout(string input)
        {
            if ( ! SessionRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( "mysessionId" , result.Identifier );
            Assert.AreEqual( 123 , result.Timeout );
        }

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  SessionRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat()
        {
            var header = new SessionRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.Identifier = "mysession";
            Assert.AreEqual( "mysession" , header.ToString() );

            header.Timeout = 12;
            Assert.AreEqual( "mysession;timeout=12" , header.ToString() );
        }

        [Test]
        public void TestValidation()
        {
            var header = new SessionRtspHeader();
            Assert.IsFalse(  header.TryValidate() );

            header.Identifier = ",,,";
            Assert.IsFalse(  header.TryValidate() );

            header.Identifier = "mysession1";
            Assert.IsTrue(  header.TryValidate() );

            header.Timeout = -1;
            Assert.IsFalse(  header.TryValidate() );

            header.Timeout = 0;
            Assert.IsFalse(  header.TryValidate() );

            header.Timeout = 1;
            Assert.IsTrue(  header.TryValidate() );

            header.Timeout = 2;
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
