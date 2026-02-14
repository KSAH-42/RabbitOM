using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class TestAcceptRangesHeader
    {
        [Test]
        [TestCase("bytes,date,double" , 3 )]
        [TestCase(" bytes , date , double " , 3 )]
        [TestCase("bytes,date,\r\ndouble" , 3 )]
        [TestCase("bytes,date, " , 2 )]
        [TestCase("bytes,, " , 1 )]
        [TestCase("bytes1" , 1 )]
        [TestCase(" bytes " , 1 )]
        [TestCase(" * " , 1 )]
        [TestCase("*" , 1 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptRangesRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Units.Count );
        }

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( "12" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  AcceptRangesRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddUnit( "bytes" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.IsTrue(  header.TryAddUnit( "bytes" ) );
            Assert.AreEqual( "bytes" , header.ToString() );

            Assert.IsTrue(  header.TryAddUnit( "dates" ) );
            Assert.AreEqual( "bytes, dates" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( 0 , header.Units.Count );
            Assert.IsFalse(  header.TryAddUnit( "") );
            Assert.IsFalse(  header.TryAddUnit( "  ") );
            Assert.IsFalse(  header.TryAddUnit( null ) );
            Assert.IsTrue(  header.TryAddUnit( "bytes") );
            Assert.AreEqual( 1 , header.Units.Count );
            header.RemoveUnits();
            Assert.AreEqual( 0 , header.Units.Count );
        }

        [Test]
        public void TestValidation()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.IsFalse(  header.TryValidate() );
            Assert.IsTrue(  header.TryAddUnit( "bytes" ) );
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
