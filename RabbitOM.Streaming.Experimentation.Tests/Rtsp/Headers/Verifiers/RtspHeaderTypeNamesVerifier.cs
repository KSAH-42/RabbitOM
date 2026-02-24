using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers.Verifiers
{
    using RabbitOM.Streaming.Experimentation.Rtsp;

    [TestFixture]
    public class RtspHeaderTypeNamesVerifier
    {
        private readonly HashSet<string> ExceptedCases = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "BlockSize",
            "RtpInfo"
        };

        [Test]
        public void CheckHeaderTypeNames()
        {
            var assembly = Assembly.GetAssembly( typeof( RtspClient ) );

            foreach ( var type in assembly.ExportedTypes )
            {
                if ( ! type.Name.EndsWith( "RtspHeader" ) )
                {
                    continue;
                }

                var headerName = type.Name.Replace( "RtspHeader" , "" );

                var field = type.GetFields(BindingFlags.Public | BindingFlags.Static )
                    .Where( x => x.Name == "TypeName" )
                    .First()
                    ;

                var value = (field.GetValue( null ) as string).Replace( "-" , "" );

                if ( headerName != value )
                {
                    if ( ExceptedCases.Contains( headerName ) && ExceptedCases.Contains( value ) )
                    {
                        continue;
                    }

                    Assert.Fail( "TypeName static member has bad name" , headerName , field.GetValue( null ) );
                }
            }
        }
    }
}
