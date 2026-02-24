using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class LastModifiedRtspHeaderTest
    {
       [TestCase( "Mon, 19 Feb 2024 14:32:10 GMT" )]
       [TestCase( "19 Feb 2024 14:32:10 GMT" )]
       [TestCase( "  Mon ,  19  Feb   2024   14:32:10   GMT  " )] 
       [TestCase( " ' Mon, 19 Feb 2024 14:32:10 GMT ' " )]
       [TestCase( " \" Mon ,  19  Feb   2024   14:32:10   GMT  \" " )] 
       public void CheckParseSucceed( string input )
        {
            Assert.IsTrue( LastModifiedRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( DayOfWeek.Monday , header.Value.DayOfWeek );
            Assert.AreEqual( 19 , header.Value.Day );
            Assert.AreEqual( 2 , header.Value.Month );
            Assert.AreEqual( 2024 , header.Value.Year );
            Assert.AreEqual( 14 , header.Value.Hour );
            Assert.AreEqual( 32 , header.Value.Minute );
            Assert.AreEqual( 10 , header.Value.Second );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( " -1 Feb 2024 14:32:10 GMT" )]
        public void CheckParseFailed( string input  )
        {
            Assert.IsFalse( LastModifiedRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new LastModifiedRtspHeader();

            header.Value = new DateTime(2024,2,19,14,32,10);

            Assert.AreEqual( "Mon, 19 Feb 2024 14:32:10 GMT" , header.ToString() );
        }
    }
}
