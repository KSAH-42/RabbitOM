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

        // TODO: add a try catch block

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            // to be tested , throw an exception to indicate that this code is not available

            result = null;

            if ( packets == null )
            {
                return false;
            }

            _imageBuilder.Clear();

            foreach ( RtpPacket packet in packets )
            {
                if ( ! JpegFragment.TryParse( packet.Payload , out JpegFragment fragment ) )
                {
                    return false; 
                }

                _imageBuilder.AddFragment( fragment );
            }

            if ( ! _imageBuilder.CanBuildFrame() )
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
