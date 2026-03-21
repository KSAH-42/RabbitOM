using NUnit.Framework;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class WarningRtspHeaderValueTest
    {
        [TestCase( "1 my-agent1" , 1 ) ]
        [TestCase( "1 my-agent1 , 2 my-agent2" , 2 ) ]
        [TestCase( "1 my-agent1 , 2 my-agent2, 3 my-agent3" , 3 ) ]
        // to fix: [TestCase( "1 my-agent1 , 2 my-agent2 'my comment', 3 my-agent3" , 3 ) ]
        
        public void CheckTryParseSucceed( string input , int count )
        {
            if ( ! WarningRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse has failed" );
            }
            
            Assert.IsNotNull( header , "the header can not by null" );
            Assert.Greater( header.Warnings.Count , 0 );
            Assert.AreEqual( count , header.Warnings.Count );

            for ( int i = 1 ; i <= header.Warnings.Count; ++ i )
            {
                var warning = header.Warnings.ElementAt( i - 1 );

                Assert.AreEqual( i , warning.Code );
                Assert.AreEqual( $"my-agent{i}" , warning.Agent );
            }
        }
    }
}
