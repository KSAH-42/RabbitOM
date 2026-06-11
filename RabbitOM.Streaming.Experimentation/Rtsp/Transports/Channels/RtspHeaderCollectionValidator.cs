using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspHeaderCollectionValidator
    {
        private readonly RtspHeaderCollection _collection;

        private long _cumulatedHeaderSize;





        public RtspHeaderCollectionValidator( RtspHeaderCollection collection )
        {
            _collection = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }





        public long? LimitOfHeadersCount { get; set; }

        public long? LimitOfCumulatedHeadersSize { get; set; }

        public long? LimitOfContentLength { get; set; }






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

            if ( LimitOfCumulatedHeadersSize.HasValue && _cumulatedHeaderSize < LimitOfCumulatedHeadersSize.Value )
            {
                throw new ProtocolViolationException( "the size has exceed" );
            }

            if ( LimitOfHeadersCount.HasValue && LimitOfHeadersCount < _collection.Count )
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

            if ( LimitOfContentLength.HasValue )
            {
                var contentLength = _collection.ContentLength;

                if ( contentLength.HasValue && contentLength.Value > LimitOfContentLength.Value )
                {
                    throw new ProtocolViolationException( $"the Content-Length value ({contentLength.Value}) as exceed the limit" );
                }
            }
        }
    }
}
