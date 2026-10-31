using System;
using System.Xml.Linq;

namespace RabbitOM.Player.Data
{
	public static class ScriptSerializer
	{
		public static string Serialize( Script model )
		{
			if ( model == null )
			{
				throw new ArgumentNullException( nameof( model ) );
			}

			var root = new XElement("script" , new XAttribute("language", model.Language ?? string.Empty) );

			var references = new XElement("references");

			foreach (var reference in model.References ?? [] )
			{
				references.Add( new XElement("reference", 
					new XAttribute("name", reference.Name ?? string.Empty ),
					new XAttribute("forceload", reference.ForceLoad.ToString() ) ));
			}

			var properties = new XElement("properties");

			foreach (var property in model.Properties ?? [] )
			{
				properties.Add( new XElement("property", new XAttribute("name", property.Name ?? string.Empty), property.Value ?? string.Empty ) );
			}

			root.Add(references);
			root.Add(properties);
			root.Add(new XElement("code", model.Code ?? string.Empty));

			return root.ToString();
		}

		public static Script Deserialize( string input )
		{
			if ( string.IsNullOrWhiteSpace( input ) )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			var root = XElement.Parse( input );

			var script = new Script
			{
				Language = root.Attribute( "language" )?.Value?.Trim(),
				Code = root.Element( "code" )?.Value,
				References = [],
				Properties = [],
			};

			foreach (var reference in root.Element( "references" )?.Elements( "reference" ) ?? XElement.EmptySequence)
			{
				script.References.Add( new ScriptReference {
					Name = reference.Attribute( "name" )?.Value?.Trim() ,
					ForceLoad = bool.Parse( reference.Attribute( "forceload" )?.Value ?? "false" ) } );
			}

			foreach (var property in root.Element( "properties" )?.Elements( "property" ) ?? XElement.EmptySequence)
			{
				script.Properties.Add( new ScriptProperty {
					Name = property.Attribute( "name" )?.Value?.Trim(),
					Value = property.Value?.Trim() } );
			}

			return script;
		}
	}
}
