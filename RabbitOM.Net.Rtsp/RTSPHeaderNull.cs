using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderNull : RTSPHeader
    {
        /// <summary>
        /// Represent a null object value
        /// </summary>
        public readonly static RTSPHeaderNull Value = new RTSPHeaderNull();



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => string.Empty;
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns false</returns>
        public override bool Validate()
        {
            return false;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
