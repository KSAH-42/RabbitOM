using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RabbitOM.Node.Shell
{
	public static class WorkflowDeserializer
	{
		/*
			name: client handler

			handlers:
			  - name: a
				type: on-communication-started
				code: |
				  curl www.google.fr

			  - name: b
				type: on-communication-stopped
				code: |
				  mkdir mydirectory
		*/
		public static Workflow Deserialize( string input )
		{
			if ( string.IsNullOrWhiteSpace( input ) )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<Workflow>(input);
		}
	}
}
