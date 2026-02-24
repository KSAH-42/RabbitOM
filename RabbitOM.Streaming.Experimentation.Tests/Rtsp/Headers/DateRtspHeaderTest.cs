using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class DateRtspHeaderTest
    {
       [TestCase( "Tue, 20 Feb 2024 14:32:10 GMT" )]
       [TestCase( "20 Feb 2024 14:32:10 GMT" )]
       [TestCase( "  Tue ,  20  Feb   2024   14:32:10   GMT  " )] 
       [TestCase( " ' Tue, 20 Feb 2024 14:32:10 GMT ' " )]
       [TestCase( " \" Tue ,  20  Feb   2024   14:32:10   GMT  \" " )] 
       public void CheckTryParseSucceed( string input )
        {
            Assert.IsTrue( DateRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( DayOfWeek.Tuesday , header.Value.DayOfWeek );
            Assert.AreEqual( 20 , header.Value.Day );
            Assert.AreEqual( 2 , header.Value.Month );
            Assert.AreEqual( 2024 , header.Value.Year );
            Assert.AreEqual( 14 , header.Value.Hour );
            Assert.AreEqual( 32 , header.Value.Minute );
            Assert.AreEqual( 10 , header.Value.Second );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( " 99 Feb 2024 14:32:10 GMT" )]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( DateRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new DateRtspHeader();

            header.Value = new DateTime(2024,2,20,14,32,10);

            Assert.AreEqual( "Tue, 20 Feb 2024 14:32:10 GMT" , header.ToString() );
        }
    }
}
