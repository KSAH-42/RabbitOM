using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class WeightedStringTest
    {
        [TestCase( "*" , "*" , null )]
        [TestCase( "application" , "application" , null )]
        [TestCase( "application/text" , "application/text" , null )]
        [TestCase( "application/text;" , "application/text" , null )]
        [TestCase( "application/text;q=" , "application/text" , null )]
        [TestCase( "application/text;q=1" , "application/text" , 1 )]
        [TestCase( "application/text;q=1;" , "application/text" , 1 )]
        [TestCase( "q=1;application/text" , "application/text" , 1 )]
        public void CheckTryParseSucceed(string input , string value , double? quality )
        {
            Assert.IsTrue( WeightedString.TryParse( input , out var element ) );
            Assert.AreEqual( value , element.Value );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( " application " , "application" , null )]
        [TestCase( " application/text " , "application/text" , null )]
        [TestCase( " application/text;" , "application/text" , null )]
        [TestCase( " application/text ; q = " , "application/text" , null )]
        [TestCase( " application/text ; q = 1" , "application/text" , 1 )]
        [TestCase( " application/text ; q = 1;" , "application/text" , 1 )]
        [TestCase( " q=1 ; application/text " , "application/text" , 1 )]
        public void CheckTryParseSucceedWithSpaces(string input , string value , double? quality )
        {
            Assert.IsTrue( WeightedString.TryParse( input , out var element ) );
            Assert.AreEqual( value , element.Value );
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
        public void CheckTryParseSucceedWithQuotes(string input , string value , double? quality )
        {
            Assert.IsTrue( WeightedString.TryParse( input , out var element ) );
            Assert.AreEqual( value , element.Value );
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
        public void CheckTryParseSucceedWithDoubleQuotes(string input , string value , double? quality )
        {
            Assert.IsTrue( WeightedString.TryParse( input , out var element ) );
            Assert.AreEqual( value , element.Value );
            Assert.AreEqual( quality , element.Quality );
        }

        [TestCase( " \" application \"" , "application" , null )]
        [TestCase( " \" application/text \"" , "application/text" , null )]
        [TestCase( " \" application/text \";" , "application/text" , null )]
        [TestCase( " \" application/text \"; q=" , "application/text" , null )]
        [TestCase( " \" application/text \"; q= \"   \" " , "application/text" , null )]
        [TestCase( " \" application/text \"; q= \" 1 \"" , "application/text" , 1 )]
        [TestCase( " \" application/text \"; q= \" 1 \";" , "application/text" , 1 )]
        public void CheckTryParseSucceedWithDoubleQuotesAndSpaces(string input , string value , double? quality )
        {
            Assert.IsTrue( WeightedString.TryParse( input , out var element ) );
            Assert.AreEqual( value , element.Value );
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
            Assert.IsFalse( WeightedString.TryParse( input , out var element ) );
            Assert.IsNull( element );
        }
    }
}
