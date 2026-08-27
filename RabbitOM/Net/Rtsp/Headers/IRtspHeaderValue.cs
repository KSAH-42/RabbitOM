using System;

namespace RabbitOM.Net.Rtsp.Headers
{
    /// <summary>
    /// Represent the header value
    /// </summary>
    /// <typeparam name="TValue">the type of the value</typeparam>
    public interface IRtspHeaderValue<TValue>
    {
        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        TValue Value
        {
            get;
            set;
        }
    }
}
