﻿using RabbitOM.Streaming.Sdp.Serialization.Formatters;
using RabbitOM.Streaming.Sdp.Validation;
using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent a sdp field
    /// </summary>
    public sealed class OriginField : BaseField, ICopyable<OriginField>
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string TypeNameValue = "o";





        private string      _userName    = string.Empty;

        private ulong       _sessionId   = 0;

        private ulong       _version     = 0;

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
        /// <remarks>
        ///     <para>the user name must not contains spaces</para>
        /// </remarks>
        public string UserName
        {
            get => _userName;
            set => _userName = DataConverter.Filter(value);
        }

        /// <summary>
        /// Gets / Sets the session identifier
        /// </summary>
        public ulong SessionId
        {
            get => _sessionId;
            set => _sessionId = value;
        }

        /// <summary>
        /// Gets / Sets the session version
        /// </summary>
        public ulong Version
        {
            get => _version;
            set => _version = value;
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
            set => _address = DataConverter.FirstPartOf(value);
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            // the user name must not contains space https://datatracker.ietf.org/doc/html/rfc4566.html#page-11
            
            if ( _userName?.Contains( " " ) == true) 
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

            return ValidatorHelper.TryValidateAddress( _address , _addressType );
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="field">the field</param>
        public void CopyFrom(OriginField field)
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
            return OriginFieldFormatter.Format( this);
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

            return OriginFieldFormatter.TryParse(value, out OriginField result) ? result : throw new FormatException();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out OriginField result)
        {
            return OriginFieldFormatter.TryParse(value, out result);
        }
    }
}
