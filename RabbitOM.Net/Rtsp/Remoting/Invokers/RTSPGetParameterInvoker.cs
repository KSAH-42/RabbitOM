using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPGetParameterInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPGetParameterInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethod.GetParameter )
        {
        }

        /// <summary>
        /// Set the content type as text parameter
        /// </summary>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker SetContentType()
        {
            return SetContentType( RTSPMimeType.TextParameters );
        }

        /// <summary>
        /// Set the content type
        /// </summary>
        /// <param name="mimeType">the mime type</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker SetContentType( RTSPMimeType mimeType )
        {
            Builder.ContentType = mimeType?.ToString() ?? string.Empty;

            return this;
        }

        /// <summary>
        /// Add parameters
        /// </summary>
        /// <param name="parameters">the parameter names</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker AddParameters( params string[] parameters )
        {
            Builder.AddBodyParameters( parameters as IEnumerable<string> );

            return this;
        }

        /// <summary>
        /// Add parameters
        /// </summary>
        /// <param name="parameters">the parameter names</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker AddParameters( IEnumerable<string> parameters )
        {
            Builder.AddBodyParameters( parameters );

            return this;
        }

        /// <summary>
        /// Add a parameter
        /// </summary>
        /// <param name="name">the name of the parameter</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker AddParameter( string name )
        {
            Builder.AddBodyParameter( name );

            return this;
        }
    }
}
