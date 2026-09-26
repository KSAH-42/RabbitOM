using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Node.Scripting.Models;

	public sealed class NodeScriptBuilder
	{
		private readonly ScriptModel _model;

		public NodeScriptBuilder( ScriptModel model )
		{
			_model = model ?? throw new ArgumentNullException( nameof( model ) );
		}

		// TODO: refactor this method
		public NodeScript Build()
		{
			var references = new List<MetadataReference>();

			var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			foreach (string refPath in trustedAssembliesPaths)
			{
				references.Add(MetadataReference.CreateFromFile(refPath));
			}

			references.Add(MetadataReference.CreateFromFile("RabbitOM.dll"));
			references.Add(MetadataReference.CreateFromFile(typeof(NodeScript).Assembly.Location));

			foreach ( var reference in _model.References ?? Enumerable.Empty<ReferenceModel>() )
			{
				if ( reference == null )
				{
					continue;
				}

				var fileName = Path.GetFileName(reference.Name);

				if ( ! File.Exists( Path.Combine( AppContext.BaseDirectory , fileName ) ) )
				{
					var target = Path.Combine( AppContext.BaseDirectory , fileName );
					File.Copy( reference.Name , target, true );

					references.Add( MetadataReference.CreateFromFile( target ) );
				}
				else
				{
					references.Add( MetadataReference.CreateFromFile( fileName ) );
				}

				if ( reference.ForceLoad )
				{
					AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine( AppContext.BaseDirectory , fileName ));
				}
			}

			var tree = RoslynCompilerHelper.CreateSyntaxTree( _model.Language , _model.Code );

			var compilation = RoslynCompilerHelper.CreateCompilation( _model.Language , "nodeScript.dll" , new [] { tree } , references );

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
