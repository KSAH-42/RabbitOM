using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Types;
    
    [TestFixture]
    public class WarningInfoTest
    {
        [TestCase( "1 agent 'my comment' " , 1 , "agent" , "my comment" ) ]
        [TestCase( "1 agent \"my comment\" " , 1 , "agent" , "my comment" ) ]
        [TestCase( "1 product-name \"my comment\" " , 1 , "product-name" , "my comment" ) ]
        [TestCase( "1 product-name1 \"my comment\" " , 1 , "product-name1" , "my comment" ) ]
        [TestCase( "1 product/1.2 \"my comment\" " , 1 , "product/1.2" , "my comment" ) ]
        [TestCase( "1 product/1.2 \"my comment\" " , 1 , "product/1.2" , "my comment" ) ]
        [TestCase( "  1     product/1.2     \"my comment\"   " , 1 , "product/1.2" , "my comment" ) ]
        public void CheckTryParseSucceed( string input , int code , string agent , string comment)
        {
            Assert.IsTrue( WarningInfo.TryParse( input, out var info ) );
            Assert.IsNotNull( info );
            Assert.AreEqual( code , info.Code );
            Assert.AreEqual( agent , info.Agent );
            Assert.AreEqual( comment , info.Comment );
        }

        [TestCase( null )]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("c")]
        [TestCase("1 ")]
        [TestCase( "-1 agent 'my comment'" ) ]
        [TestCase( "1 ag ent 'my comment'" ) ]
        [TestCase( "1 'ag ent' 'my comment'" ) ]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( WarningInfo.TryParse( input , out var info ) );
            Assert.IsNull( info );
        }
    }
}
