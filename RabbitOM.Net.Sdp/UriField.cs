using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent a sdp field
	/// </summary>
	public sealed class UriField : BaseField, ICopyable<UriField>
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "u";




		private string _value = string.Empty;




		/// <summary>
		/// Initialize a new instance of a uri field
		/// </summary>
		public UriField()
		{
		}

		/// <summary>
		/// Initialize a new instance of a uri field
		/// </summary>
		/// <param name="value">the value</param>
		public UriField( string value )
		{
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
		/// Gets / Sets the uri
		/// </summary>
		public string Value
		{
			get => _value;
			set => _value = DataConverter.Filter(value);
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
			if ( string.IsNullOrWhiteSpace( _value ) )
			{
				return false;
			}

			return Uri.IsWellFormedUriString(_value, UriKind.RelativeOrAbsolute);
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public void CopyFrom(UriField field)
		{
			if ( field == null )
			{
				return;
			}

			_value = field._value;
		}

		/// <summary>
		/// Format the field
		/// </summary>
		/// <returns>retuns a value</returns>
		public override string ToString()
		{
			return _value;
		}




		/// <summary>
		/// Parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="FormatException"/>
		public static UriField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return TryParse(value, out UriField result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out UriField result)
		{
			result = new UriField()
			{
				Value = value
			};

			return true;
		}
	}
}
