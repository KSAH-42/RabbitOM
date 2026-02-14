using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestContentRangeHeader
    {
        [Test]
        [TestCase("bytes 0-12/123" )]
        [TestCase("bytes    0-12/123  " )]
        [TestCase("  bytes    0-12/123  " )]
        [TestCase("\r \n bytes   \t 0-12/123  " )]
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

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( " , , , , , , , " )]
        
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  ContentRangeRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
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

        [Test]
        public void TestValidation()
        {
            var header = new ContentRangeRtspHeader();
            Assert.IsFalse(  header.TryValidate() );

            header.Unit = ",,,";
            Assert.IsFalse(  header.TryValidate() );

            header.Unit = "bytes";
            Assert.IsFalse(  header.TryValidate() );

            header.From = 0;
            Assert.IsFalse(  header.TryValidate() );

            header.From = null;
            header.To = 0;
            Assert.IsFalse(  header.TryValidate() );

            header.From = null;
            header.To = null;
            header.Size = 10;
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
