using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderCSeq : RTSPMessageHeader<long>
    {
        private long _value = 0;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderCSeq()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
        public RTSPHeaderCSeq( long value )
        {
            Value = value;
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.CSeq;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public override long Value
        {
            get => _value;
            set => _value = value;
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            return _value >= 0;
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
        public static bool TryParse( string value , out RTSPHeaderCSeq result )
        {
            result = new RTSPHeaderCSeq()
            {
                Value = RTSPDataConverter.ConvertToLong( value )
            };

            return true;
        }
    }
}
