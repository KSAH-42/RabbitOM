using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;
using NUnit.Framework;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers.Validation
{
    [TestFixture]
    public class HeaderValidatorTestFailed
    {
        [TestCase( null )]
        [TestCase( "\0" )]
        [TestCase( "\r" )]
        [TestCase( "\n" )]
        [TestCase( "\f" )]
        [TestCase( "\v" )]
        [TestCase( "\t" )]
        [TestCase( "a\r" )]
        [TestCase( "a\n" )]
        [TestCase( "a\v" )]
        [TestCase( "a\t" )]
        [TestCase( "é" )]
        [TestCase( "è")]
        [TestCase( "à")]
        [TestCase( "ù" )]
        [TestCase( "ç" )]
        [TestCase( "€" )]
        [TestCase( "£" )]
        [TestCase( "§" )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\rabcdef" )]
        [TestCase( "abc\ndef123" )]
        [TestCase( "AZ\nERTYUIOPQSDFGHJKLMWXCVBN0123456789" )]
        [TestCase( " AZERTY\tUIOPQSDFGH JKLMWXCVBN0123456789 " )]
        [TestCase( " AZERTY\vUIOPQSDFGH JKLMWXCVBN0123456789 " )]
        [TestCase( " AZERTY\vUIOPQSDFGH JKLMWXCVBN0123456789\r\n" )]
        public void TryValidateStringTestFailed( string text )
        {
            Assert.IsFalse( RtspHeaderValidator.TryValidateString( text ) );
        }

        
        [TestCase( null )]
        [TestCase( "\0" )]
        [TestCase( "\r" )]
        [TestCase( "\n" )]
        [TestCase( "\f" )]
        [TestCase( "\v" )]
        [TestCase( "\t" )]
        [TestCase( "a\r" )]
        [TestCase( "a\n" )]
        [TestCase( "a\v" )]
        [TestCase( "a\t" )]
        [TestCase( "é" )]
        [TestCase( "è")]
        [TestCase( "à")]
        [TestCase( "ù" )]
        [TestCase( "ç" )]
        [TestCase( "€" )]
        [TestCase( "£" )]
        [TestCase( "§" )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\rabcdef" )]
        [TestCase( "abc\ndef123" )]
        [TestCase( "AZ\nERTYUIOPQSDFGHJKLMWXCVBN0123456789" )]
        [TestCase( " AZERTY\tUIOPQSDFGH JKLMWXCVBN0123456789 " )]
        [TestCase( " AZERTY\vUIOPQSDFGH JKLMWXCVBN0123456789 " )]
        [TestCase( " AZERTY\vUIOPQSDFGH JKLMWXCVBN0123456789\r\n" )]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("[")]
        [TestCase("]")]
        [TestCase("{")]
        [TestCase("}")]
        [TestCase("<")]
        [TestCase(">")]
        [TestCase(",")]
        [TestCase(";")]
        [TestCase(":")]
        [TestCase("=")]
        [TestCase("?")]
        [TestCase(" ")]
        [TestCase(" abcd ")]
        [TestCase("ab cd")]
        public void TryValidateTokenTestFailed( string text )
        {
            Assert.IsFalse( RtspHeaderValidator.TryValidateToken( text ) );
        }

        [TestCase( null )]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("\r\nrtsp://admin:@127.0.0.1:554/screenlive")]
        [TestCase("rtsp?/127.0.0.1:554/screenlive?a=1&b=2")]
        public void TryValidateUriTestFailed( string text )
        {
            Assert.IsFalse( RtspHeaderValidator.TryValidateUri( text ) );
        }
    }
}
