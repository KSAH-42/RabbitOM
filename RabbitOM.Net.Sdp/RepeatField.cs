using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
    /// </summary>
    public sealed class RepeatField : BaseField
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string         TypeNameValue            = "r";




        private ValueTime           _repeatInterval          = ValueTime.Zero;

        private ValueTime           _activeDuration          = ValueTime.Zero;




        /// <summary>
        /// Constructor
        /// </summary>
        public RepeatField()
            : this( new ValueTime() , new ValueTime() )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repeatInterval">the repeat time</param>
        /// <param name="activeInterval">the active time</param>
        public RepeatField( ValueTime repeatInterval , ValueTime activeInterval )
        {
            _repeatInterval = repeatInterval;
            _activeDuration = activeInterval;
        }




        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName
        {
            get => TypeNameValue;
        }

        /// <summary>
        /// Gets / Sets the repeat interval
        /// </summary>
        public ValueTime RepeatInterval
        {
            get => _repeatInterval;
            set => _repeatInterval = value;
        }

        /// <summary>
        /// Gets / Sets the active duration
        /// </summary>
        public ValueTime ActiveDuration
        {
            get => _activeDuration;
            set => _activeDuration = value;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _repeatInterval.Validate()
                && _activeDuration.Validate();
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( _repeatInterval.StartTime );
            builder.Append( " " );

            builder.Append( _repeatInterval.StopTime );
            builder.Append( " " );

            builder.Append( _activeDuration.StartTime );
            builder.Append( " " );

            builder.Append( _activeDuration.StopTime );
            builder.Append( " " );

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
        public static RepeatField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            if (!TryParse(value, out RepeatField result) || result == null)
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
        public static bool TryParse( string value , out RepeatField result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries );

            result = new RepeatField()
            {
                RepeatInterval = new ValueTime( SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(0) ?? string.Empty ) , SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(1) ?? string.Empty ) ) ,
                ActiveDuration = new ValueTime( SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(2) ?? string.Empty ) , SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(3) ?? string.Empty ) ) ,
            };

            return true;
        }
    }
}
