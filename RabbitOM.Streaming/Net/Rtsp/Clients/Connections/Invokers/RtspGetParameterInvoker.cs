using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RtspGetParameterInvoker : RtspInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RtspGetParameterInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.GetParameter )
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
        /// Add parameters
        /// </summary>
        /// <param name="parameters">the parameter names</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker AddParameters( params string[] parameters )
        {
            Builder.AddBodyParameters( parameters as IEnumerable<string> );

            return this;
        }

        /// <summary>
        /// Add parameters
        /// </summary>
        /// <param name="parameters">the parameter names</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker AddParameters( IEnumerable<string> parameters )
        {
            Builder.AddBodyParameters( parameters );

            return this;
        }

        /// <summary>
        /// Add a parameter
        /// </summary>
        /// <param name="name">the name of the parameter</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker AddParameter( string name )
        {
            Builder.AddBodyParameter( name );

            return this;
        }
    }
}
