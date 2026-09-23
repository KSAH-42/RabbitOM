using System;
using System.IO;
using System.Reflection;

namespace RabbitOM.Player.Resources
{
	public static class ResourceManager
	{
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
