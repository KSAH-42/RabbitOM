using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public sealed class MediaDescriptionField : BaseField, IFormattable
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "m";




		private MediaType _type = MediaType.None;

		private int _port = 0;

		private ProtocolType _protocol = ProtocolType.None;

		private ProfileType _profile = ProfileType.None;

		private int _payload = 0;

		private readonly ConnectionField _connection = new ConnectionField();

		private readonly EncryptionField _encryption = new EncryptionField();

		private readonly BandwithFieldCollection _bandwiths = new BandwithFieldCollection();

		private readonly AttributeFieldCollection _attributes = new AttributeFieldCollection();




		/// <summary>
		/// Gets the type name
		/// </summary>
		public override string TypeName
		{
			get => TypeNameValue;
		}

		/// <summary>
		/// Gets / Sets the type
		/// </summary>
		public MediaType Type
		{
			get => _type;
			set => _type = value;
		}

		/// <summary>
		/// Gets / Sets the port
		/// </summary>
		public int Port
		{
			get => _port;
			set => _port = value;
		}

		/// <summary>
		/// Gets / Sets the protocol
		/// </summary>
		public ProtocolType Protocol
		{
			get => _protocol;
			set => _protocol = value;
		}

		/// <summary>
		/// Gets / Sets the profile
		/// </summary>
		public ProfileType Profile
		{
			get => _profile;
			set => _profile = value;
		}

		/// <summary>
		/// Gets / Sets the payload
		/// </summary>
		public int Payload
		{
			get => _payload;
			set => _payload = value;
		}

		/// <summary>
		/// Gets the connection
		/// </summary>
		public ConnectionField Connection
		{
			get => _connection;
		}

		/// <summary>
		/// Gets the encryption
		/// </summary>
		public EncryptionField Encryption
		{
			get => _encryption;
		}

		/// <summary>
		/// Gets the bandwith collection
		/// </summary>
		public BandwithFieldCollection Bandwiths
		{
			get => _bandwiths;
		}

		/// <summary>
		/// Gets the attributes collections
		/// </summary>
		public AttributeFieldCollection Attributes
		{
			get => _attributes;
		}




		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryValidate()
		{
			return _payload > 0
				&& _type != MediaType.None
				&& _protocol != ProtocolType.None
				&& _profile != ProfileType.None
				;
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
				return MediaDescriptionFieldFormatter.Format(this, format, formatProvider);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return MediaDescriptionFieldFormatter.Format(this, format, formatProvider);
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
		public static MediaDescriptionField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			if (!MediaDescriptionFieldFormatter.TryParse(value, out MediaDescriptionField result) || result == null)
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
		public static bool TryParse(string value, out MediaDescriptionField result)
		{
			return MediaDescriptionFieldFormatter.TryParse(value, out result);
		}
	}
}
