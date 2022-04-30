using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public sealed class EmailField : BaseField<EmailField> , IFormattable
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "e";






		private string _address = string.Empty;

		private string _name    = string.Empty;






		/// <summary>
		/// Constructor
		/// </summary>
		public EmailField()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="address">the address</param>
		public EmailField(string address) : this(address, string.Empty)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="address">the address</param>
		/// <param name="name">the name</param>
		public EmailField(string address, string name)
		{
			Address = address;
			Name    = name;
		}






		/// <summary>
		/// Gets the type name
		/// </summary>
		public override string TypeName
		{
			get => TypeNameValue;
		}

		/// <summary>
		/// Gets / Sets the address
		/// </summary>
		public string Address
		{
			get => _address;
			set => _address = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the name
		/// </summary>
		public string Name
		{
			get => _name;
			set => _name = SessionDescriptorDataConverter.TrimAsEmailFormat(value);
		}





		/// <summary>
		/// Validate
		/// </summary>
		/// <exception cref="Exception"/>
		public override void Validate()
		{
			if (!TryValidate())
			{
				throw new Exception("Validation failed");
			}
		}

		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryValidate()
		{
			if (string.IsNullOrWhiteSpace(_address))
			{
				return false;
			}

			return Uri.IsWellFormedUriString(_address, UriKind.Relative);
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public override void CopyFrom(EmailField field)
		{
			if ( field == null )
			{
				return;
			}

			_name = field._name;
			_address = field._address;
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
				return EmailFieldFormatter.Format(this, format, formatProvider);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return EmailFieldFormatter.Format(this, format, formatProvider);
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
		public static EmailField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return EmailFieldFormatter.TryFrom(value, out EmailField result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out EmailField result)
		{
			return EmailFieldFormatter.TryFrom(value, out result);
		}
	}
}
