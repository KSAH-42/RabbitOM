using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Verifiers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class RtspHeaderVerifier
    {
        private readonly static Assembly CurrentAssembly = Assembly.GetAssembly( typeof( WWWAuthenticateRtspHeader ) );

        private readonly static HashSet<string> ExceptedCases = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            nameof( BlockSizeRtspHeader ),
            nameof( RtpInfoRtspHeader ),
        };

        private static readonly HashSet<string> OfficialHeaderNames = new HashSet<string>()
        {
            "CSeq",
            "Session",
            "Transport",
            "Range",
            "Scale",
            "RTP-Info",
            "Require",
            "Proxy-Require",
            "Public",
            "Allow",
            "Retry-After",

            "Authorization",
            "Proxy-Authorization",
            "WWW-Authenticate",
            "Proxy-Authenticate",

            "Content-Type",
            "Content-Length",
            "Content-Base",
            "Content-Location",
            "Content-Encoding",
            "Content-Language",

            "Cache-Control",
            "Expires",
            "Pragma",

            "User-Agent",
            "Server",

            "Via",
            "Connection",
            "Location",
            "Referer",
            "From",
            "To",
            "Date",

            "Accept",
            "Accept-Encoding",
            "Accept-Language",

            "Bandwidth",
            "Blocksize",
            "Speed",

            "Warning",

            "Unsupported",
            "Conference",
            "If-Modified-Since",
            "Last-Modified",
            "Content-Range",
            "Content-Disposition"
        };

        [Test]
        public void CheckHeaderTypeNames()
        {
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var typeNameField = type.GetField( "TypeName" , BindingFlags.Public | BindingFlags.Static );
                
                var typeNameValue = (typeNameField.GetValue( null ) as string).Replace( "-" , "" ) + "RtspHeader";

                if ( type.Name == typeNameValue )
                {
                    continue;
                }

                if ( ExceptedCases.Contains( type.Name ) && ExceptedCases.Contains( typeNameValue ) )
                {
                    continue;
                }
                
                Assert.IsTrue( OfficialHeaderNames.Contains( typeNameField.GetValue( null ) as string ) );
                
                Assert.Fail( $"TypeName static member has bad name: {type.Name}" , type.Name , typeNameField.GetValue( null ) );
            }
        }

        [Test]
        public void CheckTryParseSignature()
        {
            // to avoid mistakes when a new header is created by a copy and paste from existing one

            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var method = type.GetMethod( "TryParse" , BindingFlags.Public | BindingFlags.Static );
                
                var outputParameter = method.GetParameters().First( x => x.IsOut && x.Name == "result" );

                Assert.AreEqual( outputParameter.ParameterType.FullName.Replace( "&","") , type.FullName );
            }
        }

        [Test]
        public void CheckHeaderPrivateMembers()
        {
            // to avoid mistakes

            foreach ( var type in CurrentAssembly.ExportedTypes )
            {
                foreach ( var field in type.GetFields() )
                {
                    if ( field.IsPublic && field.Name.StartsWith( "_" ) )
                    {
                        Assert.Fail( $"the public member {type.Name}:{field.Name} must be marked as private member" );
                    }
                }
            }
        }

        [Test]
        public void CheckHeaderAreSealed()
        {
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                Assert.IsTrue( type.IsSealed , $"{type.Name}" );
            }
        }
    }
}
