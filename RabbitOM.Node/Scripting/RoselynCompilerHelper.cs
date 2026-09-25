using System;
using System.IO;
using System.Collections.Generic;

namespace RabbitOM.Node.Scripting
{
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.CSharp;
	using Microsoft.CodeAnalysis.VisualBasic;

	internal static class RoselynCompilerHelper
	{
		private readonly static StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;

		public static SyntaxTree CreateSyntaxTree( string language , string code )
		{
			if ( string.IsNullOrWhiteSpace( language ) )
			{
				throw new ArgumentNullException( nameof( language ) );
			}

			if ( string.IsNullOrWhiteSpace( code ) )
			{
				throw new ArgumentNullException( nameof( code ) );
			}

			if ( ValueComparer.Equals( language , "csharp" ) )
			{
				return CSharpSyntaxTree.ParseText( code );
			}

			if ( ValueComparer.Equals( language , "vb" ) )
			{
				return VisualBasicSyntaxTree.ParseText( code );
			}

			throw new NotSupportedException( $"the language {language} is not supported" );
		}

		public static Compilation CreateCompilation( string language , string assemblyName , IEnumerable<SyntaxTree> trees , IEnumerable<MetadataReference> references )
		{
			if ( string.IsNullOrWhiteSpace( language ) )
			{
				throw new ArgumentNullException( nameof( language ) );
			}

			if ( string.IsNullOrWhiteSpace( assemblyName ) )
			{
				throw new ArgumentNullException( nameof( assemblyName ) );
			}

			if ( trees == null )
			{
				throw new ArgumentNullException( nameof( trees ) );
			}

			if ( references == null )
			{
				throw new ArgumentNullException( nameof( references ) );
			}

			var fileName = Path.Combine( AppContext.BaseDirectory , assemblyName );

			if ( File.Exists( fileName ) )
			{
				File.Delete( fileName );
			}

			if ( ValueComparer.Equals( language , "csharp" ) )
			{
				var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release );

				return CSharpCompilation.Create( assemblyName , trees , references , options: compilationOptions );
			}

			if ( ValueComparer.Equals( language , "vb" ) )
			{
				var compilationOptions = new VisualBasicCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release );

				return VisualBasicCompilation.Create( assemblyName , trees , references , options: compilationOptions );
			}

			throw new NotSupportedException( $"the language {language} is not supported" );
		}
	}
}
