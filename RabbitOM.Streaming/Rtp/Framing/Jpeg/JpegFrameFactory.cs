using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder;

        public JpegFrameFactory()
        {
            _imageBuilder = new JpegImageBuilder();
        }

        // a try catch block must be add added
        // and not tryMethod on the stream class because there is a loop
        // it is better to leave the loop when a failure happens inside it.

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            // to be tested , throw an exception to indicate that this code is not available

            throw new NotImplementedException();

            result = null;

            if ( packets == null )
            {
                return false;
            }

            _imageBuilder.Clear();

            foreach ( RtpPacket packet in packets )
            {
                if ( JpegFragment.TryParse( packet.Payload , out JpegFragment fragment ) )
                {
                    _imageBuilder.AddFragment( fragment );
                }                    
            }

            if ( ! _imageBuilder.CanBuild() )
            {
                return false;
            }

            result = new RtpFrame( _imageBuilder.BuildFrame() );

            return true;
        }

        public void Clear()
        {
            _imageBuilder.Clear();
        }

        public void Dispose()
        {
            _imageBuilder.Dispose();
        }
    }
}
