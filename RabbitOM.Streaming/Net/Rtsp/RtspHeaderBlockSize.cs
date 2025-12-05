using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderBlockSize : RtspHeader , IRtspHeaderValue<long>
    {
        private long _value = 0;






        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
		public RtspHeaderBlockSize()
		{
		}

        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
        /// <param name="value">the value</param>
		public RtspHeaderBlockSize( long value )
		{
            Value = value;
		}






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.BlockSize;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public long Value
        {
            get => _value;
            set => _value = value > 0 ? value : 0;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _value > 0;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return _value.ToString();
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderBlockSize result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RtspHeaderBlockSize( RtspDataConverter.ConvertToLong( value ) );

            return true;
        }
    }
}
