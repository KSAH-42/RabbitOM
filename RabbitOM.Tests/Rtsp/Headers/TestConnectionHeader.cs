using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestConnectionHeader
    {
        [TestMethod]
        [DataRow("Close" , 1 )]
        [DataRow(" Close " , 1 )]
        [DataRow("  \n \t \r Close  " , 1 )]
        [DataRow("Close,Keep-Alive" , 2 )]
        [DataRow(" Close , Keep-Alive " , 2 )]
        [DataRow(" Close , , Keep-Alive " , 2 )]
        [DataRow(" Close ,, Keep-Alive " , 2 )]
        [DataRow(" Close ,, Keep-Alive1 " , 2 )]
        
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ConnectionRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Directives.Count );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , ConnectionRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddDirective( "Close" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddDirective( "Close" ) );
            Assert.AreEqual( "Close" , header.ToString() );

            Assert.AreEqual( true , header.TryAddDirective( "Keep-Alive" ) );
            Assert.AreEqual( "Close, Keep-Alive" , header.ToString() );

            Assert.AreEqual( false , header.TryAddDirective( " Keep-Alive " ) );
            Assert.AreEqual( "Close, Keep-Alive" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( 0 , header.Directives.Count );
            Assert.AreEqual( false , header.TryAddDirective( "") );
            Assert.AreEqual( false , header.TryAddDirective( "  ") );
            Assert.AreEqual( false , header.TryAddDirective( null ) );
            Assert.AreEqual( true , header.TryAddDirective( "Close") );
            Assert.AreEqual( 1 , header.Directives.Count );
            header.RemoveDirectives();
            Assert.AreEqual( 0 , header.Directives.Count );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ConnectionRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddDirective( "Close" ) );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
