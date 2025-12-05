using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderNull : RtspHeader
    {
        /// <summary>
        /// Represent a null object value
        /// </summary>
        public readonly static RtspHeaderNull Value = new RtspHeaderNull();






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => string.Empty;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns false</returns>
        public override bool TryValidate()
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
