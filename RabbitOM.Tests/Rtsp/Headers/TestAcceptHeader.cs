using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestAcceptHeader
    {
        [TestMethod]
        [DataRow("application/sdp, application/text;q=1.0, *;q=0.5" , 3 )]
        [DataRow("application/sdp, application/text;q=1.0", 2)]
        [DataRow("application/sdp, \r\napplication/xml\t;q=1.0", 2)]
        [DataRow("application/sdp, " , 1 )]
        [DataRow("application/sdp ", 1 )]
        [DataRow("    application/sdp  , * " , 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Mimes.Count );
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
            Assert.AreEqual( false , AcceptRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("c") ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( "a" , header.ToString() );

            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( "a, b" , header.ToString() );

            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("c" , 1) ) );
            Assert.AreEqual( "a, b, c; q=1.0" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( 0 , header.Mimes.Count );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("b") ) );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("c") ) );
            header.AddMime( new StringWithQualityRtspHeaderValue( "d" ) );
            Assert.AreEqual( 4 , header.Mimes.Count );
            header.RemoveMimes();
            Assert.AreEqual( 0 , header.Mimes.Count );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddMime( null ) );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new AcceptRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddMime( new StringWithQualityRtspHeaderValue("a") ) );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
