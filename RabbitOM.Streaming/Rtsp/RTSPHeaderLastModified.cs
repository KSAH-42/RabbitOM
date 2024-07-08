using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderLastModified : RTSPHeader, IRTSPHeaderValue<DateTime>
    {
        private DateTime _value = DateTime.MinValue;



        /// <summary>
        /// Constructir
        /// </summary>
        public RTSPHeaderLastModified()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
        public RTSPHeaderLastModified( DateTime value )
        {
            Value = value;
        }



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.LastModified;
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
            return _value != DateTime.MinValue && _value != DateTime.MaxValue;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return RTSPDataConverter.ConvertToString( _value , RTSPDateTimeFormatType.GmtFormat );
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderLastModified result )
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            result = new RTSPHeaderLastModified()
            {
                Value = RTSPDataConverter.ConvertToDateTimeAsGMT( value )
            };

            return true;
        }
    }
}
