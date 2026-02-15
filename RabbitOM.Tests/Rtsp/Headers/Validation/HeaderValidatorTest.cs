using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;
using NUnit.Framework;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers.Validation
{
    [TestFixture]
    public class HeaderValidatorTest
    {
        [TestCase( "*" )]
        [TestCase( "!")]
        [TestCase( "#")]
        [TestCase( "$")]
        [TestCase( "%")]
        [TestCase( "&")]
        [TestCase( "'")]
        [TestCase( "*")]
        [TestCase( "+")]
        [TestCase( "-")]
        [TestCase( ".")]
        [TestCase( "^")]
        [TestCase( "_")]
        [TestCase( "`")]
        [TestCase( "|")]
        [TestCase( "~")]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("[")]
        [TestCase("]")]
        [TestCase("{")]
        [TestCase("}")]
        [TestCase("<")]
        [TestCase(">")]
        [TestCase("@")]
        [TestCase(",")]
        [TestCase(";")]
        [TestCase(":")]
        [TestCase("=")]
        [TestCase("?")]
        [TestCase("/")]
        [TestCase("\\")]
        [TestCase( "abcdef" )]
        [TestCase( "abcdef123" )]
        [TestCase( "AZERTYUIOPQSDFGHJKLMWXCVBN0123456789" )]
        [TestCase( " AZERTYUIOPQSDFGH JKLMWXCVBN0123456789 " )]
        [TestCase( "kader-sahnine" )]
        [TestCase( "kader_sahnine" )]
        [TestCase("rtsp://admin:camera123@127.0.0.1:554/screenlive")]
        [TestCase("session=123;timeout=56")]
        [TestCase("Digest username=\"admin\";nonce=\"a1a1a1a11a1a1\";")]
        [TestCase("Digest username='admin';nonce='a1a1a1a11a1a1';")]
        public void TryValidateStringTestSuccceed( string text )
        {
            Assert.IsTrue( RtspHeaderValidator.TryValidateString( text ) );
        }

        [TestCase( "*" )]
        [TestCase( "!")]
        [TestCase( "#")]
        [TestCase( "$")]
        [TestCase( "%")]
        [TestCase( "&")]
        [TestCase( "'")]
        [TestCase( "*")]
        [TestCase( "+")]
        [TestCase( "-")]
        [TestCase( ".")]
        [TestCase( "^")]
        [TestCase( "_")]
        [TestCase( "`")]
        [TestCase( "|")]
        [TestCase( "~")]
        [TestCase("/")]
        [TestCase("\\")]
        [TestCase( "abcdef" )]
        [TestCase( "abcdef123" )]
        [TestCase( "AZERTYUIOPQSDFGHJKLMWXCVBN0123456789" )]
        [TestCase( "kader-sahnine" )]
        [TestCase( "kader_sahnine" )]
        public void TryValidateTokenTestSuccceed( string text )
        {
            Assert.IsTrue( RtspHeaderValidator.TryValidateToken( text ) );
        }

        [TestCase("rtsp://127.0.0.1")]
        [TestCase("rtsp://127.0.0.1/screenlive")]
        [TestCase("rtsp://127.0.0.1/screenlive?parameter=1")]
        [TestCase("rtsp://127.0.0.1/screenlive?parameter=1&parameter2=2")]
        [TestCase("rtsp://127.0.0.1:554/screenlive")]
        [TestCase("rtsp://127.0.0.1:554/screenlive?parameter=1&parameter2=2")]
        [TestCase("rtsp://admin@127.0.0.1")]
        [TestCase("rtsp://admin@127.0.0.1/screenlive")]
        [TestCase("rtsp://admin:@127.0.0.1/screenlive")]
        [TestCase("rtsp://admin:camera123@127.0.0.1/screenlive")]
        [TestCase("rtsp://admin@127.0.0.1:554/screenlive")]
        [TestCase("rtsp://admin:@127.0.0.1:554/screenlive")]
        [TestCase("rtsp://admin:camera123@127.0.0.1:554/screenlive")]
        [TestCase("rtsp://admin:camera123@127.0.0.1:554/screenlive?a=1&b=2")]
        [TestCase("abcdef")]
        [TestCase("abcdef/ght")]
        public void TryValidateUriTestSucceed( string text )
        {
            Assert.IsTrue( RtspHeaderValidator.TryValidateUri( text ) );
        }

        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("1")]
        [TestCase(" 1 ")]
        [TestCase(" 0 ")]
        [TestCase(" -1 ")]
        [TestCase("'0'")]
        [TestCase(" '0' ")]
        [TestCase("\"0\"")]
        [TestCase(" \"0\" ")]
        public void TryValidateLongTestSucceed( string text )
        {
            Assert.IsTrue( RtspHeaderValidator.TryValidateLong( text ) );
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

        [TestCase( null )]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("x")]
        [TestCase("\r\n")]
        [TestCase("\r\n1")]
        public void TryValidateLongTestFailed( string text )
        {
            Assert.IsFalse( RtspHeaderValidator.TryValidateLong( text ) );
        }
    }
}
