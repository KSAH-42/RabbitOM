using NUnit.Framework;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp;
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved;

    [TestFixture]
    public class AllowRtspHeaderTest
    {
        [TestCase( "OPTIONS" )]
        [TestCase( " OPTIONS " )]
        [TestCase( " , OPTIONS , " )]
        [TestCase( " ,  , OPtIONS " )]
        [TestCase( " ,  , 'OPtIONS' " )]
        [TestCase( " ,  , \"OPtIONS\" " )]
        public void CheckTryParseSucceed1(string input )
        {
            Assert.IsTrue( AllowRtspHeader.TryParse( input , out var header ) );
            Assert.AreEqual( 1 , header.Methods.Count);
            Assert.IsTrue( header.Methods.Contains( RtspMethod.OPTIONS ) );
        }

        [TestCase( "OPTIONS,DESCRIBE,SETUP,PLAY,PAUSE,TEARDOWN,ccc,SET_PARAMETER,GET_PARAMETER,RECORD,ANNOUNCE,REDIRECT" )]
        [TestCase( "oPTIONS,'SETUP', DESCRIBE ,PlAY,PAUSE,TEARDOWN,tttt,SET_PARAMETER,GET_PARAMETER,RECORD,ANNOUNCE,REDIRECT" )]
        
        public void CheckTryParseSucceed2(string input )
        {
            Assert.IsTrue( AllowRtspHeader.TryParse( input , out var header ) );
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
            Assert.IsFalse( AllowRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckAddMethods()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( 0 , header.Methods.Count );

            Assert.IsTrue( header.AddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( 1, header.Methods.Count );

            Assert.IsFalse( header.AddMethod( RtspMethod.OPTIONS ) );
            Assert.AreEqual( 1, header.Methods.Count );

            Assert.IsTrue( header.AddMethod( RtspMethod.DESCRIBE ) );
            Assert.AreEqual( 2, header.Methods.Count );
        }

        [Test]
        public void CheckRemoveMethods()
        {
            var header = new AllowRtspHeader();

            Assert.AreEqual( 0 , header.Methods.Count );

            Assert.IsTrue( header.AddMethod( RtspMethod.OPTIONS ) );
            Assert.IsTrue( header.AddMethod( RtspMethod.DESCRIBE ) );
            Assert.IsTrue( header.AddMethod( RtspMethod.SETUP ) );

            Assert.IsTrue( header.Methods.Contains( RtspMethod.SETUP ) );
            Assert.IsTrue( header.RemoveMethod( RtspMethod.SETUP ) );
            Assert.IsFalse( header.Methods.Contains( RtspMethod.SETUP ) );

            Assert.IsTrue( header.RemoveMethod( RtspMethod.OPTIONS ) );
            Assert.IsFalse( header.RemoveMethod( RtspMethod.OPTIONS ) );

            Assert.AreEqual( 1 , header.Methods.Count );
            Assert.IsTrue( header.Methods.Contains( RtspMethod.DESCRIBE ) );
            
            Assert.IsTrue( header.AddMethod( RtspMethod.RECORD ) );
            Assert.IsTrue( header.RemoveMethodBy( x => x.Name == RtspMethod.RECORD.Name ) );
            Assert.IsFalse( header.RemoveMethodBy( x => x.Name == RtspMethod.RECORD.Name ) );

            header.ClearMethods();
            Assert.AreEqual( 0 , header.Methods.Count );
            Assert.IsFalse( header.Methods.Contains( RtspMethod.DESCRIBE ) );
        }

        [Test]
        public void CheckToString()
        {
            var header = new AllowRtspHeader();

            header.AddMethod( RtspMethod.OPTIONS );
            Assert.AreEqual( "OPTIONS" , header.ToString() );

            header.AddMethod( RtspMethod.SETUP );
            Assert.AreEqual( "OPTIONS, SETUP" , header.ToString() );

            header.AddMethod( RtspMethod.RECORD );
            Assert.AreEqual( "OPTIONS, SETUP, RECORD" , header.ToString() );
        }
    }
}
