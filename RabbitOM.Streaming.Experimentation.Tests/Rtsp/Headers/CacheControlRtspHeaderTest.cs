using NUnit.Framework;
using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    [TestFixture]
    public class CacheControlRtspHeaderTest
    {
        [TestCase( "stale-while-revalidate=12,no-transform,parameter1=a,parameter2=b" )]
        public void CheckParseSucceed( string input )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( 12 , header.StaleWhileRevalidate );
            Assert.IsTrue( header.NoTransform );
            Assert.AreEqual( 2 , header.Extensions.Count );
            Assert.AreEqual( "a" , header.Extensions["parameter1"] );
            Assert.AreEqual( "b" , header.Extensions["parameter2"] );
        }

        [TestCase( "no-cache" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParseNoCacheSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.NoCache );
        }

        [TestCase( "no-store" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParseNoStoreSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.NoStore );
        }

        [TestCase( "no-transform" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParseNoTransformSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.NoTransform );
        }

        [TestCase( "must-revalidate" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParseMustRevalidateSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.MustRevalidate );
        }

        [TestCase( "public" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParsePublicSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.Public );
        }

        [TestCase( "private" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParsePrivateSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.Private );
        }

        [TestCase( "immutable" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParseImmutableSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.Immutable );
        }

        [TestCase( "proxy-revalidate" , true )]
        [TestCase( "my-extension" , false )]
        public void CheckParseProxyRevalidateSucceed( string input , bool status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.ProxyRevalidate );
        }

        [TestCase( "max-age=1" , 1 )]
        [TestCase( "my-extension" , null )]
        public void CheckParseMaxAgeSucceed( string input , int? status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.MaximumAge );
        }

        [TestCase( "s-maxage=1" , 1 )]
        [TestCase( "my-extension" , null )]
        public void CheckParseSMaxAgeSucceed( string input , int? status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.ShareMaximumAge );
        }

        [TestCase( "stale-while-revalidate=1" , 1 )]
        [TestCase( "my-extension" , null )]
        public void CheckParseStaleWhileRevalidateSucceed( string input , int? status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.StaleWhileRevalidate );
        }

        [TestCase( "stale-if-error=1" , 1 )]
        [TestCase( "my-extension" , null )]
        public void CheckParseStaleIfErrorSucceed( string input , int? status )
        {
            Assert.IsTrue( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNotNull( header );
            Assert.AreEqual( status , header.StaleIfError );
        }

        [TestCase( null ) ]
        [TestCase( "" ) ]
        [TestCase( " " ) ]
        public void CheckParseFailed( string input  )
        {
            Assert.IsFalse( CacheControlRtspHeader.TryParse( input , out var header ) );
            Assert.IsNull( header );
        }

        [Test]
        public void CheckAddExtensions()
        {
            var header = new CacheControlRtspHeader();

            Assert.IsEmpty( header.Extensions );
            Assert.IsTrue( header.AddExtension( "paramter1" , "a" ) );
            Assert.AreEqual( 1 , header.Extensions.Count );

            Assert.IsTrue( header.AddExtension( "paramter2" , "a" ) );
            Assert.AreEqual( 2 , header.Extensions.Count );

            Assert.IsTrue( header.AddExtension( "paramter1" , "a" ) );
            Assert.AreEqual( 2 , header.Extensions.Count );
        }

        [Test]
        public void CheckRemoveExtensions()
        {
            var header = new CacheControlRtspHeader();

            Assert.IsEmpty( header.Extensions );
            Assert.IsTrue( header.AddExtension( "paramter1" , "a" ) );
            Assert.IsTrue( header.AddExtension( "paramter2" , "a" ) );
            Assert.IsTrue( header.AddExtension( "paramter3" , "a" ) );
            
            Assert.IsFalse( header.RemoveExtensionByName( "paramter" ) );
            Assert.IsTrue( header.RemoveExtensionByName( "paramter1" ) );
            Assert.IsTrue( header.RemoveExtensionByName( "paramter2" ) );
            
            header.RemoveExtensions();

            Assert.IsEmpty( header.Extensions );
            Assert.IsFalse( header.RemoveExtensionByName( "paramter1" ) );
        }
    }
}
