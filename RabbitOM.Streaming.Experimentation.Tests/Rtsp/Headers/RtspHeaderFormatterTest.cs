using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    /*
    [TestFixture]
    public class RtspHeaderFormatterTest
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
            Assert.AreEqual( "" , RtspHeaderParser.Formatter.Filter( input ) );
        }

        [TestCase( " abcdecf " , "abcdecf" )]

        [TestCase( "\rabcdecf\r" , "abcdecf" )]
        [TestCase( "\r  abcdecf  \r" , "abcdecf" )]
        [TestCase( "\r  abcdecf  \r" , "abcdecf" )]
        [TestCase( "\r  abc\rdecf  \r" , "abcdecf" )]
        public void CheckNormalization( string input , string output )
        {
            Assert.AreEqual( output , RtspHeaderParser.Formatter.Filter( input ) );
        }

        [TestCase( null , "\"\"" )]
        [TestCase( "" , "\"\"" )]
        [TestCase( " " , "\"\"" )]
        [TestCase( "abc" , "\"abc\"" )]
        [TestCase( " abc " , "\"abc\"" )]
        public void CheckQuoteValue( string input , string output )
        {
            Assert.AreEqual( output , RtspHeaderParser.Formatter.Quote( input ) );
        }
    }

    */
}
