using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    [TestFixture]
    public class RtpInfoTest
    {
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef" )]
        public void CheckTryParseSucceed1(string input )
        {
            Assert.IsTrue( RtpInfo.TryParse( input , out var header ) );
            Assert.AreEqual( "rtsp://127.0.0.1" , header.Url );
            Assert.AreEqual( 123 , header.Sequence );
            Assert.AreEqual( 321 , header.RtpTime );
            Assert.AreEqual( "abcdef" , header.SSRC );
        }

        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;" )]
        public void CheckTryParseSucceed2(string input )
        {
            Assert.IsTrue( RtpInfo.TryParse( input , out var header ) );
            Assert.AreEqual( "rtsp://127.0.0.1" , header.Url );
            Assert.AreEqual( 123 , header.Sequence );
            Assert.AreEqual( 321 , header.RtpTime );
            Assert.AreEqual( "" , header.SSRC );
        }

        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=;" )]
        public void CheckTryParseSucceed3(string input )
        {
            Assert.IsTrue( RtpInfo.TryParse( input , out var header ) );
            Assert.AreEqual( "rtsp://127.0.0.1" , header.Url );
            Assert.AreEqual( 123 , header.Sequence );
            Assert.AreEqual( null , header.RtpTime );
            Assert.AreEqual( "" , header.SSRC );
        }

        [TestCase( "url=rtsp://127.0.0.1;seq=;" )]
        public void CheckTryParseSucceed4(string input )
        {
            Assert.IsTrue( RtpInfo.TryParse( input , out var header ) );
            Assert.AreEqual( "rtsp://127.0.0.1" , header.Url );
            Assert.AreEqual( null , header.Sequence );
            Assert.AreEqual( null , header.RtpTime );
            Assert.AreEqual( "" , header.SSRC );
        }
    }
}
