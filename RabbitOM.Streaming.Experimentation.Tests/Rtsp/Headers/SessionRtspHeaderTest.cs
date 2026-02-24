using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class SessionRtspHeaderTest
    {
        [TestCase( "AF12BBF" , "AF12BBF")]
        [TestCase( "AF12BBF;" , "AF12BBF")]
        [TestCase( "AF12BBF;abc" , "AF12BBF")]
        [TestCase( "AF12BBF;abc=d" , "AF12BBF")]
        [TestCase( "abc=d;AF12BBF;" , "AF12BBF")]
        [TestCase( "abc=d; AF12BBF ;" , "AF12BBF")]
        [TestCase( "abc=d; 'AF12BBF' ;" , "AF12BBF")]
        [TestCase( "abc=d; ' AF12BBF ' ;" , "AF12BBF")]
        [TestCase( "abc=d; \" AF12BBF   \" ;" , "AF12BBF")]
        public void CheckParseSucceedWithNoTimeout(string input , string session )
        {
            Assert.IsTrue( SessionRtspHeader.TryParse( input , out var header ) );
            Assert.AreEqual( session , header.Identifier );
            Assert.IsNull( header.Timeout );
        }


        [TestCase( "AF12BBF;timeout=123" , "AF12BBF" , 123 )]
        [TestCase( "timeout=123;AF12BBF" , "AF12BBF" , 123 )]
        [TestCase( "timeout=123; AF12BBF " , "AF12BBF" , 123 )]
        [TestCase( " abc = d ; timeout='123'; 'AF12BBF' " , "AF12BBF" , 123 )]
        [TestCase( " abc = d ; timeout='d'; 'AF12BBF' ; timeout = \"123\" " , "AF12BBF" , 123 )]
        public void CheckParseSucceedWithTimeout(string input , string session , long timeout)
        {
            Assert.IsTrue( SessionRtspHeader.TryParse( input , out var header ) );
            Assert.AreEqual( session , header.Identifier );
            Assert.AreEqual( timeout , header.Timeout );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "," )]
        [TestCase( ",," )]
        [TestCase( ";;;; " )]
        [TestCase( " ; ; ; ;  " )]
        [TestCase( "timeout=123; , " )]
        public void CheckParseFailed( string input)
        {
            Assert.IsFalse( SessionRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckToString()
        {
            var header = new SessionRtspHeader();

            Assert.AreEqual( "" , header.ToString() );
            
            header.SetIdentifier( "  ABCDEF1234  " );
            Assert.AreEqual( "ABCDEF1234" , header.ToString() );

            header.Timeout = 123;
            Assert.AreEqual( "ABCDEF1234;timeout=123" , header.ToString() );
        }
    }
}
