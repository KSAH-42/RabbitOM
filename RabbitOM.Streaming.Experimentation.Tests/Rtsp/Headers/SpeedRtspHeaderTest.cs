using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using System.Globalization;

    [TestFixture]
    public class SpeedRtspHeaderTest
    {
        [TestCase( "0" , 0 )]
        [TestCase( "1" , 1 )]
        [TestCase( "2" , 2 )]
        [TestCase( "9223372036854775807" , 9223372036854775807 )]
         
        [TestCase( " 0 " , 0 )]
        [TestCase( " 1 " , 1 )]
        [TestCase( " 2 " , 2 )]
        [TestCase( " 9223372036854775807 " , 9223372036854775807 )]

        [TestCase( " '0' " , 0 )]
        [TestCase( " '1' " , 1 )]
        [TestCase( " '2' " , 2 )]
        [TestCase( " '9223372036854775807' " , 9223372036854775807 )]

        [TestCase( " \"0\" " , 0 )]
        [TestCase( " \"1\" " , 1 )]
        [TestCase( " \"2\" " , 2 )]
        [TestCase( " \"9223372036854775807\" " , 9223372036854775807 )]

        [TestCase( " \"-9223372036854775808\"" , -9223372036854775808 )]
        [TestCase( " \"-9223372036854775808" , -9223372036854775808 )]
        [TestCase( " '-9223372036854775808" , -9223372036854775808 )]

        public void CheckTryParseSucceed( string input , long expectedId )
        {
            Assert.IsTrue( SpeedRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( expectedId , header.Value );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        [TestCase( "a" ) ]
        [TestCase( "aa" ) ]
        [TestCase( " aa " ) ]
        [TestCase( " a1a " ) ]
        [TestCase( " ? " ) ]
        [TestCase( " '' " ) ]
        [TestCase( "''" ) ]
        [TestCase( "\"\"" ) ]
        [TestCase( "\" \"" ) ]
        [TestCase( " \" \" " ) ]
        public void CheckTryParseFailed( string input  )
        {
            Assert.IsFalse( SpeedRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new SpeedRtspHeader();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0.0" , header.ToString() );

            header.Value = 1;
            Assert.AreEqual( "1.0" , header.ToString() );

            header.Value = double.MaxValue;
            Assert.AreEqual( double.MaxValue.ToString("0.0" , CultureInfo.InvariantCulture) , header.ToString() );
        }
    }
}
