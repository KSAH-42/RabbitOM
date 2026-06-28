using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMessageReaderGuard
    {
        private readonly RtspMessageReaderGuardSettings _settings;

        private readonly RtspMessageHeaderCollection _collection;

        private long _totalHeadersLength;






        public RtspMessageReaderGuard( RtspMessageReaderGuardSettings settings , RtspMessageHeaderCollection collection )
        {
            _settings   = settings   ?? throw new ArgumentNullException( nameof( settings ) );
            _collection = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }





        public void EnsureCSeq()
        {
            var header = _collection.CSeq;

            if ( ! header.HasValue || header.Value < 0 )
            {
                throw new ProtocolViolationException( "the cseq header is not present or contains incorrect value" );
            }
        }

        public void CheckHeadersViolations( string header )
        {
            if ( string.IsNullOrEmpty( header ) )
            {
                throw new ProtocolViolationException( "empty value as header is not allowed" );
            }

            // increase the size even if the header is not accepted
            // we to need if the flow of data is too big
            checked
            {
                _totalHeadersLength += header.Length;
            }

            if ( _settings.TotalHeaderLength.HasValue && _settings.TotalHeaderLength.Value < _totalHeadersLength )
            {
                throw new ProtocolViolationException( "the total allowed header length has exceed" );
            }

            if ( _settings.HeadersCountLimit.HasValue && _settings.HeadersCountLimit < _collection.Count )
            {
                throw new ProtocolViolationException( $"too many headers {_collection.Count} that can cause security issues" );
            }

            if ( _collection.CountValues( RtspHeaderNames.CSeq ) > 1 )
            {
                throw new ProtocolViolationException( "the header CSeq is present at multiple times" );
            }

            if ( _collection.CountValues( RtspHeaderNames.ContentLength ) > 1 )
            {
                throw new ProtocolViolationException( "the header Content-Length is present at multiple times" );
            }

            var contentLength = _collection.ContentLength;

            if (  contentLength.HasValue && contentLength.Value > _settings.ContentLengthLimit.Value )
            {
                throw new ProtocolViolationException( $"the Content-Length value ({contentLength.Value}) as exceed the limit" );
            }
        }
    }
}
