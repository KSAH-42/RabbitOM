using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class CSeqRtspHeaderTest
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
            Assert.IsTrue( CSeqRtspHeader.TryParse( input , out var header ) );
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
            Assert.IsFalse( CSeqRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new CSeqRtspHeader();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 0;
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 1;
            Assert.AreEqual( "1" , header.ToString() );

            header.Value = long.MaxValue;
            Assert.AreEqual( long.MaxValue.ToString() , header.ToString() );
        }
    }
}
