using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class ConferenceRtspHeaderTest
    {
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;" )]
        [TestCase( "1234567879;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        [TestCase( ";  unicast ; ; RTP/AVP/UDP ; unicast ; 1234567879 " )]
        [TestCase( ";  unicast ; ; RTP/AVP/UDP ; 1234567879 ; unicast ; " )]
        public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;source=mysource" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Source=mysource" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;source=mysource" )]
        [TestCase( "1234567879; source=mysource;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseSourceSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "mysource" , header.Source );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;destination=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Destination=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;destination=abc" )]
        [TestCase( "1234567879; destination=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseDestinationSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Destination );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;address=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Address=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;address=abc" )]
        [TestCase( "1234567879; address=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseAddressSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Address );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;host=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Host=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;host=abc" )]
        [TestCase( "1234567879; host=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseHostSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Host );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;role=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Role=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;role=abc" )]
        [TestCase( "1234567879; role=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseRoleSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Role );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;mode=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Mode=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;mode=abc" )]
        [TestCase( "1234567879; mode=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseModeSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Mode );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;tag=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Tag=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;tag=abc" )]
        [TestCase( "1234567879; tag=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseTagSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Tag );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;session=abc" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Session=abc" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;session=abc" )]
        [TestCase( "1234567879; session=abc;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseSessionSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( "abc" , header.Session );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;ttl=123" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;TTL=123" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;ttl=123" )]
        [TestCase( "1234567879; ttl=123;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParseTTLSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 123 , header.TTL );
        }

        [TestCase( "1234567879;RTP/AVP/UDP;unicast;port=1-23" )]
        [TestCase( "1234567879;RTP/AVP/UDP;unicast;Port=1-23" )]
        [TestCase( "1234567879; RTP/AVP/UDP ; unicast ;port=1-23" )]
        [TestCase( "1234567879; port=1-23;  unicast ; ; RTP/AVP/UDP ; unicast ; " )]
        public void CheckTryParsePortSucceed( string input )
        {
            Assert.IsTrue( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( "1234567879" , header.ConferenceId );
            Assert.AreEqual( "RTP/AVP/UDP" , header.Transport );
            Assert.AreEqual( "unicast" , header.Transmission );
            Assert.AreEqual( 1 , header.Port.Value.Minimum );
            Assert.AreEqual( 23 , header.Port.Value.Maximum );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( ";;;;;;" )]
        [TestCase( "\n\r;" )]
        [TestCase( " ,,,,,,," )]
        [TestCase( " :::::::: " )]
        [TestCase( " !:;? " )]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( ConferenceRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
