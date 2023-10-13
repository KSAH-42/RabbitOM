using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPSetParameterInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPSetParameterInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethodType.Setup )
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
        /// Add a parameter
        /// </summary>
        /// <param name="parameters">the parameter</param>
        /// <returns></returns>
        public IRTSPInvoker AddParameters( IDictionary<string , string> parameters )
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
        public IRTSPInvoker AddParameter( string name , string value )
        {
            Builder.AddBodyParameter( name , value );

            return this;
        }
    }
}
