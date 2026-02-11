using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestAcceptRangesHeader
    {
        [TestMethod]
        [DataRow("bytes,date,double" , 3 )]
        [DataRow(" bytes , date , double " , 3 )]
        [DataRow("bytes,date,\r\ndouble" , 3 )]
        [DataRow("bytes,date, " , 2 )]
        [DataRow("bytes,, " , 1 )]
        [DataRow("bytes1" , 1 )]
        [DataRow(" bytes " , 1 )]
        [DataRow(" * " , 1 )]
        [DataRow("*" , 1 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! AcceptRangesRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( nbElement , result.Units.Count );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( "12" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , AcceptRangesRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddUnit( "bytes" ) );
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            Assert.AreEqual( true , header.TryAddUnit( "bytes" ) );
            Assert.AreEqual( "bytes" , header.ToString() );

            Assert.AreEqual( true , header.TryAddUnit( "dates" ) );
            Assert.AreEqual( "bytes, dates" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( 0 , header.Units.Count );
            Assert.AreEqual( false , header.TryAddUnit( "") );
            Assert.AreEqual( false , header.TryAddUnit( "  ") );
            Assert.AreEqual( false , header.TryAddUnit( null ) );
            Assert.AreEqual( true , header.TryAddUnit( "bytes") );
            Assert.AreEqual( 1 , header.Units.Count );
            header.RemoveUnits();
            Assert.AreEqual( 0 , header.Units.Count );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new AcceptRangesRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            Assert.AreEqual( true , header.TryAddUnit( "bytes" ) );
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
