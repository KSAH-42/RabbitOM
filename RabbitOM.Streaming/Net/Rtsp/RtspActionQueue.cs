using RabbitOM.Streaming.Collections;
using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent an action queue
    /// </summary>
    public sealed class RtspActionQueue : ConcurrentQueue<Action>
    {
        /// <summary>
        /// Occurs during a custom validaton
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false</returns>
        protected override bool OnValidate( Action action )
        {
            return action != null;
        }
    }
}
