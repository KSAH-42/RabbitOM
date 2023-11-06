using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderConnection : RTSPHeader , IRTSPHeaderValue<string>
    {
        private string _value = string.Empty;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderConnection()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
        public RTSPHeaderConnection( string value )
        {
            Value = value;
        }



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Connection;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Value
        {
            get => _value;
            set => _value = RTSPDataFilter.Trim( value );
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace( _value );
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
        public static bool TryParse( string value , out RTSPHeaderConnection result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace(value) )
            {
                return false;
            }

            result = new RTSPHeaderConnection()
            {
                Value = value
            };

            return true;
        }
    }
}
