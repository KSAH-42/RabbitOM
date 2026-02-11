using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestAuthorizationHeader
    {
        [TestMethod]
        [DataRow( "Digest username=\"alice\", realm=\"Streaming Server\", nonce=\"dcd98b7102dd2f0e8b11d0f600bfb0c093\", domain=\"my domain\", opaque=\"my opaque data\", uri=\"rtsp://example.com/media\", response=\"6629fae49393a05397450978507c4ef1\"" )]
        [DataRow( "\r \n digest usErname = \"alice\" ,       rEalm = \"Streaming Server\" , noncE = \"dcd98b7102dd2f0e8b11d0f600bfb0c093\"   ,  domaiN = \"my domain\" , oPaque = \"my opaque data\" , \r uRi = \"rtsp://example.com/media\" , reSponse \t = \"6629fae49393a05397450978507c4ef1\" " )]
        [DataRow( "\n \n digest usErname     =    alice      , rEalm = Streaming Server , noncE = dcd98b7102dd2f0e8b11d0f600bfb0c093   ,  domaiN = my domain , oPaque = my opaque data , \r uRi = rtsp://example.com/media , reSponse \t = 6629fae49393a05397450978507c4ef1 " )]
        [DataRow( "\r \n digest        usErname = 'alice' ,    rEalm = Streaming Server , noncE = dcd98b7102dd2f0e8b11d0f600bfb0c093   ,  domaiN = my domain , oPaque = my opaque data , \r uRi = rtsp://example.com/media , reSponse \t = 6629fae49393a05397450978507c4ef1 " )]
        [DataRow( "\r \r digest usErname   \t  =       ' alice \" ,        rEalm = Streaming Server , noncE = dcd98b7102dd2f0e8b11d0f600bfb0c093   ,  domaiN = my domain , oPaque = my opaque data , \r uRi = rtsp://example.com/media , reSponse \t = 6629fae49393a05397450978507c4ef1 " )]
        public void ParseTestSucceed(string input )
        {
            if ( ! AuthorizationRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( true , RtspAuthenticationTypes.IsDigestAuthentication( result.Type ) );
            Assert.AreEqual( "alice" , result.UserName );
            Assert.AreEqual( "Streaming Server" , result.Realm );
            Assert.AreEqual( "dcd98b7102dd2f0e8b11d0f600bfb0c093" , result.Nonce );
            Assert.AreEqual( "my domain" , result.Domain );
            Assert.AreEqual( "my opaque data" , result.Opaque );
            Assert.AreEqual( "rtsp://example.com/media" , result.Uri );
            Assert.AreEqual( "6629fae49393a05397450978507c4ef1" , result.Response );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( ",d,g,h,ff,h,?," )]
        [DataRow( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , AuthorizationRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        
        [TestMethod]
        [DataRow( "Digest", "alice", "Streaming Server" ,"dcd98b7102dd2f0e8b11d0f600bfb0c093" ,"my domain", "my opaque data" ,"rtsp://127.0.0.1/channel/0" ,"CD29fae49393a05397450978507c4ef1" )]
        public void TestFormat(string type,string username,string realm,string nonce,string domain,string opaque,string uri, string response )
        {
            var header = new AuthorizationRtspHeader();
            
            Assert.AreEqual( "" , header.ToString() );

            header.Type = type;
            header.UserName = username;
            header.Realm = realm;
            header.Nonce = nonce;
            header.Domain = domain;
            header.Opaque = opaque;
            header.Uri = uri;
            header.Response = response;
            
            var format = string.Format( "{0} username=\"{1}\", realm=\"{2}\", nonce=\"{3}\", domain=\"{4}\", opaque=\"{5}\", uri=\"{6}\", response=\"{7}\"" ,
                type,
                username,
                realm,
                nonce,
                domain,
                opaque,
                uri,
                response
                );

            var result = header.ToString();

            Assert.AreEqual( format.Trim() , result.Trim() );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new AuthorizationRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            
            header.Type = "basic";
            header.Response = "a1c2a221d12a1d2a1d2";

            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
