﻿using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the header value
    /// </summary>
    /// <typeparam name="TValue">the type of the value</typeparam>
    public interface IRTSPHeaderValue<TValue>
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
