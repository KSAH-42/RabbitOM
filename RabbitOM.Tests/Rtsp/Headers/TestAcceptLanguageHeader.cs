using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Net.RtspV2.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestAcceptLanguageHeader
    {
        [TestMethod]
        [DataRow("fr-FR,en-EN,*;q=0.5" , 3 )]
        [DataRow("  fr-FR  ,  en-EN  , * ; q = 0.5" , 3 )]
        [DataRow("fr-FR," , 1 )]
        [DataRow("\r\nfr-FR," , 1 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptLanguageRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Languages.Count );
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
            Assert.AreEqual( false , AcceptLanguageRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("en-EN") ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.AreEqual( "fr-FR" , header.ToString() );

            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("en-EN") ) );
            Assert.AreEqual( "fr-FR, en-EN" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( 0 , header.Languages.Count );
            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("en-EN") ) );
            Assert.AreEqual( 2 , header.Languages.Count );
            header.RemoveAllLanguages();
            Assert.AreEqual( 0 , header.Languages.Count );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
