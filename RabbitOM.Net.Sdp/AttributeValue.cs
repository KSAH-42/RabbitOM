namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp value content
	/// </summary>
	public abstract class AttributeValue
	{
		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public abstract bool Validate();
	}
}
