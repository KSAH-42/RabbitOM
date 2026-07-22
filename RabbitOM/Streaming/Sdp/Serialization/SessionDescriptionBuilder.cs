namespace RabbitOM.Streaming.Sdp.Serialization
{
    public sealed class SessionDescriptionBuilder
    {
        private VersionField _version;

        private OriginField _origin;

        private SessionNameField _sessionName;

        private SessionInformationField _sessionInformation;

        private ConnectionField _connection;

        private UriField _uri;

        private TimeZoneField _timeZone;

        private EncryptionField _encryption;

        private MediaDescriptionField _currentMediaDescription;

        private readonly EmailFieldCollection _emails = new EmailFieldCollection();

        private readonly PhoneFieldCollection _phones = new PhoneFieldCollection();

        private readonly BandwithFieldCollection _bandwiths = new BandwithFieldCollection();

        private readonly TimeFieldCollection _times = new TimeFieldCollection();

        private readonly RepeatFieldCollection _repeats = new RepeatFieldCollection();

        private readonly AttributeFieldCollection _attributes = new AttributeFieldCollection();

        private readonly MediaDescriptionFieldCollection _mediaDescriptions = new MediaDescriptionFieldCollection();




        public void SetVersion(string headerValue)
        {
            VersionField.TryParse(headerValue, out _version);
        }

        public void SetOrigin(string headerValue)
        {
            OriginField.TryParse(headerValue, out _origin);
        }

        public void SetSessionName(string headerValue)
        {
            SessionNameField.TryParse(headerValue, out _sessionName);
        }

        public void SetSessionInformation(string headerValue)
        {
            SessionInformationField.TryParse(headerValue, out _sessionInformation);
        }

        public void SetUri(string headerValue)
        {
            UriField.TryParse(headerValue, out _uri);
        }

        public void SetConnection(string headerValue)
        {
            ConnectionField.TryParse(headerValue, out _connection);
        }

        public void SetTimeZone(string headerValue)
        {
            TimeZoneField.TryParse(headerValue, out _timeZone);
        }

        public void SetEncryption(string headerValue)
        {
            EncryptionField.TryParse(headerValue, out _encryption);
        }

        public void AddEmail(string headerValue)
        {
            if (EmailField.TryParse(headerValue, out EmailField field))
            {
                _emails.TryAdd(field);
            }
        }

        public void AddPhone(string headerValue)
        {
            if (PhoneField.TryParse(headerValue, out PhoneField field))
            {
                _phones.TryAdd(field);
            }
        }

        public void AddBandwith(string headerValue)
        {
            if (BandwithField.TryParse(headerValue, out BandwithField field))
            {
                _bandwiths.TryAdd(field);
            }
        }

        public void AddTime(string headerValue)
        {
            if (TimeField.TryParse(headerValue, out TimeField field))
            {
                _times.TryAdd(field);
            }
        }

        public void AddRepeat(string headerValue)
        {
            if (RepeatField.TryParse(headerValue, out RepeatField field))
            {
                _repeats.TryAdd(field);
            }
        }

        public void AddAttribute(string headerValue)
        {
            if (AttributeField.TryParse(headerValue, out AttributeField field))
            {
                _attributes.TryAdd(field);
            }
        }

        public void CreateMediaDescription(string headerValue)
        {
            if (MediaDescriptionField.TryParse(headerValue, out _currentMediaDescription))
            {
                _mediaDescriptions.TryAdd(_currentMediaDescription);
            }
        }

        public void SetMediaConnection(string headerValue)
        {
            if (_currentMediaDescription != null && ConnectionField.TryParse(headerValue, out ConnectionField field))
            {
                _currentMediaDescription.Connection.CopyFrom(field);
            }
        }

        public void SetMediaEncryption(string headerValue)
        {
            if (_currentMediaDescription != null && EncryptionField.TryParse(headerValue, out EncryptionField field))
            {
                _currentMediaDescription.Encryption.CopyFrom(field);
            }
        }

        public void AddMediaBandwith(string headerValue)
        {
            if (_currentMediaDescription != null && BandwithField.TryParse(headerValue, out BandwithField field))
            {
                _currentMediaDescription.Bandwiths.TryAdd(field);
            }
        }

        public void AddMediaAttribute(string headerValue)
        {
            if (_currentMediaDescription != null && AttributeField.TryParse(headerValue, out AttributeField field))
            {
                _currentMediaDescription.Attributes.TryAdd(field);
            }
        }

        public bool CanBuild()
        {
            return _sessionName != null && _sessionInformation != null && _version != null ;
        }

        public SessionDescription Build()
        {
            var descriptor = new SessionDescription();

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