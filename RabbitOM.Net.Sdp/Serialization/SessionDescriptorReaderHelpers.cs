using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RabbitOM.Net.Sdp.Serialization
{
	/// <summary>
	/// Represent an helper class for the serialization
	/// </summary>
	public static class SessionDescriptorReaderHelpers
	{
		/// <summary>
		/// Extract the headers with their own value for a sdp 
		/// </summary>
		/// <param name="input">the input</param>
		/// <returns>return a collection</returns>
		public static IEnumerable<StringPair> ExtractTextFields(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				yield break;
			}

			using (var reader = new StringReader(input))
			{
				while (true)
				{
					string line = reader.ReadLine();

					if (line == null)
					{
						yield break;
					}

					if (!StringPair.TryParse(line, '=', out StringPair pair))
					{
						continue;
					}

					yield return pair;
				}
			}
		}
	}
}
