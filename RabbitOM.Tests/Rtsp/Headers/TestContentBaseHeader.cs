using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestContentBaseHeader
    {
        [TestMethod]
        [DataRow("rtsp://127.0.0.1" )]
        [DataRow("rtsp://127.0.0.1/channel" )]
        [DataRow("rtsp://127.0.0.1/channel?param=1" )]
        [DataRow("rtsp://127.0.0.1/channel?param1=1&param2=2" )]

        [DataRow(" rtsp://127.0.0.1" )]
        [DataRow(" \r rtsp://127.0.0.1/channel" )]
        [DataRow(" \nrtsp://127.0.0.1/channel?param=1" )]
        [DataRow(" \n\rrtsp://127.0.0.1/channel?param1=1&param2=2" )]

        [DataRow(" 'rtsp://127.0.0.1' " )]
        [DataRow(" \r 'rtsp://127.0.0.1/channel' " )]
        
        [DataRow(" \"rtsp://127.0.0.1\" " )]
        [DataRow(" \r \"rtsp://127.0.0.1/channel\" " )]

        public void ParseTestSucceed(string input )
        {
            if ( ! ContentBaseRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.IsNotNull( result.Uri );
            Assert.AreNotEqual( string.Empty , result.Uri );
            Assert.AreNotEqual( true , string.IsNullOrWhiteSpace( result.Uri ) );
            Assert.AreEqual( true , Uri.IsWellFormedUriString( result.Uri , UriKind.RelativeOrAbsolute ) );
        }

        [TestMethod]
        [DataRow( "    " )]
        [DataRow( "  ,  " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( ",d,g,h,ff,h,?," )]
        [DataRow( " , , , , , , , " )]
        [DataRow(" rtsp:1 @27.0.0.1' " )]
        [DataRow("rtsp ://127.0.0.1" )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , ContentBaseRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat()
        {
            var header = new ContentBaseRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.Uri = "  rtsp://127.0.0.1  ";

            Assert.AreEqual( "rtsp://127.0.0.1" , header.ToString() );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ContentBaseRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            
            header.Uri = " rtsp://127.0.0.1  ";

            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
