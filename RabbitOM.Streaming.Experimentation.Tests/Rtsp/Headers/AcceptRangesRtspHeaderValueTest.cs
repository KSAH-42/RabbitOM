using NUnit.Framework;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class AcceptRangesRtspHeaderValueTest
    {
        [Test]
        public void CheckTryParseSucceed()
        {
            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( " bytes , utc , ntp " , out var header ) );
            Assert.IsTrue( header.Bytes );
            Assert.IsFalse( header.Clock );
            Assert.IsTrue( header.Utc );
            Assert.IsFalse( header.Smpte );
            Assert.IsTrue( header.Ntp );

            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( "  clock , smpte " , out header ) );
            Assert.IsFalse( header.Bytes );
            Assert.IsTrue( header.Clock );
            Assert.IsFalse( header.Utc );
            Assert.IsTrue( header.Smpte );
            Assert.IsFalse( header.Ntp );
        }

        [Test]
        public void CheckTryParseSucceedForBytes()
        {
            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( " bytes " , out var header ) );
            Assert.IsTrue( header.Bytes );
        }

        [Test]
        public void CheckTryParseSucceedForClock()
        {
            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( " CloCK " , out var header ) );
            Assert.IsTrue( header.Clock );
        }

        [Test]
        public void CheckTryParseSucceedForNtp()
        {
            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( " ntp " , out var header ) );
            Assert.IsTrue( header.Ntp );
        }

        [Test]
        public void CheckTryParseSucceedForSmpte()
        {
            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( " Smpte " , out var header ) );
            Assert.IsTrue( header.Smpte );
        }

        [Test]
        public void CheckTryParseSucceedForUtc()
        {
            Assert.IsTrue( AcceptRangesRtspHeaderValue.TryParse( " UtC " , out var header ) );
            Assert.IsTrue( header.Utc );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "," )]
        [TestCase( ",," )]
        [TestCase( " , , " )]
        [TestCase( "???" )]
        [TestCase( "byte , yutc , klock , mtp , snpte ")]
        [TestCase( "cloc k")]
        public void CheckTryParseFailed( string input)
        {
            Assert.IsFalse( AcceptRangesRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptRangesRtspHeaderValue();

            Assert.AreEqual( "" , header.ToString() );

            header.Bytes = true;
            Assert.AreEqual( "bytes" , header.ToString() );

            header.Clock = true;
            Assert.IsTrue( header.ToString().Contains( "clock" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 1 );
            
            header.Ntp = true;
            Assert.IsTrue( header.ToString().Contains( "ntp" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 2 );

            header.Smpte = true;
            Assert.IsTrue( header.ToString().Contains( "smpte" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 3 );

            header.Utc = true;
            Assert.IsTrue( header.ToString().Contains( "utc" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 4 );
        }
    }
}
