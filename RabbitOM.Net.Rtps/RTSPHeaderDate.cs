using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderDate : RTSPMessageHeader<DateTime>
    {
        private DateTime _value = DateTime.MinValue;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderDate()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
        public RTSPHeaderDate( DateTime value )
        {
            Value = value;
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Date;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public override DateTime Value
        {
            get => _value;
            set => _value = value.ToUniversalTime();
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            if ( _value == DateTime.MinValue || _value == DateTime.MaxValue )
            {
                return false;
            }

            return true;
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
        public static bool TryParse( string value , out RTSPHeaderDate result )
        {
            result = new RTSPHeaderDate()
            {
                Value = RTSPDataConverter.ConvertToDateTimeAsGMT( value )
            };

            return true;
        }
    }
}
