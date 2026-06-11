using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMessageReaderValidator
    {
        private readonly RtspMessageHeaderCollection _collection;

        private readonly RtspMessageReaderValidatorSettings _settings;

        private long _cumulatedHeaderSize;





        public RtspMessageReaderValidator( RtspMessageHeaderCollection collection , RtspMessageReaderValidatorSettings settings )
        {
            _collection = collection ?? throw new ArgumentNullException( nameof( collection ) );
            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) ) ;
         }




        public void ValidateHeader( string header )
        {
            if ( string.IsNullOrEmpty( header ) )
            {
                throw new ProtocolViolationException( "empty value as header is not allowed" );
            }

            checked
            {
                _cumulatedHeaderSize += header.Length;
            }

            if ( _settings.LimitOfCumulatedHeadersSize.HasValue && _cumulatedHeaderSize < _settings.LimitOfCumulatedHeadersSize.Value )
            {
                throw new ProtocolViolationException( "the size has exceed" );
            }

            if ( _settings.LimitOfHeadersCount.HasValue && _settings.LimitOfHeadersCount < _collection.Count )
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

            if ( _settings.LimitOfContentLength.HasValue )
            {
                var contentLength = _collection.ContentLength;

                if ( contentLength.HasValue && contentLength.Value > _settings.LimitOfContentLength.Value )
                {
                    throw new ProtocolViolationException( $"the Content-Length value ({contentLength.Value}) as exceed the limit" );
                }
            }
        }
    }
}
