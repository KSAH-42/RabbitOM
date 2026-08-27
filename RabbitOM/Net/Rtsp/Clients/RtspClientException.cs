using System;
using System.Runtime.Serialization;

namespace RabbitOM.Net.Rtsp.Clients
{
    [Serializable] public class RtspClientException : Exception
    {
        public RtspClientException( RtspClientErrorCode errorCode , string message )
            : base( message )
        {
            ErrorCode = errorCode;
        }

        public RtspClientException( RtspClientErrorCode errorCode , string message , Exception inner )
            : base( message , inner )
        {
            ErrorCode = errorCode;
        }

        protected RtspClientException( RtspClientErrorCode errorCode , SerializationInfo info , StreamingContext context )
            : base( info , context )
        {
            ErrorCode = errorCode;
        }

        public RtspClientErrorCode ErrorCode
        {
            get;
        }
    }
}
