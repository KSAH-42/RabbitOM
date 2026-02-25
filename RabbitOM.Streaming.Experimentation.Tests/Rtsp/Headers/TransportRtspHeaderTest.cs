using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    [TestFixture]
    public class TransportRtspHeaderTest
    {
        [TestCase( "RTP/AVP/UDP;unicast;" )]
        [TestCase( "RTP/AVP/UDP;unicast" )]
        [TestCase( " RTP/AVP/UDP ; unicast ;" )]
        [TestCase( "  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
        }

        [TestCase( "RTP/AVP/UDP;unicast;source='my source'" )]
        [TestCase( "RTP/AVP/UDP;unicast;Source=my source" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; source=my source ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; source=my source " )]
        [TestCase( " source=my source ;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseSourceSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "my source" , header.Source );
        }

        [TestCase( "RTP/AVP/UDP;unicast;destination='my destination'" )]
        [TestCase( "RTP/AVP/UDP;unicast;destination=my destination" )]
        [TestCase( "RTP/AVP/UDP;unicast;Destination=my destination" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; destination=my destination ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; destination=my destination " )]
        [TestCase( " destination=my destination ;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseDestinationSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "my destination" , header.Destination );
        }

        [TestCase( "RTP/AVP/UDP;unicast;address='my address'" )]
        [TestCase( "RTP/AVP/UDP;unicast;address=my address" )]
        [TestCase( "RTP/AVP/UDP;unicast;Address=my address" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; address=my address ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; address=my address " )]
        [TestCase( " address=my address;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseAddressSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "my address" , header.Address );
        }

        [TestCase( "RTP/AVP/UDP;unicast;host='my host'" )]
        [TestCase( "RTP/AVP/UDP;unicast;host=my host" )]
        [TestCase( "RTP/AVP/UDP;unicast;Host=my host" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; host=my host ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; host=my host " )]
        [TestCase( " host=my host;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseHostSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "my host" , header.Host );
        }

        [TestCase( "RTP/AVP/UDP;unicast;ssrc='my ssrc'" )]
        [TestCase( "RTP/AVP/UDP;unicast;ssrc=my ssrc" )]
        [TestCase( "RTP/AVP/UDP;unicast;ssrc=my ssrc" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; ssrc=my ssrc ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; ssrc=my ssrc " )]
        [TestCase( " ssrc=my ssrc ;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseSSRCSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "my ssrc" , header.SSRC );
        }

        [TestCase( "RTP/AVP/UDP;unicast;mode='my mode'" )]
        [TestCase( "RTP/AVP/UDP;unicast;Mode='my mode'" )]
        [TestCase( "RTP/AVP/UDP;unicast;mode=my mode" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; mode=my mode ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; mode=my mode " )]
        [TestCase( " mode=my mode ;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseModeSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "my mode" , header.Mode );
        }

        [TestCase( "RTP/AVP/UDP;unicast;ttl='12'" )]
        [TestCase( "RTP/AVP/UDP;unicast;ttl=12" )]
        [TestCase( "RTP/AVP/UDP;unicast;TTL=12" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; ttl=12 ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; ttl= 12 " )]
        [TestCase( " ttl=12 ;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseTTLSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 12 , header.TTL );
        }

        [TestCase( "RTP/AVP/UDP;unicast;layers='12'" )]
        [TestCase( "RTP/AVP/UDP;unicast;layers=12" )]
        [TestCase( "RTP/AVP/UDP;unicast;layerS=12" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; layers=12 ;" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; layers= 12 " )]
        [TestCase( " layers=12 ;  ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseLayersSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 12 , header.Layers );
        }

        [TestCase( "RTP/AVP/UDP;unicast;client_port=1-2" )]
        [TestCase( "RTP/AVP/UDP;unicast;client_poRt=1-2" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; client_port = 1-2 " )]
        [TestCase( " RTP/AVP/UDP ; unicast ; client_port = '1-2' " )]
        [TestCase( "  client_port = 1-2 ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseClientPortSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 1 , header.ClientPort.Value.Minimum );
            Assert.AreEqual( 2 , header.ClientPort.Value.Maximum );
        }

        [TestCase( "RTP/AVP/UDP;unicast;server_port=1-2" )]
        [TestCase( "RTP/AVP/UDP;unicast;server_pOrt=1-2" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; server_port  = 1-2 " )]
        [TestCase( " RTP/AVP/UDP ; unicast ; server_port  = '1-2' " )]
        [TestCase( "  server_port = 1-2 ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseServerPortSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 1 , header.ServerPort.Value.Minimum );
            Assert.AreEqual( 2 , header.ServerPort.Value.Maximum );
        }

        [TestCase( "RTP/AVP/UDP;unicast;port=1-2" )]
        [TestCase( "RTP/AVP/UDP;unicast;Port=1-2" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; port  = 1-2 " )]
        [TestCase( " RTP/AVP/UDP ; unicast ; port  = '1-2' " )]
        [TestCase( "  port = 1-2 ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParsePortSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 1 , header.Port.Value.Minimum );
            Assert.AreEqual( 2 , header.Port.Value.Maximum );
        }

        [TestCase( "RTP/AVP/UDP;unicast;interleaved=1-2" )]
        [TestCase( "RTP/AVP/UDP;unicast;interLeaved=1-2" )]
        [TestCase( " RTP/AVP/UDP ; unicast ; interleaved  = 1-2 " )]
        [TestCase( " RTP/AVP/UDP ; unicast ; interleaved  = '1-2' " )]
        [TestCase( "  interleaved = 1-2 ; ;   RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseInterleavedSucceed( string input )
        {
            Assert.IsTrue( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 1 , header.Interleaved.Value.Minimum );
            Assert.AreEqual( 2 , header.Interleaved.Value.Maximum );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( ";;;;;;" )]
        [TestCase( "A/B/C;unicast;interLeaved=1-2" )]
        [TestCase( "RTP/AVP/UDP;digitalcast;interLeaved=1-2" )]
        [TestCase( "RTP/AVP/UDP;partialcast;interLeaved=1-2" )]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( TransportRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithSource()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.SetSource( "abc");
            Assert.AreEqual( "RTP/AVP/UDP;unicast;source=abc" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithDestination()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.SetDestination( "abc");
            Assert.AreEqual( "RTP/AVP/UDP;unicast;destination=abc" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithAddress()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.SetAddress( "abc");
            Assert.AreEqual( "RTP/AVP/UDP;unicast;address=abc" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithHost()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.SetHost( "abc");
            Assert.AreEqual( "RTP/AVP/UDP;unicast;host=abc" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithSSRC()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.SetSSRC( "abc");
            Assert.AreEqual( "RTP/AVP/UDP;unicast;ssrc=abc" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithMode()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.SetMode( "abc");
            Assert.AreEqual( "RTP/AVP/UDP;unicast;mode=\"abc\"" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithTTL()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.TTL = 123;
            Assert.AreEqual( "RTP/AVP/UDP;unicast;ttl=123" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithLayers()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.Layers = 123;
            Assert.AreEqual( "RTP/AVP/UDP;unicast;layers=123" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithPort()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.Port = new PortRange( 1 , 2 );
            Assert.AreEqual( "RTP/AVP/UDP;unicast;port=1-2" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithClientPort()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.ClientPort = new PortRange( 1 , 2 );
            Assert.AreEqual( "RTP/AVP/UDP;unicast;client_port=1-2" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithServerPort()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.ServerPort = new PortRange( 1 , 2 );
            Assert.AreEqual( "RTP/AVP/UDP;unicast;server_port=1-2" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithInterleaved()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            header.Interleaved = new InterleavedRange( 1 , 2 );
            Assert.AreEqual( "RTP/AVP/UDP;unicast;interleaved=1-2" , header.ToString() );
        }

        [Test]
        public void CheckToStringWithExtensions()
        {
            var header = new TransportRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.SetTransport( "RTP/AVP/UDP" );
            header.SetTransmission( "unicast" );

            Assert.AreEqual( "RTP/AVP/UDP;unicast" , header.ToString() );

            Assert.IsTrue( header.AddExtension( "a") );
            Assert.IsTrue( header.AddExtension( "b") );
            Assert.IsTrue( header.AddExtension( "c") );

            Assert.AreEqual( "RTP/AVP/UDP;unicast;a;b;c" , header.ToString() );

            Assert.IsTrue( header.RemoveExtension( "c") );

            Assert.AreEqual( "RTP/AVP/UDP;unicast;a;b" , header.ToString() );
        }
    }
}
