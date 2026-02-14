using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class ContentLanguageHeaderTest
    {
        [Test]
        [TestCase("fr", 1 )]
        [TestCase("fr,en", 2 )]
        [TestCase("fr-FR,en-EN", 2 )]
        [TestCase("fr-FR ,en-EN", 2 )]
        [TestCase("fr-FR, en-EN ", 2 )]
        [TestCase(" fr-FR , en-EN ", 2 )]
        [TestCase(" \n \r fr-FR , en-EN ", 2 )]
        [TestCase(" \r \n fr-FR , en-EN ", 2 )]
        [TestCase(" fr ,,,,, EN ", 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ContentLanguageRtspHeader.TryParse( input , out var result ) )
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
            Assert.IsFalse(  ContentLanguageRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new ContentLanguageRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddLanguage( "fr" ) );
            Assert.IsTrue(  header.TryAddLanguage( "en" ) );
            Assert.IsTrue(  header.TryAddLanguage( "us" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( " fr " ) );
            Assert.AreEqual( "fr" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( " en " ) );
            Assert.AreEqual( "fr, en" , header.ToString() );

            Assert.IsFalse(  header.TryAddLanguage( "en " ) );
            Assert.AreEqual( "fr, en" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.IsTrue(  header.TryAddLanguage( "fr" ) );
            Assert.IsTrue(  header.TryAddLanguage( "en" ) );
            Assert.IsFalse(  header.TryAddLanguage( "fr" ) );
            header.AddEncoding( "us");
            Assert.AreEqual( 3 , header.Encodings.Count );
            header.RemoveEncodings();
            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.IsTrue(  header.TryAddLanguage( "us" ) );
            Assert.Throws<ArgumentException>( () => header.AddEncoding( " us " ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddLanguage( "us") );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
