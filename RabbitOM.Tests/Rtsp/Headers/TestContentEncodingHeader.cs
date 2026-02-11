using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestContentEncodingHeader
    {
        [TestMethod]
        [DataRow("zip", 1 )]
        [DataRow("zip,gzip", 2 )]
        [DataRow("zip, gzip, tar", 3 )]
        [DataRow(",zip, gzip, tar", 3 )]
        [DataRow(",\r \n zip, gzip, tar", 3 )]
        [DataRow(" zip ", 1 )]
        [DataRow(" zip ,,,,, tar", 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ContentEncodingRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Encodings.Count );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( " , , , , , , , " )]
        [DataRow( " ?  ,  " )]
        [DataRow( "*" )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , ContentEncodingRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddEncoding( "zip" ) );
            Assert.AreEqual( true , header.TryAddEncoding( "gzip" ) );
            Assert.AreEqual( true , header.TryAddEncoding( "tar" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddEncoding( " gzip " ) );
            Assert.AreEqual( "gzip" , header.ToString() );

            Assert.AreEqual( true , header.TryAddEncoding( " tar " ) );
            Assert.AreEqual( "gzip, tar" , header.ToString() );

            Assert.AreEqual( false , header.TryAddEncoding( "tar " ) );
            Assert.AreEqual( "gzip, tar" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.AreEqual( true , header.TryAddEncoding( "zip" ) );
            Assert.AreEqual( true , header.TryAddEncoding( "gzip" ) );
            Assert.AreEqual( false , header.TryAddEncoding( "gzip" ) );
            header.AddEncoding( "tar");
            Assert.AreEqual( 3 , header.Encodings.Count );
            header.RemoveEncodings();
            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.AreEqual( true , header.TryAddEncoding( "zip" ) );
            Assert.ThrowsException<ArgumentException>( () => header.AddEncoding( " zip " ) );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddEncoding( "zip") );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
