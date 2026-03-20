using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    [TestFixture]
    public class RtpInfoRtspHeaderValueTest
    {
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef" , 1 )]
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef," , 1 )]
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef, url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef " , 2 )]
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef, url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef , url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef " , 3 )]
        public void CheckTryParseSucceed(string input , int count )
        {
            Assert.IsTrue( RtpInfoRtspHeaderValue.TryParse( input , out var header ) );
            Assert.AreEqual( count , header.RtpInfos.Count );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( "," ) ]
        [TestCase( ",,,,," ) ]
        [TestCase( "rul=rtsp://127.0.0.1;seq=a" ) ]
        public void CheckTryParseFailed(string input )
        {
            Assert.IsFalse( RtpInfoRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new RtpInfoRtspHeaderValue();

            Assert.AreEqual( "" , header.ToString() );

            Assert.True( header.AddRtpInfo( new RtpInfo("rtsp://127.0.0.1") ) );
            Assert.AreEqual( "url=rtsp://127.0.0.1" , header.ToString() );

            Assert.True( header.AddRtpInfo( new RtpInfo("rtsp://192.168.1.1" , 1 ) )  );
            Assert.AreEqual( "url=rtsp://127.0.0.1, url=rtsp://192.168.1.1;seq=1" , header.ToString() );

            Assert.True( header.AddRtpInfo( new RtpInfo("rtsp://192.168.1.2" , 1 , 2 ) )  );
            Assert.AreEqual( "url=rtsp://127.0.0.1, url=rtsp://192.168.1.1;seq=1, url=rtsp://192.168.1.2;seq=1;rtptime=2" , header.ToString() );
        }
    }
}
