using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMessageRequestWriterValidator : IMessageWriterValidator<RtspRequestMessage>
    {
        public void ValidateMessage( RtspRequestMessage request )
        {
            if ( request == null )
            {
                throw new ArgumentNullException( nameof( request ) );
            }

            var requestLine = request.RequestLine;

            if ( requestLine == null )
            {
                throw new ArgumentException( "no request line" , nameof( request ) );
            }

            if ( string.IsNullOrEmpty( requestLine.Method ) )
            {
                throw new ArgumentException( "no method" , nameof( request ) );
            }

            if ( string.IsNullOrEmpty( requestLine.Uri ) )
            {
                throw new ArgumentException( "no uri" , nameof( request ) );
            }

            if ( string.IsNullOrEmpty( requestLine.Protocol ) )
            {
                throw new ArgumentException( "no protocol name" , nameof( request ) );
            }

            if ( string.IsNullOrEmpty( requestLine.Version ) )
            {
                throw new ArgumentException( "no version" , nameof( request ) );
            }

            var headers = request.Headers;

            if ( headers == null || headers.Count <= 0 )
            {
                throw new ArgumentException( "no headers provided" , nameof( request ) );
            }

            if ( headers.CountValues( RtspHeaderNames.CSeq ) != 1 )
            {
                throw new ArgumentException( "headers must contains a single instance of cseq header" , nameof( request ) );
            }

            var cseq = headers.CSeq;

            if ( ! cseq.HasValue || cseq.Value < 0 )
            {
                throw new ArgumentException( "cseq header not found or it'has an negative value" , nameof( request ) );
            }

            if ( headers.CountValues( RtspHeaderNames.ContentLength ) > 1 )
            {
                throw new ArgumentException( "duplicated content-length headers found" , nameof( request ) );
            }

            var contentLength = request.Headers.ContentLength.HasValue ? request.Headers.ContentLength.Value : 0;
            var bodyLength    = request.Body?.Length ?? 0;

            if ( bodyLength != contentLength || bodyLength < 0 || contentLength < 0 )
            {
                throw new ArgumentException( "invadid body size or content length is different from the size of body" , nameof( request ) );
            }
        }
    }
}
