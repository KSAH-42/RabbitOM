using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the data field token
	/// </summary>
	public sealed class StringPair
	{
		/// <summary>
		/// Represent a empty value
		/// </summary>
		public readonly static StringPair Empty = new StringPair();




		private readonly string _first  = string.Empty;

		private readonly string _second = string.Empty;







		/// <summary>
		/// Constructor
		/// </summary>
		private StringPair()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="first">the first element</param>
		/// <param name="second">the second element</param>
		public StringPair(string first, string second)
		{
			_first  = first  ?? string.Empty;
			_second = second ?? string.Empty;
		}







		/// <summary>
		/// Gets the first element
		/// </summary>
		public string First
		{
			get => _first;
		}

		/// <summary>
		/// Gets the second element
		/// </summary>
		public string Second
		{
			get => _second;
		}







		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="text">the text</param>
		/// <param name="separator">the separator</param>
		/// <param name="result">the output result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryExtractField(string text, char separator, out StringPair result)
		{
			result = StringPair.Empty;

			if (string.IsNullOrWhiteSpace(text))
			{
				return false;
			}

			int separatorIndex = text.IndexOf(separator);

			if (separatorIndex < 0)
			{
				return false;
			}

			string key = text.Remove(separatorIndex) ?? string.Empty;

			if (string.IsNullOrWhiteSpace(key))
			{
				return false;
			}

			string value = string.Empty;

			if ((separatorIndex + 1) < text.Length)
			{
				value = text.Substring(separatorIndex + 1) ?? string.Empty;
			}

			result = new StringPair(key.Trim(), value.Trim());

			return true;
		}

		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="text">the text</param>
		/// <param name="separators">the separators</param>
		/// <param name="result">the output result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryExtractField(string text, char[] separators, out StringPair result)
		{
			result = null;

			if ( separators == null)
			{
				return false;
			}

			foreach ( var seperator in separators )
			{
				if ( TryExtractField( text , seperator , out result ) )
				{
					return true;
				}
			}

			return false;
		}
	}
}
