using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Microsoft.CodeAnalysis;

namespace RabbitOM.Node.Scripting
{
	using RabbitOM.Node.Models;

	// TODO: try to move this code into a separate process
	public sealed class NodeScriptBuilder
	{
		private const string DefaultAssemblyName = "RabbitOM.Node.Script.dll";
		private readonly ScriptModel _model;
		private readonly List<MetadataReference> _references;



		public NodeScriptBuilder( ScriptModel model )
		{
			_model = model ?? throw new ArgumentNullException( nameof( model ) );
			_references = new List<MetadataReference>();
		}




		public NodeScriptBuilder LoadReferences()
		{
			var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			foreach (string refPath in trustedAssembliesPaths)
			{
				_references.Add(MetadataReference.CreateFromFile(refPath));
			}

			_references.Add(MetadataReference.CreateFromFile("RabbitOM.dll"));
			_references.Add(MetadataReference.CreateFromFile(typeof(NodeScript).Assembly.Location));

			foreach ( var reference in _model.References ?? [] )
			{
				var fileName = Path.GetFileName( reference.Name );

				if ( ! File.Exists( Path.Combine( AppContext.BaseDirectory , fileName ) ) )
				{
					File.Copy( reference.Name , Path.Combine( AppContext.BaseDirectory , fileName ) , true );
				}

				_references.Add( MetadataReference.CreateFromFile( fileName ) );

				if ( reference.ForceLoad )
				{
					AssemblyLoadContext.Default.LoadFromAssemblyPath( Path.Combine( AppContext.BaseDirectory , fileName ) );
				}
			}

			return this;
		}

		public NodeScript Build()
		{
			var tree = RoslynCompilerHelper.CreateSyntaxTree( _model.Language , _model.Code );

			var compilation = RoslynCompilerHelper.CreateCompilation( _model.Language , DefaultAssemblyName , new [] { tree } , _references );

			using ( var stream = File.Create( DefaultAssemblyName ) )
			{
				var result = compilation.Emit( stream );

				if ( ! result.Success )
				{
					throw new InvalidOperationException( new StringBuilder()
							.Append( "can not create an instance of the script" )
							.AppendLine()
							.Append( string.Join( Environment.NewLine , result.Diagnostics ) )
							.ToString() );
				}
			}

			var assembly = Assembly.LoadFrom( DefaultAssemblyName );

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
