﻿using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderIfModifiedSince : RtspHeader, IRtspHeaderValue<DateTime>
    {
        private DateTime _value = DateTime.MinValue;





        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.IfModifiedSince;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public DateTime Value
        {
            get => _value;
            set => _value = value.ToUniversalTime();
        }





        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _value != DateTime.MinValue && _value == DateTime.MaxValue;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return RtspDataConverter.ConvertToString( _value , RtspDateTimeFormatType.GmtFormat );
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderIfModifiedSince result )
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            result = new RtspHeaderIfModifiedSince()
            {
                Value = RtspDataConverter.ConvertToDateTimeAsGMT( value )
            };

            return true;
        }
    }
}
