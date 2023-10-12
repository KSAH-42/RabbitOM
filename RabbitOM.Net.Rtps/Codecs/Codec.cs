using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent the base codec info class
    /// </summary>
    public abstract class Codec
    {
        /// <summary>
        /// Gets the type
        /// </summary>
        public abstract CodecType Type
        {
            get;
        }

        /// <summary>
        /// Perform a validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool Validate();
    }
}
