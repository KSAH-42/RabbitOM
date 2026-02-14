using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class AcceptLanguageHeaderTest
    {
        [Test]
        [TestCase("fr-FR,en-EN,*;q=0.5" , 3 )]
        [TestCase("  fr-FR  ,  en-EN  , * ; q = 0.5" , 3 )]
        [TestCase("fr-FR," , 1 )]
        [TestCase("\r\nfr-FR," , 1 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptLanguageRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Languages.Count );
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
            Assert.IsFalse(  AcceptLanguageRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("en-EN") ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.AreEqual( "fr-FR" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("en-EN") ) );
            Assert.AreEqual( "fr-FR, en-EN" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("jp-JP" , 1) ) );
            Assert.AreEqual( "fr-FR, en-EN, jp-JP; q=1.0" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.AreEqual( 0 , header.Languages.Count );
            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("en-EN") ) );
            header.AddLanguage( new StringWithQualityRtspHeaderValue("jp-JP") );
            Assert.AreEqual( 3 , header.Languages.Count );
            header.RemoveLanguages();
            Assert.AreEqual( 0 , header.Languages.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddLanguage( null ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddLanguage( new StringWithQualityRtspHeaderValue("fr-FR") ) );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
