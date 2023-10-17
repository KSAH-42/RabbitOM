using System.Globalization;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderScale : RTSPMessageHeader<float>
    {
        private float _value = 0;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderScale()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
        public RTSPHeaderScale( float value )
        {
            Value = value;
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Scale;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public override float Value
        {
            get => _value;
            set => _value = value;
        }



        /// <summary>
        /// Validate
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
        public static bool TryParse( string value , out RTSPHeaderScale result )
        {
            result = new RTSPHeaderScale()
            {
                Value = RTSPDataConverter.ConvertToFloat( value , NumberStyles.Any , CultureInfo.InvariantCulture )
            };

            return true;
        }
    }
}
