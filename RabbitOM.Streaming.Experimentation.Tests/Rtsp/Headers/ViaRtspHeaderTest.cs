using NUnit.Framework;
using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    [TestFixture]
    public class ViaRtspHeaderTest
    {
        [TestCase( "RTSP/1.0 proxy1.example.com:8554 (Proxy/3.1)" ) ]
        public void CheckParseSucceed( string input )
        {
            Assert.IsTrue( ViaRtspHeader.TryParse( input, out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( 1 , header.Proxies.Count );
            Assert.AreEqual( "RTSP" , header.Proxies.First().Protocol );
            Assert.AreEqual( "1.0" , header.Proxies.First().Version );
            Assert.AreEqual( "proxy1.example.com:8554" , header.Proxies.First().ReceivedBy );
            Assert.AreEqual( "Proxy/3.1" , header.Proxies.First().Comments );
        }

        [TestCase( "RTSP/1.0 proxy1.example.com:8554 (Proxy/3.1), HTTP/1.1 proxy2.example.com:8554 (Proxy/4.1)" ) ]
        public void CheckParseSucceedMultiples( string input )
        {
            Assert.IsTrue( ViaRtspHeader.TryParse( input, out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( 2 , header.Proxies.Count );
            Assert.AreEqual( "RTSP" , header.Proxies.First().Protocol );
            Assert.AreEqual( "1.0" , header.Proxies.First().Version );
            Assert.AreEqual( "proxy1.example.com:8554" , header.Proxies.First().ReceivedBy );
            Assert.AreEqual( "Proxy/3.1" , header.Proxies.First().Comments );

            Assert.AreEqual( "HTTP" , header.Proxies.Skip(1).First().Protocol );
            Assert.AreEqual( "1.1" , header.Proxies.Skip(1).First().Version );
            Assert.AreEqual( "proxy2.example.com:8554" , header.Proxies.Skip(1).First().ReceivedBy );
            Assert.AreEqual( "Proxy/4.1" , header.Proxies.Skip(1).First().Comments );
        }

        [Test]
        public void CheckAddProxies()
        {
            var header = new ViaRtspHeader();

            Assert.IsEmpty( header.Proxies );

            Assert.IsTrue( header.AddProxy( new ProxyInfo( "RTSP" , "1.0" , "a.b.c" , "some comment" )));
            Assert.AreEqual( 1 , header.Proxies.Count );

            Assert.IsTrue( header.AddProxy( new ProxyInfo( "HTTP" , "1.1" , "a.b.c.d" , "some comment" )));
            Assert.AreEqual( 2 , header.Proxies.Count );

            Assert.IsFalse( header.AddProxy( null ) );
        }

        [Test]
        public void CheckRemoveProxies()
        {
            var header = new ViaRtspHeader();

            Assert.IsEmpty( header.Proxies );

            Assert.IsTrue( header.AddProxy( new ProxyInfo( "RTSP" , "1.0" , "a.b.c" , "some comment" )));
            Assert.IsTrue( header.AddProxy( new ProxyInfo( "RTSP" , "1.0" , "a.b.c" , "some comment" )));
            Assert.IsTrue( header.RemoveProxy( header.Proxies.First() ) );
            Assert.IsFalse( header.RemoveProxy( null ) );
            Assert.AreEqual( 1 , header.Proxies.Count );
            header.RemoveProxies();
            Assert.IsEmpty( header.Proxies );
        }
    }
}
