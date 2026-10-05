using System;
using System.IO;
using System.Runtime.Loader;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Player.Data;

	public sealed class PlayerScriptBuilder
	{
		private readonly Script _script;
		private readonly AssemblyLoadContext _loadContext;
		private readonly List<MetadataReference> _metadataReferences;


		public PlayerScriptBuilder( Script script , AssemblyLoadContext loadContext )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );
			_loadContext = loadContext ?? throw new ArgumentNullException( nameof( loadContext ) );
			_metadataReferences = new List<MetadataReference>();
		}


		public string OutputAssembly { get; set; } = "RabbitOM.Player.Script.dll";


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
			try
			{
				var tree = RoslynCompilerHelper.CreateSyntaxTree( _script.Language , _script.Code );

				var compilation = RoslynCompilerHelper.CreateCompilation( _script.Language , OutputAssembly , new [] { tree } , _metadataReferences );

				var assemblyFile = Path.Combine( AppContext.BaseDirectory , OutputAssembly );

				if ( File.Exists( assemblyFile ) )
				{
					File.Delete( assemblyFile );
				}

				using ( var stream = File.Create( assemblyFile ) )
				{
					var result = compilation.Emit( stream );

					if ( ! result.Success )
					{
						throw new BuildException( "Build failed: IL emit failed" , result.Diagnostics.Select( diagnotic => diagnotic.ToString() ) );
					}
				}

				var assembly = _loadContext.LoadFromAssemblyPath( assemblyFile );

				foreach( var type in assembly.GetTypes() )
				{
					if ( typeof( PlayerScript ).IsAssignableFrom( type ) && ! type.IsAbstract )
					{
						return (PlayerScript) Activator.CreateInstance( type );
					}
				}

				throw new BuildException( "Build failed: type not found" );
			}
			catch ( BuildException )
			{
				throw;
			}
			catch ( Exception ex)
			{
				throw new BuildException( "Build failed" , ex );
			}
		}
	}
}
