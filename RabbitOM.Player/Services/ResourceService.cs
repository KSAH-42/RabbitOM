using System;
using System.IO;
using System.Reflection;

namespace RabbitOM.Player.Services
{
	public static class ResourceService
	{
		public static class Names
		{
			public const string ScriptTemplate = "ScriptTemplate.txt";
		}

		public static string GetResourceFile(string resourceName)
		{
			var asm = Assembly.GetExecutingAssembly();

			using ( var stream = asm.GetManifestResourceStream($"RabbitOM.Player.Resources.{resourceName}") )
			using ( var reader = new StreamReader(stream) )
			{
				return reader.ReadToEnd();
			}
		}
	}
}
