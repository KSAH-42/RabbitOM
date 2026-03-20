using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class AcceptLanguageRtspHeaderValueTest
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

        [TestCase( "fr-FR, en-GB, en-US" , 3 ) ]
        public void CheckTryParseSucceed( string input , int count )
        {
            if ( ! AcceptLanguageRtspHeaderValue.TryParse( input , out var header ) )
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
        [TestCase( "en-EN" ) ]
        [TestCase( "us-US" ) ]
        public void CheckTryParseFailed( string input )
        {
            if ( AcceptLanguageRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptLanguageRtspHeaderValue();

            Assert.IsEmpty( header.ToString() );

            header.AddLanguage( new StringWithQualityRtspHeaderValue( "fr-FR" ) );
            Assert.AreEqual( "fr-FR" , header.ToString() );

            header.AddLanguage( new StringWithQualityRtspHeaderValue( "en-GB" ) );
            Assert.AreEqual( "fr-FR, en-GB" , header.ToString() );

            header.AddLanguage( new StringWithQualityRtspHeaderValue( "en-US" , 1 ) );
            Assert.AreEqual( "fr-FR, en-GB, en-US; q=1.0" , header.ToString() );
        }

        [Test]
        public void CheckAddLanguage()
        {
            var header = new AcceptLanguageRtspHeaderValue();

            Assert.IsEmpty( header.Languages );

            Assert.IsTrue( header.AddLanguage( "fr-FR" ) );
            Assert.AreEqual( 1 , header.Languages.Count );

            Assert.IsTrue( header.AddLanguage( "en-GB" ) );
            Assert.AreEqual( 2 , header.Languages.Count );

            Assert.IsFalse( header.AddLanguage( "en-EN" ) );
            Assert.AreEqual( 2 , header.Languages.Count );
        }

        [Test]
        public void CheckRemoveLanguage()
        {
            var header = new AcceptLanguageRtspHeaderValue();

            Assert.IsEmpty( header.Languages );

            Assert.IsTrue( header.AddLanguage( "fr-FR" ) );
            Assert.AreEqual( 1 , header.Languages.Count );

            Assert.IsTrue( header.AddLanguage( "en-GB" ) );
            Assert.AreEqual( 2 , header.Languages.Count );

            Assert.IsTrue( header.AddLanguage( "en-US" ) );
            Assert.AreEqual( 3 , header.Languages.Count );

            Assert.IsFalse( header.RemoveLanguage( new StringWithQualityRtspHeaderValue( "fr-FR" , 1 ) ) );
            Assert.IsTrue( header.RemoveLanguage( "fr-FR" ) );
            Assert.IsFalse( header.RemoveLanguage("fr-FR" ) );
            Assert.AreEqual( 2 , header.Languages.Count );

            Assert.IsTrue( header.RemoveLanguageBy( x => x.Value == "en-GB" ) );
            Assert.IsFalse( header.RemoveLanguageBy( x => x.Value == "en-GB" ) );
            Assert.AreEqual( 1 , header.Languages.Count );

            header.RemoveLanguages();
            Assert.IsEmpty( header.Languages );
        }
    }
}
