using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class AcceptRtspHeaderValueTest
    {
        [TestCase( "application/text" , 1 ) ]
        [TestCase( "application/text, application/json" , 2 ) ]

        [TestCase( "application/text;q=1" , 1 ) ]
        [TestCase( "application/text;q=1, application/json;q=2" , 2 ) ]

        public void CheckTryParseSucceed( string input , int count )
        {
            if ( ! AcceptRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse has failed" );
            }

            Assert.IsNotNull( header , "the header can not by null" );
            Assert.Greater( header.Mimes.Count , 0 );
            Assert.AreEqual( count , header.Mimes.Count );
        }


        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\r\n" )]
        [TestCase( ",,,," )]
        [TestCase( " , , , , " )]
        [TestCase( " ;, ;, ;=,;q=1, " )]
        [TestCase( "appl ication/text" )]
        [TestCase( " appl ication/text " )]
        public void CheckTryParseFailed( string input )
        {
            if ( AcceptRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptRtspHeaderValue();

            Assert.IsEmpty( header.ToString() );

            header.AddMime( new StringWithQualityRtspHeaderValue( "application/text" ) );
            Assert.AreEqual( "application/text" , header.ToString() );
            
            header.RemoveMimes();

            header.AddMime( new StringWithQualityRtspHeaderValue( "application/text" , 1.2 ) );
            Assert.AreEqual( "application/text; q=1.2" , header.ToString() );

            header.AddMime( new StringWithQualityRtspHeaderValue( "application/xml" ) );
            Assert.AreEqual( "application/text; q=1.2, application/xml" , header.ToString() );
        }

        [Test]
        public void CheckAddMime()
        {
            var header = new AcceptRtspHeaderValue();

            Assert.IsEmpty( header.Mimes );

            Assert.IsTrue( header.AddMime( "application/text" ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            Assert.IsFalse( header.AddMime( "application/text" ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            Assert.IsTrue( header.AddMime( "application/json" ) );
            Assert.AreEqual( 2 , header.Mimes.Count );
        }

        [Test]
        public void CheckRemoveMime()
        {
            var header = new AcceptRtspHeaderValue();

            Assert.IsEmpty( header.Mimes );

            Assert.IsTrue( header.AddMime( "text/xml" ) );
            Assert.AreEqual( 1 , header.Mimes.Count );
            
            Assert.IsTrue( header.AddMime( "text/json" ) );
            Assert.AreEqual( 2 , header.Mimes.Count );

            Assert.IsTrue( header.AddMime( "text/sdp" ) );
            Assert.AreEqual( 3 , header.Mimes.Count );

            Assert.IsFalse( header.RemoveMime( new StringWithQualityRtspHeaderValue( "text/sdp" , 1 ) ) );
            Assert.IsTrue( header.RemoveMime( "text/sdp" ) );
            Assert.IsFalse( header.RemoveMime( "text/sdp" ) );
            Assert.AreEqual( 2 , header.Mimes.Count );

            Assert.IsTrue( header.RemoveMimeBy( x => x.Value == "text/json" ) );
            Assert.IsFalse( header.RemoveMimeBy( x => x.Value == "text/json" ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            header.RemoveMimes();
            Assert.IsEmpty( header.Mimes );
        }
    }
}
