using System;
using System.Linq;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RabbitOM.Node.Scripting
{
	using Microsoft.CSharp;

	public sealed class NodeScriptBuilder
	{
		public string Code { get; set; }

		public string Language { get; set; }

		public HashSet<string> Assemblies { get; } = new HashSet<string>() { "System.dll" };




		public NodeScript Build()
		{
			using ( var provider = CodeDomProviderFactory.CreateProvider( Language ) )
            {
                var parameters = new CompilerParameters
                {
                    GenerateInMemory = true,
                    TreatWarningsAsErrors = false,
                    GenerateExecutable = false
                };

				parameters.ReferencedAssemblies.AddRange( Assemblies.ToArray() );
				parameters.ReferencedAssemblies.Add( Assembly.GetExecutingAssembly().Location );

				var results = provider.CompileAssemblyFromSource( parameters , Code );

				if ( results.Errors.HasErrors )
				{
					throw new InvalidOperationException( new StringBuilder()
						.Append( "can not create an instance of the script" )
						.AppendLine()
						.Append( string.Join( Environment.NewLine , results.Errors ) )
						.ToString() );
				}

				foreach( var type in results.CompiledAssembly.GetTypes() )
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
}
