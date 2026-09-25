using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;

namespace RabbitOM.Node.Scripting
{
	public sealed class NodeScriptBuilder
	{
		public string Code { get; set; }

		public string Language { get; set; }

		public HashSet<string> References { get; } = new HashSet<string>( StringComparer.OrdinalIgnoreCase ) { "RabbitOM.dll" };

		public NodeScript Build()
		{
			var references = new List<MetadataReference>();

			references.Add(MetadataReference.CreateFromFile(typeof(NodeScript).Assembly.Location));

			foreach ( var reference in References )
			{
				references.Add( MetadataReference.CreateFromFile( reference ) );
			}

			var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			foreach (string refPath in trustedAssembliesPaths)
			{
				references.Add(MetadataReference.CreateFromFile(refPath));
			}

			var tree = CodeProviderFactory.CreateSyntaxTree( Language , Code );

			var compilation = CodeProviderFactory.CreateCompilation( Language , "nodeScript.dll" , new [] { tree } , references );

			using var memoryStream = new MemoryStream();

			var result = compilation.Emit( memoryStream );

			if ( ! result.Success )
			{
				throw new InvalidOperationException( new StringBuilder()
						.Append( "can not create an instance of the script" )
						.AppendLine()
						.Append( string.Join( Environment.NewLine , result.Diagnostics ) )
						.ToString() );
			}

			memoryStream.Position = 0;

			var assembly = Assembly.Load( memoryStream.ToArray() );

			foreach( var type in assembly.GetTypes() )
			{
				if ( typeof( NodeScript ).IsAssignableFrom( type ) && ! type.IsAbstract )
				{
					return (NodeScript) Activator.CreateInstance( type );
				}
			}

			throw new InvalidOperationException( "no valid type has been found" );
		}
	}
}
