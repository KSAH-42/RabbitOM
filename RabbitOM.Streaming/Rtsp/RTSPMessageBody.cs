using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the message body
    /// </summary>
    public sealed class RTSPMessageBody
    {
        private string _value = string.Empty;

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Value
        {
            get => _value;
            set => _value = value ?? string.Empty; // Don't trim the value
        }

        /// <summary>
        /// Check if body is not empty
        /// </summary>
        public bool HasValue
        {
            get => !string.IsNullOrWhiteSpace( _value );
        }

        /// <summary>
        /// Returns the content of the body
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return _value;
        }
    }
}
