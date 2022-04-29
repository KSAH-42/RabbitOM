using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	/// <typeparam name="TField">type of field</typeparam>
	public abstract class BaseField<TField> : BaseField , ICopyable<TField> where TField : BaseField
	{
		/// <summary>
		/// Perform a copy from a different instance object
		/// </summary>
		/// <param name="obj">the source used to perform a copy</param>
		public abstract void CopyFrom(TField obj);
	}
}
