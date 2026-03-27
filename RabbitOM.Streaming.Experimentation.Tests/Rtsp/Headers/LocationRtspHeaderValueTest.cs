using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers;

    [TestFixture]
    public class LocationRtspHeaderValueTest
    {
        [TestCase( "rtsp://127.0.0.1" )]
        [TestCase( "rtsp://127.0.0.1:554/screenlive" )]
        [TestCase( "rtsp://admin:camera123@127.0.0.1:554/screenlive" )]
        public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( LocationRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreNotEqual( "" , header.Uri );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( " a b c d e f 1 2 3" )]
        [TestCase( "?:// 127.0.0.1:554/screenlive" )]
        [TestCase( "rtsp:// 127.0.0.1:554/screenlive" )]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( LocationRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
