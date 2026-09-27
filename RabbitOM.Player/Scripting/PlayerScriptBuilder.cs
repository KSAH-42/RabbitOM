using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Runtime.Loader;
using System.Text;
using System.Reflection;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Player.Data;

	// TODO: try to move this code into a separate process
	public sealed class PlayerScriptBuilder
	{
		private const string DefaultAssemblyName = "RabbitOM.Player.Script.dll";
		private readonly Script _script;
		private readonly AssemblyLoadContext _loadContext;
		private readonly List<MetadataReference> _metadataReferences;


		public PlayerScriptBuilder( Script script , AssemblyLoadContext loadContext )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );
			_loadContext = loadContext ?? throw new ArgumentNullException( nameof( loadContext ) );
			_metadataReferences = new List<MetadataReference>();
		}


		public PlayerScriptBuilder LoadReferences()
		{
			var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			foreach (string refPath in trustedAssembliesPaths)
			{
				_metadataReferences.Add(MetadataReference.CreateFromFile(refPath));
			}

			_metadataReferences.Add( MetadataReference.CreateFromFile(typeof(PlayerScript).Assembly.Location) );

			foreach ( var reference in _script.References ?? [] )
			{
				var fileName = Path.GetFileName( reference.Name );

				if ( ! File.Exists( Path.Combine( AppContext.BaseDirectory , fileName ) ) )
				{
					File.Copy( reference.Name , Path.Combine( AppContext.BaseDirectory , fileName ) , true );
				}

				_metadataReferences.Add( MetadataReference.CreateFromFile( fileName ) );

				if ( reference.ForceLoad )
				{
					_loadContext.LoadFromAssemblyPath( Path.Combine( AppContext.BaseDirectory , fileName ) );
				}
			}

			return this;
		}

		public PlayerScript Build()
		{
			var tree = RoslynCompilerHelper.CreateSyntaxTree( _script.Language , _script.Code );

			var compilation = RoslynCompilerHelper.CreateCompilation( _script.Language , DefaultAssemblyName , new [] { tree } , _metadataReferences );

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
				if ( typeof( PlayerScript ).IsAssignableFrom( type ) && ! type.IsAbstract )
				{
					return (PlayerScript) Activator.CreateInstance( type ) !;
				}
			}

			throw new InvalidOperationException( "no valid type has been found" );
		}
	}
}
