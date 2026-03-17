using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved;

    [TestFixture]
    public class AuthorizationRtspHeaderTest
    {
        [TestCase( "digest username='my user name',realm='my realm',nonce='my nonce',domain='my domain',opaque='my opaque',uri='rtsp://0.0.0.0',response='my response',algorithm='my algorithm',cnonce='my cnonce',nc='my nc',qop='my qop'" )]
        [TestCase( "digest realm='my realm',username='my user name',nonce='my nonce',domain='my domain',opaque='my opaque',uri='rtsp://0.0.0.0',response='my response',algorithm='my algorithm',cnonce='my cnonce',nc='my nc',qop='my qop'" )]
        [TestCase( "digest realm='my realm',,,,,,username='my user name',nonce='my nonce',domain='my domain',opaque='my opaque',uri='rtsp://0.0.0.0',response='my response',algorithm='my algorithm',cnonce='my cnonce',nc='my nc',qop='my qop'" )]
        [TestCase( " digest   username =   'my user name' ,  realm=  'my realm' , nonce= 'my nonce',domain='my domain',opaque='my opaque',uri='rtsp://0.0.0.0' , response = 'my response' , algorithm = 'my algorithm' ,    cnonce = 'my cnonce', nc ='my nc' , qop ='my qop'" )]
        public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( AuthorizationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "digest" , header.Scheme );
            Assert.AreEqual( "my user name" , header.UserName );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my nonce" , header.Nonce );
            Assert.AreEqual( "my domain" , header.Domain );
            Assert.AreEqual( "my opaque" , header.Opaque );
            Assert.AreEqual( "rtsp://0.0.0.0" , header.Uri );
            Assert.AreEqual( "my response" , header.Response );
            Assert.AreEqual( "my algorithm" , header.Algorithm );
            Assert.AreEqual( "my cnonce" , header.ClientNonce );
            Assert.AreEqual( "my nc" , header.NonceCount );
            Assert.AreEqual( "my qop" , header.QualityOfProtection );
        }

        [TestCase( "digest realm='my realm', username='my user name',domain='my domain'," )]
        [TestCase( "digest realm = 'my realm' , username='my user name' , domain='my domain'," )]
        public void CheckTryParseSucceedSimple( string input )
        {
            Assert.IsTrue( AuthorizationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "digest" , header.Scheme );
            Assert.AreEqual( "my user name" , header.UserName );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my domain" , header.Domain );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( "_" ) ]
        [TestCase( "???? username='my user name', realm='my realm',domain='my domain'," )]
        [TestCase( "digest username='', realm='my realm',domain='my domain'," )]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( AuthorizationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AuthorizationRtspHeader();

            header.Scheme = "digest";
            header.UserName = "admin";

            Assert.AreEqual( "digest username=\"admin\", realm=\"\", nonce=\"\", uri=\"\", response=\"\"" , header.ToString() );
        }

        [Test]
        public void CheckToString1()
        {
            var header = new AuthorizationRtspHeader();

            header.Scheme = "digest";
            header.UserName = "admin";
            header.Realm = "my realm";

            Assert.AreEqual( "digest username=\"admin\", realm=\"my realm\", nonce=\"\", uri=\"\", response=\"\"" , header.ToString() );
        }

        [Test]
        public void CheckToString2()
        {
            var header = new AuthorizationRtspHeader();

            header.Scheme = "digest";
            header.UserName = "admin";
            header.Realm = "my realm";
            header.Nonce = "1234";

            Assert.AreEqual( "digest username=\"admin\", realm=\"my realm\", nonce=\"1234\", uri=\"\", response=\"\"" , header.ToString() );
        }

        [Test]
        public void CheckToString3()
        {
            var header = new AuthorizationRtspHeader();

            header.Scheme = "digest";
            header.UserName = "admin";
            header.Realm = "my realm";
            header.Nonce = "1234";
            header.Uri = "rtsp://127.0.0.1" ;

            Assert.AreEqual( "digest username=\"admin\", realm=\"my realm\", nonce=\"1234\", uri=\"rtsp://127.0.0.1\", response=\"\"" , header.ToString() );
        }

        [Test]
        public void CheckToString4()
        {
            var header = new AuthorizationRtspHeader();

            header.Scheme = "digest";
            header.UserName = "admin";
            header.Realm = "my realm";
            header.Nonce = "1234";
            header.Uri = "rtsp://127.0.0.1" ;
            header.Response = "my response";

            Assert.AreEqual( "digest username=\"admin\", realm=\"my realm\", nonce=\"1234\", uri=\"rtsp://127.0.0.1\", response=\"my response\"" , header.ToString() );
        }

        [Test]
        public void CheckToString5()
        {
            var header = new AuthorizationRtspHeader();

            header.Scheme = "digest";
            header.UserName = "admin";
            header.Realm = "my realm";
            header.Nonce = "1234";
            header.Uri = "rtsp://127.0.0.1" ;
            header.Response = "my response";

            header.AddExtension( "a" );
            header.AddExtension( "b" );
            header.AddExtension( "c=d" );

            Assert.AreEqual( "digest username=\"admin\", realm=\"my realm\", nonce=\"1234\", uri=\"rtsp://127.0.0.1\", response=\"my response\", a, b, c=\"d\"" , header.ToString() );
        }
    }
}
