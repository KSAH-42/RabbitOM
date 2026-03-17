using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved;

    [TestFixture]
    public class AcceptRtspHeaderTest
    {
        [TestCase( "application/text" , 1 ) ]
        [TestCase( "application/text, application/json" , 2 ) ]

        [TestCase( "application/text;q=1" , 1 ) ]
        [TestCase( "application/text;q=1, application/json;q=2" , 2 ) ]

        public void CheckTryParseSucceed( string input , int count )
        {
            if ( ! AcceptRtspHeader.TryParse( input , out var header ) )
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
        [TestCase( "application/strange" )]
        [TestCase( "application/strange; q=z" )]
        public void CheckTryParseFailed( string input )
        {
            if ( AcceptRtspHeader.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptRtspHeader();

            Assert.IsEmpty( header.ToString() );

            header.AddMime( new WeightedString( "application/text" ) );
            Assert.AreEqual( "application/text" , header.ToString() );
            
            header.ClearMimes();

            header.AddMime( new WeightedString( "application/text" , 1.2 ) );
            Assert.AreEqual( "application/text; q=1.2" , header.ToString() );

            header.AddMime( new WeightedString( "application/xml" ) );
            Assert.AreEqual( "application/text; q=1.2, application/xml" , header.ToString() );
        }

        [Test]
        public void CheckAddMime()
        {
            var header = new AcceptRtspHeader();

            Assert.IsEmpty( header.Mimes );

            Assert.IsTrue( header.AddMime( new WeightedString( "application/text" ) ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            Assert.IsFalse( header.AddMime( new WeightedString( "application/text" ) ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            Assert.IsFalse( header.AddMime( new WeightedString( "application/bizar" ) ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            Assert.IsTrue( header.AddMime( new WeightedString( "application/json" ) ) );
            Assert.AreEqual( 2 , header.Mimes.Count );
        }

        [Test]
        public void CheckRemoveMime()
        {
            var header = new AcceptRtspHeader();

            Assert.IsEmpty( header.Mimes );

            Assert.IsTrue( header.AddMime( new WeightedString( "text/xml" ) ) );
            Assert.AreEqual( 1 , header.Mimes.Count );
            
            Assert.IsTrue( header.AddMime( new WeightedString( "text/json" ) ) );
            Assert.AreEqual( 2 , header.Mimes.Count );

            Assert.IsTrue( header.AddMime( new WeightedString( "text/sdp" ) ) );
            Assert.AreEqual( 3 , header.Mimes.Count );

            Assert.IsFalse( header.RemoveMime( new WeightedString( "text/sdp" , 1 ) ) );
            Assert.IsTrue( header.RemoveMime( new WeightedString( "text/sdp" ) ) );
            Assert.IsFalse( header.RemoveMime( new WeightedString( "text/sdp" ) ) );
            Assert.AreEqual( 2 , header.Mimes.Count );

            Assert.IsTrue( header.RemoveMimeBy( x => x.Value == "text/json" ) );
            Assert.IsFalse( header.RemoveMimeBy( x => x.Value == "text/json" ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            header.ClearMimes();
            Assert.IsEmpty( header.Mimes );
        }
    }
}
