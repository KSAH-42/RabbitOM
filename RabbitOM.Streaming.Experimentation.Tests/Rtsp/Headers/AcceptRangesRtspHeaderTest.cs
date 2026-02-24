using NUnit.Framework;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class AcceptRangesRtspHeaderTest
    {
        [Test]
        public void CheckTryParseSucceed()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " bytes , utc , ntp " , out var header ) );
            Assert.IsTrue( header.Bytes );
            Assert.IsFalse( header.Clock );
            Assert.IsTrue( header.Utc );
            Assert.IsFalse( header.Smpte );
            Assert.IsTrue( header.Ntp );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( "  clock , smpte " , out header ) );
            Assert.IsFalse( header.Bytes );
            Assert.IsTrue( header.Clock );
            Assert.IsFalse( header.Utc );
            Assert.IsTrue( header.Smpte );
            Assert.IsFalse( header.Ntp );
        }

        [Test]
        public void CheckTryParseSucceedForBytes()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " bytes " , out var header ) );
            Assert.IsTrue( header.Bytes );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Bytes );
        }

        [Test]
        public void CheckTryParseSucceedForNtp()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " ntp " , out var header ) );
            Assert.IsTrue( header.Ntp );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Ntp );
        }

        [Test]
        public void CheckTryParseSucceedForClock()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " CloCK " , out var header ) );
            Assert.IsTrue( header.Clock );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Clock );
        }

        [Test]
        public void CheckTryParseSucceedForUtc()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " \rUtC " , out var header ) );
            Assert.IsTrue( header.Utc );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Utc );
        }

        [Test]
        public void CheckTryParseSucceedForNone()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out var header ) );
            Assert.IsFalse( header.Bytes );
            Assert.IsFalse( header.Ntp );
            Assert.IsFalse( header.Smpte );
            Assert.IsFalse( header.Clock );
            Assert.IsFalse( header.Utc );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "," )]
        [TestCase( ",," )]
        [TestCase( " , , " )]
        public void CheckTryParseFailed( string input)
        {
            Assert.IsFalse( AcceptRangesRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.Bytes = true;
            Assert.AreEqual( "bytes" , header.ToString() );

            header.Clock = true;
            Assert.IsTrue( header.ToString().Contains( "bytes" ) );
            Assert.IsTrue( header.ToString().Contains( "clock" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 1 );
            
            header.Ntp = true;
            Assert.IsTrue( header.ToString().Contains( "bytes" ) );
            Assert.IsTrue( header.ToString().Contains( "clock" ) );
            Assert.IsTrue( header.ToString().Contains( "ntp" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 2 );

            header.Smpte = true;
            Assert.IsTrue( header.ToString().Contains( "bytes" ) );
            Assert.IsTrue( header.ToString().Contains( "clock" ) );
            Assert.IsTrue( header.ToString().Contains( "ntp" ) );
            Assert.IsTrue( header.ToString().Contains( "smpte" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 3 );

            header.Utc = true;
            Assert.IsTrue( header.ToString().Contains( "bytes" ) );
            Assert.IsTrue( header.ToString().Contains( "clock" ) );
            Assert.IsTrue( header.ToString().Contains( "ntp" ) );
            Assert.IsTrue( header.ToString().Contains( "smpte" ) );
            Assert.IsTrue( header.ToString().Contains( "utc" ) );
            Assert.IsTrue( header.ToString().Count( x => x == ','  ) == 4 );
        }
    }
}
