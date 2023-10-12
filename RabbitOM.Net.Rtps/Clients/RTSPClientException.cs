using System;
using System.Runtime.Serialization;

namespace RabbitOM.Net.Rtps.Clients
{
    /// <summary>
    /// Represent a client exception
    /// </summary>
    [Serializable] public class RTSPClientException : Exception
    {
        private readonly RTSPClientErrorCode _errorCode = RTSPClientErrorCode.Unknown;

        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPClientException() 
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">the error code</param>
        /// <param name="message">the message</param>
        public RTSPClientException( RTSPClientErrorCode errorCode , string message ) 
            : base( message ) 
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">the error code</param>
        /// <param name="message">the message</param>
        /// <param name="inner">the inner exception</param>
        public RTSPClientException( RTSPClientErrorCode errorCode , string message , Exception inner ) 
            : base( message , inner ) 
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">the error code</param>
        /// <param name="info">the info</param>
        /// <param name="context">the context</param>
        protected RTSPClientException( RTSPClientErrorCode errorCode , SerializationInfo info , StreamingContext context ) 
            : base( info , context ) 
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code
        /// </summary>
        public RTSPClientErrorCode ErrorCode
        {
            get => _errorCode;
        }
    }
}
