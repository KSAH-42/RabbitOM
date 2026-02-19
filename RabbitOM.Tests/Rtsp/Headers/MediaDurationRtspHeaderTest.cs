using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Tests.Rtsp.Headers
{
    [TestFixture]
    public class MediaDurationRtspHeaderTest
    {
        [TestCase( "0" , 0 )]
        [TestCase( "1" , 1 )]
        [TestCase( "2" , 2 )]
        [TestCase( "1.7976931348623" , 1.7976931348623 )]
         
        [TestCase( " 0 " , 0 )]
        [TestCase( " 1 " , 1 )]
        [TestCase( " 2 " , 2 )]
        [TestCase( " 1.7976931348623 " , 1.7976931348623 )]

        [TestCase( " '0' " , 0 )]
        [TestCase( " '1' " , 1 )]
        [TestCase( " '2' " , 2 )]
        [TestCase( " '1.7976931348623' " , 1.7976931348623 )]

        [TestCase( " \"0\" " , 0 )]
        [TestCase( " \"1\" " , 1 )]
        [TestCase( " \"2\" " , 2 )]
        [TestCase( " \"1.7976931348623\" " , 1.7976931348623 )]

        [TestCase( " \"-1.7976931348623\"" , -1.7976931348623 )]
        [TestCase( " \"-1.7976931348623" , -1.7976931348623 )]
        [TestCase( " '-1.7976931348623" , -1.7976931348623 )]

        public void CheckParseSucceed( string input , double expectedId )
        {
            Assert.IsTrue( MediaDurationRtspHeader.TryParse( input , out var header ) );
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
            Assert.IsFalse( MediaDurationRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }


        [Test]
        public void CheckToString()
        {
            var header = new MediaDurationRtspHeader();

            Assert.AreEqual( 0 , header.Value );
            Assert.AreEqual( "0.000" , header.ToString() );

            header.Value = 0;
            Assert.AreEqual( "0.000" , header.ToString() );

            header.Value = 1.2;
            Assert.AreEqual( "1.200" , header.ToString() );

            header.Value = -1.2;
            Assert.AreEqual( "-1.200" , header.ToString() );
        }
    }
}
