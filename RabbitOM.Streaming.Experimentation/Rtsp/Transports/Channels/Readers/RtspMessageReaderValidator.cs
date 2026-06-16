using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMessageReaderValidator
    {
        private readonly RtspMessageReaderValidatorSettings _settings;

        private readonly RtspMessageHeaderCollection _collection;

        private long _cumulatedHeaderSize;






        public RtspMessageReaderValidator( RtspMessageReaderValidatorSettings settings , RtspMessageHeaderCollection collection )
        {
            _settings   = settings   ?? throw new ArgumentNullException( nameof( settings ) );
            _collection = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }





        public void Validate( string header )
        {
            if ( string.IsNullOrEmpty( header ) )
            {
                throw new ProtocolViolationException( "empty value as header is not allowed" );
            }

            checked
            {
                _cumulatedHeaderSize += header.Length;
            }

            if ( _settings.TotalHeadersSize.HasValue && _cumulatedHeaderSize < _settings.TotalHeadersSize.Value )
            {
                throw new ProtocolViolationException( "the size has exceed" );
            }

            if ( _settings.MaximumOfHeaders.HasValue && _settings.MaximumOfHeaders < _collection.Count )
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

            if ( _settings.ContentLengthLimit.HasValue )
            {
                var contentLength = _collection.ContentLength;

                if ( contentLength.HasValue && contentLength.Value > _settings.ContentLengthLimit.Value )
                {
                    throw new ProtocolViolationException( $"the Content-Length value ({contentLength.Value}) as exceed the limit" );
                }
            }
        }
    }
}
