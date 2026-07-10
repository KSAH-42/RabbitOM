using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public abstract class RtspRequestHandler
    {
        private RtspRequestHandler _next;

        public virtual async Task<RtspResponse> SendRequestAsync( RtspRequest request , CancellationToken cancellation )
        {
            if ( _next == null )
            {
                return await Task.FromResult<RtspResponse>( null );
            }

            return await _next.SendRequestAsync( request , cancellation ) ;
        }

        public RtspRequestHandler With( RtspRequestHandler next )
        {
            if ( next == null )
            {
                throw new ArgumentNullException( nameof( next ) );
            }

            if ( object.ReferenceEquals( this , next ) )
            {
                throw new ArgumentException( nameof( next ) );
            }

            var last = this;

            while ( last._next != null )
            {
                if ( object.ReferenceEquals( last._next , next ) )
                {
                    throw new ArgumentException( "Circular dependency detected" , nameof( next ) );
                }

                last = last._next;
            }

            last._next = next;

            return this;
        }
    }
}
