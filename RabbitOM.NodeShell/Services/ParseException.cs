using System;
using System.Runtime.Serialization;

namespace RabbitOM.NodeShell.Services
{
	[Serializable]
	public class ParseException : Exception
	{
		public ParseException()
		{
		}

		public ParseException( string message ) : base( message )
		{
		}

		public ParseException( string message , Exception innerException ) : base( message , innerException )
		{
		}

		protected ParseException( SerializationInfo info , StreamingContext context ) : base( info , context )
		{
		}
	}
}