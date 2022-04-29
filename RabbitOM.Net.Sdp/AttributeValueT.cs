namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp value content
	/// </summary>
	public abstract class AttributeValue<TAttributeValue> : AttributeValue , ICopyable<TAttributeValue> where TAttributeValue : AttributeValue
	{
		/// <summary>
		/// Perform a copy from a different instance object
		/// </summary>
		/// <param name="obj">the source used to perform a copy</param>
		public abstract void CopyFrom(TAttributeValue obj);
	}
}
