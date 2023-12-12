using System;

namespace RabbitOM.Net.Sdp.Validation
{
	/// <summary>
	/// Represent a validation exception
	/// </summary>
	[Serializable] public sealed class ValidationException : Exception
	{
		/// <summary>
		/// Initialize an new instance of the validation exception
		/// </summary>
		public ValidationException() 
			: this ( "Validation failed" )
		{
		}

		/// <summary>
		/// Initialize an new instance of the validation exception
		/// </summary>
		/// <param name="message">the message</param>
		public ValidationException(string message) 
			: base ( message ) 
		{
		}

		/// <summary>
		/// Initialize an new instance of the validation exception
		/// </summary>
		/// <param name="message">the message</param>
		/// <param name="inner">the inner exception</param>
		public ValidationException(string message, Exception inner) 
			: base( message , inner )
		{
		}
	}
}
