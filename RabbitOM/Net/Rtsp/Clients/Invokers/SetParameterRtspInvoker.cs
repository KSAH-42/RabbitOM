using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class SetParameterRtspInvoker : RtspInvoker
    {
        internal SetParameterRtspInvoker( RtspProxy proxy ) : base( proxy , RtspMethod.Setup )
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
