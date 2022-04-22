using System;
using System.Linq;
using System.Net;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class OriginField : BaseField, ICopyable<OriginField>
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string       TypeNameValue          = "o";




        private string            _userName              = string.Empty;

        private string            _sessionId             = string.Empty;

        private string            _version               = string.Empty;

        private NetworkType       _networkType           = NetworkType.None;

        private AddressType       _addressType           = AddressType.None;

        private string            _address               = string.Empty;




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
            set => _userName = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the session identifier
        /// </summary>
        public string SessionId
        {
            get => _sessionId;
            set => _sessionId = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the session version
        /// </summary>
        public string Version
        {
            get => _version;
            set => _version = SessionDescriptorDataConverter.Trim( value );
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
            set => _address = SessionDescriptorDataConverter.ConvertToIPAddress( value );
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( string.IsNullOrWhiteSpace( _sessionId )
                || string.IsNullOrWhiteSpace( _version )
                || string.IsNullOrWhiteSpace( _address )
                )
            {
                return false;
            }

            if ( _networkType == NetworkType.None )
            {
                return false;
            }

            if ( _addressType == AddressType.None )
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
        public void CopyFrom( OriginField field )
        {
            if ( field == null || object.ReferenceEquals( field , this ) )
            {
                return;
            }

            _userName    = field._userName ;
            _sessionId   = field._sessionId ;
            _version     = field._version ;
            _networkType = field._networkType;
            _addressType = field._addressType;
            _address     = field._address ;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( string.IsNullOrWhiteSpace( _userName ) )
            {
                builder.Append( "-" );
            }
            else
            {
                builder.Append( _userName );
            }

            builder.Append( " " );

            builder.Append( _sessionId );
            builder.Append( " " );

            builder.Append( _version );
            builder.Append( " " );

            builder.Append( SessionDescriptorDataConverter.ConvertToString( _networkType ) );
            builder.Append( " " );

            builder.Append( SessionDescriptorDataConverter.ConvertToString( _addressType ) );
            builder.Append( " " );

            builder.Append( _address );

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

            if (!TryParse(value, out OriginField result) || result == null)
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
        public static bool TryParse( string value , out OriginField result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 1 )
            {
                return false;
            }
            
            result = new OriginField()
            {
                UserName    = tokens.ElementAtOrDefault(0) ?? string.Empty ,
                SessionId   = tokens.ElementAtOrDefault(1) ?? string.Empty ,
                Version     = tokens.ElementAtOrDefault(2) ?? string.Empty ,
                Address     = tokens.ElementAtOrDefault(5) ?? string.Empty ,
                NetworkType = SessionDescriptorDataConverter.ConvertToNetworkType(tokens.ElementAtOrDefault(3) ?? string.Empty ) ,
                AddressType = SessionDescriptorDataConverter.ConvertToAddressType(tokens.ElementAtOrDefault(4) ?? string.Empty ) ,
            };

            return true;
        }
    }
}
