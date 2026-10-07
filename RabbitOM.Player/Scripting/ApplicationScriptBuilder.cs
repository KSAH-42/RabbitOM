using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Loader;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Player.Data;

	public sealed class ApplicationScriptBuilder
	{
		private readonly Script _script;
		private readonly AssemblyLoadContext _loadContext;
		private readonly List<MetadataReference> _metadataReferences;




		public ApplicationScriptBuilder( Script script , AssemblyLoadContext loadContext )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );
			_loadContext = loadContext ?? throw new ArgumentNullException( nameof( loadContext ) );
			_metadataReferences = new List<MetadataReference>();
		}




		public string OutputAssembly { get; set; } = $"RabbitOM.Player.Script.dll";

		public string OutputFolder { get; set; } = @"scripts";

		public bool UseTempAssembly { get; set; } = true;




		public ApplicationScriptBuilder ClearOutputDirectory()
		{
			if ( string.IsNullOrWhiteSpace( OutputFolder ) )
			{
				throw new InvalidOperationException( "the OutputFolder property must be defined" );
			}

			var path = Path.Combine( AppContext.BaseDirectory , OutputFolder );

			if ( Directory.Exists( path ) )
			{
				var assemblyName = Path.GetFileNameWithoutExtension( OutputAssembly );

				foreach ( var file in Directory.GetFiles( path ) )
				{
					var fileName = Path.GetFileNameWithoutExtension( file );
					if ( ! fileName.StartsWith( assemblyName ) )
					{
						continue;
					}

					try
					{
						File.Delete( file );
					}
					catch( Exception ex )
					{
						Debug.WriteLine( ex );
					}
				}
			}

			return this;
		}

		public ApplicationScriptBuilder LoadReferences()
		{
			var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			foreach (string refPath in trustedAssembliesPaths)
			{
				_metadataReferences.Add(MetadataReference.CreateFromFile(refPath));
			}

			_metadataReferences.Add( MetadataReference.CreateFromFile(typeof(ApplicationScript).Assembly.Location) );

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

		public ApplicationScript Build()
		{
			try
			{
				var scriptFolder = Path.Combine( AppContext.BaseDirectory , OutputFolder );

				if ( ! Directory.Exists( scriptFolder ) )
				{
					Directory.CreateDirectory( scriptFolder );
				}

				var assemblyFile = Path.Combine( scriptFolder , UseTempAssembly ? ( Path.GetFileNameWithoutExtension( OutputAssembly ) + $".{Guid.NewGuid()}.dll" ) : OutputAssembly );

				var tree = RoslynCompilerHelper.CreateSyntaxTree( _script.Language , _script.Code );

				var compilation = RoslynCompilerHelper.CreateCompilation( _script.Language , OutputAssembly , new [] { tree } , _metadataReferences );

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
					if ( typeof( ApplicationScript ).IsAssignableFrom( type ) && ! type.IsAbstract )
					{
						return (ApplicationScript) Activator.CreateInstance( type );
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
