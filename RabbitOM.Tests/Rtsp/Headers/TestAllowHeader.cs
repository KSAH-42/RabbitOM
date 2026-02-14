using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestAllowHeader
    {
        [Test]
        [TestCase("OPTIONS, DESCRIBE, SETUP, PLAY, PAUSE, TEARDOWN, KEEPALIVE, GET_PARAMETER, SET_PARAMETER, ANNOUNCE, REDIRECT, RECORD, ABC, EDF" , 12 )]
        [TestCase("  OPTiONS  ,\tdESCRIBE, SEtUP, " , 3 )]
        [TestCase(" \r OPTiONS , \n dESCRIBE, SEtUP, " , 3 )]
        [TestCase("  OPTiONS  " , 1 )]
        [TestCase("OPTIONS" , 1 )]
        [TestCase("??,OPTIONS,??" , 1 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AllowRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Methods.Count );
        }

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( ",d,g,h,ff,h,?," )]
        [TestCase( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  AllowRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.IsTrue(  header.TryAddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( "OPTIONS" , header.ToString() );

            Assert.IsTrue(  header.TryAddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreEqual( "OPTIONS, DESCRIBE" , header.ToString() );

            Assert.IsTrue(  header.TryAddMethod( RtspMethod.SETUP ) );
            Assert.AreEqual( "OPTIONS, DESCRIBE, SETUP" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new AllowRtspHeader();
 
            Assert.AreEqual( 0 , header.Methods.Count );
            Assert.IsTrue(  header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.IsTrue(  header.TryAddMethod( RtspMethod.DESCRIBE ) );
            Assert.IsFalse(  header.TryAddMethod( RtspMethod.DESCRIBE ) );
            header.AddMethod( RtspMethod.SETUP  );
            Assert.AreEqual( 3 , header.Methods.Count );
            header.RemoveMethods();
            Assert.AreEqual( 0 , header.Methods.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddMethod( null ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new AllowRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddMethod( RtspMethod.OPTIONS ) );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
