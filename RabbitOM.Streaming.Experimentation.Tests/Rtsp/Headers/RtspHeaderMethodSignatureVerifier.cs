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
    public class RtspHeaderMethodSignatureVerifier
    {
        [Test]
        public void CheckHeaderTypeNames()
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
    }
}
