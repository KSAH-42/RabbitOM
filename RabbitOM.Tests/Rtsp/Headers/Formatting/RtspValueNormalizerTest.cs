using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers.Formatting
{
    [TestFixture]
    public class RtspValueNormalizerTest
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
        [TestCase( "..." )]
        [TestCase( " ... " )]
        public void CheckReturnsValueAsEmpty( string input )
        {
            Assert.AreEqual( "" , RtspValueNormalizer.Normalize( input ) );
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
            Assert.AreEqual( output , RtspValueNormalizer.Normalize( input ) );
        }

        [TestCase( "\r \" Abc\rd§Ecf \" \r" , "bcdEcf" )]
        [TestCase( "\r \" 1 Abc\rd§Ecf 1 \" \r" , "1 bcdEcf 1" )]
        public void CheckNormalizationWithFilter( string input , string output )
        {
            Assert.AreEqual( output , RtspValueNormalizer.Normalize( input , "A" ) );
        }
    }
}
