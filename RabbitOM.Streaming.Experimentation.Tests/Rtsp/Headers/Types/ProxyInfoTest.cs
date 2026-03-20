using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    [TestFixture]
    public class ProxyInfoTest
    {
        [TestCase( "RTSP/1.1 proxy.mysite.com" , "RTSP" , "1.1" , "proxy.mysite.com" , "" ) ]
        [TestCase( "RTSP/1.1 proxy.mysite.com ()" , "RTSP" , "1.1" , "proxy.mysite.com" , "" ) ]
        [TestCase( "RTSP/1.1 proxy.mysite.com (    )" , "RTSP" , "1.1" , "proxy.mysite.com" , "" ) ]
        [TestCase( "RTSP/1.1 proxy.mysite.com (my comment)" , "RTSP" , "1.1" , "proxy.mysite.com" , "my comment" ) ]
        [TestCase( "  RTSP / 1.1      proxy.mysite.com   (   my comment   )    " , "RTSP" , "1.1" , "proxy.mysite.com" , "my comment" ) ]
        public void CheckTryParseSucceed( string input , string protocol , string version , string receivedBy , string comments )
        {
            Assert.IsTrue( ProxyInfo.TryParse( input , out var result ) );
            Assert.AreEqual( protocol , result.Protocol );
            Assert.AreEqual( version , result.Version );
            Assert.AreEqual( receivedBy , result.ReceivedBy );
            Assert.AreEqual( comments , result.Comment );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "RTSP/ proxy.mysite.com" )]
        [TestCase( "RTSP/1.1" )]
        [TestCase( "/1.1 proxy.mysite.com" )]
        [TestCase( "(comment)" )]
        [TestCase( "RTSP/x.1 proxy.mysite.com")]
        [TestCase( "RTSP/1.0 proxy.mysite.com ((er)")]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( ProxyInfo.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestCase( null , null , null , null )]
        [TestCase( "" , "" , "" , "" )]
        [TestCase( " " , " " , " " , " " )]
        [TestCase( "RTSP" , "x.y" , "" , "" )]
        [TestCase( "RTSP" , "1.0" , "site.proxy.fr" , "(" )]
        [TestCase( "RTSP" , "1.0" , "site.proxy.fr" , ")" )]
        [TestCase( "RTSP" , "1.0" , "site.proxy.fr" , "(abc)" )]
        public void CheckCtorFailed( string protocol , string version , string receivedBy , string comment )
        {
            Assert.Throws<ArgumentException>( () =>  new ProxyInfo( protocol , version , receivedBy , comment ) );
        }
    }
}
