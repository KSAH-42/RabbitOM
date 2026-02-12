using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;
using System.Runtime.Serialization;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestSessionHeader
    {
        [TestMethod]
        [DataRow("mysessionId" )]
        [DataRow("mysessionId;;;" )]
        [DataRow("  mysessionId  " )]
        [DataRow("  mysessionId  ;;;" )]
        [DataRow("\r\nmysessionId" )]
        [DataRow("\r\n'mysessionId'" )]
        [DataRow("\r\n\"mysessionId\"" )]
        [DataRow("\r\n\" mysessionId \"" )]
        public void ParseTestSucceed(string input)
        {
            if ( ! SessionRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( "mysessionId" , result.Identifier );
        }

        [TestMethod]
        [DataRow("mysessionId;timeout=123" )]
        [DataRow("mysessionId;timeout=123;" )]
        [DataRow("mysessionId ; timeout = 123 ;" )]
        [DataRow("mysessionId ; timeout = '123' ;" )]
        [DataRow("mysessionId ; timeout = \"123\" ;" )]
        [DataRow(" mysessionId ; timeout = \"123\" ;" )]
        [DataRow(" mysessionId ; parameter=321; timeout = \"123\" ;" )]
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

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , SessionRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat()
        {
            var header = new SessionRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.Identifier = "mysession";
            Assert.AreEqual( "mysession" , header.ToString() );

            header.Timeout = 12;
            Assert.AreEqual( "mysession;timeout=12" , header.ToString() );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new SessionRtspHeader();
            Assert.AreEqual( false , header.TryValidate() );

            header.Identifier = ",,,";
            Assert.AreEqual( false , header.TryValidate() );

            header.Identifier = "mysession1";
            Assert.AreEqual( true , header.TryValidate() );

            header.Timeout = -1;
            Assert.AreEqual( false , header.TryValidate() );

            header.Timeout = 0;
            Assert.AreEqual( false , header.TryValidate() );

            header.Timeout = 1;
            Assert.AreEqual( true , header.TryValidate() );

            header.Timeout = 2;
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
