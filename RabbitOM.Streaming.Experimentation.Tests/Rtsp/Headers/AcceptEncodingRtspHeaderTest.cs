using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class RtspMethodTest
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

        public void CheckTryParseSucceed( string input , int count )
        {
            if ( ! AcceptEncodingRtspHeaderValue.TryParse( input , out var header ) )
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
        [TestCase( "dzip" )]
        [TestCase( "dzip, text" )]
        public void CheckTryParseFailed( string input )
        {
            if ( AcceptEncodingRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptEncodingRtspHeaderValue();

            Assert.IsEmpty( header.ToString() );

            header.AddEncoding( new StringWithQuality( "br" ) );
            Assert.AreEqual( "br" , header.ToString() );

            header.AddEncoding( new StringWithQuality( "gzip" ) );
            Assert.AreEqual( "br, gzip" , header.ToString() );

            header.AddEncoding( new StringWithQuality( "identity" , 1 ) );
            Assert.AreEqual( "br, gzip, identity; q=1.0" , header.ToString() );
        }

        [Test]
        public void CheckAddEncoding()
        {
            var header = new AcceptEncodingRtspHeaderValue();

            Assert.IsEmpty( header.Encodings );

            Assert.IsTrue( header.AddEncoding( new StringWithQuality( "br" ) ) );
            Assert.AreEqual( 1 , header.Encodings.Count );

            Assert.IsTrue( header.AddEncoding( new StringWithQuality( "gzip" ) ) );
            Assert.AreEqual( 2 , header.Encodings.Count );

            Assert.IsFalse( header.AddEncoding( new StringWithQuality( "gzip" ) ) );
            Assert.IsFalse( header.AddEncoding( new StringWithQuality( "notsupported" ) ) );
            Assert.AreEqual( 2 , header.Encodings.Count );

            header.RemoveEncodings();
            Assert.IsEmpty( header.Encodings );
        }
    }
}
