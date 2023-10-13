using System;
using System.Linq;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent a tolerant data type converter that doesn't raise any exceptions and also returns default value in cases of failures.
	/// </summary>
	public static class SessionDescriptorDataConverter
	{
		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static string ConvertToString(bool value)
		{
			return value.ToString();
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static string ConvertToString(byte value)
		{
			return value.ToString();
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static string ConvertToString(int value)
		{
			return value.ToString();
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static string ConvertToString(long value)
		{
			return value.ToString();
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static bool ConvertToBool(string value)
		{
			return bool.TryParse(value ?? string.Empty, out bool result) && result;
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static int ConvertToInt(string value)
		{
			return int.TryParse(value ?? string.Empty, out int result) ? result : 0;
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static uint ConvertToUInt(string value)
		{
			return uint.TryParse(value ?? string.Empty, out uint result) ? result : 0;
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static long ConvertToLong(string value)
		{
			return long.TryParse(value ?? string.Empty, out long result) ? result : 0;
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static long? ConvertToNullableLong(string value)
		{
			return long.TryParse(value ?? string.Empty, out long result) ? new Nullable<long>(result) : null;
		}

		/// <summary>
		/// Convert a value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static byte ConvertToByte(string value)
		{
			return byte.TryParse(value ?? string.Empty, out byte result) ? result : byte.MinValue;
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a string</returns>
		public static string ConvertToString(AddressType value)
		{
			switch (value)
			{
				case AddressType.IPV4: return "IP4";
				case AddressType.IPV6: return "IP6";
			}

			return "NONE";
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a string</returns>
		public static string ConvertToString(MediaType value)
		{
			switch (value)
			{
				case MediaType.Application: return "application";
				case MediaType.Audio: return "audio";
				case MediaType.Text: return "text";
				case MediaType.Video: return "video";
				case MediaType.Message: return "message";
			}

			return "NONE";
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a string</returns>
		public static string ConvertToString(NetworkType value)
		{
			return (value == NetworkType.Internet) ? "IN" : "NONE";
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a string</returns>
		public static string ConvertToString(ProfileType value)
		{
			switch (value)
			{
				case ProfileType.AVP: return "AVP";
				case ProfileType.SAVP: return "SAVP";
			}

			return "NONE";
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a string</returns>
		public static string ConvertToString(ProtocolType value)
		{
			return value == ProtocolType.RTP ? "RTP" : "NONE";
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static AddressType ConvertToAddressType(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return AddressType.None;
			}

			string text = value.Trim() ?? string.Empty;

			if (string.Compare(text, "IP4", true) == 0 || string.Compare(text, "IPV4", true) == 0)
			{
				return AddressType.IPV4;
			}

			if (string.Compare(text, "IP6", true) == 0 || string.Compare(text, "IPV6", true) == 0)
			{
				return AddressType.IPV6;
			}

			return AddressType.None;
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static MediaType ConvertToMediaType(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return MediaType.None;
			}

			string text = value.Trim() ?? string.Empty;

			if (string.Compare(text, "video", true) == 0)
			{
				return MediaType.Video;
			}

			if (string.Compare(text, "application", true) == 0)
			{
				return MediaType.Application;
			}

			if (string.Compare(text, "audio", true) == 0)
			{
				return MediaType.Audio;
			}

			if (string.Compare(text, "text", true) == 0)
			{
				return MediaType.Text;
			}

			if (string.Compare(text, "message", true) == 0)
			{
				return MediaType.Message;
			}

			return MediaType.None;
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static NetworkType ConvertToNetworkType(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return NetworkType.None;
			}

			string text = value.Trim() ?? string.Empty;

			if (string.Compare(text, "IN", true) == 0)
			{
				return NetworkType.Internet;
			}

			return NetworkType.None;
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static ProfileType ConvertToProfileType(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return ProfileType.None;
			}

			string text = value.Trim() ?? string.Empty;

			if (string.Compare(text, "AVP", true) == 0)
			{
				return ProfileType.AVP;
			}

			if (string.Compare(text, "SAVP", true) == 0)
			{
				return ProfileType.SAVP;
			}

			return ProfileType.None;
		}

		/// <summary>
		/// Convert
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static ProtocolType ConvertToProtocolType(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return ProtocolType.None;
			}

			string text = value.Trim() ?? string.Empty;

			if (string.Compare(text, "RTP", true) == 0)
			{
				return ProtocolType.RTP;
			}

			return ProtocolType.None;
		}


		/// <summary>
		/// Convert to a string value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a trimed value</returns>
		public static string ConvertToIPAddress(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

			return value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
		}

		/// <summary>
		/// Convert to a byte value
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a value</returns>
		public static byte ConvertToByteFromTTLFormat(string value)
		{							   
			if (string.IsNullOrWhiteSpace(value))
			{
				return byte.MinValue;
			}

			var tokens = value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length < 2)
			{
				return byte.MinValue;
			}

			return byte.TryParse(tokens[1], out byte result) ? result : byte.MinValue;
		}

		/// <summary>
		/// Filter occurcences
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a trimed value</returns>
		public static string Filter(string value)
		{
			return Filter(value, true);
		}

		/// <summary>
		/// Filter occurcences
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="ignoreQuotes">set true to ignore quotes</param>
		/// <returns>returns a trimed value</returns>
		public static string Filter(string value, bool ignoreQuotes)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

			if (ignoreQuotes)
			{
				return RemoveOccurrences(value, '"', '\'')?.Trim() ?? string.Empty;
			}

			return value.Trim() ?? string.Empty;
		}

		/// <summary>
		/// Filter occurcences
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns a trimed value</returns>
		public static string FilterAsEmailFormat(string value)
		{
			return Filter(RemoveOccurrences(value, '(', ')', '<', '>', '[', ']', '{', '}'));
		}

		/// <summary>
		/// Remove characters
		/// </summary>
		/// <param name="value">the input value</param>
		/// <param name="characters">a collection of illegals caracters</param>
		/// <returns>returns a string</returns>
		public static string RemoveOccurrences(string value, params char[] characters)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

			if (characters == null || characters.Length <= 0)
			{
				return string.Empty;
			}

			string text = value.Trim();

			foreach (var character in characters)
			{
				text = text.Replace(character.ToString(), "");
			}

			return text;
		}


		/// <summary>
		/// Extract a field as a key pair value
		/// </summary>
		/// <param name="text">the text</param>
		/// <param name="separator">the separator</param>
		/// <returns>returns a field,otherwise null</returns>
		public static StringPair ExtractField(string text, char separator)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}

			int separatorIndex = text.IndexOf(separator);

			if (separatorIndex < 0)
			{
				return null;
			}

			string key = text.Remove(separatorIndex) ?? string.Empty;

			if (string.IsNullOrWhiteSpace(key))
			{
				return null;
			}

			string value = string.Empty;

			if ((separatorIndex + 1) < text.Length)
			{
				value = text.Substring(separatorIndex + 1) ?? string.Empty;
			}

			return new StringPair(key.Trim(), value.Trim());
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

			try
			{
				result = ExtractField(text, separator);

				return result != null && !string.IsNullOrWhiteSpace(result.First);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}

			return false;
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

			if (separators != null)
			{
				foreach (var seperator in separators)
				{
					if (TryExtractField(text, seperator, out result))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
