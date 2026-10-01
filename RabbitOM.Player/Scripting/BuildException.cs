using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Player.Scripting
{
	public sealed class BuildException : Exception
	{
		private readonly List<string> _buildErrors = new List<string>();

		public BuildException() : base()
		{
		}

		public BuildException( string message ) : base( message )
		{
		}

		public BuildException( string message , Exception innerException ) : base( message , innerException )
		{
		}

		public BuildException( string message , Exception innerException , IEnumerable<string> errors ) : base ( message , innerException )
		{
			_buildErrors.AddRange( errors ?? Array.Empty<string>() );
		}

		public BuildException( string message , IEnumerable<string> errors ) : base ( message )
		{
			_buildErrors.AddRange( errors ?? Array.Empty<string>() );
		}

		public IReadOnlyList<string> BuildErrors { get => _buildErrors; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.AppendLine( base.ToString() );

			foreach ( var error in _buildErrors )
			{
				builder.AppendLine( $"  error: {error}" );
			}

			return builder.ToString();
		}
	}
}
