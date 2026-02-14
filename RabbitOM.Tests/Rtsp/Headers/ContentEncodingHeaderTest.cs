using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class ContentEncodingHeaderTest
    {
        [Test]
        [TestCase("zip", 1 )]
        [TestCase("zip,gzip", 2 )]
        [TestCase("zip, gzip, tar", 3 )]
        [TestCase(",zip, gzip, tar", 3 )]
        [TestCase(",\r \n zip, gzip, tar", 3 )]
        [TestCase(" zip ", 1 )]
        [TestCase(" zip ,,,,, tar", 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ContentEncodingRtspHeader.TryParse( input , out var result ) )
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
        [TestCase( " ?  ,  " )]
        [TestCase( "*" )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  ContentEncodingRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddLanguage( "zip" ) );
            Assert.IsTrue(  header.TryAddLanguage( "gzip" ) );
            Assert.IsTrue(  header.TryAddLanguage( "tar" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( " gzip " ) );
            Assert.AreEqual( "gzip" , header.ToString() );

            Assert.IsTrue(  header.TryAddLanguage( " tar " ) );
            Assert.AreEqual( "gzip, tar" , header.ToString() );

            Assert.IsFalse(  header.TryAddLanguage( "tar " ) );
            Assert.AreEqual( "gzip, tar" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.IsTrue(  header.TryAddLanguage( "zip" ) );
            Assert.IsTrue(  header.TryAddLanguage( "gzip" ) );
            Assert.IsFalse(  header.TryAddLanguage( "gzip" ) );
            header.AddEncoding( "tar");
            Assert.AreEqual( 3 , header.Encodings.Count );
            header.RemoveEncodings();
            Assert.AreEqual( 0 , header.Encodings.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddEncoding( null ) );
            Assert.IsTrue(  header.TryAddLanguage( "zip" ) );
            Assert.Throws<ArgumentException>( () => header.AddEncoding( " zip " ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new ContentEncodingRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddLanguage( "zip") );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
