using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an action queue
    /// </summary>
    public sealed class RtspActionQueue : RtspQueue<Action>
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
