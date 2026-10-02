using System;
using System.IO;
using System.Reflection;

namespace RabbitOM.Player.Services
{
	public static class ResourceService
	{
		public const string ScriptTemplate = "ScriptTemplate.txt";

		public static string GetResourceFile(string resourceName)
		{
			var assembly = Assembly.GetExecutingAssembly();

			using ( var stream = assembly.GetManifestResourceStream($"RabbitOM.Player.Resources.{resourceName}") )
			using ( var reader = new StreamReader(stream) )
			{
				return reader.ReadToEnd();
			}
		}
	}
}
