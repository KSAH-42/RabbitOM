using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers;

    [TestFixture]
    public class UserAgentRtspHeaderValueTest
    {
        [TestCase( "productA/1.1 (mycomments)" , "productA" , "1.1" , "mycomments" ) ]
        [TestCase( "productA/1.1 (my comments)" , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "productA/1.1 ((my comments))" , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "productA/1.1 ((my comments)" , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "productA/1.1 (my comments))" , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "productA/1.1 (my comments)" , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "  (my comments)  productA/1.1 " , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "  (my comments)  productA / 1.1 " , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "  (my comments) my data test productA/1.1 " , "productA" , "1.1" , "my comments" ) ]
        [TestCase( "LibVLC/3.0.12" , "LibVLC" , "3.0.12" , "" )]
        public void CheckTryParseSucceed( string input , string product , string version , string comment)
        {
            Assert.IsTrue( UserAgentRtspHeaderValue.TryParse( input, out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( product , header.Product );
            Assert.AreEqual( version , header.Version );
            Assert.AreEqual( comment , header.Comment );
        }

        [TestCase( null )]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("LibVLC")]
        [TestCase("3.0.12")]
        [TestCase("/3.0.12")]
        [TestCase("/")]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( UserAgentRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }
    }
}
