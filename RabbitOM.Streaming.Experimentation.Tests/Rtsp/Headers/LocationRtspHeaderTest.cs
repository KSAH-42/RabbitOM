using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class LocationRtspHeaderTest
    {
        [TestCase( "rtsp://127.0.0.1" )]
        [TestCase( "rtsp://admin:camera123@127.0.0.1:554/screenlive" )]
        [TestCase( " rtsp://admin:camera123@127.0.0.1:554/screenlive " )]
        public void CheckParseSucceed( string input )
        {
            Assert.IsTrue( LocationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.IsNotNull( header.Uri);
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( " ::::::::: " ) ]
        public void CheckParseFailed( string input  )
        {
            Assert.IsFalse( LocationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
