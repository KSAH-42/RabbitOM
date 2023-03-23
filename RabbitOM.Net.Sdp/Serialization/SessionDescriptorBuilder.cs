namespace RabbitOM.Net.Sdp.Serialization
{
	/// <summary>
	/// Represent a session description builder
	/// </summary>
	public class SessionDescriptorBuilder
	{
		private VersionField _version = null;

		private OriginField _origin = null;

		private SessionNameField _sessionName = null;

		private SessionInformationField _sessionInformation = null;

		private ConnectionField _connection = null;

		private UriField _uri = null;

		private TimeZoneField _timeZone = null;

		private EncryptionField _encryption = null;

		private MediaDescriptionField _currentMediaDescription = null;

		private readonly EmailFieldCollection _emails = new EmailFieldCollection();

		private readonly PhoneFieldCollection _phones = new PhoneFieldCollection();

		private readonly BandwithFieldCollection _bandwiths = new BandwithFieldCollection();

		private readonly TimeFieldCollection _times = new TimeFieldCollection();

		private readonly RepeatFieldCollection _repeats = new RepeatFieldCollection();

		private readonly AttributeFieldCollection _attributes = new AttributeFieldCollection();

		private readonly MediaDescriptionFieldCollection _mediaDescriptions = new MediaDescriptionFieldCollection();




		/// <summary>
		/// Set the version
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetVersion(string headerValue)
		{
			VersionField.TryParse(headerValue, out _version);

			return this;
		}

		/// <summary>
		/// Set the origin
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetOrigin(string headerValue)
		{
			OriginField.TryParse(headerValue, out _origin);

			return this;
		}

		/// <summary>
		/// Set the session name
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetSessionName(string headerValue)
		{
			SessionNameField.TryParse(headerValue, out _sessionName);

			return this;
		}

		/// <summary>
		/// Set the session information
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetSessionInformation(string headerValue)
		{
			SessionInformationField.TryParse(headerValue, out _sessionInformation);

			return this;
		}

		/// <summary>
		/// Set the uri
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetUri(string headerValue)
		{
			UriField.TryParse(headerValue, out _uri);

			return this;
		}

		/// <summary>
		/// Set the connection
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetConnection(string headerValue)
		{
			ConnectionField.TryParse(headerValue, out _connection);

			return this;
		}

		/// <summary>
		/// Set the time zone
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetTimeZone(string headerValue)
		{
			TimeZoneField.TryParse(headerValue, out _timeZone);

			return this;
		}

		/// <summary>
		/// Set the encryption key
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetEncryption(string headerValue)
		{
			EncryptionField.TryParse(headerValue, out _encryption);

			return this;
		}

		/// <summary>
		/// Add the email
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddEmail(string headerValue)
		{
			if (EmailField.TryParse(headerValue, out EmailField field))
			{
				_emails.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Add a phone number
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddPhone(string headerValue)
		{
			if (PhoneField.TryParse(headerValue, out PhoneField field))
			{
				_phones.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Add a bandwith
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddBandwith(string headerValue)
		{
			if (BandwithField.TryParse(headerValue, out BandwithField field))
			{
				_bandwiths.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Add a time values
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddTime(string headerValue)
		{
			if (TimeField.TryParse(headerValue, out TimeField field))
			{
				_times.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Add a repeat time values
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddRepeat(string headerValue)
		{
			if (RepeatField.TryParse(headerValue, out RepeatField field))
			{
				_repeats.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Add an attribute
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddAttribute(string headerValue)
		{
			if (AttributeField.TryParse(headerValue, out AttributeField field))
			{
				_attributes.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Create a media description
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder CreateMediaDescription(string headerValue)
		{
			if (MediaDescriptionField.TryParse(headerValue, out _currentMediaDescription))
			{
				_mediaDescriptions.TryAdd(_currentMediaDescription);
			}

			return this;
		}

		/// <summary>
		/// Set a connection on the current media description
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetMediaConnection(string headerValue)
		{
			if (_currentMediaDescription != null && ConnectionField.TryParse(headerValue, out ConnectionField field))
			{
				_currentMediaDescription.Connection.CopyFrom(field);
			}

			return this;
		}

		/// <summary>
		/// Set a encryption on the current media description
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder SetMediaEncryption(string headerValue)
		{
			if (_currentMediaDescription != null && EncryptionField.TryParse(headerValue, out EncryptionField field))
			{
				_currentMediaDescription.Encryption.CopyFrom(field);
			}

			return this;
		}

		/// <summary>
		/// Add a bandwith on the current media description
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddMediaBandwith(string headerValue)
		{
			if (_currentMediaDescription != null && BandwithField.TryParse(headerValue, out BandwithField field))
			{
				_currentMediaDescription.Bandwiths.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Add an attribute on the current media description
		/// </summary>
		/// <param name="headerValue">the header value</param>
		/// <returns>returns the builder instance</returns>
		public SessionDescriptorBuilder AddMediaAttribute(string headerValue)
		{
			if (_currentMediaDescription != null && AttributeField.TryParse(headerValue, out AttributeField field))
			{
				_currentMediaDescription.Attributes.TryAdd(field);
			}

			return this;
		}

		/// <summary>
		/// Check if the descriptor can be build
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool CanBuild()
		{
			return _sessionName == null
				|| _sessionInformation == null
				|| _version == null
				? false
				: true
				;
		}

		/// <summary>
		/// Build
		/// </summary>
		/// <returns>returns a new document object</returns>
		public SessionDescriptor Build()
		{
			var descriptor = new SessionDescriptor();

			descriptor.Version.CopyFrom(_version);
			descriptor.Origin.CopyFrom(_origin);
			descriptor.SessionName.CopyFrom(_sessionName);
			descriptor.SessionInformation.CopyFrom(_sessionInformation);
			descriptor.Uri.CopyFrom(_uri);
			descriptor.Connection.CopyFrom(_connection);
			descriptor.TimeZone.CopyFrom(_timeZone);
			descriptor.Encryption.CopyFrom(_encryption);

			descriptor.Emails.TryAddRange(_emails);
			descriptor.Phones.TryAddRange(_phones);
			descriptor.Bandwiths.TryAddRange(_bandwiths);
			descriptor.Times.TryAddRange(_times);
			descriptor.Repeats.TryAddRange(_repeats);
			descriptor.Attributes.TryAddRange(_attributes);
			descriptor.MediaDescriptions.TryAddRange(_mediaDescriptions);

			return descriptor;
		}
	}
}