using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	/// <remarks>
	/// <para>This class has been introduce to act as a value object AND to avoid the primitive type obsession</para>
	/// <para>The session descriptor can expose primitive type but for maintenance reason, an object will be introduce even for tiny object</para>
	/// </remarks>
	public sealed class VersionField : BaseField<VersionField>
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "v";




		private long _value = 0;




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
		public long Value
		{
			get => _value;
			set => _value = value;
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
			return _value >= 0;
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public override void CopyFrom(VersionField field)
		{
			if (field == null || object.ReferenceEquals(field, this))
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
			return _value.ToString();
		}





		/// <summary>
		/// Parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="FormatException"/>
		public static VersionField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			if (!TryParse(value, out VersionField result) || result == null)
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
		public static bool TryParse(string value, out VersionField result)
		{
			result = new VersionField()
			{
				Value = SessionDescriptorDataConverter.ConvertToLong(value)
			};

			return true;
		}
	}
}
