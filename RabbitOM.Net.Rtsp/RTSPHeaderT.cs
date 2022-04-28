using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    /// <typeparam name="TValue">the value type</typeparam>
    public abstract class RTSPMessageHeader<TValue> : RTSPHeader
    {
        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public abstract TValue Value
        {
            get;
            set;
        }
    }
}
