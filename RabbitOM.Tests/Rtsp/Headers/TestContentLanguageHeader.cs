using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestContentLanguageHeader
    {
        [TestMethod]
        [DataRow("fr", 1 )]
        [DataRow("fr,en", 2 )]
        [DataRow("fr-FR,en-EN", 2 )]
        [DataRow("fr-FR ,en-EN", 2 )]
        [DataRow("fr-FR, en-EN ", 2 )]
        [DataRow(" fr-FR , en-EN ", 2 )]
        [DataRow(" \n \r fr-FR , en-EN ", 2 )]
        [DataRow(" \r \n fr-FR , en-EN ", 2 )]
        [DataRow(" fr ,,,,, EN ", 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ContentLanguageRtspHeader.TryParse( input , out var result ) )
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
            Assert.AreEqual( false , ContentLanguageRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new ContentLanguageRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddLanguage( "fr" ) );
            Assert.AreEqual( true , header.TryAddLanguage( "en" ) );
            Assert.AreEqual( true , header.TryAddLanguage( "us" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddLanguage( " fr " ) );
            Assert.AreEqual( "fr" , header.ToString() );

            Assert.AreEqual( true , header.TryAddLanguage( " en " ) );
            Assert.AreEqual( "fr, en" , header.ToString() );

            Assert.AreEqual( false , header.TryAddLanguage( "en " ) );
            Assert.AreEqual( "fr, en" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.AreEqual( true , header.TryAddLanguage( "fr" ) );
            Assert.AreEqual( true , header.TryAddLanguage( "en" ) );
            Assert.AreEqual( false , header.TryAddLanguage( "fr" ) );
            header.AddEncoding( "us");
            Assert.AreEqual( 3 , header.Encodings.Count );
            header.RemoveEncodings();
            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.AreEqual( true , header.TryAddLanguage( "us" ) );
            Assert.ThrowsException<ArgumentException>( () => header.AddEncoding( " us " ) );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddLanguage( "us") );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
