using System;
using System.Xml.Linq;

namespace RabbitOM.Node.Scripting.Models
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
				Language = root.Element( "language" )?.Value?.Trim() ,
				Code     = root.Element( "code" )?.Value
			};

			foreach (var reference in root.Element( "references" )?.Elements( "reference" ) ?? XElement.EmptySequence)
			{
				script.References.Add( new ReferenceModel { Name = reference.Attribute( "name" )?.Value?.Trim() } );
			}

			foreach ( var property in root.Element( "properties" )?.Elements( "property" ) ?? XElement.EmptySequence )
			{
				script.Properties.Add( new PropertyModel { Name = property.Attribute( "name" )?.Value?.Trim() , Value = property.Value?.Trim() , } );
			}

			return script;
		}
	}
}
