using System;
using System.Net;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspHeaderCollectionVerifier
    {
        private readonly RtspHeaderCollection _collection;
        private long _actualTotalSize;


        public RtspHeaderCollectionVerifier( RtspHeaderCollection collection )
        {
            _collection = collection;
        }



        public long? MaximumOfHeaders { get; set; }

        public long? MaximumOfHeadersSize { get; set; }


        public void IncreaseTotalSize( int size )
        {
            if ( size < 0 )
            {
                throw new ArgumentException( nameof( size ) );
            }

            if ( MaximumOfHeadersSize.HasValue )
            {
                checked
                {
                    _actualTotalSize += size;
                }

                if ( _actualTotalSize < MaximumOfHeaders.Value )
                {
                    throw new ProtocolViolationException( "the size has exeed" );
                }
            }
        }

        public void EnsureNoIllegalDuplication()
        {
            if ( _collection.CountValues( RtspHeaderNames.CSeq ) > 1 )
            {
                throw new ProtocolViolationException( $"the header CSeq is present at multiple times that can cause security issues" );
            }

            if ( _collection.CountValues( RtspHeaderNames.ContentLength ) > 1 )
            {
                throw new ProtocolViolationException( $"the header Content-Length is present at multiple times that can cause security issues" );
            }
        }


        public void EnsureMaximumOfHeaders()
        {
            if ( MaximumOfHeaders.HasValue && MaximumOfHeaders < _collection.Count )
            {
                throw new ProtocolViolationException( $"too many headers {_collection.Count} that can cause security issues" );
            }
        }
    }
}
