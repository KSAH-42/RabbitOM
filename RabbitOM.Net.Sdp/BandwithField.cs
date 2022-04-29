
using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp document bandwith infos
	/// </summary>
	public sealed class BandwithField : BaseField<BandwithField> , IFormattable
	{
		/// <summary>
		/// Represent a modifier name
		/// </summary>
		public const string ConferenceTotal     = "CT";

		/// <summary>
		/// Represent a modifier name
		/// </summary>
		public const string ApplicationSpecific = "AS";

		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue       = "b";






		private string _modifier = string.Empty;

		private long   _value    = 0;






		/// <summary>
		/// Constructor
		/// </summary>
		public BandwithField()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="modifier">the modifier</param>
		/// <param name="value">the value</param>
		public BandwithField(string modifier, int value)
		{
			Modifier = modifier;
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
		/// Gets / Sets the modifier
		/// </summary>
		public string Modifier
		{
			get => _modifier;
			set => _modifier = SessionDescriptorDataConverter.Trim(value);
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
			return !string.IsNullOrEmpty(_modifier);
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public override void CopyFrom(BandwithField field)
		{
			if (field == null || object.ReferenceEquals(field, this))
			{
				return;
			}

			_modifier = field._modifier;
			_value = field._value;
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
				return BandwithFieldFormatter.Format(this, format, formatProvider);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return BandwithFieldFormatter.Format(this, format, formatProvider);
			}

			throw new FormatException();
		}





		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="FormatException"/>
		public static BandwithField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			if (!BandwithFieldFormatter.TryFrom(value, out BandwithField result) || result == null)
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
		public static bool TryParse(string value, out BandwithField result)
		{
			return BandwithFieldFormatter.TryFrom(value, out result);
		}
	}
}
