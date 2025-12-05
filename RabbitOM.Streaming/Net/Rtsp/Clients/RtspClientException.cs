using System;
using System.Runtime.Serialization;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent a client exception
    /// </summary>
    [Serializable] public class RtspClientException : Exception
    {
        private readonly RtspClientErrorCode _errorCode = RtspClientErrorCode.Unknown;

        /// <summary>
        /// Constructor
        /// </summary>
        public RtspClientException() 
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">the error code</param>
        /// <param name="message">the message</param>
        public RtspClientException( RtspClientErrorCode errorCode , string message ) 
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
        public RtspClientException( RtspClientErrorCode errorCode , string message , Exception inner ) 
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
        protected RtspClientException( RtspClientErrorCode errorCode , SerializationInfo info , StreamingContext context ) 
            : base( info , context ) 
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code
        /// </summary>
        public RtspClientErrorCode ErrorCode
        {
            get => _errorCode;
        }
    }
}
