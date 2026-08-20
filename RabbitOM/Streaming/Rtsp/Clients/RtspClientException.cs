using System;
using System.Runtime.Serialization;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    [Serializable] public class RtspClientException : Exception
    {
        private readonly RtspClientErrorCode _errorCode = RtspClientErrorCode.Unknown;

        public RtspClientException()
        {
        }

        public RtspClientException( RtspClientErrorCode errorCode , string message )
            : base( message )
        {
            _errorCode = errorCode;
        }

        public RtspClientException( RtspClientErrorCode errorCode , string message , Exception inner )
            : base( message , inner )
        {
            _errorCode = errorCode;
        }

        protected RtspClientException( RtspClientErrorCode errorCode , SerializationInfo info , StreamingContext context )
            : base( info , context )
        {
            _errorCode = errorCode;
        }

        public RtspClientErrorCode ErrorCode
        {
            get => _errorCode;
        }
    }
}
