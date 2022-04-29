using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public abstract class BaseField
	{
		/// <summary>
		/// Gets the type name
		/// </summary>
		public abstract string TypeName
		{
			get;
		}




		/// <summary>
		/// Validate
		/// </summary>
		/// <exception cref="Exception"/>
		public virtual void Validate()
		{
			if (!TryValidate())
			{
				throw new Exception("Validation failed");
			}
		}

		/// <summary>
		/// Try to validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool TryValidate();
	}
}
