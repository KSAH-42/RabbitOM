using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMessageReaderValidator : IMessageReaderValidator
    {
        public long? MaximumOfHeaders { get; set; }

        public long? MaximumOfHeaderLength { get; set; }

        public long? MaximumOfHeaderTotalLength { get; set; }

        public long? MaximumOfHeaderContentLength { get; set; }

        public long ActualHeaderTotalLength { get; private set; }





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

            if ( source.CountValues( RtspHeaderNames.ContentLength ) > 1 )
            {
                throw new ProtocolViolationException( "the collection must contains zero or only one single content-length header" );
            }

            var cseq = source.CSeq;

            if ( ! cseq.HasValue || cseq.Value < 0 )
            {
                throw new ProtocolViolationException( "cseq header is invalid" );
            }

            var maximumOfHeaderContentLength = MaximumOfHeaderContentLength;

            if ( maximumOfHeaderContentLength.HasValue )
            {
                var contentLength = source.ContentLength;

                if ( contentLength.HasValue && contentLength.Value > maximumOfHeaderContentLength.Value )
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
                ActualHeaderTotalLength += headerLength;
            }

            var maximumOfHeaderTotalLength = MaximumOfHeaderTotalLength;

            if ( maximumOfHeaderTotalLength.HasValue && maximumOfHeaderTotalLength < ActualHeaderTotalLength )
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

        public void Reset()
        {
            ActualHeaderTotalLength = 0;
        }
    }
}
