using System;
using System.CodeDom.Compiler;

namespace RabbitOM.Player.Scripting
{
	using Microsoft.CSharp;
	using Microsoft.VisualBasic;

	internal static class CodeDomProviderFactory
	{
		private readonly static StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;

		public static CodeDomProvider CreateProvider( string language )
		{
			if ( string.IsNullOrWhiteSpace( language ) )
			{
				throw new ArgumentNullException( nameof( language ) );
			}

			if ( ValueComparer.Equals( language , "csharp" ) )
			{
				return new CSharpCodeProvider();
			}

			if ( ValueComparer.Equals( language , "vb" ) )
			{
				return new VBCodeProvider();
			}

			throw new NotSupportedException( $"the language {language} is not supported" );
		}
	}
}
