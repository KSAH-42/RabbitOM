using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspRequestMessageValidator
    {
        public void ValidateRequest( RtspRequestMessage request )
        {
            if ( request == null )
            {
                throw new ArgumentNullException( nameof( request ) );
            }

            var requestLine = request.RequestLine;

            if ( requestLine == null )
            {
                throw new ArgumentException( nameof( request ) , "no request line");
            }

            if ( string.IsNullOrEmpty( requestLine.Method ) )
            {
                throw new ArgumentException( nameof( request ) , "no method" );
            }

            if ( string.IsNullOrEmpty( requestLine.Uri ) )
            {
                throw new ArgumentException( nameof( request ) , "no uri" );
            }

            if ( string.IsNullOrEmpty( requestLine.Protocol ) )
            {
                throw new ArgumentException( nameof( request ) , "no protocol name");
            }

            if ( string.IsNullOrEmpty( requestLine.Version ) )
            {
                throw new ArgumentException( nameof( request ) , "no version" );
            }

            var headers = request.Headers;

            if ( headers == null || headers.Count <= 0 )
            {
                throw new ArgumentException( nameof( request ) , "no headers provided" );
            }

            if ( ! headers.CSeq.HasValue || headers.CSeq.Value < 0 )
            {
                throw new ArgumentException( nameof( request ) , "cseq header not found or it'has an negative value");
            }

            var contentLength = request.Headers.ContentLength.HasValue ? request.Headers.ContentLength.Value : 0;
            var bodyLength    = request.Body?.Length ?? 0;

            if ( bodyLength != contentLength || bodyLength < 0 || contentLength < 0 )
            {
                throw new ArgumentException( nameof( request ) , "invadid body size or content length is different from the size of body" );
            }
        }
    }
}
