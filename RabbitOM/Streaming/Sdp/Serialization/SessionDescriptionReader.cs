using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp.Serialization
{
    public sealed class SessionDescriptionReader : IDisposable
    {
        private bool _isUnderMediaSection = false;

        private readonly IEnumerator<StringPair> _textFields = null;




        public SessionDescriptionReader(string input)
            : this( StringPair.ParseAll( input ) )
        {
        }

        public SessionDescriptionReader( IEnumerable<StringPair> fields )
        {
            if ( fields == null )
            {
                throw new ArgumentNullException( nameof( fields ) );
            }

            _textFields = fields.GetEnumerator();
        }




        public bool IsVersionHeader
        {
            get => CompareFieldName(VersionField.TypeNameValue);
        }

        public bool IsOriginHeader
        {
            get => CompareFieldName(OriginField.TypeNameValue);
        }

        public bool IsSessionNameHeader
        {
            get => CompareFieldName(SessionNameField.TypeNameValue);
        }

        public bool IsSessionInformationHeader
        {
            get => CompareFieldName(SessionInformationField.TypeNameValue);
        }

        public bool IsEmailHeader
        {
            get => CompareFieldName(EmailField.TypeNameValue);
        }

        public bool IsPhoneHeader
        {
            get => CompareFieldName(PhoneField.TypeNameValue);
        }

        public bool IsTimeHeader
        {
            get => CompareFieldName(TimeField.TypeNameValue);
        }

        public bool IsRepeatHeader
        {
            get => CompareFieldName(RepeatField.TypeNameValue);
        }

        public bool IsTimeZoneHeader
        {
            get => CompareFieldName(TimeZoneField.TypeNameValue);
        }

        public bool IsConnectionHeader
        {
            get => CompareFieldName(ConnectionField.TypeNameValue);
        }

        public bool IsUriHeader
        {
            get => CompareFieldName(UriField.TypeNameValue);
        }

        public bool IsEncryptionHeader
        {
            get => CompareFieldName(EncryptionField.TypeNameValue);
        }

        public bool IsBandwithHeader
        {
            get => CompareFieldName(BandwithField.TypeNameValue);
        }

        public bool IsMediaDescriptionHeader
        {
            get => CompareFieldName(MediaDescriptionField.TypeNameValue);
        }

        public bool IsAttributeHeader
        {
            get => CompareFieldName(AttributeField.TypeNameValue);
        }

        public bool IsUnderMediaSection
        {
            get => _isUnderMediaSection;
        }

        public string CurrentHeaderName
        {
            get => _textFields.Current.First ?? string.Empty;
        }

        public string CurrentHeaderValue
        {
            get => _textFields.Current.Second ?? string.Empty;
        }




        public bool Read()
        {
            if (!_textFields.MoveNext())
            {
                _isUnderMediaSection = false;

                return false;
            }

            if (!_isUnderMediaSection)
            {
                _isUnderMediaSection = IsMediaDescriptionHeader;
            }

            return true;
        }

        private bool CompareFieldName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return string.Compare(_textFields.Current.First, name, true) == 0;
        }

        public void Dispose()
        {
            _textFields.Dispose();
        }
    }
}
