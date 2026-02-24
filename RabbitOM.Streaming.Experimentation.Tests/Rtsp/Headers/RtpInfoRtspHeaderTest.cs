using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    [TestFixture]
    public class RtpInfoRtspHeaderTest
    {
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef" , 1 )]
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef," , 1 )]
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef, url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef " , 2 )]
        [TestCase( "url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef, url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef , url=rtsp://127.0.0.1;seq=123;rtptime=321;ssrc=abcdef " , 3 )]
        public void CheckTryParseSucceed(string input , int count )
        {
            Assert.IsTrue( RtpInfoRtspHeader.TryParse( input , out var header ) );
            Assert.AreEqual( count , header.Infos.Count );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( "," ) ]
        [TestCase( ",,,,," ) ]
        [TestCase( "rul=rtsp://127.0.0.1;seq=a" ) ]
        public void CheckTryParseFailed(string input )
        {
            Assert.IsFalse( RtpInfoRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

    }
}
