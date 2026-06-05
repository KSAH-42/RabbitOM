using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    internal sealed class RtspMessageValidator
    {
        public void ValidateMessage( RtspInterleavedMessage message )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( nameof( message ) );
            }

            // according to the rfc, the length field can be equal to zero
            // the following sequence is also valid:

            // 0x24 0x00 0x00 0x00
            // 0x24 0x00 0x00 0x01 0x01
            // 0x24 0x00 0x00 0x02 0x01 0x10

            var bufferLength = message.Buffer?.Length ?? 0;

            if ( bufferLength != message.Length )
            {
                throw new ArgumentException( nameof( message ) , "invalid buffer size" );
            }
        }

        public void ValidateMessage( RtspRequestMessage request )
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
