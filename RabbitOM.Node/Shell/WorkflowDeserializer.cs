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
			  - name: output file
				type: on-communication-started
				code: |
				  curl -s www.google.fr >> c:\projects\curl-output.txt

			  - name: create temp directory
				type: on-communication-stopped
				code: |
				  powershell -command "mkdir tempdir"
		          powershell -command "cat c:\projects\curl-output.txt >> c:\projects\curl-output2.txt"
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
