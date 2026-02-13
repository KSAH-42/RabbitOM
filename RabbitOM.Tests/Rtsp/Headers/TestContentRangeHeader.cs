using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestContentRangeHeader
    {
        [TestMethod]
        [DataRow("bytes 0-12/123" )]
        [DataRow("bytes    0-12/123  " )]
        [DataRow("  bytes    0-12/123  " )]
        [DataRow("\r \n bytes   \t 0-12/123  " )]
        public void ParseTestSucceed(string input)
        {
            if ( ! ContentRangeRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( "bytes" , result.Unit );
            Assert.AreEqual( 0 , result.From );
            Assert.AreEqual( 12 , result.To );
            Assert.AreEqual( 123 , result.Size );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( " , , , , , , , " )]
        
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , ContentRangeRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat()
        {
            var header = new ContentRangeRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.Unit = " bytes ";
            Assert.AreEqual( "bytes */*" , header.ToString() );

            header.From = 0;
            header.To = 1;
            Assert.AreEqual( "bytes 0-1/*" , header.ToString() );

            header.From = null;
            header.To = null;
            header.Size = 1;
            Assert.AreEqual( "bytes */1" , header.ToString() );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ContentRangeRtspHeader();
            Assert.AreEqual( false , header.TryValidate() );

            header.Unit = ",,,";
            Assert.AreEqual( false , header.TryValidate() );

            header.Unit = "bytes";
            Assert.AreEqual( false , header.TryValidate() );

            header.From = 0;
            Assert.AreEqual( false , header.TryValidate() );

            header.From = null;
            header.To = 0;
            Assert.AreEqual( false , header.TryValidate() );

            header.From = null;
            header.To = null;
            header.Size = 10;
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
