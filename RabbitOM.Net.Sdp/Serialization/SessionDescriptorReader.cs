using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp.Serialization
{
    /// <summary>
    /// Represent the session description reader
    /// </summary>
    public sealed class SessionDescriptorReader : IDisposable
    {
        private bool                             _isUnderMediaSection = false;

        private readonly IEnumerator<StringPair> _textFields          = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">the input value</param>
        public SessionDescriptorReader( string input )
        {
            _textFields = SessionDescriptorReaderHelpers.ExtractTextFields( input ).GetEnumerator();
        }



        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsVersionField
        {
            get => CompareFieldName( VersionField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsOriginField
        {
            get => CompareFieldName( OriginField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsSessionNameField
        {
            get => CompareFieldName( SessionNameField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsSessionInformationField
        {
            get => CompareFieldName( SessionInformationField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsEmailField
        {
            get => CompareFieldName( EmailField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsPhoneField
        {
            get => CompareFieldName( PhoneField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsTimeField
        {
            get => CompareFieldName( TimeField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsRepeatField
        {
            get => CompareFieldName( RepeatField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsTimeZoneField
        {
            get => CompareFieldName( TimeZoneField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsConnectionField
        {
            get => CompareFieldName( ConnectionField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsUriField
        {
            get => CompareFieldName( UriField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsEncryptionField
        {
            get => CompareFieldName( EncryptionField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsBandwithField
        {
            get => CompareFieldName( BandwithField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsMediaDescriptionField
        {
            get => CompareFieldName( MediaDescriptionField.TypeNameValue );
        }

        /// <summary>
        /// Check the field name
        /// </summary>
        public bool IsAttributeField
        {
            get => CompareFieldName( AttributeField.TypeNameValue );
        }

        /// <summary>
        /// Check if the reading element which are located as the lastest elements of the document.
        /// The SDP document is split certain way. The media description must be located at the end of the document.
        /// This status indicate that the reader is currently under the media description section.
        /// Please note also, that this section contains only header related to the media description, you can not find times section, or emails section, etc...
        /// <see href="https://tools.ietf.org/html/rfc4566#page-39"/>
        /// </summary>
        public bool IsUnderMediaSection
        {
            get => _isUnderMediaSection;
        }

        /// <summary>
        /// Gets the field name
        /// </summary>
        public string FieldName
        {
            get => _textFields.Current.First ?? string.Empty;
        }

        /// <summary>
        /// Gets the current value
        /// </summary>
        public string CurrentValue
        {
            get => _textFields.Current.Second ?? string.Empty;
        }




        /// <summary>
        /// Read the 
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            if ( !_textFields.MoveNext() )
            {
                _isUnderMediaSection = false;

                return false;
            }

            if ( !_isUnderMediaSection )
            {
                _isUnderMediaSection = IsMediaDescriptionField;
            }

            return true;
        }

        /// <summary>
        /// Compare the current header
        /// </summary>
        /// <param name="name">the name of the header</param>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool CompareFieldName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            return string.Compare( _textFields.Current.First , name , true ) == 0;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            _textFields.Dispose();
        }
    }
}
