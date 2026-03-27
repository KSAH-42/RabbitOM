using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers;

    [TestFixture]
    public class AuthenticateRtspHeaderValueTest
    {
        [TestCase( "digest realm='my realm',nonce='my nonce',opaque='my opaque',algorithm='my algorithm',stale='True',qop='my qop'" )]
        [TestCase( "digest nonce='my nonce',realm='my realm',opaque='my opaque',stale='true',qop='my qop',algorithm='my algorithm'" )]
        [TestCase( "digest realm=my realm,nonce='my nonce',opaque='my opaque',algorithm='my algorithm',stale='true',qop='my qop'" )]
        [TestCase( "digest realm=\"my realm\",nonce='my nonce',opaque='my opaque',algorithm='my algorithm',stale='TrUe',qop='my qop'" )]
        [TestCase( "   digest      opaque='my opaque' , realm = \"my realm \" , nonce = 'my nonce' ,algorithm='my algorithm',stale= 'true' ,qop='my qop'" )]
        [TestCase( "digest realm='my realm',,, ,,,nonce='my nonce',opaque='my opaque',algorithm='my algorithm',stale= 'TRUE' ,qop='my qop'" )]
        public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( AuthenticateRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "digest" , header.Scheme );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my nonce" , header.Nonce );
            Assert.AreEqual( "my opaque" , header.Opaque );
            Assert.AreEqual( "my algorithm" , header.Algorithm );
            Assert.AreEqual( true , header.Stale );
            Assert.AreEqual( "my qop" , header.QualityOfProtection );
        }

        
        [TestCase( "digest realm='my realm',nonce='my nonce',stale='True'" , true )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale= 'true' " , true )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale= ' trUe ' " , true )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale=\"false\"" , false )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale=\"falSe\"" , false )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale= true " , true )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale= false " , false )]
        [TestCase( "digest realm='my realm',nonce='my nonce',stale=  " , null )]
        [TestCase( "digest realm='my realm',nonce='my nonce'" , null )]
        public void CheckTryParseSucceedForStale( string input , object stale)
        {
            Assert.IsTrue( AuthenticateRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "digest" , header.Scheme );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my nonce" , header.Nonce );
            Assert.AreEqual( stale , header.Stale );
        }

        [TestCase( "digest nonce='my nonce', realm='my realm'", "digest")]
        [TestCase( "DIGEST noNce=my nonce, realm='my realm'", "DIGEST")]
        public void CheckTryParseSucceedForDigest( string input , string scheme )
        {
            Assert.IsTrue( AuthenticateRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( scheme , header.Scheme );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my nonce" , header.Nonce );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( "?" ) ]
        [TestCase( "???? realm='my realm',nonce='my nonce',opaque='my opaque',algorithm='my algorithm',stale='my stale',qop='my qop'" )]
        [TestCase( "digest realm='my realm',nonce=''" )]
        [TestCase( "digest realm='',nonce='a'" )]
        [TestCase( "digest " )]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( AuthenticateRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AuthenticateRtspHeaderValue();

            Assert.AreEqual( "" , header.ToString() );

            header.Scheme = "Digest";
            header.Realm = "a";

            Assert.AreEqual( "Digest realm=\"a\"" , header.ToString() );

            header.Nonce = "b";
            Assert.AreEqual( "Digest realm=\"a\", nonce=\"b\"" , header.ToString() );

            header.Opaque = "c";
            Assert.AreEqual( "Digest realm=\"a\", nonce=\"b\", opaque=\"c\"" , header.ToString() );

            header.Algorithm = "d";
            Assert.AreEqual( "Digest realm=\"a\", nonce=\"b\", opaque=\"c\", algorithm=\"d\"" , header.ToString() );

            header.Stale = true;
            Assert.AreEqual( "Digest realm=\"a\", nonce=\"b\", opaque=\"c\", algorithm=\"d\", stale=\"true\"" , header.ToString() );

            header.Stale = false;
            Assert.AreEqual( "Digest realm=\"a\", nonce=\"b\", opaque=\"c\", algorithm=\"d\", stale=\"false\"" , header.ToString() );

            header.Stale = null;

            header.QualityOfProtection = "f";
            Assert.AreEqual( "Digest realm=\"a\", nonce=\"b\", opaque=\"c\", algorithm=\"d\", qop=\"f\"" , header.ToString() );
        }
    }
}
