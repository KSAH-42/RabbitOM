using RabbitOM.Streaming.Net.Sdp.Validation;
using RabbitOM.Streaming.Net.Sdp.Serialization;
using System;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent the session descriptor. For more details please take some times to read the RFC https://tools.ietf.org/html/rfc4566
    /// </summary>
    public sealed class SessionDescriptor
    {
        /// <summary>
        /// Gets the version (mandatory)
        /// </summary>
        public VersionField Version { get; } = new VersionField();

        /// <summary>
        /// Gets the originator (mandatory)
        /// </summary>
        public OriginField Origin { get; } = new OriginField();

        /// <summary>
        /// Gets the session (mandatory)
        /// </summary>
        public SessionNameField SessionName { get; } = new SessionNameField();

        /// <summary>
        /// Gets / Sets the session title (optional)
        /// </summary>
        public SessionInformationField SessionInformation { get; } = new SessionInformationField();

        /// <summary>
        /// Gets the time zone (optional)
        /// </summary>
        public TimeZoneField TimeZone { get; } = new TimeZoneField();

        /// <summary>
        /// Gets the uri (optional)
        /// </summary>
        public UriField Uri { get; } = new UriField();

        /// <summary>
        /// Gets the emails list (optional)
        /// </summary>
        public EmailFieldCollection Emails { get; } = new EmailFieldCollection();

        /// <summary>
        /// Gets the phones list (optional)
        /// </summary>
        public PhoneFieldCollection Phones { get; } = new PhoneFieldCollection();

        /// <summary>
        /// Gets the connection infos (optional)
        /// </summary>
        public ConnectionField Connection { get; } = new ConnectionField();

        /// <summary>
        /// Gets the bandwiths list (optional)
        /// </summary>
        public BandwithFieldCollection Bandwiths { get; } = new BandwithFieldCollection();

        /// <summary>
        /// Gets the times
        /// </summary>
        public TimeFieldCollection Times { get; } = new TimeFieldCollection();

        /// <summary>
        /// Gets the repeat fields
        /// </summary>
        public RepeatFieldCollection Repeats { get; } = new RepeatFieldCollection();

        /// <summary>
        /// Gets the encryption (optional)
        /// </summary>
        public EncryptionField Encryption { get; } = new EncryptionField();

        /// <summary>
        /// Gets the attributes (optional)
        /// </summary>
        public AttributeFieldCollection Attributes { get; } = new AttributeFieldCollection();

        /// <summary>
        /// Gets the media description list
        /// </summary>
        public MediaDescriptionFieldCollection MediaDescriptions { get; } = new MediaDescriptionFieldCollection();





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

            result.Add( descriptor.Version );
            result.Add( descriptor.SessionName );
            result.Add( descriptor.SessionInformation );
            result.Add( descriptor.Uri );
            result.Add( descriptor.Origin );
            result.AddRange( descriptor.Emails );
            result.AddRange( descriptor.Phones );
            result.Add( descriptor.Connection );
            result.AddRange( descriptor.Bandwiths );
            result.AddRange( descriptor.Times );
            result.AddRange( descriptor.Repeats );
            result.Add( descriptor.TimeZone );
            result.Add( descriptor.Encryption );
            result.AddRange( descriptor.Attributes );
            result.AddRange( descriptor.MediaDescriptions );

            return result;
        }
    }
}
