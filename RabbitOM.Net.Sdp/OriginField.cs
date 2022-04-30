using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;
using System.Globalization;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent the sdp field
	/// </summary>
	public sealed class OriginField : BaseField<OriginField> , IFormattable
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "o";





		private string      _userName    = string.Empty;

		private string      _sessionId   = string.Empty;

		private string      _version     = string.Empty;

		private NetworkType _networkType = NetworkType.None;

		private AddressType _addressType = AddressType.None;

		private string      _address     = string.Empty;





		/// <summary>
		/// Gets the type name
		/// </summary>
		public override string TypeName
		{
			get => TypeNameValue;
		}

		/// <summary>
		/// Gets / Sets the user name
		/// </summary>
		public string UserName
		{
			get => _userName;
			set => _userName = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the session identifier
		/// </summary>
		public string SessionId
		{
			get => _sessionId;
			set => _sessionId = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the session version
		/// </summary>
		public string Version
		{
			get => _version;
			set => _version = SessionDescriptorDataConverter.Trim(value);
		}

		/// <summary>
		/// Gets / Sets the network type
		/// </summary>
		public NetworkType NetworkType
		{
			get => _networkType;
			set => _networkType = value;
		}

		/// <summary>
		/// Gets / Sets the address type
		/// </summary>
		public AddressType AddressType
		{
			get => _addressType;
			set => _addressType = value;
		}

		/// <summary>
		/// Gets / Sets the unicast address
		/// </summary>
		public string Address
		{
			get => _address;
			set => _address = SessionDescriptorDataConverter.ConvertToIPAddress(value);
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
			return !string.IsNullOrWhiteSpace(_sessionId)
				&& !string.IsNullOrWhiteSpace(_version)
				&& !string.IsNullOrWhiteSpace(_address)

				&& _networkType != NetworkType.None
				&& _addressType != AddressType.None
				;
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public override void CopyFrom(OriginField field)
		{
			if ( field == null )
			{
				return;
			}

			_userName    = field._userName;
			_sessionId   = field._sessionId;
			_version     = field._version;
			_networkType = field._networkType;
			_addressType = field._addressType;
			_address     = field._address;
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
				return OriginFieldFormatter.Format(formatProvider, this);
			}

			if (format.Equals("sdp", StringComparison.OrdinalIgnoreCase))
			{
				return OriginFieldFormatter.Format(formatProvider, this);
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
		public static OriginField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return OriginFieldFormatter.TryFrom(value, out OriginField result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out OriginField result)
		{
			return OriginFieldFormatter.TryFrom(value, out result);
		}
	}
}
