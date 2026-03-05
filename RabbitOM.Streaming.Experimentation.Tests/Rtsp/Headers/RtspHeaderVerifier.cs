using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class RtspHeaderVerifier
    {
        private readonly HashSet<string> ExceptedCases = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            nameof( BlockSizeRtspHeader ),
            nameof( RtpInfoRtspHeader ),
        };

        [Test]
        public void CheckHeaderTypeNames()
        {
            var assembly = Assembly.GetAssembly( typeof( RtspClient ) );

            foreach ( var type in assembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var typeNameField = type.GetFields(BindingFlags.Public | BindingFlags.Static )
                    .Where( x => x.Name == "TypeName" )
                    .First()
                    ;
                
                var typeNameValue = (typeNameField.GetValue( null ) as string).Replace( "-" , "" ) + nameof( RtspHeader );

                if ( type.Name == typeNameValue )
                {
                    continue;
                }

                if ( ExceptedCases.Contains( type.Name ) && ExceptedCases.Contains( typeNameValue ) )
                {
                    continue;
                }
                
                Assert.Fail( "TypeName static member has bad name" , type.Name , typeNameField.GetValue( null ) );
            }
        }

        [Test]
        public void CheckTryParseSignature()
        {
            var assembly = Assembly.GetAssembly( typeof( RtspClient ) );

            foreach ( var type in assembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var method = type.GetMethods(BindingFlags.Public | BindingFlags.Static )
                    .Where( x => x.Name == "TryParse" )
                    .First()
                    ;
                
                // to avoid mistakes when a new header is created by a copy and paste from existing one

                var outputParameter = method.GetParameters().First( x => x.IsOut && x.Name == "result" );

                Assert.AreEqual( outputParameter.ParameterType.FullName.Replace( "&","") , type.FullName );
            }
        }

        [Test]
        public void CheckToString()
        { 
            var assembly = Assembly.GetAssembly( typeof( RtspClient ) );

            foreach ( var type in assembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var method = type.GetMethod( "ToString" );
                
                var instance = Activator.CreateInstance( type );

                var output = method.Invoke( instance , null ) as string;

                Assert.IsNotNull( output );
                Assert.IsFalse( output.Contains( type.Name ) );
            }
        }
    }
}
