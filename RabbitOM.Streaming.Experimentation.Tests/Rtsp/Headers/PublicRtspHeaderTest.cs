using NUnit.Framework;
using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    [TestFixture]
    public class PublicRtspHeaderTest
    {
        [TestCase( "OPTIONS" )]
        [TestCase( " OPTIONS " )]
        [TestCase( " , OPTIONS , " )]
        [TestCase( " ,  , OPtIONS " )]
        [TestCase( " ,  , 'OPtIONS' " )]
        [TestCase( " ,  , \"OPtIONS\" " )]
        public void CheckTryParseSucceed1(string input )
        {
            Assert.IsTrue( PublicRtspHeaderValue.TryParse( input , out var header ) );
            Assert.AreEqual( 1 , header.Methods.Count);
            Assert.IsTrue( header.Methods.Contains( RtspMethod.OPTIONS ) );
        }

        [TestCase( "OPTIONS,DESCRIBE,SETUP,PLAY,PAUSE,TEARDOWN,xxx,SET_PARAMETER,GET_PARAMETER,RECORD,ANNOUNCE,REDIRECT" )]
        [TestCase( "oPTIONS,'SETUP', DESCRIBE ,PlAY,PAUSE,TEARDOWN,yyy,SET_PARAMETER,GET_PARAMETER,RECORD,ANNOUNCE,REDIRECT" )]
        
        public void CheckTryParseSucceed2(string input )
        {
            Assert.IsTrue( PublicRtspHeaderValue.TryParse( input , out var header ) );
            Assert.Greater( header.Methods.Count , 0 );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.OPTIONS ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.DESCRIBE ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.SETUP ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.PLAY) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.PAUSE ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.TEARDOWN) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.SET_PARAMETER ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.GET_PARAMETER) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.RECORD ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.REDIRECT ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.ANNOUNCE ) );
        }


        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "," )]
        [TestCase( ",," )]
        [TestCase( ";;;; " )]
        [TestCase( " ; ; ; ;  " )]
        [TestCase( "a=123; , " )]
        [TestCase( "x, y ,z  , " )]
        [TestCase( "OPTION" )]
        public void CheckTryParseFailed( string input)
        {
            Assert.IsFalse( PublicRtspHeaderValue.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckAddingMethods()
        {
            var header = new PublicRtspHeaderValue();

            Assert.AreEqual( 0 , header.Methods.Count );

            Assert.IsTrue( header.AddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( 1, header.Methods.Count );

            Assert.IsFalse( header.AddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( 1, header.Methods.Count );

            Assert.IsTrue( header.AddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreEqual( 2, header.Methods.Count );
        }

        [Test]
        public void CheckRemovingMethods()
        {
            var header = new PublicRtspHeaderValue();

            Assert.AreEqual( 0 , header.Methods.Count );

            Assert.IsTrue( header.AddMethod( RtspMethod.OPTIONS ) );
            Assert.IsTrue( header.AddMethod( RtspMethod.DESCRIBE ) );
            Assert.IsTrue( header.AddMethod( RtspMethod.SETUP ) );

            Assert.IsTrue( header.Methods.Contains( RtspMethod.SETUP ) );
            Assert.IsTrue( header.RemoveMethod( RtspMethod.SETUP ) );
            Assert.IsFalse( header.Methods.Contains( RtspMethod.SETUP ) );

            Assert.IsTrue( header.Methods.Contains( RtspMethod.OPTIONS ) );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.DESCRIBE ) );
            Assert.IsFalse( header.Methods.Contains( RtspMethod.SETUP ) );

            Assert.AreEqual( 2 , header.Methods.Count );
            Assert.IsTrue( header.RemoveMethod( RtspMethod.DESCRIBE ) );
            Assert.IsFalse( header.Methods.Contains( RtspMethod.DESCRIBE ) );
            Assert.AreEqual( 1 , header.Methods.Count );
            header.RemoveMethods();
            Assert.AreEqual( 0 , header.Methods.Count );
            Assert.IsFalse( header.Methods.Contains( RtspMethod.DESCRIBE ) );
            
        }
    }
}
