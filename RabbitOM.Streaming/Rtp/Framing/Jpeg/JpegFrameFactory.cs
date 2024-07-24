using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegFrameBuilder _builder;
        private readonly JpegStreamWriter _stream;
        private readonly JpegImageBuilder _imageBuilder;

        public JpegFrameFactory( JpegFrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );
            _stream = new JpegStreamWriter();
            _imageBuilder = new JpegImageBuilder( _stream );
        }

        // a try catch block must be add added
        // and not tryMethod on the stream class because there is a loop
        // it is better to leave the loop when a failure happens inside it.

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            /// according to rtp mjpeg
            /// it is not possible to have sequence that containts multiple fragment with different width and height size
            /// we can create a optimization by storing the jpeg headers inside the class used to write fragments, it could save a lot time
            /// much more than the previous projects and from different existing projects.

            /*
             
            var rtpPackets = packets as RtpPacketQueue;

            builder.WriteInitialFragment( JpegFragment.Parse( rtpPackets.Dequeue().Payload() );

            for ( int i = 0 ; i < rtpPackets.Count - 1 ; ++ i )
            {
                builder.WriteFragment( JpegFragment.Parse( rtpPackets.Dequeue().Payload() );
            }

            builder.WriteLastFragment( JpegFragment.Parse( rtpPackets.Dequeue().Payload() );

            result = new RtpFrame( _imageBuilder.BuildImage() );
            
             */


            throw new NotImplementedException();
        }

        public void Clear()
        {
            _stream.Clear();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
