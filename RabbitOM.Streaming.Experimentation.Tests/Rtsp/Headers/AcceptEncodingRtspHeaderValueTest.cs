using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class AcceptEncodingRtspHeaderValueTest
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
            
            header.AddEncoding( new StringWithQualityRtspHeaderValue( "br" ) );
            Assert.AreEqual( "br" , header.ToString() );

            header.AddEncoding( new StringWithQualityRtspHeaderValue( "gzip" ) );
            Assert.AreEqual( "br, gzip" , header.ToString() );

            header.AddEncoding( new StringWithQualityRtspHeaderValue( "identity" , 1 ) );
            Assert.AreEqual( "br, gzip, identity; q=1.0" , header.ToString() );
        }

        [Test]
        public void CheckAddEncoding()
        {
            var header = new AcceptEncodingRtspHeaderValue();

            Assert.IsEmpty( header.Encodings );

            Assert.IsTrue( header.AddEncoding( new StringWithQualityRtspHeaderValue( "br" ) ) );
            Assert.AreEqual( 1 , header.Encodings.Count );

            Assert.IsTrue( header.AddEncoding( new StringWithQualityRtspHeaderValue( "gzip" ) ) );
            Assert.AreEqual( 2 , header.Encodings.Count );

            Assert.IsFalse( header.AddEncoding( new StringWithQualityRtspHeaderValue( "gzip" ) ) );
            Assert.IsFalse( header.AddEncoding( new StringWithQualityRtspHeaderValue( "gzip" , 1 ) ) );
            Assert.IsFalse( header.AddEncoding( new StringWithQualityRtspHeaderValue( "notsupported" ) ) );
            Assert.AreEqual( 2 , header.Encodings.Count );
        }

        [Test]
        public void CheckRemoveEncoding()
        {
            var header = new AcceptEncodingRtspHeaderValue();
            
            Assert.IsEmpty( header.Encodings );

            Assert.IsTrue( header.AddEncoding( new StringWithQualityRtspHeaderValue( "br" ) ) );
            Assert.IsTrue( header.AddEncoding( new StringWithQualityRtspHeaderValue( "gzip" ) ) );
            Assert.IsTrue( header.AddEncoding( new StringWithQualityRtspHeaderValue( "zip" ) ) );
            Assert.IsTrue( header.AddEncoding( new StringWithQualityRtspHeaderValue( "tar" ) ) );
            Assert.AreEqual( 4 , header.Encodings.Count );

            Assert.IsFalse( header.RemoveEncoding( new StringWithQualityRtspHeaderValue( "br" , 1 ) ) );
            Assert.IsTrue( header.RemoveEncoding( new StringWithQualityRtspHeaderValue( "br" ) ) );
            Assert.AreEqual( 3 , header.Encodings.Count );
            
            Assert.IsTrue( header.RemoveEncodingBy(  x => x.Value == "tar" ) );
            Assert.AreEqual( 2 , header.Encodings.Count );
            
            Assert.IsFalse( header.RemoveEncodingBy(  x => x.Value == "tar" ) );
            Assert.AreEqual( 2 , header.Encodings.Count );

            header.RemoveEncodings();
            Assert.IsEmpty( header.Encodings );
        }
    }
}
