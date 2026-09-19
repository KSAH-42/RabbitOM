using System;
using System.Xml.Linq;

namespace RabbitOM.Node.Scripting.Models
{
	public static class ScriptModelDeserializer
	{
		public static ScriptModel Deserialize( string input )
		{
			if ( string.IsNullOrWhiteSpace( input ) )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			var root = XElement.Parse( input );

			var script = new ScriptModel
			{
				Name     = root.Element( "name" )?.Value?.Trim() ,
				Language = root.Element( "language" )?.Value?.Trim() ,
				Code     = root.Element( "code" )?.Value
			};

			foreach ( var assembly in root.Element( "assemblies" )?.Elements( "assembly" ) ?? XElement.EmptySequence )
			{
				script.Assemblies.Add( new ScriptAssemblyModel { Name = assembly.Value?.Trim() } );
			}

			foreach ( var property in root.Element( "properties" )?.Elements( "property" ) ?? XElement.EmptySequence )
			{
				script.Properties.Add( new ScriptPropertyModel { Name = property.Attribute( "name" )?.Value?.Trim() , Value = property.Value?.Trim() , } );
			}

			return script;
		}
	}
}
