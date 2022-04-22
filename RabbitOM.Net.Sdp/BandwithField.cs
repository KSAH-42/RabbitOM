using System;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp document bandwith infos
    /// </summary>
    public sealed class BandwithField : BaseField
    {
        /// <summary>
        /// Represent a modifier name
        /// </summary>
        public const string      ConferenceTotal            = "CT";

        /// <summary>
        /// Represent a modifier name
        /// </summary>
        public const string      ApplicationSpecific        = "AS";

        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string      TypeNameValue              = "b";






        private string           _modifier                  = string.Empty;

        private long             _value                     = 0;






        /// <summary>
        /// Constructor
        /// </summary>
        public BandwithField()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="modifier">the modifier</param>
        /// <param name="value">the value</param>
        public BandwithField( string modifier , int value )
        {
            Modifier = modifier;
            Value = value;
        }






        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName
        {
            get => TypeNameValue;
        }

        /// <summary>
        /// Gets / Sets the modifier
        /// </summary>
        public string Modifier
        {
            get => _modifier;
            set => _modifier = SessionDescriptorDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public long Value
        {
            get => _value;
            set => _value = value;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return !string.IsNullOrEmpty( _modifier );
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _modifier );
            builder.Append( ":" );
            builder.Append( _value );

            return builder.ToString();
        }




        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        public static BandwithField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            if (!TryParse(value, out BandwithField result) || result == null)
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
        public static bool TryParse( string value , out BandwithField result )
        {
            result = null;

            if ( !SessionDescriptorDataConverter.TryExtractField( value , ':' , out StringPair field ) )
            {
                return false;
            }

            result = new BandwithField()
            {
                Modifier = field.First ,
                Value    = SessionDescriptorDataConverter.ConvertToLong( field.Second )
            };

            return true;
        }
    }
}
