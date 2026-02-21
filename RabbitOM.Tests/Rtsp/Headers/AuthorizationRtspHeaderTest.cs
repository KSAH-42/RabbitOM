using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class AuthorizationRtspHeaderTest
    {
        [TestCase( "digest username='my user name',realm='my realm',nonce='my nonce',domain='my domain',opaque='my opaque',uri='rtsp://0.0.0.0',response='my response',algorithm='my algorithm',cnonce='my cnonce',nc='my nc',qop='my qop'" )]
        public void CheckParseSucceed( string input )
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
            Assert.AreEqual( "my cnonce" , header.CNonce );
            Assert.AreEqual( "my nc" , header.NC );
            Assert.AreEqual( "my qop" , header.QualityOfProtection );
        }

        [TestCase( "digest realm='my realm', username='my user name',domain='my domain'," )]
        public void CheckParseSucceedSimple( string input )
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
        public void CheckParseFailed( string input  )
        {
            Assert.IsFalse( AuthorizationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
