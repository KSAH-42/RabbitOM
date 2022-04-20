using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class MediaDescriptionField : BaseField
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string                  TypeNameValue          = "m";




        private MediaType                    _type                  = MediaType.None;

        private int                          _port                  = 0;

        private ProtocolType                 _protocol              = ProtocolType.None;

        private ProfileType                  _profile               = ProfileType.None;

        private int                          _payload               = 0;




        private readonly ConnectionField            _connection            = new ConnectionField();

        private readonly EncryptionField            _encryption            = new EncryptionField();

        private readonly BandwithFieldCollection    _bandwiths             = new BandwithFieldCollection();

        private readonly AttributeFieldCollection   _attributes            = new AttributeFieldCollection();




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
            return Validate( true );
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Validate( bool ignoreControlValidation )
        {
            if ( _payload < 0 )
            {
                return false;
            }

            if ( _type == MediaType.None )
            {
                return false;
            }

            if ( _protocol == ProtocolType.None )
            {
                return false;
            }

            if ( _profile == ProfileType.None )
            {
                return false;
            }

            return ignoreControlValidation || _connection.TryValidate();
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( SessionDescriptorDataConverter.ConvertToString( _type ) );
            builder.Append( " " );

            builder.Append( _port );
            builder.Append( " " );

            builder.Append( SessionDescriptorDataConverter.ConvertToString( _protocol ) );
            builder.Append( "/" );
            builder.Append( SessionDescriptorDataConverter.ConvertToString( _profile ) );
            builder.Append( " " );

            builder.Append( _payload );

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

            if (!TryParse(value, out MediaDescriptionField result) || result == null)
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
        public static bool TryParse( string value , out MediaDescriptionField result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            string[] tokens = value.Split( new char[] { ' ' } );

            if ( tokens == null || tokens.Length < 4 )
            {
                return false;
            }

            string[] protocolTokens = tokens[ 2 ].Split( '/' );

            if ( protocolTokens == null || protocolTokens.Length <= 1 )
            {
                return false;
            }

            result = new MediaDescriptionField()
            {
                Type = SessionDescriptorDataConverter.ConvertToMediaType( tokens.Length > 0 ? tokens[0] : string.Empty ) ,
                Port = SessionDescriptorDataConverter.ConvertToInt( tokens.Length > 1 ? tokens[1] : string.Empty ) ,
                Payload = SessionDescriptorDataConverter.ConvertToInt( tokens.Length > 3 ? tokens[3] : string.Empty ) ,
                Protocol = SessionDescriptorDataConverter.ConvertToProtocolType( protocolTokens[0] ) ,
                Profile = SessionDescriptorDataConverter.ConvertToProfileType( protocolTokens[1] ) ,
            };

            return true;
        }
    }
}
