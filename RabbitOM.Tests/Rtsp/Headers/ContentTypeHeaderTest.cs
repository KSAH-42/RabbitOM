using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class ContentTypeHeaderTest
    {
        [Test]
        [TestCase("multipart/form-data", 0 )]
        [TestCase("multipart/form-data ", 0 )]
        [TestCase("multipart/form-data ;", 0 )]
        [TestCase("multipart/form-data; boundary=ExampleBoundaryString", 1 )]
        [TestCase("multipart/form-data; boundary=ExampleBoundaryString;", 1 )]
        [TestCase("multipart/form-data;boundary=ExampleBoundaryString;", 1 )]
        [TestCase(" multipart/form-data ;", 0 )]
        [TestCase(" multipart/form-data ; boundary = ExampleBoundaryString ; ", 1 )]
        [TestCase(" multipart/form-data ; boundary = ExampleBoundaryString ; boundary = ExampleBoundaryString ; ", 1 )]
        [TestCase(" multipart/form-data ; boundary = ExampleBoundaryString ; boundary2 = ExampleBoundaryString2 ; ", 2 )]
        [TestCase(" \r \n multipart/form-data ; boundary = ExampleBoundaryString ; boundary2 = ExampleBoundaryString2 ; ", 2 )]
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

        [Test]
        [TestCase( "  ,  " )]
        [TestCase( "    " )]
        [TestCase( "" )]
        [TestCase( null )]
        [TestCase( ";;;;;;;;" )]
        [TestCase( " ; ; ; ; ; ; ; ;" )]
        [TestCase( ",,,,,,," )]
        [TestCase( " , , , , , , , " )]
        public void ParseTestFailed( string input )
        {
            Assert.IsFalse(  ContentTypeRtspHeader.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }

        [Test]
        public void TestFormat1()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( 0 , header.ToString().Length );
            Assert.IsTrue(  header.TryAddParameter( "a" , "b" ) );
            Assert.IsTrue(  header.TryAddParameter( "b" , "b" ) );
            Assert.AreEqual( 0 , header.ToString().Length );
            header.MediaType = "application/text";
            Assert.AreNotEqual( 0 , header.ToString().Length );
        }

        [Test]
        public void TestFormat2()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( "" , header.ToString() );

            header.MediaType = " application/text ";
            Assert.AreEqual( "application/text" , header.ToString() );

            Assert.IsTrue(  header.TryAddParameter( " a " , " aa " ) );
            Assert.AreEqual( "application/text; a=aa" , header.ToString() );

            Assert.IsFalse(  header.TryAddParameter( " a " , " aa " ) );
            Assert.AreEqual( "application/text; a=aa" , header.ToString() );

            Assert.IsTrue(  header.TryAddParameter( " b " , " bb " ) );
            Assert.AreEqual( "application/text; a=aa; b=bb" , header.ToString() );
        }

        [Test]
        public void TestCollection()
        {
            var header = new ContentTypeRtspHeader();

            Assert.AreEqual( 0 , header.Parameters.Count );
            Assert.IsTrue(  header.TryAddParameter( "a" , "a" ) );
            Assert.IsTrue(  header.TryAddParameter( "b" , "a" ) );
            Assert.IsFalse(  header.TryAddParameter( "b" , "a" ) );
            header.AddParameter( "c" , "c" );
            Assert.AreEqual( 3 , header.Parameters.Count );
            header.RemoveParameters();
            Assert.AreEqual( 0 , header.Parameters.Count );
            Assert.Throws<ArgumentNullException>( () => header.AddParameter( null , "d" ) );
        }

        [Test]
        public void TestValidation()
        {
            var header = new ContentTypeRtspHeader();
            Assert.IsFalse(  header.TryValidate() );
            
            header.MediaType = "   ";
            Assert.IsFalse(  header.TryValidate() );

            header.MediaType = "  text  ";
            Assert.IsTrue(  header.TryValidate() );
        }
    }
}
