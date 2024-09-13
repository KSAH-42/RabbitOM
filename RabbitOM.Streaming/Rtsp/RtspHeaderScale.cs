﻿using System.Globalization;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderScale : RtspHeader, IRtspHeaderValue<float>
    {
        private float _value = 0;






        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        public RtspHeaderScale()
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="value">the value</param>
        public RtspHeaderScale( float value )
        {
            Value = value;
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Scale;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public float Value
        {
            get => _value;
            set => _value = value;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true</returns>
        public override bool TryValidate()
        {
            return true;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return _value.ToString( "G2" , CultureInfo.InvariantCulture );
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderScale result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RtspHeaderScale( RtspDataConverter.ConvertToFloat( value , NumberStyles.Any , CultureInfo.InvariantCulture ) );

            return true;
        }
    }
}
