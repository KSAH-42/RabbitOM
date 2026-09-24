using System;
using System.IO;
using System.Linq;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RabbitOM.Player.Scripting
{
	using Microsoft.CSharp;

	public sealed class PlayerScriptBuilder
	{
		public string Folder { get; set; }

		public string Code { get; set; }

		public string Language { get; set; }

		public HashSet<string> Assemblies { get; } = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
		{
			"System.dll" ,
			"System.Core.dll" ,
			"WindowsBase.dll" ,
			"RabbitOM.dll"
		};



		public string BuildAssembly()
		{
			var assemblyFile = Path.Combine( Folder ?? string.Empty , $"\\Scripts\\script{Guid.NewGuid().ToString()}.dll" );

			using ( var provider = CodeDomProviderFactory.CreateProvider( Language ) )
            {
                var parameters = new CompilerParameters
                {
                    GenerateInMemory = false,
                    GenerateExecutable = false,
                    TreatWarningsAsErrors = false,
					OutputAssembly = assemblyFile,
                };

				parameters.ReferencedAssemblies.AddRange( Assemblies.ToArray() );
				parameters.ReferencedAssemblies.Add( Assembly.GetExecutingAssembly().Location );

				var results = provider.CompileAssemblyFromSource( parameters , Code );

				if ( results.Errors.HasErrors )
				{
					if ( File.Exists(assemblyFile) )
					{
						File.Delete(assemblyFile);
					}

					throw new InvalidOperationException( new StringBuilder()
						.Append( "can not create an instance of the script" )
						.AppendLine()
						.Append( string.Join( Environment.NewLine , results.Errors ) )
						.ToString() );
				}

				return assemblyFile;
			}
		}
	}
}
