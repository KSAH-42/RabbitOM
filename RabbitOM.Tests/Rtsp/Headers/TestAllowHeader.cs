using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestAllowHeader
    {
        [TestMethod]
        [DataRow("OPTIONS, DESCRIBE, SETUP, PLAY, PAUSE, TEARDOWN, KEEPALIVE, GET_PARAMETER, SET_PARAMETER, ANNOUNCE, REDIRECT, RECORD, ABC, EDF" , 12 )]
        [DataRow("  OPTiONS , dESCRIBE, SEtUP, " , 3 )]
        [DataRow(" \r OPTiONS , \n dESCRIBE, SEtUP, " , 3 )]
        [DataRow("  OPTiONS  " , 1 )]
        [DataRow("OPTIONS" , 1 )]
        [DataRow("??,OPTIONS,??" , 1 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AllowRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Methods.Count );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( ",d,g,h,ff,h,?," )]
        [DataRow( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , AllowRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( "OPTIONS" , header.ToString() );

            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreEqual( "OPTIONS, DESCRIBE" , header.ToString() );

            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.SETUP ) );
            Assert.AreEqual( "OPTIONS, DESCRIBE, SETUP" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( 0 , header.Methods.Count );
            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreEqual( false , header.TryAddMethod( RtspMethod.DESCRIBE ) );
            header.AddMethod( RtspMethod.SETUP  );
            Assert.AreEqual( 3 , header.Methods.Count );
            header.RemoveMethods();
            Assert.AreEqual( 0 , header.Methods.Count );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddMethod( null ) );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
