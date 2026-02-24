using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class UserAgentRtspHeaderTest
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
        public void CheckParseSuccee( string input , string product , string version , string comment)
        {
            Assert.IsTrue( UserAgentRtspHeader.TryParse( input, out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( product , header.Product );
            Assert.AreEqual( version , header.Version );
            Assert.AreEqual( comment , header.Comments );
        }
    }
}
