using System;
using System.Net;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Readers
{
    using RabbitOM.Streaming.RtspV2.Headers;

    public sealed class RtspMessageReaderValidator
    {
        private readonly RtspMessageReaderValidatorOptions _options;

        private readonly RtspMessageHeaderCollection _headers;

        private long _actualTotalHeadersLength;




        public RtspMessageReaderValidator( RtspMessageReaderValidatorOptions options , RtspMessageHeaderCollection headers )
        {
            _options = options ?? throw new ArgumentNullException( nameof( options ) );
            _headers = headers ?? throw new ArgumentNullException( nameof( headers ) );
        }




        public void Validate()
        {
           if ( _headers.CountValues( RtspHeaderNames.CSeq ) != 1 )
            {
                throw new ProtocolViolationException( "the collection must contains only one instance cseq header" );
            }

            var cseq = _headers.CSeq;

            if ( ! cseq.HasValue || cseq.Value < 0 )
            {
                throw new ProtocolViolationException( "cseq header is invalid" );
            }

            if ( _headers.CountValues( RtspHeaderNames.ContentLength ) > 1 )
            {
                throw new ProtocolViolationException( "the collection must contains zero or only one single content-length header" );
            }

            var contentLength = _headers.ContentLength;

            if ( contentLength.HasValue )
            {
                if ( contentLength.Value < 0 )
                {
                    throw new ProtocolViolationException( "invalid content-length value" );
                }

                var maximumOfHeaderContentLength = _options.MaximumOfContentLengthValue;

                if ( maximumOfHeaderContentLength.HasValue && maximumOfHeaderContentLength.Value < contentLength.Value )
                {
                    throw new ProtocolViolationException( "content-length value is too big" );
                }
            }
        }

        public void Validate( string header )
        {
            var headerLength = header?.Length ?? 0;

            checked
            {
                _actualTotalHeadersLength += headerLength;
            }

            var maximumOfHeaderTotalLength = _options.MaximumOfHeadersTotalLength;

            if ( maximumOfHeaderTotalLength.HasValue && maximumOfHeaderTotalLength < _actualTotalHeadersLength )
            {
                throw new ProtocolViolationException( "the total size of headers has exceed" );
            }

            var maximumOfHeaderLength = _options.MaximumOfHeaderLength;

            if ( maximumOfHeaderLength.HasValue && maximumOfHeaderLength < headerLength )
            {
                throw new ProtocolViolationException( "the header length is too big" );
            }

            var maximumOfHeaders = _options.MaximumOfHeaders;

            if ( maximumOfHeaders.HasValue && maximumOfHeaders.Value < _headers.Count )
            {
                throw new ProtocolViolationException( "too many headers present on the collection" );
            }
        }
    }
}
