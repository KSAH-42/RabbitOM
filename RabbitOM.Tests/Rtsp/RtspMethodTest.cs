using NUnit.Framework;

namespace RabbitOM.Tests.Rtsp
{
    using RabbitOM.Net.RtspV2;

    [TestFixture]
    public class RtspMethodTest
    {
        [TestCase( "MY_VALID_CUSTOM_METHOD" , "MY_VALID_CUSTOM_METHOD" ) ]
        [TestCase( "MY_VALID_CUSTOM_METHOD1" , "MY_VALID_CUSTOM_METHOD1" ) ]
        [TestCase( "OPTIONS" , "OPTIONS" ) ]
        [TestCase( "DESCRIBE" , "DESCRIBE" ) ]
        [TestCase( "SETUP" , "SETUP" ) ]
        [TestCase( "PLAY" , "PLAY" ) ]
        [TestCase( "PAUSE" , "PAUSE" ) ]
        [TestCase( "TEARDOWN" , "TEARDOWN" ) ]
        [TestCase( "GET_PARAMETER" , "GET_PARAMETER" ) ]
        [TestCase( "SET_PARAMETER" , "SET_PARAMETER" ) ]
        [TestCase( "ANNOUNCE" , "ANNOUNCE" ) ]
        [TestCase( "REDIRECT" , "REDIRECT" ) ]
        [TestCase( "RECORD" , "RECORD" ) ]
        [TestCase( "*" , "*" ) ]
        public void CheckTryParseSucceed( string input , string method )
        {
            Assert.IsTrue( RtspMethod.TryParse( input , out var result ) );
            Assert.AreEqual( method , result.Value );
            Assert.AreEqual( method , result.ToString() );
        }

        [TestCase( null! )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "!" )]
        [TestCase( " myMethod " )]
        [TestCase( "OPTiONS" ) ]
        [TestCase( "DEScRIBE" ) ]
        [TestCase( "SEtUP" ) ]
        [TestCase( "PLaY" ) ]
        [TestCase( "PaUSE" ) ]
        [TestCase( "TEArDOWN" ) ]
        [TestCase( "GeT_PARAMETER" ) ]
        [TestCase( "SEt_PARAMETER" ) ]
        [TestCase( "ANnOUNCE" ) ]
        [TestCase( "ReCORD") ]
        [TestCase( " RECORD") ]
        [TestCase( "RECORD ") ]
        [TestCase( "REC\0ORD") ]
        [TestCase( "RECORD\r") ]
        [TestCase( "RECORD\b") ]
        [TestCase( "'RECORD'") ]
        [TestCase( "\"RECORD\"") ]
        [TestCase( "BAD CUSTOM METHOD" ) ]
        public void CheckTryParseFailed( string input )
        {
            Assert.IsFalse( RtspMethod.TryParse( input , out var result ) );
            Assert.IsNull( result );
        }
    }
}
