using System;
using System.Xml.Linq;

namespace RabbitOM.Node.Models
{
	public static class Deserializer
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
				Language = root.Attribute( "language" )?.Value?.Trim() ,
				Code     = root.Element( "code" )?.Value
			};

			foreach (var reference in root.Element( "references" )?.Elements( "reference" ) ?? XElement.EmptySequence)
			{
				script.References.Add( new ScriptReferenceModel { Name = reference.Attribute( "name" )?.Value?.Trim() , ForceLoad = bool.Parse( reference.Attribute( "forceload" )?.Value?.Trim() ?? "false" ) } );
			}

			foreach ( var property in root.Element( "properties" )?.Elements( "property" ) ?? XElement.EmptySequence )
			{
				script.Properties.Add( new ScriptPropertyModel { Name = property.Attribute( "name" )?.Value?.Trim() , Value = property.Value?.Trim() } );
			}

			return script;
		}
	}
}
