using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent an event args
    /// </summary>
    public class RTSPMethodProtocolErrorEventArgs : RTSPProtocolErrorEventArgs
    {
        private readonly RTSPMethodType _methodType;

        /// <summary>
        /// Initialize an instance of event args class
        /// </summary>
        /// <param name="methodType">the method type</param>
		public RTSPMethodProtocolErrorEventArgs(RTSPMethodType methodType)
        {
            _methodType = methodType;
        }

        /// <summary>
        /// Gets the method type
        /// </summary>
        public RTSPMethodType MethodType
        {
            get => _methodType;
        }
    }
}
