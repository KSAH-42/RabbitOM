using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    [TestFixture]
    public class AcceptLanguageRtspHeaderTest
    {
        [TestCase( "fr" , 1 ) ]
        [TestCase( "fr, fr-FR" , 2 ) ]
        [TestCase( "fr, fr-FR, en-GB" , 3 ) ]

        [TestCase( " fr " , 1 ) ]
        [TestCase( " fr , fr-FR " , 2 ) ]
        [TestCase( " fr , fr-FR , en-GB" , 3 ) ]

        [TestCase( "fr" , 1 ) ]
        [TestCase( "fr,fr-FR" , 2 ) ]
        [TestCase( "fr,fr-FR,en-GB", 3 ) ]

        [TestCase( "\ffr" , 1 ) ]
        [TestCase( "fr,\r\nfr-FR" , 2 ) ]
        [TestCase( "fr\v,\ffr-FR,\r\nen-GB" , 3 ) ]

        [TestCase( "fr-FR, en-GB, en-US" , 3 ) ]
        public void CheckTryParseSuccess( string input , int count )
        {
            if ( ! AcceptLanguageRtspHeader.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse has failed" );
            }

            Assert.IsNotNull( header , "the header can not by null" );
            Assert.Greater( header.Languages.Count , 0 );
            Assert.AreEqual( count , header.Languages.Count );
        }


        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\r\n" )]
        [TestCase( ",,,," )]
        [TestCase( " , , , , " )]
        [TestCase( "fr-EN" ) ]
        [TestCase( "\fr" ) ]
        public void CheckTryParseFailed( string input )
        {
            if ( AcceptLanguageRtspHeader.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.IsEmpty( header.ToString() );

            header.AddLanguage( new StringWithQuality( "fr-FR" ) );
            Assert.AreEqual( "fr-FR" , header.ToString() );

            header.AddLanguage( new StringWithQuality( "en-GB" ) );
            Assert.AreEqual( "fr-FR, en-GB" , header.ToString() );

            header.AddLanguage( new StringWithQuality( "en-US" , 1 ) );
            Assert.AreEqual( "fr-FR, en-GB, en-US; q=1.0" , header.ToString() );
        }

        [Test]
        public void CheckAddLanguage()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.IsEmpty( header.Languages );

            Assert.IsTrue( header.AddLanguage( new StringWithQuality( "fr-FR" ) ) );
            Assert.AreEqual( 1 , header.Languages.Count );

            Assert.IsTrue( header.AddLanguage( new StringWithQuality( "en-GB" ) ) );
            Assert.AreEqual( 2 , header.Languages.Count );

            Assert.IsFalse( header.AddLanguage( new StringWithQuality( "en-EN" ) ) );
            Assert.AreEqual( 2 , header.Languages.Count );

            header.RemoveLanguages();
            Assert.IsEmpty( header.Languages );
        }
    }
}
