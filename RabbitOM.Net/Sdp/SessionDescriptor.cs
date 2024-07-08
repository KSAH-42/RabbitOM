using RabbitOM.Net.Sdp.Validation;
using RabbitOM.Net.Sdp.Serialization;
using System;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the session descriptor. For more details please take some times to read the RFC https://tools.ietf.org/html/rfc4566
    /// </summary>
    public sealed class SessionDescriptor
    {
        private readonly VersionField                    _version = new VersionField();

        private readonly SessionNameField                _sessionName = new SessionNameField();

        private readonly SessionInformationField         _sessionInformation = new SessionInformationField();

        private readonly UriField                        _uri = new UriField();

        private readonly OriginField                     _origin = new OriginField();

        private readonly EmailFieldCollection            _emails = new EmailFieldCollection();

        private readonly PhoneFieldCollection            _phones = new PhoneFieldCollection();

        private readonly ConnectionField                 _connection = new ConnectionField();

        private readonly BandwithFieldCollection         _bandwiths = new BandwithFieldCollection();

        private readonly TimeFieldCollection             _times = new TimeFieldCollection();

        private readonly RepeatFieldCollection           _repeats = new RepeatFieldCollection();

        private readonly TimeZoneField                   _timeZone = new TimeZoneField();
                                                        
        private readonly EncryptionField                 _encryption = new EncryptionField();

        private readonly AttributeFieldCollection        _attributes = new AttributeFieldCollection();

        private readonly MediaDescriptionFieldCollection _mediaDescriptions = new MediaDescriptionFieldCollection();




        /// <summary>
        /// Gets the version (mandatory)
        /// </summary>
        public VersionField Version
        {
            get => _version;
        }

        /// <summary>
        /// Gets the originator (mandatory)
        /// </summary>
        public OriginField Origin
        {
            get => _origin;
        }

        /// <summary>
        /// Gets the session (mandatory)
        /// </summary>
        public SessionNameField SessionName
        {
            get => _sessionName;
        }

        /// <summary>
        /// Gets / Sets the session title (optional)
        /// </summary>
        public SessionInformationField SessionInformation
        {
            get => _sessionInformation;
        }

        /// <summary>
        /// Gets the time zone (optional)
        /// </summary>
        public TimeZoneField TimeZone
        {
            get => _timeZone;
        }

        /// <summary>
        /// Gets the uri (optional)
        /// </summary>
        public UriField Uri
        {
            get => _uri;
        }

        /// <summary>
        /// Gets the emails list (optional)
        /// </summary>
        public EmailFieldCollection Emails
        {
            get => _emails;
        }

        /// <summary>
        /// Gets the phones list (optional)
        /// </summary>
        public PhoneFieldCollection Phones
        {
            get => _phones;
        }

        /// <summary>
        /// Gets the connection infos (optional)
        /// </summary>
        public ConnectionField Connection
        {
            get => _connection;
        }

        /// <summary>
        /// Gets the bandwiths list (optional)
        /// </summary>
        public BandwithFieldCollection Bandwiths
        {
            get => _bandwiths;
        }

        /// <summary>
        /// Gets the times
        /// </summary>
        public TimeFieldCollection Times
        {
            get => _times;
        }

        /// <summary>
        /// Gets the repeat fields
        /// </summary>
        public RepeatFieldCollection Repeats
        {
            get => _repeats;
        }

        /// <summary>
        /// Gets the encryption (optional)
        /// </summary>
        public EncryptionField Encryption
        {
            get => _encryption;
        }

        /// <summary>
        /// Gets the attributes (optional)
        /// </summary>
        public AttributeFieldCollection Attributes
        {
            get => _attributes;
        }

        /// <summary>
        /// Gets the media description list
        /// </summary>
        public MediaDescriptionFieldCollection MediaDescriptions
        {
            get => _mediaDescriptions;
        }





        /// <summary>
        /// Just perform a minimal validation
        /// </summary>
        /// <remarks>
        ///		<para>this method does not validate all the fields</para>
        ///		<para>use custom validation instead</para>
        /// </remarks>
        public void Validate()
        {
            Validate(SessionDescriptorValidator.DefaultValidator);
        }

        /// <summary>
        /// Just perform a validation only on mandatory fields
        /// </summary>
        /// <param name="validator">the validator</param>
        /// <exception cref="ArgumentNullException"/>
        public void Validate(SessionDescriptorValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            validator.Validate(this);
        }

        /// <summary>
        /// Just perform a minimal validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///		<para>this method does not validate all the fields</para>
        ///		<para>use custom validation instead</para>
        /// </remarks>
        public bool TryValidate()
        {
            return TryValidate(SessionDescriptorValidator.DefaultValidator);
        }

        /// <summary>
        /// Just perform a validation only on mandatory fields
        /// </summary>
        /// <param name="validator">the validator</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate(SessionDescriptorValidator validator)
        {
            return validator?.TryValidate(this) ?? false;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            return SessionDescriptorSerializer.Serialize( this );
        }





        /// <summary>
        /// Parse and create an session descriptor
        /// </summary>
        /// <param name="value">the input value</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FormatException" />
        public static SessionDescriptor Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if ( string.IsNullOrWhiteSpace(value) )
            {
                throw new ArgumentException(nameof(value));
            }

            return SessionDescriptorSerializer.Deserialize(value) ?? throw new FormatException();
        }

        /// <summary>
        /// Parse and create an session descriptor
        /// </summary>
        /// <param name="value">the input value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out SessionDescriptor result)
        {
            result = null;

            if ( string.IsNullOrWhiteSpace(value) )
            {
                return false;
            }

            try
            {
                result = SessionDescriptorSerializer.Deserialize(value);

                return result != null ;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return false;
        }

        /// <summary>
        /// List all fields present in the descriptor
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static BaseFieldCollection EnumerateFields( SessionDescriptor descriptor )
        {
            if ( descriptor == null )
            {
                throw new ArgumentNullException( nameof( descriptor ) );
            }

            var result = new BaseFieldCollection();

            result.TryAdd( descriptor.Version );
            result.TryAdd( descriptor.SessionName );
            result.TryAdd( descriptor.SessionInformation );
            result.TryAdd( descriptor.Uri );
            result.TryAdd( descriptor.Origin );
            result.TryAddRange( descriptor.Emails );
            result.TryAddRange( descriptor.Phones );
            result.TryAdd( descriptor.Connection );
            result.TryAddRange( descriptor.Bandwiths );
            result.TryAddRange( descriptor.Times );
            result.TryAddRange( descriptor.Repeats );
            result.TryAdd( descriptor.TimeZone );
            result.TryAdd( descriptor.Encryption );
            result.TryAddRange( descriptor.Attributes );
            result.TryAddRange( descriptor.MediaDescriptions );

            return result;
        }
    }
}
