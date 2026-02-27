using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class RtspHeaderValueNormalizerTest
    {
        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\r" )]
        [TestCase( "\n" )]
        [TestCase( "\r\n" )]
        [TestCase( "¨" )]
        [TestCase( "^" )]
        [TestCase( "$" )]
        [TestCase( "€" )]
        [TestCase( "£" )]
        [TestCase( "¤" )]
        [TestCase( "ù" )]
        [TestCase( "§" )]
        [TestCase( "µ" )]
        [TestCase( "é" )]
        [TestCase( "è" )]
        [TestCase( "ç" )]
        [TestCase( "à" )]
        [TestCase( "²" )]
        public void CheckReturnsValueAsEmpty( string input )
        {
            Assert.AreEqual( "" , StringRtspHeaderNormalizer.Normalize( input ) );
        }

        [TestCase( " abcdecf " , "abcdecf" )]

        [TestCase( "\rabcdecf\r" , "abcdecf" )]
        [TestCase( "\r  abcdecf  \r" , "abcdecf" )]
        [TestCase( "\r  abcdecf  \r" , "abcdecf" )]
        [TestCase( "\r  abc\rdecf  \r" , "abcdecf" )]
        [TestCase( "\r ' abc\rdecf ' \r" , "abcdecf" )]
        [TestCase( "\r \' abc\rdecf \' \r" , "abcdecf" )]
        [TestCase( "\r \" abc\rdecf \" \r" , "abcdecf" )]
        [TestCase( "\r \" abc\rd¤ecf \" \r" , "abcdecf" )]
        [TestCase( "\r \" Abc\rd§Ecf \" \r" , "AbcdEcf" )]
        [TestCase( "\r \" 1 Abc\rd§Ecf 1 \" \r" , "1 AbcdEcf 1" )]
        public void CheckNormalization( string input , string output )
        {
            Assert.AreEqual( output , StringRtspHeaderNormalizer.Normalize( input ) );
        }

        [TestCase( "\r \" Abc\rd§Ecf \" \r" , "bcdEcf" )]
        [TestCase( "\r \" 1 Abc\rd§Ecf 1 \" \r" , "1 bcdEcf 1" )]
        public void CheckNormalizationWithFilter( string input , string output )
        {
            Assert.AreEqual( output , StringRtspHeaderNormalizer.Normalize( input , "A" ) );
        }
    }
}
