using RabbitOM.Streaming.Sdp.Serialization.Formatters;
using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent a sdp field
    /// </summary>
    public sealed class RepeatField : BaseField, ICopyable<RepeatField>
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string TypeNameValue = "r";




        private ValueTime _repeatInterval = ValueTime.Zero;

        private ValueTime _activeDuration = ValueTime.Zero;




        /// <summary>
        /// Constructor
        /// </summary>
        public RepeatField() : this( ValueTime.Zero , ValueTime.Zero )
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
            return _repeatInterval.TryValidate()
                && _activeDuration.TryValidate();
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="field">the field</param>
        public void CopyFrom(RepeatField field)
        {
            if ( field == null )
            {
                return;
            }

            _repeatInterval = field._repeatInterval;
            _activeDuration = field._activeDuration;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            return RepeatFieldFormatter.Format( this );
        }




        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        public static RepeatField Parse( string value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return RepeatFieldFormatter.TryParse( value, out RepeatField result ) ? result : throw new FormatException();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string value , out RepeatField result )
        {
            return RepeatFieldFormatter.TryParse( value , out result );
        }
    }
}
