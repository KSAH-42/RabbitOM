using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestBandwithHeader
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
            if ( ! BandwithRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.AreEqual( valueToCompare , result.BitRate );
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
            Assert.IsFalse(  BandwithRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat()
        {
            var header = new BandwithRtspHeader();

            Assert.AreEqual( "0" , header.ToString() );

            header.BitRate = 123;

            Assert.AreEqual( "123" , header.ToString() );
        }

        [Test]
        public void TestValidation()
        {
            var header = new BandwithRtspHeader();

            header.BitRate = - 1;

            Assert.IsFalse(  header.TryValidate() );
            
            header.BitRate = 0;
            Assert.IsTrue(  header.TryValidate() );

            header.BitRate = 1;
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
