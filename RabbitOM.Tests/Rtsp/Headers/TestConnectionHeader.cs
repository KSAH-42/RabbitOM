using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestConnectionHeader
    {
        [Test]
        [TestCase("Close" , 1 )]
        [TestCase(" Close " , 1 )]
        [TestCase("  \n \t \r Close  " , 1 )]
        [TestCase("Close,Keep-Alive" , 2 )]
        [TestCase(" Close , Keep-Alive " , 2 )]
        [TestCase(" Close , , Keep-Alive " , 2 )]
        [TestCase(" Close ,, Keep-Alive " , 2 )]
        [TestCase(" Close ,, Keep-Alive1 " , 2 )]
        
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ConnectionRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Directives.Count );
        }

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  ConnectionRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddDirective( "Close" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddDirective( "Close" ) );
            Assert.AreEqual( "Close" , header.ToString() );

            Assert.IsTrue(  header.TryAddDirective( "Keep-Alive" ) );
            Assert.AreEqual( "Close, Keep-Alive" , header.ToString() );

            Assert.IsFalse(  header.TryAddDirective( " Keep-Alive " ) );
            Assert.AreEqual( "Close, Keep-Alive" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( 0 , header.Directives.Count );
            Assert.IsFalse(  header.TryAddDirective( "") );
            Assert.IsFalse(  header.TryAddDirective( "  ") );
            Assert.IsFalse(  header.TryAddDirective( null ) );
            Assert.IsTrue(  header.TryAddDirective( "Close") );
            Assert.AreEqual( 1 , header.Directives.Count );
            header.RemoveDirectives();
            Assert.AreEqual( 0 , header.Directives.Count );
        }

        [Test]
        public void TestValidation()
        {
            var header = new ConnectionRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddDirective( "Close" ) );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
