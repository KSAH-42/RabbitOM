using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestAcceptEncodingHeader
    {
        [TestMethod]
        [DataRow("deflate, gzip;q=1.0, *;q=0.5" , 3 )]
        [DataRow("deflate, gzip;q=1.0", 2)]
        [DataRow("deflate, \r\ngzip\t;q=1.0", 2)]
        [DataRow("deflate, " , 1 )]
        [DataRow("deflate ", 1 )]
        [DataRow("    deflate  , * " , 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptEncodingRtspHeader.TryParse( input , out var result ) )
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
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , AcceptEncodingRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("c") ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( "a" , header.ToString() );

            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( "a, b" , header.ToString() );

            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("c" , 1) ) );
            Assert.AreEqual( "a, b, c; q=1.0" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("c") ) );
            header.AddEncoding( new StringWithQualityRtspHeaderValue( "d" ) );
            Assert.AreEqual( 4 , header.Encodings.Count );
            header.RemoveEncodings();
            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.ThrowsException<ArgumentException>( () => header.AddEncoding( new StringWithQualityRtspHeaderValue(" a ") ) );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
