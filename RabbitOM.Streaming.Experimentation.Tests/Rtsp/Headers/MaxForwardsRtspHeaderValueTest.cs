using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers;
    
    [TestFixture]
    public class MaxForwardsRtspHeaderValueTest
    {
        [TestCase( "0" , 0 )]
        [TestCase( "1" , 1 )]
        [TestCase( "2" , 2 )]
        [TestCase( "4294967295" , 4294967295 )]
         
        [TestCase( " 0 " , 0 )]
        [TestCase( " 1 " , 1 )]
        [TestCase( " 2 " , 2 )]
        [TestCase( " 4294967295 " , 4294967295 )]

        [TestCase( " '0' " , 0 )]
        [TestCase( " '1' " , 1 )]
        [TestCase( " '2' " , 2 )]
        [TestCase( " '4294967295' " , 4294967295 )]

        [TestCase( " \"0\" " , 0 )]
        [TestCase( " \"1\" " , 1 )]
        [TestCase( " \"2\" " , 2 )]
        [TestCase( " \"4294967295\" " , 4294967295 )]

        public void CheckTryParseSucceed( string input , long expectedId )
        {
            Assert.IsTrue( MaxForwardsRtspHeaderValue.TryParse( input , out var header ) );
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
            Assert.IsFalse( MaxForwardsRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new MaxForwardsRtspHeaderValue();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 0;
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 1;
            Assert.AreEqual( "1" , header.ToString() );

            header.Value = uint.MaxValue;
            Assert.AreEqual( uint.MaxValue.ToString() , header.ToString() );
        }
    }
}
