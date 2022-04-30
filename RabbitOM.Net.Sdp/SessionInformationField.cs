using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public sealed class SessionInformationField : BaseField<SessionInformationField>
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "i";




		private string _value = string.Empty;




		/// <summary>
		/// Initialize a new instance of a session information field
		/// </summary>
		public SessionInformationField()
		{
		}

		/// <summary>
		/// Initialize a new instance of a session information field
		/// </summary>
		/// <param name="value">the value</param>
		public SessionInformationField( string value )
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
			return !string.IsNullOrWhiteSpace(_value);
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public override void CopyFrom(SessionInformationField field)
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
		public static SessionInformationField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return TryParse(value, out SessionInformationField result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out SessionInformationField result)
		{
			result = new SessionInformationField()
			{
				Value = value
			};

			return true;
		}
	}
}
