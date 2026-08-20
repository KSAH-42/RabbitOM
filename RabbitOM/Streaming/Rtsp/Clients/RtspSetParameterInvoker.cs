using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspSetParameterInvoker : RtspInvoker
    {
        internal RtspSetParameterInvoker( RtspConnector proxy ) : base( proxy , RtspMethod.Setup )
        {
        }

        public IRtspInvoker SetContentType()
        {
            return SetContentType( RtspMimeType.TextParameters );
        }

        public IRtspInvoker SetContentType( RtspMimeType mimeType )
        {
            Builder.ContentType = mimeType?.ToString() ?? string.Empty;

            return this;
        }

        public IRtspInvoker AddParameters( IDictionary<string , string> parameters )
        {
            Builder.WriteBodyParameters( parameters );

            return this;
        }

        public IRtspInvoker AddParameter( string name , string value )
        {
            Builder.AddBodyParameter( name , value );

            return this;
        }
    }
}
