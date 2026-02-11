using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestContentLengthRtspHeader
    {
        [TestMethod]
        [DataRow("4" , 4 )]
        [DataRow("\"4\"" , 4 )]
        [DataRow("'4'" , 4 )]
        [DataRow(" 4 " , 4 )]
        [DataRow(" \"4\" " , 4 )]
        [DataRow(" '4' " , 4 )]
        public void ParseTestSucceed(string input , int valueToCompare )
        {
            if ( ! ContentLengthRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail(  "parse failed" );
            }

            Assert.AreEqual( valueToCompare , result.Value );
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
            Assert.AreEqual( false , ContentLengthRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat()
        {
            var header = new ContentLengthRtspHeader();

            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 123;

            Assert.AreEqual( "123" , header.ToString() );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ContentLengthRtspHeader();

            header.Value = - 1;

            Assert.AreEqual( false , header.TryValidate() );
            
            header.Value = 1;

            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
