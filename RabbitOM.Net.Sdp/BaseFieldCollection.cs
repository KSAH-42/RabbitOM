using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the base field collection class
	/// </summary>
	public class BaseFieldCollection : FieldCollection<BaseField> 
	{ 
		/// <summary>
		/// Initialize a new instance of the collection
		/// </summary>
		public BaseFieldCollection()
		{
		}


		/// <summary>
		/// Initialize a new instance of the collection
		/// </summary>
		/// <param name="collection">the collection</param>
		public BaseFieldCollection(IEnumerable<BaseField> collection)
		{
			AddRange(collection);
		}
	}
}
