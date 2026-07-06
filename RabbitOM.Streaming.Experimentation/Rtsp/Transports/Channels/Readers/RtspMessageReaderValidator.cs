using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMessageReaderValidator : IMessageReaderValidator
    {
        public long? MaximumOfHeaders { get; set; }

        public long? MaximumOfHeadersTotalLength { get; set; }

        public long? MaximumOfHeaderLength { get; set; }

        public long? MaximumOfContentLengthValue { get; set; }

        public long ActualTotalHeadersLength { get; private set; }





        public void Validate( RtspMessageHeaderCollection source )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            if ( source.CountValues( RtspHeaderNames.CSeq ) != 1 )
            {
                throw new ProtocolViolationException( "the collection must contains only one instance cseq header" );
            }

            var cseq = source.CSeq;

            if ( ! cseq.HasValue || cseq.Value < 0 )
            {
                throw new ProtocolViolationException( "cseq header is invalid" );
            }

            if ( source.CountValues( RtspHeaderNames.ContentLength ) > 1 )
            {
                throw new ProtocolViolationException( "the collection must contains zero or only one single content-length header" );
            }

            var contentLength = source.ContentLength;

            if ( contentLength.HasValue )
            {
                if ( contentLength.Value < 0 )
                {
                    throw new ProtocolViolationException( "invalid content-length value" );
                }

                var maximumOfHeaderContentLength = MaximumOfContentLengthValue;

                if ( maximumOfHeaderContentLength.HasValue && maximumOfHeaderContentLength.Value < contentLength.Value )
                {
                    throw new ProtocolViolationException( "content-length value is too big" );
                }
            }
        }

        public void Validate( RtspMessageHeaderCollection source , string header )
        {
            if ( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            var headerLength = header?.Length ?? 0;

            checked
            {
                ActualTotalHeadersLength += headerLength;
            }

            var maximumOfHeaderTotalLength = MaximumOfHeadersTotalLength;

            if ( maximumOfHeaderTotalLength.HasValue && maximumOfHeaderTotalLength < ActualTotalHeadersLength )
            {
                throw new ProtocolViolationException( "the total size of headers has exceed" );
            }

            var maximumOfHeaderLength = MaximumOfHeaderLength;

            if ( maximumOfHeaderLength.HasValue && maximumOfHeaderLength < headerLength )
            {
                throw new ProtocolViolationException( "the header length is too big" );
            }

            var maximumOfHeaders = MaximumOfHeaders;

            if ( maximumOfHeaders.HasValue && maximumOfHeaders.Value < source.Count )
            {
                throw new ProtocolViolationException( "too many headers present on the collection" );
            }
        }

        public void Setup()
        {
            ActualTotalHeadersLength = 0;
        }
    }
}
