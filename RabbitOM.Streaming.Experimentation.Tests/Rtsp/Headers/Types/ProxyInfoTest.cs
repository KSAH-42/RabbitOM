using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    [TestFixture]
    public class ProxyInfoTest
    {
        [TestCase( "RTSP/1.1 proxy.mysite.com" , "RTSP" , "1.1" , "proxy.mysite.com" , "" ) ]
        [TestCase( "RTSP/1.1 proxy.mysite.com ()" , "RTSP" , "1.1" , "proxy.mysite.com" , "" ) ]
        [TestCase( "RTSP/1.1 proxy.mysite.com (    )" , "RTSP" , "1.1" , "proxy.mysite.com" , "" ) ]
        [TestCase( "RTSP/1.1 proxy.mysite.com (my comments)" , "RTSP" , "1.1" , "proxy.mysite.com" , "my comments" ) ]
        [TestCase( "  RTSP / 1.1      proxy.mysite.com   (   my comments   )    " , "RTSP" , "1.1" , "proxy.mysite.com" , "my comments" ) ]
        public void CheckTryParseSucceed( string input , string protocol , string version , string receivedBy , string comments )
        {
            Assert.IsTrue( ProxyInfo.TryParse( input , out var result ) );
            Assert.AreEqual( protocol , result.Protocol );
            Assert.AreEqual( version , result.Version );
            Assert.AreEqual( receivedBy , result.ReceivedBy );
            Assert.AreEqual( comments , result.Comments );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "RTSP/ proxy.mysite.com" )]
        [TestCase( "RTSP/1.1" )]
        [TestCase( "/1.1 proxy.mysite.com" )]
        [TestCase( "(comments)" )]
        [TestCase( "RTSP/x.1 proxy.mysite.com")]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( ProxyInfo.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }
    }
}
