using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp;

    [TestFixture]
    public class RtspMethodTest
    {
        [TestCase( "OPTIONS" , "OPTIONS" ) ]
        [TestCase( " OPTiONS " , "OPTIONS" ) ]
        
        [TestCase( "DESCRIBE" , "DESCRIBE" ) ]
        [TestCase( " DEScRIBE " , "DESCRIBE" ) ]

        [TestCase( "SETUP" , "SETUP" ) ]
        [TestCase( " SEtUP " , "SETUP" ) ]

        [TestCase( "PLAY" , "PLAY" ) ]
        [TestCase( " PLaY " , "PLAY" ) ]

        [TestCase( "PAUSE" , "PAUSE" ) ]
        [TestCase( " PaUSE " , "PAUSE" ) ]

        [TestCase( "TEARDOWN" , "TEARDOWN" ) ]
        [TestCase( " TEArDOWN " , "TEARDOWN" ) ]

        [TestCase( "GET_PARAMETER" , "GET_PARAMETER" ) ]
        [TestCase( " GeT_PARAMETER " , "GET_PARAMETER" ) ]

        [TestCase( "SET_PARAMETER" , "SET_PARAMETER" ) ]
        [TestCase( " SEt_PARAMETER " , "SET_PARAMETER" ) ]

        [TestCase( "ANNOUNCE" , "ANNOUNCE" ) ]
        [TestCase( " ANnOUNCE " , "ANNOUNCE" ) ]

        [TestCase( "REDIRECT" , "REDIRECT" ) ]
        [TestCase( " REDiRECT " , "REDIRECT" ) ]
        
        [TestCase( "RECORD" , "RECORD" ) ]
        [TestCase( " REcORD " , "RECORD" ) ]

        public void CheckTryParseSucceed( string input , string method )
        {
            Assert.IsTrue( RtspMethod.TryParse( input , out var result ) );
            Assert.AreEqual( method , result.Name );
            Assert.AreEqual( method , result.ToString() );
        }

        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( " myMethod " )]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( RtspMethod.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }
    }
}
