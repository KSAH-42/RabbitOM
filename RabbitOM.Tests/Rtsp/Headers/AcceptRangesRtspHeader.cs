using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class AcceptRangesRtspHeaderTest
    {
        [Test]
        public void CheckParseSucceed1()
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
        public void CheckParseSucceedForBytes()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " bytes " , out var header ) );
            Assert.IsTrue( header.Bytes );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Bytes );
        }

        [Test]
        public void CheckParseSucceedForNtp()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " ntp " , out var header ) );
            Assert.IsTrue( header.Ntp );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Ntp );
        }

        [Test]
        public void CheckParseSucceedForClock()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " CloCK " , out var header ) );
            Assert.IsTrue( header.Clock );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Clock );
        }

        [Test]
        public void CheckParseSucceedForUtc()
        {
            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " \rUtC " , out var header ) );
            Assert.IsTrue( header.Utc );

            Assert.IsTrue( AcceptRangesRtspHeader.TryParse( " unknown " , out header ) );
            Assert.IsFalse( header.Utc );
        }

        [Test]
        public void CheckParseSucceedForNone()
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
        public void CheckParseFailed( string input)
        {
            Assert.IsFalse( AcceptRangesRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
