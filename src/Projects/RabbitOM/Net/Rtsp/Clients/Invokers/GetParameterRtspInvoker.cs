using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Clients
{
    public sealed class GetParameterRtspInvoker : RtspInvoker
    {
        internal GetParameterRtspInvoker( RtspProxy proxy )
            : base( proxy , RtspMethod.GetParameter )
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

        public IRtspInvoker AddParameters( params string[] parameters )
        {
            Builder.AddBodyParameters( parameters as IEnumerable<string> );

            return this;
        }

        public IRtspInvoker AddParameters( IEnumerable<string> parameters )
        {
            Builder.AddBodyParameters( parameters );

            return this;
        }

        public IRtspInvoker AddParameter( string name )
        {
            Builder.AddBodyParameter( name );

            return this;
        }
    }
}
