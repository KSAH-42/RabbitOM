using NUnit.Framework;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers;
    
    [TestFixture]
    public class IfMatchRtspHeaderValueTest
    {
        [TestCase( "\"a\"" , 1 ) ]
        [TestCase( "\"a\"" , 1 ) ]
        [TestCase( "\"a\",\"a\"" , 1 ) ]
        [TestCase( " \"a\" , \"a\" " , 1 ) ]
        [TestCase( " \"a\" , , \"a\" " , 1 ) ]
        [TestCase( "\"a\", \"\" " , 1 ) ]

        [TestCase( "\"a\",\"b\"" , 2 ) ]
        [TestCase( "\"a\",,\"b\"" , 2 ) ]
        [TestCase( "\"a\", \"b\" " , 2 ) ]
        [TestCase( "\"a\", \"b\" , \"b\" " , 2 ) ]

        public void CheckTryParseSucceed( string input , int count )
        {
            if ( ! IfMatchRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse has failed" );
            }

            Assert.IsNotNull( header , "the header can not by null" );
            Assert.AreEqual( count , header.EntitiesTags.Count );
        }


        [TestCase( null )]
        [TestCase( "" )]
        [TestCase( " " )]
        [TestCase( "\r\n" )]
        [TestCase( ",,,," )]
        [TestCase( " , , , , " )]
        public void CheckTryParseFailed( string input )
        {
            if ( IfMatchRtspHeaderValue.TryParse( input , out var header ) )
            {
                Assert.Fail( "parse must failed" );
            }

            Assert.IsNull( header , "the header be null null" );
        }

        [Test]
        public void CheckToString()
        {
            var header = new IfMatchRtspHeaderValue();

            Assert.IsEmpty( header.ToString() );

            header.AddEntityTag( "x" );
            Assert.AreEqual( "\"x\"" , header.ToString() );

            header.AddEntityTag( "x" );
            Assert.AreEqual( "\"x\"" , header.ToString() );

            header.AddEntityTag( "y" );
            Assert.AreEqual( "\"x\", \"y\"" , header.ToString() );
        }

        [Test]
        public void CheckAddEntityTag()
        {
            var header = new IfMatchRtspHeaderValue();

            Assert.IsEmpty( header.EntitiesTags );

            Assert.IsTrue( header.AddEntityTag( "x" ));
            Assert.AreEqual( 1 , header.EntitiesTags.Count );

            Assert.IsTrue( header.AddEntityTag( "y" ));
            Assert.AreEqual( 2 , header.EntitiesTags.Count );

            Assert.IsFalse( header.AddEntityTag( "y" ));
            Assert.AreEqual( 2 , header.EntitiesTags.Count );
        }

        [Test]
        public void CheckRemoveEntityTag()
        {
            var header = new IfMatchRtspHeaderValue();

            Assert.IsEmpty( header.EntitiesTags );

            Assert.IsTrue( header.AddEntityTag( "x" ) );
            Assert.AreEqual( 1 , header.EntitiesTags.Count );

            Assert.IsTrue( header.AddEntityTag( "y" ) );
            Assert.AreEqual( 2 , header.EntitiesTags.Count );

            Assert.IsFalse( header.RemoveEntityTag( null ) );
            Assert.IsFalse( header.RemoveEntityTag( "" ) );
            Assert.IsFalse( header.RemoveEntityTag( " " ) );

            Assert.IsTrue( header.RemoveEntityTag( "x" ) );
            Assert.IsFalse( header.RemoveEntityTag( "x" ) );
            
            Assert.AreEqual( 1 , header.EntitiesTags.Count );

            header.RemoveEntitiesTags();
            Assert.IsEmpty( header.EntitiesTags );
        }
    }
}
