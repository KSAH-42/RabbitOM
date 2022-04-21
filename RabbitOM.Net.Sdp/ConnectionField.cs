using System;
using System.Linq;
using System.Net;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class ConnectionField : BaseField
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string       TypeNameValue          = "c";






        private NetworkType       _networkType           = NetworkType.None;

        private AddressType       _addressType           = AddressType.None;

        private string            _address               = string.Empty;

        private byte              _ttl                   = 0;






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
            set => _address = SessionDescriptorDataConverter.ConvertToIPAddress( value );
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
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _networkType == NetworkType.None )
            {
                return false;
            }

            if ( _addressType == AddressType.None )
            {
                return false;
            }

            if ( string.IsNullOrWhiteSpace( _address ) )
            {
                return false;
            }

            if ( !IPAddress.TryParse( _address , out IPAddress ipAddress ) || ipAddress == null )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="field">the field</param>
        public void CopyFrom( ConnectionField field )
        {
            if ( field == null || object.ReferenceEquals( field , this ) )
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
            var builder = new StringBuilder();

            builder.Append( SessionDescriptorDataConverter.ConvertToString( _networkType ) );
            builder.Append( " " );

            builder.Append( SessionDescriptorDataConverter.ConvertToString( _addressType ) );
            builder.Append( " " );

            builder.Append( _address );

            if ( _ttl > 0 )
            {
                builder.Append( "/" );
                builder.Append( _ttl );
            }

            return builder.ToString();
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

            if (!TryParse(value, out ConnectionField result) || result == null)
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
        public static bool TryParse( string value , out ConnectionField result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            string[] tokens = value.Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length < 3 )
            {
                return false;
            }

            result = new ConnectionField()
            {
                NetworkType = SessionDescriptorDataConverter.ConvertToNetworkType( tokens.ElementAtOrDefault( 0 ) ?? string.Empty ) ,
                AddressType = SessionDescriptorDataConverter.ConvertToAddressType( tokens.ElementAtOrDefault( 1 ) ?? string.Empty ) ,
                Address     = tokens.ElementAtOrDefault( 2 ) ,
                TTL         = SessionDescriptorDataConverter.ConvertToTTL( tokens.ElementAtOrDefault( 2 ) ) ,
            };

            return true;
        }
    }
}
