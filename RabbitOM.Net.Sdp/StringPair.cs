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



		private readonly string _first = string.Empty;

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
		public StringPair(string first)
			: this(first, string.Empty)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="first">the first element</param>
		/// <param name="second">the second element</param>
		public StringPair(string first, string second)
		{
			_first = first ?? string.Empty;
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
		/// <param name="value">the value</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="InvalidOperationException"/>
		public static StringPair Parse(string value)
		{
			return Parse(value, ':');
		}

		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="separator">the separator</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="InvalidOperationException"/>
		public static StringPair Parse(string value, char separator)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (!value.Contains(separator))
			{
				throw new ArgumentException(nameof(value));
			}

			var tokens = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

			return new StringPair(tokens.First(), tokens.ElementAtOrDefault(1));
		}




		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the output result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out StringPair result)
		{
			return TryParse(value, ':', out result);
		}

		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="separator">the separator</param>
		/// <param name="result">the output result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, char separator, out StringPair result)
		{
			result = null;

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var tokens = value.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length == 0)
			{
				return false;
			}

			result = new StringPair(tokens.ElementAtOrDefault(0), tokens.ElementAtOrDefault(1));

			return true;
		}

		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="value">the text</param>
		/// <param name="anySeparators">the separators</param>
		/// <param name="result">the output result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		internal static bool TryParse(string value, IEnumerable<char> anySeparators, out StringPair result)
		{
			result = anySeparators?.Select(separator => TryParse(value, separator, out StringPair pair) ? pair : null).Where(pairElement => pairElement != null).FirstOrDefault();

			return result != null;
		}
	}
}
