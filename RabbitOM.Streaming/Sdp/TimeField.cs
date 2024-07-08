﻿using RabbitOM.Streaming.Sdp.Serialization.Formatters;
using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent a sdp field
    /// </summary>
    public sealed class TimeField : BaseField, ICopyable<TimeField>
    {
        /// <summary>
        /// Represent the type name
        /// </summary>
        public const string TypeNameValue = "t";




        private long _startTime = 0;

        private long _stopTime  = 0;




        /// <summary>
        /// Construct
        /// </summary>
        public TimeField()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startTime">the start time</param>
        /// <param name="stopTime">the stop time</param>
        public TimeField(long startTime, long stopTime)
        {
            _startTime = startTime;
            _stopTime  = stopTime;
        }




        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName
        {
            get => TypeNameValue;
        }

        /// <summary>
        /// Gets / Sets the start time
        /// </summary>
        public long StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        /// <summary>
        /// Gets / Sets the stop time
        /// </summary>
        public long StopTime
        {
            get => _stopTime;
            set => _stopTime = value;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return 0 <= _startTime && _startTime <= _stopTime;
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="field">the field</param>
        public void CopyFrom(TimeField field)
        {
            if ( field == null )
            {
                return;
            }

            _startTime = field._startTime;
            _stopTime  = field._stopTime;
        }

        /// <summary>
        /// Format the field
        /// </summary>
        /// <returns>retuns a value</returns>
        public override string ToString()
        {
            return TimeFieldFormatter.Format(this);
        }




        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        public static TimeField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            return TimeFieldFormatter.TryParse(value, out TimeField result) ? result : throw new FormatException();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the field result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out TimeField result)
        {
            return TimeFieldFormatter.TryParse(value, out result);
        }
    }
}
