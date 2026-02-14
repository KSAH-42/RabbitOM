using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class ContentLocationHeaderTest
    {
        [Test]
        [TestCase("rtsp://127.0.0.1" )]
        [TestCase("rtsp://127.0.0.1/channel" )]
        [TestCase("rtsp://127.0.0.1/channel?param=1" )]
        [TestCase("rtsp://127.0.0.1/channel?param1=1&param2=2" )]

        [TestCase(" rtsp://127.0.0.1" )]
        [TestCase(" \r rtsp://127.0.0.1/channel" )]
        [TestCase(" \nrtsp://127.0.0.1/channel?param=1" )]
        [TestCase(" \n\rrtsp://127.0.0.1/channel?param1=1&param2=2" )]

        [TestCase(" 'rtsp://127.0.0.1' " )]
        [TestCase(" \r 'rtsp://127.0.0.1/channel' " )]
        
        [TestCase(" \"rtsp://127.0.0.1\" " )]
        [TestCase(" \r \"rtsp://127.0.0.1/channel\" " )]

        public void ParseTestSucceed(string input )
        {
            if ( ! ContentLocationRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.IsNotNull( result.Uri );
            Assert.AreNotEqual( string.Empty , result.Uri );
            Assert.AreNotEqual( true , string.IsNullOrWhiteSpace( result.Uri ) );
            Assert.IsTrue(  Uri.IsWellFormedUriString( result.Uri , UriKind.RelativeOrAbsolute ) );
        }

        [Test]
        [TestCase( "    " )]
        [TestCase( "  ,  " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( ",d,g,h,ff,h,?," )]
        [TestCase( " , , , , , , , " )]
        [TestCase( " rtps :// r " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  ContentLocationRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat()
        {
            var header = new ContentLocationRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.Uri = "  rtsp://127.0.0.1  ";

            Assert.AreEqual( "rtsp://127.0.0.1" , header.ToString() );
        }

        [Test]
        public void TestValidation()
        {
            var header = new ContentLocationRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            
            header.Uri = " rtsp://127.0.0.1  ";

            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
