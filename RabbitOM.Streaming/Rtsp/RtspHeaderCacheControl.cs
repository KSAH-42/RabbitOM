﻿using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderCacheControl : RtspHeader, IRtspHeaderValue<string>
    {
        private string _value = string.Empty;






        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
		public RtspHeaderCacheControl()
		{
		}

        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
        /// <param name="value">the value</param>
		public RtspHeaderCacheControl( string value )
		{
            Value = value;
		}






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.CacheControl;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Value
        {
            get => _value;
            set => _value = RtspDataConverter.Trim( value );
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _value );
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return _value;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderCacheControl result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RtspHeaderCacheControl( value );

            return true;
        }
    }
}
