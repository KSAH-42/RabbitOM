using System;
using System.Linq;
using System.Xml.Linq;

namespace RabbitOM.Player.Scripting.Models
{
	public static class Serializer
	{
		public static string Serialize( ScriptModel model )
		{
			if ( model == null )
			{
				throw new ArgumentNullException( nameof( model ) );
			}

			var root = new XElement("script" , new XElement("language", model.Language ?? string.Empty) );

			var references = new XElement("references");

			foreach (var reference in model.References ?? Enumerable.Empty<ReferenceModel>())
			{
				references.Add(new XElement("reference", new XAttribute("name", reference.Name ?? string.Empty)));
			}

			var properties = new XElement("properties");

			foreach (var property in model.Properties ?? Enumerable.Empty<PropertyModel>())
			{
				properties.Add( new XElement("property", new XAttribute("name", property.Name ?? string.Empty), property.Value ?? string.Empty ) );
			}

			root.Add(references);
			root.Add(properties);
			root.Add(new XElement("code", model.Code ?? string.Empty));

			return root.ToString();
		}

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

			foreach (var property in root.Element( "properties" )?.Elements( "property" ) ?? XElement.EmptySequence)
			{
				script.Properties.Add( new PropertyModel { Name = property.Attribute( "name" )?.Value?.Trim() , Value = property.Value?.Trim() , } );
			}

			return script;
		}
	}
}
