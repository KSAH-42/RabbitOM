using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class AcceptRtspHeaderTest
    {
        [TestCase( "applicaton/text" , 1 ) ]
        [TestCase( "applicaton/text, applicaton/json" , 2 ) ]

        [TestCase( "applicaton/text;q=1" , 1 ) ]
        [TestCase( "applicaton/text;q=1, applicaton/json;q=2" , 2 ) ]


        public void CheckTryParseSuccess( string input , int count )
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

            header.AddMime( StringRtspHeader.NewString( "application/text" ) );
            Assert.AreEqual( "application/text" , header.ToString() );

            header.AddMime( StringRtspHeader.NewString( "application/text" , 1.2 ) );
            Assert.AreEqual( "application/text, application/text; q=1.2" , header.ToString() );
        }

        [Test]
        public void CheckAddMime()
        {
            var header = new AcceptRtspHeader();

            Assert.IsEmpty( header.Mimes );

            header.AddMime( StringRtspHeader.NewString( "application/text" ) );
            Assert.AreEqual( 1 , header.Mimes.Count );

            header.AddMime( StringRtspHeader.NewString( "application/text" ) );
            Assert.AreEqual( 2 , header.Mimes.Count );

            header.RemoveMimes();
            Assert.IsEmpty( header.Mimes );
        }
    }
}
