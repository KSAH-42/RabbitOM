using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class EmailField : BaseField
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string       TypeNameValue      = "e";






        private string            _address           = string.Empty;

        private string            _name              = string.Empty;






        /// <summary>
        /// Constructor
        /// </summary>
        public EmailField()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">the address</param>
        public EmailField( string address )
            : this( address , string.Empty )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">the address</param>
        /// <param name="name">the name</param>
        public EmailField( string address , string name )
        {
            Address = address;
            Name = name;
        }






        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName
        {
            get => TypeNameValue;
        }

        /// <summary>
        /// Gets / Sets the address
        /// </summary>
        public string Address
        {
            get => _address;
            set => _address = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the name
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = SessionDescriptorDataConverter.TrimAsEmailFormat( value );
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            if ( string.IsNullOrWhiteSpace( _address ) )
            {
                return false;
            }

            return Uri.IsWellFormedUriString( _address , UriKind.Relative );
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _address );

            if ( string.IsNullOrWhiteSpace( _name ) )
            {
                return builder.ToString();
            }

            builder.Append( " " );
            builder.Append( "(" );
            builder.Append( _name );
            builder.Append( ")" );

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
        public static EmailField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            if (!TryParse(value, out EmailField result) || result == null)
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
        public static bool TryParse( string value , out EmailField result )
        {
            result = null;

            if ( !SessionDescriptorDataConverter.TryExtractField( value , ' ' , out StringPair field ) )
            {
                return false;
            }

            result = new EmailField()
            {
                Address = field.First ,
                Name = field.Second
            };

            return true;
        }
    }
}
