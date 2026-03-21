using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Verifiers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    using System.CodeDom;

    [TestFixture]
    public class RtspHeaderVerifier
    {
        private readonly static Assembly CurrentAssembly = Assembly.GetAssembly( typeof( AuthenticateRtspHeaderValue ) );

        private readonly static HashSet<string> ExceptedCases = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            nameof( BlockSizeRtspHeaderValue ),
            nameof( RtpInfoRtspHeaderValue ),
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
            "Accept-Ranges",

            "Bandwidth",
            "Blocksize",
            "Speed",

            "Warning",

            "Unsupported",
            "Conference",
            "If-Match",
            "If-Modified-Since",
            "Last-Modified",
            "Content-Range",
            "Content-Disposition",
            "Max-Forwards",
            "Media-Duration",
        };

        [Test]
        public void CheckHeaderTypeNames()
        {
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element != typeof( StringWithQualityRtspHeaderValue) && element.IsSubclassOf( typeof( RtspHeaderValue ) ) ) )
            {
                var typeNameField = type.GetField( "TypeName" , BindingFlags.Public | BindingFlags.Static );
                
                var typeNameValue = (typeNameField.GetValue( null ) as string).Replace( "-" , "" ) + "RtspHeaderValue";

                Assert.IsTrue( OfficialHeaderNames.Contains( typeNameField.GetValue( null ) as string ) );

                if ( typeNameValue == type.Name )
                {
                    continue;
                }

                if ( ExceptedCases.Contains( typeNameValue ) && ExceptedCases.Contains( type.Name ) )
                {
                    continue;
                }

                if ( typeNameValue.StartsWith( "WWW") && typeNameValue.Replace("WWW" , "" ) == type.Name )
                {
                    continue;
                }
                                
                Assert.Fail( $"TypeName static member has bad name: {type.Name}" , type.Name , typeNameField.GetValue( null ) );
            }
        }

        [Test]
        public void CheckTryParseSignature()
        {
            // to avoid mistakes when a new header is created by a copy and paste from existing one

            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeaderValue ) ) ) )
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
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeaderValue ) ) ) )
            {
                Assert.IsTrue( type.IsSealed , $"{type.Name}" );
            }
        }
    }
}
