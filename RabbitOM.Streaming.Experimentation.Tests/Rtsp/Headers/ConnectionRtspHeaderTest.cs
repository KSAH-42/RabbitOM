using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class ConnectionRtspHeaderTest
    {
        [TestCase( "closed" , 1 )]
        [TestCase( "closed, connected" , 2 )]
        [TestCase( "closed, connected, error" , 3 )]
       
        public void CheckTryParseSucceed( string input , long count )
        {
            Assert.IsTrue( ConnectionRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( count , header.Directives.Count );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( ConnectionRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckAddDirectives()
        {
            var header = new ConnectionRtspHeader();

            Assert.IsEmpty( header.Directives );
            Assert.IsTrue( header.AddDirective( "connected" ) );
            Assert.IsTrue( header.AddDirective( "closed" ) );
            Assert.AreEqual( 2 , header.Directives.Count );
            Assert.IsFalse( header.AddDirective( "closed" ) );
        }

        [Test]
        public void CheckRemoveDirectives()
        {
            var header = new ConnectionRtspHeader();

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
            var header = new ContentLengthRtspHeader();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 0;
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 1;
            Assert.AreEqual( "1" , header.ToString() );

            header.Value = long.MaxValue;
            Assert.AreEqual( long.MaxValue.ToString() , header.ToString() );
        }
    }
}
