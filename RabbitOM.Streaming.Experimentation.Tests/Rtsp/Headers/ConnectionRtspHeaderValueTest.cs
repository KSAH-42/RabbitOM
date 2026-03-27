using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers;

    [TestFixture]
    public class ConnectionRtspHeaderValueTest
    {
        [TestCase( "closed" , 1 )]
        [TestCase( "closed, connected" , 2 )]
        [TestCase( "closed, connected, error" , 3 )]
       
        public void CheckTryParseSucceed( string input , long count )
        {
            Assert.IsTrue( ConnectionRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( count , header.Directives.Count );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( ConnectionRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [TestCase("connected")]
        [TestCase(" connected ")]
        [TestCase("keep-alive")]
        [TestCase("keep_alive")]
        [TestCase(" keep-alive" )]
        [TestCase(" keep_alive" )]
        public void CheckAddDirectivesSucceed(string input)
        {
            var header = new ConnectionRtspHeaderValue();

            Assert.IsEmpty( header.Directives );
            Assert.IsTrue( header.AddDirective( input ) );
            Assert.AreEqual( 1 , header.Directives.Count );
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("?")]
        [TestCase(",")]
        [TestCase(",,")]
        [TestCase(";")]
        [TestCase(";;")]
        [TestCase("-connected")]
        [TestCase("connected-" )]
        [TestCase("keep?alive" )]
        [TestCase("keep/alive" )]
        public void CheckAddDirectivesFailed( string input )
        {
            var header = new ConnectionRtspHeaderValue();

            Assert.IsEmpty( header.Directives );
            Assert.IsFalse( header.AddDirective( input ) );
            Assert.AreEqual( 0 , header.Directives.Count );
        }

        [Test]
        public void CheckRemoveDirectives()
        {
            var header = new ConnectionRtspHeaderValue();
            
            Assert.IsEmpty( header.Directives );
            
            Assert.IsFalse( header.RemoveDirective( "abc" ) );

            Assert.IsTrue( header.AddDirective( "connected" ) );
            Assert.IsTrue( header.AddDirective( "closed" ) );
            Assert.IsTrue( header.AddDirective( "failed" ) );

            Assert.IsTrue( header.RemoveDirective( "closed" ) );
            Assert.IsTrue( header.RemoveDirective( "connected" ) );

            Assert.IsFalse( header.RemoveDirective( "connected" ) );

            header.RemoveDirectives();

            Assert.IsEmpty( header.Directives );
        }


        [Test]
        public void CheckToString()
        {
            var header = new ConnectionRtspHeaderValue();

            Assert.AreEqual( "" , header.ToString() );

            header.AddDirective( "closed" );
            Assert.AreEqual( "closed" , header.ToString() );

            header.AddDirective( "error" );
            Assert.AreEqual( "closed, error" , header.ToString() );

            header.AddDirective( "keep-alive" );
            Assert.AreEqual( "closed, error, keep-alive" , header.ToString() );
        }
    }
}
