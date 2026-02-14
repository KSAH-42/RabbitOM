using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestAcceptHeader
    {
        [Test]
        [TestCase("application/sdp, application/text;q=1.0, *;q=0.5" , 3 )]
        [TestCase("application/sdp, application/text;q=1.0", 2)]
        [TestCase("application/sdp, \r\napplication/xml\t;q=1.0", 2)]
        [TestCase("application/sdp, " , 1 )]
        [TestCase("application/sdp ", 1 )]
        [TestCase("    application/sdp  , * " , 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Mimes.Count );
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
            Assert.IsFalse(  AcceptRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("c") ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( "a" , header.ToString() );

            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( "a, b" , header.ToString() );

            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("c" , 1) ) );
            Assert.AreEqual( "a, b, c; q=1.0" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( 0 , header.Mimes.Count );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("c") ) );
            header.AddMime( new StringWithQualityRtspHeaderValue( "d" ) );
            Assert.AreEqual( 4 , header.Mimes.Count );
            header.RemoveMimes();
            Assert.AreEqual( 0 , header.Mimes.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddMime( null ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new AcceptRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
