using RabbitOM.Net.Sdp.Serialization.Formatters;
using System;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent a sdp field
	/// </summary>
	public sealed class ConnectionField : BaseField, ICopyable<ConnectionField>
	{
		/// <summary>
		/// Represent the type name
		/// </summary>
		public const string TypeNameValue = "c";






		private NetworkType _networkType = NetworkType.None;

		private AddressType _addressType = AddressType.None;

		private string      _address     = string.Empty;

		private byte        _ttl         = 0;






		/// <summary>
		/// Gets the type name
		/// </summary>
		public override string TypeName
		{
			get => TypeNameValue;
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
		/// Gets / Sets the address
		/// </summary>
		public string Address
		{
			get => _address;
			set => _address = SessionDescriptorDataConverter.ConvertToIPAddress(value);
		}

		/// <summary>
		/// Gets / Sets the TTL
		/// </summary>
		public byte TTL
		{
			get => _ttl;
			set => _ttl = value;
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
			return !string.IsNullOrWhiteSpace(_address)

				&& _addressType != AddressType.None
				&& _networkType != NetworkType.None;
		}

		/// <summary>
		/// Make a copy
		/// </summary>
		/// <param name="field">the field</param>
		public void CopyFrom(ConnectionField field)
		{
			if ( field == null )
			{
				return;
			}

			_address     = field._address;
			_addressType = field._addressType;
			_networkType = field._networkType;
			_ttl         = field._ttl;
		}

		/// <summary>
		/// Format the field
		/// </summary>
		/// <returns>retuns a value</returns>
		public override string ToString()
		{
			return ConnectionFieldFormatter.Format(this);
		}






		/// <summary>
		/// Parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="FormatException"/>
		public static ConnectionField Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return ConnectionFieldFormatter.TryParse(value, out ConnectionField result) ? result: throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the value</param>
		/// <param name="result">the field result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out ConnectionField result)
		{
			return ConnectionFieldFormatter.TryParse(value, out result);
		}
	}
}
