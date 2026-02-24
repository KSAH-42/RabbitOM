using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Types
{
    [TestFixture]
    public class StringWithQualityTest
    {
        [TestCase( "*" , "*" , null )]
        [TestCase( "application" , "application" , null )]
        [TestCase( "application/text" , "application/text" , null )]
        [TestCase( "application/text;" , "application/text" , null )]
        [TestCase( "application/text;q=" , "application/text" , null )]
        [TestCase( "application/text;q=1" , "application/text" , 1 )]
        [TestCase( "application/text;q=1;" , "application/text" , 1 )]
        [TestCase( "q=1;application/text" , "application/text" , 1 )]
        public void CheckTryParseSucceed(string input , string name , double? quality )
        {
            Assert.IsTrue( StringWithQuality.TryParse( input , out var element ) );
            Assert.AreEqual( name , element.Name );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( " application " , "application" , null )]
        [TestCase( " application/text " , "application/text" , null )]
        [TestCase( " application/text;" , "application/text" , null )]
        [TestCase( " application/text ; q = " , "application/text" , null )]
        [TestCase( " application/text ; q = 1" , "application/text" , 1 )]
        [TestCase( " application/text ; q = 1;" , "application/text" , 1 )]
        [TestCase( " q=1 ; application/text " , "application/text" , 1 )]
        public void CheckTryParseSucceedWithSpaces(string input , string name , double? quality )
        {
            Assert.IsTrue( StringWithQuality.TryParse( input , out var element ) );
            Assert.AreEqual( name , element.Name );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( "'application'" , "application" , null )]
        [TestCase( "'application/text'" , "application/text" , null )]
        [TestCase( "'application/text';" , "application/text" , null )]
        [TestCase( "'application/text';q=" , "application/text" , null )]
        [TestCase( "'application/text';q=''" , "application/text" , null )]
        [TestCase( "'application/text';q='1'" , "application/text" , 1 )]
        [TestCase( "'application/text';q='1';" , "application/text" , 1 )]
        [TestCase( ";q='1';;'application/text'" , "application/text" , 1 )]
        public void CheckTryParseSucceedWithQuotes(string input , string name , double? quality )
        {
            Assert.IsTrue( StringWithQuality.TryParse( input , out var element ) );
            Assert.AreEqual( name , element.Name );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( "\"application\"" , "application" , null )]
        [TestCase( "\"application/text\"" , "application/text" , null )]
        [TestCase( "\"application/text\";" , "application/text" , null )]
        [TestCase( "\"application/text\";q=" , "application/text" , null )]
        [TestCase( "\"application/text\";q=\"\"" , "application/text" , null )]
        [TestCase( "\"application/text\";q=\"1\"" , "application/text" , 1 )]
        [TestCase( "\"application/text\";q=\"1\";" , "application/text" , 1 )]
        [TestCase( ";q=\"1\";;\"application/text\"" , "application/text" , 1 )]
        public void CheckTryParseSucceedWithDoubleQuotes(string input , string name , double? quality )
        {
            Assert.IsTrue( StringWithQuality.TryParse( input , out var element ) );
            Assert.AreEqual( name , element.Name );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( " \" application \"" , "application" , null )]
        [TestCase( " \" application/text \"" , "application/text" , null )]
        [TestCase( " \" application/text \";" , "application/text" , null )]
        [TestCase( " \" application/text \"; q=" , "application/text" , null )]
        [TestCase( " \" application/text \"; q= \"   \" " , "application/text" , null )]
        [TestCase( " \" application/text \"; q= \" 1 \"" , "application/text" , 1 )]
        [TestCase( " \" application/text \"; q= \" 1 \";" , "application/text" , 1 )]
        public void CheckTryParseSucceedWithDoubleQuotesAndSpaces(string input , string name , double? quality )
        {
            Assert.IsTrue( StringWithQuality.TryParse( input , out var element ) );
            Assert.AreEqual( name , element.Name );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "  " )]
        [TestCase( " ; " )]
        [TestCase( " ;q=1" )]
        public void CheckTryParseFailure(string input)
        {
            Assert.IsFalse( StringWithQuality.TryParse( input , out var element ) );
            Assert.IsNull( element );
        }
    }
}
