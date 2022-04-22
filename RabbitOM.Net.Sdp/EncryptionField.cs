using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class EncryptionField : BaseField, ICopyable<EncryptionField>
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string       TypeNameValue          = "k";




        private string            _method                = string.Empty;

        private string            _key                   = string.Empty;




        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName
        {
            get => TypeNameValue;
        }

        /// <summary>
        /// Gets / Sets the method
        /// </summary>
        public string Method
        {
            get => _method;
            set => _method = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the key
        /// </summary>
        public string Key
        {
            get => _key;
            set => _key = SessionDescriptorDataConverter.Trim( value );
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace( _method );
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="field">the field</param>
        public void CopyFrom( EncryptionField field )
        {
            if ( field == null || object.ReferenceEquals( field , this ) )
            {
                return;
            }

            _method = field._method;
            _key    = field._key;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _method );

            if ( string.IsNullOrWhiteSpace( _key ) )
            {
                return builder.ToString();
            }

            builder.Append( ":" );
            builder.Append( _key );

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
        public static EncryptionField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            if (!TryParse(value, out EncryptionField result) || result == null)
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
        public static bool TryParse( string value , out EncryptionField result )
        {
            result = null;

            if ( !SessionDescriptorDataConverter.TryExtractField( value , ':' , out StringPair field ) )
            {
                return false;
            }

            result = new EncryptionField()
            {
                Method = field.First ,
                Key    = field.Second
            };

            return true;
        }
    }
}
