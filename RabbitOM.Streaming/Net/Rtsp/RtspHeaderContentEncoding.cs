using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderContentEncoding : RtspHeader , IRtspHeaderValue<string>
    {
        private string _value = string.Empty;






        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
		public RtspHeaderContentEncoding()
		{
		}

        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
        /// <param name="value">the value</param>
		public RtspHeaderContentEncoding( string value )
		{
            Value = value;
		}






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.ContentEncoding;
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
        public static bool TryParse( string value , out RtspHeaderContentEncoding result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RtspHeaderContentEncoding( value );

            return true;
        }
    }
}
