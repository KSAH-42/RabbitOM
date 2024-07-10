using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspSetParameterInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspSetParameterInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.Setup )
        {
        }

        /// <summary>
        /// Set the content type as text parameter
        /// </summary>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker SetContentType()
        {
            return SetContentType( RtspMimeType.TextParameters );
        }

        /// <summary>
        /// Set the content type
        /// </summary>
        /// <param name="mimeType">the mime type</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker SetContentType( RtspMimeType mimeType )
        {
            Builder.ContentType = mimeType?.ToString() ?? string.Empty;

            return this;
        }

        /// <summary>
        /// Add a parameter
        /// </summary>
        /// <param name="parameters">the parameter</param>
        /// <returns></returns>
        public IRtspInvoker AddParameters( IDictionary<string , string> parameters )
        {
            Builder.WriteBodyParameters( parameters );

            return this;
        }

        /// <summary>
        /// Add a parameter
        /// </summary>
        /// <param name="name">the parameter name</param>
        /// <param name="value">the parameter value</param>
        /// <returns></returns>
        public IRtspInvoker AddParameter( string name , string value )
        {
            Builder.AddBodyParameter( name , value );

            return this;
        }
    }
}
