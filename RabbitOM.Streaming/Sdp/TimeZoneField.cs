﻿using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent a sdp field
    /// </summary>
    public sealed class TimeZoneField : BaseField, ICopyable<TimeZoneField>
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string TypeNameValue = "z";




        private string _value = string.Empty;




        /// <summary>
        /// Initialize a new instance of a time zone field
        /// </summary>
        public TimeZoneField()
        {

        }

        /// <summary>
        /// Initialize a new instance of a time zone field
        /// </summary>
        /// <param name="value">the value</param>
        public TimeZoneField( string value )
        {
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
        /// Gets / Sets the value
        /// </summary>
        public string Value
        {
            get => _value;
            set => _value = DataConverter.Filter(value);
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _value );
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="field">the field</param>
        public void CopyFrom(TimeZoneField field)
        {
            if ( field == null )
            {
                return;
            }

            _value = field._value;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            return _value;
        }





        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        public static TimeZoneField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            return TryParse(value, out TimeZoneField result) ? result : throw new FormatException();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out TimeZoneField result)
        {
            result = new TimeZoneField()
            {
                Value = value
            };

            return true;
        }
    }
}
