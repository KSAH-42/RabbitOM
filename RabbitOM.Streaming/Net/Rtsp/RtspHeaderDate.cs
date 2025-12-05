using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderDate : RtspHeader, IRtspHeaderValue<DateTime>
    {
        private DateTime _value = DateTime.MinValue;






        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        public RtspHeaderDate()
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="value">the value</param>
        public RtspHeaderDate( DateTime value )
        {
            Value = value;
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Date;
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
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _value != DateTime.MinValue && _value != DateTime.MaxValue;
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
        public static bool TryParse( string value , out RtspHeaderDate result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RtspHeaderDate( RtspDataConverter.ConvertToDateTimeAsGMT( value ) );

            return true;
        }
    }
}
