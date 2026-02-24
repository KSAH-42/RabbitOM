using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class WWWAuthenticateRtspHeaderTest
    {
        [TestCase( "digest realm='my realm',nonce='my nonce',opaque='my opaque',algorithm='my algorithm',stale='my stale',qop='my qop'" )]
        public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( WWWAuthenticateRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "digest" , header.Scheme );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my nonce" , header.Nonce );
            Assert.AreEqual( "my opaque" , header.Opaque );
            Assert.AreEqual( "my algorithm" , header.Algorithm );
            Assert.AreEqual( "my stale" , header.Stale );
            Assert.AreEqual( "my qop" , header.QualityOfProtection );
        }

        [TestCase( "digest nonce='my nonce',realm='my realm',opaque='my opaque',stale='my stale',qop='my qop',algorithm='my algorithm'" )]
        public void CheckTryParseSucceedDifferentOrder( string input )
        {
            Assert.IsTrue( WWWAuthenticateRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "digest" , header.Scheme );
            Assert.AreEqual( "my realm" , header.Realm );
            Assert.AreEqual( "my nonce" , header.Nonce );
            Assert.AreEqual( "my opaque" , header.Opaque );
            Assert.AreEqual( "my algorithm" , header.Algorithm );
            Assert.AreEqual( "my stale" , header.Stale );
            Assert.AreEqual( "my qop" , header.QualityOfProtection );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( WWWAuthenticateRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
