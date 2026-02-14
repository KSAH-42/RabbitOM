using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestContentLengthRtspHeader
    {
        [Test]
        [TestCase("4" , 4 )]
        [TestCase("\"4\"" , 4 )]
        [TestCase("'4'" , 4 )]
        [TestCase(" 4 " , 4 )]
        [TestCase(" \"4\" " , 4 )]
        [TestCase(" '4' " , 4 )]
        public void ParseTestSucceed(string input , int valueToCompare )
        {
            if ( ! ContentLengthRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.AreEqual( valueToCompare , result.Value );
        }

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( ",d,g,h,ff,h,?," )]
        [TestCase( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  ContentLengthRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat()
        {
            var header = new ContentLengthRtspHeader();

            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 123;

            Assert.AreEqual( "123" , header.ToString() );
        }

        [Test]
        public void TestValidation()
        {
            var header = new ContentLengthRtspHeader();

            header.Value = - 1;

            Assert.IsFalse(  header.TryValidate() );
            
            header.Value = 1;

            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
