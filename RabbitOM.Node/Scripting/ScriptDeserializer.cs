using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RabbitOM.Node.Scripting
{
	public static class ScriptDeserializer
	{
		public static Script Deserialize( string input )
		{
			if ( string.IsNullOrWhiteSpace( input ) )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			var root = XElement.Parse( input );

			var script = new Script
			{
				Name       = root.Element( "name" )?.Value?.Trim() ,
				Language   = root.Element( "language" )?.Value?.Trim() ,
				Code       = root.Element( "code" )?.Value ,
				Assemblies = new List<AssemblyFile>()
			};

			var assembliesNode = root.Element( "assemblies" );

			if ( assembliesNode != null )
			{
				foreach ( var assembly in assembliesNode.Elements( "assembly" ) )
				{
					script.Assemblies.Add( new AssemblyFile { Name = assembly.Value?.Trim() } );
				}
			}

			return script;
		}
	}
}
