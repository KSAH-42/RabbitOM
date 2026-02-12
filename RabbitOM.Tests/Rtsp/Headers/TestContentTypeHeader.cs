using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestClass]
    public class TestContentTypeHeader
    {
        [TestMethod]
        [DataRow("multipart/form-data", 0 )]
        [DataRow("multipart/form-data ", 0 )]
        [DataRow("multipart/form-data ;", 0 )]
        [DataRow("multipart/form-data; boundary=ExampleBoundaryString", 1 )]
        [DataRow("multipart/form-data; boundary=ExampleBoundaryString;", 1 )]
        [DataRow("multipart/form-data;boundary=ExampleBoundaryString;", 1 )]
        [DataRow(" multipart/form-data ;", 0 )]
        [DataRow(" multipart/form-data ; boundary = ExampleBoundaryString ; ", 1 )]
        [DataRow(" multipart/form-data ; boundary = ExampleBoundaryString ; boundary = ExampleBoundaryString ; ", 1 )]
        [DataRow(" multipart/form-data ; boundary = ExampleBoundaryString ; boundary2 = ExampleBoundaryString2 ; ", 2 )]
        [DataRow(" \r \n multipart/form-data ; boundary = ExampleBoundaryString ; boundary2 = ExampleBoundaryString2 ; ", 2 )]
        public void ParseTestSucceed(string input , int nbElement )
        {
            if ( ! ContentTypeRtspHeader.TryParse( input , out var result ) )
            {
                Assert.Fail( "parse failed" );
            }

            Assert.IsNotNull( result );
            Assert.AreEqual( "multipart/form-data" , result.MediaType );
            Assert.AreEqual( nbElement , result.Parameters.Count );
        }

        [TestMethod]
        [DataRow( "  ,  " )]
        [DataRow( "    " )]
        [DataRow( "" )]
        [DataRow( null )]
        [DataRow( ";;;;;;;;" )]
        [DataRow( " ; ; ; ; ; ; ; ;" )]
        [DataRow( ",,,,,,," )]
        [DataRow( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.AreEqual( false , ContentTypeRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestFormat1()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.AreEqual( true , header.TryAddParameter( "a" , "b" ) );
            Assert.AreEqual( true , header.TryAddParameter( "b" , "b" ) );
            Assert.AreEqual( 0 , header.ToString().Length );
            header.MediaType = "application/text";
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [TestMethod]
        public void TestFormat2()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.MediaType = " application/text ";
            Assert.AreEqual( "application/text" , header.ToString() );

            Assert.AreEqual( true , header.TryAddParameter( " a " , " aa " ) );
            Assert.AreEqual( "application/text; a=aa" , header.ToString() );

            Assert.AreEqual( false , header.TryAddParameter( " a " , " aa " ) );
            Assert.AreEqual( "application/text; a=aa" , header.ToString() );

            Assert.AreEqual( true , header.TryAddParameter( " b " , " bb " ) );
            Assert.AreEqual( "application/text; a=aa; b=bb" , header.ToString() );
        }

        [TestMethod]
        public void TestCollection()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( 0 , header.Parameters.Count );
            Assert.AreEqual( true , header.TryAddParameter( "a" , "a" ) );
            Assert.AreEqual( true , header.TryAddParameter( "b" , "a" ) );
            Assert.AreEqual( false , header.TryAddParameter( "b" , "a" ) );
            header.AddParameter( "c" , "c" );
            Assert.AreEqual( 3 , header.Parameters.Count );
            header.RemoveParameters();
            Assert.AreEqual( 0 , header.Parameters.Count );
            Assert.ThrowsException<ArgumentNullException>( () => header.AddParameter( null , "d" ) );
        }

        [TestMethod]
        public void TestValidation()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( false , header.TryValidate() );
            header.MediaType = "   ";
            Assert.AreEqual( false , header.TryValidate() );

            header.MediaType = "  text  ";
            Assert.AreEqual( true , header.TryValidate() );
        }
    }
}
