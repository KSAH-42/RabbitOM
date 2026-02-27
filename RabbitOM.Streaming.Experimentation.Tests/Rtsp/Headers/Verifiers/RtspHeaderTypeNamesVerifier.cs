using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Verifiers
{
    using RabbitOM.Streaming.Experimentation.Rtsp;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class RtspHeaderTypeNamesVerifier
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
    }
}
