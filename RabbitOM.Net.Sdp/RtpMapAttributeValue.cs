using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the rtp map
	/// </summary>
	public sealed class RtpMapAttributeValue : AttributeValue<RtpMapAttributeValue>, IFormattable
	{
		private string                 _encoding     = string.Empty;

		private byte                   _payloadType  = 0;

		private uint                   _clockRate    = 0;

		private readonly ExtensionList _extensions   = new ExtensionList();



		/// <summary>
		/// Gets / Sets the encoding
		/// </summary>
		public string Encoding
		{
			get => _encoding;
			set => _encoding = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the payload type
		/// </summary>
		public byte PayloadType
		{
			get => _payloadType;
			set => _payloadType = value;
		}

		/// <summary>
		/// Gets / Sets the clock rate
		/// </summary>
		public uint ClockRate
		{
			get => _clockRate;
			set => _clockRate = value;
		}

		/// <summary>
		/// Gets the extensions data
		/// </summary>
		public ExtensionList Extensions
		{
			get => _extensions;
		}




		/// <summary>
		/// Validate
		/// </summary>
		/// <exception cref="Exception"/>
		public override void Validate()
		{
			if ( ! TryValidate() )
			{
				throw new Exception("Validation failed");
			}
		}

		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		/// <remarks>
		///   <para>a payload value with a null value is considered as valid value</para>
		/// </remarks>
		public override bool TryValidate()
		{
			return !string.IsNullOrWhiteSpace(_encoding) && _clockRate > 0;
		}

		/// <summary>
		/// Copy from
		/// </summary>
		/// <param name="info">the object</param>
		public override void CopyFrom(RtpMapAttributeValue info)
		{
			if (info == null || object.ReferenceEquals(this, info))
			{
				return;
			}

			_clockRate   = info._clockRate;
			_payloadType = info._payloadType;
			_encoding    = info._encoding;

			_extensions.Clear();

			_extensions.TryAddRange(info.Extensions);
		}

		/// <summary>
		/// Format the field
		/// </summary>
		/// <returns>retuns a value</returns>
		public override string ToString()
		{
			return ToString(null);
		}

		/// <summary>
		/// Format the field
		/// </summary>
		/// <param name="format">the format</param>
		/// <returns>retuns a value</returns>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Format the field
		/// </summary>
		/// <param name="format">the format</param>
		/// <param name="formatProvider">the format provider</param>
		/// <returns>retuns a value</returns>
		/// <exception cref="FormatException"/>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if ( string.IsNullOrEmpty( format ) )
			{
				return RtpMapAttributeValueFormatter.Format(this, format, formatProvider);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return RtpMapAttributeValueFormatter.Format(this, format, formatProvider);
			}

			throw new FormatException();
		}




		/// <summary>
		/// Parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="FormatException"/>
		public static RtpMapAttributeValue Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			if ( ! RtpMapAttributeValueFormatter.TryFrom(value, out RtpMapAttributeValue result) )
			{
				throw new FormatException();
			}

			return result;
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out RtpMapAttributeValue result)
		{
			return RtpMapAttributeValueFormatter.TryFrom(value, out result);
		}
	}
}
