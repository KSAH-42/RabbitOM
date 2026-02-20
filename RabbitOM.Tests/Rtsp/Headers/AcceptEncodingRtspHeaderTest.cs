using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class AcceptEncodingRtspHeaderTest
    {
        [TestCase( "*" , 1 ) ]
        [TestCase( "identity" , 1 ) ]

        [TestCase( "gzip" , 1 ) ]
        [TestCase( "gzip, deflate" , 2 ) ]
        [TestCase( "gzip, deflate, identity" , 3 ) ]

        [TestCase( " gzip " , 1 ) ]
        [TestCase( " gzip , deflate " , 2 ) ]
        [TestCase( " gzip , deflate , identity " , 3 ) ]

        [TestCase( "gzip" , 1 ) ]
        [TestCase( "gzip,deflate" , 2 ) ]
        [TestCase( "gzip,deflate,identity" , 3 ) ]

        [TestCase( "\rgzip" , 1 ) ]
        [TestCase( "gzip,\r\ndeflate" , 2 ) ]
        [TestCase( "gzip\v,\fdeflate,\r\nidentity" , 3 ) ]

        [TestCase( "gzip_1" , 1 ) ]
        [TestCase( "gzip-1" , 1 ) ]
        public void CheckTryParseSuccess( string input , int count )
        {
            if ( ! AcceptEncodingRtspHeader.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse has failed" );
            }

            Assert.IsNotNull( header , "the header can not by null" );
            Assert.Greater( header.Encodings.Count , 0 );
            Assert.AreEqual( count , header.Encodings.Count );
        }


        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\r\n" )]
        [TestCase( ",,,," )]
        [TestCase( " , , , , " )]
        public void CheckTryParseFailed( string input )
        {
            if ( AcceptEncodingRtspHeader.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.IsEmpty( header.ToString() );

            header.AddEncoding( StringRtspHeader.NewString( "application/text" ) );
            Assert.AreEqual( "application/text" , header.ToString() );

            header.AddEncoding( StringRtspHeader.NewString( "application/json" ) );
            Assert.AreEqual( "application/text, application/json" , header.ToString() );

            header.AddEncoding( StringRtspHeader.NewString( "application/xml" , 1 ) );
            Assert.AreEqual( "application/text, application/json, application/xml; q=1.0" , header.ToString() );
        }

        [Test]
        public void CheckAddEncoding()
        {
            var header = new AcceptEncodingRtspHeader();

            Assert.IsEmpty( header.Encodings );

            header.AddEncoding( StringRtspHeader.NewString( "application/text" ) );
            Assert.AreEqual( 1 , header.Encodings.Count );

            header.AddEncoding( StringRtspHeader.NewString( "application/json" ) );
            Assert.AreEqual( 2 , header.Encodings.Count );

            header.RemoveEncodings();
            Assert.IsEmpty( header.Encodings );
        }
    }
}
