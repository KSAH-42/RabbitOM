using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the invoker result
    /// </summary>
    public sealed class RtspInvokerResultRequest
    {
        private readonly RtspMessageRequest _message = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">the message request</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspInvokerResultRequest( RtspMessageRequest message )
        {
            _message = message ?? throw new ArgumentNullException( nameof( message ) );
        }




        /// <summary>
        /// Gets the message
        /// </summary>
        public RtspMessageRequest Message
        {
            get => _message;
        }




        /// <summary>
        /// Gets the method
        /// </summary>
        /// <returns>return the value</returns>
        public RtspMethod GetMethod()
        {
            return _message.Method;
        }

        /// <summary>
        /// Gets the header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <returns>returns a header instance</returns>
        public RtspHeader GetHeader( string name )
        {
            return _message.Headers.GetByName( name );
        }

        /// <summary>
        /// Gets the header
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <returns>returns a header instance, otherwise null</returns>
        public THeader GetHeader<THeader>() where THeader : RtspHeader
        {
            return _message.Headers.Find<THeader>();
        }

        /// <summary>
        /// Gets the header
        /// </summary>
        /// <typeparam name="THeader">the type of the header</typeparam>
        /// <param name="name">the header name</param>
        /// <returns>returns a header instance, otherwise null</returns>
        public THeader GetHeader<THeader>( string name ) where THeader : RtspHeader
        {
            return _message.Headers.FindByName<THeader>( name );
        }

        /// <summary>
        /// Gets the sequence number
        /// </summary>
        /// <returns>returns a value</returns>
        public long GetHeaderCSeq()
        {
            return _message.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq )?.Value ?? 0;
        }

        /// <summary>
        /// Gets the session identifier
        /// </summary>
        /// <returns>returns a header instance</returns>
        public string GetHeaderSessionId()
        {
            return _message.Headers.FindByName<RtspHeaderSession>( RtspHeaderNames.Session )?.Number ?? string.Empty;
        }

        /// <summary>
        /// Gets the header content type
        /// </summary>
        /// <returns>returns a value</returns>
        public string GetHeaderContentType()
        {
            return _message.Headers.FindByName<RtspHeaderContentType>( RtspHeaderNames.ContentType )?.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets the header content length
        /// </summary>
        /// <returns>returns a value</returns>
        public long GetHeaderContentLength()
        {
            return _message.Headers.FindByName<RtspHeaderContentLength>( RtspHeaderNames.ContentLength )?.Value ?? 0;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        /// <returns>retruns a body</returns>
        public string GetBody()
        {
            return _message.Body.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        /// <returns>retruns a body</returns>
        public int GetBodyLength()
        {
            return _message.Body.Value?.Length ?? 0;
        }
    }
}
