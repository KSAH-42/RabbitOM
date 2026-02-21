using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TransportRtspHeaderTest
    {
        [TestCase( "source='ef';ab;ttl='1';cd;" )]
        public void CheckParseSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( "ef" , header.Source );
            Assert.AreEqual( 1 , header.TTL );
        }

        [TestCase( "ab;cd;source='ef'" )]
        public void CheckParseSourceSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( "ef" , header.Source );
        }

        [TestCase( "ab;cd;destination='ef'" )]
        public void CheckParseDestinationSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( "ef" , header.Destination );
        }

        [TestCase( "ab;cd;ssrc='ef'" )]
        public void CheckParseSSRCSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( "ef" , header.SSRC );
        }

        [TestCase( "ab;cd;mode='ef'" )]
        public void CheckParseModeSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( "ef" , header.Mode );
        }

        [TestCase( "ab;cd;ttl='12'" )]
        public void CheckParseTtlSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( 12 , header.TTL );
        }

        [TestCase( "ab;cd;layers='12'" )]
        public void CheckParseLayersSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( 12 , header.Layers );
        }

        [TestCase( "ab;cd;client_port='1-2'" )]
        public void CheckParseClientPortSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( 1 , header.ClientPortStart );
            Assert.AreEqual( 2 , header.ClientPortEnd );
        }

        [TestCase( "ab;cd;server_port='1-2'" )]
        public void CheckParseServerPortSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( 1 , header.ServerPortStart );
            Assert.AreEqual( 2 , header.ServerPortEnd );
        }

        [TestCase( "ab;cd;port='1-2'" )]
        public void CheckParsePortSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( 1 , header.PortStart );
            Assert.AreEqual( 2 , header.PortEnd );
        }

        [TestCase( "ab;cd;interleaved='1-2'" )]
        public void CheckParseInterleavedSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "ab" , header.Transport );
            Assert.AreEqual( "cd" , header.Transmission );
            Assert.AreEqual( 1 , header.InterleavedStart );
            Assert.AreEqual( 2 , header.InterleavedEnd );
        }
    }
}
