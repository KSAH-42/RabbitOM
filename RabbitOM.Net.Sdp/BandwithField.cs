using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp document bandwith infos
	/// </summary>
	public sealed class BandwithField : BaseField , ICopyable<BandwithField>
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
			Value    = value;
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
			set => _modifier = DataConverter.Filter(value);
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
		public void CopyFrom(BandwithField field)
		{
			if ( field == null )
			{
				return;
			}

			_modifier = field._modifier;
			_value    = field._value;
		}
		
		/// <summary>
		/// Format the field
		/// </summary>
		/// <returns>retuns a value</returns>
		public override string ToString()
		{
			return BandwithFieldFormatter.Format( this );
		}





		/// <summary>
		/// Parse
		/// </summary>
		/// <param name="value">the input text</param>
		/// <returns>returns an instance</returns>
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

			return BandwithFieldFormatter.TryParse(value, out BandwithField result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out BandwithField result)
		{
			return BandwithFieldFormatter.TryParse(value, out result);
		}
	}
}
