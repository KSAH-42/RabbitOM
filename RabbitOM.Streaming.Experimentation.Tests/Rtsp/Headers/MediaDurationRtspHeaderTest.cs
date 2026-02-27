using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class MediaDurationRtspHeaderTest
    {
        [TestCase( "0" , 0 )]
        [TestCase( "1" , 1 )]
        [TestCase( "2" , 2 )]
        [TestCase( "1.7976931348623" , 1.7976931348623 )]
         
        [TestCase( " 0 " , 0 )]
        [TestCase( " 1 " , 1 )]
        [TestCase( " 2 " , 2 )]
        [TestCase( " 1.7976931348623 " , 1.7976931348623 )]

        [TestCase( " '0' " , 0 )]
        [TestCase( " '1' " , 1 )]
        [TestCase( " '2' " , 2 )]
        [TestCase( " '1.7976931348623' " , 1.7976931348623 )]

        [TestCase( " \"0\" " , 0 )]
        [TestCase( " \"1\" " , 1 )]
        [TestCase( " \"2\" " , 2 )]
        [TestCase( " \"1.7976931348623\" " , 1.7976931348623 )]

        [TestCase( " \"-1.7976931348623\"" , -1.7976931348623 )]
        [TestCase( " \"-1.7976931348623" , -1.7976931348623 )]
        [TestCase( " '-1.7976931348623" , -1.7976931348623 )]

        public void CheckTryParseSucceed( string input , double expectedId )
        {
            Assert.IsTrue( MediaDurationRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( expectedId , header.Value );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( "a" ) ]
        [TestCase( "aa" ) ]
        [TestCase( " aa " ) ]
        [TestCase( " a1a " ) ]
        [TestCase( " ? " ) ]
        [TestCase( " '' " ) ]
        [TestCase( "''" ) ]
        [TestCase( "\"\"" ) ]
        [TestCase( "\" \"" ) ]
        [TestCase( " \" \" " ) ]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( MediaDurationRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new MediaDurationRtspHeaderValue();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0.000" , header.ToString() );

            header.Value = 0;
            Assert.AreEqual( "0.000" , header.ToString() );

            header.Value = 1.2;
            Assert.AreEqual( "1.200" , header.ToString() );

            header.Value = -1.2;
            Assert.AreEqual( "-1.200" , header.ToString() );
        }
    }
}
