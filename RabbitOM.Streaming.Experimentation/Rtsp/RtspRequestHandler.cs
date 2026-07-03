/* The With method has been introduce for writing code like this,
    * for creating a request pipeline, that can include 
    * CSeq enricher, 
    * Add Autorization header 
    * Adding extra header
    * Custom Header encryptions
    * Custom body encryption

    before the final stage 

    var handler = new CustomRequestHandler() { Name = "Handler1" }
    .With( new CustomRequestHandler() { Name = "Handler2"} )
    .With( new CustomRequestHandler() { Name = "Handler3"} )
    .With( new CustomRequestHandler() { Name = "Handler4"} )
    .With( new CustomRequestHandler() { Name = "Handler5"} )
    .With( new CustomRequestHandler() { Name = "Sending"} );

    handler.SendRequest( new Request() );
        
Output:

    Handler1
    Handler2
    Handler3
    Handler4
    Handler5
    Sending

    Sending
    Handler5
    Handler4
    Handler3
    Handler2
    Handler1
*/
using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public abstract class RtspRequestHandler
    {
        private RtspRequestHandler _next;

        public virtual RtspClientResponse SendRequest( RtspContext context , RtspClientRequest request )
        {
            if ( _next == null )
            {
                throw new InvalidOperationException( "the next next can not be null" );
            }

            return _next.SendRequest( context , request ) ?? throw new InvalidOperationException( "no response available" );
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
                last = last._next;
            }

            last._next = next;

            return this;
        }
    }
}
