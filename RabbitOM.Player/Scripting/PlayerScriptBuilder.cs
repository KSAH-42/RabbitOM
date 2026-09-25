using System;
using System.IO;
using System.Runtime.Loader;
using System.Text;
using Microsoft.CodeAnalysis;

namespace RabbitOM.Player.Scripting
{
	public sealed class PlayerScriptBuilder
	{
		private readonly AssemblyLoadContext _loadContext;



		public PlayerScriptBuilder( AssemblyLoadContext loadContext )
		{
			_loadContext = loadContext ?? throw new ArgumentNullException( nameof( loadContext ) );
		}



		public string ScriptFileName { get; set; } = "playerScript.dll";

		public string Code { get; set; }

		public string Language { get; set; }

		public HashSet<string> References { get; } = new HashSet<string>( StringComparer.OrdinalIgnoreCase ) { "RabbitOM.dll" };



		public void ClearOutputDirectory()
		{
			var assemblyFileName = Path.Combine( AppContext.BaseDirectory , ScriptFileName );

			if ( File.Exists( assemblyFileName ) )
			{
				File.Delete( assemblyFileName );
			}
		}

		public PlayerScript Build()
		{
			var references = new List<MetadataReference>();

			references.Add(MetadataReference.CreateFromFile(typeof(PlayerScript).Assembly.Location));

			foreach ( var reference in References )
			{
				references.Add( MetadataReference.CreateFromFile( reference ) );
			}

			var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			foreach (string refPath in trustedAssembliesPaths)
			{
				references.Add(MetadataReference.CreateFromFile(refPath));
			}

			var tree = RoslynCompilerHelper.CreateSyntaxTree( Language , Code );

			var compilation = RoslynCompilerHelper.CreateCompilation( Language , ScriptFileName , new [] { tree } , references );

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

			var assembly = _loadContext.LoadFromStream( memoryStream );

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
