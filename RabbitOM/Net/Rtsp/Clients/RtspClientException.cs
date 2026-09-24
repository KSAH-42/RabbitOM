using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public class RtspClientException : Exception
    {
		public RtspClientException(RtspClientErrorCode errorCode ) : base()
		{
			ErrorCode = errorCode;
		}

		public RtspClientException( RtspClientErrorCode errorCode , string message ) : base( message )
		{
			ErrorCode = errorCode;
		}

		public RtspClientException( RtspClientErrorCode errorCode , string message , Exception innerException ) : base( message , innerException )
		{
			ErrorCode = errorCode;
		}

		public RtspClientErrorCode ErrorCode
        {
            get;
        }
    }
}
