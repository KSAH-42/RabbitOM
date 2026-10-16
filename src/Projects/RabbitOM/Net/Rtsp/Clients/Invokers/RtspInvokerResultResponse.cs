using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    using RabbitOM.Net.Rtsp.Headers;

    public sealed class RtspInvokerResultResponse
    {
        private readonly RtspMessageResponse _message;


        public RtspInvokerResultResponse( RtspMessageResponse message )
        {
            _message = message ?? throw new ArgumentNullException( nameof( message ) );
        }


        public RtspMessageResponse Message
        {
            get => _message;
        }


        public RtspStatusCode GetStatusCode()
        {
            return _message.Status.Code;
        }

        public RtspHeader GetHeader( string name )
        {
            return _message.Headers.GetByName( name );
        }

        public THeader GetHeader<THeader>() where THeader : RtspHeader
        {
            return _message.Headers.Find<THeader>();
        }

        public THeader GetHeader<THeader>( string name ) where THeader : RtspHeader
        {
            return _message.Headers.FindByName<THeader>( name );
        }

        public long GetHeaderCSeq()
        {
            return _message.Headers.FindByName<CSeqRtspHeader>( RtspHeaderNames.CSeq )?.Value ?? 0;
        }

        public string GetHeaderSessionId()
        {
            return _message.Headers.FindByName<SessionRtspHeader>( RtspHeaderNames.Session )?.Number ?? string.Empty;
        }

        public long GetHeaderSessionTimeout()
        {
            return _message.Headers.FindByName<SessionRtspHeader>( RtspHeaderNames.Session )?.Timeout ?? 0;
        }

        public RtspMethodList GetHeaderPublicOptions()
        {
            return _message.Headers.FindByName<PublicRtspHeader>( RtspHeaderNames.Public )?.Methods ?? new RtspMethodList();
        }

        public string GetHeaderContentType()
        {
            return _message.Headers.FindByName<ContentTypeRtspHeader>( RtspHeaderNames.ContentType )?.Value ?? string.Empty;
        }

        public long GetHeaderContentLength()
        {
            return _message.Headers.FindByName<ContentLengthRtspHeader>( RtspHeaderNames.ContentLength )?.Value ?? 0;
        }

        public string GetBody()
        {
            return _message.Body.Value ?? string.Empty;
        }

        public int GetBodyLength()
        {
            return _message.Body.Value?.Length ?? 0;
        }
    }
}
