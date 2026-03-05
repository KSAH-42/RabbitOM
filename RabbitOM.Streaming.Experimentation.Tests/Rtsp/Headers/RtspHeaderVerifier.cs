using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    [TestFixture]
    public class RtspHeaderVerifier
    {
        private readonly static HashSet<string> ExceptedCases = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            nameof( BlockSizeRtspHeader ),
            nameof( RtpInfoRtspHeader ),
        };

        private readonly static Assembly CurrentAssembly = Assembly.GetAssembly( typeof( RtspHeader ) );


        [Test]
        public void CheckHeaderTypeNames()
        {
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var typeNameField = type.GetProperty( "TypeName" , BindingFlags.Public | BindingFlags.Static );
                
                var typeNameValue = (typeNameField.GetValue( null ) as string).Replace( "-" , "" ) + nameof( RtspHeader );

                if ( type.Name == typeNameValue )
                {
                    continue;
                }

                if ( ExceptedCases.Contains( type.Name ) && ExceptedCases.Contains( typeNameValue ) )
                {
                    continue;
                }
                
                Assert.Fail( $"TypeName static member has bad name: {type.Name}" , type.Name , typeNameField.GetValue( null ) );
            }
        }

        [Test]
        public void CheckTryParseSignature()
        {
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
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
        public void CheckToStringIsImplemented()
        { 
            foreach ( var type in CurrentAssembly.ExportedTypes.Where( element => element.IsSubclassOf( typeof( RtspHeader ) ) ) )
            {
                var method = type.GetMethod( "ToString" );
                
                Assert.IsTrue( method.DeclaringType != typeof(object) );

                var instance = Activator.CreateInstance( type );

                // TODO: find a setter a set an abritary value and check the returns value after calling the tostring method

                
                var output = method.Invoke( instance , null ) as string;

                Assert.IsNotNull( output );
                Assert.IsFalse( output.Contains( type.Name ) );
            }
        }
    }
}
