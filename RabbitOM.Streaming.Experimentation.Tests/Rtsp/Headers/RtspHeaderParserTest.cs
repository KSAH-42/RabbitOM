using NUnit.Framework;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class RtspHeaderParserTest
    {
        [TestCase("apple banana pear" , " " )]
        [TestCase("apple  banana  pear" , " " )]
        [TestCase(" apple banana pear " , " " )]
        [TestCase("apple[]banana[]pear" , "[]" )]
        [TestCase("apple [] banana [] pear" , "[]" )]
        [TestCase("apple[][] banana[][] pear" , "[]" )]
        [TestCase("apple[] banana[][] pear" , "[]" )]
        public void CheckTryParse( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 3 , tokens.Length );
            Assert.AreEqual( "apple" , tokens[0] );
            Assert.AreEqual( "banana" , tokens[1] );
            Assert.AreEqual( "pear" , tokens[2] );
        }

        [TestCase("apple 'banana     with     chocolat' pear" , " " )]
        [TestCase("apple \"banana     with     chocolat\" pear" , " " )]
        [TestCase("apple 'banana     with     chocolat\" pear" , " " )]
        public void CheckTryParse1( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 3 , tokens.Length );
            Assert.AreEqual( "apple" , tokens[0] );
            Assert.AreEqual( "banana     with     chocolat" , tokens[1] );
            Assert.AreEqual( "pear" , tokens[2] );
        }

        [TestCase("apple[[] banana[][] pear" , "[]" )]
        public void CheckTryParse2( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 3 , tokens.Length );
            Assert.AreEqual( "apple[" , tokens[0] );
            Assert.AreEqual( "banana" , tokens[1] );
            Assert.AreEqual( "pear" , tokens[2] );
        }

        [TestCase("apple 'banana with chocolat' pear" , " " )]
        [TestCase("apple ' banana with chocolat ' pear" , " " )]
        [TestCase("apple \"banana with chocolat\" pear" , " " )]
        [TestCase("apple 'banana with chocolat\" pear" , " " )]
        [TestCase("apple \"banana with chocolat' pear" , " " )]
        public void CheckTryParse3( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 3 , tokens.Length );
            Assert.AreEqual( "apple" , tokens[0] );
            Assert.AreEqual( "banana with chocolat" , tokens[1] );
            Assert.AreEqual( "pear" , tokens[2] );
        }

        [TestCase("applemy seperator'bananamy seperatorwith chocolat'my seperatorpear" , "my seperator" )]
        public void CheckTryParse4( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 3 , tokens.Length );
            Assert.AreEqual( "apple" , tokens[0] );
            Assert.AreEqual( "bananamy seperatorwith chocolat" , tokens[1] );
            Assert.AreEqual( "pear" , tokens[2] );
        }
        
        [TestCase("[*]" , "[]" )]
        [TestCase("[][*]" , "[]" )]
        [TestCase("[*][]" , "[]" )]
        [TestCase("[][*][]" , "[]" )]
        [TestCase("[] [*] []" , "[]" )]
        public void CheckTryParse5( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 1 , tokens.Length );
            Assert.AreEqual( "[*]" , tokens[0] );
        }

        [TestCase("this is a test" , null )]
        [TestCase("this is a test" , "" )]
        public void CheckTryParse6( string input , string seperator )
        {
            Assert.True( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.AreEqual( 1 , tokens.Length );
            Assert.AreEqual( "this is a test" , tokens[0] );
        }

        [TestCase( null , null )]
        [TestCase( null , "" )]
        [TestCase( null , " " )]
        [TestCase( "" , "" )]
        [TestCase( "" , " " )]
        [TestCase( "apple 'banana with chocolat' pear" , "'" )]
        [TestCase( "apple 'banana with chocolat' pear" , "\"" )]
        public void CheckTryParseFailed( string input , string seperator )
        {
            Assert.False( RtspHeaderParser.TryParse( input , seperator , out string[] tokens ) );
            Assert.IsNull( tokens );
        }
    }
}
