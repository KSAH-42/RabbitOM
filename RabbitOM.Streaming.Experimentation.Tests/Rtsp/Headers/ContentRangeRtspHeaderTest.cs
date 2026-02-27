using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class ContentRangeRtspHeaderTest
    {
        [TestCase( "bytes 1-2/3" , "bytes" , 1 , 2 , 3 )]
        [TestCase( "bytes 1-2/*" , "bytes" , 1 , 2 , null )]
        [TestCase( "bytes */3" , "bytes" , null , null , 3 )]
        public void CheckTryParseSucceed( string input , string unit , long? start , long? end , long? size )
        {
            Assert.IsTrue( ContentRangeRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( unit , header.Unit );
            Assert.AreEqual( start , header.Start );
            Assert.AreEqual( end , header.End );
            Assert.AreEqual( size , header.Size );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( ContentRangeRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new ContentRangeRtspHeaderValue();

            Assert.AreEqual( "" , header.ToString() );

            header.SetUnit( "bytes" );
            header.Start = 1;
            header.End = 2;
            Assert.AreEqual( "bytes 1-2/*" , header.ToString() );

            header.Size = 3;
            Assert.AreEqual( "bytes 1-2/3" , header.ToString() );

            header.Start = null;
            header.End = null;
            Assert.AreEqual( "bytes */3" , header.ToString() );
        }
    }
}
