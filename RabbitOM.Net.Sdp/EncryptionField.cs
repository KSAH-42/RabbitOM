using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public sealed class EncryptionField : BaseField<EncryptionField> , IFormattable
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "k";





		private string _method = string.Empty;

		private string _key    = string.Empty;




		/// <summary>
		/// Initialize a new instance of the encryption field
		/// </summary>
		public EncryptionField()
		{
		}


		/// <summary>
		/// Initialize a new instance of the encryption field
		/// </summary>
		/// <param name="method">the method</param>
		/// <param name="key">the key</param>
		public EncryptionField( string method , string key )
		{
			Method = method;
			Key    = key;
		}



		/// <summary>
		/// Gets the type name
		/// </summary>
		public override string TypeName
		{
			get => TypeNameValue;
		}

		/// <summary>
		/// Gets / Sets the method
		/// </summary>
		public string Method
		{
			get => _method;
			set => _method = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the key
		/// </summary>
		public string Key
		{
			get => _key;
			set => _key = SessionDescriptorDataConverter.Trim(value);
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
			return !string.IsNullOrWhiteSpace(_method);
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public override void CopyFrom(EncryptionField field)
		{
			if ( field == null )
			{
				return;
			}

			_method = field._method;
			_key    = field._key;
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
				return EncryptionFieldFormatter.Format(formatProvider, this);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return EncryptionFieldFormatter.Format(formatProvider, this);
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
		public static EncryptionField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return EncryptionFieldFormatter.TryFrom(value, out EncryptionField result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out EncryptionField result)
		{
			return EncryptionFieldFormatter.TryFrom(value, out result);
		}
	}
}
