using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public class Binding
    {
        public Binding( int receiveRetries, int receiveBufferSize , int sendBufferSize , TimeSpan receiveTimeout , TimeSpan sendTimeout , TimeSpan openTimeout , TimeSpan closeTimeout )
        {
            // TODO: at the end of the experimetation impl, don't forget to throw args exceptions
            // TODO: in case if the number of properties goes up, think to use nested builder class and remove the constructor

            ReceiveRetries = receiveRetries;
            ReceiveBufferSize = receiveBufferSize;
            SendBufferSize = sendBufferSize;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
            OpenTimeout = openTimeout;
            CloseTimeout = closeTimeout;
        }

        public int ReceiveRetries { get; } // number of retries during socket read timeout exception

        public int ReceiveBufferSize { get; }

        public int SendBufferSize { get; }

        public TimeSpan ReceiveTimeout { get; }

        public TimeSpan SendTimeout { get; }

        public TimeSpan OpenTimeout { get; }

        /// <summary>
        /// Gets the timeout used to wait underlaying threads before to abort threads and closing the sockets at the end
        /// </summary>
        public TimeSpan CloseTimeout { get; }
    }
}
