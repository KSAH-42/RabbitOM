using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public sealed class AttributeField : BaseField, IFormattable
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "a";





		private string _name = string.Empty;

		private string _value = string.Empty;





		/// <summary>
		/// Constructor
		/// </summary>
		public AttributeField()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">the name</param>
		public AttributeField(string name) : this(name, string.Empty)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="value">the value</param>
		public AttributeField(string name, string value)
		{
			Name = name;
			Value = value;
		}





		/// <summary>
		/// Gets the type name
		/// </summary>
		public override string TypeName
		{
			get => TypeNameValue;
		}

		/// <summary>
		/// Gets / Sets the name
		/// </summary>
		public string Name
		{
			get => _name;
			set => _name = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the value
		/// </summary>
		public string Value
		{
			get => _value;
			set => _value = SessionDescriptorDataConverter.Trim(value);
		}





		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryValidate()
		{
			return !string.IsNullOrWhiteSpace(_name);
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
			if (string.IsNullOrWhiteSpace(format))
			{
				return AttributeFieldFormatter.Format(this, format, formatProvider);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return AttributeFieldFormatter.Format(this, format, formatProvider);
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
		public static AttributeField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			if (!AttributeFieldFormatter.TryParse(value, out AttributeField result) || result == null)
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
		public static bool TryParse(string value, out AttributeField result)
		{
			return AttributeFieldFormatter.TryParse(value, out result);
		}
	}
}
