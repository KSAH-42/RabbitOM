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
				Name     = root.Element( "name" )?.Value?.Trim() ,
				Language = root.Element( "language" )?.Value?.Trim() ,
				Code     = root.Element( "code" )?.Value
			};

			var assembliesNode = root.Element( "assemblies" );

			if ( assembliesNode != null )
			{
				script.Assemblies = new List<AssemblyFile>();

				foreach ( var assembly in assembliesNode.Elements( "property" ) )
				{
					script.Assemblies.Add( new AssemblyFile { Name = assembly.Value?.Trim() } );
				}
			}

			var propertiesNode = root.Element( "properties" );

			if ( propertiesNode != null )
			{
				script.Properties = new List<Property>();

				foreach ( var property in propertiesNode.Elements( "property" ) )
				{
					script.Properties.Add( new Property
					{
						Name = property.Attribute( "name" )?.Value?.Trim() ,
						Value = property.Value?.Trim() ,
					} );
				}
			}

			return script;
		}
	}
}
