using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;
using NUnit.Framework;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers.Validation
{
    [TestFixture]
    public class HeaderValidatorTestSucceed
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
    }
}
