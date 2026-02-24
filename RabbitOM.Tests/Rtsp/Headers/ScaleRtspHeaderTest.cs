using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class ScaleRtspHeaderTest
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

        public void CheckParseSucceed( string input , long expectedId )
        {
            Assert.IsTrue( ScaleRtspHeader.TryParse( input , out var header ) );
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
        public void CheckParseFailed( string input  )
        {
            Assert.IsFalse( ScaleRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new ScaleRtspHeader();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 0;
            Assert.AreEqual( "0" , header.ToString() );

            header.Value = 1;
            Assert.AreEqual( "1" , header.ToString() );

            header.Value = 4.1f;
            Assert.AreEqual( "4.1" , header.ToString() );
        }
    }
}
