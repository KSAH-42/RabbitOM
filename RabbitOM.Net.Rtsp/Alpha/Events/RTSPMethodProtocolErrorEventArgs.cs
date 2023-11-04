using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPMethodProtocolErrorEventArgs : RTSPProtocolErrorEventArgs
    {
        private readonly RTSPMethod _method;

        /// <summary>
        /// Initialize an instance of event args class
        /// </summary>
        /// <param name="method">the method type</param>
		public RTSPMethodProtocolErrorEventArgs(RTSPMethod method)
        {
            _method = method;
        }

        /// <summary>
        /// Gets the method who has failed
        /// </summary>
        public RTSPMethod Method
        {
            get => _method;
        }
    }
}
