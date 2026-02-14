using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestAcceptEncodingHeader
    {
        [Test]
        [TestCase("deflate, gzip;q=1.0, *;q=0.5" , 3 )]
        [TestCase("deflate, gzip;q=1.0", 2)]
        [TestCase("deflate, \r\ngzip\t;q=1.0", 2)]
        [TestCase("deflate, " , 1 )]
        [TestCase("deflate ", 1 )]
        [TestCase("    deflate  , * " , 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptEncodingRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Encodings.Count );
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
            Assert.IsFalse(  AcceptEncodingRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("c") ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( "a" , header.ToString() );

            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( "a, b" , header.ToString() );

            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("c" , 1) ) );
            Assert.AreEqual( "a, b, c; q=1.0" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("c") ) );
            header.AddEncoding( new StringWithQualityRtspHeaderValue( "d" ) );
            Assert.AreEqual( 4 , header.Encodings.Count );
            header.RemoveEncodings();
            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.Throws<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.Throws<ArgumentException>( () => header.AddEncoding( new StringWithQualityRtspHeaderValue(" a ") ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new AcceptEncodingRtspHeader();
            
            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddEncoding( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
