using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class AcceptLanguageRtspHeaderTest
    {
        [TestCase( "fr" , 1 ) ]
        [TestCase( "fr, fr-FR" , 2 ) ]
        [TestCase( "fr, fr-FR, en-EN" , 3 ) ]

        [TestCase( " fr " , 1 ) ]
        [TestCase( " fr , fr-FR " , 2 ) ]
        [TestCase( " fr , fr-FR , en-EN " , 3 ) ]

        [TestCase( "fr" , 1 ) ]
        [TestCase( "fr,fr-FR" , 2 ) ]
        [TestCase( "fr,fr-FR,en-EN", 3 ) ]

        [TestCase( "\fr" , 1 ) ]
        [TestCase( "fr,\r\nfr-FR" , 2 ) ]
        [TestCase( "fr\v,\ffr-FR,\r\nen-EN" , 3 ) ]

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

            header.AddLanguage( StringRtspHeader.NewString( "fr-FR" ) );
            Assert.AreEqual( "fr-FR" , header.ToString() );

            header.AddLanguage( StringRtspHeader.NewString( "en-EN" ) );
            Assert.AreEqual( "fr-FR, en-EN" , header.ToString() );

            header.AddLanguage( StringRtspHeader.NewString( "us-US" , 1 ) );
            Assert.AreEqual( "fr-FR, en-EN, us-US; q=1.0" , header.ToString() );
        }

        [Test]
        public void CheckAddLanguage()
        {
            var header = new AcceptLanguageRtspHeader();

            Assert.IsEmpty( header.Languages );

            header.AddLanguage( StringRtspHeader.NewString( "fr-FR" ) );
            Assert.AreEqual( 1 , header.Languages.Count );

            header.AddLanguage( StringRtspHeader.NewString( "en-EN" ) );
            Assert.AreEqual( 2 , header.Languages.Count );

            header.RemoveLanguages();
            Assert.IsEmpty( header.Languages );
        }
    }
}
